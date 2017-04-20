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

using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals
{
    public sealed class MealListAdapter : BaseModelRecyclerListAdapter<Meal>
    {
        private sealed class MealViewHolder : BaseModelRecyclerViewHolder
        {
            protected override int ToolbarResourceId => Resource.Id.view_meal_toolbar;

            protected override int MenuResourceId => Resource.Menu.meal_menu;

            protected override int DeleteActionResourceId => Resource.Id.action_delete_meal;

            private readonly TextView _textViewServings;
            private readonly TextView _textViewWeightPerServing;
            private readonly TextView _textViewCaloriesPerServing;
            private readonly TextView _textViewProteinPerServing;
            private readonly TextView _textViewFiberPerServing;
            private readonly TextView _textViewWeight;
            private readonly TextView _textViewCalories;
            private readonly TextView _textViewCost;

            public MealViewHolder(View view, BaseRecyclerListAdapter<Meal> adapter)
                : base(view, adapter)
            {
                _textViewServings = view.FindViewById<TextView>(Resource.Id.view_meal_servings);
                _textViewWeightPerServing = view.FindViewById<TextView>(Resource.Id.view_meal_weight_per_serving);
                _textViewCaloriesPerServing = view.FindViewById<TextView>(Resource.Id.view_meal_calories_per_serving);
                _textViewProteinPerServing = view.FindViewById<TextView>(Resource.Id.view_meal_protein_per_serving);
                _textViewFiberPerServing = view.FindViewById<TextView>(Resource.Id.view_meal_fiber_per_serving);
                _textViewWeight = view.FindViewById<TextView>(Resource.Id.view_meal_weight);
                _textViewCalories = view.FindViewById<TextView>(Resource.Id.view_meal_calories);
                _textViewCost = view.FindViewById<TextView>(Resource.Id.view_meal_cost);
            }

            protected override ViewItemFragment<Meal> CreateViewItemFragment()
            {
                return new ViewMealFragment();
            }

            public override void UpdateView(Meal meal)
            {
                base.UpdateView(meal);

                _textViewServings.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_meal_servings),
                    meal.ServingCount, meal.MealTime.ToString()
                );

                int weightInUnitsPerServing = (int)meal.GetWeightInUnitsPerServing(BaseActivity.BackpackPlannerState.Settings);
                _textViewWeightPerServing.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_meal_weight_per_serving),
                    weightInUnitsPerServing, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnitsPerServing != 1)
                );

                _textViewCaloriesPerServing.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_meal_calories_per_serving),
                    (int)meal.CaloriesPerServing
                );

                _textViewProteinPerServing.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_meal_protein_per_serving),
                    (int)meal.ProteinPerServing
                );

                _textViewFiberPerServing.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_meal_fiber_per_serving),
                    (int)meal.FiberPerServing
                );                

                int weightInUnits = (int)meal.GetWeightInUnits(BaseActivity.BackpackPlannerState.Settings);
                _textViewWeight.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_meal_weight),
                    weightInUnits, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
                );

                int caloriesPerWeight = (int)meal.GetCaloriesPerWeightInUnits(BaseActivity.BackpackPlannerState.Settings);
                _textViewCalories.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_meal_calories),
                    caloriesPerWeight, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );

                string formattedCost = meal.GetCostInCurrency(BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                string formattedCostPerWeight = meal.GetCostInCurrencyPerWeightInUnits(BaseActivity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
                _textViewCost.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_meal_cost),
                    formattedCost, formattedCostPerWeight, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
                );
            }
        }

        protected override int LayoutResource => Resource.Layout.view_meal;

        public MealListAdapter(ListItemsFragment<Meal> fragment)
            : base(fragment)
        {
        }

        protected override IEnumerable<Meal> SortItemsByPosition(int position, IEnumerable<Meal> items)
        {
            switch(position)
            {
            case 0:         // Name
                return from x in items orderby x?.Name select x;
            case 1:         // Meal
                return from x in items orderby x?.MealTime select x;
            case 2:         // Weight
                return from x in items orderby x?.WeightInGrams select x;
            case 3:         // Cost
                return from x in items orderby x?.CostInUSDP select x;
            case 4:         // Cost / Weight
                // TODO
                return items;
            case 5:         // Calories
                // TODO
                return items;
            }
            return items;
        }

        protected override BaseRecyclerViewHolder CreateViewHolder(View view, BaseRecyclerListAdapter<Meal> adapter)
        {
            return new MealViewHolder(view, adapter);
        }
    }
}
