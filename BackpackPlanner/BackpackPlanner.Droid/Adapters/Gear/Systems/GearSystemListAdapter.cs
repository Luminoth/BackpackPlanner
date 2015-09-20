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
using System.Globalization;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Systems
{
    public class GearSystemListAdapter : BaseListAdapter<GearSystem>
    {
        private class GearSystemViewHolder : BaseViewHolder
        {
            private readonly TextView _textViewName;
            private readonly TextView _textViewItems;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCost;

            public GearSystemViewHolder(View itemView, ListItemsFragment<GearSystem> fragment) : base(itemView, fragment)
            {
                _textViewName = itemView.FindViewById<TextView>(Resource.Id.view_gear_system_name);
                _textViewItems = itemView.FindViewById<TextView>(Resource.Id.view_gear_system_items);
                _textViewWeight = itemView.FindViewById<TextView>(Resource.Id.view_gear_system_weight);
                _textViewCost = itemView.FindViewById<TextView>(Resource.Id.view_gear_system_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearSystemFragment();
            }

            protected override void UpdateView()
            {
                _textViewName.Text = ListItem.Name;
                _textViewItems.Text = $"{ListItem.GearItemCount} item(s)";
                /*_textViewWeight.Text = $"{ListItem.WeightInUnits} {BackpackPlannerState.Instance.Settings.Units.GetSmallWeightString()}";

                string formattedCost = ListItem.CostInCurrency.ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = $"{formattedCost} (cost per weight)";*/
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_system;

        public GearSystemListAdapter(ListItemsFragment<GearSystem> fragment, IEnumerable<GearSystem> listItems) : base(fragment, listItems)
        {
        }

        public override void SortByItemSelectedEventHander(object sender, AdapterView.ItemSelectedEventArgs args)
        {
            // TODO: sort the list
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearSystemViewHolder(itemView, Fragment);
        }
    }
}
