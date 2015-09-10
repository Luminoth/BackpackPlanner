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

using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

namespace EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TripItinerary
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<TripItinerary>().ConfigureAwait(false);
        }

        public static async Task<List<TripItinerary>> GetTripItinerariesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.GetAllWithChildrenAsync<TripItinerary>().ConfigureAwait(false);
        }

        public static async Task<TripItinerary> GetTripItineraryAsync(SQLiteAsyncConnection asyncDbConnection, int tripItineraryId)
        {
            return await asyncDbConnection.GetWithChildrenAsync<TripItinerary>(tripItineraryId).ConfigureAwait(false);
        }

        public static async Task SaveTripItineraryAsync(SQLiteAsyncConnection asyncDbConnection, TripItinerary tripItinerary)
        {
            if(tripItinerary.TripItineraryId <= 0) {
                await asyncDbConnection.InsertWithChildrenAsync(tripItinerary).ConfigureAwait(false);
            } else {
                await asyncDbConnection.UpdateWithChildrenAsync(tripItinerary).ConfigureAwait(false);
            }
        }

        public static async Task<int> DeleteTripItineraryAsync(SQLiteAsyncConnection asyncDbConnection, TripItinerary tripItinerary)
        {
            return await asyncDbConnection.DeleteAsync(tripItinerary).ConfigureAwait(false);
        }

        public static async Task<int> DeleteAllTripItinerariesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.DeleteAllAsync<TripItinerary>().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the trip itinerary identifier.
        /// </summary>
        /// <value>
        /// The trip itinerary identifier.
        /// </value>
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int TripItineraryId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the trip itinerary name.
        /// </summary>
        /// <value>
        /// The trip itinerary name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the trip itinerary note.
        /// </summary>
        /// <value>
        /// The trip itinerary note.
        /// </value>
        [MaxLength(1024)]
        public string Note { get; set; } = string.Empty;

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<TripPlan> TripPlans { get; set; }

        public override bool Equals(object obj)
        {
            if(TripItineraryId < 1) {
                return false;
            }

            TripItinerary tripItinerary = obj as TripItinerary;
            return TripItineraryId == tripItinerary?.TripItineraryId;
        }

        public override int GetHashCode()
        {
            return TripItineraryId.GetHashCode();
        }
    }
}
