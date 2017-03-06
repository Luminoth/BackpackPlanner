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
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear
{
    public sealed class GearSystemListAdapter : BaseListAdapter<GearSystem>
    {
        private sealed class GearSystemViewHolder : BaseViewHolder
        {
            protected override int DeleteActionResourceId => Resource.Id.action_delete_gear_system;

            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            private readonly TextView _textViewItems;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCost;

            public GearSystemViewHolder(View itemView, BaseListAdapter<GearSystem> adapter) : base(itemView, adapter)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_gear_system_toolbar);
                _toolbar.InflateMenu(Resource.Menu.gear_system_menu);
                _toolbar.SetOnMenuItemClickListener(this);

                _textViewItems = itemView.FindViewById<TextView>(Resource.Id.view_gear_system_items);
                _textViewWeight = itemView.FindViewById<TextView>(Resource.Id.view_gear_system_weight);
                _textViewCost = itemView.FindViewById<TextView>(Resource.Id.view_gear_system_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearSystemFragment
                {
                    Item = ListItem
                };
            }

            protected override void UpdateView()
            {
                _toolbar.Title = ListItem.Name;

                _textViewItems.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_system_items),
                    ListItem.GearItemCount
                );

                int weightInUnits = (int)ListItem.GetWeightInUnits();
                _textViewWeight.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_system_weight),
                    weightInUnits, DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                string formattedCost = ListItem.GetCostInCurrency().ToString("C", CultureInfo.CurrentCulture);
                string formattedCostPerWeight = ListItem.GetCostPerWeightInCurrency().ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_system_cost),
                    formattedCost, formattedCostPerWeight, DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_system;

        public GearSystemListAdapter(ListItemsFragment<GearSystem> fragment) : base(fragment)
        {
        }

        protected override void SortItemsByPosition(int position)
        {
            switch(position)
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
        }

        protected override void FilterItems(string text)
        {
            FilteredListItems = from item in ListItems where item.Name.ToLower().Contains(text) select item;
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearSystemViewHolder(itemView, this);
        }
    }
}
