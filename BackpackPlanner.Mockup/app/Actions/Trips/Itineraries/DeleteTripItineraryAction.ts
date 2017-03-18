/// <reference path="../../../Models/Trips/TripItinerary.ts"/>

/// <reference path="../../Command.ts"/>

/// <reference path="../../../AppState.ts"/>

module BackpackPlanner.Mockup.Actions.Trips.Itineraries {
    "use strict";

    export class DeleteTripItineraryAction implements ICommand {
        public TripItinerary: Models.Trips.TripItinerary;

        private _tripPlans = <Array<Models.Trips.TripPlan>>[];

        public doAction() {
            this._tripPlans = AppState.getInstance().getTripState().removeTripItineraryFromPlans(this.TripItinerary);

            AppState.getInstance().getTripState().deleteTripItinerary(this.TripItinerary);
        }

        public undoAction() {
            AppState.getInstance().getTripState().addTripItinerary(this.TripItinerary);

            for(let i=0; i<this._tripPlans.length; ++i) {
                const tripPlan = this._tripPlans[i];
                tripPlan.tripItineraryId(this.TripItinerary.Id);
            }
        }
    }
}
