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

namespace EnergonSoftware.BackpackPlanner.Actions
{
    /// <summary>
    /// Adds an item.
    /// </summary>
    public class AddItemAction<T> : Action where T: DatabaseItem
    {
        public T Item { get; }

        public AddItemAction(T item)
        {
            if(null == item) {
                throw new ArgumentNullException(nameof(item));
            }

            Item = item;
        }

        public async override Task DoActionAsync()
        {
            await DatabaseItem.SaveItemAsync(Item).ConfigureAwait(false);
        }
    }
}
