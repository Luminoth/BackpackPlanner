/*
   Copyright 2017 Shane Lillie

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

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.DAL.Meals
{
    public abstract class MealEntries<T> : ItemEntries<T, Meal, MealEntry<T> >
        where T: BaseModel<T>, new()
    {
        public abstract class MealEntryViewHolder : BaseModelEntryViewHolder<T, Meal, MealEntry<T>>
        {
            protected override int NoItemsResource => Resource.Id.no_meals;

            protected override int NoItemsAddedResource => Resource.Id.no_meals_added;

            protected override int ItemListAdapterResource => Resource.Id.meals_list;

            protected override int AddItemButtonResource => Resource.Id.fab_add_meal;

            protected MealEntryViewHolder(BaseActivity activity, View view)
                : base(activity, view)
            {
            }
        }

        protected MealEntries(T model, IReadOnlyCollection<MealEntry<T>> entries)
            : base(model, entries)
        {
        }
    }

    public sealed class TripPlanMealEntries : MealEntries<TripPlan>
    {
        public sealed class TripPlanMealEntryViewHolder : MealEntryViewHolder
        {
            public TripPlanMealEntryViewHolder(BaseActivity activity, View view)
                : base(activity, view)
            {
            }

            public override void DoDataExchange(TripPlan item, ItemEntries<TripPlan, Meal, MealEntry<TripPlan>> itemEntries, DatabaseContext dbContext)
            {
                item.SetMeals(dbContext, itemEntries.ItemListAdapter?.Items);
            }
        }

        public override MealEntry<TripPlan> GetItemEntry(Meal meal)
        {
            return Model.Meals.FirstOrDefault(x => x.Model.Id == meal.Id);
        }

        public TripPlanMealEntries(TripPlan tripPlan)
            : base(tripPlan, tripPlan.Meals)
        {
        }
    }
}
