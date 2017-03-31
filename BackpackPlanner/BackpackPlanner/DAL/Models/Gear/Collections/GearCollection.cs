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

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using Newtonsoft.Json;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class GearCollection : BaseModel, IBackpackPlannerItem
    {
        public override int Id => GearCollectionId;

#region Database Properties
        /// <summary>
        /// Gets or sets the gear collection identifier.
        /// </summary>
        /// <value>
        /// The gear collection identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearCollectionId { get; private set; }

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the gear collection name.
        /// </summary>
        /// <value>
        /// The gear collection name.
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

        private readonly List<GearSystemEntry> _gearSystems = new List<GearSystemEntry>();

        /// <summary>
        /// Gets or sets the gear systems contained in this collection.
        /// </summary>
        /// <value>
        /// The gear systems contained in this collection.
        /// </value>
        public virtual IReadOnlyCollection<GearSystemEntry> GearSystems => _gearSystems;

        private readonly List<GearItemEntry> _gearItems = new List<GearItemEntry>();

        /// <summary>
        /// Gets or sets the gear items contained in this collection.
        /// </summary>
        /// <value>
        /// The gear items contained in this collection.
        /// </value>
        public virtual IReadOnlyCollection<GearItemEntry> GearItems => _gearItems;

        private string _note = string.Empty;

        /// <summary>
        /// Gets or sets the gear collection note.
        /// </summary>
        /// <value>
        /// The gear collection note.
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

#if DEBUG
        [NotMapped, JsonIgnore]
        public List<GearSystemEntry> TestGearSystems
        {
            get { return _gearSystems; }
            set
            {
                _gearSystems.Clear();
                _gearSystems.AddRange(value);
            }
        }

        [NotMapped, JsonIgnore]
        public List<GearItemEntry> TestGearItems
        {
            get { return _gearItems; }
            set
            {
                _gearItems.Clear();
                _gearItems.AddRange(value);
            }
        }
#endif

        public int GetTotalGearItemCount()
        {
            var visitedGearItems = new List<int>();
            return GearItemContainerExtensions.GetGearItemCount(_gearSystems, visitedGearItems)
                + GetGearItemCount(visitedGearItems);
        }

#region Gear Systems
        public void AddGearSystem(GearSystemEntry gearSystem)
        {
            GearSystemEntry entry = (from item in _gearSystems where item.ModelId == gearSystem.ModelId select item).FirstOrDefault();
            if(null != entry) {
                entry.Count += gearSystem.Count;
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
                entry.Count += gearItem.Count;
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

#region Weight
        public int GetTotalWeightInGrams(List<int> visitedGearItems=null)
        {
            return GearSystemEntry.GetTotalWeightInGrams(_gearSystems, visitedGearItems)
                + GearItemEntry.GetTotalWeightInGrams(_gearItems, visitedGearItems);
        }

        public float GetTotalWeightInUnits(BackpackPlannerSettings settings)
        {
            int weightInGrams = GetTotalWeightInGrams();
            return settings.Units.WeightFromGrams(weightInGrams);
        }
#endregion

#region Cost
        public int GetTotalCostInUSDP(List<int> visitedGearItems=null)
        {
            return GearSystemEntry.GetTotalCostInUSDP(_gearSystems, visitedGearItems)
                + GearItemEntry.GetTotalCostInUSDP(_gearItems, visitedGearItems);
        }

        public float GetTotalCostInCurrency(BackpackPlannerSettings settings)
        {
            int costInUSDP = GetTotalCostInUSDP();
            return settings.Currency.CurrencyFromUSDP(costInUSDP);
        }

        public float GetCostInCurrencyPerWeightInUnits(BackpackPlannerSettings settings)
        {
            float weightInUnits = GetTotalWeightInUnits(settings);
            return 0.0f == weightInUnits ? 0.0f : GetTotalCostInCurrency(settings) / weightInUnits;
        }
#endregion

        public GearCollection()
        {
        }

        public override bool Equals(object obj)
        {
            if(Id < 1) {
                return false;
            }

            GearCollection gearCollection = obj as GearCollection;
            return Id == gearCollection?.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
