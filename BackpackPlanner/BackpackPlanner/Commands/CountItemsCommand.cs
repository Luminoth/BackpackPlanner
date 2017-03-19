﻿/*
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

namespace EnergonSoftware.BackpackPlanner.Commands
{
    /// <summary>
    /// Gets all items.
    /// </summary>
    public class CountItemsCommand<T> : Command<CountItemsCommand<T>> where T: DatabaseItem
    {
        /// <summary>
        /// Gets the count of items in the database.
        /// </summary>
        /// <value>
        /// The count of items in the database.
        /// </value>
        public int Count { get; private set; }

        public override async Task DoActionAsync(BackpackPlannerState state)
        {
            await base.DoActionAsync(state).ConfigureAwait(false);

            Count = await DatabaseItem.CountValidItemsAsync<T>(state).ConfigureAwait(false);
        }
    }
}