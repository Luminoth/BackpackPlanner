///<reference path="../scripts/typings/angularjs/angular.d.ts" />

///<reference path="Models/Trips/TripItinerary.ts" />
///<reference path="Models/Trips/TripPlan.ts" />

///<reference path="Resources/Trips/TripItineraryResource.ts" />
///<reference path="Resources/Trips/TripPlanResource.ts" />

///<reference path="Services/Trips/TripItineraryService.ts" />
///<reference path="Services/Trips/TripPlanService.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export class TripState {
        /* Trip Itineraries */

        private _tripItineraries = <Array<Models.Trips.TripItinerary>>[];

        // TODO: this should be a read-only collection
        public getTripItineraries() {
            return this._tripItineraries;
        }

        public getTripItineraryIndexById(tripItineraryId: number) {
            for(let i=0; i<this._tripItineraries.length; ++i) {
                const tripItinerary = this._tripItineraries[i];
                if(tripItinerary.Id == tripItineraryId) {
                    return i;
                }
            }
            return -1;
        }

        public getTripItineraryById(tripItineraryId: number) {
            const idx = this.getTripItineraryIndexById(tripItineraryId);
            return idx < 0 ? null : this._tripItineraries[idx];
        }

        private static _lastTripItineraryId = 0;

        private getNextTripItineraryId() {
            return ++TripState._lastTripItineraryId;
        }

        public addTripItinerary(tripItinerary: Models.Trips.TripItinerary) {
            if(tripItinerary.Id < 0) {
                tripItinerary.Id = this.getNextTripItineraryId();
            } else if(tripItinerary.Id > TripState._lastTripItineraryId) {
                TripState._lastTripItineraryId = tripItinerary.Id;
            }

            this._tripItineraries.push(tripItinerary);
            return tripItinerary.Id;
        }

        public deleteTripItinerary(tripItinerary: Models.Trips.TripItinerary) {
            const idx = this.getTripItineraryIndexById(tripItinerary.Id);
            if(idx < 0) {
                return false;
            }
            this._tripItineraries.splice(idx, 1);

            // TODO: remove the itinerary from the trip plans it belongs to

            return true;
        }

        public deleteAllTripItineraries() {
            this._tripItineraries = <Array<Models.Trips.TripItinerary>>[];
        }

        /* Trip Plans */

        private _tripPlans = <Array<Models.Trips.TripPlan>>[];

        // TODO: this should be a read-only collection
        public getTripPlans() {
            return this._tripPlans;
        }

        public getTripPlanIndexById(tripPlanId: number) {
            for(let i=0; i<this._tripPlans.length; ++i) {
                const tripPlan = this._tripPlans[i];
                if(tripPlan.Id == tripPlanId) {
                    return i;
                }
            }
            return -1;
        }

        public getTripPlanById(tripPlanId: number) {
            const idx = this.getTripPlanIndexById(tripPlanId);
            return idx < 0 ? null : this._tripPlans[idx];
        }

        private static _lastTripPlanId = 0;

        private getNextTripPlanId() {
            return ++TripState._lastTripPlanId;
        }

        public addTripPlan(tripPlan: Models.Trips.TripPlan) {
            if(tripPlan.Id < 0) {
                tripPlan.Id = this.getNextTripPlanId();
            } else if(tripPlan.Id > TripState._lastTripPlanId) {
                TripState._lastTripPlanId = tripPlan.Id;
            }

            this._tripPlans.push(tripPlan);
            return tripPlan.Id;
        }

        public deleteTripPlan(tripPlan: Models.Trips.TripPlan) {
            const idx = this.getTripPlanIndexById(tripPlan.Id);
            if(idx < 0) {
                return false;
            }
            this._tripPlans.splice(idx, 1);

            return true;
        }

        public deleteAllTripPlans() {
            this._tripPlans = <Array<Models.Trips.TripPlan>>[];
        }

        /* Utilities */

        public deleteAllData() {
            this.deleteAllTripItineraries();
            this.deleteAllTripPlans();
        }

        /* Load/Save */

        private loadTripItineraries($q: ng.IQService, tripItineraryResources: Resources.Trips.ITripItineraryResource[]) {
            const promises = <Array<ng.IPromise<any>>>[];
            this._tripItineraries = <Array<Models.Trips.TripItinerary>>[];
            for(let i=0; i<tripItineraryResources.length; ++i) {
                const tripItinerary = new Models.Trips.TripItinerary();
                promises.push(tripItinerary.loadFromDevice($q, tripItineraryResources[i]).then(
                    (loadedTripItinerary) => {
                        this.addTripItinerary(loadedTripItinerary);
                    }
                ));
            }
            return $q.all(promises);
        }

        private loadTripPlans($q: ng.IQService, tripPlanResources: Resources.Trips.ITripPlanResource[]) {
            const promises = <Array<ng.IPromise<any>>>[];
            this._tripPlans = <Array<Models.Trips.TripPlan>>[];
            for(let i=0; i<tripPlanResources.length; ++i) {
                const tripPlan = new Models.Trips.TripPlan();
                promises.push(tripPlan.loadFromDevice($q, tripPlanResources[i]).then(
                    (loadedTripPlan) => {
                        this.addTripPlan(loadedTripPlan);
                    }
                ));
            }
            return $q.all(promises);
        }

        public loadFromDevice($q: ng.IQService, tripItineraryService: Services.Trips.ITripItineraryService, tripPlanService: Services.Trips.ITripPlanService) : ng.IPromise<any[]> {
            const promises = <Array<ng.IPromise<any>>>[];

            promises.push(tripItineraryService.query().$promise.then(
                (tripItineraryResources: Resources.Trips.ITripItineraryResource[]) => {
                    this.loadTripItineraries($q, tripItineraryResources).then(
                        () => {
                        }
                    );
                }
            ));

            promises.push(tripPlanService.query().$promise.then(
                (tripPlanResources: Resources.Trips.ITripPlanResource[]) => {
                    this.loadTripPlans($q, tripPlanResources).then(
                        () => {
                        }
                    );
                }
            ));

            return $q.all(promises);
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any[]> {
            alert("TripState.saveToDevice");
            return $q.defer().promise;
        }
    }
}