/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

/// <reference path="../../Resources/Trips/TripItineraryResource.ts" />

module BackpackPlanner.Mockup.Services.Trips {
    "use strict";

    export interface ITripItineraryService extends ng.resource.IResourceClass<Resources.Trips.ITripItineraryResource> {
        query: ng.resource.IResourceArrayMethod<Resources.Trips.ITripItineraryResource>;
    }

    export function tripItineraryServiceFactory($resource: ng.resource.IResourceService) {
        return <ITripItineraryService> $resource("data/trips/itineraries.json", {}, {
            query: { method: "GET", isArray: true }
        });
    }
}
