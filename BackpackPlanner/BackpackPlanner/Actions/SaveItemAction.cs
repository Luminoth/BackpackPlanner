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

using EnergonSoftware.BackpackPlanner.Core.Database;
using EnergonSoftware.BackpackPlanner.Models;
using EnergonSoftware.BackpackPlanner.Settings;

namespace EnergonSoftware.BackpackPlanner.Actions
{
    /// <summary>
    /// Saves an item.
    /// </summary>
    public class SaveItemAction<T> : Action where T: DatabaseItem
    {
        /// <summary>
        /// Gets the item to save.
        /// </summary>
        /// <value>
        /// The item to save.
        /// </value>
        public T Item { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveItemAction{T}"/> class.
        /// </summary>
        /// <param name="item">The item to save.</param>
        public SaveItemAction(T item)
        {
            if(null == item) {
                throw new ArgumentNullException(nameof(item));
            }

            Item = item;
        } 

        public async override Task DoActionAsync(DatabaseState databaseState, BackpackPlannerSettings settings)
        {
            await ValidateDatabaseStateAsync(databaseState).ConfigureAwait(false);

            await DatabaseItem.SaveItemAsync(databaseState, Item).ConfigureAwait(false);
        }
    }
}
