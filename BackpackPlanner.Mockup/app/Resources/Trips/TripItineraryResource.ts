///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Models/Trips/TripItinerary.ts" />

module BackpackPlanner.Mockup.Resources.Trips {
    "use strict";

    export interface ITripItinerary {
        Id: number;
        Name: string;
        Note: string;
    }

    export interface ITripItineraryResource extends ITripItinerary, ng.resource.IResource<ITripItinerary> {
    }
}
