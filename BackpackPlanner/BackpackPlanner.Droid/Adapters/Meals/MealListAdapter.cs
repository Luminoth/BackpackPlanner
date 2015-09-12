using System.Collections.Generic;

using Android.Views;

using EnergonSoftware.BackpackPlanner.Models.Meals;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals
{
    public class MealListAdapter : Android.Support.V7.Widget.RecyclerView.Adapter
    {
        private class MealViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
        {
            public MealViewHolder(View itemView) : base(itemView)
            {
            }
        }

        public override int ItemCount => Meals?.Count ?? 0;

        public IReadOnlyCollection<Meal> Meals { get; set; } 

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_meal, parent, false);
            return new MealViewHolder(itemView);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            MealViewHolder mealViewHolder = (MealViewHolder)holder;

            // setup the view holder
        }
    }
}
