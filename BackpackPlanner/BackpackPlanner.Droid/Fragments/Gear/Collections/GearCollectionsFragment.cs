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

using Android.OS;

using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections
{
    public class GearCollectionsFragment : ListItemsFragment<GearCollection>
    {
        protected override int LayoutResource => Resource.Layout.fragment_gear_collections;

        protected override int TitleResource => Resource.String.title_gear_collections;

        protected override int ListLayoutResource => Resource.Id.gear_collections_layout;

        protected override int NoItemsResource => Resource.Id.no_gear_collections;

        protected override int SortItemsResource => Resource.Id.gear_collections_sort;

        protected override int AddItemResource => Resource.Id.fab_add_gear_collection;

        protected override bool HasSearchView => true;

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddGearCollectionFragment();
        }

        protected override BaseListAdapter<GearCollection> CreateAdapter()
        {
            return new GearCollectionListAdapter(this, ListItems);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // TODO
#region Test Items
            ListItems.Add(new GearCollection
                {
                    Name = "One"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Two"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Three"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Four"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Five"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Six"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Seven"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Eight"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Nine"
                }
            );

            ListItems.Add(new GearCollection
                {
                    Name = "Ten"
                }
            );
#endregion
        }
    }
}
