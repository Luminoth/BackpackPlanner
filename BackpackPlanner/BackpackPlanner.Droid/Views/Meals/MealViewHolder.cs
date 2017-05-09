/*
   Copyright 2017 Shane Lillie

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

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Views.Meals
{
    public sealed class MealViewHolder : BaseModelViewHolder<Meal>
    {
        private readonly Android.Support.Design.Widget.TextInputLayout _mealNameEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _mealWebsiteEditText;
        private readonly Spinner _mealMealTimeSpinner;
        private readonly Android.Support.Design.Widget.TextInputLayout _mealServingsEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _mealWeightEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _mealCostEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _mealCaloriesEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _mealProteinEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _mealFiberEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _mealNoteEditText;

        public MealViewHolder(BaseActivity activity, View view)
            : base(activity)
        {
            _mealNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_name);
            _mealNameEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Name");
            };

            _mealWebsiteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_website);
            _mealWebsiteEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Website");
            };

            _mealMealTimeSpinner = view.FindViewById<Spinner>(Resource.Id.meal_mealtime);
            _mealMealTimeSpinner.ItemSelected += (sender, args) =>
            {
                NotifyPropertyChanged("Mealtime");
            };

            _mealServingsEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_servings);
            _mealServingsEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Servings");
            };

            _mealWeightEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_weight);
            _mealWeightEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Weight");
            };

            _mealWeightEditText.Hint = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_meal_weight),
                Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(true)
            );

            _mealCostEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_cost);
            _mealCostEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Cost");
            };

            _mealCostEditText.Hint = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_meal_cost),
                Activity.BackpackPlannerState.Settings.Currency.GetCurrencyString()
            );

            _mealCaloriesEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_calories);
            _mealCaloriesEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Calories");
            };

            _mealProteinEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_protein);
            _mealProteinEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Protein");
            };

            _mealFiberEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_fiber);
            _mealFiberEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Fiber");
            };

            _mealNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_note);
            _mealNoteEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Note");
            };
        }

        public override void UpdateView(Meal meal)
        {
            base.UpdateView(meal);

            _mealNameEditText.EditText.Text = meal.Name;
            _mealWebsiteEditText.EditText.Text = meal.Url;
            _mealMealTimeSpinner.SetSelection(((ArrayAdapter)_mealMealTimeSpinner.Adapter).GetPosition(meal.MealTime.ToString()));
            _mealServingsEditText.EditText.Text = meal.ServingCount.ToString();
            _mealWeightEditText.EditText.Text = ((int)meal.GetWeightInUnits(Activity.BackpackPlannerState.Settings)).ToString();
            _mealCostEditText.EditText.Text = ((int)meal.GetCostInCurrency(Activity.BackpackPlannerState.Settings)).ToString();
            _mealCaloriesEditText.EditText.Text = meal.Calories.ToString();
            _mealProteinEditText.EditText.Text = meal.ProteinInGrams.ToString();
            _mealFiberEditText.EditText.Text = meal.FiberInGrams.ToString();
            _mealNoteEditText.EditText.Text = meal.Note;
        }

        public override bool Validate()
        {
            bool valid = base.Validate();

            if(string.IsNullOrWhiteSpace(_mealNameEditText.EditText.Text)) {
                _mealNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        public override void DoDataExchange(Meal meal, DatabaseContext dbContext)
        {
            meal.Name = _mealNameEditText.EditText.Text;
            meal.Url = _mealWebsiteEditText.EditText.Text;
            meal.MealTime = (MealTime)Enum.Parse(typeof(MealTime), _mealMealTimeSpinner.SelectedItem.ToString());
            meal.ServingCount = Convert.ToInt32(_mealServingsEditText.EditText.Text);
            meal.SetWeightInUnits(Activity.BackpackPlannerState.Settings, Convert.ToSingle(_mealWeightEditText.EditText.Text));
            meal.SetCostInCurrency(Activity.BackpackPlannerState.Settings, Convert.ToSingle(_mealCostEditText.EditText.Text));
            meal.Calories = Convert.ToInt32(_mealCaloriesEditText.EditText.Text);
            meal.ProteinInGrams = Convert.ToInt32(_mealProteinEditText.EditText.Text);
            meal.FiberInGrams = Convert.ToInt32(_mealFiberEditText.EditText.Text);
            meal.Note = _mealNoteEditText.EditText.Text;

            base.DoDataExchange(meal, dbContext);
        }
    }

    public sealed class MealListViewHolder : BaseModelRecyclerViewHolder<Meal>
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

        public MealListViewHolder(View view, BaseRecyclerListAdapter<Meal> adapter)
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

            _textViewServings.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_meal_servings),
                meal.ServingCount, meal.MealTime.ToString()
            );

            int weightInUnitsPerServing = (int)meal.GetWeightInUnitsPerServing(Activity.BackpackPlannerState.Settings);
            _textViewWeightPerServing.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_meal_weight_per_serving),
                weightInUnitsPerServing, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnitsPerServing != 1)
            );

            _textViewCaloriesPerServing.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_meal_calories_per_serving),
                (int)meal.CaloriesPerServing
            );

            _textViewProteinPerServing.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_meal_protein_per_serving),
                (int)meal.ProteinPerServing
            );

            _textViewFiberPerServing.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_meal_fiber_per_serving),
                (int)meal.FiberPerServing
            );                

            int weightInUnits = (int)meal.GetWeightInUnits(Activity.BackpackPlannerState.Settings);
            _textViewWeight.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_meal_weight),
                weightInUnits, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
            );

            int caloriesPerWeight = (int)meal.GetCaloriesPerWeightInUnits(Activity.BackpackPlannerState.Settings);
            _textViewCalories.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_meal_calories),
                caloriesPerWeight, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
            );

            string formattedCost = meal.GetCostInCurrency(Activity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
            string formattedCostPerWeight = meal.GetCostInCurrencyPerWeightInUnits(Activity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
            _textViewCost.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_meal_cost),
                formattedCost, formattedCostPerWeight, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
            );
        }
    }

    public sealed class MealEntryViewHolder<T> : BaseModelEntryViewHolder<MealEntry<T>, T, Meal>
        where T: BaseModel<T>, new()
    {
        private readonly TextView _textViewName;
        private readonly TextView _textViewTotalWeight;
        private readonly Android.Support.Design.Widget.TextInputLayout _editTextQuantity;

        public MealEntryViewHolder(View view, BaseActivity activity)
            : base(activity)
        {
            _textViewName = view.FindViewById<TextView>(Resource.Id.view_meal_name);
            _textViewTotalWeight = view.FindViewById<TextView>(Resource.Id.view_meal_total_weight);

            _editTextQuantity = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_quantity);
            _editTextQuantity.EditText.AfterTextChanged += (sender, args) =>
            {
                UpdateTotalWeight(Item);
                NotifyPropertyChanged("Quantity");
            };
        }

        public override void UpdateView(MealEntry<T> item)
        {
            base.UpdateView(item);

            _textViewName.Text = item.Model.Name;
            _editTextQuantity.EditText.Text = item.Count.ToString();

            UpdateTotalWeight(item);
        }

        private void UpdateTotalWeight(MealEntry<T> item)
        {
            item.Count = string.IsNullOrWhiteSpace(_editTextQuantity.EditText.Text)
                ? 0
                : Convert.ToInt32(_editTextQuantity.EditText.Text);

            int totalWeightInUnits = (int)item.GetTotalWeightInUnits(Activity.BackpackPlannerState.Settings);
            _textViewTotalWeight.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_gear_item_total_weight),
                totalWeightInUnits, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(totalWeightInUnits != 1)
            );
        }
    }
}