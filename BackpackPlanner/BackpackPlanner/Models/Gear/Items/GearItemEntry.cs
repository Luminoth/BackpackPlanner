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

using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Units;

using JetBrains.Annotations;

using SQLite.Net.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Items
{
    public abstract class GearItemEntry<T> : DatabaseIntermediateItem<T, GearItem>  where T: DatabaseItem
    {
#region Static Helpers
        public static int GetGearItemCount<TE>(List<TE> gearItems, [CanBeNull] List<int> visitedGearItems) where TE: GearItemEntry<T>
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

        public static int GetTotalWeightInGrams<TE>(List<TE> gearItems, [CanBeNull] List<int> visitedGearItems) where TE: GearItemEntry<T>
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

        public static int GetTotalCostInUSDP<TE>(List<TE> gearItems, [CanBeNull] List<int> visitedGearItems) where TE: GearItemEntry<T>
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

        public abstract int GearItemId { get; set; }

        /// <summary>
        /// Gets or sets the total weight of these gear items in grams.
        /// </summary>
        /// <value>
        /// The total weight of these gear items in grams.
        /// </value>
        [Ignore]
        public int TotalWeightInGrams => Count * (Child?.WeightInGrams ?? 0);

        /// <summary>
        /// Gets the total weight of these gear items in weight units.
        /// </summary>
        /// <value>
        /// The total weight of these gear items in weight units.
        /// </value>
        [Ignore]
        public float TotalWeightInUnits => Settings?.Units.WeightFromGrams(TotalWeightInGrams) ?? TotalWeightInGrams;

        /// <summary>
        /// Gets the total cost in USDP.
        /// </summary>
        /// <value>
        /// The total cost in USDP.
        /// </value>
        [Ignore]
        public int TotalCostInUSDP => Count * (Child?.CostInUSDP ?? 0);


        protected GearItemEntry()
        {
        }

        protected GearItemEntry(T parent, GearItem gearItem, BackpackPlannerSettings settings)
            : base(parent, gearItem, settings)
        {
        }
    }
}
