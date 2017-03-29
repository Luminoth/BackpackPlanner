/*
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
using System.Linq;

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans
{
    /// <summary>
    /// 
    /// </summary>
    public class TripPlan : BaseModel, IBackpackPlannerItem
    {
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
            get { return _name; }
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
            get { return _startDate; }
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
            get { return _endDateTime; }
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
            get { return _tripItinerary; }
            set
            {
                _tripItinerary = value;
                NotifyPropertyChanged();
            }
        }

        private readonly List<GearCollectionEntry> _gearCollections = new List<GearCollectionEntry>();

        /// <summary>
        /// Gets or sets the gear collections contained in this plan.
        /// </summary>
        /// <value>
        /// The gear collections contained in this plan.
        /// </value>
        public virtual IReadOnlyCollection<GearCollectionEntry> GearCollections => _gearCollections;

        private readonly List<GearSystemEntry> _gearSystems = new List<GearSystemEntry>();

        /// <summary>
        /// Gets or sets the gear systems contained in this plan.
        /// </summary>
        /// <value>
        /// The gear systems contained in this plan.
        /// </value>
        public virtual IReadOnlyCollection<GearSystemEntry> GearSystems => _gearSystems;

        private readonly List<GearItemEntry> _gearItems = new List<GearItemEntry>();

        /// <summary>
        /// Gets or sets the gear items contained in this plan.
        /// </summary>
        /// <value>
        /// The gear items contained in this plan.
        /// </value>
        public virtual IReadOnlyCollection<GearItemEntry> GearItems => _gearItems;

        private readonly List<MealEntry> _meals = new List<MealEntry>();

        /// <summary>
        /// Gets or sets the meals contained in this plan.
        /// </summary>
        /// <value>
        /// The meals contained in this plan.
        /// </value>
        public virtual IReadOnlyCollection<MealEntry> Meals => _meals;

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
            set
            {
                _note = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }
#endregion

        [NotMapped]
        public int Days => (StartDate - EndDate).Days;

#if DEBUG
        [NotMapped]
        public List<GearCollectionEntry> TestGearCollections
        {
            get { return _gearCollections; }
            set
            {
                _gearCollections.Clear();
                _gearCollections.AddRange(value);
            }
        }

        [NotMapped]
        public List<GearSystemEntry> TestGearSystems
        {
            get { return _gearSystems; }
            set
            {
                _gearSystems.Clear();
                _gearSystems.AddRange(value);
            }
        }

        [NotMapped]
        public List<GearItemEntry> TestGearItems
        {
            get { return _gearItems; }
            set
            {
                _gearItems.Clear();
                _gearItems.AddRange(value);
            }
        }

        [NotMapped]
        public List<MealEntry> TestMeals
        {
            get { return _meals; }
            set
            {
                _meals.Clear();
                _meals.AddRange(value);
            }
        }
#endif

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
            return MealEntry.GetTotalCalories(_meals, visitedMeals);
        }

#region Gear Collections
        public void AddGearCollection(GearCollectionEntry gearCollection)
        {
            GearCollectionEntry entry = (from item in _gearCollections where item.ModelId == gearCollection.ModelId select item).FirstOrDefault();
            if(null != entry) {
                ++entry.Count;
                return;
            }

            gearCollection.PropertyChanged += (sender, args) => {
                NotifyPropertyChanged(nameof(GearCollections));
            };

            _gearCollections.Add(gearCollection);
            NotifyPropertyChanged(nameof(GearCollections));
        }

        public void AddGearCollections(IReadOnlyCollection<GearCollectionEntry> gearCollections)
        {
            foreach(GearCollectionEntry gearCollection in gearCollections) {
                AddGearCollection(gearCollection);
            }
        }

        public void RemoveGearCollection(GearCollection gearCollection)
        {
            RemoveGearCollections(new List<GearCollection> { gearCollection });
        }

        public void RemoveGearCollections(IReadOnlyCollection<GearCollection> gearCollections)
        {
            var removeItems = (from item in _gearCollections where gearCollections.Any(x => x.Id == item.ModelId) select item).ToList();
            foreach(GearCollectionEntry item in removeItems) {
                item.OnRemove();
                _gearCollections.Remove(item);
            }

            NotifyPropertyChanged(nameof(GearCollections));
        } 

        public int GetGearCollectionCount(List<int> visitedGearCollections=null)
        {
            return GearCollectionEntry.GetGearCollectionCount(_gearCollections, visitedGearCollections);
        } 
#endregion

#region Gear Systems
        public void AddGearSystem(GearSystemEntry gearSystem)
        {
            GearSystemEntry entry = (from item in _gearSystems where item.ModelId == gearSystem.ModelId select item).FirstOrDefault();
            if(null != entry) {
                ++entry.Count;
                return;
            }

            gearSystem.PropertyChanged += (sender, args) => {
                NotifyPropertyChanged(nameof(GearSystems));
            };

            _gearSystems.Add(gearSystem);
            NotifyPropertyChanged(nameof(GearSystems));
        }

        public void AddGearSystems(IReadOnlyCollection<GearSystemEntry> gearSystems)
        {
            foreach(GearSystemEntry gearSystem in gearSystems) {
                AddGearSystem(gearSystem);
            }
        }

        public void RemoveGearSystem(GearSystem gearSystem)
        {
            RemoveGearSystems(new List<GearSystem> { gearSystem });
        }

        public void RemoveGearSystems(IReadOnlyCollection<GearSystem> gearSystems)
        {
            var removeItems = (from item in _gearSystems where gearSystems.Any(x => x.Id == item.ModelId) select item).ToList();
            foreach(GearSystemEntry item in removeItems) {
                item.OnRemove();
                _gearSystems.Remove(item);
            }

            NotifyPropertyChanged(nameof(GearSystems));
        } 

        public int GetGearSystemCount(List<int> visitedGearSystems=null)
        {
            return GearSystemEntry.GetGearSystemCount(_gearSystems, visitedGearSystems);
        } 
#endregion

#region Gear Items
        public void AddGearItem(GearItemEntry gearItem)
        {
            GearItemEntry entry = (from item in _gearItems where item.ModelId == gearItem.ModelId select item).FirstOrDefault();
            if(null != entry) {
                ++entry.Count;
                return;
            }

            gearItem.PropertyChanged += (sender, args) => {
                NotifyPropertyChanged(nameof(GearItems));
            };

            _gearItems.Add(gearItem);
            NotifyPropertyChanged(nameof(GearItems));
        }

        public void AddGearItems(IReadOnlyCollection<GearItemEntry> gearItems)
        {
            foreach(GearItemEntry gearItem in gearItems) {
                AddGearItem(gearItem);
            }
        }

        public void RemoveGearItem(GearItem gearItem)
        {
            RemoveGearItems(new List<GearItem> { gearItem });
        }

        public void RemoveGearItems(IReadOnlyCollection<GearItem> gearItems)
        {
            var removeItems = (from item in _gearItems where gearItems.Any(x => x.Id == item.ModelId) select item).ToList();
            foreach(GearItemEntry item in removeItems) {
                item.OnRemove();
                _gearItems.Remove(item);
            }

            NotifyPropertyChanged(nameof(GearItems));
        } 

        public int GetGearItemCount(List<int> visitedGearItems=null)
        {
            return GearItemEntry.GetGearItemCount(_gearItems, visitedGearItems);
        }
#endregion

#region Meals
        public void AddMeal(MealEntry meal)
        {
            MealEntry entry = (from item in _meals where item.ModelId == meal.ModelId select item).FirstOrDefault();
            if(null != entry) {
                ++entry.Count;
                return;
            }

            meal.PropertyChanged += (sender, args) => {
                NotifyPropertyChanged(nameof(Meals));
            };

            _meals.Add(meal);
            NotifyPropertyChanged(nameof(Meals));
        }

        public void AddMeals(IReadOnlyCollection<MealEntry> meals)
        {
            foreach(MealEntry meal in meals) {
                AddMeal(meal);
            }
        }

        public void RemoveMeal(Meal meal)
        {
            RemoveMeals(new List<Meal> { meal });
        }

        public void RemoveMeals(IReadOnlyCollection<Meal> meals)
        {
            var removeItems = (from item in _meals where meals.Any(x => x.Id == item.ModelId) select item).ToList();
            foreach(MealEntry item in removeItems) {
                item.OnRemove();
                _meals.Remove(item);
            }

            NotifyPropertyChanged(nameof(Meals));
        } 

        public int GetMealCount(List<int> visitedMeals=null)
        {
            return MealEntry.GetMealCount(_meals, visitedMeals);
        }
#endregion

#region Weight
        public int GetTotalWeightInGrams(List<int> visitedGearItems=null, List<int> visitedMeals=null)
        {
            return GearCollectionEntry.GetTotalWeightInGrams(_gearCollections, visitedGearItems)
                + GearSystemEntry.GetTotalWeightInGrams(_gearSystems, visitedGearItems)
                + GearItemEntry.GetTotalWeightInGrams(_gearItems, visitedGearItems)
                + MealEntry.GetTotalWeightInGrams(_meals, visitedMeals);
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
        public int GetTotalCostInUSDP(List<int> visitedGearItems=null, List<int> visitedMeals=null)
        {
            return GearCollectionEntry.GetTotalCostInUSDP(_gearCollections, visitedGearItems)
                + GearSystemEntry.GetTotalCostInUSDP(_gearSystems, visitedGearItems)
                + GearItemEntry.GetTotalCostInUSDP(_gearItems, visitedGearItems)
                + MealEntry.GetTotalCostInUSDP(_meals, visitedMeals);
        }

        public float GetTotalCostInCurrency(BackpackPlannerSettings settings)
        {
            int costInUSDP = GetTotalCostInUSDP();
            return settings.Currency.CurrencyFromUSDP(costInUSDP);
        }

        public float GetCostPerWeightInCurrency(BackpackPlannerSettings settings)
        {
            float weightInUnits = GetTotalWeightInUnits(settings);
            return 0.0f == weightInUnits ? 0.0f : GetTotalCostInCurrency(settings) / weightInUnits;
        }
#endregion

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
