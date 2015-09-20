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

        protected override bool HasSearchView => true;

        protected override int AddItemResource => Resource.Id.fab_add_meal;

        protected override Android.Support.V4.App.Fragment CreateAddItemFragment()
        {
            return new AddMealFragment();
        }

        protected override BaseListAdapter<Meal> CreateAdapter()
        {
            return new MealListAdapter(this, ListItems);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // TODO
#region Test Items
            ListItems.Add(new Meal
                {
                    Name = "One",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 1,
                    CostInUSDP = 20
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Two",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 2,
                    CostInUSDP = 19
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Three",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 3,
                    CostInUSDP = 18
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Four",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 4,
                    CostInUSDP = 17
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Five",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 5,
                    CostInUSDP = 16
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Six",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 6,
                    CostInUSDP = 15
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Seven",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 7,
                    CostInUSDP = 14
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Eight",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 8,
                    CostInUSDP = 13
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Nine",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 9,
                    CostInUSDP = 12
                }
            );

            ListItems.Add(new Meal
                {
                    Name = "Ten",
                    MealTime = MealTime.Dinner,
                    ServingCount = 1,
                    Calories = 50,
                    ProteinInGrams = 1,
                    FiberInGrams = 1,
                    WeightInGrams = 10,
                    CostInUSDP = 11
                }
            );
#endregion
        }
    }
}
