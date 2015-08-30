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

using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Meals
{
    /// <summary>
    /// 
    /// </summary>
    public class Meal
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<Meal>().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the meal identifier.
        /// </summary>
        /// <value>
        /// The meal identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public int MealId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the meal name.
        /// </summary>
        /// <value>
        /// The meal name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the meal url.
        /// </summary>
        /// <value>
        /// The meal url.
        /// </value>
        [MaxLength(2048)]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the meal time of this meal.
        /// </summary>
        /// <value>
        /// The meal time of this meal.
        /// </value>
        [NotNull]
        public MealTime MealTime  { get; set; } = MealTime.Other;

        private int _servingCount = 1;

        /// <summary>
        /// Gets or sets the serving count of this meal.
        /// </summary>
        /// <value>
        /// The serving count of this meal.
        /// </value>
        [NotNull]
        public int ServingCount
        {
            get { return _servingCount; }
            set { _servingCount = value < 1 ? 1 : value; }
        }

        private int _calories = 0;

        /// <summary>
        /// Gets or sets the calories in this meal.
        /// </summary>
        /// <value>
        /// The calories in this meal.
        /// </value>
        [NotNull]
        public int Calories
        {
            get { return _calories; }
            set { _calories = value < 0 ? 0 : value; }
        }

        private int _proteinInGrams = 0;

        /// <summary>
        /// Gets or sets the protein in this meal in grams.
        /// </summary>
        /// <value>
        /// The protein in this meal in grams.
        /// </value>
        [NotNull]
        public int ProteinInGrams
        {
            get { return _proteinInGrams; }
            set { _proteinInGrams = value < 0 ? 0 : value; }
        }

        private int _fiberInGrams = 0;

        /// <summary>
        /// Gets or sets the fiber in this meal in grams.
        /// </summary>
        /// <value>
        /// The fiber in this meal in grams.
        /// </value>
        [NotNull]
        public int FiberInGrams
        {
            get { return _fiberInGrams; }
            set { _fiberInGrams = value < 0 ? 0 : value; }
        }

        private int _weightInGrams;

        /// <summary>
        /// Gets or sets the weight of this meal in grams.
        /// </summary>
        /// <value>
        /// The weight of this meal in grams.
        /// </value>
        [NotNull]
        public int WeightInGrams
        {
            get { return _weightInGrams; }
            set { _weightInGrams = value < 0 ? 0 : value; }
        }

        // ReSharper disable once InconsistentNaming
        private int _costInUSDP;

        /// <summary>
        /// Gets or sets the cost of this meal in US pennies.
        /// </summary>
        /// <value>
        /// The cost of this meal in US pennies.
        /// </value>
        // ReSharper disable once InconsistentNaming
        [NotNull]
        public int CostInUSDP
        {
            get { return _costInUSDP; }
            set { _costInUSDP = value < 0 ? 0 : value; }
        }

        /// <summary>
        /// Gets or sets the meal note.
        /// </summary>
        /// <value>
        /// The meal note.
        /// </value>
        [MaxLength(1024)]
        public string Note { get; set; } = string.Empty;

        [ManyToMany(typeof(TripPlanMeal))]
        public List<TripPlan> TripPlans { get; set; }

        public override bool Equals(object obj)
        {
            if(MealId < 1) {
                return false;
            }

            Meal meal = obj as Meal;
            return MealId == meal?.MealId;
        }

        public override int GetHashCode()
        {
            return MealId.GetHashCode();
        }
    }
}
