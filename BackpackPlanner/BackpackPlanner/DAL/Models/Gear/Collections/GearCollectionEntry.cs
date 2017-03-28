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

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections
{
    public class GearCollectionEntry : BaseModelEntry, IGearItemContainer
    {
#region Static Helpers
        public static int GetGearCollectionCount<TE>(List<TE> gearCollections, [CanBeNull] List<int> visitedGearCollections) where TE: GearCollectionEntry
        {
            int count = 0;
            foreach(TE gearCollection in gearCollections) {
                if(visitedGearCollections?.Contains(gearCollection.GearCollectionId) ?? false) {
                    continue;
                }

                visitedGearCollections?.Add(gearCollection.GearCollectionId);
                count += gearCollection.Count;
            }
            return count;
        }

        public static int GetTotalWeightInGrams<TE>(List<TE> gearCollections, [CanBeNull] List<int> visitedGearItems) where TE: GearCollectionEntry
        {
            return gearCollections.Sum(gearSystem => gearSystem.GetTotalWeightInGrams(visitedGearItems));
        }

        public static int GetTotalCostInUSDP<TE>(List<TE> gearCollections, [CanBeNull] List<int> visitedGearItems) where TE: GearCollectionEntry
        {
            return gearCollections.Sum(gearSystem => gearSystem.GetTotalCostInUSDP(visitedGearItems));
        }
#endregion

        public override BaseModel Model => GearCollection;

        public override IBackpackPlannerItem Item => GearCollection;

#region Database Properties
        /// <summary>
        /// Gets or sets the gear collection entry identifier.
        /// </summary>
        /// <value>
        /// The gear collection entry identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearCollectionEntryId { get; private set; }

        [Required, ForeignKey("GearCollection")]
        public int GearCollectionId { get; private set; }

        public virtual GearCollection GearCollection { get; set; }
#endregion

        public  GearCollectionEntry(GearCollection gearCollection, BackpackPlannerSettings settings)
            : base(settings)
        {
            GearCollection = gearCollection;
        }

        public  GearCollectionEntry(BackpackPlannerSettings settings)
            : base(settings)
        {
        }

        public GearCollectionEntry()
        {
        }

        public int GetGearItemCount(List<int> visitedGearItems = null)
        {
            return GearCollection?.GetGearItemCount(visitedGearItems) ?? 0;
        }

        public int GetTotalWeightInGrams(List<int> visitedGearItems = null)
        {
            return GearCollection?.GetTotalWeightInGrams(visitedGearItems) ?? 0;
        }

        public int GetTotalCostInUSDP(List<int> visitedGearItems = null)
        {
            return GearCollection?.GetTotalCostInUSDP(visitedGearItems) ?? 0;
        }
    }
}
