﻿/*
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
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DatabaseState
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DatabaseState));

        private sealed class LoggingProvider : Microsoft.Extensions.Logging.ILoggerProvider
        {
            private sealed class DatabaseLogger : Microsoft.Extensions.Logging.ILogger
            {
                public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
                {
#if DEBUG
                    return true;
#else
                    return false;
#endif
                }

                public void Log<T>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, T state, Exception exception, Func<T, Exception, string> formatter)
                {
                    Logger.Debug(formatter(state, exception));
                }

                public IDisposable BeginScope<T>(T state)
                {
                    return null;
                }
            }

            public void Dispose()
            {
            }

            public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
            {
                return new DatabaseLogger();
            }
        }

        /// <summary>
        /// The database name
        /// </summary>
        public const string DatabaseName = "BackpackPlanner.db";

        private bool _isInitialized;

        /// <summary>
        /// The database file path
        /// </summary>
        public string DatabasePath { get; private set; }

        public DatabaseContext CreateContext()
        {
            return new DatabaseContext(DatabasePath);
        }

        /// <summary>
        /// Initializes the database state.
        /// </summary>
        /// <param name="dbPath">The database path.</param>
        public void Init(string dbPath)
        {
            DatabasePath = Path.Combine(dbPath, DatabaseName);

            if(_isInitialized) {
                return;
            }

            using(DatabaseContext dbContext = CreateContext()) {
                Microsoft.Extensions.Logging.ILoggerFactory loggerFactory = dbContext.GetService<Microsoft.Extensions.Logging.ILoggerFactory>();
                loggerFactory.AddProvider(new LoggingProvider());
            }

            _isInitialized = true;
        }

        /// <summary>
        /// Migrates the database.
        /// </summary>
        /// <param name="state">The system state.</param>
        public async Task<bool> MigrateAsync(BackpackPlannerState state)
        {
            using(DatabaseContext dbContext = CreateContext()) {
                Logger.Info("Migrating database...");
                try {
                    await dbContext.Database.MigrateAsync().ConfigureAwait(false);
                } catch(Exception e) {
                    Logger.Error($"Unable to migrate database: {e.Message}!", e);
                    return false;
                }

                try {
                    await PopulateInitialDatabaseAsync(dbContext, state).ConfigureAwait(false);
                } catch(Exception e) {
                    Logger.Error($"Unable to populate initial database: {e.Message}", e);
                    return false;
                }
            }
            return true;
        }

#if DEBUG
        private async Task SaveChangesAsync(DatabaseContext dbContext)
        {
            Logger.Debug("Saving changes...");
            int count = await dbContext.SaveChangesAsync().ConfigureAwait(false);
            Logger.Debug($"Saved {count} objects!");
        }
#endif

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
                    Name = "Tree Straps",
                    Url = "http://shop.whoopieslings.com/Tree-Huggers-TH.htm",
                    WeightInGrams = 198,
                    CostInUSDP = 1200,
                    Note = "12'x1\". Includes dutch buckle and titanium dutch clip (max 300 pounds)."
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
                    Name = "Toilet Paper",
                    IsConsumable = true,
                    ConsumedPerDay = 10,
                    WeightInGrams = 1,
                    CostInUSDP = 1,
                    Note = "Can't have too much!"
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
                    Name = "5g Water Jug",
                    Carried = GearCarried.NotCarried
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
                    Name = "Alcohol Stove",
                    Make = "Zelph's Stoveworks",
                    Model = "StarLyte",
                    Url = "http://www.woodgaz-stove.com/starlyte-burner-with-lid.php",
                    WeightInGrams = 19,
                    CostInUSDP = 1300,
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
            };

            Logger.Debug("Inserting test gear items...");
            await dbContext.GearItems.AddRangeAsync(gearItems).ConfigureAwait(false);
            await SaveChangesAsync(dbContext).ConfigureAwait(false);
#endregion

#region Test Gear Systems
            var gearSystems = new List<GearSystem>
            {
                new GearSystem
                {
                    Name = "New Hammock Setup",
                    Note = "3 season"
                },
                new GearSystem
                {
                    Name = "Old Hammock Setup",
                    Note = "3 season"
                },
                new GearSystem
                {
                    Name = "Car Camping",
                    Note = "Leave this junk in the car"
                },
                new GearSystem
                {
                    Name = "Cook System"
                }
            };

            // New Hammock Setup
            gearSystems[0].SetGearItems(dbContext,
                new List<GearItemEntry<GearSystem>>
                {
                    // Hammock
                    new GearItemEntry<GearSystem>(gearItems[1])
                    {
                        Count = 1
                    },

                    // Tree Straps
                    new GearItemEntry<GearSystem>(gearItems[2])
                    {
                        Count = 1
                    },

                    // Overquilt
                    new GearItemEntry<GearSystem>(gearItems[4])
                    {
                        Count = 1
                    },

                    // New Underquilt
                    new GearItemEntry<GearSystem>(gearItems[5])
                    {
                        Count = 1
                    }
                }
            );

            // Old Hammock Setup
            gearSystems[1].SetGearItems(dbContext,
                new List<GearItemEntry<GearSystem>>
                {
                    // Hammock
                    new GearItemEntry<GearSystem>(gearItems[1])
                    {
                        Count = 1
                    },

                    // Tree Straps
                    new GearItemEntry<GearSystem>(gearItems[2])
                    {
                        Count = 1
                    },

                    // Old Underquilt
                    new GearItemEntry<GearSystem>(gearItems[3])
                    {
                        Count = 1
                    },

                    // Overquilt
                    new GearItemEntry<GearSystem>(gearItems[4])
                    {
                        Count = 1
                    }
                }
            );

            // Car Camping
            gearSystems[2].SetGearItems(dbContext,
                new List<GearItemEntry<GearSystem>>
                {
                    // 5g Water Jug
                    new GearItemEntry<GearSystem>(gearItems[8])
                    {
                        Count = 2
                    }
                }
            );

            // Cook System
            gearSystems[3].SetGearItems(dbContext,
                new List<GearItemEntry<GearSystem>>
                {
                    // Alcohol Stove
                    new GearItemEntry<GearSystem>(gearItems[10])
                    {
                        Count = 1
                    },

                    // Wind Screen
                    new GearItemEntry<GearSystem>(gearItems[11])
                    {
                        Count = 1
                    }
                }
            );

            Logger.Debug("Inserting test gear systems...");
            await dbContext.GearSystems.AddRangeAsync(gearSystems).ConfigureAwait(false);
            await SaveChangesAsync(dbContext).ConfigureAwait(false);
#endregion

#region Test Gear Collections
            var gearCollections = new List<GearCollection>
            {
                new GearCollection
                {
                    Name = "3 Season Hammock",
                    Note = "Test Collection"
                }
            };

            // 3 Season Hammock
            gearCollections[0].SetGearSystems(dbContext,
                new List<GearSystemEntry<GearCollection>>
                {
                    // New Hammock Setup
                    new GearSystemEntry<GearCollection>(gearSystems[0])
                    {
                        Count = 1
                    },

                    // Cook System
                    new GearSystemEntry<GearCollection>(gearSystems[3])
                    {
                        Count = 1
                    }
                }
            );

            gearCollections[0].SetGearItems(dbContext,
                new List<GearItemEntry<GearCollection>>
                {
                    // Backpack
                    new GearItemEntry<GearCollection>(gearItems[0])
                    {
                        Count = 1
                    },

                    // Head Lamp
                    new GearItemEntry<GearCollection>(gearItems[9])
                    {
                        Count = 1
                    }
                }
            );

            Logger.Debug("Inserting test gear collections...");
            await dbContext.GearCollections.AddRangeAsync(gearCollections).ConfigureAwait(false);
            await SaveChangesAsync(dbContext).ConfigureAwait(false);
#endregion

#region Test Meals
            var meals = new List<Meal>
            {
                new Meal
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
            await dbContext.Meals.AddRangeAsync(meals).ConfigureAwait(false);
            await SaveChangesAsync(dbContext).ConfigureAwait(false);
#endregion

#region Test Trip Itineraries
            var tripItineraries = new List<TripItinerary>
            {
                new TripItinerary
                {
                    Name = "Turkey Camp 2015",
                    Note = "Looks like an easy hike!"
                }
            };

            Logger.Debug("Inserting test trip itineraries...");
            await dbContext.TripItineraries.AddRangeAsync(tripItineraries).ConfigureAwait(false);
            await SaveChangesAsync(dbContext).ConfigureAwait(false);
#endregion

#region Test Trip Plans
            var tripPlans = new List<TripPlan>
            {
                new TripPlan
                {
                    Name = "Turkey Camp 2015",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                }
            };

            // Turkey Camp 2015
            tripPlans[0].SetTripItinerary(dbContext, tripItineraries[0]);

            tripPlans[0].SetGearCollections(dbContext,
                new List<GearCollectionEntry<TripPlan>>
                {
                    // 3 Season Hammock
                    new GearCollectionEntry<TripPlan>(gearCollections[0])
                    {
                        Count = 1
                    }
                }
            );

            tripPlans[0].SetGearSystems(dbContext,
                new List<GearSystemEntry<TripPlan>>
                {
                    // Cook System
                    new GearSystemEntry<TripPlan>(gearSystems[3])
                    {
                        Count = 1
                    }
                }
            );

            tripPlans[0].SetGearItems(dbContext,
                new List<GearItemEntry<TripPlan>>
                {
                    // 5g Water Jug
                    new GearItemEntry<TripPlan>(gearItems[8])
                    {
                        Count = 1
                    }
                }
            );

            tripPlans[0].SetMeals(dbContext,
                new List<MealEntry<TripPlan>>
                {
                    // Cheese Chicken Dinner
                    new MealEntry<TripPlan>(meals[0])
                    {
                        Count = 1
                    }
                }
            );

            Logger.Debug("Inserting test trip plans...");
            await dbContext.TripPlans.AddRangeAsync(tripPlans).ConfigureAwait(false);
            await SaveChangesAsync(dbContext).ConfigureAwait(false);
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
