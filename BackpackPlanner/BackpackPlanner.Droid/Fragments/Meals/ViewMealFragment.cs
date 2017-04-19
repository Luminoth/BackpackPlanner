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

using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Droid.Views.Meals;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals
{
    public sealed class ViewMealFragment : ViewItemFragment<Meal>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_meal;

        protected override int CleanTitleResource => Resource.String.title_view_meal;

        protected override int DirtyTitleResource => Resource.String.title_view_meal_dirty;

        public ViewMealFragment(Meal meal)
            : base(meal)
        {
        }

        protected override BaseModelViewHolder<Meal> CreateViewHolder(BaseActivity activity, View view)
        {
            return new MealViewHolder(activity, view);
        }
    }
}
