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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems
{
    /// <summary>
    /// 
    /// </summary>
    public class GearSystem : BaseModel, IBackpackPlannerItem
    {
        [NotMapped]
        public override int Id { get { return GearSystemId; } set { GearSystemId = value; } }

#region Database Properties
        /// <summary>
        /// Gets or sets the gear system identifier.
        /// </summary>
        /// <value>
        /// The gear system identifier.
        /// </value>
        [Key]
        public int GearSystemId { get; set; } = -1;

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the gear system name.
        /// </summary>
        /// <value>
        /// The gear system name.
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

        private readonly List<GearItemEntry> _gearItems = new List<GearItemEntry>();

        /// <summary>
        /// Gets or sets the gear items contained in this system.
        /// </summary>
        /// <value>
        /// The gear items contained in this system.
        /// </value>
        public virtual IReadOnlyCollection<GearItemEntry> GearItems => _gearItems;

        private string _note = string.Empty;

        /// <summary>
        /// Gets or sets the gear system note.
        /// </summary>
        /// <value>
        /// The gear system note.
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
#endif

        public GearSystem(BackpackPlannerSettings settings)
            : base(settings)
        {
        }

        public GearSystem()
        {
        }

#region Gear Items
        public void AddGearItem(GearItem gearItem)
        {
            GearItemEntry entry = (from item in _gearItems where item.GearItemId == gearItem.Id select item).FirstOrDefault();
            if(null != entry) {
                ++entry.Count;
                return;
            }

            entry = new GearItemEntry(gearItem, Settings);
            entry.PropertyChanged += (sender, args) => {
                NotifyPropertyChanged(nameof(GearItems));
            };

            _gearItems.Add(entry);
            NotifyPropertyChanged(nameof(GearItems));
        }

        public void AddGearItems(IReadOnlyCollection<GearItem> gearItems)
        {
            foreach(GearItem gearItem in gearItems) {
                AddGearItem(gearItem);
            }
        }

        public void RemoveGearItem(GearItem gearItem)
        {
            RemoveGearItems(new List<GearItem> { gearItem });
        }

        public void RemoveGearItems(IReadOnlyCollection<GearItem> gearItems)
        {
            var removeItems = (from item in _gearItems where gearItems.Any(x => x.Id == item.GearItemId) select item).ToList();
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
            return GearItemEntry.GetTotalWeightInGrams(_gearItems, visitedGearItems);
        }

        public float GetTotalWeightInUnits()
        {
            int weightInGrams = GetTotalWeightInGrams();
            return Settings?.Units.WeightFromGrams(weightInGrams) ?? weightInGrams;
        }
#endregion

#region Cost
        // ReSharper disable once InconsistentNaming
        public int GetTotalCostInUSDP(List<int> visitedGearItems=null)
        {
            return GearItemEntry.GetTotalCostInUSDP(_gearItems, visitedGearItems);
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
            if(GearSystemId < 1) {
                return false;
            }

            GearSystem gearSystem = obj as GearSystem;
            return GearSystemId == gearSystem?.GearSystemId;
        }

        public override int GetHashCode()
        {
            return GearSystemId.GetHashCode();
        }
    }
}
