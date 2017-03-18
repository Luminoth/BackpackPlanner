/// <reference path="../../../Models/Trips/TripPlan.ts"/>

/// <reference path="../../Command.ts"/>

/// <reference path="../../../AppState.ts"/>

module BackpackPlanner.Mockup.Actions.Trips.Plans {
    "use strict";

    export class DeleteTripPlanAction implements ICommand {
        public TripPlan: Models.Trips.TripPlan;

        public doAction() {
            AppState.getInstance().getTripState().deleteTripPlan(this.TripPlan);
        }

        public undoAction() {
            AppState.getInstance().getTripState().addTripPlan(this.TripPlan);
        }
    }
}
