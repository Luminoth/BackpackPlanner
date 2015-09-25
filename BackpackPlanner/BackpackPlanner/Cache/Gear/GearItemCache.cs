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

using EnergonSoftware.BackpackPlanner.Logging;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;

using SQLite.Net.Async;

namespace EnergonSoftware.BackpackPlanner.Cache.Gear
{
    /// <summary>
    /// Caches gear items.
    /// </summary>
    public sealed class GearItemCache : ItemCache<GearItem>
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GearItemCache));

        /// <summary>
        /// Initializes the gear item tables in the database.
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
                Logger.Debug("Database versions match, nothing to do for gear item tables...");
                return;
            }

            if(oldVersion < 1 && newVersion >= 1) {
                Logger.Debug("Creating gear item tables...");
                await GearItem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            }
        }

        protected async override Task<List<GearItem>> GetItemsAsync(SQLiteAsyncConnection dbConnection)
        {
            return await GearItem.GetGearItemsAsync(dbConnection).ConfigureAwait(false);
        }

        protected async override Task<GearItem> GetItemAsync(SQLiteAsyncConnection dbConnection, int gearItemId)
        {
            return await GearItem.GetGearItemAsync(dbConnection, gearItemId).ConfigureAwait(false);
        }

        protected async override Task SaveItemAsync(SQLiteAsyncConnection dbConnection, GearItem gearItem)
        {
            await GearItem.SaveGearItemAsync(dbConnection, gearItem).ConfigureAwait(false);
        }

        protected async override Task DeleteItemAsync(SQLiteAsyncConnection dbConnection, GearItem gearItem)
        {
            await GearItem.DeleteGearItemAsync(dbConnection, gearItem).ConfigureAwait(false);
        }

        protected async override Task DeleteAllItemsAsync(SQLiteAsyncConnection dbConnection)
        {
            await GearItem.DeleteAllGearItemsAsync(dbConnection).ConfigureAwait(false);
        }
    }
}
