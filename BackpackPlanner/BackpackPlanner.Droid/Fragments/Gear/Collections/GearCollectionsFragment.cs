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

using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections
{
    public class GearCollectionsFragment : RecyclerFragment
    {
        public override int LayoutResource => Resource.Layout.fragment_gear_collections;

        public override int TitleResource => Resource.String.title_gear_collections;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            // TODO
            var gearCollections = new List<GearCollection>();
            for(int i=0; i<20; ++i) {
                gearCollections.Add(new GearCollection());
            }

            TextView noGearCollectionsTextView = view.FindViewById<TextView>(Resource.Id.no_gear_collections);
            Spinner gearCollectionsSort = view.FindViewById<Spinner>(Resource.Id.gear_collections_sort);

            if(gearCollections.Count > 0) {
                noGearCollectionsTextView.Visibility = ViewStates.Gone;
                gearCollectionsSort.Visibility = ViewStates.Visible;

                InitLayout(view, Resource.Id.gear_collections_layout,
                    new GearCollectionListAdapter
                    {
                        GearCollections = gearCollections
                    }
                );

                Layout.Visibility = ViewStates.Visible;
            }

            Android.Support.Design.Widget.FloatingActionButton addGearCollectionButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_add_gear_collection);
            addGearCollectionButton.Click += (sender, args) => {
                // TODO
            };
        }
    }
}
