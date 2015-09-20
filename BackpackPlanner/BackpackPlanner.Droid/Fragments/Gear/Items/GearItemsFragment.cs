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
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items
{
    public class GearItemsFragment : ListItemsFragment<GearItem>
    {
        protected override int LayoutResource => Resource.Layout.fragment_gear_items;

        protected override int TitleResource => Resource.String.title_gear_items;

        protected override int ListLayoutResource => Resource.Id.gear_items_layout;

        protected override int NoItemsResource => Resource.Id.no_gear_items;

        protected override int SortItemsResource => Resource.Id.gear_items_sort;

        protected override bool HasSearchView => true;

        protected override int AddItemResource => Resource.Id.fab_add_gear_item;

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddGearItemFragment();
        }

        protected override BaseListAdapter<GearItem> CreateAdapter()
        {
            return new GearItemListAdapter(this, ListItems);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // TODO
#region Test Items
            ListItems.Add(new GearItem
                {
                    Name = "One",
                    Make = "Test Make",
                    Model = "Test Model",
                    WeightInGrams = 1,
                    CostInUSDP = 20
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Two",
                    Make = "Test Make",
                    WeightInGrams = 2,
                    CostInUSDP = 19
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Three",
                    Model = "Test Model",
                    WeightInGrams = 3,
                    CostInUSDP = 18
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Four",
                    WeightInGrams = 4,
                    CostInUSDP = 17
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Five",
                    WeightInGrams = 5,
                    CostInUSDP = 16
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Six",
                    WeightInGrams = 6,
                    CostInUSDP = 15
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Seven",
                    WeightInGrams = 7,
                    CostInUSDP = 14
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Eight",
                    WeightInGrams = 8,
                    CostInUSDP = 13
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Nine",
                    WeightInGrams = 9,
                    CostInUSDP = 12
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Ten",
                    WeightInGrams = 10,
                    CostInUSDP = 11
                }
            );
#endregion
        }
    }
}
