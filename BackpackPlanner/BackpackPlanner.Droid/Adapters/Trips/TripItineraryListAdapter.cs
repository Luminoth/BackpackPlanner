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
using System.Linq;

using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips
{
    public sealed class TripItineraryListAdapter : BaseModelRecyclerListAdapter<TripItinerary>
    {
        private sealed class TripItineraryViewHolder : BaseModelViewHolder
        {
            protected override int DeleteActionResourceId => Resource.Id.action_delete_trip_itinerary;

            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            public TripItineraryViewHolder(View itemView, BaseModelRecyclerListAdapter<TripItinerary> adapter)
                : base(itemView, adapter)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_trip_itinerary_toolbar);
                _toolbar.InflateMenu(Resource.Menu.trip_itinerary_menu);
                _toolbar.SetOnMenuItemClickListener(this);
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewTripItineraryFragment
                {
                    Item = ListItem
                };
            }

            protected override void UpdateView()
            {
                _toolbar.Title = ListItem.Name;
            }
        }

        public override int LayoutResource => Resource.Layout.view_trip_itinerary;

        public TripItineraryListAdapter(ListItemsFragment<TripItinerary> fragment)
            : base(fragment)
        {
        }

        protected override void SortItemsByPosition(int position)
        {
            switch(position)
            {
            case 0:         // Name
                FilteredListItems = FilteredListItems.OrderBy(x => x.Name, StringComparer.CurrentCulture);
                break;
            }
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new TripItineraryViewHolder(itemView, this);
        }
    }
}
