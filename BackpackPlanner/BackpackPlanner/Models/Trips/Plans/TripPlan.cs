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

using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Models.Meals;
using EnergonSoftware.BackpackPlanner.Models.Trips.Itineraries;

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Trips.Plans
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TripPlan
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<TripPlan>().ConfigureAwait(false);

            await TripPlanGearCollection.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            await TripPlanGearSystem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            await TripPlanGearItem.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
            await TripPlanMeal.CreateTablesAsync(asyncDbConnection).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the trip plan identifier.
        /// </summary>
        /// <value>
        /// The trip plan identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public int TripPlanId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the trip plan name.
        /// </summary>
        /// <value>
        /// The trip plan name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the trip plan.
        /// </summary>
        /// <value>
        /// The start date of the trip plan.
        /// </value>
        public DateTime StartDate { get; set; } = new DateTime();

        /// <summary>
        /// Gets or sets the end date of the trip plan.
        /// </summary>
        /// <value>
        /// The end date of the trip plan.
        /// </value>
        public DateTime EndDate { get; set; } = new DateTime();

        /// <summary>
        /// Gets or sets the trip itinerary identifier.
        /// </summary>
        /// <value>
        /// The trip itinerary identifier.
        /// </value>
        [ForeignKey(typeof(TripItinerary))]
        public int TripItineraryId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the trip itinerary.
        /// </summary>
        /// <value>
        /// The trip itinerary.
        /// </value>
        [ManyToOne]
        public TripItinerary TripItinerary { get; set; }

        /// <summary>
        /// Gets or sets the gear collections contained in this plan.
        /// </summary>
        /// <value>
        /// The gear collections contained in this plan.
        /// </value>
        [ManyToMany(typeof(TripPlanGearCollection))]
        public List<GearCollection> GearCollections { get; set; }

        /// <summary>
        /// Gets or sets the gear systems contained in this plan.
        /// </summary>
        /// <value>
        /// The gear systems contained in this plan.
        /// </value>
        [ManyToMany(typeof(TripPlanGearSystem))]
        public List<GearSystem> GearSystems { get; set; }

        /// <summary>
        /// Gets or sets the gear items contained in this plan.
        /// </summary>
        /// <value>
        /// The gear items contained in this plan.
        /// </value>
        [ManyToMany(typeof(TripPlanGearItem))]
        public List<GearItem> GearItems { get; set; }

        /// <summary>
        /// Gets or sets the meals contained in this plan.
        /// </summary>
        /// <value>
        /// The meals contained in this plan.
        /// </value>
        [ManyToMany(typeof(TripPlanMeal))]
        public List<Meal> Meals { get; set; }

        /// <summary>
        /// Gets or sets the trip plan note.
        /// </summary>
        /// <value>
        /// The trip plan note.
        /// </value>
        [MaxLength(1024)]
        public string Note { get; set; } = string.Empty;

        public override bool Equals(object obj)
        {
            if(TripPlanId < 1) {
                return false;
            }

            TripPlan tripPlan = obj as TripPlan;
            return TripPlanId == tripPlan?.TripPlanId;
        }

        public override int GetHashCode()
        {
            return TripPlanId.GetHashCode();
        }
    }
}
