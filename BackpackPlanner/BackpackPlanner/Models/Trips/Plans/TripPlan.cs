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
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using SQLite;

namespace EnergonSoftware.BackpackPlanner.Models.Trips.Plans
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TripPlan : DatabaseItem, IBackpackPlannerItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(TripPlan));

#region Database Init
        /// <summary>
        /// Initializes the trip plans tables in the database.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <param name="oldVersion">The old database version.</param>
        /// <param name="newVersion">The new database version.</param>
        /// <remarks>
        /// The connection should be thread locked
        /// </remarks>
        public static async Task InitDatabaseAsync(BackpackPlannerState state, int oldVersion, int newVersion)
        {
            ValidateState(state);
            
            if(oldVersion >= newVersion) {
                Logger.Debug("Database versions match, nothing to do for trip plan tables...");
                return;
            }

            if(oldVersion < 2 && newVersion >= 2) {
                await CreateTablesAsync(state).ConfigureAwait(false);
            }
        }

        private static async Task CreateTablesAsync(BackpackPlannerState state)
        {
            Logger.Debug("Creating trip plan table...");
            await state.DatabaseState.Connection.AsyncConnection.CreateTableAsync<TripPlan>().ConfigureAwait(false);

            await TripPlanGearCollection.CreateTablesAsync(state).ConfigureAwait(false);
            await TripPlanGearSystem.CreateTablesAsync(state).ConfigureAwait(false);
            await TripPlanGearItem.CreateTablesAsync(state).ConfigureAwait(false);
            await TripPlanMeal.CreateTablesAsync(state).ConfigureAwait(false);
        }
#endregion

#region Properties
        [Ignore]
        public override int Id { get { return TripPlanId; } set { TripPlanId = value; } }

        /// <summary>
        /// Gets or sets the trip plan identifier.
        /// </summary>
        /// <value>
        /// The trip plan identifier.
        /// </value>
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int TripPlanId { get; set; } = -1;

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the trip plan name.
        /// </summary>
        /// <value>
        /// The trip plan name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name
        {
            get { return _name; }
            set { _name = value ?? string.Empty; }
        }

        /// <summary>
        /// Gets or sets the start date of the trip plan.
        /// </summary>
        /// <value>
        /// The start date of the trip plan.
        /// </value>
        public DateTime StartDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the end date of the trip plan.
        /// </summary>
        /// <value>
        /// The end date of the trip plan.
        /// </value>
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Ignore]
        public int Days => (StartDate - EndDate).Days;

        /// <summary>
        /// Gets or sets the trip itinerary identifier.
        /// </summary>
        /// <value>
        /// The trip itinerary identifier.
        /// </value>
        public int TripItineraryId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the trip itinerary.
        /// </summary>
        /// <value>
        /// The trip itinerary.
        /// </value>
        public TripItinerary TripItinerary { get; set; }

        private List<TripPlanGearCollection> _gearCollections = new List<TripPlanGearCollection>();

        /// <summary>
        /// Gets or sets the gear collections contained in this plan.
        /// </summary>
        /// <value>
        /// The gear collections contained in this plan.
        /// </value>
        public List<TripPlanGearCollection> GearCollections
        {
            get { return _gearCollections; }
            set { _gearCollections = value ?? new List<TripPlanGearCollection>(); }
        }

        private List<TripPlanGearSystem> _gearSystems = new List<TripPlanGearSystem>();

        /// <summary>
        /// Gets or sets the gear systems contained in this plan.
        /// </summary>
        /// <value>
        /// The gear systems contained in this plan.
        /// </value>
        public List<TripPlanGearSystem> GearSystems
        {
            get { return _gearSystems; }
            set { _gearSystems = value ?? new List<TripPlanGearSystem>(); }
        }

        private List<TripPlanGearItem> _gearItems = new List<TripPlanGearItem>();

        /// <summary>
        /// Gets or sets the gear items contained in this plan.
        /// </summary>
        /// <value>
        /// The gear items contained in this plan.
        /// </value>
        public List<TripPlanGearItem> GearItems
        {
            get { return _gearItems; }
            set { _gearItems = value ?? new List<TripPlanGearItem>(); }
        }

        private List<TripPlanMeal> _meals = new List<TripPlanMeal>();

        /// <summary>
        /// Gets or sets the meals contained in this plan.
        /// </summary>
        /// <value>
        /// The meals contained in this plan.
        /// </value>
        public List<TripPlanMeal> Meals
        {
            get { return _meals; }
            set { _meals = value ?? new List<TripPlanMeal>(); }
        }

        private string _note = string.Empty;

        /// <summary>
        /// Gets or sets the trip plan note.
        /// </summary>
        /// <value>
        /// The trip plan note.
        /// </value>
        [MaxLength(1024)]
        public string Note
        {
            get { return _note; }
            set { _note = value ?? string.Empty; }
        }

        [Ignore]
        public WeightClass WeightClass => Settings?.GetWeightClass(GetBaseWeightInGrams()) ?? WeightClass.Traditional;
#endregion

        public TripPlan()
        {
        }

        public TripPlan(BackpackPlannerSettings settings)
            : base(settings)
        {
        }

        public int GetTotalGearItemCount()
        {
            var visitedGearItems = new List<int>();
            return IGearItemContainerExtensions.GetGearItemCount(_gearCollections, visitedGearItems)
                + IGearItemContainerExtensions.GetGearItemCount(_gearSystems, visitedGearItems)
                + GetGearItemCount(visitedGearItems);
        }

        public int GetTotalCalories()
        {
            var visitedMeals = new List<int>();
            return TripPlanMeal.GetTotalCalories(_meals, visitedMeals);
        }

#region Gear Collections
        public int GetGearCollectionCount(List<int> visitedGearCollections=null)
        {
            return TripPlanGearCollection.GetGearCollectionCount(_gearCollections, visitedGearCollections);
        } 
#endregion

#region Gear Systems
        public int GetGearSystemCount(List<int> visitedGearSystems=null)
        {
            return TripPlanGearSystem.GetGearSystemCount(_gearSystems, visitedGearSystems);
        } 
#endregion

#region Gear Items
        public int GetGearItemCount(List<int> visitedGearItems=null)
        {
            return TripPlanGearItem.GetGearItemCount(_gearItems, visitedGearItems);
        }
#endregion

#region Meals
        public int GetMealCount(List<int> visitedMeals=null)
        {
            return TripPlanMeal.GetMealCount(_meals, visitedMeals);
        }
#endregion

#region Weight
        public int GetTotalWeightInGrams(List<int> visitedGearItems=null, List<int> visitedMeals=null)
        {
            return TripPlanGearCollection.GetTotalWeightInGrams(_gearCollections, visitedGearItems)
                + TripPlanGearSystem.GetTotalWeightInGrams(_gearSystems, visitedGearItems)
                + TripPlanGearItem.GetTotalWeightInGrams(_gearItems, visitedGearItems)
                + TripPlanMeal.GetTotalWeightInGrams(_meals, visitedMeals);
        }

        public float GetTotalWeightInUnits()
        {
            int weightInGrams = GetTotalWeightInGrams();
            return Settings?.Units.WeightFromGrams(weightInGrams) ?? weightInGrams;
        }

        public int GetBaseWeightInGrams()
        {
            // TODO: calculate this
            return 0;
        }

        public float GetBaseWeightInUnits()
        {
            // TODO: calculate this
            return 0.0f;
        }

        public float GetPackWeightInUnits()
        {
            // TODO: calculate this
            return 0.0f;
        }

        public float GetSkinOutWeightInUnits()
        {
            // TODO: calculate this
            return 0.0f;
        }
#endregion

#region Cost
        public int GetTotalCostInUSDP(List<int> visitedGearItems=null, List<int> visitedMeals=null)
        {
            return TripPlanGearCollection.GetTotalCostInUSDP(_gearCollections, visitedGearItems)
                + TripPlanGearSystem.GetTotalCostInUSDP(_gearSystems, visitedGearItems)
                + TripPlanGearItem.GetTotalCostInUSDP(_gearItems, visitedGearItems)
                + TripPlanMeal.GetTotalCostInUSDP(_meals, visitedMeals);
        }

        public float GetTotalCostInCurrency()
        {
            int costInUSDP = GetTotalCostInUSDP();
            return Settings?.Currency.CurrencyFromUSDP(costInUSDP) ?? costInUSDP;
        }

        public float GetCostPerWeightInCurrency()
        {
            float weightInUnits = GetTotalWeightInUnits();
            float costInCurrency = GetTotalCostInCurrency();

            return 0.0f == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }
#endregion

        public override bool Equals(object obj)
        {
            if(TripPlanId < 1) {
                return false;
            }

            TripPlan tripPlan = obj as TripPlan;
            return TripPlanId == tripPlan?.TripPlanId;
        }

        public override int GetHashCode()
        {
            return TripPlanId.GetHashCode();
        }
    }
}
