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

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Settings;

using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TripItinerary : DatabaseItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(TripItinerary));

        /// <summary>
        /// Initializes the trip itinerary tables in the database.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <param name="oldVersion">The old database version.</param>
        /// <param name="newVersion">The new database version.</param>
        /// <remarks>
        /// The connection should be thread locked
        /// </remarks>
        public static async Task InitDatabaseAsync(BackpackPlannerState state, int oldVersion, int newVersion)
        {
            ValidateState(state);
            
            if(oldVersion >= newVersion) {
                Logger.Debug("Database versions match, nothing to do for trip itinerary tables...");
                return;
            }

            if(oldVersion < 2 && newVersion >= 2) {
                Logger.Debug("Creating trip itinerary tables...");
                await CreateTablesAsync(state).ConfigureAwait(false);
            }
        }

        private static async Task CreateTablesAsync(BackpackPlannerState state)
        {
            await state.DatabaseState.Connection.AsyncConnection.CreateTableAsync<TripItinerary>().ConfigureAwait(false);
        }

        [Ignore]
        public override int Id { get { return TripItineraryId; } set { TripItineraryId = value; } }

        public override DateTime LastUpdated { get; set; } = DateTime.Now;

        public override bool IsDeleted { get; set; }

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

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<TripPlan> TripPlans { get; set; }

        [Ignore]
        public int TripPlanCount => TripPlans?.Count ?? 0;

        public TripItinerary()
        {
        }

        public TripItinerary(BackpackPlannerSettings settings)
            : base(settings)
        {
        }

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
