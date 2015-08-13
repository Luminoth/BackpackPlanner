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

        private _tripItineraries: Models.Trips.TripItinerary[];

        // TODO: this should be a read-only collection
        public getTripItineraries() : Models.Trips.TripItinerary[] {
            return this._tripItineraries;
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

        private getNextTripItineraryId() : number {
            // TODO: write this
            return -1;
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

        public deleteAllTripItineraries() : void {
            this._tripItineraries = <Array<Models.Trips.TripItinerary>>[];
        }

        /* Trip Plans */

        private _tripPlans: Models.Trips.TripPlan[];

        // TODO: this should be a read-only collection
        public getTripPlans() : Models.Trips.TripPlan[] {
            return this._tripPlans;
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

        private getNextTripPlanId() : number {
            // TODO: write this
            return -1;
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

        public deleteAllTripPlans() : void {
            this._tripPlans = <Array<Models.Trips.TripPlan>>[];
        }

        /* Load/Save */

        private loadTripItineraries(tripItineraryResources: Resources.Trips.ITripItineraryResource[]) {
            if(this._tripItineraries) {
                throw new Error("Trip itineraries already loaded!");
            }

            this._tripItineraries = <Array<Models.Trips.TripItinerary>>[];
            for(let i=0; i<tripItineraryResources.length; ++i) {
                //this._tripItineraries.push(new Models.Trips.TripItinerary(tripItineraryResources[i]));
            }
        }

        private loadTripPlans(tripPlanResources: Resources.Trips.ITripPlanResource[]) {
            if(this._tripPlans) {
                throw new Error("Trip plans already loaded!");
            }

            this._tripPlans = <Array<Models.Trips.TripPlan>>[];
            for(let i=0; i<tripPlanResources.length; ++i) {
                //this._tripPlans.push(new Models.Trips.TripPlan(tripPlanResources[i]));
            }
        }

        public loadFromDevice($q: ng.IQService, tripItineraryService: Services.Trips.ITripItineraryService, tripPlanService: Services.Trips.ITripPlanService) : ng.IPromise<any[]> {
            const promises = <Array<ng.IPromise<any>>>[];

            promises.push(tripItineraryService.query().$promise.then(
                (tripItineraryResources: Resources.Trips.ITripItineraryResource[]) => {
                    this.loadTripItineraries(tripItineraryResources);
                }
            ));

            promises.push(tripPlanService.query().$promise.then(
                (tripPlanResources: Resources.Trips.ITripPlanResource[]) => {
                    this.loadTripPlans(tripPlanResources);
                }
            ));

            return $q.all(promises);
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            // mockup does nothing here
            return $q.defer().promise;
        }
    }
}