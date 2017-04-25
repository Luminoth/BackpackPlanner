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

using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.DAL.Gear
{
    public abstract class GearCollectionEntries<T> : ItemEntries<T, GearCollection, GearCollectionEntry<T>>
        where T: BaseModel<T>, new()
    {
        public abstract class GearCollectionEntryViewHolder : BaseModelEntryContainerViewHolder<T, GearCollection, GearCollectionEntry<T>>
        {
            protected override int NoItemsResource => Resource.Id.no_gear_collections;

            protected override int NoItemsAddedResource => Resource.Id.no_gear_collections_added;

            protected override int ItemListAdapterResource => Resource.Id.gear_collections_list;

            protected override int AddItemButtonResource => Resource.Id.fab_add_gear_collection;

            protected GearCollectionEntryViewHolder(BaseActivity activity, View view)
                : base(activity, view)
            {
            }
        }

        protected GearCollectionEntries(T model, IReadOnlyCollection<GearCollectionEntry<T>> entries)
            : base(model, entries)
        {
        }
    }

    public sealed class TripPlanGearCollectionEntries : GearCollectionEntries<TripPlan>
    {
        public sealed class TripPlanGearCollectionEntryViewHolder : GearCollectionEntryViewHolder
        {
            public TripPlanGearCollectionEntryViewHolder(BaseActivity activity, View view)
                : base(activity, view)
            {
            }

            public override void DoDataExchange(TripPlan item, ItemEntries<TripPlan, GearCollection, GearCollectionEntry<TripPlan>> itemEntries, DatabaseContext dbContext)
            {
                item.SetGearCollections(dbContext, itemEntries.ItemListAdapter?.Items);
            }
        }

        public override GearCollectionEntry<TripPlan> GetItemEntry(GearCollection gearCollection)
        {
            return Model.GearCollections.FirstOrDefault(x => x.Model.Id == gearCollection.Id);
        }

        public TripPlanGearCollectionEntries(TripPlan tripPlan)
            : base(tripPlan, tripPlan.GearCollections)
        {
        }
    }
}
