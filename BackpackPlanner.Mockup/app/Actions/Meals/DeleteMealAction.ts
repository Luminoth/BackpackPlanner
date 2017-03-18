/// <reference path="../../Models/Meals/Meal.ts"/>

/// <reference path="../Command.ts"/>

/// <reference path="../../AppState.ts"/>

module BackpackPlanner.Mockup.Actions.Meals {
    "use strict";

    export class DeleteMealAction implements ICommand {
        public Meal: Models.Meals.Meal;

        private _tripPlans = <Array<Models.Trips.TripPlan>>[];

        public doAction() {
            this._tripPlans = AppState.getInstance().getTripState().removeMealFromPlans(this.Meal);

            AppState.getInstance().getMealState().deleteMeal(this.Meal);
        }

        public undoAction() {
            AppState.getInstance().getMealState().addMeal(this.Meal);

            for(let i=0; i<this._tripPlans.length; ++i) {
                const tripPlan = this._tripPlans[i];
                tripPlan.addMeal(this.Meal);
            }
        }
    }
}
