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

using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals
{
    public sealed class MealListAdapter : BaseModelRecyclerListAdapter<Meal>
    {
        private sealed class MealViewHolder : BaseViewHolder
        {
            protected override int DeleteActionResourceId => Resource.Id.action_delete_meal;

            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            private readonly TextView _textViewServings;
            private readonly TextView _textViewWeightPerServing;
            private readonly TextView _textViewCaloriesPerServing;
            private readonly TextView _textViewProteinPerServing;
            private readonly TextView _textViewFiberPerServing;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCalories;
            private readonly TextView _textViewCost;

            public MealViewHolder(View itemView, BaseModelRecyclerListAdapter<Meal> adapter)
                : base(itemView, adapter)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_meal_toolbar);
                _toolbar.InflateMenu(Resource.Menu.meal_menu);
                _toolbar.SetOnMenuItemClickListener(this);

                _textViewServings = itemView.FindViewById<TextView>(Resource.Id.view_meal_servings);
                _textViewWeightPerServing = itemView.FindViewById<TextView>(Resource.Id.view_meal_weight_per_serving);
                _textViewCaloriesPerServing = itemView.FindViewById<TextView>(Resource.Id.view_meal_calories_per_serving);
                _textViewProteinPerServing = itemView.FindViewById<TextView>(Resource.Id.view_meal_protein_per_serving);
                _textViewFiberPerServing = itemView.FindViewById<TextView>(Resource.Id.view_meal_fiber_per_serving);
                _textViewWeight = itemView.FindViewById<TextView>(Resource.Id.view_meal_weight);
                _textViewCalories = itemView.FindViewById<TextView>(Resource.Id.view_meal_calories);
                _textViewCost = itemView.FindViewById<TextView>(Resource.Id.view_meal_cost);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewMealFragment
                {
                    Item = ListItem
                };
            }

            protected override void UpdateView()
            {
                _toolbar.Title = ListItem.Name;

                _textViewServings.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_meal_servings),
                    ListItem.ServingCount, ListItem.MealTime.ToString()
                );

                int weightInUnitsPerServing = (int)ListItem.GetWeightInUnitsPerServing(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings);
                _textViewWeightPerServing.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_meal_weight_per_serving),
                    weightInUnitsPerServing, Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnitsPerServing != 1)
                );

                _textViewCaloriesPerServing.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_meal_calories_per_serving),
                    (int)ListItem.CaloriesPerServing
                );

                _textViewProteinPerServing.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_meal_protein_per_serving),
                    (int)ListItem.ProteinPerServing
                );

                _textViewFiberPerServing.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_meal_fiber_per_serving),
                    (int)ListItem.FiberPerServing
                );                

                int weightInUnits = (int)ListItem.GetWeightInUnits(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings);
                _textViewWeight.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_meal_weight),
                    weightInUnits, Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                int caloriesPerWeight = (int)ListItem.GetCaloriesPerWeightInUnits(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings);
                _textViewCalories.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_meal_calories),
                    caloriesPerWeight, Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );

                string formattedCost = ListItem.GetCostInCurrency(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                string formattedCostPerWeight = ListItem.GetCostInCurrencyPerWeightInUnits(Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = Java.Lang.String.Format(Adapter.Fragment.BaseActivity.Resources.GetString(Resource.String.label_view_meal_cost),
                    formattedCost, formattedCostPerWeight, Adapter.Fragment.BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );
            }
        }

        public override int LayoutResource => Resource.Layout.view_meal;

        public MealListAdapter(ListItemsFragment<Meal> fragment)
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
            case 1:         // Meal
                FilteredListItems = FilteredListItems.OrderBy(x => x.MealTime);
                break;
            case 2:         // Weight
                FilteredListItems = FilteredListItems.OrderBy(x => x.WeightInGrams);
                break;
            case 3:         // Cost
                FilteredListItems = FilteredListItems.OrderBy(x => x.CostInUSDP);
                break;
            case 4:         // Cost / Weight
                // TODO
                break;
            case 5:         // Calories
                // TODO
                break;
            }
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new MealViewHolder(itemView, this);
        }
    }
}
