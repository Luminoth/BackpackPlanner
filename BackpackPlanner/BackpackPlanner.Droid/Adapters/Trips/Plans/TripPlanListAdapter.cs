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

using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Trips;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Plans
{
    public sealed class TripPlanListAdapter : BaseModelRecyclerListAdapter<TripPlan>
    {
        protected override int LayoutResource => Resource.Layout.view_trip_plan;

        public TripPlanListAdapter(ListItemsFragment<TripPlan> fragment)
            : base(fragment)
        {
        }

        protected override IEnumerable<TripPlan> SortItemsByPosition(int position, IEnumerable<TripPlan> items)
        {
            switch(position)
            {
            case 0:         // Name
                return from x in items orderby x?.Name select x;
            case 1:         // Items
                // TODO
                return items;
            case 2:         // Weight
                // TODO
                return items;
            case 3:         // Cost
                // TODO
                return items;
            case 4:         // Cost / Weight
                // TODO
                return items;
            }
            return items;
        }

        protected override BaseRecyclerViewHolder<TripPlan> CreateViewHolder(View view, BaseRecyclerListAdapter<TripPlan> adapter)
        {
            return new TripPlanListViewHolder(view, adapter);
        }
    }
}
