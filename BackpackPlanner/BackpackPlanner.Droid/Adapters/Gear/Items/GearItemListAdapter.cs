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

using Android.Views;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items
{
    public class GearItemListAdapter : BaseListAdapter
    {
        private class GearItemViewHolder : BaseViewHolder
        {
            public GearItemViewHolder(View itemView, BaseFragment fragment) : base(itemView, fragment)
            {
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearItemFragment();
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_item;

        public override int ItemCount => GearItems?.Count ?? 0;

        public IReadOnlyCollection<GearItem> GearItems { get; set; }

        public GearItemListAdapter(BaseFragment fragment) : base(fragment)
        {
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearItemViewHolder(itemView, Fragment);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            GearItemViewHolder gearItemViewHolder = (GearItemViewHolder)holder;

            // setup the view holder
        }
    }
}
