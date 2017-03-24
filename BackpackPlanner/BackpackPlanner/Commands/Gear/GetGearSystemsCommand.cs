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
using System.Linq;
using System.Threading.Tasks;

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
                await GetGearItemsAsync(state, item).ConfigureAwait(false);
            }
        }

        private async Task GetGearItemsAsync(BackpackPlannerState state, GearSystem gearSystem)
        {
            gearSystem.GearItems = await GearSystemGearItem.GetItemsAsync(state, gearSystem).ConfigureAwait(false);

            var gearItemIds = gearSystem.GearItems.Select(gearItem => gearItem.GearItemId).ToList();
            GetGearItemsCommand command = new GetGearItemsCommand(x => gearItemIds.Contains(x.Id));
            await command.DoActionAsync(state).ConfigureAwait(false);

            var gearItemMap = command.Items.ToDictionary(gearItem => gearItem.Id);
            foreach(GearSystemGearItem gearItem in gearSystem.GearItems) {
                gearItem.Parent = gearSystem;
                gearItem.Child = gearItemMap[gearItem.GearItemId];
            }
        }
    }
}
