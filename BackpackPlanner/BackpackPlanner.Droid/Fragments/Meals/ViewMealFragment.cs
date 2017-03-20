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
    public sealed class ViewMealFragment : ViewItemFragment<Meal>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_meal;

        protected override int TitleResource => Resource.String.title_view_meal;

        protected override int SaveItemResource => Resource.Id.fab_save_meal;

        protected override int ResetItemResource => Resource.Id.fab_reset_meal;

        protected override int DeleteItemResource => Resource.Id.fab_delete_meal;

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

            _mealNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_name);
            _mealNameEditText.EditText.Text = Item.Name;

            _mealWebsiteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_website);
            _mealWebsiteEditText.EditText.Text = Item.Url;

            _mealMealTimeSpinner = view.FindViewById<Spinner>(Resource.Id.view_meal_mealtime);
            _mealMealTimeSpinner.SetSelection(((ArrayAdapter)_mealMealTimeSpinner.Adapter).GetPosition(Item.MealTime.ToString()));

            _mealServingsEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_servings);
            _mealServingsEditText.EditText.Text = Item.ServingCount.ToString();

            _mealWeightEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_weight);
            _mealWeightEditText.EditText.Text = ((int)Item.WeightInUnits).ToString();

            _mealCostEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_cost);
            _mealCostEditText.EditText.Text = ((int)Item.CostInCurrency).ToString();

            _mealCaloriesEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_calories);
            _mealCaloriesEditText.EditText.Text = Item.Calories.ToString();

            _mealProteinEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_protein);
            _mealProteinEditText.EditText.Text = Item.ProteinInGrams.ToString();

            _mealFiberEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_fiber);
            _mealFiberEditText.EditText.Text = Item.FiberInGrams.ToString();

            _mealNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_note);
            _mealNoteEditText.EditText.Text = Item.Note;

            _mealWeightEditText.Hint = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_meal_weight),
                DroidState.Instance.BackpackPlannerState.Settings.Units.GetSmallWeightString(true)
            );

            _mealCostEditText.Hint = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_meal_cost),
                DroidState.Instance.BackpackPlannerState.Settings.Currency.GetCurrencyString()
            );
        }

        protected override void OnDoDataExchange()
        {
            Item.Name = _mealNameEditText.EditText.Text;
            Item.Url = _mealWebsiteEditText.EditText.Text;
            Item.MealTime = (MealTime)Enum.Parse(typeof(MealTime), _mealMealTimeSpinner.SelectedItem.ToString());
            Item.ServingCount = Convert.ToInt32(_mealServingsEditText.EditText.Text);
            Item.WeightInUnits = Convert.ToSingle(_mealWeightEditText.EditText.Text);
            Item.CostInCurrency = Convert.ToSingle(_mealCostEditText.EditText.Text);
            Item.Calories = Convert.ToInt32(_mealCaloriesEditText.EditText.Text);
            Item.ProteinInGrams = Convert.ToInt32(_mealProteinEditText.EditText.Text);
            Item.FiberInGrams = Convert.ToInt32(_mealFiberEditText.EditText.Text);
            Item.Note = _mealNoteEditText.EditText.Text;
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

        protected override void OnReset()
        {
// TODO
        }
    }
}
