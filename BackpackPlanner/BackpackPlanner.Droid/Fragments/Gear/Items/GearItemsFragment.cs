﻿/*
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

using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items
{
    public class GearItemsFragment : RecyclerFragment
    {
        public override int LayoutResource => Resource.Layout.fragment_gear_items;

        public override int TitleResource => Resource.String.title_gear_items;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            // TODO
            var gearItems = new List<GearItem>();
            for(int i=0; i<20; ++i) {
                gearItems.Add(new GearItem());
            }

            TextView noGearItemsTextView = view.FindViewById<TextView>(Resource.Id.no_gear_items);
            Spinner gearItemsSort = view.FindViewById<Spinner>(Resource.Id.gear_items_sort);

            if(gearItems.Count > 0) {
                noGearItemsTextView.Visibility = ViewStates.Gone;
                gearItemsSort.Visibility = ViewStates.Visible;

                InitLayout(view, Resource.Id.gear_items_layout,
                    new GearItemListAdapter
                    {
                        GearItems = gearItems
                    }
                );

                Layout.Visibility = ViewStates.Visible;
            }

            Android.Support.Design.Widget.FloatingActionButton addGearItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_add_gear_item);
            addGearItemButton.Click += (sender, args) => {
                Android.Support.V4.App.FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.frame_content, new AddGearItemFragment());
                fragmentTransaction.AddToBackStack(null);
                fragmentTransaction.Commit();
            };
        }
    }
}
