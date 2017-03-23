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
                await ReadGearSystems(state, item).ConfigureAwait(false);
                await ReadGearItems(state, item).ConfigureAwait(false);
            }
        }

        private async Task ReadGearSystems(BackpackPlannerState state, GearCollection gearCollection)
        {
            var intermediateItems = await (from x in state.DatabaseState.Connection.AsyncConnection.Table<GearCollectionGearSystem>() where x.GearSystemId == gearCollection.Id select x).ToListAsync().ConfigureAwait(false);

            GetGearSystemsCommand command = new GetGearSystemsCommand(x => (from i in intermediateItems where i.GearSystemId == x.Id select i).Any());
            await command.DoActionAsync(state).ConfigureAwait(false);
            gearCollection.GearSystems = command.Items;
        }

        private async Task ReadGearItems(BackpackPlannerState state, GearCollection gearCollection)
        {
            var intermediateItems = await (from x in state.DatabaseState.Connection.AsyncConnection.Table<GearCollectionGearItem>() where x.GearItemId == gearCollection.Id select x).ToListAsync().ConfigureAwait(false);

            GetGearItemsCommand command = new GetGearItemsCommand(x => (from i in intermediateItems where i.GearItemId == x.Id select i).Any());
            await command.DoActionAsync(state).ConfigureAwait(false);
            gearCollection.GearItems = command.Items;
        }
    }
}
