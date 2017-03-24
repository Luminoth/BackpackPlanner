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
    /// Saves an item.
    /// </summary>
    public class SaveItemCommand<T> : Command<SaveItemCommand<T>> where T: DatabaseItem
    {
        /// <summary>
        /// Gets the item to save.
        /// </summary>
        /// <value>
        /// The item to save.
        /// </value>
        [NotNull]
        public T Item { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveItemCommand{T}"/> class.
        /// </summary>
        /// <param name="item">The item to save.</param>
        public SaveItemCommand(T item)
        {
            if(null == item) {
                throw new ArgumentNullException(nameof(item));
            }

            Item = item;
        }

// TODO: needs to save children after saving the item

        public override async Task DoActionAsync(BackpackPlannerState state)
        {
            await base.DoActionAsync(state).ConfigureAwait(false);

            await DatabaseItem.SaveItemAsync(state, Item).ConfigureAwait(false);
        }
    }
}
