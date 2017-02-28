/*
   Copyright 2015 Shane Lillie

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core;
using EnergonSoftware.BackpackPlanner.Core.HockeyApp;
using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.Permissions;
using EnergonSoftware.BackpackPlanner.Core.PlayServices;
using EnergonSoftware.BackpackPlanner.Core.Settings;
using EnergonSoftware.BackpackPlanner.Models.Personal;
using EnergonSoftware.BackpackPlanner.Settings;

using SQLite.Net.Interop;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// Collects the general library state
    /// </summary>
    public sealed class BackpackPlannerState : IDisposable
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BackpackPlannerState));

        /// <summary>
        /// Gets the library settings.
        /// </summary>
        /// <value>
        /// The library settings.
        /// </value>
        public BackpackPlannerSettings Settings { get; }

        /// <summary>
        /// Gets or sets the user's personal information.
        /// </summary>
        /// <value>
        /// The user's personal information.
        /// </value>
        public PersonalInformation PersonalInformation { get; }

        /// <summary>
        /// Gets the database state.
        /// </summary>
        /// <value>
        /// The database state.
        /// </value>
        public DatabaseState DatabaseState { get; } = new DatabaseState();

#region Platform DI
        /// <summary>
        /// Gets the platform HockeyApp manager.
        /// </summary>
        /// <value>
        /// The platform HockeyApp manager.
        /// </value>
        public IHockeyAppManager PlatformHockeyAppManager { get; }

        /// <summary>
        /// Gets the platform google play services interface.
        /// </summary>
        /// <value>
        /// The platform google play services interface.
        /// </value>
        public PlayServicesManager PlatformPlayServicesManager { get; }

        /// <summary>
        /// Gets the platform database sync manager.
        /// </summary>
        /// <value>
        /// The platform database sync manager.
        /// </value>
        public DatabaseSyncManager PlatformDatabaseSyncManager { get; }

        /// <summary>
        /// Gets the platform settings manager.
        /// </summary>
        /// <value>
        /// The platform settings manager.
        /// </value>
        public SettingsManager PlatformSettingsManager { get; }

        public PermissionRequestFactory PlatformPermissionRequestFactory { get; }
#endregion

        private readonly Dictionary<PermissionRequest.PermissionType, List<PermissionRequest>> _permissionRequests = new Dictionary<PermissionRequest.PermissionType, List<PermissionRequest>>();

#region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(disposing) {
                DatabaseState?.Dispose();
                PlatformPlayServicesManager?.Dispose();
            }
        }
#endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BackpackPlannerState" /> class.
        /// </summary>
        /// <param name="platformHockeyAppManager">The platform HockeyApp manager.</param>
        /// <param name="platformSettingsManager">The platform settings manager.</param>
        /// <param name="platformPlayServicesManager">The platform google play services manager.</param>
        /// <param name="platformDatabaseSyncManager">The platform database sync manager.</param>
        /// <param name="sqlitePlatform">The SQLite platform.</param>
        public BackpackPlannerState(IHockeyAppManager platformHockeyAppManager, SettingsManager platformSettingsManager, PlayServicesManager platformPlayServicesManager, DatabaseSyncManager platformDatabaseSyncManager, ISQLitePlatform sqlitePlatform, PermissionRequestFactory platformPermissionRequestFactory)
        {
            if(null == platformHockeyAppManager) {
                throw new ArgumentNullException(nameof(platformHockeyAppManager));
            }

            if(null == platformSettingsManager) {
                throw new ArgumentNullException(nameof(platformSettingsManager));
            }

            if(null == platformPlayServicesManager) {
                throw new ArgumentNullException(nameof(platformPlayServicesManager));
            }

            if(null == platformDatabaseSyncManager) {
                throw new ArgumentNullException(nameof(platformDatabaseSyncManager));
            }

            if(null == sqlitePlatform) {
                throw new ArgumentNullException(nameof(sqlitePlatform));
            }

            if(null == platformPermissionRequestFactory) {
                throw new ArgumentNullException(nameof(platformPermissionRequestFactory));
            }

            PlatformHockeyAppManager = platformHockeyAppManager;

            PlatformSettingsManager = platformSettingsManager;
            Settings = new BackpackPlannerSettings(PlatformSettingsManager);

            DatabaseState.SQLitePlatform = sqlitePlatform;

            PlatformPlayServicesManager = platformPlayServicesManager;
            PlatformDatabaseSyncManager = platformDatabaseSyncManager;

            PlatformPermissionRequestFactory = platformPermissionRequestFactory;

            PersonalInformation = new PersonalInformation(PlatformSettingsManager, Settings);
        }

        ~BackpackPlannerState()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initializes the dependency state.
        /// </summary>
        public async Task InitAsync()
        {
            Logger.Debug("Initializing platform state...");

            await PlatformHockeyAppManager.InitAsync().ConfigureAwait(false);

            await PlatformPlayServicesManager.InitAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Destroys the dependency state.
        /// </summary>
        public async Task DestroyAsync()
        {
            Logger.Debug("Destroying platform state...");

            await PlatformPlayServicesManager.DestroyAsync().ConfigureAwait(false);

            await DatabaseState.DisconnectAsync().ConfigureAwait(false);

            await PlatformHockeyAppManager.DestroyAsync().ConfigureAwait(false);
        }

#region Permissions
        /// <summary>
        /// Adds a permission request.
        /// </summary>
        /// <param name="permissionRequest">The permission request.</param>
        /// <returns>True if the permission is new, false if it was added to an existin grequest.</returns>
        public void AddPermissionRequest(PermissionRequest permissionRequest)
        {
            Logger.Debug($"Adding request for permission {permissionRequest.Permission}...");

            List<PermissionRequest> requests;
            if(!_permissionRequests.TryGetValue(permissionRequest.Permission, out requests)) {
                requests = new List<PermissionRequest>();
                _permissionRequests.Add(permissionRequest.Permission, requests);
            }

            requests.Add(permissionRequest);
        }

        public int RemovePermissionRequests(Predicate<PermissionRequest> match)
        {
            return _permissionRequests.Sum(requests => requests.Value.RemoveAll(match));
        }

        /// <summary>
        /// Notifies that a permission request has completed.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="granted">if set to <c>true</c> the permission was granted, otherwise it was denied.</param>
        public void NotifyPermissionRequests(PermissionRequest.PermissionType permission, bool granted)
        {
            List<PermissionRequest> requests;
            if(!_permissionRequests.TryGetValue(permission, out requests)) {
                Logger.Warn($"Attempt to notify for permission {permission}, which does not exist!");
                return;
            }

            Logger.Debug($"Notifying for permission {permission}: {granted}");

            foreach(PermissionRequest request in requests) {
                request.Notify(granted);
            }
            requests.Clear();
        }
#endregion
    }
}
