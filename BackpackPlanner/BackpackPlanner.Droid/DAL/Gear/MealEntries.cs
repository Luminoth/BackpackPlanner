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

using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;

namespace EnergonSoftware.BackpackPlanner.Droid.DAL.Gear
{
    public abstract class MealEntries<T> : ItemEntries<T, Meal, MealEntry<T> >
        where T: BaseModel<T>, new()
    {
        public abstract class MealEntryViewHolder : DataFragment<T>.ItemEntryViewHolder<Meal, MealEntry<T>>
        {
            protected override int NoItemsResource => Resource.Id.no_meals;

            protected override int NoItemsAddedResource => Resource.Id.no_meals_added;

            protected override int ItemListAdapterResource => Resource.Id.meals_list;

            protected override int AddItemButtonResource => Resource.Id.fab_add_meal;

            protected override int AddItemDialogTitleResource => Resource.String.label_add_meals;

            protected MealEntryViewHolder(DataFragment<T> fragment, T item, MealEntries<T> itemEntries)
                : base(fragment, item, itemEntries)
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
            public TripPlanMealEntryViewHolder(DataFragment<TripPlan> fragment, TripPlan item, TripPlanMealEntries itemEntries)
                : base(fragment, item, itemEntries)
            {
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
