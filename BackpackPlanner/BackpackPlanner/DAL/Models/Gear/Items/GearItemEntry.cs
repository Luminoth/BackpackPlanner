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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Units;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items
{
    public class GearItemEntry : BaseModelEntry
    {
#region Static Helpers
        public static int GetGearItemCount<TE>(List<TE> gearItems, [CanBeNull] List<int> visitedGearItems) where TE: GearItemEntry
        {
            int count = 0;
            foreach(TE gearItem in gearItems) {
                if(visitedGearItems?.Contains(gearItem.GearItemId) ?? false) {
                    continue;
                }

                visitedGearItems?.Add(gearItem.GearItemId);
                count += gearItem.Count;
            }
            return count;
        }

        public static int GetTotalWeightInGrams<TE>(List<TE> gearItems, [CanBeNull] List<int> visitedGearItems) where TE: GearItemEntry
        {
            int weightInGrams = 0;
            foreach(TE gearItem in gearItems) {
                if(visitedGearItems?.Contains(gearItem.GearItemId) ?? false) {
                    continue;
                }

                visitedGearItems?.Add(gearItem.GearItemId);
                weightInGrams += gearItem.TotalWeightInGrams;
            }
            return weightInGrams;
        }

        // ReSharper disable once InconsistentNaming
        public static int GetTotalCostInUSDP<TE>(List<TE> gearItems, [CanBeNull] List<int> visitedGearItems) where TE: GearItemEntry
        {
            int costInUSDP = 0;
            foreach(TE gearItem in gearItems) {
                if(visitedGearItems?.Contains(gearItem.GearItemId) ?? false) {
                    continue;
                }

                visitedGearItems?.Add(gearItem.GearItemId);
                costInUSDP += gearItem.TotalCostInUSDP;
            }
            return costInUSDP;
        }
#endregion

        public override BaseModel ItemModel => GearItem;

        public override IBackpackPlannerItem Item => GearItem;

#region Database Properties
        /// <summary>
        /// Gets or sets the gear item entry identifier.
        /// </summary>
        /// <value>
        /// The gear item entry identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearItemEntryId { get; private set; }

        [Required, ForeignKey("GearItem")]
        public int GearItemId { get; private set; }

        public virtual GearItem GearItem { get; set; }
#endregion

        /// <summary>
        /// Gets or sets the total weight of these gear items in grams.
        /// </summary>
        /// <value>
        /// The total weight of these gear items in grams.
        /// </value>
        [NotMapped]
        public int TotalWeightInGrams => Count * (GearItem?.WeightInGrams ?? 0);

        /// <summary>
        /// Gets the total weight of these gear items in weight units.
        /// </summary>
        /// <value>
        /// The total weight of these gear items in weight units.
        /// </value>
        public float GetTotalWeightInUnits(BackpackPlannerSettings settings)
        {
            return settings.Units.WeightFromGrams(TotalWeightInGrams);
        }

        /// <summary>
        /// Gets the total cost in USDP.
        /// </summary>
        /// <value>
        /// The total cost in USDP.
        /// </value>
        [NotMapped]
        // ReSharper disable once InconsistentNaming
        public int TotalCostInUSDP => Count * (GearItem?.CostInUSDP ?? 0);

        public GearItemEntry(GearItem gearItem)
        {
            GearItem = gearItem;
        }
    }
}
