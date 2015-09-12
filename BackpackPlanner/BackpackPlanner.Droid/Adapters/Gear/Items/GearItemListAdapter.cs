using System.Collections.Generic;

using Android.Views;

using EnergonSoftware.BackpackPlanner.Models.Gear.Items;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items
{
    public class GearItemListAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        private class GearItemViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
        {
            public GearItemViewHolder(View itemView) : base(itemView)
            {
            }
        }

        public override int ItemCount => GearItems?.Count ?? 0;

        public IReadOnlyCollection<GearItem> GearItems { get; set; } 

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_gear_item, parent, false);
            return new GearItemViewHolder(itemView);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            GearItemViewHolder gearItemViewHolder = (GearItemViewHolder)holder;

            // setup the view holder
        }
    }
}
