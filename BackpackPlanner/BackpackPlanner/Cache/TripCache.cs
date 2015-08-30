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

using System.Diagnostics;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using SQLite.Net;
using SQLite.Net.Async;

namespace EnergonSoftware.BackpackPlanner.Cache
{
    /// <summary>
    /// Caches trip itineraries and trip plans
    /// </summary>
    /// <remarks>
    /// Fow now this is an all or nothing cache. Later on, to conserve resources,
    /// it might start allowing cached items to decay
    /// </remarks>
    public sealed class TripCache
    {
        /// <summary>
        /// Initializes the trip tables in the database.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        /// <param name="oldVersion">The old database version.</param>
        /// <param name="newVersion">The new database version.</param>
        public static async Task InitDatabaseAsync(SQLiteAsyncConnection asyncDbConnection, int oldVersion, int newVersion)
        {
            if(oldVersion >= newVersion) {
                Debug.WriteLine("Database versions match, nothing to do for trip cache update...");
                return;
            }

            if(oldVersion < 2 && newVersion >= 2) {
                Debug.WriteLine("Creating trip cache tables...");
                await TripItinerary.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
                await TripPlan.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            }
        }

#region LoadFromDevice
        /// <summary>
        /// Loads the trip state from the database.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public async Task LoadFromDeviceAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            Debug.WriteLine("Loading trip cache from device...");
        }
#endregion
    }
}
