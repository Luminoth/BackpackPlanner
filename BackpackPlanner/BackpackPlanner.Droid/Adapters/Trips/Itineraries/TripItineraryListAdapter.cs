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
using System.Collections.Generic;
using System.Linq;

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Itineraries
{
    public class TripItineraryListAdapter : BaseListAdapter<TripItinerary>
    {
        private class TripItineraryViewHolder : BaseViewHolder, Android.Support.V7.Widget.Toolbar.IOnMenuItemClickListener
        {
            private readonly Android.Support.V7.Widget.Toolbar _toolbar;

            public TripItineraryViewHolder(View itemView, ListItemsFragment<TripItinerary> fragment) : base(itemView, fragment)
            {
                _toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.view_trip_itinerary_toolbar);
                _toolbar.InflateMenu(Resource.Menu.trip_itinerary_menu);
                _toolbar.SetOnMenuItemClickListener(this);
            }

            public bool OnMenuItemClick(IMenuItem menuItem)
            {
                // TODO
                return true;
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

        public TripItineraryListAdapter(ListItemsFragment<TripItinerary> fragment, IEnumerable<TripItinerary> listItems) : base(fragment, listItems)
        {
        }

        public override void SortByItemSelectedEventHander(object sender, AdapterView.ItemSelectedEventArgs args)
        {
            // TODO: can this be made clearer somehow by using args.Id?
            switch(args.Position)
            {
            case 0:         // Name
                FilteredListItems = FilteredListItems.OrderBy(x => x.Name, StringComparer.CurrentCulture);
                break;
            }
        }

        public override void FilterItems(object sender, Android.Support.V7.Widget.SearchView.QueryTextChangeEventArgs args)
        {
            FilteredListItems = from item in ListItems where item.Name.ToLower().Contains(args.NewText) select item;
            NotifyDataSetChanged();
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new TripItineraryViewHolder(itemView, Fragment);
        }
    }
}
