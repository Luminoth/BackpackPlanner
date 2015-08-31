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

        private readonly HashSet<TripItinerary> _tripItineraryCache = new HashSet<TripItinerary>();
        private readonly HashSet<TripPlan> _tripPlanCache = new HashSet<TripPlan>();

        /// <summary>
        /// Gets the trip itinerary count.
        /// </summary>
        /// <value>
        /// The trip itinerary count.
        /// </value>
        public int TripItineraryCount => _tripItineraryCache.Count;

        /// <summary>
        /// Gets the trip plan count.
        /// </summary>
        /// <value>
        /// The trip plan count.
        /// </value>
        public int TripPlanCount => _tripPlanCache.Count;

#region Trip Itineraries
        public async Task LoadTripItinerariesAsync()
        {
            _tripItineraryCache.Clear();

            Debug.WriteLine("Loading trip itinerary cache...");
            using(SQLiteConnectionWithLock dbConnection = BackpackPlannerState.Instance.GetDatabaseConnection()) {
                SQLiteAsyncConnection asyncDbConnection = new SQLiteAsyncConnection(() => dbConnection);

                var tripItineraries = await TripItinerary.GetTripItinerariesAsync(asyncDbConnection).ConfigureAwait(false);
                foreach(TripItinerary tripItinerary in tripItineraries) {
                    //await AddTripItineraryAsync(tripItinerary).ConfigureAwait(false);
                }
            }
        }
#endregion

#region Trip Plans
        public async Task LoadTripPlansAsync()
        {
            _tripPlanCache.Clear();

            Debug.WriteLine("Loading trip plan cache...");
            using(SQLiteConnectionWithLock dbConnection = BackpackPlannerState.Instance.GetDatabaseConnection()) {
                SQLiteAsyncConnection asyncDbConnection = new SQLiteAsyncConnection(() => dbConnection);

                var tripPlans = await TripPlan.GetTripPlansAsync(asyncDbConnection).ConfigureAwait(false);
                foreach(TripPlan tripPlan in tripPlans) {
                    //await AddTripPlanAsync(tripPlan).ConfigureAwait(false);
                }
            }
        }
#endregion
    }
}
