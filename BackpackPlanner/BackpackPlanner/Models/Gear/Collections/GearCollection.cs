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

using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearCollection
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<GearCollection>().ConfigureAwait(false);

            await GearCollectionGearSystem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            await GearCollectionGearItem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
        }

        public static async Task<List<GearCollection>>  GetGearCollectionsAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.Table<GearCollection>().ToListAsync().ConfigureAwait(false);
        }

        public static async Task<GearCollection> GetGearCollectionAsync(SQLiteAsyncConnection asyncDbConnection, int gearCollectionId)
        {
            return await asyncDbConnection.GetAsync<GearCollection>(gearCollectionId).ConfigureAwait(false);
        }

        public static async Task<int> SaveGearCollectionAsync(SQLiteAsyncConnection asyncDbConnection, GearCollection gearCollection)
        {
            return gearCollection.GearCollectionId <= 0
                ? await asyncDbConnection.InsertAsync(gearCollection).ConfigureAwait(false)
                : await asyncDbConnection.UpdateAsync(gearCollection).ConfigureAwait(false);
        }

        public static async Task<int> DeleteGearCollectionAsync(SQLiteAsyncConnection asyncDbConnection, GearCollection gearCollection)
        {
            return await asyncDbConnection.DeleteAsync(gearCollection).ConfigureAwait(false);
        }

        public static async Task<int> DeleteAllGearCollectionsAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.DeleteAllAsync<GearCollection>().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the gear collection identifier.
        /// </summary>
        /// <value>
        /// The gear collection identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public int GearCollectionId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the gear collection name.
        /// </summary>
        /// <value>
        /// The gear collection name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the gear systems contained in this collection.
        /// </summary>
        /// <value>
        /// The gear systems contained in this collection.
        /// </value>
        [ManyToMany(typeof(GearCollectionGearSystem))]
        public List<GearSystem> GearSystems { get; set; }

        /// <summary>
        /// Gets or sets the gear items contained in this collection.
        /// </summary>
        /// <value>
        /// The gear items contained in this collection.
        /// </value>
        [ManyToMany(typeof(GearCollectionGearItem))]
        public List<GearItem> GearItems { get; set; }

        /// <summary>
        /// Gets or sets the gear collection note.
        /// </summary>
        /// <value>
        /// The gear collection note.
        /// </value>
        [MaxLength(1024)]
        public string Note { get; set; } = string.Empty;

        [ManyToMany(typeof(TripPlanGearCollection))]
        public List<TripPlan> TripPlans { get; set; }

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
