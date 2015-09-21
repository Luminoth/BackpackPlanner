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

namespace EnergonSoftware.BackpackPlanner.Cache
{
    /// <summary>
    /// Caches gear items, systems, and collections
    /// </summary>
    /// <remarks>
    /// Fow now this is an all or nothing cache. Later on, to conserve resources,
    /// it might start allowing cached items to decay
    /// </remarks>
    public sealed class GearCache
    {
        /// <summary>
        /// Initializes the gear state tables in the database.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        /// <param name="oldVersion">The old database version.</param>
        /// <param name="newVersion">The new database version.</param>
        /// <remarks>
        /// The connection should be thread locked
        /// </remarks>
        public static async Task InitDatabaseAsync(SQLiteAsyncConnection asyncDbConnection, int oldVersion, int newVersion)
        {
            if(oldVersion >= newVersion) {
                BackpackPlannerState.Instance.Logger.Debug("Database versions match, nothing to do for gear cache update...");
                return;
            }

            if(oldVersion < 1 && newVersion >= 1) {
                BackpackPlannerState.Instance.Logger.Debug("Creating gear cache tables...");
                await GearItem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
                await GearSystem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
                await GearCollection.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            }
        }

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

#region Gear Items
        public async Task LoadGearItemsAsync()
        {
            _gearItemCache.Clear();

            BackpackPlannerState.Instance.Logger.Debug("Loading gear item cache...");
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                var gearItems = await GearItem.GetGearItemsAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection).ConfigureAwait(false);
                foreach(GearItem gearItem in gearItems) {
                    await AddGearItemAsync(gearItem).ConfigureAwait(false);
                }
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

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
            if(null != gearItem) {
                return gearItem;
            }

            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                gearItem = await GearItem.GetGearItemAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, gearItemId).ConfigureAwait(false);
                if(null != gearItem) {
                    await AddGearItemAsync(gearItem).ConfigureAwait(false);
                }
                return gearItem;
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
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
                await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
                try {
                    await GearItem.SaveGearItemAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, gearItem).ConfigureAwait(false);
                } finally {
                    BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
                }
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

            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                await GearItem.DeleteGearItemAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, gearItem).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }

            _gearItemCache.Remove(gearItem);
        }

        /// <summary>
        /// Deletes all of the gear items.
        /// </summary>
        public async Task RemoveAllGearItemsAsync()
        {
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                await GearItem.DeleteAllGearItemsAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }

            _gearItemCache.Clear();
        }
#endregion

#region Gear Systems
        public async Task LoadGearSystemsAsync()
        {
            _gearSystemCache.Clear();

            BackpackPlannerState.Instance.Logger.Debug("Loading gear system cache...");
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                var gearSystems = await GearSystem.GetGearSystemsAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection).ConfigureAwait(false);
                foreach(GearSystem gearSystem in gearSystems) {
                    await AddGearSystemAsync(gearSystem).ConfigureAwait(false);
                }
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

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
            if(null != gearSystem) {
                return gearSystem;
            }

            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                gearSystem = await GearSystem.GetGearSystemAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, gearSystemId).ConfigureAwait(false);
                if(null != gearSystem) {
                    await AddGearSystemAsync(gearSystem).ConfigureAwait(false);
                }
                return gearSystem;
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
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
                await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
                try {
                    await GearSystem.SaveGearSystemAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, gearSystem).ConfigureAwait(false);
                } finally {
                    BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
                }
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

            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                await GearSystem.DeleteGearSystemAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, gearSystem).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }

            _gearSystemCache.Remove(gearSystem);
        }

        /// <summary>
        /// Deletes all of the gear systems.
        /// </summary>
        public async Task RemoveAllGearSystemsAsync()
        {
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                await GearSystem.DeleteAllGearSystemsAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }

            _gearSystemCache.Clear();
        }
#endregion

#region Gear Collections
        public async Task LoadGearCollectionsAsync()
        {
            _gearCollectionCache.Clear();

            BackpackPlannerState.Instance.Logger.Debug("Loading gear collection cache...");
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                var gearCollections = await GearCollection.GetGearCollectionsAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection).ConfigureAwait(false);
                foreach(GearCollection gearCollection in gearCollections) {
                    await AddGearCollectionAsync(gearCollection).ConfigureAwait(false);
                }
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

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
            if(null != gearCollection) {
                return gearCollection;
            }

            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                gearCollection = await GearCollection.GetGearCollectionAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, gearCollectionId).ConfigureAwait(false);
                if(null != gearCollection) {
                    await AddGearCollectionAsync(gearCollection).ConfigureAwait(false);
                }
                return gearCollection;
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
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
                await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
                try {
                    await GearCollection.SaveGearCollectionAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, gearCollection).ConfigureAwait(false);
                } finally {
                    BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
                }
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

            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                await GearCollection.DeleteGearCollectionAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, gearCollection).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }

            _gearCollectionCache.Remove(gearCollection);
        }

        /// <summary>
        /// Deletes all of the gear collections.
        /// </summary>
        public async Task RemoveAllGearCollectionsAsync()
        {
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                await GearCollection.DeleteAllGearCollectionsAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }

            _gearCollectionCache.Clear();
        }
#endregion
    }
}
