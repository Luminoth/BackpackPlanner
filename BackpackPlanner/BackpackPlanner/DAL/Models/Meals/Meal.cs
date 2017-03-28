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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Meals
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Meal : BaseModel, IBackpackPlannerItem
    {
        public override int Id => MealId;

#region Database Properties
        /// <summary>
        /// Gets or sets the meal identifier.
        /// </summary>
        /// <value>
        /// The meal identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MealId { get; private set; }

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the meal name.
        /// </summary>
        /// <value>
        /// The meal name.
        /// </value>
        [Required, MaxLength(64)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }

        private string _url = string.Empty;

        /// <summary>
        /// Gets or sets the meal url.
        /// </summary>
        /// <value>
        /// The meal url.
        /// </value>
        [MaxLength(2048)]
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }

        private MealTime _mealTime = MealTime.Other;

        /// <summary>
        /// Gets or sets the meal time of this meal.
        /// </summary>
        /// <value>
        /// The meal time of this meal.
        /// </value>
        [Required]
        public MealTime MealTime
        {
            get { return _mealTime; }
            set
            {
                _mealTime = value;
                NotifyPropertyChanged();
            }
        }

        private int _servingCount = 1;

        /// <summary>
        /// Gets or sets the serving count of this meal.
        /// </summary>
        /// <value>
        /// The serving count of this meal.
        /// </value>
        [Required]
        public int ServingCount
        {
            get { return _servingCount; }
            set
            {
                _servingCount = value < 1 ? 1 : value;
                NotifyPropertyChanged();
            }
        }

        private int _calories;

        /// <summary>
        /// Gets or sets the calories in this meal.
        /// </summary>
        /// <value>
        /// The calories in this meal.
        /// </value>
        [Required]
        public int Calories
        {
            get { return _calories; }
            set
            {
                _calories = value < 0 ? 0 : value;
                NotifyPropertyChanged();
            }
        }

        private int _proteinInGrams;

        /// <summary>
        /// Gets or sets the protein in this meal in grams.
        /// </summary>
        /// <value>
        /// The protein in this meal in grams.
        /// </value>
        [Required]
        public int ProteinInGrams
        {
            get { return _proteinInGrams; }
            set
            {
                _proteinInGrams = value < 0 ? 0 : value;
                NotifyPropertyChanged();
            }
        }

        private int _fiberInGrams;

        /// <summary>
        /// Gets or sets the fiber in this meal in grams.
        /// </summary>
        /// <value>
        /// The fiber in this meal in grams.
        /// </value>
        [Required]
        public int FiberInGrams
        {
            get { return _fiberInGrams; }
            set
            {
                _fiberInGrams = value < 0 ? 0 : value;
                NotifyPropertyChanged();
            }
        }

        private int _weightInGrams;

        /// <summary>
        /// Gets or sets the weight of this meal in grams.
        /// </summary>
        /// <value>
        /// The weight of this meal in grams.
        /// </value>
        [Required]
        public int WeightInGrams
        {
            get { return _weightInGrams; }
            set
            {
                _weightInGrams = value < 0 ? 0 : value;
                NotifyPropertyChanged();
            }
        }

        // ReSharper disable once InconsistentNaming
        private int _costInUSDP;

        /// <summary>
        /// Gets or sets the cost of this meal in US pennies.
        /// </summary>
        /// <value>
        /// The cost of this meal in US pennies.
        /// </value>
        [Required]
        // ReSharper disable once InconsistentNaming
        public int CostInUSDP
        {
            get { return _costInUSDP; }
            set
            {
                _costInUSDP = value < 0 ? 0 : value;
                NotifyPropertyChanged();
            }
        }

        private string _note = string.Empty;

        /// <summary> 
        /// Gets or sets the meal note. 
        /// </summary> 
        /// <value> 
        /// The meal note. 
        /// </value> 
        [MaxLength(1024)] 
        public string Note
        {
            get { return _note; }
            set
            {
                _note = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }
#endregion

        [NotMapped]
        public float CaloriesPerServing => Calories / (float)ServingCount;

        [NotMapped]
        public float CaloriesPerWeight => Calories / WeightInUnits;

        [NotMapped]
        public float ProteinPerServing => ProteinInGrams / (float)ServingCount;

        [NotMapped]
        public float FiberPerServing => FiberInGrams / (float)ServingCount;

        /// <summary>
        /// Gets or sets the weight of this meal in weight units.
        /// </summary>
        /// <value>
        /// The weight of this meal in weight units.
        /// </value>
        [NotMapped]
        public float WeightInUnits
        {
            get { return Settings?.Units.WeightFromGrams(WeightInGrams) ?? WeightInGrams; }
            set { _weightInGrams = (int)(Settings?.Units.GramsFromWeight(value) ?? value); }
        }

        [NotMapped]
        public float WeightInUnitsPerServing => WeightInUnits / ServingCount;

        /// <summary>
        /// Gets or sets the cost of this meal in currency units.
        /// </summary>
        /// <value>
        /// The cost of this meal in currency units.
        /// </value>
        [NotMapped]
        public float CostInCurrency
        {
            get { return Settings?.Currency.CurrencyFromUSDP(CostInUSDP) ?? CostInUSDP; }
            set { _costInUSDP = (int)(Settings?.Currency.USDPFromCurrency(value) ?? value); }
        }

        [NotMapped]
        public float CostPerWeightInCurrency => 0.0f == WeightInUnits ? 0.0f : CostInCurrency / WeightInUnits;

        public Meal(BackpackPlannerSettings settings)
            : base(settings)
        {
        }

        public Meal()
        {
        }

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
