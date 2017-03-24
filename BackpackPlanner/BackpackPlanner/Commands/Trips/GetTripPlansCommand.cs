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
                await GetGearCollectionsAsync(state, item).ConfigureAwait(false);
                await GetGearSystemsAsync(state, item).ConfigureAwait(false);
                await GetGearItemsAsync(state, item).ConfigureAwait(false);
                await GetMealsAsync(state, item).ConfigureAwait(false);
            }
        }

        private async Task GetGearCollectionsAsync(BackpackPlannerState state, TripPlan tripPlan)
        {
            tripPlan.GearCollections = await TripPlanGearCollection.GetItemsAsync(state, tripPlan).ConfigureAwait(false);

            var gearCollectionIds = tripPlan.GearCollections.Select(gearCollection => gearCollection.GearCollectionId).ToList();
            GetGearCollectionsCommand command = new GetGearCollectionsCommand(x => gearCollectionIds.Contains(x.Id));
            await command.DoActionAsync(state).ConfigureAwait(false);

            var gearCollectionMap = command.Items.ToDictionary(gearCollection => gearCollection.Id);
            foreach(TripPlanGearCollection gearCollection in tripPlan.GearCollections) {
                gearCollection.Parent = tripPlan;
                gearCollection.Child = gearCollectionMap[gearCollection.GearCollectionId];
            }
        }

        private async Task GetGearSystemsAsync(BackpackPlannerState state, TripPlan tripPlan)
        {
            tripPlan.GearSystems = await TripPlanGearSystem.GetItemsAsync(state, tripPlan).ConfigureAwait(false);

            var gearSystemIds = tripPlan.GearSystems.Select(gearSystem => gearSystem.GearSystemId).ToList();
            GetGearSystemsCommand command = new GetGearSystemsCommand(x => gearSystemIds.Contains(x.Id));
            await command.DoActionAsync(state).ConfigureAwait(false);

            var gearSystemMap = command.Items.ToDictionary(gearSystem => gearSystem.Id);
            foreach(TripPlanGearSystem gearSystem in tripPlan.GearSystems) {
                gearSystem.Parent = tripPlan;
                gearSystem.Child = gearSystemMap[gearSystem.GearSystemId];
            }
        }

        private async Task GetGearItemsAsync(BackpackPlannerState state, TripPlan tripPlan)
        {
            tripPlan.GearItems = await TripPlanGearItem.GetItemsAsync(state, tripPlan).ConfigureAwait(false);

            var gearItemIds = tripPlan.GearItems.Select(gearItem => gearItem.GearItemId).ToList();
            GetGearItemsCommand command = new GetGearItemsCommand(x => gearItemIds.Contains(x.Id));
            await command.DoActionAsync(state).ConfigureAwait(false);

            var gearItemMap = command.Items.ToDictionary(gearItem => gearItem.Id);
            foreach(TripPlanGearItem gearItem in tripPlan.GearItems) {
                gearItem.Parent = tripPlan;
                gearItem.Child = gearItemMap[gearItem.GearItemId];
            }
        }

        private async Task GetMealsAsync(BackpackPlannerState state, TripPlan tripPlan)
        {
            tripPlan.Meals = await TripPlanMeal.GetItemsAsync(state, tripPlan).ConfigureAwait(false);

            var mealIds = tripPlan.Meals.Select(meal => meal.MealId).ToList();
            GetMealsCommand command = new GetMealsCommand(x => mealIds.Contains(x.Id));
            await command.DoActionAsync(state).ConfigureAwait(false);

            var mealMap = command.Items.ToDictionary(meal => meal.Id);
            foreach(TripPlanMeal meal in tripPlan.Meals) {
                meal.Parent = tripPlan;
                meal.Child = mealMap[meal.MealId];
            }
        }
    }
}
