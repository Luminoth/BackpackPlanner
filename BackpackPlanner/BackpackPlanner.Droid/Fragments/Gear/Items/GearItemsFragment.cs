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
                    Name = "Alcohol Stove",
                    Make = "Zelph's Stoveworks",
                    Model = "StarLyte",
                    Url = "http://www.woodgaz-stove.com/starlyte-burner-with-lid.php",
                    WeightInGrams = 19,
                    CostInUSDP = 1300,
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Backpack",
                    Make = "ULA",
                    Model = "Circuit",
                    Url = "http://www.ula-equipment.com/product_p/circuit.htm",
                    WeightInGrams = 986,
                    CostInUSDP = 22500,
                    Note = "Medium torso (18\" - 21\"). Medium hipbelt (34\" - 38\"). J-Curve shoulder strap. Aluminum stay removed. Includes hanging s-biner \"Ahhh\" and water shoe carabiner. 39L main body. Max 15 pound base weight, 30-35 pack weight."
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Hammock",
                    Make = "Aaron Erbe",
                    Model = "DIY",
                    WeightInGrams = 422,
                    CostInUSDP = 0,
                    Note = "Includes adjustable ridge line and 2x whoopie slings from whoopieslings.com, and bishop bag."
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Head Lamp",
                    Make = "Petzl",
                    Model = "Tikka Plus 2",
                    WeightInGrams = 79,
                    CostInUSDP = 2995
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Kilt",
                    Make = "Utilikilt",
                    Model = "Survival",
                    Url = "http://www.utilikilts.com/index.php/the-survival.html",
                    Carried = GearCarried.Worn,
                    WeightInGrams = 989,
                    CostInUSDP = 33000,
                    Note = "100% cotton. Cargo pockets removed (3.8 ounces each)."
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "New Underquilt",
                    Make = "Arrowhead Equipment",
                    Model = "Anniversary Jarbridge 3S (25F)",
                    Url = "http://www.arrowhead-equipment.com/store/p510/Anniversary_Jarbidge_UnderQuilt.htmll",
                    WeightInGrams = 566,
                    CostInUSDP = 7500,
                    Note = "6oz APEX Climashield synthetic"
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Old Underquilt",
                    Make = "Aaron Erbe",
                    Model = "DIY",
                    WeightInGrams = 887,
                    CostInUSDP = 0,
                    Note = "Synthetic material. Need to have Aaron or Joe possibly remove some material from the overstuff collars to get the size and weight down on this."
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Overquilt",
                    Make = "Arrowhead Equipment",
                    Model = "Owyhee Top Quilt Regular 3S (25F)",
                    Url = "http://www.arrowhead-equipment.com/store/p314/Owyhee_Top_Quilt_Regular.html",
                    WeightInGrams = 802,
                    CostInUSDP = 17900,
                    Note = "6oz APEX Climashield synthetic"
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Toilet Paper",
                    IsConsumable = true,
                    ConsumedPerDay = 10,
                    WeightInGrams = 1,
                    CostInUSDP = 1,
                    Note = "Can't have too much!"
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Tree Straps",
                    Url = "http://shop.whoopieslings.com/Tree-Huggers-TH.htm",
                    WeightInGrams = 198,
                    CostInUSDP = 1200,
                    Note = "12'x1\". Includes dutch buckle and titanium dutch clip (max 300 pounds)."
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "5g Water Jug",
                    Carried = GearCarried.NotCarried
                }
            );

            ListItems.Add(new GearItem
                {
                    Name = "Wind Screen",
                    Make = "Trail Designs",
                    Model = "Caldera Cone System",
                    Url = "http://www.traildesigns.com/stoves/caldera-cone-system",
                    WeightInGrams = 141,
                    CostInUSDP = 3400
                }
            );
#endregion
        }
    }
}
