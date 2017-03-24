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

using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Systems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearSystemGearItem : GearItemEntry<GearSystem>
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(GearSystemGearItem));

        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="state">The system state.</param>
        public static async Task CreateTablesAsync(BackpackPlannerState state)
        {
            Logger.Debug("Creating gear system gear item table...");
            await state.DatabaseState.Connection.AsyncConnection.CreateTableAsync<GearSystemGearItem>().ConfigureAwait(false);
        }

        public static async Task<List<GearSystemGearItem>> GetItemsAsync(BackpackPlannerState state, GearSystem gearSystem)
        {
            return await (from x in state.DatabaseState.Connection.AsyncConnection.Table<GearSystemGearItem>() where x.GearSystemId == gearSystem.Id select x).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the gear system identifier.
        /// </summary>
        /// <value>
        /// The gear system identifier.
        /// </value>
        [ForeignKey(typeof(GearSystem))]
        [Indexed(Name="GearSystemGearItemId", Order=1, Unique=true)]
        public int GearSystemId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the gear item identifier.
        /// </summary>
        /// <value>
        /// The gear item identifier.
        /// </value>
        [ForeignKey(typeof(GearItem))]
        [Indexed(Name="GearSystemGearItemId", Order=2, Unique=true)]
        public override int GearItemId { get; set; } = -1;



        public GearSystemGearItem()
        {
        }

        public GearSystemGearItem(GearSystem gearSystem, GearItem gearItem, BackpackPlannerSettings settings)
            : base(gearSystem, gearItem, settings)
        {
        }
    }
}
