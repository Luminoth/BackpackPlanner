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

using SQLite.Net.Async;
using SQLite.Net.Attributes;

namespace EnergonSoftware.BackpackPlanner.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DatabaseVersion
    {
        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task CreateTablesAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            await asyncDbConnection.CreateTableAsync<DatabaseVersion>().ConfigureAwait(false);

            await asyncDbConnection.InsertAsync(new DatabaseVersion { Version = BackpackPlannerState.CurrentDatabaseVersion }).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the database version entry.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        public static async Task<DatabaseVersion> GetAsync(SQLiteAsyncConnection asyncDbConnection)
        {
            return await asyncDbConnection.Table<DatabaseVersion>().FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the database version.
        /// </summary>
        /// <param name="asyncDbConnection">The asynchronous database connection.</param>
        /// <param name="databaseVersion">The current database version.</param>
        public static async Task<int> UpdateAsync(SQLiteAsyncConnection asyncDbConnection, DatabaseVersion databaseVersion)
        {
            return await asyncDbConnection.UpdateAsync(databaseVersion).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the database version identifier.
        /// </summary>
        /// <value>
        /// The database version identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public int DatabaseVersionId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the database version.
        /// </summary>
        /// <value>
        /// The databaseversion.
        /// </value>
        public int Version { get; set; } = -1;
    }
}
