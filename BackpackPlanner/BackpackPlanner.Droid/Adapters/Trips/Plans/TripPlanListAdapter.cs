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
using System.Linq;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Plans
{
    public sealed class TripPlanListAdapter : BaseModelRecyclerListAdapter<TripPlan>
    {
        private sealed class TripPlanViewHolder : BaseModelViewHolder
        {
            protected override int ToolbarResourceId => Resource.Id.view_trip_plan_toolbar;

            protected override int MenuResourceId => Resource.Menu.trip_plan_menu;

            protected override int DeleteActionResourceId => Resource.Id.action_delete_trip_plan;

            private readonly TextView _textViewDays;
            private readonly TextView _textViewMeals;
            private readonly TextView _textViewCollections;
            private readonly TextView _textViewSystems;
            private readonly TextView _textViewItems;
            private readonly TextView _textViewBaseWeight;
            private readonly TextView _textViewPackWeight;
            private readonly TextView _textViewSkinOutWeight;
            private readonly TextView _textViewCost;

            public TripPlanViewHolder(View view, BaseRecyclerListAdapter<TripPlan> adapter)
                : base(view, adapter)
            {
                _textViewDays = view.FindViewById<TextView>(Resource.Id.view_trip_plan_days);
                _textViewMeals = view.FindViewById<TextView>(Resource.Id.view_trip_plan_meals);
                _textViewCollections = view.FindViewById<TextView>(Resource.Id.view_trip_plan_collections);
                _textViewSystems = view.FindViewById<TextView>(Resource.Id.view_trip_plan_systems);
                _textViewItems = view.FindViewById<TextView>(Resource.Id.view_trip_plan_items);
                _textViewBaseWeight = view.FindViewById<TextView>(Resource.Id.view_trip_plan_base_weight);
                _textViewPackWeight = view.FindViewById<TextView>(Resource.Id.view_trip_plan_pack_weight);
                _textViewSkinOutWeight = view.FindViewById<TextView>(Resource.Id.view_trip_plan_skinout_weight);
                _textViewCost = view.FindViewById<TextView>(Resource.Id.view_trip_plan_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewTripPlanFragment(Item);
            }

            public override void UpdateView(TripPlan tripPlan)
            {
                base.UpdateView(tripPlan);

                _textViewDays.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_days),
                    tripPlan.Days
                );

                _textViewMeals.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_meals),
                    tripPlan.Meals.Count, tripPlan.GetTotalCalories()
                );

                _textViewCollections.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_collections),
                    tripPlan.GearCollections.Count
                );

                _textViewSystems.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_systems),
                    tripPlan.GearSystems.Count
                );

                _textViewItems.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_items),
                    tripPlan.GearItems.Count, tripPlan.GetTotalGearItemCount()
                );

                int weightInUnits = (int)tripPlan.GetBaseWeightInUnits();
                _textViewBaseWeight.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_base_weight),
                    weightInUnits, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                weightInUnits = (int)tripPlan.GetBaseWeightInUnits();
                _textViewPackWeight.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_pack_weight),
                    weightInUnits, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                weightInUnits = (int)tripPlan.GetSkinOutWeightInUnits();
                _textViewSkinOutWeight.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_skinout_weight),
                    weightInUnits, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                string formattedCost = tripPlan.GetTotalCostInCurrency(BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                string formattedCostPerWeight = tripPlan.GetCostPerWeightInCurrency(BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_trip_plan_cost),
                    formattedCost, formattedCostPerWeight, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );
            }
        }

        protected override int LayoutResource => Resource.Layout.view_trip_plan;

        public TripPlanListAdapter(ListItemsFragment<TripPlan> fragment)
            : base(fragment)
        {
        }

        protected override IEnumerable<TripPlan> SortItemsByPosition(int position, IEnumerable<TripPlan> items)
        {
            switch(position)
            {
            case 0:         // Name
                return from x in items orderby x?.Name select x;
            case 1:         // Items
                // TODO
                return items;
            case 2:         // Weight
                // TODO
                return items;
            case 3:         // Cost
                // TODO
                return items;
            case 4:         // Cost / Weight
                // TODO
                return items;
            }
            return items;
        }

        protected override BaseViewHolder CreateViewHolder(View view, BaseRecyclerListAdapter<TripPlan> adapter)
        {
            return new TripPlanViewHolder(view, adapter);
        }
    }
}
