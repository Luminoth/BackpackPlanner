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
        /// Gets all of the items from the database.
        /// </summary>
        /// <typeparam name="T">The type of item to get</typeparam>
        /// <param name="databaseState">State of the database.</param>
        /// <param name="settings">The planner settings.</param>
        /// <returns>
        /// All of the items from the database
        /// </returns>
        public static async Task<List<T>> GetItemsAsync<T>(DatabaseState databaseState, BackpackPlannerSettings settings) where T: DatabaseItem, new()
        {
            if(null == databaseState) {
                throw new ArgumentNullException(nameof(databaseState));
            }

            if(null == settings) {
                throw new ArgumentNullException(nameof(settings));
            }

            await databaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Reading all {typeof(T)}s from the database...");

                Stopwatch stopWatch = Stopwatch.StartNew();
                var items = await databaseState.Connection.AsyncConnection.GetAllWithChildrenAsync<T>().ConfigureAwait(false);
                foreach(T item in items) {
                    item.Settings = settings;
                }
                stopWatch.Stop();
                Logger.Debug($"Database read took {stopWatch.ElapsedMilliseconds}ms");

                return items;
            } finally {
                databaseState.Connection.Release();
            }
        }

        /// <summary>
        /// Gets a single item from the database.
        /// </summary>
        /// <typeparam name="T">The type of item to get</typeparam>
        /// <param name="databaseState">State of the database.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>
        /// A single item from the database
        /// </returns>
        public static async Task<T> GetItemAsync<T>(DatabaseState databaseState, BackpackPlannerSettings settings, int itemId) where T: DatabaseItem, new()
        {
            if(null == databaseState) {
                throw new ArgumentNullException(nameof(databaseState));
            }

            if(null == settings) {
                throw new ArgumentNullException(nameof(settings));
            }

            await databaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Reading a {typeof(T)} with Id={itemId} from the database...");

                Stopwatch stopWatch = Stopwatch.StartNew();
                T item = await databaseState.Connection.AsyncConnection.GetWithChildrenAsync<T>(itemId).ConfigureAwait(false);
                item.Settings = settings;
                stopWatch.Stop();
                Logger.Debug($"Database read took {stopWatch.ElapsedMilliseconds}ms");

                return item;
            } finally {
                databaseState.Connection.Release();
            }
        }

        /// <summary>
        /// Inserts a list of items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="databaseState">State of the database.</param>
        /// <param name="items">The items.</param>
        public static async Task InsertItemsAsync<T>(DatabaseState databaseState, List<T> items) where T: DatabaseItem
        {
            if(null == databaseState) {
                throw new ArgumentNullException(nameof(databaseState));
            }

            await databaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Inserting {items.Count} new {typeof(T)}s into the database...");
                await databaseState.Connection.AsyncConnection.InsertAllWithChildrenAsync(items).ConfigureAwait(false);
            } finally {
                databaseState.Connection.Release();
            }
        }

        // TODO: no UpdateAllWithChildrenAsync() method?
        /*public static async Task UpdateItemsAsync<T>(DatabaseState databaseState, List<T> items) where T: DatabaseItem
        {
            if(null == databaseState) {
                throw new ArgumentNullException(nameof(databaseState));
            }

            await databaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Updating {items.Count} new {typeof(T)}s into the database...");
                await databaseState.Connection.AsyncConnection.UpdateAllWithChildrenAsync(items).ConfigureAwait(false);
            } finally {
                databaseState.Connection.Release();
            }
        }*/

        /// <summary>
        /// Saves an item in the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to save</typeparam>
        /// <param name="databaseState">State of the database.</param>
        /// <param name="item">The item.</param>
        public static async Task SaveItemAsync<T>(DatabaseState databaseState, T item) where T: DatabaseItem
        {
            if(null == databaseState) {
                throw new ArgumentNullException(nameof(databaseState));
            }

            await databaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                if(item.Id <= 0) {
                    Logger.Debug($"Inserting a new {typeof(T)} into the database...");
                    await databaseState.Connection.AsyncConnection.InsertWithChildrenAsync(item).ConfigureAwait(false);
                    Logger.Debug($"The inserted {typeof(T)}'s Id={item.Id}");
                } else {
                    Logger.Debug($"Updating the {typeof(T)} with Id={item.Id} in the database...");
                    await databaseState.Connection.AsyncConnection.UpdateWithChildrenAsync(item).ConfigureAwait(false);
                }
            } finally {
                databaseState.Connection.Release();
            }
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to delete</typeparam>
        /// <param name="databaseState">State of the database.</param>
        /// <param name="item">The item.</param>
        /// <returns>The number of items deleted?</returns>
        public static async Task<int> DeleteItemAsync<T>(DatabaseState databaseState, T item) where T: DatabaseItem
        {
            if(null == databaseState) {
                throw new ArgumentNullException(nameof(databaseState));
            }

            await databaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Deleting the {typeof(T)} with Id={item.Id} from the database...");
                return await databaseState.Connection.AsyncConnection.DeleteAsync(item).ConfigureAwait(false);
            } finally {
                databaseState.Connection.Release();
            }
        }

        /// <summary>
        /// Deletes all items from the database.
        /// </summary>
        /// <typeparam name="T">The type of item to delete</typeparam>
        /// <param name="databaseState">State of the database.</param>
        /// <returns>The number of items deleted?</returns>
        public static async Task<int> DeleteAllItemsAsync<T>(DatabaseState databaseState) where T: DatabaseItem
        {
            if(null == databaseState) {
                throw new ArgumentNullException(nameof(databaseState));
            }

            await databaseState.Connection.LockAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Deleting all {typeof(T)}s from the database...");
                return await databaseState.Connection.AsyncConnection.DeleteAllAsync<T>().ConfigureAwait(false);
            } finally {
                databaseState.Connection.Release();
            }
        }

        /// <summary>
        /// Gets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        public abstract int Id { get; set; }

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
