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

using EnergonSoftware.BackpackPlanner.Logging;
using EnergonSoftware.BackpackPlanner.Models;

using SQLite.Net.Async;

namespace EnergonSoftware.BackpackPlanner.Cache
{
    /// <summary>
    /// Caches database items.
    /// </summary>
    /// <remarks>
    /// Fow now this is an all or nothing cache. Later on, to conserve resources,
    /// it might start allowing cached items to decay
    /// </remarks>
    public abstract class ItemCache<T> where T: IItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(ItemCache<T>));

        private readonly HashSet<T> _itemCache = new HashSet<T>();

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <value>
        /// The item count.
        /// </value>
        public int ItemCount => _itemCache.Count;

        /// <summary>
        /// Loads the items into the cache.
        /// </summary>
        public async Task LoadItemsAsync()
        {
            _itemCache.Clear();

            Logger.Debug($"Loading item cache for type {typeof(T)}...");
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                var items = await GetItemsAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection).ConfigureAwait(false);
                foreach(T item in items) {
                    await AddItemAsync(item).ConfigureAwait(false);
                }
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

        /// <summary>
        /// Gets an item by identifier.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>The first item with the given identifier or null if it isn't in the container</returns>
        public async Task<T> GetItemByIdAsync(int itemId)
        {
            if(itemId < 1) {
                throw new ArgumentException("ItemId cannot be less than 1", nameof(itemId));
            }

            T item = _itemCache.FirstOrDefault(x => x.Id == itemId);
            if(null != item) {
                return item;
            }

            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                item = await GetItemAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, itemId).ConfigureAwait(false);
                if(null != item) {
                    await AddItemAsync(item).ConfigureAwait(false);
                }
                return item;
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

        /// <summary>
        /// Adds the item to the container.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>The id of the newly added item or -1 if it couldn't be added</returns>
        public async Task<int> AddItemAsync(T item)
        {
            if(null == item) {
                throw new ArgumentNullException(nameof(item));
            }

            if(item.Id < 1) {
                await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
                try {
                    await SaveItemAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, item).ConfigureAwait(false);
                } finally {
                    BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
                }
            }

            return item.Id < 1 ? -1 : (_itemCache.Add(item) ? item.Id : -1);
        }

        /// <summary>
        /// Deletes the item from the container.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        public async Task RemoveItemAsync(T item)
        {
            if(null == item) {
                throw new ArgumentNullException(nameof(item));
            }

            if(item.Id < 1) {
                throw new ArgumentException("ItemId cannot be less than 1!", nameof(item));
            }

            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                await DeleteItemAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection, item).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }

            _itemCache.Remove(item);
        }

        /// <summary>
        /// Deletes all of the items.
        /// </summary>
        public async Task RemoveAllItemsAsync()
        {
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                await DeleteAllItemsAsync(BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }

            _itemCache.Clear();
        }

        /// <summary>
        /// Gets the items from the database.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        protected abstract Task<List<T>> GetItemsAsync(SQLiteAsyncConnection dbConnection);

        /// <summary>
        /// Gets an item from the database.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>The item or null if it couldn't be found.</returns>
        protected abstract Task<T> GetItemAsync(SQLiteAsyncConnection dbConnection, int itemId);

        /// <summary>
        /// Saves the item in the database.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="item">The item.</param>
        protected abstract Task SaveItemAsync(SQLiteAsyncConnection dbConnection, T item);

        /// <summary>
        /// Deletes the item from the database.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="item">The item.</param>
        protected abstract Task DeleteItemAsync(SQLiteAsyncConnection dbConnection, T item);

        /// <summary>
        /// Deletes all of the items from the database.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        protected abstract Task DeleteAllItemsAsync(SQLiteAsyncConnection dbConnection);
    }
}
