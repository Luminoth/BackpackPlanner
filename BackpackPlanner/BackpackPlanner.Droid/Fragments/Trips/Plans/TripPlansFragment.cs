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

using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public class TripPlansFragment : ListItemsFragment<TripPlan>
    {
        protected override int LayoutResource => Resource.Layout.fragment_trip_plans;

        protected override int TitleResource => Resource.String.title_trip_plans;

        protected override int ListLayoutResource => Resource.Id.trip_plans_layout;

        protected override int NoItemsResource => Resource.Id.no_trip_plans;

        protected override int SortItemsResource => Resource.Id.trip_plans_sort;

        protected override bool HasSearchView => true;

        protected override int AddItemResource => Resource.Id.fab_add_trip_plan;

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddTripPlanFragment();
        }

        protected override BaseListAdapter<TripPlan> CreateAdapter()
        {
            return new TripPlanListAdapter(this, ListItems);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // TODO
#region Test Items
            ListItems.Add(new TripPlan
                {
                    Name = "One"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Two"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Three"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Four"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Five"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Six"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Seven"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Eight"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Nine"
                }
            );

            ListItems.Add(new TripPlan
                {
                    Name = "Ten"
                }
            );
#endregion
        }
    }
}
