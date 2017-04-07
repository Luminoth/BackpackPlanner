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

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems
{
    /// <summary>
    /// 
    /// </summary>

    [Serializable]
    public class GearSystem : BaseModel, IBackpackPlannerItem
    {
        public override int Id => GearSystemId;

#region Database Properties
        /// <summary>
        /// Gets or sets the gear system identifier.
        /// </summary>
        /// <value>
        /// The gear system identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearSystemId { get; private set; }

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
            get => _name;

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
            get => _note;

            set
            {
                _note = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }
#endregion

#region Gear Items
        public void SetGearItems(DatabaseContext dbContext, [CanBeNull] IReadOnlyCollection<GearItemEntry> gearItems)
        {
            UpdateItemEntries<GearItemEntry, GearItem>(dbContext, _gearItems, gearItems);
            NotifyPropertyChanged(nameof(GearItems));
        }

        public int GetGearItemCount(ICollection<int> visitedGearItems=null)
        {
            return GearItemEntry.GetGearItemCount(_gearItems, visitedGearItems);
        }
#endregion

#region Weight
        public int GetTotalWeightInGrams(ICollection<int> visitedGearItems=null)
        {
            return GearItemEntry.GetTotalWeightInGrams(_gearItems, visitedGearItems);
        }

        public float GetTotalWeightInUnits(BackpackPlannerSettings settings)
        {
            int weightInGrams = GetTotalWeightInGrams();
            return settings.Units.WeightFromGrams(weightInGrams);
        }
#endregion

#region Cost
        // ReSharper disable once InconsistentNaming
        public int GetTotalCostInUSDP(ICollection<int> visitedGearItems=null)
        {
            return GearItemEntry.GetTotalCostInUSDP(_gearItems, visitedGearItems);
        }

        public float GetTotalCostInCurrency(BackpackPlannerSettings settings)
        {
            // ReSharper disable once InconsistentNaming
            int costInUSDP = GetTotalCostInUSDP();
            return settings.Currency.CurrencyFromUSDP(costInUSDP);
        }

        public float GetCostInCurrencyPerWeight(BackpackPlannerSettings settings)
        {
            float weightInUnits = GetTotalWeightInUnits(settings);
            return Math.Abs(weightInUnits) < 0.01f ? 0.0f : GetTotalCostInCurrency(settings) / weightInUnits;
        }
#endregion

        public GearSystem()
        {
        }

        public override bool Equals(object obj)
        {
            if(Id < 1) {
                return false;
            }

            GearSystem gearSystem = obj as GearSystem;
            return Id == gearSystem?.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
