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
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class GearCollection : BaseModel, IBackpackPlannerItem
    {
#region Static Helpers
        public static async Task<IReadOnlyCollection<GearCollection>> GetAll(DatabaseContext dbContext)
        {
            return await dbContext.GearCollections
                .Include(gearCollection => gearCollection.GearSystems)
                    .ThenInclude(gearSystem => gearSystem.Model)
                .Include(gearCollection => gearCollection.GearItems)
                    .ThenInclude(gearItem => gearItem.Model)
                .ToListAsync().ConfigureAwait(false);
        }
#endregion

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
            get => _name;

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
            get => _note;

            set
            {
                _note = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }
#endregion

        public int GetTotalGearItemCount()
        {
            var visitedGearItems = new List<int>();
            return GearItemContainerExtensions.GetGearItemCount(_gearSystems, visitedGearItems)
                + GetGearItemCount(visitedGearItems);
        }

#region Gear Systems
        public void SetGearSystems(DatabaseContext dbContext, [CanBeNull] IReadOnlyCollection<GearSystemEntry> gearSystems)
        {
            UpdateItemEntries<GearSystemEntry, GearSystem>(dbContext, _gearSystems, gearSystems);
            NotifyPropertyChanged(nameof(GearSystems));
        }

        public int GetGearSystemCount(ICollection<int> visitedGearSystems=null)
        {
            return GearSystemEntry.GetGearSystemCount(_gearSystems, visitedGearSystems);
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
        // ReSharper disable once InconsistentNaming
        public int GetTotalCostInUSDP(ICollection<int> visitedGearItems=null)
        {
            return GearSystemEntry.GetTotalCostInUSDP(_gearSystems, visitedGearItems)
                + GearItemEntry.GetTotalCostInUSDP(_gearItems, visitedGearItems);
        }

        public float GetTotalCostInCurrency(BackpackPlannerSettings settings)
        {
            // ReSharper disable once InconsistentNaming
            int costInUSDP = GetTotalCostInUSDP();
            return settings.Currency.CurrencyFromUSDP(costInUSDP);
        }

        public float GetCostInCurrencyPerWeightInUnits(BackpackPlannerSettings settings)
        {
            float weightInUnits = GetTotalWeightInUnits(settings);
            return Math.Abs(weightInUnits) < 0.01f ? 0.0f : GetTotalCostInCurrency(settings) / weightInUnits;
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
