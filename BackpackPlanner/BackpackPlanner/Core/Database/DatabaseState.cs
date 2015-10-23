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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Models;
using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Models.Meals;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Settings;

using SQLite.Net;
using SQLite.Net.Interop;

namespace EnergonSoftware.BackpackPlanner.Core.Database
{
    /// <summary>
    /// 
    /// </summary>
    // TODO: this doesn't belong in core
    public sealed class DatabaseState : IDisposable
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DatabaseState));

        /// <summary>
        /// The database name
        /// </summary>
        public const string DatabaseName = "BackpackPlanner.db";

        /// <summary>
        /// The database version
        /// </summary>
        public const int CurrentDatabaseVersion = 3;

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
        public SQLiteDatabaseConnection Connection { get; } = new SQLiteDatabaseConnection();

        /// <summary>
        /// Gets or sets the SQLite platform.
        /// </summary>
        /// <value>
        /// The SQLite platform.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public ISQLitePlatform SQLitePlatform { get; set; }

        /// <summary>
        /// Gets a value indicating whether the database is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the database is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the database is being initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the database is being initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitializing { get; private set; }

#region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(disposing) {
                Connection?.Dispose();
            }
        }
#endregion

        /// <summary>
        /// Connects the library database connections.
        /// </summary>
        /// <param name="dbPath">The database path.</param>
        /// <param name="dbName">Name of the database.</param>
        public async Task ConnectAsync(string dbPath, string dbName)
        {
            if(Connection.IsConnected) {
                Logger.Warn("Database connection already initialized!");
                return;
            }

            if(null == SQLitePlatform) {
                throw new InvalidOperationException("Invalid SQLite platform!");
            }

            // connect to the database
            SQLiteConnectionString connectionString = new SQLiteConnectionString(Path.Combine(dbPath, dbName), true);
            Logger.Info($"Connecting to database at {connectionString.ConnectionString}...");
            await Connection.ConnectAsync(SQLitePlatform, connectionString).ConfigureAwait(false);
        }

        /// <summary>
        /// Disconnects from the database.
        /// </summary>
        public async Task DisconnectAsync()
        {
            if(!Connection.IsConnected) {
                return;
            }

            await Connection.CloseAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Initializes the database.
        /// </summary>
        public async Task InitDatabaseAsync(BackpackPlannerSettings settings)
        {
            if(IsInitialized || IsInitializing) {
                return;
            }

            IsInitializing = true;

            Logger.Info("Initializing database...");

            DatabaseVersion newVersion = new DatabaseVersion
            {
                Version = CurrentDatabaseVersion
            };
            DatabaseVersion oldVersion;

            await Connection.LockAsync().ConfigureAwait(false);
            try {
                var databaseVersionTableInfo = Connection.Connection.GetTableInfo("DatabaseVersion");
                if(!databaseVersionTableInfo.Any()) {
                    Logger.Debug("Creating a new database...");
                    await DatabaseVersion.CreateTablesAsync(Connection.AsyncConnection).ConfigureAwait(false);
                }

                oldVersion = await DatabaseVersion.GetAsync(Connection.AsyncConnection).ConfigureAwait(false);
                if(null == oldVersion) {
                    Logger.Debug("DatabaseVersion.Get returned null!?!");
                    throw new InvalidOperationException("DatabaseVersion.Get returned null!?!");
                }
                Logger.Debug($"Old database version: {oldVersion.Version}, current database version: {CurrentDatabaseVersion}");

                // TODO: find a way to make this transactional
                // so that we can roll it back on error and avoid updating the db version
                await GearItem.InitDatabaseAsync(Connection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await GearSystem.InitDatabaseAsync(Connection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await GearCollection.InitDatabaseAsync(Connection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await Meal.InitDatabaseAsync(Connection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await TripItinerary.InitDatabaseAsync(Connection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await TripPlan.InitDatabaseAsync(Connection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);

                newVersion.DatabaseVersionId = oldVersion.DatabaseVersionId;
                await DatabaseVersion.UpdateAsync(Connection.AsyncConnection, newVersion).ConfigureAwait(false);
            } finally {
                Connection.Release();
            }

            // finish initialization in a background task because
            // populating the database can take a while
            Task task = Task.Run(async () => {
                if(oldVersion.Version < 1) {
                    // NOTE: this is the only thing that
                    // requires the settings parameter
                    await PopulateInitialDatabaseAsync(settings).ConfigureAwait(false);
                }

                IsInitialized = true;
                IsInitializing = false;
            });
            task.NoOp();
        }

        private async Task PopulateInitialDatabaseAsync(BackpackPlannerSettings settings)
        {
#if DEBUG
            Logger.Debug("Populating test data, this will take a while...");

#region Test Gear Items
            Logger.Debug("Inserting test gear items...");
            await DatabaseItem.InsertItemsAsync(this, new List<GearItem>
                {
                    new GearItem(settings)
                    {
                        Name = "Alcohol Stove",
                        Make = "Zelph's Stoveworks",
                        Model = "StarLyte",
                        Url = "http://www.woodgaz-stove.com/starlyte-burner-with-lid.php",
                        WeightInGrams = 19,
                        CostInUSDP = 1300,
                    },
                    new GearItem(settings)
                    {
                        Name = "Backpack",
                        Make = "ULA",
                        Model = "Circuit",
                        Url = "http://www.ula-equipment.com/product_p/circuit.htm",
                        WeightInGrams = 986,
                        CostInUSDP = 22500,
                        Note = "Medium torso (18\" - 21\"). Medium hipbelt (34\" - 38\"). J-Curve shoulder strap. Aluminum stay removed. Includes hanging s-biner \"Ahhh\" and water shoe carabiner. 39L main body. Max 15 pound base weight, 30-35 pack weight."
                    },
                    new GearItem(settings)
                    {
                        Name = "Hammock",
                        Make = "Aaron Erbe",
                        Model = "DIY",
                        WeightInGrams = 422,
                        CostInUSDP = 0,
                        Note = "Includes adjustable ridge line and 2x whoopie slings from whoopieslings.com, and bishop bag."
                    },
                    new GearItem(settings)
                    {
                        Name = "Head Lamp",
                        Make = "Petzl",
                        Model = "Tikka Plus 2",
                        WeightInGrams = 79,
                        CostInUSDP = 2995
                    },
                    new GearItem(settings)
                    {
                        Name = "Kilt",
                        Make = "Utilikilt",
                        Model = "Survival",
                        Url = "http://www.utilikilts.com/index.php/the-survival.html",
                        Carried = GearCarried.Worn,
                        WeightInGrams = 989,
                        CostInUSDP = 33000,
                        Note = "100% cotton. Cargo pockets removed (3.8 ounces each)."
                    },
                    new GearItem(settings)
                    {
                        Name = "New Underquilt",
                        Make = "Arrowhead Equipment",
                        Model = "Anniversary Jarbridge 3S (25F)",
                        Url = "http://www.arrowhead-equipment.com/store/p510/Anniversary_Jarbidge_UnderQuilt.htmll",
                        WeightInGrams = 566,
                        CostInUSDP = 7500,
                        Note = "6oz APEX Climashield synthetic"
                    },
                    new GearItem(settings)
                    {
                        Name = "Old Underquilt",
                        Make = "Aaron Erbe",
                        Model = "DIY",
                        WeightInGrams = 887,
                        CostInUSDP = 0,
                        Note = "Synthetic material. Need to have Aaron or Joe possibly remove some material from the overstuff collars to get the size and weight down on this."
                    },
                    new GearItem(settings)
                    {
                        Name = "Overquilt",
                        Make = "Arrowhead Equipment",
                        Model = "Owyhee Top Quilt Regular 3S (25F)",
                        Url = "http://www.arrowhead-equipment.com/store/p314/Owyhee_Top_Quilt_Regular.html",
                        WeightInGrams = 802,
                        CostInUSDP = 17900,
                        Note = "6oz APEX Climashield synthetic"
                    },
                    new GearItem(settings)
                    {
                        Name = "Toilet Paper",
                        IsConsumable = true,
                        ConsumedPerDay = 10,
                        WeightInGrams = 1,
                        CostInUSDP = 1,
                        Note = "Can't have too much!"
                    },
                    new GearItem(settings)
                    {
                        Name = "Tree Straps",
                        Url = "http://shop.whoopieslings.com/Tree-Huggers-TH.htm",
                        WeightInGrams = 198,
                        CostInUSDP = 1200,
                        Note = "12'x1\". Includes dutch buckle and titanium dutch clip (max 300 pounds)."
                    },
                    new GearItem(settings)
                    {
                        Name = "5g Water Jug",
                        Carried = GearCarried.NotCarried
                    },
                    new GearItem(settings)
                    {
                        Name = "Wind Screen",
                        Make = "Trail Designs",
                        Model = "Caldera Cone System",
                        Url = "http://www.traildesigns.com/stoves/caldera-cone-system",
                        WeightInGrams = 141,
                        CostInUSDP = 3400
                    }
                }
            );
#endregion

#region Test Gear Systems
            Logger.Debug("Inserting test gear systems...");
            await DatabaseItem.InsertItemsAsync(this, new List<GearSystem>
                {
                    new GearSystem(settings)
                    {
                        Name = "One"
                    },
                    new GearSystem(settings)
                    {
                        Name = "Two"
                    },
                    new GearSystem(settings)
                    {
                        Name = "Three"
                    },
                    new GearSystem(settings)
                    {
                        Name = "Four"
                    },
                    new GearSystem(settings)
                    {
                        Name = "Five"
                    },
                    new GearSystem(settings)
                    {
                        Name = "Six"
                    },
                    new GearSystem(settings)
                    {
                        Name = "Seven"
                    },
                    new GearSystem(settings)
                    {
                        Name = "Eight"
                    },
                    new GearSystem(settings)
                    {
                        Name = "Nine"
                    },
                    new GearSystem(settings)
                    {
                        Name = "Ten"
                    }
                }
            );
#endregion

#region Test Gear Collections
            Logger.Debug("Inserting test gear collections...");
            await DatabaseItem.InsertItemsAsync(this, new List<GearCollection>
                {
                    new GearCollection(settings)
                    {
                        Name = "One"
                    },
                    new GearCollection(settings)
                    {
                        Name = "Two"
                    },
                    new GearCollection(settings)
                    {
                        Name = "Three"
                    },
                    new GearCollection(settings)
                    {
                        Name = "Four"
                    },
                    new GearCollection(settings)
                    {
                        Name = "Five"
                    },
                    new GearCollection(settings)
                    {
                        Name = "Six"
                    },
                    new GearCollection(settings)
                    {
                        Name = "Seven"
                    },
                    new GearCollection(settings)
                    {
                        Name = "Eight"
                    },
                    new GearCollection(settings)
                    {
                        Name = "Nine"
                    },
                    new GearCollection(settings)
                    {
                        Name = "Ten"
                    }
                }
            );
#endregion

#region Test Meals
            Logger.Debug("Inserting test meals...");
            await DatabaseItem.InsertItemsAsync(this, new List<Meal>
                {
                    new Meal(settings)
                    {
                        Name = "One",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 1,
                        CostInUSDP = 20
                    },
                    new Meal(settings)
                    {
                        Name = "Two",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 2,
                        CostInUSDP = 19
                    },
                    new Meal(settings)
                    {
                        Name = "Three",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 3,
                        CostInUSDP = 18
                    },
                    new Meal(settings)
                    {
                        Name = "Four",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 4,
                        CostInUSDP = 17
                    },
                    new Meal(settings)
                    {
                        Name = "Five",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 5,
                        CostInUSDP = 16
                    },
                    new Meal(settings)
                    {
                        Name = "Six",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 6,
                        CostInUSDP = 15
                    },
                    new Meal(settings)
                    {
                        Name = "Seven",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 7,
                        CostInUSDP = 14
                    },
                    new Meal(settings)
                    {
                        Name = "Eight",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 8,
                        CostInUSDP = 13
                    },
                    new Meal(settings)
                    {
                        Name = "Nine",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 9,
                        CostInUSDP = 12
                    },
                    new Meal(settings)
                    {
                        Name = "Ten",
                        MealTime = MealTime.Dinner,
                        ServingCount = 1,
                        Calories = 50,
                        ProteinInGrams = 1,
                        FiberInGrams = 1,
                        WeightInGrams = 10,
                        CostInUSDP = 11
                    }
                }
            );
#endregion

#region Test Trip Itineraries
            Logger.Debug("Inserting test trip itineraries...");
            await DatabaseItem.InsertItemsAsync(this, new List<TripItinerary>
                {
                    new TripItinerary(settings)
                    {
                        Name = "One"
                    },
                    new TripItinerary(settings)
                    {
                        Name = "Two"
                    },
                    new TripItinerary(settings)
                    {
                        Name = "Three"
                    },
                    new TripItinerary(settings)
                    {
                        Name = "Four"
                    },
                    new TripItinerary(settings)
                    {
                        Name = "Five"
                    },
                    new TripItinerary(settings)
                    {
                        Name = "Six"
                    },
                    new TripItinerary(settings)
                    {
                        Name = "Seven"
                    },
                    new TripItinerary(settings)
                    {
                        Name = "Eight"
                    },
                    new TripItinerary(settings)
                    {
                        Name = "Nine"
                    },
                    new TripItinerary(settings)
                    {
                        Name = "Ten"
                    }
                }
            );
#endregion

#region Test Trip Plans
            Logger.Debug("Inserting test trip plans...");
            await DatabaseItem.InsertItemsAsync(this, new List<TripPlan>
                {
                    new TripPlan(settings)
                    {
                        Name = "One"
                    },
                    new TripPlan(settings)
                    {
                        Name = "Two"
                    },
                    new TripPlan(settings)
                    {
                        Name = "Three"
                    },
                    new TripPlan(settings)
                    {
                        Name = "Four"
                    },
                    new TripPlan(settings)
                    {
                        Name = "Five"
                    },
                    new TripPlan(settings)
                    {
                        Name = "Six"
                    },
                    new TripPlan(settings)
                    {
                        Name = "Seven"
                    },
                    new TripPlan(settings)
                    {
                        Name = "Eight"
                    },
                    new TripPlan(settings)
                    {
                        Name = "Nine"
                    },
                    new TripPlan(settings)
                    {
                        Name = "Ten"
                    }
                }
            );
#endregion

            Logger.Debug("Finished populating test data!");
#else
            await Task.Delay(0).ConfigureAwait(false);
#endif
        }
    }
}
