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

using EnergonSoftware.BackpackPlanner.Core.Database;
using EnergonSoftware.BackpackPlanner.Core.Logging;
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
        /// Gets the singleton instance.
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        public static readonly BackpackPlannerState Instance = new BackpackPlannerState();

        /// <summary>
        /// Gets the platform logger.
        /// </summary>
        /// <value>
        /// The platform logger.
        /// </value>
        public ILogger PlatformLogger { get; private set; } = new DiagnosticsLogger();

        /// <summary>
        /// Gets the library settings.
        /// </summary>
        /// <value>
        /// The library settings.
        /// </value>
        public BackpackPlannerSettings Settings { get; } = new BackpackPlannerSettings();

        /// <summary>
        /// Gets or sets the user's personal information.
        /// </summary>
        /// <value>
        /// The user's personal information.
        /// </value>
        public PersonalInformation PersonalInformation { get; set; } = new PersonalInformation();

        /// <summary>
        /// Gets the database state.
        /// </summary>
        /// <value>
        /// The database state.
        /// </value>
        public DatabaseState DatabaseState { get; } = new DatabaseState();

#region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(disposing) {
                DatabaseState.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// Initializes the platform-specific state.
        /// </summary>
        /// <param name="platformLogger">The platform logger.</param>
        /// <param name="sqlitePlatform">The SQLite platform.</param>
        /// <param name="settingsChangedEventHandler">The settings changed event handler.</param>
        public void InitPlatform(ILogger platformLogger, ISQLitePlatform sqlitePlatform, EventHandler<SettingsChangedEventArgs> settingsChangedEventHandler)
        {
            if(null == platformLogger) {
                throw new ArgumentNullException(nameof(platformLogger));
            }

            if(null == sqlitePlatform) {
                throw new ArgumentNullException(nameof(sqlitePlatform));
            }

            Logger.Debug("Initializing platform state...");

            PlatformLogger = platformLogger;

            DatabaseState.SQLitePlatform = sqlitePlatform;

            if(null != settingsChangedEventHandler) {
                Settings.SettingsChangedEvent += settingsChangedEventHandler;
            }
        }

        private BackpackPlannerState()
        {
        }
    }
}
