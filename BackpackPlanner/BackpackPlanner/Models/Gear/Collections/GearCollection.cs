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
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Settings;
using EnergonSoftware.BackpackPlanner.Units.Currency;
using EnergonSoftware.BackpackPlanner.Units.Units;

using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearCollection : DatabaseItem, IBackpackPlannerItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GearCollection));

#region Database Init
        /// <summary>
        /// Initializes the gear collection tables in the database.
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
                Logger.Debug("Database versions match, nothing to do for gear collection tables...");
                return;
            }

            if(oldVersion < 1 && newVersion >= 1) {
                await CreateTablesAsync(state).ConfigureAwait(false);
            }
        }

        private static async Task CreateTablesAsync(BackpackPlannerState state)
        {
            Logger.Debug("Creating gear collection table...");
            await state.DatabaseState.Connection.AsyncConnection.CreateTableAsync<GearCollection>().ConfigureAwait(false);

            await GearCollectionGearSystem.CreateTablesAsync(state).ConfigureAwait(false);
            await GearCollectionGearItem.CreateTablesAsync(state).ConfigureAwait(false);
        }
#endregion

#region Properties
        [Ignore]
        public override int Id { get { return GearCollectionId; } set { GearCollectionId = value; } }

        /// <summary>
        /// Gets or sets the gear collection identifier.
        /// </summary>
        /// <value>
        /// The gear collection identifier.
        /// </value>
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int GearCollectionId { get; set; } = -1;

        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the gear collection name.
        /// </summary>
        /// <value>
        /// The gear collection name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name
        {
            get { return _name; }
            set { _name = value ?? string.Empty; }
        }

        private List<GearCollectionGearSystem> _gearSystems = new List<GearCollectionGearSystem>();

        /// <summary>
        /// Gets or sets the gear systems contained in this collection.
        /// </summary>
        /// <value>
        /// The gear systems contained in this collection.
        /// </value>
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<GearCollectionGearSystem> GearSystems
        {
            get { return _gearSystems; }
            set { _gearSystems = value ?? new List<GearCollectionGearSystem>(); }
        }

        private List<GearCollectionGearItem> _gearItems = new List<GearCollectionGearItem>();

        /// <summary>
        /// Gets or sets the gear items contained in this collection.
        /// </summary>
        /// <value>
        /// The gear items contained in this collection.
        /// </value>
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<GearCollectionGearItem> GearItems
        {
            get { return _gearItems; }
            set { _gearItems = value ?? new List<GearCollectionGearItem>(); }
        }

        private string _note = string.Empty;

        /// <summary>
        /// Gets or sets the gear collection note.
        /// </summary>
        /// <value>
        /// The gear collection note.
        /// </value>
        [MaxLength(1024)]
        public string Note
        {
            get { return _note; }
            set { _note = value ?? string.Empty; }
        }
#endregion

        public GearCollection()
        {
        }

        public GearCollection(BackpackPlannerSettings settings)
            : base(settings)
        {
        }

        public int GetTotalGearItemCount()
        {
            var visitedGearItems = new List<int>();
            return IGearItemContainerExtensions.GetGearItemCount(_gearSystems, visitedGearItems)
                + GetGearItemCount(visitedGearItems);
        }

#region Gear Systems
        public int GetGearSystemCount(List<int> visitedGearSystems=null)
        {
            return GearCollectionGearSystem.GetGearSystemCount(_gearSystems, visitedGearSystems);
        } 
#endregion

#region Gear Items
        public int GetGearItemCount(List<int> visitedGearItems=null)
        {
            return GearCollectionGearItem.GetGearItemCount(_gearItems, visitedGearItems);
        }
#endregion

#region Weight
        public int GetTotalWeightInGrams(List<int> visitedGearItems=null)
        {
            return GearCollectionGearSystem.GetTotalWeightInGrams(_gearSystems, visitedGearItems)
                + GearCollectionGearItem.GetTotalWeightInGrams(_gearItems, visitedGearItems);
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
            return GearCollectionGearSystem.GetTotalCostInUSDP(_gearSystems, visitedGearItems)
                + GearCollectionGearItem.GetTotalCostInUSDP(_gearItems, visitedGearItems);
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
            if(GearCollectionId < 1) {
                return false;
            }

            GearCollection gearCollection = obj as GearCollection;
            return GearCollectionId == gearCollection?.GearCollectionId;
        }

        public override int GetHashCode()
        {
            return GearCollectionId.GetHashCode();
        }
    }
}
