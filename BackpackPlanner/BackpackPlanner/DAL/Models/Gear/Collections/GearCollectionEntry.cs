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

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections
{
    [Serializable]
    public class GearCollectionEntry : BaseModelEntry<GearCollection>, IGearItemContainer
    {
#region Static Helpers
        public static int GetGearCollectionCount<TE>(List<TE> gearCollections, [CanBeNull] List<int> visitedGearCollections) where TE: GearCollectionEntry
        {
            int count = 0;
            foreach(TE gearCollection in gearCollections) {
                if(visitedGearCollections?.Contains(gearCollection.ModelId) ?? false) {
                    continue;
                }

                visitedGearCollections?.Add(gearCollection.ModelId);
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

        public override int Id => GearCollectionEntryId;

#region Database Properties
        /// <summary>
        /// Gets or sets the gear collection entry identifier.
        /// </summary>
        /// <value>
        /// The gear collection entry identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearCollectionEntryId { get; private set; }

        [Required, ForeignKey("Model")]
        public override int ModelId { get; protected set; }

        public override GearCollection Model { get; set; }
#endregion

        public GearCollectionEntry(GearCollection gearCollection)
            : base(gearCollection)
        {
        }

        public GearCollectionEntry()
        {
        }

        public int GetGearItemCount(List<int> visitedGearItems = null)
        {
            return Model?.GetGearItemCount(visitedGearItems) ?? 0;
        }

        public int GetTotalWeightInGrams(List<int> visitedGearItems = null)
        {
            return Model?.GetTotalWeightInGrams(visitedGearItems) ?? 0;
        }

        public int GetTotalCostInUSDP(List<int> visitedGearItems = null)
        {
            return Model?.GetTotalCostInUSDP(visitedGearItems) ?? 0;
        }
    }
}
