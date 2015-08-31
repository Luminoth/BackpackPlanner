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

using EnergonSoftware.BackpackPlanner.Cache;
using EnergonSoftware.BackpackPlanner.Models;
using EnergonSoftware.BackpackPlanner.Models.Personal;

using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// Collects the general library state
    /// </summary>
    public sealed class BackpackPlannerState
    {
        /// <summary>
        /// The database name
        /// </summary>
        public const string DatabaseName = "BackpackPlanner.db";

        /// <summary>
        /// The database version
        /// </summary>
        public const int CurrentDatabaseVersion = 3;

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
        /// Gets or sets the user's personal information.
        /// </summary>
        /// <value>
        /// The user's personal information.
        /// </value>
        public PersonalInformation PersonalInformation { get; set; } = new PersonalInformation();

        private SQLiteConnectionString _connectionString;
        private SQLiteConnectionPool _connectionPool;

        /// <summary>
        /// Gets a connection to the database.
        /// </summary>
        /// <returns>A connection to the database.</returns>
        public SQLiteConnectionWithLock GetDatabaseConnection()
        {
            if(null == _connectionPool) {
                throw new InvalidOperationException("SQLite connection pool not initialized!");
            }

            if(null == _connectionString) {
                throw new InvalidOperationException("SQLite connection string is null!");
            }

            Debug.WriteLine("Getting a new database connection...");
            return _connectionPool.GetConnection(_connectionString);
        }

        /// <summary>
        /// Initializes the library database.
        /// </summary>
        /// <param name="sqlitePlatform">The sqlite platform.</param>
        /// <param name="dbPath">The database path.</param>
        /// <param name="dbName">Name of the database.</param>
        public async Task InitDatabaseAsync(ISQLitePlatform sqlitePlatform, string dbPath, string dbName)
        {
            if(null != _connectionPool) {
                Debug.WriteLine("Connection pool already initialized!");
                return;
            }

            _connectionString = new SQLiteConnectionString(Path.Combine(dbPath, dbName), true);
            _connectionPool = new SQLiteConnectionPool(sqlitePlatform);

            Debug.WriteLine($"Initializing database at {_connectionString.ConnectionString}...");
            using(SQLiteConnectionWithLock dbConnection = GetDatabaseConnection()) {
                SQLiteAsyncConnection asyncDbConnection = new SQLiteAsyncConnection(() => dbConnection);

                DatabaseVersion newVersion = new DatabaseVersion
                {
                    Version = CurrentDatabaseVersion
                };
                DatabaseVersion oldVersion = new DatabaseVersion();

                var databaseVersionTableInfo = dbConnection.GetTableInfo("DatabaseVersion");
                if(!databaseVersionTableInfo.Any()) {
                    Debug.WriteLine("New database!");
                    await DatabaseVersion.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
                } else {
                    oldVersion = await DatabaseVersion.GetAsync(asyncDbConnection).ConfigureAwait(false);
                    Debug.WriteLine($"Old database version: {oldVersion.Version}, current database version: {CurrentDatabaseVersion}");
                }

                // TODO: find a way to make this transactional
                // so that we can roll it back on error and avoid updating the db version
                await GearCache.InitDatabaseAsync(asyncDbConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await MealCache.InitDatabaseAsync(asyncDbConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await TripCache.InitDatabaseAsync(asyncDbConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);

                await DatabaseVersion.UpdateAsync(asyncDbConnection, newVersion).ConfigureAwait(false);
            }
        }

        private BackpackPlannerState()
        {
        }
    }
}
