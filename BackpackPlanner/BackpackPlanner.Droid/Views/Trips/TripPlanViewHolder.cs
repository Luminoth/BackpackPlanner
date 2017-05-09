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

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Util;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Views.Trips
{
    public sealed class TripPlanViewHolder : BaseModelViewHolder<TripPlan>
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(TripPlanViewHolder));

        private readonly Android.Support.Design.Widget.TextInputLayout _tripPlanNameEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _tripPlanStartDateText;
        private readonly Android.Support.Design.Widget.TextInputLayout _tripPlanEndDateText;
        private readonly Android.Support.Design.Widget.TextInputLayout _tripPlanNoteEditText;

        public TripPlanViewHolder(BaseActivity activity, View view)
            : base(activity)
        {
            _tripPlanNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.trip_plan_name);
            _tripPlanNameEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Name");
            };

            _tripPlanStartDateText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.trip_plan_startdate);
            _tripPlanStartDateText.EditText.Click += (sender, args) =>
            {
                DateTime dateTime = DateTime.Now;
                try {
                    dateTime = Convert.ToDateTime(_tripPlanStartDateText.EditText.Text);
                } catch(FormatException) {
                }

                DatePickerFragment picker = new DatePickerFragment(dateTime);
                picker.DateSetEvent += (s, a) =>
                {
                    _tripPlanStartDateText.EditText.Text = a.Date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                    NotifyPropertyChanged("StartDate");
                };
                picker.Show(Activity.SupportFragmentManager, null);
            };

            _tripPlanEndDateText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.trip_plan_enddate);
            _tripPlanEndDateText.EditText.Click += (sender, args) =>
            {
                DateTime dateTime = DateTime.Now;
                try {
                    dateTime = Convert.ToDateTime(_tripPlanEndDateText.EditText.Text);
                } catch(FormatException) {
                }

                DatePickerFragment picker = new DatePickerFragment(dateTime);
                picker.DateSetEvent += (s, a) =>
                {
                    _tripPlanEndDateText.EditText.Text = a.Date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                    NotifyPropertyChanged("EndDate");
                };
                picker.Show(Activity.SupportFragmentManager, null);
            };

            _tripPlanNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.trip_plan_note);
            _tripPlanNoteEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Note");
            };
        }

        public override void UpdateView(TripPlan tripPlan)
        {
            base.UpdateView(tripPlan);

            _tripPlanNameEditText.EditText.Text = tripPlan.Name;
            _tripPlanStartDateText.EditText.Text = tripPlan.StartDate.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
            _tripPlanEndDateText.EditText.Text = tripPlan.EndDate.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
            _tripPlanNoteEditText.EditText.Text = tripPlan.Note;
        }

        public override bool Validate()
        {
            bool valid = base.Validate();

            if(string.IsNullOrWhiteSpace(_tripPlanNameEditText.EditText.Text)) {
                _tripPlanNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        public override void DoDataExchange(TripPlan tripPlan, DatabaseContext dbContext)
        {
            tripPlan.Name = _tripPlanNameEditText.EditText.Text;

            try {
                tripPlan.StartDate = Convert.ToDateTime(_tripPlanStartDateText.EditText.Text);
            } catch(FormatException) {
                Logger.Error("Error parsing start date!");
            }

            try {
                tripPlan.EndDate = Convert.ToDateTime(_tripPlanEndDateText.EditText.Text);
            } catch(FormatException) {
                Logger.Error("Error parsing end date!");
            }

            tripPlan.Note = _tripPlanNoteEditText.EditText.Text;

            base.DoDataExchange(tripPlan, dbContext);
        }
    }

    public sealed class TripPlanListViewHolder : BaseModelRecyclerViewHolder<TripPlan>
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

        public TripPlanListViewHolder(View view, BaseRecyclerListAdapter<TripPlan> adapter)
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

        protected override ViewItemFragment<TripPlan> CreateViewItemFragment()
        {
            return new ViewTripPlanFragment();
        }

        public override void UpdateView(TripPlan tripPlan)
        {
            base.UpdateView(tripPlan);

            _textViewDays.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_trip_plan_days),
                tripPlan.Days
            );

            _textViewMeals.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_trip_plan_meals),
                tripPlan.Meals.Count, tripPlan.GetTotalCalories()
            );

            _textViewCollections.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_trip_plan_collections),
                tripPlan.GearCollections.Count
            );

            _textViewSystems.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_trip_plan_systems),
                tripPlan.GearSystems.Count
            );

            _textViewItems.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_trip_plan_items),
                tripPlan.GearItems.Count, tripPlan.GetTotalGearItemCount()
            );

            int weightInUnits = (int)tripPlan.GetBaseWeightInUnits();
            _textViewBaseWeight.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_trip_plan_base_weight),
                weightInUnits, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
            );

            weightInUnits = (int)tripPlan.GetBaseWeightInUnits();
            _textViewPackWeight.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_trip_plan_pack_weight),
                weightInUnits, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
            );

            weightInUnits = (int)tripPlan.GetSkinOutWeightInUnits();
            _textViewSkinOutWeight.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_trip_plan_skinout_weight),
                weightInUnits, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1)
            );

            string formattedCost = tripPlan.GetTotalCostInCurrency(Activity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
            string formattedCostPerWeight = tripPlan.GetCostPerWeightInCurrency(Activity.BackpackPlannerState.Settings).ToString("C", CultureInfo.CurrentCulture);
            _textViewCost.Text = Java.Lang.String.Format(Activity.Resources.GetString(Resource.String.label_view_trip_plan_cost),
                formattedCost, formattedCostPerWeight, Activity.BackpackPlannerState.Settings.Units.GetSmallWeightString(false)
            );
        }
    }
}