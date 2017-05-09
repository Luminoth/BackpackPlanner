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

using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Meals;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals
{
    public sealed class MealListAdapter : BaseModelRecyclerListAdapter<Meal>
    {
        protected override int LayoutResource => Resource.Layout.view_meal;

        public MealListAdapter(ListItemsFragment<Meal> fragment)
            : base(fragment)
        {
        }

        protected override IEnumerable<Meal> SortItemsByPosition(int position, IEnumerable<Meal> items)
        {
            switch(position)
            {
            case 0:         // Name
                return from x in items orderby x?.Name select x;
            case 1:         // Meal
                return from x in items orderby x?.MealTime select x;
            case 2:         // Weight
                return from x in items orderby x?.WeightInGrams select x;
            case 3:         // Cost
                return from x in items orderby x?.CostInUSDP select x;
            case 4:         // Cost / Weight
                // TODO
                return items;
            case 5:         // Calories
                // TODO
                return items;
            }
            return items;
        }

        protected override BaseRecyclerViewHolder<Meal> CreateViewHolder(View view, BaseRecyclerListAdapter<Meal> adapter)
        {
            return new MealListViewHolder(view, adapter);
        }
    }
}
