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
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public sealed class TripPlansFragment : ListItemsFragment<TripPlan>
    {
        protected override int LayoutResource => Resource.Layout.fragment_trip_plans;

        protected override int TitleResource => Resource.String.title_trip_plans;

        protected override int ListLayoutResource => Resource.Id.trip_plans_layout;

        protected override int WhatIsAnItemButtonResource => Resource.Id.button_what_trip_plan;

        protected override int WhatIsAnItemTitleResource => Resource.String.title_what_trip_plan;

        protected override int WhatIsAnItemTextResource => Resource.String.what_is_a_trip_plan;

        protected override int DeleteItemConfirmationTextResource => Resource.String.confirm_delete_trip_plan;

        protected override int DeleteItemConfirmationTitleResource => Resource.String.title_delete_confirmation_trip_plan;

        protected override int NoItemsResource => Resource.Id.no_trip_plans;

        protected override int SortItemsResource => Resource.Id.trip_plans_sort;

        protected override int AddItemResource => Resource.Id.fab_add_trip_plan;

        protected override async Task<IReadOnlyCollection<TripPlan>> GetItemsAsync(DatabaseContext dbContext)
        {
            return await TripPlan.GetAll(dbContext).ConfigureAwait(false);
        }

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddTripPlanFragment();
        }

        protected override BaseModelRecyclerListAdapter<TripPlan> CreateAdapter()
        {
            return new TripPlanListAdapter(this);
        }
    }
}
