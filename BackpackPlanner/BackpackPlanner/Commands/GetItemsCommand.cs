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
using System.Collections.Generic;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Commands
{
    /// <summary>
    /// Gets all items.
    /// </summary>
    public abstract class GetItemsCommand<T> : Command<GetItemsCommand<T>> where T: DatabaseItem
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [CanBeNull]
        public List<T> Items { get; private set; } = new List<T>();

        /// <summary>
        /// Gets or sets the item filter.
        /// </summary>
        /// <value>
        /// The item filter.
        /// </value>
        [CanBeNull]
        public Func<T, bool> Filter { get; set; }

        protected GetItemsCommand(Func<T, bool> filter=null)
        {
            Filter = filter;
        }

        public override async Task DoActionAsync(BackpackPlannerState state)
        {
            await base.DoActionAsync(state).ConfigureAwait(false);

            Items = await DatabaseItem.GetValidItemsAsync<T>(state, Filter).ConfigureAwait(false);
        }
    }
}
