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

namespace EnergonSoftware.BackpackPlanner.Commands
{
    /// <summary>
    /// Deletes an item.
    /// </summary>
    public class DeleteItemCommand<T> : Command where T: DatabaseItem
    {
        /// <summary>
        /// Gets the item to delete.
        /// </summary>
        /// <value>
        /// The item to delete.
        /// </value>
        public T Item { get; }

        /// <summary>
        /// Gets a value indicating whether the item was deleted or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the item was deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsItemDeleted { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteItemAction{T}"/> class.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        public DeleteItemCommand(T item)
        {
            if(null == item) {
                throw new ArgumentNullException(nameof(item));
            }

            Item = item;
        } 

        public async override Task DoActionAsync(DatabaseState databaseState, BackpackPlannerSettings settings)
        {
            await ValidateDatabaseStateAsync(databaseState).ConfigureAwait(false);

            int count = await DatabaseItem.DeleteItemAsync(databaseState, Item).ConfigureAwait(false);

// TODO: how do we save "undo" state? maybe in the item itself?

            IsItemDeleted = count > 0;
        }

        public async override Task UndoActionAsync(DatabaseState databaseState, BackpackPlannerSettings settings)
        {
            await ValidateDatabaseStateAsync(databaseState).ConfigureAwait(false);

            await DatabaseItem.SaveItemAsync(databaseState, Item).ConfigureAwait(false);

// TODO: how do we load "undo" state? maybe in the item itself?
        }
    }
}
