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

using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using SQLite.Net.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Items
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearItem : DatabaseItem, IBackpackPlannerItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GearItem));

#region Database Init
        /// <summary>
        /// Initializes the gear item tables in the database.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <param name="oldVersion">The old database version.</param>
        /// <param name="newVersion">The new database version.</param>
        /// <remarks>
        /// The connection should be thread locked.
        /// </remarks>
        public static async Task InitDatabaseAsync(BackpackPlannerState state, int oldVersion, int newVersion)
        {
            ValidateState(state);

            if(oldVersion >= newVersion) {
                Logger.Debug("Database versions match, nothing to do for gear item tables...");
                return;
            }

            if(oldVersion < 1 && newVersion >= 1) {
                await CreateTablesAsync(state).ConfigureAwait(false);
            }
        }

        private static async Task CreateTablesAsync(BackpackPlannerState state)
        {
            Logger.Debug("Creating gear item table...");
            await state.DatabaseState.Connection.AsyncConnection.CreateTableAsync<GearItem>().ConfigureAwait(false);
        }
#endregion

#region Properties
        [Ignore]
        public override int Id { get { return GearItemId; } set { GearItemId = value; } }

        /// <summary>
        /// Gets or sets the gear item identifier.
        /// </summary>
        /// <value>
        /// The gear item identifier.
        /// </value>
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int GearItemId { get; set; } = -1;

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the gear item name.
        /// </summary>
        /// <value>
        /// The gear item name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name
        {
            get { return _name; }
            set { _name = value ?? string.Empty; }
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
            get { return _make; }
            set { _make = value ?? string.Empty; }
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
            get { return _model; }
            set { _model = value ?? string.Empty; }
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
            get { return _url; }
            set { _url = value ?? string.Empty; }
        }

        /// <summary>
        /// Gets or sets the carried-ness of this gear item.
        /// </summary>
        /// <value>
        /// The carried-ness of this gear item.
        /// </value>
        [NotNull]
        public GearCarried Carried  { get; set; } = GearCarried.Carried;

        /// <summary>
        /// Gets or sets whether this gear item is consumable or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this gear item is consumable; otherwise, <c>false</c>.
        /// </value>
        [NotNull]
        public bool IsConsumable { get; set; } = false;

        private int _consumedPerDay = 1;

        /// <summary>
        /// Gets or sets the amount of this gear item consumed per day, if consumable.
        /// </summary>
        /// <value>
        /// The amount of this gear item consumed per day, if consumable.
        /// </value>
        [NotNull]
        public int ConsumedPerDay
        {
            get { return _consumedPerDay; }
            set { _consumedPerDay = value < 1 ? 1 : value; }
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
        [NotNull]
        public int WeightInGrams
        {
            get { return _weightInGrams; }
            set { _weightInGrams = value < 0 ? 0 : value; }
        }

        /// <summary>
        /// Gets or sets the weight of this gear item in weight units.
        /// </summary>
        /// <value>
        /// The weight of this gear item in weight units.
        /// </value>
        [Ignore]
        public float WeightInUnits
        {
            get { return Settings?.Units.WeightFromGrams(WeightInGrams) ?? WeightInGrams; }
            set { _weightInGrams = (int)(Settings?.Units.GramsFromWeight(value) ?? value); }
        }

        // ReSharper disable once InconsistentNaming
        private int _costInUSDP;

        /// <summary>
        /// Gets or sets the cost of this gear item in US pennies.
        /// </summary>
        /// <value>
        /// The cost of this gear item in US pennies.
        /// </value>
        [NotNull]
        // ReSharper disable once InconsistentNaming
        public int CostInUSDP
        {
            get { return _costInUSDP; }
            set { _costInUSDP = value < 0 ? 0 : value; }
        }

        /// <summary>
        /// Gets or sets the cost of this gear item in currency units.
        /// </summary>
        /// <value>
        /// The cost of this gear item in currency units.
        /// </value>
        [Ignore]
        public float CostInCurrency
        {
            get { return Settings?.Currency.CurrencyFromUSDP(CostInUSDP) ?? CostInUSDP; }
            set { _costInUSDP = (int)(Settings?.Currency.USDPFromCurrency(value) ?? value); }
        }

        [Ignore]
        public float CostPerWeightInCurrency => 0.0f == WeightInUnits ? 0.0f : CostInCurrency / WeightInUnits;

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
            get { return _note; }
            set { _note = value ?? string.Empty; }
        }

        [Ignore]
        public WeightCategory WeightCategory => GearCarried.NotCarried == Carried ? WeightCategory.None : Settings.GetWeightCategory(WeightInGrams);
#endregion

        public GearItem()
        {
        }

        public GearItem(BackpackPlannerSettings settings)
            : base(settings)
        {
        }

        public override bool Equals(object obj)
        {
            if(GearItemId < 1) {
                return false;
            }

            GearItem gearItem = obj as GearItem;
            return GearItemId == gearItem?.GearItemId;
        }

        public override int GetHashCode()
        {
            return GearItemId.GetHashCode();
        }
    }
}
