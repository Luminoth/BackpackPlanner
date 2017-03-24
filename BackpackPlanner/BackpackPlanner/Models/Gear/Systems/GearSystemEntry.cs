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
using System.Linq;

using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Settings;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Systems
{
    public abstract class GearSystemEntry<T> : DatabaseIntermediateItem<T, GearSystem>, IGearItemContainer where T: DatabaseItem
    {
#region Static Helpers
        public static int GetGearSystemCount<TE>(List<TE> gearSystems, [CanBeNull] List<int> visitedGearSystems) where TE: GearSystemEntry<T>
        {
            int count = 0;
            foreach(TE gearSystem in gearSystems) {
                if(visitedGearSystems?.Contains(gearSystem.GearSystemId) ?? false) {
                    continue;
                }

                visitedGearSystems?.Add(gearSystem.GearSystemId);
                count += gearSystem.Count;
            }
            return count;
        }

        public static int GetTotalWeightInGrams<TE>(List<TE> gearSystems, [CanBeNull] List<int> visitedGearItems) where TE: GearSystemEntry<T>
        {
            return gearSystems.Sum(gearSystem => gearSystem.GetTotalWeightInGrams(visitedGearItems));
        }

        public static int GetTotalCostInUSDP<TE>(List<TE> gearSystems, [CanBeNull] List<int> visitedGearItems) where TE: GearSystemEntry<T>
        {
            return gearSystems.Sum(gearSystem => gearSystem.GetTotalCostInUSDP(visitedGearItems));
        }
#endregion

        public abstract int GearSystemId { get; set; }

        public int GetGearItemCount(List<int> visitedGearItems=null)
        {
            return Child?.GetGearItemCount(visitedGearItems) ?? 0;
        }

        public int GetTotalWeightInGrams(List<int> visitedGearItems=null)
        {
            return Child?.GetTotalWeightInGrams(visitedGearItems) ?? 0;
        }

        public int GetTotalCostInUSDP(List<int> visitedGearItems=null)
        {
            return Child?.GetTotalCostInUSDP(visitedGearItems) ?? 0;
        }

        protected GearSystemEntry()
        {
        }

        protected GearSystemEntry(T parent, GearSystem gearSystem, BackpackPlannerSettings settings)
            : base(parent, gearSystem, settings)
        {
        }
    }
}
