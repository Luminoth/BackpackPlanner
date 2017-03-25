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

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Settings;

using SQLite;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearCollectionGearItem : GearItemEntry<GearCollection>
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GearCollectionGearItem));

        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="state">The system state.</param>
        public static async Task CreateTablesAsync(BackpackPlannerState state)
        {
            Logger.Debug("Creating gear collection gear item table...");
            await state.DatabaseState.Connection.AsyncConnection.CreateTableAsync<GearCollectionGearItem>().ConfigureAwait(false);
        }

        public static async Task<List<GearCollectionGearItem>> GetItemsAsync(BackpackPlannerState state, GearCollection gearCollection)
        {
            return await (from x in state.DatabaseState.Connection.AsyncConnection.Table<GearCollectionGearItem>() where x.GearCollectionId == gearCollection.Id select x).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the gear collection identifier.
        /// </summary>
        /// <value>
        /// The gear collection identifier.
        /// </value>
        [Indexed(Name="GearCollectionGearItemId", Order=1, Unique=true)]
        public int GearCollectionId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the gear item identifier.
        /// </summary>
        /// <value>
        /// The gear item identifier.
        /// </value>
        [Indexed(Name="GearCollectionGearItemId", Order=2, Unique=true)]
        public override int GearItemId { get; set; } = -1;

        public GearCollectionGearItem()
        {
        }

        public GearCollectionGearItem(GearCollection gearCollection, GearItem gearItem, BackpackPlannerSettings settings)
            : base(gearCollection, gearItem, settings)
        {
        }
    }
}
