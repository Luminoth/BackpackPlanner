using System.Collections.Generic;

using Android.Views;

using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Itineraries
{
    public class TripItineraryListAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        private class TripItineraryViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
        {
            public TripItineraryViewHolder(View itemView) : base(itemView)
            {
            }
        }

        public override int ItemCount => TripItineraries?.Count ?? 0;

        public IReadOnlyCollection<TripItinerary> TripItineraries { get; set; } 

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_trip_itinerary, parent, false);
            return new TripItineraryViewHolder(itemView);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            TripItineraryViewHolder tripItineraryViewHolder = (TripItineraryViewHolder)holder;

            // setup the view holder
        }
    }
}
