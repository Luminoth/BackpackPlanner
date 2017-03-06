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
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips
{
    public sealed class TripPlanListAdapter : BaseListAdapter<TripPlan>
    {
        private sealed class TripPlanViewHolder : BaseViewHolder
        {
            protected override int DeleteActionResourceId => Resource.Id.action_delete_trip_plan;

            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            private readonly TextView _textViewDays;
            private readonly TextView _textViewMeals;
            private readonly TextView _textViewCollections;
            private readonly TextView _textViewSystems;
            private readonly TextView _textViewItems;
            private readonly TextView _textViewBaseWeight;
            private readonly TextView _textViewPackWeight;
            private readonly TextView _textViewSkinOutWeight;
            private readonly TextView _textViewCost;

            public TripPlanViewHolder(View itemView, BaseListAdapter<TripPlan> adapter) : base(itemView, adapter)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_trip_plan_toolbar);
                _toolbar.InflateMenu(Resource.Menu.trip_plan_menu);
                _toolbar.SetOnMenuItemClickListener(this);

                _textViewDays = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_days);
                _textViewMeals = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_meals);
                _textViewCollections = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_collections);
                _textViewSystems = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_systems);
                _textViewItems = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_items);
                _textViewBaseWeight = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_base_weight);
                _textViewPackWeight = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_pack_weight);
                _textViewSkinOutWeight = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_skinout_weight);
                _textViewCost = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewTripPlanFragment
                {
                    Item = ListItem
                };
            }

            protected override void UpdateView()
            {
                _toolbar.Title = ListItem.Name;

                _textViewDays.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_days),
                    ListItem.Days
                );

                _textViewMeals.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_meals),
                    ListItem.MealCount, ListItem.GetTotalCalories()
                );

                _textViewCollections.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_collections),
                    ListItem.GearSystemCount
                );

                _textViewSystems.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_systems),
                    ListItem.GearSystemCount
                );

                _textViewItems.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_items),
                    ListItem.GearItemCount, ListItem.GetTotalGearItemCount()
                );

                int weightInUnits = (int)ListItem.GetBaseWeightInUnits();
                _textViewBaseWeight.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_base_weight),
                    weightInUnits, DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                weightInUnits = (int)ListItem.GetBaseWeightInUnits();
                _textViewPackWeight.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_pack_weight),
                    weightInUnits, DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                weightInUnits = (int)ListItem.GetSkinOutWeightInUnits();
                _textViewSkinOutWeight.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_skinout_weight),
                    weightInUnits, DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                string formattedCost = ListItem.GetCostInCurrency().ToString("C", CultureInfo.CurrentCulture);
                string formattedCostPerWeight = ListItem.GetCostPerWeightInCurrency().ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_cost),
                    formattedCost, formattedCostPerWeight, DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );
            }
        }

        public override int LayoutResource => Resource.Layout.view_trip_plan;

        public TripPlanListAdapter(ListItemsFragment<TripPlan> fragment) : base(fragment)
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
            return new TripPlanViewHolder(itemView, this);
        }
    }
}
