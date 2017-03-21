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

using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Settings;

using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Systems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearSystemGearItem : DatabaseIntermediateItem<GearSystem, GearItem>
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="state">The system state.</param>
        public static async Task CreateTablesAsync(BackpackPlannerState state)
        {
            await state.DatabaseState.Connection.AsyncConnection.CreateTableAsync<GearSystemGearItem>().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the gear system identifier.
        /// </summary>
        /// <value>
        /// The gear system identifier.
        /// </value>
        [ForeignKey(typeof(GearSystem))]
        public int GearSystemId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the gear item identifier.
        /// </summary>
        /// <value>
        /// The gear item identifier.
        /// </value>
        [ForeignKey(typeof(GearItem))]
        public int GearItemId { get; set; } = -1;

        public GearSystemGearItem()
        {
        }

        public GearSystemGearItem(GearSystem gearSystem, GearItem gearItem, BackpackPlannerSettings settings)
            : base(gearSystem, gearItem, settings)
        {
        }
    }
}
