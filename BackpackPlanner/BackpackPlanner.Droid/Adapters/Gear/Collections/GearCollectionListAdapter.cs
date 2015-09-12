using System.Collections.Generic;

using Android.Views;

using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections
{
    public class GearCollectionListAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        private class GearCollectionViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
        {
            public GearCollectionViewHolder(View itemView) : base(itemView)
            {
            }
        }

        public override int ItemCount => GearCollections?.Count ?? 0;

        public IReadOnlyCollection<GearCollection> GearCollections { get; set; } 

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_gear_collection, parent, false);
            return new GearCollectionViewHolder(itemView);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            GearCollectionViewHolder gearCollectionViewHolder = (GearCollectionViewHolder)holder;

            // setup the view holder
        }
    }
}
