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

using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;

namespace EnergonSoftware.BackpackPlanner.Droid.DAL.Gear
{
    public abstract class GearSystemEntries<T> : ItemEntries<T, GearSystem, GearSystemEntry>
        where T: BaseModel
    {
        public abstract class GearSystemEntryViewHolder : DataFragment<T>.ItemEntryViewHolder<GearSystem, GearSystemEntry>
        {
            protected override int NoItemsResource => Resource.Id.no_gear_systems;

            protected override int NoItemsAddedResource => Resource.Id.no_gear_systems_added;

            protected override int ItemListAdapterResource => Resource.Id.gear_systems_list;

            protected override int AddItemButtonResource => Resource.Id.fab_add_gear_system;

            protected override int AddItemDialogTitleResource => Resource.String.label_add_gear_systems;

            protected GearSystemEntryViewHolder(DataFragment<T> fragment, T item, GearSystemEntries<T> itemEntries)
                : base(fragment, item, itemEntries)
            {
            }
        }

        protected GearSystemEntries(T model, IReadOnlyCollection<GearSystemEntry> entries)
            : base(model, entries)
        {
        }
    }

    public sealed class GearCollectionGearSystemEntries : GearSystemEntries<GearCollection>
    {
        public sealed class GearCollectionGearSystemEntryViewHolder : GearSystemEntryViewHolder
        {
            public GearCollectionGearSystemEntryViewHolder(DataFragment<GearCollection> fragment, GearCollection item, GearCollectionGearSystemEntries itemEntries)
                : base(fragment, item, itemEntries)
            {
            }
        }

        public override GearSystemEntry GetItemEntry(GearSystem gearSystem)
        {
            return Model.GearSystems.FirstOrDefault(x => x.Model.Id == gearSystem.Id);
        }

        public GearCollectionGearSystemEntries(GearCollection gearCollection)
            : base(gearCollection, gearCollection.GearSystems)
        {
        }
    }

    public sealed class TripPlanGearSystemEntries : GearSystemEntries<TripPlan>
    {
        public sealed class TripPlanGearSystemEntryViewHolder : GearSystemEntryViewHolder
        {
            public TripPlanGearSystemEntryViewHolder(DataFragment<TripPlan> fragment, TripPlan item, TripPlanGearSystemEntries itemEntries)
                : base(fragment, item, itemEntries)
            {
            }
        }

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
