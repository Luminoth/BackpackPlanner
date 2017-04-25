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
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.DAL.Gear
{
    public abstract class GearItemEntries<T> : ItemEntries<T, GearItem, GearItemEntry<T>>
        where T: BaseModel<T>, new()
    {
        public abstract class GearItemEntryViewHolder : BaseModelEntryContainerViewHolder<T, GearItem, GearItemEntry<T>>
        {
            protected override int NoItemsResource => Resource.Id.no_gear_items;

            protected override int NoItemsAddedResource => Resource.Id.no_gear_items_added;

            protected override int ItemListAdapterResource => Resource.Id.gear_items_list;

            protected override int AddItemButtonResource => Resource.Id.fab_add_gear_item;

            protected GearItemEntryViewHolder(BaseActivity activity, View view)
                : base(activity, view)
            {
            }
        }

        protected GearItemEntries(T model, IReadOnlyCollection<GearItemEntry<T>> entries)
            : base(model, entries)
        {
        }
    }

    public sealed class GearSystemGearItemEntries : GearItemEntries<GearSystem>
    {
        public sealed class GearSystemGearItemEntryViewHolder : GearItemEntryViewHolder
        {
            public GearSystemGearItemEntryViewHolder(BaseActivity activity, View view)
                : base(activity, view)
            {
            }

            public override void DoDataExchange(GearSystem item, ItemEntries<GearSystem, GearItem, GearItemEntry<GearSystem>> itemEntries, DatabaseContext dbContext)
            {
                item.SetGearItems(dbContext, itemEntries.ItemListAdapter?.Items);
            }
        }

        public override GearItemEntry<GearSystem> GetItemEntry(GearItem gearItem)
        {
            return Model.GearItems.FirstOrDefault(x => x.Model.Id == gearItem.Id);
        }

        public GearSystemGearItemEntries(GearSystem gearSystem)
            : base(gearSystem, gearSystem.GearItems)
        {
        }
    }

    public sealed class GearCollectionGearItemEntries : GearItemEntries<GearCollection>
    {
        public sealed class GearCollectionGearItemEntryViewHolder : GearItemEntryViewHolder
        {
            public GearCollectionGearItemEntryViewHolder(BaseActivity activity, View view)
                : base(activity, view)
            {
            }

            public override void DoDataExchange(GearCollection item, ItemEntries<GearCollection, GearItem, GearItemEntry<GearCollection>> itemEntries, DatabaseContext dbContext)
            {
                item.SetGearItems(dbContext, itemEntries.ItemListAdapter?.Items);
            }
        }

        public override GearItemEntry<GearCollection> GetItemEntry(GearItem gearItem)
        {
            return Model.GearItems.FirstOrDefault(x => x.Model.Id == gearItem.Id);
        }

        public GearCollectionGearItemEntries(GearCollection gearCollection)
            : base(gearCollection, gearCollection.GearItems)
        {
        }
    }

    public sealed class TripPlanGearItemEntries : GearItemEntries<TripPlan>
    {
        public sealed class TripPlanGearItemEntryViewHolder : GearItemEntryViewHolder
        {
            public TripPlanGearItemEntryViewHolder(BaseActivity activity, View view)
                : base(activity, view)
            {
            }

            public override void DoDataExchange(TripPlan item, ItemEntries<TripPlan, GearItem, GearItemEntry<TripPlan>> itemEntries, DatabaseContext dbContext)
            {
                item.SetGearItems(dbContext, itemEntries.ItemListAdapter?.Items);
            }
        }

        public override GearItemEntry<TripPlan> GetItemEntry(GearItem gearItem)
        {
            return Model.GearItems.FirstOrDefault(x => x.Model.Id == gearItem.Id);
        }

        public TripPlanGearItemEntries(TripPlan tripPlan)
            : base(tripPlan, tripPlan.GearItems)
        {
        }
    }
}
