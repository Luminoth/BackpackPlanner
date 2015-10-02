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
using System.Globalization;
using System.Linq;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear
{
    public class GearItemListAdapter : BaseListAdapter<GearItem>
    {
        private class GearItemViewHolder : BaseViewHolder
        {
            protected override int DeleteActionResourceId => Resource.Id.action_delete_gear_item;

            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            private readonly TextView _textViewMakeModel;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCost;

            public GearItemViewHolder(View itemView, BaseListAdapter<GearItem> adapter) : base(itemView, adapter)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_gear_item_toolbar);
                _toolbar.InflateMenu(Resource.Menu.gear_item_menu);
                _toolbar.SetOnMenuItemClickListener(this);

                _textViewMakeModel = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_make_model);
                _textViewWeight = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_weight);
                _textViewCost = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearItemFragment
                {
                    Item = ListItem
                };
            }

            protected override void UpdateView()
            {
                _toolbar.Title = ListItem.Name;

                string makeModel = $"{ListItem.Make} {ListItem.Model}";
                if(string.IsNullOrWhiteSpace(makeModel)) {
                    _textViewMakeModel.Visibility = ViewStates.Gone;
                    _textViewMakeModel.Text = string.Empty;
                } else {
                    _textViewMakeModel.Visibility = ViewStates.Visible;
                    _textViewMakeModel.Text = makeModel;
                }

                _textViewWeight.Text = $"{ListItem.WeightInUnits} {BackpackPlannerState.Instance.Settings.Units.GetSmallWeightString()}";

                string formattedCost = ListItem.CostInCurrency.ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = $"{formattedCost} (cost per weight)";
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_item;

        public GearItemListAdapter(ListItemsFragment<GearItem> fragment) : base(fragment)
        {
        }

        protected override void SortItemsByPosition(int position)
        {
            switch(position)
            {
            case 0:         // Name
                FilteredListItems = FilteredListItems.OrderBy(x => x.Name, StringComparer.CurrentCulture);
                break;
            case 1:         // Weight
                FilteredListItems = FilteredListItems.OrderBy(x => x.WeightInGrams);
                break;
            case 2:         // Cost
                FilteredListItems = FilteredListItems.OrderBy(x => x.CostInUSDP);
                break;
            case 3:         // Cost / Weight
                // TODO
                break;
            }
        }

        protected override void FilterItems(string text)
        {
            FilteredListItems = from item in ListItems where item.Name.ToLower().Contains(text) select item;
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearItemViewHolder(itemView, this);
        }
    }
}
