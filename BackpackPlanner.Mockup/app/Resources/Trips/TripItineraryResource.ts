///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Models/Trips/TripItinerary.ts" />

module BackpackPlanner.Mockup.Resources.Trips {
    "use strict";

    export interface ITripItineraryResource extends Models.Trips.ITripItinerary, ng.resource.IResource<Models.Trips.ITripItinerary> {
    }
}
