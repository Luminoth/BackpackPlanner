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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Database;
using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Models;
using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Models.Meals;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using SQLite;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// 
    /// </summary>
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
        public const int CurrentDatabaseVersion = 4;

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
        /// Gets a value indicating whether the database is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the database is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized { get; private set; }

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

        ~DatabaseState()
        {
            Dispose(false);
        }

        /// <summary>
        /// Connects the library database connections.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <param name="dbPath">The database path.</param>
        /// <param name="dbName">Name of the database.</param>
        public async Task ConnectAsync(BackpackPlannerState state, string dbPath, string dbName)
        {
            if(Connection.IsConnected) {
                Logger.Warn("Database connection already initialized!");
                return;
            }

            // connect to the database
            SQLiteConnectionString connectionString = new SQLiteConnectionString(Path.Combine(dbPath, dbName), true);
            Logger.Info($"Connecting to database at {connectionString.ConnectionString}...");
            await Connection.ConnectAsync(state, connectionString).ConfigureAwait(false);
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
        /// <param name="state">The system state.</param>
        public async Task InitDatabaseAsync(BackpackPlannerState state)
        {
            if(IsInitialized) {
                return;
            }

            DatabaseVersion newVersion = new DatabaseVersion
            {
                Version = CurrentDatabaseVersion
            };
            DatabaseVersion oldVersion;

            Logger.Info("Initializing database...");

            await Connection.LockAsync().ConfigureAwait(false);
            try {
                var databaseVersionTableInfo = Connection.Connection.GetTableInfo("DatabaseVersion");
                if(!databaseVersionTableInfo.Any()) {
                    Logger.Debug("Creating a new database...");
                    await DatabaseVersion.CreateTablesAsync(state).ConfigureAwait(false);
                }

                oldVersion = await DatabaseVersion.GetAsync(state).ConfigureAwait(false);
                if(null == oldVersion) {
                    Logger.Debug("DatabaseVersion.Get returned null!?!");
                    throw new InvalidOperationException("DatabaseVersion.Get returned null!?!");
                }
                Logger.Debug($"Old database version: {oldVersion.Version}, current database version: {CurrentDatabaseVersion}");

                // TODO: find a way to make this transactional
                // so that we can roll it back on error and avoid updating the db version
                await GearItem.InitDatabaseAsync(state, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await GearSystem.InitDatabaseAsync(state, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await GearCollection.InitDatabaseAsync(state, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await Meal.InitDatabaseAsync(state, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await TripItinerary.InitDatabaseAsync(state, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await TripPlan.InitDatabaseAsync(state, oldVersion.Version, newVersion.Version).ConfigureAwait(false);

                newVersion.DatabaseVersionId = oldVersion.DatabaseVersionId;
                await DatabaseVersion.UpdateAsync(state, newVersion).ConfigureAwait(false);
            } finally {
                Connection.Release();
            }

            if(oldVersion.Version < 1) {
                await PopulateInitialDatabaseAsync(state).ConfigureAwait(false);
            }

            IsInitialized = true;
        }

        private async Task PopulateInitialDatabaseAsync(BackpackPlannerState state)
        {
#if DEBUG
            Logger.Debug("Populating test data, this will take a while...");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

#region Test Gear Items
            var gearItems = new List<GearItem>
            {
                new GearItem(state.Settings)
                {
                    Name = "Backpack",
                    Make = "ULA",
                    Model = "Circuit",
                    Url = "http://www.ula-equipment.com/product_p/circuit.htm",
                    WeightInGrams = 986,
                    CostInUSDP = 22500,
                    Note = "Medium torso (18\" - 21\"). Medium hipbelt (34\" - 38\"). J-Curve shoulder strap. Aluminum stay removed. Includes hanging s-biner \"Ahhh\" and water shoe carabiner. 39L main body. Max 15 pound base weight, 30-35 pack weight."
                },
                new GearItem(state.Settings)
                {
                    Name = "Hammock",
                    Make = "Aaron Erbe",
                    Model = "DIY",
                    WeightInGrams = 422,
                    CostInUSDP = 0,
                    Note = "Includes adjustable ridge line and 2x whoopie slings from whoopieslings.com, and bishop bag."
                },
                new GearItem(state.Settings)
                {
                    Name = "Tree Straps",
                    Url = "http://shop.whoopieslings.com/Tree-Huggers-TH.htm",
                    WeightInGrams = 198,
                    CostInUSDP = 1200,
                    Note = "12'x1\". Includes dutch buckle and titanium dutch clip (max 300 pounds)."
                },
                new GearItem(state.Settings)
                {
                    Name = "Old Underquilt",
                    Make = "Aaron Erbe",
                    Model = "DIY",
                    WeightInGrams = 887,
                    CostInUSDP = 0,
                    Note = "Synthetic material. Need to have Aaron or Joe possibly remove some material from the overstuff collars to get the size and weight down on this."
                },
                new GearItem(state.Settings)
                {
                    Name = "Overquilt",
                    Make = "Arrowhead Equipment",
                    Model = "Owyhee Top Quilt Regular 3S (25F)",
                    Url = "http://www.arrowhead-equipment.com/store/p314/Owyhee_Top_Quilt_Regular.html",
                    WeightInGrams = 802,
                    CostInUSDP = 17900,
                    Note = "6oz APEX Climashield synthetic"
                },
                new GearItem(state.Settings)
                {
                    Name = "New Underquilt",
                    Make = "Arrowhead Equipment",
                    Model = "Anniversary Jarbridge 3S (25F)",
                    Url = "http://www.arrowhead-equipment.com/store/p510/Anniversary_Jarbidge_UnderQuilt.htmll",
                    WeightInGrams = 566,
                    CostInUSDP = 7500,
                    Note = "6oz APEX Climashield synthetic"
                },
                new GearItem(state.Settings)
                {
                    Name = "Toilet Paper",
                    IsConsumable = true,
                    ConsumedPerDay = 10,
                    WeightInGrams = 1,
                    CostInUSDP = 1,
                    Note = "Can't have too much!"
                },
                new GearItem(state.Settings)
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
                new GearItem(state.Settings)
                {
                    Name = "5g Water Jug",
                    Carried = GearCarried.NotCarried
                },
                new GearItem(state.Settings)
                {
                    Name = "Head Lamp",
                    Make = "Petzl",
                    Model = "Tikka Plus 2",
                    WeightInGrams = 79,
                    CostInUSDP = 2995
                },
                new GearItem(state.Settings)
                {
                    Name = "Alcohol Stove",
                    Make = "Zelph's Stoveworks",
                    Model = "StarLyte",
                    Url = "http://www.woodgaz-stove.com/starlyte-burner-with-lid.php",
                    WeightInGrams = 19,
                    CostInUSDP = 1300,
                },
                new GearItem(state.Settings)
                {
                    Name = "Wind Screen",
                    Make = "Trail Designs",
                    Model = "Caldera Cone System",
                    Url = "http://www.traildesigns.com/stoves/caldera-cone-system",
                    WeightInGrams = 141,
                    CostInUSDP = 3400
                }
            };

            Logger.Debug("Inserting test gear items...");
            await DatabaseItem.InsertItemsAsync(state, gearItems);
#endregion

#region Test Gear Systems
            var gearSystems = new List<GearSystem>
            {
                new GearSystem(state.Settings)
                {
                    Name = "New Hammock Setup",
                    GearItems = new List<GearSystemGearItem>
                    {
                        // Hammock
                        new GearSystemGearItem
                        {
                            GearItemId = 2,
                            Count = 1
                        },

                        // Tree Straps
                        new GearSystemGearItem
                        {
                            GearItemId = 3,
                            Count = 1
                        },

                        // Overquilt
                        new GearSystemGearItem
                        {
                            GearItemId = 5,
                            Count = 1
                        },

                        // New Underquilt
                        new GearSystemGearItem
                        {
                            GearItemId = 6,
                            Count = 1
                        }
                    },
                    Note = "3 season"
                },
                new GearSystem(state.Settings)
                {
                    Name = "Old Hammock Setup",
                    GearItems = new List<GearSystemGearItem>
                    {
                        // Hammock
                        new GearSystemGearItem
                        {
                            GearItemId = 2,
                            Count = 1
                        },

                        // Tree Straps
                        new GearSystemGearItem
                        {
                            GearItemId = 3,
                            Count = 1
                        },

                        // Old Underquilt
                        new GearSystemGearItem
                        {
                            GearItemId = 4,
                            Count = 1
                        },

                        // Overquilt
                        new GearSystemGearItem
                        {
                            GearItemId = 5,
                            Count = 1
                        }
                    },
                    Note = "3 season"
                },
                new GearSystem(state.Settings)
                {
                    Name = "Car Camping",
                    GearItems = new List<GearSystemGearItem>
                    {
                        // 5g Water Jug
                        new GearSystemGearItem
                        {
                            GearItemId = 9,
                            Count = 2
                        }
                    },
                    Note = "Leave this junk in the car"
                },
                new GearSystem(state.Settings)
                {
                    Name = "Cook System",
                    GearItems = new List<GearSystemGearItem>
                    {
                        // Alcohol Stove
                        new GearSystemGearItem
                        {
                            GearItemId = 11,
                            Count = 1
                        },

                        // Wind Screen
                        new GearSystemGearItem
                        {
                            GearItemId = 12,
                            Count = 1
                        }
                    }
                }
            };

            Logger.Debug("Inserting test gear systems...");
            await DatabaseItem.InsertItemsAsync(state, gearSystems);
#endregion

#region Test Gear Collections
            var gearCollections = new List<GearCollection>
            {
                new GearCollection(state.Settings)
                {
                    Name = "3 Season Hammock",
                    GearSystems = new List<GearCollectionGearSystem>
                    {
                        // New Hammock Setup
                        new GearCollectionGearSystem
                        {
                            GearSystemId = 1,
                            Count = 1
                        },

                        // Cook System
                        new GearCollectionGearSystem
                        {
                            GearSystemId = 4,
                            Count = 1
                        }
                    },
                    GearItems = new List<GearCollectionGearItem>
                    {
                        // Backpack
                        new GearCollectionGearItem
                        {
                            GearItemId = 1,
                            Count = 1
                        },

                        // Head Lamp
                        new GearCollectionGearItem
                        {
                            GearItemId = 10,
                            Count = 1
                        }
                    },
                    Note = "Test Collection"
                }
            };

            Logger.Debug("Inserting test gear collections...");
            await DatabaseItem.InsertItemsAsync(state, gearCollections);
#endregion

#region Test Meals
            var meals = new List<Meal>
            {
                new Meal(state.Settings)
                {
                    Name = "Cheesy Chicken Dinner",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 300,
                    ProteinInGrams = 20,
                    FiberInGrams = 5,
                    WeightInGrams = 300,
                    CostInUSDP = 10
                }
            };

            Logger.Debug("Inserting test meals...");
            await DatabaseItem.InsertItemsAsync(state, meals);
#endregion

#region Test Trip Itineraries
            var tripItineraries = new List<TripItinerary>
            {
                new TripItinerary(state.Settings)
                {
                    Name = "Turkey Camp 2015",
                    Note = "Looks like an easy hike!"
                }
            };

            Logger.Debug("Inserting test trip itineraries...");
            await DatabaseItem.InsertItemsAsync(state, tripItineraries);
#endregion

#region Test Trip Plans
            var tripPlans = new List<TripPlan>
            {
                new TripPlan(state.Settings)
                {
                    Name = "Turkey Camp 2015",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,

                    // Turkey Camp 2015
                    TripItineraryId = 1,

                    GearCollections = new List<TripPlanGearCollection>
                    {
                        // 3 Season Hammock
                        new TripPlanGearCollection
                        {
                            GearCollectionId = 1,
                            Count = 1
                        }
                    },
                    GearSystems = new List<TripPlanGearSystem>
                    {
                        // Cook System
                        new TripPlanGearSystem
                        {
                            GearSystemId = 4,
                            Count = 1
                        }
                    },
                    GearItems = new List<TripPlanGearItem>
                    {
                        // 5g Water Jug
                        new TripPlanGearItem
                        {
                            GearItemId = 9,
                            Count = 1
                        }
                    },
                    Meals = new List<TripPlanMeal>
                    {
                        // Cheese Chicken Dinner
                        new TripPlanMeal
                        {
                            MealId = 1,
                            Count = 1
                        }
                    }
                }
            };

            Logger.Debug("Inserting test trip plans...");
            await DatabaseItem.InsertItemsAsync(state, tripPlans);
#endregion

            stopwatch.Stop();
            Logger.Debug($"Finished populating test data in {stopwatch.ElapsedMilliseconds}ms");
#else
            await Task.Delay(0).ConfigureAwait(false);
#endif
        }
    }
}
