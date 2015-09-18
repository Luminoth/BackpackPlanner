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
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items
{
    public class GearItemListAdapter : BaseListAdapter<GearItem>
    {
        private class GearItemViewHolder : BaseViewHolder
        {
            private readonly TextView _textViewName;
            private readonly TextView _textViewMakeModel;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCost;

            public GearItemViewHolder(View itemView, BaseFragment fragment) : base(itemView, fragment)
            {
                _textViewName = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_name);
                _textViewMakeModel = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_make_model);
                _textViewWeight = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_weight);
                _textViewCost = itemView.FindViewById<TextView>(Resource.Id.view_gear_item_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearItemFragment();
            }

            protected override void UpdateView()
            {
                _textViewName.Text = ListItem.Name;

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

        public GearItemListAdapter(ListItemsFragment<GearItem> fragment, IEnumerable<GearItem> listItems) : base(fragment, listItems)
        {
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearItemViewHolder(itemView, Fragment);
        }
    }
}
