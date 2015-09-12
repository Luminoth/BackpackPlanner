using System.Collections.Generic;

using Android.Views;

using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Plans
{
    public class TripPlanListAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        private class TripPlanViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
        {
            public TripPlanViewHolder(View itemView) : base(itemView)
            {
            }
        }

        public override int ItemCount => TripPlans?.Count ?? 0;

        public IReadOnlyCollection<TripPlan> TripPlans { get; set; } 

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_trip_plan, parent, false);
            return new TripPlanViewHolder(itemView);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            TripPlanViewHolder tripPlanViewHolder = (TripPlanViewHolder)holder;

            // setup the view holder
        }
    }
}
