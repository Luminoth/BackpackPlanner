using System.Collections.Generic;

using Android.Views;

using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Systems
{
    public class GearSystemListAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        private class GearSystemViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
        {
            public GearSystemViewHolder(View itemView) : base(itemView)
            {
            }
        }

        public override int ItemCount => GearSystems?.Count ?? 0;

        public IReadOnlyCollection<GearSystem> GearSystems { get; set; } 

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_gear_system, parent, false);
            return new GearSystemViewHolder(itemView);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            GearSystemViewHolder gearSystemViewHolder = (GearSystemViewHolder)holder;

            // setup the view holder
        }
    }
}
