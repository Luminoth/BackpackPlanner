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

using System.Collections.Generic;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Itineraries
{
    public class TripItineraryListAdapter : BaseListAdapter<TripItinerary>
    {
        private class TripItineraryViewHolder : BaseViewHolder
        {
            private readonly TextView _textViewName;

            public TripItineraryViewHolder(View itemView, ListItemsFragment<TripItinerary> fragment) : base(itemView, fragment)
            {
                _textViewName = itemView.FindViewById<TextView>(Resource.Id.view_trip_itinerary_name);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewTripItineraryFragment();
            }

            protected override void UpdateView()
            {
                _textViewName.Text = ListItem.Name;
            }
        }

        public override int LayoutResource => Resource.Layout.view_trip_itinerary;

        public TripItineraryListAdapter(ListItemsFragment<TripItinerary> fragment, IEnumerable<TripItinerary> listItems) : base(fragment, listItems)
        {
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new TripItineraryViewHolder(itemView, Fragment);
        }
    }
}
