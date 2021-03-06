﻿/*
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
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items
{
    /// <summary>
    /// 
    /// </summary>

    [Serializable]
    public class GearItem : BaseModel<GearItem>, IBackpackPlannerItem
    {
#region Static Helpers
        public static async Task<IReadOnlyCollection<GearItem>> GetAll(DatabaseContext dbContext)
        {
            return await dbContext.GearItems.ToListAsync().ConfigureAwait(false);
        }
#endregion

        public override int Id => GearItemId;

#region Database Properties
        /// <summary>
        /// Gets or sets the gear item identifier.
        /// </summary>
        /// <value>
        /// The gear item identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GearItemId { get; private set; }

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the gear item name.
        /// </summary>
        /// <value>
        /// The gear item name.
        /// </value>
        [Required, MaxLength(64)]
        public string Name
        {
            get => _name;

            set
            {
                _name = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }

        private string _make = string.Empty;

        /// <summary>
        /// Gets or sets the gear item make.
        /// </summary>
        /// <value>
        /// The gear item make.
        /// </value>
        [MaxLength(32)]
        public string Make
        {
            get => _make;

            set
            {
                _make = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }

        private string _model = string.Empty;

        /// <summary>
        /// Gets or sets the gear item model.
        /// </summary>
        /// <value>
        /// The gear item model.
        /// </value>
        [MaxLength(32)]
        public string Model
        {
            get => _model;

            set
            {
                _model = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }

        private string _url = string.Empty;

        /// <summary>
        /// Gets or sets the gear item url.
        /// </summary>
        /// <value>
        /// The gear item url.
        /// </value>
        [MaxLength(2048)]
        public string Url
        {
            get => _url;

            set
            {
                _url = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }

        private GearCarried _gearCarried = GearCarried.Carried;

        /// <summary>
        /// Gets or sets the carried-ness of this gear item.
        /// </summary>
        /// <value>
        /// The carried-ness of this gear item.
        /// </value>
        [Required]
        public GearCarried Carried
        {
            get => _gearCarried;

            set
            {
                _gearCarried = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isConsumable;

        /// <summary>
        /// Gets or sets whether this gear item is consumable or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this gear item is consumable; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool IsConsumable
        {
            get => _isConsumable;

            set
            {
                _isConsumable = value;
                NotifyPropertyChanged();
            }
        }

        private int _consumedPerDay = 1;

        /// <summary>
        /// Gets or sets the amount of this gear item consumed per day, if consumable.
        /// </summary>
        /// <value>
        /// The amount of this gear item consumed per day, if consumable.
        /// </value>
        [Required]
        public int ConsumedPerDay
        {
            get => _consumedPerDay;

            set
            {
                _consumedPerDay = value < 1 ? 1 : value;
                NotifyPropertyChanged();
            }
        }

        private int _weightInGrams;

        /// <summary>
        /// Gets or sets the weight of this gear item in grams.
        /// </summary>
        /// <value>
        /// The weight of this gear item in grams.
        /// </value>
        /// <remarks>
        /// TODO: it's possible this should be in milligrams
        /// </remarks>
        [Required]
        public int WeightInGrams
        {
            get => _weightInGrams;

            set
            {
                _weightInGrams = value < 0 ? 0 : value;
                NotifyPropertyChanged();
            }
        }

        // ReSharper disable once InconsistentNaming
        private int _costInUSDP;

        /// <summary>
        /// Gets or sets the cost of this gear item in US pennies.
        /// </summary>
        /// <value>
        /// The cost of this gear item in US pennies.
        /// </value>
        [Required]
        // ReSharper disable once InconsistentNaming
        public int CostInUSDP
        {
            get => _costInUSDP;

            set
            {
                _costInUSDP = value < 0 ? 0 : value;
                NotifyPropertyChanged();
            }
        }

        private string _note = string.Empty;

        /// <summary>
        /// Gets or sets the gear item note.
        /// </summary>
        /// <value>
        /// The gear item note.
        /// </value>
        [MaxLength(1024)]
        public string Note
        {
            get => _note;

            set
            {
                _note = value ?? string.Empty;
                NotifyPropertyChanged();
            }
        }
#endregion

        public override GearItem DeepCopy()
        {
            GearItem gearItem = base.DeepCopy();

            gearItem.GearItemId = GearItemId;
            gearItem.Name = Name;
            gearItem.Make = Make;
            gearItem.Model = Model;
            gearItem.Url = Url;
            gearItem.Carried = Carried;
            gearItem.IsConsumable = IsConsumable;
            gearItem.ConsumedPerDay = ConsumedPerDay;
            gearItem.WeightInGrams = WeightInGrams;
            gearItem.CostInUSDP = CostInUSDP;
            gearItem.Note = Note;

            return gearItem;
        }

        /// <summary>
        /// Gets the weight of this gear item in weight units.
        /// </summary>
        /// <returns>
        /// The weight of this gear item in weight units.
        /// </returns>
        public float GetWeightInUnits(BackpackPlannerSettings settings)
        {
            return settings.Units.WeightFromGrams(WeightInGrams);
        }

        /// <summary>
        /// Sets the weight of this gear item in weight units.
        /// </summary>
        public void SetWeightInUnits(BackpackPlannerSettings settings, float value)
        {
            WeightInGrams = settings.Units.GramsFromWeight(value);
        }

        /// <summary>
        /// Gets the cost of this gear item in currency units.
        /// </summary>
        /// <returns>
        /// The cost of this gear item in currency units.
        /// </returns>
        public float GetCostInCurrency(BackpackPlannerSettings settings)
        {
            return settings.Currency.CurrencyFromUSDP(CostInUSDP);
        }

        /// <summary>
        /// Gets or sets the cost of this gear item in currency units.
        /// </summary>
        /// <value>
        /// The cost of this gear item in currency units.
        /// </value>
        public void SetCostInCurrency(BackpackPlannerSettings settings, float value)
        {
            CostInUSDP = settings.Currency.USDPFromCurrency(value);
        }

        public float GetCostInCurrencyPerWeightInUnits(BackpackPlannerSettings settings)
        {
            float weightInUnits = GetWeightInUnits(settings);
            return Math.Abs(weightInUnits) < 0.01f ? 0.0f : GetCostInCurrency(settings) / weightInUnits;
        }

        public WeightCategory GetWeightCategory(BackpackPlannerSettings settings)
        {
            return GearCarried.NotCarried == Carried ? WeightCategory.None : settings.GetWeightCategory(WeightInGrams);
        }

        public GearItem()
        {
        }

        public override bool Equals(object obj)
        {
            if(Id < 1) {
                return false;
            }

            GearItem gearItem = obj as GearItem;
            return Id == gearItem?.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
