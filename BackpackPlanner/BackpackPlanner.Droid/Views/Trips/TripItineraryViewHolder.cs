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

using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries;

namespace EnergonSoftware.BackpackPlanner.Droid.Views.Trips
{
    public sealed class TripItineraryViewHolder : BaseModelViewHolder<TripItinerary>
    {
        private readonly Android.Support.Design.Widget.TextInputLayout _tripItineraryNameEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _tripItineraryNoteEditText;

        public TripItineraryViewHolder(BaseActivity activity, View view)
            : base(activity)
        {
            _tripItineraryNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.trip_itinerary_name);
            _tripItineraryNameEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Name");
            };

            _tripItineraryNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.trip_itinerary_note);
            _tripItineraryNoteEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Note");
            };
        }

        public override void UpdateView(TripItinerary tripItinerary)
        {
            base.UpdateView(tripItinerary);

            _tripItineraryNameEditText.EditText.Text = tripItinerary.Name;
            _tripItineraryNoteEditText.EditText.Text = tripItinerary.Note;
        }

        public override bool Validate()
        {
            bool valid = base.Validate();

            if(string.IsNullOrWhiteSpace(_tripItineraryNameEditText.EditText.Text)) {
                _tripItineraryNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        public override void DoDataExchange(TripItinerary tripItinerary, DatabaseContext dbContext)
        {
            tripItinerary.Name = _tripItineraryNameEditText.EditText.Text;
            tripItinerary.Note = _tripItineraryNoteEditText.EditText.Text;

            base.DoDataExchange(tripItinerary, dbContext);
        }
    }

    public sealed class TripItineraryListViewHolder : BaseModelRecyclerViewHolder<TripItinerary>
    {
        protected override int ToolbarResourceId => Resource.Id.view_trip_itinerary_toolbar;

        protected override int MenuResourceId => Resource.Menu.trip_itinerary_menu;

        protected override int DeleteActionResourceId => Resource.Id.action_delete_trip_itinerary;

        public TripItineraryListViewHolder(View view, BaseRecyclerListAdapter<TripItinerary> adapter)
            : base(view, adapter)
        {
        }

        protected override ViewItemFragment<TripItinerary> CreateViewItemFragment()
        {
            return new ViewTripItineraryFragment();
        }

        public override void UpdateView(TripItinerary tripItinerary)
        {
            base.UpdateView(tripItinerary);
        }
    }
}