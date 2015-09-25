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
using EnergonSoftware.BackpackPlanner.Units;

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

namespace EnergonSoftware.BackpackPlanner.Models.Meals
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Meal : IItem
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<Meal>().ConfigureAwait(false);
        }

        public static async Task<List<Meal>> GetMealsAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.GetAllWithChildrenAsync<Meal>().ConfigureAwait(false);
        }

        public static async Task<Meal> GetMealAsync(SQLiteAsyncConnection asyncDbConnection, int mealId)
        {
            return await asyncDbConnection.GetWithChildrenAsync<Meal>(mealId).ConfigureAwait(false);
        }

        public static async Task SaveMealAsync(SQLiteAsyncConnection asyncDbConnection, Meal meal)
        {
            if(meal.MealId <= 0) {
                await asyncDbConnection.InsertWithChildrenAsync(meal).ConfigureAwait(false);
            } else {
                await asyncDbConnection.UpdateWithChildrenAsync(meal).ConfigureAwait(false);
            }
        }

        public static async Task<int> DeleteMealAsync(SQLiteAsyncConnection asyncDbConnection, Meal meal)
        {
            return await asyncDbConnection.DeleteAsync(meal).ConfigureAwait(false);
        }

        public static async Task<int> DeleteAllMealsAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.DeleteAllAsync<Meal>().ConfigureAwait(false);
        }

        [Ignore]
        public int Id { get { return MealId; } set { MealId = value; } }

        /// <summary>
        /// Gets or sets the meal identifier.
        /// </summary>
        /// <value>
        /// The meal identifier.
        /// </value>
        [PrimaryKey, AutoIncrement, Column("_id")]
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

        /// <summary>
        /// Gets or sets the weight of this meal in weight units.
        /// </summary>
        /// <value>
        /// The weight of this meal in weight units.
        /// </value>
        [Ignore]
        public double WeightInUnits
        {
            get { return BackpackPlannerState.Instance.Settings.Units.WeightFromGrams(WeightInGrams); }
            set { _weightInGrams = (int)BackpackPlannerState.Instance.Settings.Units.GramsFromWeight(value); }
        }

        // ReSharper disable once InconsistentNaming
        private int _costInUSDP;

        /// <summary>
        /// Gets or sets the cost of this meal in US pennies.
        /// </summary>
        /// <value>
        /// The cost of this meal in US pennies.
        /// </value>
        [NotNull]
        // ReSharper disable once InconsistentNaming
        public int CostInUSDP
        {
            get { return _costInUSDP; }
            set { _costInUSDP = value < 0 ? 0 : value; }
        }

        /// <summary>
        /// Gets or sets the cost of this meal in currency units.
        /// </summary>
        /// <value>
        /// The cost of this meal in currency units.
        /// </value>
        [Ignore]
        public double CostInCurrency
        {
            get { return BackpackPlannerState.Instance.Settings.Currency.CurrencyFromUSDP(CostInUSDP); }
            set { _costInUSDP = (int)BackpackPlannerState.Instance.Settings.Currency.USDPFromCurrency(value); }
        }

        /// <summary>
        /// Gets or sets the meal note.
        /// </summary>
        /// <value>
        /// The meal note.
        /// </value>
        [MaxLength(1024)]
        public string Note { get; set; } = string.Empty;

        [ManyToMany(typeof(TripPlanMeal), CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<TripPlan> TripPlans { get; set; }

        [Ignore]
        public int TripPlanCount => TripPlans?.Count ?? 0;

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
