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
using System.Linq;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

using SQLite.Net.Async;

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// Caches gear items, systems, and collections
    /// </summary>
    /// <remarks>
    /// Fow now this is an all or nothing cache. Later on, to conserve resources,
    /// it might start allowing cached items to decay
    /// </remarks>
    public class GearCache
    {
        /// <summary>
        /// Initializes the gear state tables in the database.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        /// <param name="oldVersion">The old version. If this is less than 1, this is a new database.</param>
        /// <param name="newVersion">The new version.</param>
        public static async Task InitDatabaseAsync(SQLiteAsyncConnection asyncDbConnection, int oldVersion, int newVersion)
        {
            if(oldVersion >= newVersion) {
                return;
            }

            if(oldVersion < 1) {
                await GearItem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
                await GearSystem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
                await GearCollection.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            }
        }

// TODO: move these
#region Gear Item Queries
        private static async Task<List<GearItem>>  GetGearItemsAsync()
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.Table<GearItem>().ToListAsync().ConfigureAwait(false);
            }
            return new List<GearItem>();
        }

        private static async Task<GearItem> GetGearItemAsync(int gearItemId)
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.GetAsync<GearItem>(gearItemId).ConfigureAwait(false);
            }
            return null;
        }

        private static async Task<int> SaveGearItemAsync(GearItem gearItem)
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                if(gearItem.GearItemId <= 0) {
                    return await BackpackPlannerState.Instance.AsyncDbConnection.InsertAsync(gearItem).ConfigureAwait(false);
                }
                return await BackpackPlannerState.Instance.AsyncDbConnection.UpdateAsync(gearItem).ConfigureAwait(false);
            }
            return -1;
        }

        private static async Task<int> DeleteGearItemAsync(GearItem gearItem)
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.DeleteAsync(gearItem).ConfigureAwait(false);
            }
            return 0;
        }

        private static async Task<int> DeleteAllGearItemsAsync()
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.DeleteAllAsync<GearItem>().ConfigureAwait(false);
            }
            return 0;
        }
#endregion

// TODO: move these
#region Gear System Queries
        private static async Task<List<GearSystem>>  GetGearSystemsAsync()
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.Table<GearSystem>().ToListAsync().ConfigureAwait(false);
            }
            return new List<GearSystem>();
        }

        private static async Task<GearSystem> GetGearSystemAsync(int gearSystemId)
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.GetAsync<GearSystem>(gearSystemId).ConfigureAwait(false);
            }
            return null;
        }

        private static async Task<int> SaveGearSystemAsync(GearSystem gearSystem)
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                if(gearSystem.GearSystemId <= 0) {
                    return await BackpackPlannerState.Instance.AsyncDbConnection.InsertAsync(gearSystem).ConfigureAwait(false);
                }
                return await BackpackPlannerState.Instance.AsyncDbConnection.UpdateAsync(gearSystem).ConfigureAwait(false);
            }
            return -1;
        }

        private static async Task<int> DeleteGearSystemAsync(GearSystem gearSystem)
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.DeleteAsync(gearSystem).ConfigureAwait(false);
            }
            return 0;
        }

        private static async Task<int> DeleteAllGearSystemsAsync()
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.DeleteAllAsync<GearSystem>().ConfigureAwait(false);
            }
            return 0;
        }
#endregion

// TODO: move these
#region Gear Collection Queries
        private static async Task<List<GearCollection>>  GetGearCollectionsAsync()
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.Table<GearCollection>().ToListAsync().ConfigureAwait(false);
            }
            return new List<GearCollection>();
        }

        private static async Task<GearCollection> GetGearCollectionAsync(int gearCollectionId)
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.GetAsync<GearCollection>(gearCollectionId).ConfigureAwait(false);
            }
            return null;
        }

        private static async Task<int> SaveGearCollectionAsync(GearCollection gearCollection)
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                if(gearCollection.GearCollectionId <= 0) {
                    return await BackpackPlannerState.Instance.AsyncDbConnection.InsertAsync(gearCollection).ConfigureAwait(false);
                }
                return await BackpackPlannerState.Instance.AsyncDbConnection.UpdateAsync(gearCollection).ConfigureAwait(false);
            }
            return -1;
        }

        private static async Task<int> DeleteGearCollectionAsync(GearCollection gearCollection)
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.DeleteAsync(gearCollection).ConfigureAwait(false);
            }
            return 0;
        }

        private static async Task<int> DeleteAllGearCollectionsAsync()
        {
            if(null != BackpackPlannerState.Instance.AsyncDbConnection) {
                return await BackpackPlannerState.Instance.AsyncDbConnection.DeleteAllAsync<GearCollection>().ConfigureAwait(false);
            }
            return 0;
        }
#endregion

        private readonly HashSet<GearItem> _gearItemCache = new HashSet<GearItem>();
        private readonly HashSet<GearSystem> _gearSystemCache = new HashSet<GearSystem>();
        private readonly HashSet<GearCollection> _gearCollectionCache = new HashSet<GearCollection>();  

        /// <summary>
        /// Gets the gear item count.
        /// </summary>
        /// <value>
        /// The gear item count.
        /// </value>
        public int GearItemCount => _gearItemCache.Count;

        /// <summary>
        /// Gets the gear system count.
        /// </summary>
        /// <value>
        /// The gear system count.
        /// </value>
        public int GearSystemCount => _gearSystemCache.Count;

        /// <summary>
        /// Gets the gear collection count.
        /// </summary>
        /// <value>
        /// The gear collection count.
        /// </value>
        public int GearCollectionCount => _gearCollectionCache.Count;

#region LoadFromDevice
        /// <summary>
        /// Loads the gear state from the device.
        /// </summary>
        public async Task LoadFromDeviceAsync()
        {
            await LoadGearItemsFromDeviceAsync().ConfigureAwait(false);

            await LoadGearSystemsFromDeviceAsync().ConfigureAwait(false);

            await LoadGearCollectionsFromDeviceAsync().ConfigureAwait(false);
        }

        private async Task LoadGearItemsFromDeviceAsync()
        {
            var gearItems = await GetGearItemsAsync().ConfigureAwait(false);
            foreach(GearItem gearItem in gearItems) {
                await AddGearItemAsync(gearItem).ConfigureAwait(false);
            }
        }

        private async Task LoadGearSystemsFromDeviceAsync()
        {
            var gearSystems = await GetGearSystemsAsync().ConfigureAwait(false);
            foreach(GearSystem gearSystem in gearSystems) {
                await AddGearSystemAsync(gearSystem).ConfigureAwait(false);
            }
        }

        private async Task LoadGearCollectionsFromDeviceAsync()
        {
            var gearCollections = await GetGearCollectionsAsync().ConfigureAwait(false);
            foreach(GearCollection gearCollection in gearCollections) {
                await AddGearCollectionAsync(gearCollection).ConfigureAwait(false);
            }
        }
#endregion

#region Gear Items
        /// <summary>
        /// Gets a gear item by identifier.
        /// </summary>
        /// <param name="gearItemId">The gear item identifier.</param>
        /// <returns>The first gear item with the given identifier or null if it isn't in the container</returns>
        public async Task<GearItem> GetGearItemByIdAsync(int gearItemId)
        {
            if(gearItemId < 1) {
                throw new ArgumentException("GearItemId cannot be less than 1", nameof(gearItemId));
            }

            GearItem gearItem = _gearItemCache.FirstOrDefault(x => x.GearItemId == gearItemId);
            if(null == gearItem) {
                gearItem = await GetGearItemAsync(gearItemId).ConfigureAwait(false);
                if(null != gearItem) {
                    await AddGearItemAsync(gearItem).ConfigureAwait(false);
                }
            }
            return gearItem;
        }

        /// <summary>
        /// Adds the gear item to the container.
        /// </summary>
        /// <param name="gearItem">The gear item to add.</param>
        /// <returns>The id of the newly added gear item or -1 if it couldn't be added</returns>
        public async Task<int> AddGearItemAsync(GearItem gearItem)
        {
            if(null == gearItem) {
                throw new ArgumentNullException(nameof(gearItem));
            }

            if(gearItem.GearItemId < 1) {
                await SaveGearItemAsync(gearItem).ConfigureAwait(false);
            }

            return gearItem.GearItemId < 1 ? -1 : (_gearItemCache.Add(gearItem) ? gearItem.GearItemId : -1);
        }

        /// <summary>
        /// Deletes the gear item from the container.
        /// </summary>
        /// <param name="gearItem">The gear item to delete.</param>
        public async Task RemoveGearItemAsync(GearItem gearItem)
        {
            if(null == gearItem) {
                throw new ArgumentNullException(nameof(gearItem));
            }

            if(gearItem.GearItemId < 1) {
                throw new ArgumentException("GearItemId cannot be less than 1!", nameof(gearItem));
            }

            await DeleteGearItemAsync(gearItem).ConfigureAwait(false);

            _gearItemCache.Remove(gearItem);
        }

        /// <summary>
        /// Deletes all of the gear items.
        /// </summary>
        public async Task RemoveAllGearItemsAsync()
        {
            await DeleteAllGearItemsAsync().ConfigureAwait(false);

            _gearItemCache.Clear();
        }
#endregion

#region Gear Systems
        /// <summary>
        /// Gets a gear system by identifier.
        /// </summary>
        /// <param name="gearSystemId">The gear system identifier.</param>
        /// <returns>The first gear system with the given identifier or null if it isn't in the container</returns>
        public async Task<GearSystem> GetGearSystemByIdAsync(int gearSystemId)
        {
            if(gearSystemId < 1) {
                throw new ArgumentException("GearSystemId cannot be less than 1", nameof(gearSystemId));
            }

            GearSystem gearSystem = _gearSystemCache.FirstOrDefault(x => x.GearSystemId == gearSystemId);
            if(null == gearSystem) {
                gearSystem = await GetGearSystemAsync(gearSystemId).ConfigureAwait(false);
                if(null != gearSystem) {
                    await AddGearSystemAsync(gearSystem).ConfigureAwait(false);
                }
            }
            return gearSystem;
        }

        /// <summary>
        /// Adds the gear system to the container.
        /// </summary>
        /// <param name="gearSystem">The gear system to add.</param>
        /// <returns>The id of the newly added gear system or -1 if it couldn't be added</returns>
        public async Task<int> AddGearSystemAsync(GearSystem gearSystem)
        {
            if(null == gearSystem) {
                throw new ArgumentNullException(nameof(gearSystem));
            }

            if(gearSystem.GearSystemId < 1) {
                await SaveGearSystemAsync(gearSystem).ConfigureAwait(false);
            }

            return gearSystem.GearSystemId < 1 ? -1 : (_gearSystemCache.Add(gearSystem) ? gearSystem.GearSystemId : -1);
        }

        /// <summary>
        /// Deletes the gear system from the container.
        /// </summary>
        /// <param name="gearSystem">The gear system to delete.</param>
        public async Task RemoveGearSystemAsync(GearSystem gearSystem)
        {
            if(null == gearSystem) {
                throw new ArgumentNullException(nameof(gearSystem));
            }

            if(gearSystem.GearSystemId < 1) {
                throw new ArgumentException("GearSystemId cannot be less than 1!", nameof(gearSystem));
            }

            await DeleteGearSystemAsync(gearSystem).ConfigureAwait(false);

            _gearSystemCache.Remove(gearSystem);
        }

        /// <summary>
        /// Deletes all of the gear systems.
        /// </summary>
        public async Task RemoveAllGearSystemsAsync()
        {
            await DeleteAllGearSystemsAsync().ConfigureAwait(false);

            _gearSystemCache.Clear();
        }
#endregion

#region Gear Collections
        /// <summary>
        /// Gets a gear collection by identifier.
        /// </summary>
        /// <param name="gearCollectionId">The gear collection identifier.</param>
        /// <returns>The first gear collection with the given identifier or null if it isn't in the container</returns>
        public async Task<GearCollection> GetGearCollectionByIdAsync(int gearCollectionId)
        {
            if(gearCollectionId < 1) {
                throw new ArgumentException("GearCollectionId cannot be less than 1", nameof(gearCollectionId));
            }

            GearCollection gearCollection = _gearCollectionCache.FirstOrDefault(x => x.GearCollectionId == gearCollectionId);
            if(null == gearCollection) {
                gearCollection = await GetGearCollectionAsync(gearCollectionId).ConfigureAwait(false);
                if(null != gearCollection) {
                    await AddGearCollectionAsync(gearCollection).ConfigureAwait(false);
                }
            }
            return gearCollection;
        }

        /// <summary>
        /// Adds the gear collection to the container.
        /// </summary>
        /// <param name="gearCollection">The gear collection to add.</param>
        /// <returns>The id of the newly added gear collection or -1 if it couldn't be added</returns>
        public async Task<int> AddGearCollectionAsync(GearCollection gearCollection)
        {
            if(null == gearCollection) {
                throw new ArgumentNullException(nameof(gearCollection));
            }

            if(gearCollection.GearCollectionId < 1) {
                await SaveGearCollectionAsync(gearCollection).ConfigureAwait(false);
            }

            return gearCollection.GearCollectionId < 1 ? -1 : (_gearCollectionCache.Add(gearCollection) ? gearCollection.GearCollectionId : -1);
        }

        /// <summary>
        /// Deletes the gear collection from the container.
        /// </summary>
        /// <param name="gearCollection">The gear collection to delete.</param>
        public async Task RemoveGearCollectionAsync(GearCollection gearCollection)
        {
            if(null == gearCollection) {
                throw new ArgumentNullException(nameof(gearCollection));
            }

            if(gearCollection.GearCollectionId < 1) {
                throw new ArgumentException("GearCollectionId cannot be less than 1!", nameof(gearCollection));
            }

            await DeleteGearCollectionAsync(gearCollection).ConfigureAwait(false);

            _gearCollectionCache.Remove(gearCollection);
        }

        /// <summary>
        /// Deletes all of the gear collections.
        /// </summary>
        public async Task RemoveAllGearCollectionsAsync()
        {
            await DeleteAllGearCollectionsAsync().ConfigureAwait(false);

            _gearCollectionCache.Clear();
        }
#endregion
    }
}
