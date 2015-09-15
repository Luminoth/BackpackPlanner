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
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections
{
    public class GearCollectionListAdapter : BaseListAdapter
    {
        private class GearCollectionViewHolder : BaseViewHolder
        {
            private GearCollection _gearCollection;

            public GearCollection GearCollection
            {
                get { return _gearCollection; }
                set { _gearCollection = value; UpdateView(); }
            }

            public GearCollectionViewHolder(View itemView, BaseFragment fragment) : base(itemView, fragment)
            {
                // TODO: get handles to controls here
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearCollectionFragment();
            }

            private void UpdateView()
            {
                // TODO: update the controls here
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_collection;

        public override int ItemCount => _gearCollections?.Count ?? 0;

        private readonly SortedList<string, GearCollection> _gearCollections = new SortedList<string, GearCollection>();

        public GearCollectionListAdapter(BaseFragment fragment, IReadOnlyCollection<GearCollection> gearCollections) : base(fragment)
        {
            // TODO: ok, so next step is handling different sorting methods
            // and updating when the sort method is changed

            foreach(GearCollection gearCollection in gearCollections) {
                _gearCollections.Add(gearCollection.Name, gearCollection);
            }
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearCollectionViewHolder(itemView, Fragment);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            GearCollectionViewHolder gearCollectionViewHolder = (GearCollectionViewHolder)holder;
            GearCollection gearCollection = _gearCollections.ElementAt(position).Value;
            gearCollectionViewHolder.GearCollection = gearCollection;
        }
    }
}
