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

using EnergonSoftware.BackpackPlanner.Models;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Commands
{
    /// <summary>
    /// Deletes an item.
    /// </summary>
    public class DeleteItemCommand<T> : Command<DeleteItemCommand<T>> where T: DatabaseItem
    {
        /// <summary>
        /// Gets the item to delete.
        /// </summary>
        /// <value>
        /// The item to delete.
        /// </value>
        [NotNull]
        public T Item { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteItemCommand{T}"/> class.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        public DeleteItemCommand(T item)
        {
            if(null == item) {
                throw new ArgumentNullException(nameof(item));
            }

            Item = item;
        }

        public override async Task DoActionAsync(BackpackPlannerState state)
        {
            await base.DoActionAsync(state).ConfigureAwait(false);

            Item.IsDeleted = true;

            await DatabaseItem.SaveItemAsync(state, Item).ConfigureAwait(false);
        }

        public override async Task UndoActionAsync(BackpackPlannerState state)
        {
            await base.UndoActionAsync(state).ConfigureAwait(false);

            Item.IsDeleted = false;

            await DatabaseItem.SaveItemAsync(state, Item).ConfigureAwait(false);
        }
    }
}
