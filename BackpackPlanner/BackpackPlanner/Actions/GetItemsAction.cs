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

using EnergonSoftware.BackpackPlanner.Core.Database;
using EnergonSoftware.BackpackPlanner.Models;
using EnergonSoftware.BackpackPlanner.Settings;

namespace EnergonSoftware.BackpackPlanner.Actions
{
    /// <summary>
    /// Gets all items.
    /// </summary>
    public class GetItemsAction<T> : Action where T: DatabaseItem, new()
    {
        /// <summary>
        /// Gets or sets the item to add.
        /// </summary>
        /// <value>
        /// The item to add.
        /// </value>
        public List<T> Items { get; private set; } = new List<T>();

        public async override Task DoActionAsync(DatabaseState databaseState, BackpackPlannerSettings settings)
        {
            await ValidateDatabaseStateAsync(databaseState).ConfigureAwait(false);

            Items = await DatabaseItem.GetItemsAsync<T>(databaseState, settings).ConfigureAwait(false);
        }
    }
}
