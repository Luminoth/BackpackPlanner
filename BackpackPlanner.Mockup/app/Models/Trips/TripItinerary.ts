/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />

/// <reference path="../../Resources/Trips/TripItineraryResource.ts"/>

module BackpackPlanner.Mockup.Models.Trips {
    "use strict";

    export class TripItinerary {
        private _id = -1;
        private _name = "";
        private _note = "";

        public get Id() {
            return this._id;
        }

        public set Id(id: number) {
            this._id = id;
        }

        public name(name?: string) {
            return arguments.length
                ? (this._name = name)
                : this._name;
        }

        public note(note?: string) {
            return arguments.length
                ? (this._note = note)
                : this._note;
        }

        /* Load/Save */

        public update(tripItinerary: TripItinerary) {
            this._name = tripItinerary._name;
            this._note = tripItinerary._note;
        }

        public loadFromDevice($q: ng.IQService, tripItineraryResource: Resources.Trips.ITripItineraryResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this._id = tripItineraryResource.Id;
            this._name = tripItineraryResource.Name;
            this._note = tripItineraryResource.Note;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("TripItinerary.saveToDevice");
            return $q.defer().promise;
        }
    }
}
