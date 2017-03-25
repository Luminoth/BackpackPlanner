/*
   Copyright 2017 Shane Lillie

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

using EnergonSoftware.BackpackPlanner.Models;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Commands
{
    /// <summary>
    /// Gets all items.
    /// </summary>
    public class GetItemCommand<T> : Command<GetItemCommand<T>> where T: DatabaseItem, new()
    {
        /// <summary>
        /// Gets the id of the item to get.
        /// </summary>
        /// <value>
        /// The id of the item to get.
        /// </value>
        public int Id { get; }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        [CanBeNull]
        public T Item { get; private set; }

        public GetItemCommand(int id)
        {
            Id = id;
        }

        public override async Task DoActionAsync(BackpackPlannerState state)
        {
            await base.DoActionAsync(state).ConfigureAwait(false);

            Item = await DatabaseItem.GetValidItemAsync<T>(state, Id).ConfigureAwait(false);
        }
    }
}
