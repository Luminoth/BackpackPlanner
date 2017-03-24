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

using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;

namespace EnergonSoftware.BackpackPlanner.Commands.Gear
{
    /// <summary>
    /// Gets all items.
    /// </summary>
    public sealed class GetGearCollectionsCommand : GetItemsCommand<GearCollection>
    {
        public GetGearCollectionsCommand(Func<GearCollection, bool> filter=null)
            : base(filter)
        {
        }

        public override async Task DoActionAsync(BackpackPlannerState state)
        {
            await base.DoActionAsync(state).ConfigureAwait(false);

            foreach(GearCollection item in Items) {
                await GetGearSystemsAsync(state, item).ConfigureAwait(false);
                await GetGearItemsAsync(state, item).ConfigureAwait(false);
            }
        }

        private async Task GetGearSystemsAsync(BackpackPlannerState state, GearCollection gearCollection)
        {
            gearCollection.GearSystems = await GearCollectionGearSystem.GetItemsAsync(state, gearCollection).ConfigureAwait(false);

            var gearSystemIds = gearCollection.GearSystems.Select(gearSystem => gearSystem.GearSystemId).ToList();
            GetGearSystemsCommand command = new GetGearSystemsCommand(x => gearSystemIds.Contains(x.Id));
            await command.DoActionAsync(state).ConfigureAwait(false);

            var gearSystemMap = command.Items.ToDictionary(gearSystem => gearSystem.Id);
            foreach(GearCollectionGearSystem gearSystem in gearCollection.GearSystems) {
                gearSystem.Parent = gearCollection;
                gearSystem.Child = gearSystemMap[gearSystem.GearSystemId];
            }
        }

        private async Task GetGearItemsAsync(BackpackPlannerState state, GearCollection gearCollection)
        {
            gearCollection.GearItems = await GearCollectionGearItem.GetItemsAsync(state, gearCollection).ConfigureAwait(false);

            var gearItemIds = gearCollection.GearItems.Select(gearItem => gearItem.GearItemId).ToList();
            GetGearItemsCommand command = new GetGearItemsCommand(x => gearItemIds.Contains(x.Id));
            await command.DoActionAsync(state).ConfigureAwait(false);

            var gearItemMap = command.Items.ToDictionary(gearItem => gearItem.Id);
            foreach(GearCollectionGearItem gearItem in gearCollection.GearItems) {
                gearItem.Parent = gearCollection;
                gearItem.Child = gearItemMap[gearItem.GearItemId];
            }
        }
    }
}
