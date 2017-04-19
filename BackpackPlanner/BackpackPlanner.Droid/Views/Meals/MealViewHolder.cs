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

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
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

            _mealWeightEditText.Hint = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_meal_weight),
                BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(true)
            );

            _mealCostEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.meal_cost);
            _mealCostEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Cost");
            };

            _mealCostEditText.Hint = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_meal_cost),
                BaseActivity.BackpackPlannerState.Settings.Currency.GetCurrencyString()
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
            _mealWeightEditText.EditText.Text = ((int)meal.GetWeightInUnits(BaseActivity.BackpackPlannerState.Settings)).ToString();
            _mealCostEditText.EditText.Text = ((int)meal.GetCostInCurrency(BaseActivity.BackpackPlannerState.Settings)).ToString();
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
            meal.SetWeightInUnits(BaseActivity.BackpackPlannerState.Settings, Convert.ToSingle(_mealWeightEditText.EditText.Text));
            meal.SetCostInCurrency(BaseActivity.BackpackPlannerState.Settings, Convert.ToSingle(_mealCostEditText.EditText.Text));
            meal.Calories = Convert.ToInt32(_mealCaloriesEditText.EditText.Text);
            meal.ProteinInGrams = Convert.ToInt32(_mealProteinEditText.EditText.Text);
            meal.FiberInGrams = Convert.ToInt32(_mealFiberEditText.EditText.Text);
            meal.Note = _mealNoteEditText.EditText.Text;

            base.DoDataExchange(meal, dbContext);
        }
    }
}