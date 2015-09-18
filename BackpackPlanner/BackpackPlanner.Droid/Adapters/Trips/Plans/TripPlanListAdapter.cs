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
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Plans
{
    public class TripPlanListAdapter : BaseListAdapter<TripPlan>
    {
        private class TripPlanViewHolder : BaseViewHolder
        {
            public TripPlanViewHolder(View itemView, ListItemsFragment<TripPlan> fragment) : base(itemView, fragment)
            {
                // TODO: get handles to controls here
            }

            protected override Android.Support.V4.App.Fragment CreateViewItemFragment()
            {
                return new ViewTripPlanFragment();
            }

            protected override void UpdateView()
            {
                // TODO: update the controls here
            }
        }

        public override int LayoutResource => Resource.Layout.view_trip_plan;

        public TripPlanListAdapter(ListItemsFragment<TripPlan> fragment, IEnumerable<TripPlan> listItems) : base(fragment, listItems)
        {
        }

        protected override BaseViewHolder CreateViewHolder(View itemView)
        {
            return new TripPlanViewHolder(itemView, Fragment);
        }
    }
}
