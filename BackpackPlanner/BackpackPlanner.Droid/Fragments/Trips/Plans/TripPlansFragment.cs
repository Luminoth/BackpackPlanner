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

using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public class TripPlansFragment : RecyclerFragment
    {
        public override int LayoutResource => Resource.Layout.fragment_trip_plans;

        public override int TitleResource => Resource.String.title_trip_plans;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            // TODO
            var tripPlans = new List<TripPlan>();
            for(int i=0; i<20; ++i) {
                tripPlans.Add(new TripPlan());
            }

            TextView noTripPlansTextView = view.FindViewById<TextView>(Resource.Id.no_trip_plans);
            Spinner tripPlansSort = view.FindViewById<Spinner>(Resource.Id.trip_plans_sort);

            if(tripPlans.Count > 0) {
                noTripPlansTextView.Visibility = ViewStates.Gone;
                tripPlansSort.Visibility = ViewStates.Visible;

                InitLayout(view, Resource.Id.trip_plans_layout,
                    new TripPlanListAdapter
                    {
                        TripPlans = tripPlans
                    }
                );

                Layout.Visibility = ViewStates.Visible;
            }

            Android.Support.Design.Widget.FloatingActionButton addTripPlanButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_add_trip_plan);
            addTripPlanButton.Click += (sender, args) => {
                // TODO
            };
        }
    }
}
