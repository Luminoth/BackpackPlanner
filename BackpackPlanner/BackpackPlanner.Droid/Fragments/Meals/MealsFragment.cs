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

using EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals;
using EnergonSoftware.BackpackPlanner.Models.Meals;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals
{
    public class MealsFragment : ListItemsFragment<Meal>
    {
        protected override int LayoutResource => Resource.Layout.fragment_meals;

        protected override int TitleResource => Resource.String.title_meals;

       protected override int ListLayoutResource => Resource.Id.meals_layout;

        protected override int NoItemsResource => Resource.Id.no_meals;

        protected override int SortItemsResource => Resource.Id.meals_sort;

        private List<Meal> _meals = new List<Meal>(); 

        protected override int ItemCount => _meals.Count;

        protected override int AddItemResource => Resource.Id.fab_add_meal;

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddMealFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // TODO
            _meals = new List<Meal>();
            for(int i=0; i<20; ++i) {
                _meals.Add(new Meal());
            }
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Layout.SetAdapter(
                new MealListAdapter
                {
                    Meals = _meals
                }
            );
        }
    }
}
