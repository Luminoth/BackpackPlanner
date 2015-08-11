///<reference path="Models/Trips/TripItinerary.ts" />
///<reference path="Models/Trips/TripPlan.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export class TripState {
        /* Trip Itineraries */

        private _tripItineraries: Models.Trips.TripItinerary[];

        public getTripItineraries() : Models.Trips.TripItinerary[] {
            return this._tripItineraries;
        }

        public loadTripItineraries(tripItineraryResource: any[]) {
            if(this._tripItineraries) {
                throw new Error("Trip itineraries already loaded!");
            }

            this._tripItineraries = <Array<Models.Trips.TripItinerary>>[];
            for(let i=0; i<tripItineraryResource.length; ++i) {
                //this._tripItineraries.push(new Models.Trips.TripItinerary(tripItineraryResource[i]));
            }
        }

        private getNextTripItineraryId() : number {
            // TODO: write this
            return -1;
        }

        public getTripItineraryIndexById(tripItineraryId: number) : number {
            for(let i=0; i<this._tripItineraries.length; ++i) {
                const tripItinerary = this._tripItineraries[i];
                if(tripItinerary.Id == tripItineraryId) {
                    return i;
                }
            }
            return -1;
        }

        public getTripItineraryById(tripItineraryId: number) : Models.Trips.TripItinerary {
            const idx = this.getTripItineraryIndexById(tripItineraryId);
            return idx < 0 ? null : this._tripItineraries[idx];
        }

        public addTripItinerary(tripItinerary: Models.Trips.TripItinerary) : number {
            if(tripItinerary.Id < 0) {
                tripItinerary.Id = this.getNextTripItineraryId();
            }
            this._tripItineraries.push(tripItinerary);
            return tripItinerary.Id;
        }

        public deleteTripItinerary(tripItinerary: Models.Trips.TripItinerary) : boolean {
            const idx = this.getTripItineraryIndexById(tripItinerary.Id);
            if(idx < 0) {
                return false;
            }
            this._tripItineraries.splice(idx, 1);

            // TODO: remove the itinerary from the trip plans it belongs to

            return true;
        }

        /* Trip Plans */

        private _tripPlans: Models.Trips.TripPlan[];

        public getTripPlans() : Models.Trips.TripPlan[] {
            return this._tripPlans;
        }

        public loadTripPlans(tripPlanResource: any[]) {
            if(this._tripPlans) {
                throw new Error("Trip plans already loaded!");
            }

            this._tripPlans = <Array<Models.Trips.TripPlan>>[];
            for(let i=0; i<tripPlanResource.length; ++i) {
                //this._tripPlans.push(new Models.Trips.TripPlan(tripPlanResource[i]));
            }
        }

        private getNextTripPlanId() : number {
            // TODO: write this
            return -1;
        }

        public getTripPlanIndexById(tripPlanId: number) : number {
            for(let i=0; i<this._tripPlans.length; ++i) {
                const tripPlan = this._tripPlans[i];
                if(tripPlan.Id == tripPlanId) {
                    return i;
                }
            }
            return -1;
        }

        public getTripPlanById(tripPlanId: number) : Models.Trips.TripPlan {
            const idx = this.getTripPlanIndexById(tripPlanId);
            return idx < 0 ? null : this._tripPlans[idx];
        }

        public addTripPlan(tripPlan: Models.Trips.TripPlan) : number {
            if(tripPlan.Id < 0) {
                tripPlan.Id = this.getNextTripPlanId();
            }
            this._tripPlans.push(tripPlan);
            return tripPlan.Id;
        }

        public deleteTripPlan(tripPlan: Models.Trips.TripPlan) : boolean {
            const idx = this.getTripPlanIndexById(tripPlan.Id);
            if(idx < 0) {
                return false;
            }
            this._tripPlans.splice(idx, 1);

            return true;
        }

        /* Load/Save */

        public loadFromDevice() {
            // TODO: load from the resources here and return a promise
        }

        public saveToDevice() {
            // TODO: don't do anything here, just return a promise
        }
    }
}