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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Settings;

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Systems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearSystem : DatabaseItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GearSystem));

        /// <summary>
        /// Initializes the gear system tables in the database.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        /// <param name="oldVersion">The old database version.</param>
        /// <param name="newVersion">The new database version.</param>
        /// <remarks>
        /// The connection should be thread locked.
        /// </remarks>
        public static async Task InitDatabaseAsync(SQLiteAsyncConnection asyncDbConnection, int oldVersion, int newVersion)
        {
            if(null == asyncDbConnection) {
                throw new ArgumentNullException(nameof(asyncDbConnection));
            }

            if(oldVersion >= newVersion) {
                Logger.Debug("Database versions match, nothing to do for gear system tables...");
                return;
            }

            if(oldVersion < 1 && newVersion >= 1) {
                Logger.Debug("Creating gear system tables...");
                await CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            }
        }

        private static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<GearSystem>().ConfigureAwait(false);

            await GearSystemGearItem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
        }

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

        /// <summary>
        /// Gets or sets the gear system name.
        /// </summary>
        /// <value>
        /// The gear system name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the gear items contained in this system.
        /// </summary>
        /// <value>
        /// The gear items contained in this system.
        /// </value>
        [ManyToMany(typeof(GearSystemGearItem), CascadeOperations = CascadeOperation.All)]
        public List<GearItem> GearItems { get; set; }

        [Ignore]
        public int GearItemCount => GearItems?.Count ?? 0;

        /// <summary>
        /// Gets or sets the gear system note.
        /// </summary>
        /// <value>
        /// The gear system note.
        /// </value>
        [MaxLength(1024)]
        public string Note { get; set; } = string.Empty;

        [ManyToMany(typeof(GearCollectionGearSystem), CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<GearCollection> GearCollections { get; set; }

        [Ignore]
        public int GearCollectionCount => GearCollections?.Count ?? 0;

        [ManyToMany(typeof(TripPlanGearSystem), CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<TripPlan> TripPlans { get; set; }

        [Ignore]
        public int TripPlanCount => TripPlans?.Count ?? 0;

        public GearSystem()
        {
        }

        public GearSystem(BackpackPlannerSettings settings) : base(settings)
        {
        }

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
