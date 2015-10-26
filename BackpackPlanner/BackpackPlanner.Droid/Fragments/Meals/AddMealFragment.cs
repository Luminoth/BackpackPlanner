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

using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Models.Meals;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals
{
    public class AddMealFragment : AddItemFragment<Meal>
    {
        protected override int LayoutResource => Resource.Layout.fragment_add_meal;

        protected override int TitleResource => Resource.String.title_add_meal;

        protected override int AddItemResource => Resource.Id.button_add_meal;

        protected override bool HasSearchView => false;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _mealNameEditText;
        private Android.Support.Design.Widget.TextInputLayout _mealWebsiteEditText;
        private Spinner _mealMealTimeSpinner;
        private Android.Support.Design.Widget.TextInputLayout _mealServingsEditText;
        private Android.Support.Design.Widget.TextInputLayout _mealWeightEditText;
        private Android.Support.Design.Widget.TextInputLayout _mealCostEditText;
        private Android.Support.Design.Widget.TextInputLayout _mealCaloriesEditText;
        private Android.Support.Design.Widget.TextInputLayout _mealProteinEditText;
        private Android.Support.Design.Widget.TextInputLayout _mealFiberEditText;
        private Android.Support.Design.Widget.TextInputLayout _mealNoteEditText;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _mealNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_meal_name);
            _mealWebsiteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_meal_website);
            _mealMealTimeSpinner = view.FindViewById<Spinner>(Resource.Id.add_meal_mealtime);
            _mealServingsEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_meal_servings);
            _mealWeightEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_meal_weight);
            _mealCostEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_meal_cost);
            _mealCaloriesEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_meal_calories);
            _mealProteinEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_meal_protein);
            _mealFiberEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_meal_fiber);
            _mealNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_meal_note);

            _mealWeightEditText.Hint = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_meal_weight),
                BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(true)
            );

            _mealCostEditText.Hint = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_meal_cost),
                BaseActivity.BackpackPlannerState.Settings.Currency.GetCurrencyString()
            );
        }

        protected override void OnDoDataExchange()
        {
            Item = new Meal(BaseActivity.BackpackPlannerState.Settings)
            {
                Name = _mealNameEditText.EditText.Text,
                Url = _mealWebsiteEditText.EditText.Text,
                MealTime = (MealTime)Enum.Parse(typeof(MealTime), _mealMealTimeSpinner.SelectedItem.ToString()),
                ServingCount = Convert.ToInt32(_mealServingsEditText.EditText.Text),
                WeightInUnits = Convert.ToSingle(_mealWeightEditText.EditText.Text),
                CostInCurrency = Convert.ToSingle(_mealCostEditText.EditText.Text),
                Calories = Convert.ToInt32(_mealCaloriesEditText.EditText.Text),
                ProteinInGrams = Convert.ToInt32(_mealProteinEditText.EditText.Text),
                FiberInGrams = Convert.ToInt32(_mealFiberEditText.EditText.Text),
                Note = _mealNoteEditText.EditText.Text
            };
        }

        protected override bool OnValidate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_mealNameEditText.EditText.Text)) {
                _mealNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }
    }
}
