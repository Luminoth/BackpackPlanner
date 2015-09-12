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

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Itineraries
{
    public class TripItineraryListAdapter : BaseListAdapter
    {
        private class TripItineraryViewHolder : BaseViewHolder
        {
            public TripItineraryViewHolder(View itemView, BaseFragment fragment) : base(itemView, fragment)
            {
            }
        }

        public override int LayoutResource => Resource.Layout.view_trip_itinerary;

        public override int ItemCount => TripItineraries?.Count ?? 0;

        public IReadOnlyCollection<TripItinerary> TripItineraries { get; set; } 

        public TripItineraryListAdapter(BaseFragment fragment) : base(fragment)
        {
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new TripItineraryViewHolder(itemView, Fragment);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            TripItineraryViewHolder tripItineraryViewHolder = (TripItineraryViewHolder)holder;

            // setup the view holder
        }
    }
}
