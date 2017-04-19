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

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Util;

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
                picker.Show(BaseActivity.SupportFragmentManager, null);
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
                picker.Show(BaseActivity.SupportFragmentManager, null);
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
}