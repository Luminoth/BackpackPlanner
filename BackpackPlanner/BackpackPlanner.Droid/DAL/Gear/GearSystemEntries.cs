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
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Droid.DAL.Gear
{
    public sealed class GearCollectionGearSystemEntries : ItemEntries<GearCollection, GearSystem, GearSystemEntry>
    {
        public override GearSystemEntry GetItemEntry(GearSystem gearSystem)
        {
            return Model.GearSystems.FirstOrDefault(x => x.Model.Id == gearSystem.Id);
        }

        public GearCollectionGearSystemEntries(GearCollection gearCollection)
            : base(gearCollection, gearCollection.GearSystems)
        {
        }
    }

    public sealed class TripPlanGearSystemEntries : ItemEntries<TripPlan, GearSystem, GearSystemEntry>
    {
        public override GearSystemEntry GetItemEntry(GearSystem gearSystem)
        {
            return Model.GearSystems.FirstOrDefault(x => x.Model.Id == gearSystem.Id);
        }

        public TripPlanGearSystemEntries(TripPlan tripPlan)
            : base(tripPlan, tripPlan.GearSystems)
        {
        }
    }
}
