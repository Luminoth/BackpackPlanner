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
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals
{
    public sealed class MealsFragment : ListItemsFragment<Meal>
    {
        protected override int LayoutResource => Resource.Layout.fragment_meals;

        protected override int TitleResource => Resource.String.title_meals;

       protected override int ListLayoutResource => Resource.Id.meals_layout;

        protected override int WhatIsAnItemButtonResource => Resource.Id.button_what_meal;

        protected override int WhatIsAnItemTitleResource => Resource.String.title_what_meal;

        protected override int WhatIsAnItemTextResource => Resource.String.what_is_a_meal;

        protected override int DeleteItemConfirmationTextResource => Resource.String.confirm_delete_meal;

        protected override int DeleteItemConfirmationTitleResource => Resource.String.title_delete_confirmation_meal;

        protected override int NoItemsResource => Resource.Id.no_meals;

        protected override int SortItemsResource => Resource.Id.meals_sort;

        protected override int AddItemResource => Resource.Id.fab_add_meal;

        protected override async Task<List<Meal>> GetItemsAsync(DatabaseContext dbContext)
        {
            return await dbContext.Meals.ToListAsync().ConfigureAwait(false);
        }

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddMealFragment();
        }

        protected override BaseListAdapter<Meal> CreateAdapter()
        {
            return new MealListAdapter(this);
        }
    }
}
