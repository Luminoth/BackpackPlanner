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

using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public class ViewTripPlanFragment : ViewItemFragment<TripPlan>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_trip_plan;

        protected override int TitleResource => Resource.String.title_view_trip_plan;

        protected override int SaveItemResource => Resource.Id.button_save_trip_plan;

        protected override bool HasSearchView => false;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _tripPlanNameEditText;
        private Android.Support.Design.Widget.TextInputLayout _tripPlanNoteEditText;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _tripPlanNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_trip_plan_name);
            _tripPlanNameEditText.EditText.Text = Item.Name;

            _tripPlanNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_trip_plan_note);
            _tripPlanNoteEditText.EditText.Text = Item.Note;
        }

        protected override void OnDoDataExchange()
        {
            Item.Name = _tripPlanNameEditText.EditText.Text;
            Item.Note = _tripPlanNoteEditText.EditText.Text;
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
