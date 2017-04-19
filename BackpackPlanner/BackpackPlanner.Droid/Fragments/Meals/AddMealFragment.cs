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

using System.Threading.Tasks;

using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Meals;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals
{
    public sealed class AddMealFragment : AddItemFragment<Meal>
    {
        protected override int LayoutResource => Resource.Layout.fragment_add_meal;

        protected override int CleanTitleResource => Resource.String.title_add_meal;

        protected override int DirtyTitleResource => Resource.String.title_add_meal_dirty;

        protected override Meal CreateItem()
        {
            return new Meal();
        }

        protected override BaseModelViewHolder<Meal> CreateViewHolder(BaseActivity activity, View view)
        {
            return new MealViewHolder(activity, view);
        }

        protected override async Task AddItemAsync(DatabaseContext dbContext)
        {
            await dbContext.Meals.AddAsync(Item).ConfigureAwait(false);
        }
    }
}
