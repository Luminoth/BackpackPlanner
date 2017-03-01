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
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;

using SQLite.Net.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

namespace EnergonSoftware.BackpackPlanner.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DatabaseVersion
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DatabaseVersion));

        private static void ValidateState(BackpackPlannerState state)
        {
            if(null == state) {
                throw new ArgumentNullException(nameof(state));
            }

            if(null == state.DatabaseState) {
                throw new ArgumentNullException(nameof(state.DatabaseState));
            }

            if(null == state.DatabaseState.Connection) {
                throw new ArgumentNullException(nameof(state.DatabaseState.Connection));
            }

            if(null == state.DatabaseState.Connection.AsyncConnection) {
                throw new ArgumentNullException(nameof(state.DatabaseState.Connection.AsyncConnection));
            }

            if(null == state.PlatformPermissionRequestFactory) {
                throw new ArgumentNullException(nameof(state.PlatformPermissionRequestFactory));
            }
        }

        /// <summary>
        /// Creates the database tables.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <remarks>
        /// This will insert the initial version entry.
        /// </remarks>
        public static async Task CreateTablesAsync(BackpackPlannerState state)
        {
            ValidateState(state);

            await state.DatabaseState.Connection.AsyncConnection.CreateTableAsync<DatabaseVersion>().ConfigureAwait(false);

            // sets the version to -1 (we haven't built the database yet!)
            await state.DatabaseState.Connection.AsyncConnection.InsertAsync(new DatabaseVersion()).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the database version entry.
        /// </summary>
        /// <param name="state">The system state.</param>
        public static async Task<DatabaseVersion> GetAsync(BackpackPlannerState state)
        {
            ValidateState(state);

            return await state.DatabaseState.Connection.AsyncConnection.Table<DatabaseVersion>().FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the database version.
        /// </summary>
        /// <param name="state">The system state.</param>
        /// <param name="databaseVersion">The current database version.</param>
        public static async Task UpdateAsync(BackpackPlannerState state, DatabaseVersion databaseVersion)
        {
            ValidateState(state);

            Logger.Debug($"Updating database version to {databaseVersion.Version}...");
            await state.DatabaseState.Connection.AsyncConnection.UpdateWithChildrenAsync(databaseVersion).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets or sets the database version identifier.
        /// </summary>
        /// <value>
        /// The database version identifier.
        /// </value>
        [PrimaryKey, AutoIncrement, Column("_id")]
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
