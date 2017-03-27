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
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DatabaseState
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DatabaseState));

        /// <summary>
        /// The database name
        /// </summary>
        public const string DatabaseName = "BackpackPlanner.db";

        /// <summary>
        /// Connects the library database connections.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <param name="dbPath">The database path.</param>
        /// <param name="dbName">Name of the database.</param>
        public async Task InitAsync(BackpackPlannerState state, string dbPath, string dbName)
        {
            string fullPath = Path.Combine(dbPath, dbName);
            Logger.Info($"Connecting to database at {fullPath}...");
            using(DatabaseContext dbContext = new DatabaseContext(fullPath)) {
                Logger.Info("Migrating database...");
                await dbContext.Database.MigrateAsync().ConfigureAwait(false);

                await PopulateInitialDatabaseAsync(dbContext, state).ConfigureAwait(false);
            }
        }

        private async Task PopulateInitialDatabaseAsync(DatabaseContext dbContext, BackpackPlannerState state)
        {
#if DEBUG
            if(state.Settings.MetaSettings.TestDataEntered) {
                return;
            }

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
            dbContext.GearItems.AddRange(gearItems);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
#endregion

#region Test Gear Systems
            var gearSystems = new List<GearSystem>
            {
                new GearSystem(state.Settings)
                {
                    Name = "New Hammock Setup",
                    TestGearItems = new List<GearItemEntry>
                    {
                        // Hammock
                        new GearItemEntry
                        {
                            GearItemId = 2,
                            Count = 1
                        },

                        // Tree Straps
                        new GearItemEntry
                        {
                            GearItemId = 3,
                            Count = 1
                        },

                        // Overquilt
                        new GearItemEntry
                        {
                            GearItemId = 5,
                            Count = 1
                        },

                        // New Underquilt
                        new GearItemEntry
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
                    TestGearItems = new List<GearItemEntry>
                    {
                        // Hammock
                        new GearItemEntry
                        {
                            GearItemId = 2,
                            Count = 1
                        },

                        // Tree Straps
                        new GearItemEntry
                        {
                            GearItemId = 3,
                            Count = 1
                        },

                        // Old Underquilt
                        new GearItemEntry
                        {
                            GearItemId = 4,
                            Count = 1
                        },

                        // Overquilt
                        new GearItemEntry
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
                    TestGearItems = new List<GearItemEntry>
                    {
                        // 5g Water Jug
                        new GearItemEntry
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
                    TestGearItems = new List<GearItemEntry>
                    {
                        // Alcohol Stove
                        new GearItemEntry
                        {
                            GearItemId = 11,
                            Count = 1
                        },

                        // Wind Screen
                        new GearItemEntry
                        {
                            GearItemId = 12,
                            Count = 1
                        }
                    }
                }
            };

            Logger.Debug("Inserting test gear systems...");
            dbContext.GearSystems.AddRange(gearSystems);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
#endregion

#region Test Gear Collections
            var gearCollections = new List<GearCollection>
            {
                new GearCollection(state.Settings)
                {
                    Name = "3 Season Hammock",
                    TestGearSystems = new List<GearSystemEntry>
                    {
                        // New Hammock Setup
                        new GearSystemEntry
                        {
                            GearSystemId = 1,
                            Count = 1
                        },

                        // Cook System
                        new GearSystemEntry
                        {
                            GearSystemId = 4,
                            Count = 1
                        }
                    },
                    TestGearItems = new List<GearItemEntry>
                    {
                        // Backpack
                        new GearItemEntry
                        {
                            GearItemId = 1,
                            Count = 1
                        },

                        // Head Lamp
                        new GearItemEntry
                        {
                            GearItemId = 10,
                            Count = 1
                        }
                    },
                    Note = "Test Collection"
                }
            };

            Logger.Debug("Inserting test gear collections...");
            dbContext.GearCollections.AddRange(gearCollections);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
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
            dbContext.Meals.AddRange(meals);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
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
            dbContext.TripItineraries.AddRange(tripItineraries);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
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
                    TestTripItineraryId = 1,

                    TestGearCollections = new List<GearCollectionEntry>
                    {
                        // 3 Season Hammock
                        new GearCollectionEntry
                        {
                            GearCollectionId = 1,
                            Count = 1
                        }
                    },
                    TestGearSystems = new List<GearSystemEntry>
                    {
                        // Cook System
                        new GearSystemEntry
                        {
                            GearSystemId = 4,
                            Count = 1
                        }
                    },
                    TestGearItems = new List<GearItemEntry>
                    {
                        // 5g Water Jug
                        new GearItemEntry
                        {
                            GearItemId = 9,
                            Count = 1
                        }
                    },
                    TestMeals = new List<MealEntry>
                    {
                        // Cheese Chicken Dinner
                        new MealEntry
                        {
                            MealId = 1,
                            Count = 1
                        }
                    }
                }
            };

            Logger.Debug("Inserting test trip plans...");
            dbContext.TripPlans.AddRange(tripPlans);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
#endregion

            state.Settings.MetaSettings.TestDataEntered = true;

            stopwatch.Stop();
            Logger.Debug($"Finished populating test data in {stopwatch.ElapsedMilliseconds}ms");
#else
            await Task.Delay(0).ConfigureAwait(false);
#endif
        }
    }
}
