/*
   Copyright 2015 Shane Lillie

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

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Gear;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items
{
    public sealed class GearItemListAdapter : BaseModelRecyclerListAdapter<GearItem>
    {
        protected override int LayoutResource => Resource.Layout.view_gear_item;

        public GearItemListAdapter(ListItemsFragment<GearItem> fragment)
            : base(fragment)
        {
        }

        protected override IEnumerable<GearItem> SortItemsByPosition(int position, IEnumerable<GearItem> items)
        {
            switch(position)
            {
            case 0:         // Name
                return from x in items orderby x?.Name select x;
            case 1:         // Weight
                return from x in items orderby x?.WeightInGrams select x;
            case 2:         // Cost
                return from x in items orderby x?.CostInUSDP select x;
            case 3:         // Cost / Weight
                // TODO
                return items;
            }
            return items;
        }

        protected override BaseRecyclerViewHolder<GearItem> CreateViewHolder(View view, BaseRecyclerListAdapter<GearItem> adapter)
        {
            return new GearItemListViewHolder(view, adapter);
        }
    }
}
