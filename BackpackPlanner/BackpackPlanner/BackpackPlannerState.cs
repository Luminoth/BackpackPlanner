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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Database;
using EnergonSoftware.BackpackPlanner.Logging;
using EnergonSoftware.BackpackPlanner.Models;
using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Models.Meals;
using EnergonSoftware.BackpackPlanner.Models.Personal;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using SQLite.Net;
using SQLite.Net.Interop;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// Collects the general library state
    /// </summary>
    public sealed class BackpackPlannerState
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BackpackPlannerState));

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

        private ILogger _systemLogger = new DebugLogger();

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger SystemLogger
        {
            get { return _systemLogger; }
            set { _systemLogger = value ?? new DebugLogger(); }
        }

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
        /// Gets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        /// <remarks>
        /// Android recommends only having a single, long-lived
        /// connection to the database... so here we are
        /// </remarks>
        public DatabaseConnection DatabaseConnection { get; } = new DatabaseConnection();

        /// <summary>
        /// Initializes the library database.
        /// </summary>
        /// <param name="sqlitePlatform">The sqlite platform.</param>
        /// <param name="dbPath">The database path.</param>
        /// <param name="dbName">Name of the database.</param>
        public async Task InitDatabaseAsync(ISQLitePlatform sqlitePlatform, string dbPath, string dbName)
        {
            if(DatabaseConnection.IsConnected) {
                Logger.Warn("SQLite connection already initialized!");
                return;
            }

            // connect to the database
            SQLiteConnectionString connectionString = new SQLiteConnectionString(Path.Combine(dbPath, dbName), true);
            Logger.Info($"Initializing database at {connectionString.ConnectionString}...");
            await DatabaseConnection.ConnectAsync(sqlitePlatform, connectionString).ConfigureAwait(false);

            DatabaseVersion newVersion = new DatabaseVersion
            {
                Version = CurrentDatabaseVersion
            };
            DatabaseVersion oldVersion = new DatabaseVersion();

            await DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                var databaseVersionTableInfo = DatabaseConnection.Connection.GetTableInfo("DatabaseVersion");
                if(!databaseVersionTableInfo.Any()) {
                    Logger.Debug("Creating a new database...");
                    await DatabaseVersion.CreateTablesAsync(DatabaseConnection.AsyncConnection).ConfigureAwait(false);
                } else {
                    oldVersion = await DatabaseVersion.GetAsync(DatabaseConnection.AsyncConnection).ConfigureAwait(false);
                    if(null == oldVersion) {
                        Logger.Debug("DatabaseVersion.Get returned null!?!");
                        throw new InvalidOperationException("DatabaseVersion.Get returned null!?!");
                    }
                    Logger.Debug($"Old database version: {oldVersion.Version}, current database version: {CurrentDatabaseVersion}");
                }

                // TODO: find a way to make this transactional
                // so that we can roll it back on error and avoid updating the db version
                await GearItem.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await GearSystem.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await GearCollection.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await Meal.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await TripItinerary.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await TripPlan.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);

                await DatabaseVersion.UpdateAsync(DatabaseConnection.AsyncConnection, newVersion).ConfigureAwait(false);
            } finally {
                DatabaseConnection.Lock.Release();
            }
        }

        private BackpackPlannerState()
        {
        }
    }
}
