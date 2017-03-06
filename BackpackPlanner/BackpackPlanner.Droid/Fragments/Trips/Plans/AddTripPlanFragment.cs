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

using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Util;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public sealed class AddTripPlanFragment : AddItemFragment<TripPlan>
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(AddTripPlanFragment));

        protected override int LayoutResource => Resource.Layout.fragment_add_trip_plan;

        protected override int TitleResource => Resource.String.title_add_trip_plan;

        protected override int AddItemResource => Resource.Id.button_add_trip_plan;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _tripPlanNameEditText;
        private Android.Support.Design.Widget.TextInputLayout _tripPlanStartDateText;
        private Android.Support.Design.Widget.TextInputLayout _tripPlanEndDateText;
        private Android.Support.Design.Widget.TextInputLayout _tripPlanNoteEditText;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _tripPlanNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_trip_plan_name);

            _tripPlanStartDateText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_trip_plan_startdate);
            _tripPlanStartDateText.EditText.Text = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
            _tripPlanStartDateText.EditText.Click += (sender, args) => {
                DateTime dateTime = DateTime.Now;
                try {
                    dateTime = Convert.ToDateTime(_tripPlanStartDateText.EditText.Text);
                } catch(FormatException) {
                }

                DatePickerFragment picker = new DatePickerFragment(dateTime);
                picker.DateSetEvent += (s, a) => {
                    _tripPlanStartDateText.EditText.Text = a.Date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                };
                picker.Show(Activity.SupportFragmentManager, null);
            };

            _tripPlanEndDateText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_trip_plan_enddate);
            _tripPlanEndDateText.EditText.Text = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
            _tripPlanEndDateText.EditText.Click += (sender, args) => {
                DateTime dateTime = DateTime.Now;
                try {
                    dateTime = Convert.ToDateTime(_tripPlanEndDateText.EditText.Text);
                } catch(FormatException) {
                }

                DatePickerFragment picker = new DatePickerFragment(dateTime);
                picker.DateSetEvent += (s, a) => {
                    _tripPlanEndDateText.EditText.Text = a.Date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                };
                picker.Show(Activity.SupportFragmentManager, null);
            };

            _tripPlanNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_trip_plan_note);
        }

        protected override void OnDoDataExchange()
        {
            Item = new TripPlan(DroidState.Instance.BackpackPlannerState.Settings)
            {
                Name = _tripPlanNameEditText.EditText.Text,
                Note = _tripPlanNoteEditText.EditText.Text
            };

            try {
                Item.StartDate = Convert.ToDateTime(_tripPlanStartDateText.EditText.Text);
            } catch(FormatException) {
                Logger.Error("Error parsing start date!");
            }

            try {
                Item.EndDate = Convert.ToDateTime(_tripPlanEndDateText.EditText.Text);
            } catch(FormatException) {
                Logger.Error("Error parsing end date!");
            }
        }

        protected override bool OnValidate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_tripPlanNameEditText.EditText.Text)) {
                _tripPlanNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }
    }
}
