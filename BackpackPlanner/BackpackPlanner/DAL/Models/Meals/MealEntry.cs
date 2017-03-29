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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Meals
{
    public class MealEntry : BaseModelEntry<Meal>
    {
#region Static Helpers
        public static int GetMealCount<TE>(List<TE> meals, [CanBeNull] List<int> visitedMeals) where TE: MealEntry
        {
            int count = 0;
            foreach(TE meal in meals) {
                if(visitedMeals?.Contains(meal.ModelId) ?? false) {
                    continue;
                }

                visitedMeals?.Add(meal.ModelId);
                count += meal.Count;
            }
            return count;
        }

        public static int GetTotalCalories<TE>(List<TE> meals, [CanBeNull] List<int> visitedMeals) where TE: MealEntry
        {
            int calories = 0;
            foreach(TE meal in meals) {
                if(visitedMeals?.Contains(meal.ModelId) ?? false) {
                    continue;
                }

                visitedMeals?.Add(meal.ModelId);
                calories += meal.Calories;
            }
            return calories;
        }

        public static int GetTotalWeightInGrams<TE>(List<TE> meals, [CanBeNull] List<int> visitedMeals) where TE: MealEntry
        {
            int weightInGrams = 0;
            foreach(TE meal in meals) {
                if(visitedMeals?.Contains(meal.ModelId) ?? false) {
                    continue;
                }

                visitedMeals?.Add(meal.ModelId);
                weightInGrams += meal.TotalWeightInGrams;
            }
            return weightInGrams;
        }

        public static int GetTotalCostInUSDP<TE>(List<TE> meals, [CanBeNull] List<int> visitedMeals) where TE: MealEntry
        {
            int costInUSDP = 0;
            foreach(TE meal in meals) {
                if(visitedMeals?.Contains(meal.ModelId) ?? false) {
                    continue;
                }

                visitedMeals?.Add(meal.ModelId);
                costInUSDP += meal.TotalCostInUSDP;
            }
            return costInUSDP;
        }
#endregion

        public override int Id => MealEntryId;

#region Database Properties
        /// <summary>
        /// Gets or sets the meal entry identifier.
        /// </summary>
        /// <value>
        /// The meal entry identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MealEntryId { get; private set; }

        [Required, ForeignKey("Model")]
        public override int ModelId { get; protected set; }

        public override Meal Model { get; protected set; }
#endregion

        [NotMapped]
        public int Calories => Count * (Model?.Calories ?? 0);

        [NotMapped]
        public int TotalWeightInGrams => Count * (Model?.WeightInGrams ?? 0);

        [NotMapped]
        // ReSharper disable once InconsistentNaming
        public int TotalCostInUSDP => Count * (Model?.CostInUSDP ?? 0);

        public MealEntry(Meal meal)
            : base(meal)
        {
        }
    }
}
