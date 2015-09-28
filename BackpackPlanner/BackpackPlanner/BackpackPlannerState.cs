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
        public SQLiteDatabaseConnection DatabaseConnection { get; } = new SQLiteDatabaseConnection();

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
            DatabaseVersion oldVersion;

            await DatabaseConnection.LockAsync().ConfigureAwait(false);
            try {
                var databaseVersionTableInfo = DatabaseConnection.Connection.GetTableInfo("DatabaseVersion");
                if(!databaseVersionTableInfo.Any()) {
                    Logger.Debug("Creating a new database...");
                    await DatabaseVersion.CreateTablesAsync(DatabaseConnection.AsyncConnection).ConfigureAwait(false);
                }

                oldVersion = await DatabaseVersion.GetAsync(DatabaseConnection.AsyncConnection).ConfigureAwait(false);
                if(null == oldVersion) {
                    Logger.Debug("DatabaseVersion.Get returned null!?!");
                    throw new InvalidOperationException("DatabaseVersion.Get returned null!?!");
                }
                Logger.Debug($"Old database version: {oldVersion.Version}, current database version: {CurrentDatabaseVersion}");

                // TODO: find a way to make this transactional
                // so that we can roll it back on error and avoid updating the db version
                await GearItem.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await GearSystem.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await GearCollection.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await Meal.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await TripItinerary.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);
                await TripPlan.InitDatabaseAsync(DatabaseConnection.AsyncConnection, oldVersion.Version, newVersion.Version).ConfigureAwait(false);

                newVersion.DatabaseVersionId = oldVersion.DatabaseVersionId;
                await DatabaseVersion.UpdateAsync(DatabaseConnection.AsyncConnection, newVersion).ConfigureAwait(false);
            } finally {
                DatabaseConnection.Release();
            }

            if(oldVersion.Version < 1) {
                await PopulateInitialDatabaseAsync().ConfigureAwait(false);
            }
        }

        private static async Task PopulateInitialDatabaseAsync()
        {
#if DEBUG
            Logger.Debug("Populating test data...");

#region Test Gear Items
            Logger.Debug("Inserting test gear items...");
            await DatabaseItem.InsertItemsAsync(new List<GearItem>
                {
                    new GearItem
                    {
                        Name = "Alcohol Stove",
                        Make = "Zelph's Stoveworks",
                        Model = "StarLyte",
                        Url = "http://www.woodgaz-stove.com/starlyte-burner-with-lid.php",
                        WeightInGrams = 19,
                        CostInUSDP = 1300,
                    },
                    new GearItem
                    {
                        Name = "Backpack",
                        Make = "ULA",
                        Model = "Circuit",
                        Url = "http://www.ula-equipment.com/product_p/circuit.htm",
                        WeightInGrams = 986,
                        CostInUSDP = 22500,
                        Note = "Medium torso (18\" - 21\"). Medium hipbelt (34\" - 38\"). J-Curve shoulder strap. Aluminum stay removed. Includes hanging s-biner \"Ahhh\" and water shoe carabiner. 39L main body. Max 15 pound base weight, 30-35 pack weight."
                    },
                    new GearItem
                    {
                        Name = "Hammock",
                        Make = "Aaron Erbe",
                        Model = "DIY",
                        WeightInGrams = 422,
                        CostInUSDP = 0,
                        Note = "Includes adjustable ridge line and 2x whoopie slings from whoopieslings.com, and bishop bag."
                    },
                    new GearItem
                    {
                        Name = "Head Lamp",
                        Make = "Petzl",
                        Model = "Tikka Plus 2",
                        WeightInGrams = 79,
                        CostInUSDP = 2995
                    },
                    new GearItem
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
                    new GearItem
                    {
                        Name = "New Underquilt",
                        Make = "Arrowhead Equipment",
                        Model = "Anniversary Jarbridge 3S (25F)",
                        Url = "http://www.arrowhead-equipment.com/store/p510/Anniversary_Jarbidge_UnderQuilt.htmll",
                        WeightInGrams = 566,
                        CostInUSDP = 7500,
                        Note = "6oz APEX Climashield synthetic"
                    },
                    new GearItem
                    {
                        Name = "Old Underquilt",
                        Make = "Aaron Erbe",
                        Model = "DIY",
                        WeightInGrams = 887,
                        CostInUSDP = 0,
                        Note = "Synthetic material. Need to have Aaron or Joe possibly remove some material from the overstuff collars to get the size and weight down on this."
                    },
                    new GearItem
                    {
                        Name = "Overquilt",
                        Make = "Arrowhead Equipment",
                        Model = "Owyhee Top Quilt Regular 3S (25F)",
                        Url = "http://www.arrowhead-equipment.com/store/p314/Owyhee_Top_Quilt_Regular.html",
                        WeightInGrams = 802,
                        CostInUSDP = 17900,
                        Note = "6oz APEX Climashield synthetic"
                    },
                    new GearItem
                    {
                        Name = "Toilet Paper",
                        IsConsumable = true,
                        ConsumedPerDay = 10,
                        WeightInGrams = 1,
                        CostInUSDP = 1,
                        Note = "Can't have too much!"
                    },
                    new GearItem
                    {
                        Name = "Tree Straps",
                        Url = "http://shop.whoopieslings.com/Tree-Huggers-TH.htm",
                        WeightInGrams = 198,
                        CostInUSDP = 1200,
                        Note = "12'x1\". Includes dutch buckle and titanium dutch clip (max 300 pounds)."
                    },
                    new GearItem
                    {
                        Name = "5g Water Jug",
                        Carried = GearCarried.NotCarried
                    },
                    new GearItem
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
            await DatabaseItem.InsertItemsAsync(new List<GearSystem>
                {
                    new GearSystem
                    {
                        Name = "One"
                    },
                    new GearSystem
                    {
                        Name = "Two"
                    },
                    new GearSystem
                    {
                        Name = "Three"
                    },
                    new GearSystem
                    {
                        Name = "Four"
                    },
                    new GearSystem
                    {
                        Name = "Five"
                    },
                    new GearSystem
                    {
                        Name = "Six"
                    },
                    new GearSystem
                    {
                        Name = "Seven"
                    },
                    new GearSystem
                    {
                        Name = "Eight"
                    },
                    new GearSystem
                    {
                        Name = "Nine"
                    },
                    new GearSystem
                    {
                        Name = "Ten"
                    }
                }
            );
#endregion

/*
#region Test Gear Collections
            ListItems.Add(new GearCollection
                {
                    Name = "One"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Two"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Three"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Four"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Five"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Six"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Seven"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Eight"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Nine"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Ten"
                }
            );
#endregion
*/
/*
#region Test Meals
            ListItems.Add(new Meal
                {
                    Name = "One",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 1,
                    CostInUSDP = 20
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Two",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 2,
                    CostInUSDP = 19
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Three",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 3,
                    CostInUSDP = 18
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Four",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 4,
                    CostInUSDP = 17
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Five",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 5,
                    CostInUSDP = 16
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Six",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 6,
                    CostInUSDP = 15
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Seven",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 7,
                    CostInUSDP = 14
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Eight",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 8,
                    CostInUSDP = 13
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Nine",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 9,
                    CostInUSDP = 12
                }
            );

            ListItems.Add(new Meal
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
            );
#endregion
*/
/*
#region Test Trip Itineraries
            ListItems.Add(new TripItinerary
                {
                    Name = "One"
                }
            );

            ListItems.Add(new TripItinerary
                {
                    Name = "Two"
                }
            );

            ListItems.Add(new TripItinerary
                {
                    Name = "Three"
                }
            );

            ListItems.Add(new TripItinerary
                {
                    Name = "Four"
                }
            );

            ListItems.Add(new TripItinerary
                {
                    Name = "Five"
                }
            );

            ListItems.Add(new TripItinerary
                {
                    Name = "Six"
                }
            );

            ListItems.Add(new TripItinerary
                {
                    Name = "Seven"
                }
            );

            ListItems.Add(new TripItinerary
                {
                    Name = "Eight"
                }
            );

            ListItems.Add(new TripItinerary
                {
                    Name = "Nine"
                }
            );

            ListItems.Add(new TripItinerary
                {
                    Name = "Ten"
                }
            );
#endregion
*/
/*
#region Test Trip Plans
            ListItems.Add(new TripPlan
                {
                    Name = "One"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Two"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Three"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Four"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Five"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Six"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Seven"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Eight"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Nine"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Ten"
                }
            );
#endregion
*/
            Logger.Debug("Finished populating test data!");
#endif
        }

        private BackpackPlannerState()
        {
        }
    }
}
