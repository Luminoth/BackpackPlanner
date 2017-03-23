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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

namespace EnergonSoftware.BackpackPlanner.Commands.Gear
{
    /// <summary>
    /// Gets all items.
    /// </summary>
    public sealed class GetGearSystemsCommand : GetItemsCommand<GearSystem>
    {
        public GetGearSystemsCommand(Func<GearSystem, bool> filter=null)
            : base(filter)
        {
        }

        public override async Task DoActionAsync(BackpackPlannerState state)
        {
            await base.DoActionAsync(state).ConfigureAwait(false);

            foreach(GearSystem item in Items) {
                await ReadGearItems(state, item).ConfigureAwait(false);
            }
        }

// TODO: apply these changes to other container-types

        private async Task ReadGearItems(BackpackPlannerState state, GearSystem gearSystem)
        {
            var intermediateItems = await (from x in state.DatabaseState.Connection.AsyncConnection.Table<GearSystemGearItem>() where x.GearSystemId == gearSystem.Id select x).ToListAsync().ConfigureAwait(false);

            var intermediateItemMap = new Dictionary<int, int>();
            foreach(GearSystemGearItem intermediateItem in intermediateItems) {
                if(intermediateItemMap.ContainsKey(intermediateItem.GearItemId)) {
                    ++intermediateItemMap[intermediateItem.GearItemId];
                } else {
                    intermediateItemMap.Add(intermediateItem.GearItemId, 1);
                }
            }

            GetGearItemsCommand command = new GetGearItemsCommand(x => intermediateItemMap.ContainsKey(x.Id));
            await command.DoActionAsync(state).ConfigureAwait(false);

            gearSystem.GearItems.Clear();
            foreach(GearItem item in command.Items) {
                int count = intermediateItemMap[item.Id];
                for(int i=0; i<count; ++i) {
                    gearSystem.GearItems.Add(item);
                }
            }
        }
    }
}
