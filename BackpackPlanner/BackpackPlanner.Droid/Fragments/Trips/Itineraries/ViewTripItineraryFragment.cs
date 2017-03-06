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

using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries
{
    public sealed class ViewTripItineraryFragment : ViewItemFragment<TripItinerary>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_trip_itinerary;

        protected override int TitleResource => Resource.String.title_view_trip_itinerary;

        protected override int SaveItemResource => Resource.Id.button_save_trip_itinerary;

        protected override bool HasSearchView => false;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _tripItineraryNameEditText;
        private Android.Support.Design.Widget.TextInputLayout _tripItineraryNoteEditText;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _tripItineraryNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_trip_itinerary_name);
            _tripItineraryNameEditText.EditText.Text = Item.Name;

            _tripItineraryNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_trip_itinerary_note);
            _tripItineraryNoteEditText.EditText.Text = Item.Note;
        }

        protected override void OnDoDataExchange()
        {
            Item.Name = _tripItineraryNameEditText.EditText.Text;
            Item.Note = _tripItineraryNoteEditText.EditText.Text;
        }

        protected override bool OnValidate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_tripItineraryNameEditText.EditText.Text)) {
                _tripItineraryNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }
    }
}
