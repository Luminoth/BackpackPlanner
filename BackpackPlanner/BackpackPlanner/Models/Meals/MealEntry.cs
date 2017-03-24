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

using EnergonSoftware.BackpackPlanner.Settings;

using JetBrains.Annotations;

using SQLite.Net.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Meals
{
    public abstract class MealEntry<T> : DatabaseIntermediateItem<T, Meal>  where T: DatabaseItem
    {
#region Static Helpers
        public static int GetMealCount<TE>(List<TE> meals, [CanBeNull] List<int> visitedMeals) where TE: MealEntry<T>
        {
            int count = 0;
            foreach(TE meal in meals) {
                if(visitedMeals?.Contains(meal.MealId) ?? false) {
                    continue;
                }

                visitedMeals?.Add(meal.MealId);
                count += meal.Count;
            }
            return count;
        }

        public static int GetTotalCalories<TE>(List<TE> meals, [CanBeNull] List<int> visitedMeals) where TE: MealEntry<T>
        {
            int calories = 0;
            foreach(TE meal in meals) {
                if(visitedMeals?.Contains(meal.MealId) ?? false) {
                    continue;
                }

                visitedMeals?.Add(meal.MealId);
                calories += meal.Calories;
            }
            return calories;
        }

        public static int GetTotalWeightInGrams<TE>(List<TE> meals, [CanBeNull] List<int> visitedMeals) where TE: MealEntry<T>
        {
            int weightInGrams = 0;
            foreach(TE meal in meals) {
                if(visitedMeals?.Contains(meal.MealId) ?? false) {
                    continue;
                }

                visitedMeals?.Add(meal.MealId);
                weightInGrams += meal.TotalWeightInGrams;
            }
            return weightInGrams;
        }

        public static int GetTotalCostInUSDP<TE>(List<TE> meals, [CanBeNull] List<int> visitedMeals) where TE: MealEntry<T>
        {
            int costInUSDP = 0;
            foreach(TE meal in meals) {
                if(visitedMeals?.Contains(meal.MealId) ?? false) {
                    continue;
                }

                visitedMeals?.Add(meal.MealId);
                costInUSDP += meal.TotalCostInUSDP;
            }
            return costInUSDP;
        }
#endregion

        public abstract int MealId { get; set; }

        [Ignore]
        public int Calories => Count * (Child?.Calories ?? 0);

        [Ignore]
        public int TotalWeightInGrams => Count * (Child?.WeightInGrams ?? 0);

        [Ignore]
        public int TotalCostInUSDP => Count * (Child?.CostInUSDP ?? 0);


        protected MealEntry()
        {
        }

        protected MealEntry(T parent, Meal meal, BackpackPlannerSettings settings)
            : base(parent, meal, settings)
        {
        }
    }
}
