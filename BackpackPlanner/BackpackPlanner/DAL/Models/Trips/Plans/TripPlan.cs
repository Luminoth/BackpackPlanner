﻿/*
   Copyright 2017 Shane Lillie

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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TripPlan : BaseModel<TripPlan>, IBackpackPlannerItem
    {
#region Static Helpers
        public static async Task<IReadOnlyCollection<TripPlan>> GetAll(DatabaseContext dbContext)
        {
            return await dbContext.TripPlans
                .Include(tripPlan => tripPlan.GearCollections)
                    .ThenInclude(gearCollection => gearCollection.Model)
                .Include(tripPlan => tripPlan.GearSystems)
                    .ThenInclude(gearSystem => gearSystem.Model)
                .Include(tripPlan => tripPlan.GearItems)
                    .ThenInclude(gearItem => gearItem.Model)
                .Include(tripPlan => tripPlan.Meals)
                    .ThenInclude(meal => meal.Model)
                .Include(tripPlan => tripPlan.TripItinerary)
                .ToListAsync().ConfigureAwait(false);
        }
#endregion

        public override int Id => TripPlanId;

#region Database Properties
        /// <summary>
        /// Gets or sets the trip plan identifier.
        /// </summary>
        /// <value>
        /// The trip plan identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripPlanId { get; private set; }

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the trip plan name.
        /// </summary>
        /// <value>
        /// The trip plan name.
        /// </value>
        [Required, MaxLength(64)]
        public string Name
        {
            get => _name;

            set
            {
                _name = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }

        private DateTime _startDate = DateTime.Now;

        /// <summary>
        /// Gets or sets the start date of the trip plan.
        /// </summary>
        /// <value>
        /// The start date of the trip plan.
        /// </value>
        public DateTime StartDate
        {
            get => _startDate;

            set
            {
                _startDate = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _endDateTime = DateTime.Now;

        /// <summary>
        /// Gets or sets the end date of the trip plan.
        /// </summary>
        /// <value>
        /// The end date of the trip plan.
        /// </value>
        public DateTime EndDate
        {
            get => _endDateTime;

            set
            {
                _endDateTime = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the trip itinerary identifier.
        /// </summary>
        /// <value>
        /// The trip itinerary identifier.
        /// </value>
        [ForeignKey("TripItinerary")]
        public int TripItineraryId { get; private set; }

        private TripItinerary _tripItinerary;

        /// <summary>
        /// Gets or sets the trip itinerary.
        /// </summary>
        /// <value>
        /// The trip itinerary.
        /// </value>
        public virtual TripItinerary TripItinerary
        {
            get => _tripItinerary;

            private set
            {
                _tripItinerary = value;
                TripItineraryId = _tripItinerary?.Id ?? 0;
                NotifyPropertyChanged();
            }
        }

        private readonly List<GearCollectionEntry<TripPlan>> _gearCollections = new List<GearCollectionEntry<TripPlan>>();

        /// <summary>
        /// Gets or sets the gear collections contained in this plan.
        /// </summary>
        /// <value>
        /// The gear collections contained in this plan.
        /// </value>
        public virtual IReadOnlyCollection<GearCollectionEntry<TripPlan>> GearCollections => _gearCollections;

        private readonly List<GearSystemEntry<TripPlan>> _gearSystems = new List<GearSystemEntry<TripPlan>>();

        /// <summary>
        /// Gets or sets the gear systems contained in this plan.
        /// </summary>
        /// <value>
        /// The gear systems contained in this plan.
        /// </value>
        public virtual IReadOnlyCollection<GearSystemEntry<TripPlan>> GearSystems => _gearSystems;

        private readonly List<GearItemEntry<TripPlan>> _gearItems = new List<GearItemEntry<TripPlan>>();

        /// <summary>
        /// Gets or sets the gear items contained in this plan.
        /// </summary>
        /// <value>
        /// The gear items contained in this plan.
        /// </value>
        public virtual IReadOnlyCollection<GearItemEntry<TripPlan>> GearItems => _gearItems;

        private readonly List<MealEntry<TripPlan>> _meals = new List<MealEntry<TripPlan>>();

        /// <summary>
        /// Gets or sets the meals contained in this plan.
        /// </summary>
        /// <value>
        /// The meals contained in this plan.
        /// </value>
        public virtual IReadOnlyCollection<MealEntry<TripPlan>> Meals => _meals;

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
            get => _note;

            set
            {
                _note = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }
#endregion

        public override TripPlan DeepCopy()
        {
            TripPlan tripPlan = base.DeepCopy();

            tripPlan.TripPlanId = TripPlanId;
            tripPlan.Name = Name;
            tripPlan.StartDate = StartDate;
            tripPlan.EndDate = EndDate;
            tripPlan.TripItineraryId = TripItineraryId;
            tripPlan.TripItinerary = TripItinerary;         // TODO: should we copy this as well? we shouldn't be able to edit it, so probably not?
            tripPlan.Note = Note;

            foreach(var gearCollection in GearCollections) {
                tripPlan._gearCollections.Add(gearCollection.DeepCopy());
            }

            foreach(var gearSystem in GearSystems) {
                tripPlan._gearSystems.Add(gearSystem.DeepCopy());
            }

            foreach(var gearItem in GearItems) {
                tripPlan._gearItems.Add(gearItem.DeepCopy());
            }

            foreach(var meal in Meals) {
                tripPlan._meals.Add(meal.DeepCopy());
            }

            return tripPlan;
        }

        [NotMapped, JsonIgnore]
        public int Days => (StartDate - EndDate).Days;

        public WeightClass GetWeightClass(BackpackPlannerSettings settings)
        {
            return settings.GetWeightClass(GetBaseWeightInGrams());
        }

        public int GetTotalGearItemCount()
        {
            var visitedGearItems = new List<int>();
            return GearItemContainerExtensions.GetGearItemCount(_gearCollections, visitedGearItems)
                + GearItemContainerExtensions.GetGearItemCount(_gearSystems, visitedGearItems)
                + GetGearItemCount(visitedGearItems);
        }

        public int GetTotalCalories()
        {
            var visitedMeals = new List<int>();
            return MealEntry<TripPlan>.GetTotalCalories(_meals, visitedMeals);
        }

#region Trip Itinerary
        public void SetTripItinerary(DatabaseContext dbContext, [CanBeNull] TripItinerary tripItinerary)
        {
            TripItinerary = tripItinerary;
            if(null != TripItinerary) {
                dbContext.Entry(TripItinerary).State = EntityState.Unchanged;
            }
        }
#endregion

#region Gear Collections
        public void SetGearCollections(DatabaseContext dbContext, [CanBeNull] IReadOnlyCollection<GearCollectionEntry<TripPlan>> gearCollections)
        {
            UpdateItemEntries<GearCollectionEntry<TripPlan>, GearCollection>(dbContext, _gearCollections, gearCollections);
            NotifyPropertyChanged(nameof(GearCollections));
        }

        public int GetGearCollectionCount(ICollection<int> visitedGearCollections=null)
        {
            return GearCollectionEntry<TripPlan>.GetGearCollectionCount(_gearCollections, visitedGearCollections);
        } 
#endregion

#region Gear Systems
        public void SetGearSystems(DatabaseContext dbContext, [CanBeNull] IReadOnlyCollection<GearSystemEntry<TripPlan>> gearSystems)
        {
            UpdateItemEntries<GearSystemEntry<TripPlan>, GearSystem>(dbContext, _gearSystems, gearSystems);
            NotifyPropertyChanged(nameof(GearSystems));
        }

        public int GetGearSystemCount(ICollection<int> visitedGearSystems=null)
        {
            return GearSystemEntry<TripPlan>.GetGearSystemCount(_gearSystems, visitedGearSystems);
        } 
#endregion

#region Gear Items
        public void SetGearItems(DatabaseContext dbContext, [CanBeNull] IReadOnlyCollection<GearItemEntry<TripPlan>> gearItems)
        {
            UpdateItemEntries<GearItemEntry<TripPlan>, GearItem>(dbContext, _gearItems, gearItems);
            NotifyPropertyChanged(nameof(GearItems));
        }

        public int GetGearItemCount(ICollection<int> visitedGearItems=null)
        {
            return GearItemEntry<TripPlan>.GetGearItemCount(_gearItems, visitedGearItems);
        }
#endregion

#region Meals
        public void SetMeals(DatabaseContext dbContext, [CanBeNull] IReadOnlyCollection<MealEntry<TripPlan>> meals)
        {
            UpdateItemEntries<MealEntry<TripPlan>, Meal>(dbContext, _meals, meals);
            NotifyPropertyChanged(nameof(Meals));
        }

        public int GetMealCount(ICollection<int> visitedMeals=null)
        {
            return MealEntry<TripPlan>.GetMealCount(_meals, visitedMeals);
        }
#endregion

#region Weight
        public int GetTotalWeightInGrams(ICollection<int> visitedGearItems=null, ICollection<int> visitedMeals=null)
        {
            return GearCollectionEntry<TripPlan>.GetTotalWeightInGrams(_gearCollections, visitedGearItems)
                + GearSystemEntry<TripPlan>.GetTotalWeightInGrams(_gearSystems, visitedGearItems)
                + GearItemEntry<TripPlan>.GetTotalWeightInGrams(_gearItems, visitedGearItems)
                + MealEntry<TripPlan>.GetTotalWeightInGrams(_meals, visitedMeals);
        }

        public float GetTotalWeightInUnits(BackpackPlannerSettings settings)
        {
            int weightInGrams = GetTotalWeightInGrams();
            return settings.Units.WeightFromGrams(weightInGrams);
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
        // ReSharper disable once InconsistentNaming
        public int GetTotalCostInUSDP(ICollection<int> visitedGearItems=null, ICollection<int> visitedMeals=null)
        {
            return GearCollectionEntry<TripPlan>.GetTotalCostInUSDP(_gearCollections, visitedGearItems)
                + GearSystemEntry<TripPlan>.GetTotalCostInUSDP(_gearSystems, visitedGearItems)
                + GearItemEntry<TripPlan>.GetTotalCostInUSDP(_gearItems, visitedGearItems)
                + MealEntry<TripPlan>.GetTotalCostInUSDP(_meals, visitedMeals);
        }

        public float GetTotalCostInCurrency(BackpackPlannerSettings settings)
        {
            // ReSharper disable once InconsistentNaming
            int costInUSDP = GetTotalCostInUSDP();
            return settings.Currency.CurrencyFromUSDP(costInUSDP);
        }

        public float GetCostPerWeightInCurrency(BackpackPlannerSettings settings)
        {
            float weightInUnits = GetTotalWeightInUnits(settings);
            return Math.Abs(weightInUnits) < 0.01f ? 0.0f : GetTotalCostInCurrency(settings) / weightInUnits;
        }
#endregion

        public TripPlan()
        {
        }

        public override bool Equals(object obj)
        {
            if(Id < 1) {
                return false;
            }

            TripPlan tripPlan = obj as TripPlan;
            return Id == tripPlan?.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
