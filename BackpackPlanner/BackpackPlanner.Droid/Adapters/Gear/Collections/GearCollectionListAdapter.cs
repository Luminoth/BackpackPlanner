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

using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections
{
    public sealed class GearCollectionListAdapter : BaseModelRecyclerListAdapter<GearCollection>
    {
        private sealed class GearCollectionViewHolder : BaseModelViewHolder
        {
            protected override int DeleteActionResourceId => Resource.Id.action_delete_gear_collection;

            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            private readonly TextView _textViewSystems;
            private readonly TextView _textViewItems;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCost;

            public GearCollectionViewHolder(View itemView, BaseModelRecyclerListAdapter<GearCollection> adapter)
                : base(itemView, adapter)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_gear_collection_toolbar);
                _toolbar.InflateMenu(Resource.Menu.gear_collection_menu);
                _toolbar.SetOnMenuItemClickListener(this);

                _textViewSystems = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_systems);
                _textViewItems = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_items);
                _textViewWeight = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_weight);
                _textViewCost = itemView.FindViewById<TextView>(Resource.Id.view_gear_collection_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewGearCollectionFragment
                {
                    Item = ListItem
                };
            }

            protected override void UpdateView()
            {
                _toolbar.Title = ListItem.Name;

                _textViewSystems.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_collection_systems),
                    ListItem.GearSystems.Count
                );

                _textViewItems.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_collection_items),
                    ListItem.GearItems.Count, ListItem.GetTotalGearItemCount()
                );

                int weightInUnits = (int)ListItem.GetTotalWeightInUnits(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings);
                _textViewWeight.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_collection_weight),
                    weightInUnits, Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                string formattedCost = ListItem.GetTotalCostInCurrency(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                string formattedCostPerWeight = ListItem.GetCostInCurrencyPerWeightInUnits(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_gear_collection_cost),
                    formattedCost, formattedCostPerWeight, Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );
            }
        }

        public override int LayoutResource => Resource.Layout.view_gear_collection;

        public GearCollectionListAdapter(ListItemsFragment<GearCollection> fragment)
            : base(fragment)
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

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new GearCollectionViewHolder(itemView, this);
        }
    }
}
