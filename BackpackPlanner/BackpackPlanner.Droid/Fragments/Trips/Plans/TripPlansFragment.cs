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

using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public class TripPlansFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_trip_plans, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            TextView noTripPlansTextView = view.FindViewById<TextView>(Resource.Id.no_trip_plans);

            ViewGroup tripPlansLayout = view.FindViewById<LinearLayout>(Resource.Id.trip_plans_layout);
            tripPlansLayout.Visibility = ViewStates.Gone;

            FloatingActionButton addTripPlanButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_add_trip_plan);
            addTripPlanButton.Click += (sender, args) => {
                // TODO
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Title = Resources.GetString(Resource.String.title_trip_plans);
        }
    }
}
