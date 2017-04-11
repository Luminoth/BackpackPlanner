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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Units;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Meals
{
    [Serializable]
    public class MealEntry<T> : BaseModelEntry<MealEntry<T>, T, Meal> where T: BaseModel<T>, new()
    {
#region Static Helpers
        public static int GetMealCount<TE>(IReadOnlyCollection<TE> meals, [CanBeNull] ICollection<int> visitedMeals)
            where TE: MealEntry<T>
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

        public static int GetTotalCalories<TE>(IReadOnlyCollection<TE> meals, [CanBeNull] ICollection<int> visitedMeals)
            where TE: MealEntry<T>
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

        public static int GetTotalWeightInGrams<TE>(IReadOnlyCollection<TE> meals, [CanBeNull] ICollection<int> visitedMeals)
            where TE: MealEntry<T>
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

        // ReSharper disable once InconsistentNaming
        public static int GetTotalCostInUSDP<TE>(IReadOnlyCollection<TE> meals, [CanBeNull] ICollection<int> visitedMeals)
            where TE: MealEntry<T>
        {
            // ReSharper disable once InconsistentNaming
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

        public override MealEntry<T> DeepCopy()
        {
            MealEntry<T> mealEntry = base.DeepCopy();

            mealEntry.MealEntryId = MealEntryId;

            return mealEntry;
        }

        [NotMapped, JsonIgnore]
        public int Calories => Count * (Model?.Calories ?? 0);

        [NotMapped, JsonIgnore]
        public int TotalWeightInGrams => Count * (Model?.WeightInGrams ?? 0);

        public float GetTotalWeightInUnits(BackpackPlannerSettings settings)
        {
            return settings.Units.WeightFromGrams(TotalWeightInGrams);
        }

        [NotMapped, JsonIgnore]
        // ReSharper disable once InconsistentNaming
        public int TotalCostInUSDP => Count * (Model?.CostInUSDP ?? 0);

        public MealEntry(Meal meal)
            : base(meal)
        {
        }

        public MealEntry()
        {
        }
    }
}
