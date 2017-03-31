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

using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Units;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items
{
 
    [Serializable]
   public class GearItemEntry : BaseModelEntry<GearItem>
    {
#region Static Helpers
        public static int GetGearItemCount<TE>(List<TE> gearItems, [CanBeNull] List<int> visitedGearItems) where TE: GearItemEntry
        {
            int count = 0;
            foreach(TE gearItem in gearItems) {
                if(visitedGearItems?.Contains(gearItem.ModelId) ?? false) {
                    continue;
                }

                visitedGearItems?.Add(gearItem.ModelId);
                count += gearItem.Count;
            }
            return count;
        }

        public static int GetTotalWeightInGrams<TE>(List<TE> gearItems, [CanBeNull] List<int> visitedGearItems) where TE: GearItemEntry
        {
            int weightInGrams = 0;
            foreach(TE gearItem in gearItems) {
                if(visitedGearItems?.Contains(gearItem.ModelId) ?? false) {
                    continue;
                }

                visitedGearItems?.Add(gearItem.ModelId);
                weightInGrams += gearItem.TotalWeightInGrams;
            }
            return weightInGrams;
        }

        // ReSharper disable once InconsistentNaming
        public static int GetTotalCostInUSDP<TE>(List<TE> gearItems, [CanBeNull] List<int> visitedGearItems) where TE: GearItemEntry
        {
            int costInUSDP = 0;
            foreach(TE gearItem in gearItems) {
                if(visitedGearItems?.Contains(gearItem.ModelId) ?? false) {
                    continue;
                }

                visitedGearItems?.Add(gearItem.ModelId);
                costInUSDP += gearItem.TotalCostInUSDP;
            }
            return costInUSDP;
        }
#endregion

        public override int Id => GearItemEntryId;

#region Database Properties
        /// <summary>
        /// Gets or sets the gear item entry identifier.
        /// </summary>
        /// <value>
        /// The gear item entry identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearItemEntryId { get; private set; }

        [Required, ForeignKey("Model")]
        public override int ModelId { get; protected set; }

        public override GearItem Model { get; protected set; }
#endregion

        /// <summary>
        /// Gets or sets the total weight of these gear items in grams.
        /// </summary>
        /// <value>
        /// The total weight of these gear items in grams.
        /// </value>
        [NotMapped, JsonIgnore]
        public int TotalWeightInGrams => Count * (Model?.WeightInGrams ?? 0);

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
        [NotMapped, JsonIgnore]
        // ReSharper disable once InconsistentNaming
        public int TotalCostInUSDP => Count * (Model?.CostInUSDP ?? 0);

        public GearItemEntry(GearItem gearItem)
            : base(gearItem)
        {
        }

        protected GearItemEntry()
        {
        }
    }
}
