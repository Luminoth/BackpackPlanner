﻿/*
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

using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Systems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearSystem
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<GearSystem>().ConfigureAwait(false);

            await GearSystemGearItem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
        }

        public static async Task<List<GearSystem>>  GetGearSystemsAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.Table<GearSystem>().ToListAsync().ConfigureAwait(false);
        }

        public static async Task<GearSystem> GetGearSystemAsync(SQLiteAsyncConnection asyncDbConnection, int gearSystemId)
        {
            return await asyncDbConnection.GetAsync<GearSystem>(gearSystemId).ConfigureAwait(false);
        }

        public static async Task<int> SaveGearSystemAsync(SQLiteAsyncConnection asyncDbConnection, GearSystem gearSystem)
        {
            return gearSystem.GearSystemId <= 0
                ? await asyncDbConnection.InsertAsync(gearSystem).ConfigureAwait(false)
                : await asyncDbConnection.UpdateAsync(gearSystem).ConfigureAwait(false);
        }

        public static async Task<int> DeleteGearSystemAsync(SQLiteAsyncConnection asyncDbConnection, GearSystem gearSystem)
        {
            return await asyncDbConnection.DeleteAsync(gearSystem).ConfigureAwait(false);
        }

        public static async Task<int> DeleteAllGearSystemsAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.DeleteAllAsync<GearSystem>().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the gear system identifier.
        /// </summary>
        /// <value>
        /// The gear system identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
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
        [ManyToMany(typeof(GearSystemGearItem))]
        public List<GearItem> GearItems { get; set; }

        /// <summary>
        /// Gets or sets the gear system note.
        /// </summary>
        /// <value>
        /// The gear system note.
        /// </value>
        [MaxLength(1024)]
        public string Note { get; set; } = string.Empty;

        [ManyToMany(typeof(GearCollectionGearSystem))]
        public List<GearCollection> GearCollections { get; set; }

        [ManyToMany(typeof(TripPlanGearSystem))]
        public List<TripPlan> TripPlans { get; set; }

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