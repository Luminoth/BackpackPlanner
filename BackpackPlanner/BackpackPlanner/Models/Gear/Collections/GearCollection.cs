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

using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public class GearCollection
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<GearCollection>().ConfigureAwait(false);

            // TODO: move these
            await asyncDbConnection.CreateTableAsync<GearCollectionGearSystem>().ConfigureAwait(false);
            await asyncDbConnection.CreateTableAsync<GearCollectionGearItem>().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the gear collection identifier.
        /// </summary>
        /// <value>
        /// The gear collection identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public int GearCollectionId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the gear collection name.
        /// </summary>
        /// <value>
        /// The gear collection name.
        /// </value>
        [MaxLength(64), NotNull]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the gear systems included in this collection.
        /// </summary>
        /// <value>
        /// The gear systems included in this collection.
        /// </value>
        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public IReadOnlyCollection<GearCollectionGearSystem> GearSystems { get; set; }

        /// <summary>
        /// Gets or sets the gear items included in this collection.
        /// </summary>
        /// <value>
        /// The gear items included in this collection.
        /// </value>
        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public IReadOnlyCollection<GearCollectionGearItem> GearItems { get; set; }

        /// <summary>
        /// Gets or sets the gear collection note.
        /// </summary>
        /// <value>
        /// The gear collection note.
        /// </value>
        [MaxLength(1024)]
        public string Note { get; set; } = string.Empty;

        public override bool Equals(object obj)
        {
            if(GearCollectionId < 1) {
                return false;
            }

            GearCollection gearCollection = obj as GearCollection;
            return GearCollectionId == gearCollection?.GearCollectionId;
        }

        public override int GetHashCode()
        {
            return GearCollectionId.GetHashCode();
        }
    }
}
