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

using EnergonSoftware.BackpackPlanner.Commands.Gear;
using EnergonSoftware.BackpackPlanner.Commands.Meals;
using EnergonSoftware.BackpackPlanner.Models.Trips.Plans;

namespace EnergonSoftware.BackpackPlanner.Commands.Trips
{
    /// <summary>
    /// Gets all items.
    /// </summary>
    public sealed class GetTripPlansCommand : GetItemsCommand<TripPlan>
    {
        public GetTripPlansCommand(Func<TripPlan, bool> filter=null)
            : base(filter)
        {
        }

        public override async Task DoActionAsync(BackpackPlannerState state)
        {
            await base.DoActionAsync(state).ConfigureAwait(false);

            foreach(TripPlan item in Items) {
                await ReadGearCollections(state, item).ConfigureAwait(false);
                await ReadGearSystems(state, item).ConfigureAwait(false);
                await ReadGearItems(state, item).ConfigureAwait(false);
                await ReadMeals(state, item).ConfigureAwait(false);
            }
        }

        private async Task ReadGearCollections(BackpackPlannerState state, TripPlan tripPlan)
        {
            var intermediateItems = await (from x in state.DatabaseState.Connection.AsyncConnection.Table<TripPlanGearCollection>() where x.GearCollectionId == tripPlan.Id select x).ToListAsync().ConfigureAwait(false);

            GetGearSystemsCommand command = new GetGearSystemsCommand(x => (from i in intermediateItems where i.GearCollectionId == x.Id select i).Any());
            await command.DoActionAsync(state).ConfigureAwait(false);
            tripPlan.GearSystems = command.Items;
        }

        private async Task ReadGearSystems(BackpackPlannerState state, TripPlan tripPlan)
        {
            var intermediateItems = await (from x in state.DatabaseState.Connection.AsyncConnection.Table<TripPlanGearSystem>() where x.GearSystemId == tripPlan.Id select x).ToListAsync().ConfigureAwait(false);

            GetGearSystemsCommand command = new GetGearSystemsCommand(x => (from i in intermediateItems where i.GearSystemId == x.Id select i).Any());
            await command.DoActionAsync(state).ConfigureAwait(false);
            tripPlan.GearSystems = command.Items;
        }

        private async Task ReadGearItems(BackpackPlannerState state, TripPlan tripPlan)
        {
            var intermediateItems = await (from x in state.DatabaseState.Connection.AsyncConnection.Table<TripPlanGearItem>() where x.GearItemId == tripPlan.Id select x).ToListAsync().ConfigureAwait(false);

            GetGearItemsCommand command = new GetGearItemsCommand(x => (from i in intermediateItems where i.GearItemId == x.Id select i).Any());
            await command.DoActionAsync(state).ConfigureAwait(false);
            tripPlan.GearItems = command.Items;
        }

        private async Task ReadMeals(BackpackPlannerState state, TripPlan tripPlan)
        {
            var intermediateItems = await (from x in state.DatabaseState.Connection.AsyncConnection.Table<TripPlanMeal>() where x.MealId == tripPlan.Id select x).ToListAsync().ConfigureAwait(false);

            GetMealsCommand command = new GetMealsCommand(x => (from i in intermediateItems where i.MealId == x.Id select i).Any());
            await command.DoActionAsync(state).ConfigureAwait(false);
            tripPlan.Meals = command.Items;
        }
    }
}
