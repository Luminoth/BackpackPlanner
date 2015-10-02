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

using System;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models;

namespace EnergonSoftware.BackpackPlanner.Actions
{
    /// <summary>
    /// Deletes an item.
    /// </summary>
    public class DeleteItemAction<T> : IAction where T: DatabaseItem
    {
        /// <summary>
        /// Gets or sets the item to delete.
        /// </summary>
        /// <value>
        /// The item to delete.
        /// </value>
        public T Item { get; }

        public DeleteItemAction(T item)
        {
            if(null == item) {
                throw new ArgumentNullException(nameof(item));
            }

            Item = item;
        } 

        public async Task<bool> DoActionAsync()
        {
            int count = await DatabaseItem.DeleteItemAsync(Item).ConfigureAwait(false);

// TODO: how do we save "undo" state? maybe in the item itself?

            return count > 0;
        }

        public async Task<bool> UndoActionAsync()
        {
            await DatabaseItem.SaveItemAsync(Item).ConfigureAwait(false);

// TODO: how do we load "undo" state? maybe in the item itself?

            return true;
        }
    }
}
