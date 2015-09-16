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
using System.Linq;

using Android.Views;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Itineraries
{
    public class TripItineraryListAdapter : BaseListAdapter
    {
        private class TripItineraryViewHolder : BaseViewHolder
        {
            private TripItinerary _tripItinerary;

            public TripItinerary TripItinerary
            {
                get { return _tripItinerary; }
                set { _tripItinerary = value; UpdateView(); }
            }

            public TripItineraryViewHolder(View itemView, BaseFragment fragment) : base(itemView, fragment)
            {
                // TODO: get handles to controls here
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewTripItineraryFragment();
            }

            private void UpdateView()
            {
                // TODO: update the controls here
            }
        }

        public override int LayoutResource => Resource.Layout.view_trip_itinerary;

        public override int ItemCount => _tripItineraries?.Count ?? 0;

        private readonly ICollection<TripItinerary> _tripItineraries;

        public TripItineraryListAdapter(BaseFragment fragment, IEnumerable<TripItinerary> tripItineraries) : base(fragment)
        {
            // TODO: ok, so next step is handling different sorting methods
            // and updating when the sort method is changed

            _tripItineraries = tripItineraries.OrderBy(x => x.Name).ToList();
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new TripItineraryViewHolder(itemView, Fragment);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            TripItineraryViewHolder tripItineraryViewHolder = (TripItineraryViewHolder)holder;
            TripItinerary tripItinerary = _tripItineraries.ElementAt(position);
            tripItineraryViewHolder.TripItinerary = tripItinerary;
        }
    }
}
