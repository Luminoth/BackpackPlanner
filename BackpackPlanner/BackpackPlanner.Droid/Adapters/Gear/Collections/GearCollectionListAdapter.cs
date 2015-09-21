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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections
{
    public class GearCollectionListAdapter : BaseListAdapter<GearCollection>
    {
        private class GearCollectionViewHolder : BaseViewHolder, Android.Support.V7.Widget.Toolbar.IOnMenuItemClickListener
        {
            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            private readonly TextView _textViewSystems;
            private readonly TextView _textViewItems;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCost;

            public GearCollectionViewHolder(View itemView, ListItemsFragment<GearCollection> fragment) : base(itemView, fragment)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_gear_collection_toolbar);
                _toolbar.InflateMenu(Resource.Menu.gear_collection_menu);
                _toolbar.SetOnMenuItemClickListener(this);

                _textViewSystems = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_systems);
                _textViewItems = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_items);
                _textViewWeight = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_weight);
                _textViewCost = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_cost);
            }

            public bool OnMenuItemClick(IMenuItem menuItem)
            {
                // TODO
                return true;
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearCollectionFragment();
            }

            protected override void UpdateView()
            {
                _toolbar.Title = ListItem.Name;

                _textViewSystems.Text = $"{ListItem.GearSystemCount} system(s)";
                _textViewItems.Text = $"{ListItem.GearItemCount} item(s) (some total)";
                /*_textViewWeight.Text = $"{ListItem.WeightInUnits} {BackpackPlannerState.Instance.Settings.Units.GetSmallWeightString()}";

                string formattedCost = ListItem.CostInCurrency.ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = $"{formattedCost} (cost per weight)";*/
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_collection;

        public GearCollectionListAdapter(ListItemsFragment<GearCollection> fragment, IEnumerable<GearCollection> listItems) : base(fragment, listItems)
        {
        }

        public override void SortByItemSelectedEventHander(object sender, AdapterView.ItemSelectedEventArgs args)
        {
            // TODO: can this be made clearer somehow by using args.Id?
            switch(args.Position)
            {
            case 0:         // Name
                FilteredListItems = FilteredListItems.OrderBy(x => x.Name, StringComparer.CurrentCulture);
                break;
            case 1:         // Items
                // TODO
                break;
            case 2:         // Weight
                // TODO
                break;
            case 3:         // Cost
                // TODO
                break;
            case 4:         // Cost / Weight
                // TODO
                break;
            }
            NotifyDataSetChanged();
        }

        public override void FilterItems(object sender, Android.Support.V7.Widget.SearchView.QueryTextChangeEventArgs args)
        {
            FilteredListItems = from item in ListItems where item.Name.ToLower().Contains(args.NewText) select item;
            NotifyDataSetChanged();
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearCollectionViewHolder(itemView, Fragment);
        }
    }
}
