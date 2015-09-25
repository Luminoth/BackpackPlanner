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
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using SQLite.Net.Async;

namespace EnergonSoftware.BackpackPlanner.Cache.Trips
{
    /// <summary>
    /// Caches trip plans
    /// </summary>
    /// <remarks>
    /// Fow now this is an all or nothing cache. Later on, to conserve resources,
    /// it might start allowing cached items to decay
    /// </remarks>
    public sealed class TripPlanCache : ItemCache<TripPlan>
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(TripPlanCache));

        /// <summary>
        /// Initializes the trip plans tables in the database.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        /// <param name="oldVersion">The old database version.</param>
        /// <param name="newVersion">The new database version.</param>
        /// <remarks>
        /// The connection should be thread locked
        /// </remarks>
        public static async Task InitDatabaseAsync(SQLiteAsyncConnection asyncDbConnection, int oldVersion, int newVersion)
        {
            if(null == asyncDbConnection) {
                throw new ArgumentNullException(nameof(asyncDbConnection));
            }
            
            if(oldVersion >= newVersion) {
                Logger.Debug("Database versions match, nothing to do for trip plan tables...");
                return;
            }

            if(oldVersion < 2 && newVersion >= 2) {
                Logger.Debug("Creating trip plan tables...");
                await TripPlan.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            }
        }

        protected async override Task<List<TripPlan>> GetItemsAsync(SQLiteAsyncConnection dbConnection)
        {
            return await TripPlan.GetTripPlansAsync(dbConnection).ConfigureAwait(false);
        }

        protected async override Task<TripPlan> GetItemAsync(SQLiteAsyncConnection dbConnection, int tripPlanId)
        {
            return await TripPlan.GetTripPlanAsync(dbConnection, tripPlanId).ConfigureAwait(false);
        }

        protected async override Task SaveItemAsync(SQLiteAsyncConnection dbConnection, TripPlan tripPlan)
        {
            await TripPlan.SaveTripPlanAsync(dbConnection, tripPlan).ConfigureAwait(false);
        }

        protected async override Task DeleteItemAsync(SQLiteAsyncConnection dbConnection, TripPlan tripPlan)
        {
            await TripPlan.DeleteTripPlanAsync(dbConnection, tripPlan).ConfigureAwait(false);
        }

        protected async override Task DeleteAllItemsAsync(SQLiteAsyncConnection dbConnection)
        {
            await TripPlan.DeleteAllTripPlansAsync(dbConnection).ConfigureAwait(false);
        }
    }
}
