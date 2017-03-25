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

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using SQLite;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Systems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearSystem : DatabaseItem, IBackpackPlannerItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GearSystem));

#region Database Init
        /// <summary>
        /// Initializes the gear system tables in the database.
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
                Logger.Debug("Database versions match, nothing to do for gear system tables...");
                return;
            }

            if(oldVersion < 1 && newVersion >= 1) {
                await CreateTablesAsync(state).ConfigureAwait(false);
            }
        }

        private static async Task CreateTablesAsync(BackpackPlannerState state)
        {
            Logger.Debug("Creating gear system table...");
            await state.DatabaseState.Connection.AsyncConnection.CreateTableAsync<GearSystem>().ConfigureAwait(false);

            await GearSystemGearItem.CreateTablesAsync(state).ConfigureAwait(false);
        }
#endregion

#region Properties
        [Ignore]
        public override int Id { get { return GearSystemId; } set { GearSystemId = value; } }

        /// <summary>
        /// Gets or sets the gear system identifier.
        /// </summary>
        /// <value>
        /// The gear system identifier.
        /// </value>
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int GearSystemId { get; set; } = -1;

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the gear system name.
        /// </summary>
        /// <value>
        /// The gear system name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name
        {
            get { return _name; }
            set { _name = value ?? string.Empty; }
        }

        private List<GearSystemGearItem> _gearItems = new List<GearSystemGearItem>();

        /// <summary>
        /// Gets or sets the gear items contained in this system.
        /// </summary>
        /// <value>
        /// The gear items contained in this system.
        /// </value>
        public List<GearSystemGearItem> GearItems
        {
            get { return _gearItems; }
            set { _gearItems = value ?? new List<GearSystemGearItem>(); }
        }

        private string _note = string.Empty;

        /// <summary>
        /// Gets or sets the gear system note.
        /// </summary>
        /// <value>
        /// The gear system note.
        /// </value>
        [MaxLength(1024)]
        public string Note
        {
            get { return _note; }
            set { _note = value ?? string.Empty; }
        }
#endregion

        public GearSystem()
        {
        }

        public GearSystem(BackpackPlannerSettings settings)
            : base(settings)
        {
        }

#region Gear Items
        public int GetGearItemCount(List<int> visitedGearItems=null)
        {
            return GearSystemGearItem.GetGearItemCount(_gearItems, visitedGearItems);
        }
#endregion

#region Weight
        public int GetTotalWeightInGrams(List<int> visitedGearItems=null)
        {
            return GearSystemGearItem.GetTotalWeightInGrams(_gearItems, visitedGearItems);
        }

        public float GetTotalWeightInUnits()
        {
            int weightInGrams = GetTotalWeightInGrams();
            return Settings?.Units.WeightFromGrams(weightInGrams) ?? weightInGrams;
        }
#endregion

#region Cost
        public int GetTotalCostInUSDP(List<int> visitedGearItems=null)
        {
            return GearSystemGearItem.GetTotalCostInUSDP(_gearItems, visitedGearItems);
        }

        public float GetTotalCostInCurrency()
        {
            int costInUSDP = GetTotalCostInUSDP();
            return Settings?.Currency.CurrencyFromUSDP(costInUSDP) ?? costInUSDP;
        }

        public float GetCostPerWeightInCurrency()
        {
            float weightInUnits = GetTotalWeightInUnits();
            float costInCurrency = GetTotalCostInCurrency();

            return 0.0f == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }
#endregion

        public override bool Equals(object obj)
        {
            if(GearSystemId < 1) {
                return false;
            }

            GearSystem gearSystem = obj as GearSystem;
            return GearSystemId == gearSystem?.GearSystemId;
        }

        public override int GetHashCode()
        {
            return GearSystemId.GetHashCode();
        }
    }
}
