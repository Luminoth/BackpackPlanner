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

using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Trips;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Itineraries
{
    public sealed class TripItineraryListAdapter : BaseModelRecyclerListAdapter<TripItinerary>
    {
        protected override int LayoutResource => Resource.Layout.view_trip_itinerary;

        public TripItineraryListAdapter(ListItemsFragment<TripItinerary> fragment)
            : base(fragment)
        {
        }

        protected override IEnumerable<TripItinerary> SortItemsByPosition(int position, IEnumerable<TripItinerary> items)
        {
            switch(position)
            {
            case 0:         // Name
                return from x in items orderby x?.Name select x;
            }
            return items;
        }

        protected override BaseRecyclerViewHolder<TripItinerary> CreateViewHolder(View view, BaseRecyclerListAdapter<TripItinerary> adapter)
        {
            return new TripItineraryListViewHolder(view, adapter);
        }
    }
}
