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

using EnergonSoftware.BackpackPlanner.Logging;

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
        /// <returns>All of the items from the database</returns>
        public static async Task<List<T>> GetItemsAsync<T>() where T: DatabaseItem
        {
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Reading all {typeof(T)}s from the database...");
                return await BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection.GetAllWithChildrenAsync<T>().ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

        /// <summary>
        /// Gets a single item from the database.
        /// </summary>
        /// <typeparam name="T">The type of item to get</typeparam>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>A single item from the database</returns>
        public static async Task<T> GetItemAsync<T>(int itemId) where T: DatabaseItem
        {
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Reading a {typeof(T)} with Id={itemId} from the database...");
                return await BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection.GetWithChildrenAsync<T>(itemId).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

        /// <summary>
        /// Saves an item in the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to save</typeparam>
        /// <param name="item">The item.</param>
        public static async Task SaveItemAsync<T>(T item) where T: DatabaseItem
        {
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                if(item.Id <= 0) {
                    Logger.Debug($"Inserting a new {typeof(T)} into the database...");
                    await BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection.InsertWithChildrenAsync(item).ConfigureAwait(false);
                    Logger.Debug($"The inserted {typeof(T)}'s Id={item.Id}");
                } else {
                    Logger.Debug($"Updating the {typeof(T)} with Id={item.Id} in the database...");
                    await BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection.UpdateWithChildrenAsync(item).ConfigureAwait(false);
                }
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <typeparam name="T">The type of the item to delete</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>The number of items deleted?</returns>
        public static async Task<int> DeleteItemAsync<T>(T item) where T: DatabaseItem
        {
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Deleting the {typeof(T)} with Id={item.Id} from the database...");
                return await BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection.DeleteAsync(item).ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

        /// <summary>
        /// Deletes all items from the database.
        /// </summary>
        /// <typeparam name="T">The type of item to delete</typeparam>
        /// <returns>The number of items deleted?</returns>
        public static async Task<int> DeleteAllItemsAsync<T>() where T: DatabaseItem
        {
            await BackpackPlannerState.Instance.DatabaseConnection.Lock.WaitAsync().ConfigureAwait(false);
            try {
                Logger.Debug($"Deleting all {typeof(T)}s from the database...");
                return await BackpackPlannerState.Instance.DatabaseConnection.AsyncConnection.DeleteAllAsync<T>().ConfigureAwait(false);
            } finally {
                BackpackPlannerState.Instance.DatabaseConnection.Lock.Release();
            }
        }

        /// <summary>
        /// Gets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        public abstract int Id { get; set; }
    }
}
