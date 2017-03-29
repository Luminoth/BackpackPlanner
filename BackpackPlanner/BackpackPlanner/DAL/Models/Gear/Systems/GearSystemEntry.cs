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

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems
{
    public class GearSystemEntry : BaseModelEntry, IGearItemContainer
    {
        public override BaseModel ItemModel => GearSystem;

        public override IBackpackPlannerItem Item => GearSystem;

#region Static Helpers
        public static int GetGearSystemCount<TE>(List<TE> gearSystems, [CanBeNull] List<int> visitedGearSystems) where TE: GearSystemEntry
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

        public static int GetTotalWeightInGrams<TE>(List<TE> gearSystems, [CanBeNull] List<int> visitedGearItems) where TE: GearSystemEntry
        {
            return gearSystems.Sum(gearSystem => gearSystem.GetTotalWeightInGrams(visitedGearItems));
        }

        public static int GetTotalCostInUSDP<TE>(List<TE> gearSystems, [CanBeNull] List<int> visitedGearItems) where TE: GearSystemEntry
        {
            return gearSystems.Sum(gearSystem => gearSystem.GetTotalCostInUSDP(visitedGearItems));
        }
#endregion

#region Database Properties
        /// <summary>
        /// Gets or sets the gear system entry identifier.
        /// </summary>
        /// <value>
        /// The gear system entry identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearSystemEntryId { get; private set; }

        [Required, ForeignKey("GearSystem")]
        public int GearSystemId { get; private set; }

        public virtual GearSystem GearSystem { get; set; }
#endregion

        public GearSystemEntry(GearSystem gearSystem)
        {
            GearSystem = gearSystem;
        }

        public int GetGearItemCount(List<int> visitedGearItems=null)
        {
            return GearSystem?.GetGearItemCount(visitedGearItems) ?? 0;
        }

        public int GetTotalWeightInGrams(List<int> visitedGearItems=null)
        {
            return GearSystem?.GetTotalWeightInGrams(visitedGearItems) ?? 0;
        }

        public int GetTotalCostInUSDP(List<int> visitedGearItems=null)
        {
            return GearSystem?.GetTotalCostInUSDP(visitedGearItems) ?? 0;
        }
    }
}
