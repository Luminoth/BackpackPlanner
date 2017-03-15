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
using System.Diagnostics;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Database;
using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Settings;

using SQLiteNetExtensionsAsync.Extensions;

namespace EnergonSoftware.BackpackPlanner.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DatabaseItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DatabaseItem));

        /// <summary>
        /// Validates the state to ensure it can be used for database operations.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        protected static void ValidateState(BackpackPlannerState state)
        {
            if(null == state) {
                throw new ArgumentNullException(nameof(state));
            }

            if(null == state.DatabaseState) {
                throw new ArgumentNullException(nameof(state.DatabaseState));
            }

            if(null == state.DatabaseState.Connection) {
                throw new ArgumentNullException(nameof(state.DatabaseState.Connection));
            }

            if(null == state.DatabaseState.Connection.AsyncConnection) {
                throw new ArgumentNullException(nameof(state.DatabaseState.Connection.AsyncConnection));
            }

            if(null == state.PlatformPermissionRequestFactory) {
                throw new ArgumentNullException(nameof(state.PlatformPermissionRequestFactory));
            }

            if(null == state.Settings) {
                throw new ArgumentNullException(nameof(state.Settings));
            }

            if(!state.DatabaseState.IsInitializing && !state.DatabaseState.IsInitialized) {
                throw new DatabaseException("Database not initialized!");
            }

            Logger.Info($"Awaiting database initialization: {!state.DatabaseState.IsInitializing}");
            while(state.DatabaseState.IsInitializing) {
                await Task.Delay(1).ConfigureAwait(false);
            }
            Logger.Debug("Database initialized!");
        }

        /// <summary>
        /// Gets all of the not-deleted items from the database.
        /// </summary>
        /// <typeparam name="T">The type of item to get</typeparam>
        /// <param name="state">The system state.</param>
        /// <returns>
        /// All of the not-deleted items from the database
        /// </returns>
        public static async Task<List<T>> GetValidItemsAsync<T>(BackpackPlannerState state) where T: DatabaseItem, new()
        {
            await ValidateState(state).ConfigureAwait(false);

            await state.DatabaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Reading all valid {typeof(T)}s from the database...");

                Stopwatch stopWatch = Stopwatch.StartNew();
                var items = await (from x in state.DatabaseState.Connection.AsyncConnection.Table<T>() where !x.IsDeleted select x).ToListAsync().ConfigureAwait(false);
                foreach(T item in items) {
                    await state.DatabaseState.Connection.AsyncConnection.GetChildrenAsync(item).ConfigureAwait(false);
                    item.Settings = state.Settings;
                }
                stopWatch.Stop();
                Logger.Debug($"Database read took {stopWatch.ElapsedMilliseconds}ms");

                return items;
            } finally {
                state.DatabaseState.Connection.Release();
            }
        }

        /// <summary>
        /// Gets all of the items from the database, including those that have been deleted.
        /// </summary>
        /// <typeparam name="T">The type of item to get</typeparam>
        /// <param name="state">The system state.</param>
        /// <returns>
        /// All of the items from the database
        /// </returns>
        public static async Task<List<T>> GetAllItemsAsync<T>(BackpackPlannerState state) where T: DatabaseItem, new()
        {
            await ValidateState(state).ConfigureAwait(false);

            await state.DatabaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Reading all {typeof(T)}s from the database...");

                Stopwatch stopWatch = Stopwatch.StartNew();
                var items = await state.DatabaseState.Connection.AsyncConnection.GetAllWithChildrenAsync<T>().ConfigureAwait(false);
                foreach(T item in items) {
                    item.Settings = state.Settings;
                }
                stopWatch.Stop();
                Logger.Debug($"Database read took {stopWatch.ElapsedMilliseconds}ms");

                return items;
            } finally {
                state.DatabaseState.Connection.Release();
            }
        }

        /// <summary>
        /// Gets a single item from the database.
        /// </summary>
        /// <typeparam name="T">The type of item to get</typeparam>
        /// <param name="state">The system state.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>
        /// A single item from the database
        /// </returns>
        public static async Task<T> GetItemAsync<T>(BackpackPlannerState state, int itemId) where T: DatabaseItem, new()
        {
            await ValidateState(state).ConfigureAwait(false);

            await state.DatabaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Reading a {typeof(T)} with Id={itemId} from the database...");

                Stopwatch stopWatch = Stopwatch.StartNew();
                T item = await (from x in state.DatabaseState.Connection.AsyncConnection.Table<T>() where !x.IsDeleted && x.Id == itemId select x).FirstOrDefaultAsync().ConfigureAwait(false);
                if(null == item) {
                    return null;
                }

                await state.DatabaseState.Connection.AsyncConnection.GetChildrenAsync(item).ConfigureAwait(false);
                item.Settings = state.Settings;

                stopWatch.Stop();
                Logger.Debug($"Database read took {stopWatch.ElapsedMilliseconds}ms");

                return item;
            } finally {
                state.DatabaseState.Connection.Release();
            }
        }

        /// <summary>
        /// Inserts a list of items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="state">The system state.</param>
        /// <param name="items">The items.</param>
        public static async Task InsertItemsAsync<T>(BackpackPlannerState state, List<T> items) where T: DatabaseItem
        {
            await ValidateState(state).ConfigureAwait(false);

            await state.DatabaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                foreach(T item in items) {
                    item.LastUpdated = DateTime.Now;
                }

                Logger.Debug($"Inserting {items.Count} new {typeof(T)}s into the database...");
                await state.DatabaseState.Connection.AsyncConnection.InsertAllWithChildrenAsync(items).ConfigureAwait(false);
            } finally {
                state.DatabaseState.Connection.Release();
            }
        }

        // TODO: no UpdateAllWithChildrenAsync() method?
        /*public static async Task UpdateItemsAsync<T>(BackpackPlannerState state, List<T> items) where T: DatabaseItem
        {
            await ValidateState(state).ConfigureAwait(false);

            await state.DatabaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                foreach(T item in items) {
                    item.LastUpdated = DateTime.Now;
                }

                Logger.Debug($"Updating {items.Count} new {typeof(T)}s into the database...");
                await state.DatabaseState.Connection.AsyncConnection.UpdateAllWithChildrenAsync(items).ConfigureAwait(false);
            } finally {
                state.DatabaseState.Connection.Release();
            }
        }*/

        /// <summary>
        /// Saves an item in the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to save</typeparam>
        /// <param name="state">The system state.</param>
        /// <param name="item">The item.</param>
        public static async Task SaveItemAsync<T>(BackpackPlannerState state, T item) where T: DatabaseItem
        {
            await ValidateState(state).ConfigureAwait(false);

            await state.DatabaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                item.LastUpdated = DateTime.Now;

                if(item.Id <= 0) {
                    Logger.Debug($"Inserting a new {typeof(T)} into the database...");
                    await state.DatabaseState.Connection.AsyncConnection.InsertWithChildrenAsync(item).ConfigureAwait(false);
                    Logger.Debug($"The inserted {typeof(T)}'s Id={item.Id}");
                } else {
                    Logger.Debug($"Updating the {typeof(T)} with Id={item.Id} in the database...");
                    await state.DatabaseState.Connection.AsyncConnection.UpdateWithChildrenAsync(item).ConfigureAwait(false);
                }
            } finally {
                state.DatabaseState.Connection.Release();
            }
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to delete</typeparam>
        /// <param name="state">The system state.</param>
        /// <returns>The number of items deleted?</returns>
        /*public static async Task<int> DeleteAllItemsAsync<T>(BackpackPlannerState state) where T: DatabaseItem
        {
            await ValidateState(state).ConfigureAwait(false);

            await state.DatabaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Deleting all {typeof(T)}s from the database...");
                return await state.DatabaseState.Connection.AsyncConnection.DeleteAllAsync<T>().ConfigureAwait(false);
            } finally {
                state.DatabaseState.Connection.Release();
            }
        }*/

        /// <summary>
        /// Gets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        public abstract int Id { get; set; }

        /// <summary>
        /// Gets or sets the last update timestamp.
        /// </summary>
        /// <value>
        /// The last update timestamp.
        /// </value>
        public abstract DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this item is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this item is deleted; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsDeleted { get; set; }

        /// <summary>
        /// Gets the planner settings.
        /// </summary>
        /// <value>
        /// The planner settings.
        /// </value>
        protected BackpackPlannerSettings Settings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseItem"/> class.
        /// </summary>
        /// <param name="settings">The planner settings.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        protected DatabaseItem(BackpackPlannerSettings settings)
        {
            if(null == settings) {
                throw new ArgumentNullException(nameof(settings));
            }

            Settings = settings;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseItem"/> class.
        /// </summary>
        protected DatabaseItem()
        {
        }
    }
}
