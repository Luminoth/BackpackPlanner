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

using System.Threading.Tasks;

using SQLite.Net.Async;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// Collects the general library state
    /// </summary>
    public class BackpackPlannerState
    {
        /// <summary>
        /// The database version
        /// </summary>
        public const int DatabaseVersion = 1;

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        public static BackpackPlannerState Instance => new BackpackPlannerState();

        /// <summary>
        /// Gets the library settings.
        /// </summary>
        /// <value>
        /// The library settings.
        /// </value>
        public BackpackPlannerSettings Settings => new BackpackPlannerSettings();

        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        public SQLiteAsyncConnection DbConnection { get; set; }

#region Caches
        private readonly GearCache _gearCache = new GearCache();
#endregion

        /// <summary>
        /// Initializes the library database.
        /// </summary>
        public async Task InitDatabaseAsync()
        {
            await GearCache.InitDatabaseAsync(-1, DatabaseVersion).ConfigureAwait(false);
        }

        /// <summary>
        /// Loads the library state from the device.
        /// </summary>
        public async Task LoadFromDeviceAsync()
        {
            await _gearCache.LoadFromDeviceAsync().ConfigureAwait(false);
        }

        private BackpackPlannerState()
        {
        }
    }
}
