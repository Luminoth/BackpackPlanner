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

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items
{
    public class GearItemListAdapter : BaseListAdapter
    {
        private class GearItemViewHolder : BaseViewHolder
        {
            private GearItem _gearItem;

            public GearItem GearItem
            {
                get { return _gearItem; }
                set { _gearItem = value; UpdateView(); }
            }

            public GearItemViewHolder(View itemView, BaseFragment fragment) : base(itemView, fragment)
            {
                // TODO: get handles to controls here
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearItemFragment();
            }

            private void UpdateView()
            {
                // TODO: update the controls here
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_item;

        public override int ItemCount => _gearItems?.Count ?? 0;

        private readonly ICollection<GearItem> _gearItems;

        public GearItemListAdapter(BaseFragment fragment, IEnumerable<GearItem> gearItems) : base(fragment)
        {
            // TODO: ok, so next step is handling different sorting methods
            // and updating when the sort method is changed

            _gearItems = gearItems.OrderBy(x => x.Name).ToList();
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearItemViewHolder(itemView, Fragment);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            GearItemViewHolder gearItemViewHolder = (GearItemViewHolder)holder;
            GearItem gearItem = _gearItems.ElementAt(position);
            gearItemViewHolder.GearItem = gearItem;
        }
    }
}
