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

using System.Linq;

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Droid.DAL.Gear
{
    public sealed class GearSystemGearItemEntries : ItemEntries<GearSystem, GearItem, GearItemEntry>
    {
        public override GearItemEntry GetItemEntry(GearItem gearItem)
        {
            return Model.GearItems.FirstOrDefault(x => x.ModelId == gearItem.Id);
        }

        public GearSystemGearItemEntries(GearSystem gearSystem)
            : base(gearSystem)
        {
        }
    }

    public sealed class GearCollectionGearItemEntries : ItemEntries<GearCollection, GearItem, GearItemEntry>
    {
        public override GearItemEntry GetItemEntry(GearItem gearItem)
        {
            return Model.GearItems.FirstOrDefault(x => x.ModelId == gearItem.Id);
        }

        public GearCollectionGearItemEntries(GearCollection gearCollection)
            : base(gearCollection)
        {
        }
    }

    public sealed class TripPlanGearItemEntries : ItemEntries<TripPlan, GearItem, GearItemEntry>
    {
        public override GearItemEntry GetItemEntry(GearItem gearItem)
        {
            return Model.GearItems.FirstOrDefault(x => x.ModelId == gearItem.Id);
        }

        public TripPlanGearItemEntries(TripPlan tripPlan)
            : base(tripPlan)
        {
        }
    }
}
