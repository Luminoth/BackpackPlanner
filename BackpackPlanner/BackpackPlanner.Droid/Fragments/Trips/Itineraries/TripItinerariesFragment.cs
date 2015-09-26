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

using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries
{
    public class TripItinerariesFragment : ListItemsFragment<TripItinerary>
    {
        protected override int LayoutResource => Resource.Layout.fragment_trip_itineraries;

        protected override int TitleResource => Resource.String.title_trip_itineraries;

        protected override int ListLayoutResource => Resource.Id.trip_itineraries_layout;

        protected override int NoItemsResource => Resource.Id.no_trip_itineraries;

        protected override int SortItemsResource => Resource.Id.trip_itineraries_sort;

        protected override bool HasSearchView => true;

        protected override int AddItemResource => Resource.Id.fab_add_trip_itinerary;

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddTripItineraryFragment();
        }

        protected override BaseListAdapter<TripItinerary> CreateAdapter()
        {
            return new TripItineraryListAdapter(this, ListItems);
        }
    }
}
