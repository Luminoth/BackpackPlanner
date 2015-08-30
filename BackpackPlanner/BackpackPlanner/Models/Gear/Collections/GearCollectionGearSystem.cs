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

using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GearCollectionGearSystem
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<GearCollectionGearSystem>().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the gear collection identifier.
        /// </summary>
        /// <value>
        /// The gear collection identifier.
        /// </value>
        [ForeignKey(typeof(GearCollection))]
        public int GearCollectionId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the gear system identifier.
        /// </summary>
        /// <value>
        /// The gear system identifier.
        /// </value>
        [ForeignKey(typeof(GearSystem))]
        public int GearSystemId { get; set; } = -1;

        private int _amount = 1;

        /// <summary>
        /// Gets or sets the amount of the gear system in the gear collection.
        /// </summary>
        /// <value>
        /// The amount of the gear system in the gear collection.
        /// </value>
        [NotNull]
        public int Amount
        {
            get { return _amount; }
            set { _amount = value < 1 ? 1 : value; }
        }
    }
}
