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
using System.Threading.Tasks;

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
        /// Gets the platform settings manager.
        /// </summary>
        /// <value>
        /// The platform settings manager.
        /// </value>
        public SettingsManager PlatformSettingsManager { get; }

        /// <summary>
        /// Gets the platform permission request factory.
        /// </summary>
        /// <value>
        /// The platform permission request factory.
        /// </value>
        public PermissionRequestFactory PlatformPermissionRequestFactory { get; }
#endregion

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
        /// <param name="sqlitePlatform">The SQLite platform.</param>
        /// <param name="platformPermissionRequestFactory">The platform permission request factory.</param>
        public BackpackPlannerState(IHockeyAppManager platformHockeyAppManager, SettingsManager platformSettingsManager, PlayServicesManager platformPlayServicesManager, ISQLitePlatform sqlitePlatform, PermissionRequestFactory platformPermissionRequestFactory)
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

            PlatformPlayServicesManager.Init();
        }

        /// <summary>
        /// Destroys the dependency state.
        /// </summary>
        public void Destroy()
        {
            Logger.Debug("Destroying platform state...");

            PlatformPlayServicesManager.Destroy();

            DatabaseState.DisconnectAsync().Wait();

            PlatformHockeyAppManager.Destroy();
        }
    }
}
