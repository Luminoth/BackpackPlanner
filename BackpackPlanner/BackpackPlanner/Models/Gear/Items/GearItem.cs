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

using System.Collections.Generic;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Items
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearItem
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<GearItem>().ConfigureAwait(false);
        }

        public static async Task<List<GearItem>>  GetGearItemsAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.Table<GearItem>().ToListAsync().ConfigureAwait(false);
        }

        public static async Task<GearItem> GetGearItemAsync(SQLiteAsyncConnection asyncDbConnection, int gearItemId)
        {
            return await asyncDbConnection.GetAsync<GearItem>(gearItemId).ConfigureAwait(false);
        }

        public static async Task<int> SaveGearItemAsync(SQLiteAsyncConnection asyncDbConnection, GearItem gearItem)
        {
            return gearItem.GearItemId <= 0
                ? await asyncDbConnection.InsertAsync(gearItem).ConfigureAwait(false)
                : await asyncDbConnection.UpdateAsync(gearItem).ConfigureAwait(false);
        }

        public static async Task<int> DeleteGearItemAsync(SQLiteAsyncConnection asyncDbConnection, GearItem gearItem)
        {
            return await asyncDbConnection.DeleteAsync(gearItem).ConfigureAwait(false);
        }

        public static async Task<int> DeleteAllGearItemsAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.DeleteAllAsync<GearItem>().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the gear item identifier.
        /// </summary>
        /// <value>
        /// The gear item identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public int GearItemId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the gear item name.
        /// </summary>
        /// <value>
        /// The gear item name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the gear item make.
        /// </summary>
        /// <value>
        /// The gear item make.
        /// </value>
        [MaxLength(32)]
        public string Make { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the gear item model.
        /// </summary>
        /// <value>
        /// The gear item model.
        /// </value>
        [MaxLength(32)]
        public string Model { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the gear item url.
        /// </summary>
        /// <value>
        /// The gear item url.
        /// </value>
        [MaxLength(2048)]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the carried-ness of this gear item.
        /// </summary>
        /// <value>
        /// The carried-ness of this gear item.
        /// </value>
        [NotNull]
        public GearCarried Carried  { get; set; } = GearCarried.Carried;

        /// <summary>
        /// Gets or sets whether this gear item is consumable or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this gear item is consumable; otherwise, <c>false</c>.
        /// </value>
        [NotNull]
        public bool IsConsumable { get; set; } = false;

        private int _consumedPerDay = 1;

        /// <summary>
        /// Gets or sets the amount of this gear item consumed per day, if consumable.
        /// </summary>
        /// <value>
        /// The amount of this gear item consumed per day, if consumable.
        /// </value>
        [NotNull]
        public int ConsumedPerDay
        {
            get { return _consumedPerDay; }
            set { _consumedPerDay = value < 1 ? 1 : value; }
        }

        private int _weightInGrams;

        /// <summary>
        /// Gets or sets the weight of this gear item in grams.
        /// </summary>
        /// <value>
        /// The weight of this gear item in grams.
        /// </value>
        [NotNull]
        public int WeightInGrams
        {
            get { return _weightInGrams; }
            set { _weightInGrams = value < 0 ? 0 : value; }
        }

        // ReSharper disable once InconsistentNaming
        private int _costInUSDP;

        /// <summary>
        /// Gets or sets the cost of this gear item in US pennies.
        /// </summary>
        /// <value>
        /// The cost of this gear item in US pennies.
        /// </value>
        // ReSharper disable once InconsistentNaming
        [NotNull]
        public int CostInUSDP
        {
            get { return _costInUSDP; }
            set { _costInUSDP = value < 0 ? 0 : value; }
        }

        /// <summary>
        /// Gets or sets the gear item note.
        /// </summary>
        /// <value>
        /// The gear item note.
        /// </value>
        [MaxLength(1024)]
        public string Note { get; set; } = string.Empty;

        [ManyToMany(typeof(GearSystemGearItem))]
        public List<GearSystem> GearSystems { get; set; }

        [ManyToMany(typeof(GearCollectionGearItem))]
        public List<GearCollection> GearCollections { get; set; }

        [ManyToMany(typeof(TripPlanGearItem))]
        public List<TripPlan> TripPlans { get; set; }

        public override bool Equals(object obj)
        {
            if(GearItemId < 1) {
                return false;
            }

            GearItem gearItem = obj as GearItem;
            return GearItemId == gearItem?.GearItemId;
        }

        public override int GetHashCode()
        {
            return GearItemId.GetHashCode();
        }
    }
}