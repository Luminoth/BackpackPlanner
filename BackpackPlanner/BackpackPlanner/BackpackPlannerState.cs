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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models;

using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// Collects the general library state
    /// </summary>
    public class BackpackPlannerState
    {
        /// <summary>
        /// The database name
        /// </summary>
        public const string DatabaseName = "BackpackPlanner.db";

        /// <summary>
        /// The database version
        /// </summary>
        public const int CurrentDatabaseVersion = 1;

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        public static readonly BackpackPlannerState Instance = new BackpackPlannerState();

        /// <summary>
        /// Gets the library settings.
        /// </summary>
        /// <value>
        /// The library settings.
        /// </value>
        public BackpackPlannerSettings Settings { get; } = new BackpackPlannerSettings();

        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        public SQLiteConnectionWithLock DbConnection { get; set; }

        /// <summary>
        /// Gets or sets the async database connection.
        /// </summary>
        /// <value>
        /// The async database connection.
        /// </value>
        public SQLiteAsyncConnection AsyncDbConnection { get; set; }

        private readonly GearCache _gearCache = new GearCache();

        /// <summary>
        /// Initializes the library database.
        /// </summary>
        /// <param name="sqlitePlatform">The sqlite platform.</param>
        /// <param name="dbPath">The database path.</param>
        /// <param name="dbName">Name of the database.</param>
        public async Task InitDatabaseAsync(ISQLitePlatform sqlitePlatform, string dbPath, string dbName)
        {
            if(null != DbConnection || null != AsyncDbConnection) {
                throw new InvalidOperationException("Database already initialized!");
            }

            string combinedPath = Path.Combine(dbPath, dbName);
            Debug.WriteLine($"Using database at {combinedPath}");
            SQLiteConnectionString connectionString = new SQLiteConnectionString(combinedPath, true);

            DbConnection = new SQLiteConnectionWithLock(sqlitePlatform, connectionString);
            AsyncDbConnection = new SQLiteAsyncConnection(() => DbConnection);

            DatabaseVersion oldVersion = new DatabaseVersion();
            var databaseVersionTableInfo = DbConnection.GetTableInfo("DatabaseVersion");
            if(!databaseVersionTableInfo.Any()) {
                Debug.WriteLine("Empty database!");
                await DatabaseVersion.CreateTablesAsync(AsyncDbConnection).ConfigureAwait(false);
            } else {
                oldVersion = await DatabaseVersion.GetAsync(AsyncDbConnection).ConfigureAwait(false);
                Debug.WriteLine($"Old database version: {oldVersion.Version}, current database version: {CurrentDatabaseVersion}");
            }

            await GearCache.InitDatabaseAsync(oldVersion.Version, CurrentDatabaseVersion).ConfigureAwait(false);
        }

        /// <summary>
        /// Loads the library state from the device.
        /// </summary>
        public async Task LoadFromDeviceAsync()
        {
            Debug.WriteLine("Loading data from device...");
            await _gearCache.LoadFromDeviceAsync().ConfigureAwait(false);
        }

        private BackpackPlannerState()
        {
        }
    }
}
