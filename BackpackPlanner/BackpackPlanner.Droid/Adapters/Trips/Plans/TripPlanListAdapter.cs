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
using EnergonSoftware.BackpackPlanner.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Plans
{
    public class TripPlanListAdapter : BaseListAdapter<TripPlan>
    {
        private class TripPlanViewHolder : BaseViewHolder
        {
            protected override int DeleteActionResourceId => Resource.Id.action_delete_trip_plan;

            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            private readonly TextView _textViewDays;
            private readonly TextView _textViewMeals;
            private readonly TextView _textViewCollections;
            private readonly TextView _textViewSystems;
            private readonly TextView _textViewItems;
            private readonly TextView _textViewCost;

            public TripPlanViewHolder(View itemView, ListItemsFragment<TripPlan> fragment) : base(itemView, fragment)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_trip_plan_toolbar);
                _toolbar.InflateMenu(Resource.Menu.trip_plan_menu);
                _toolbar.SetOnMenuItemClickListener(this);

                _textViewDays = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_days);
                _textViewMeals = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_meals);
                _textViewCollections = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_collections);
                _textViewSystems = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_systems);
                _textViewItems = itemView.FindViewById<TextView>(Resource.Id.view_trip_plan_items);
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

                //_textViewDays.Text = $"{ListItem.Days} day(s)";
                _textViewCollections.Text = $"{ListItem.MealCount} meal(s)";
                _textViewCollections.Text = $"{ListItem.GearCollectionCount} collection(s)";
                _textViewSystems.Text = $"{ListItem.GearSystemCount} system(s)";
                _textViewItems.Text = $"{ListItem.GearItemCount} item(s) (some total)";

                /*string formattedCost = ListItem.CostInCurrency.ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = $"{formattedCost} (cost per weight)";*/
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

        public override void FilterItems(object sender, Android.Support.V7.Widget.SearchView.QueryTextChangeEventArgs args)
        {
            FilteredListItems = from item in ListItems where item.Name.ToLower().Contains(args.NewText) select item;
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new TripPlanViewHolder(itemView, Fragment);
        }
    }
}
