///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Resources/Trips/TripPlanResource.ts" />

module BackpackPlanner.Mockup.Services.Trips {
    "use strict";

    export interface ITripPlanService extends ng.resource.IResourceClass<Resources.Trips.ITripPlanResource> {
        query: ng.resource.IResourceArrayMethod<Resources.Trips.ITripPlanResource>;
    }

    export function tripPlanServiceFactory($resource: ng.resource.IResourceService) {
        return <ITripPlanService> $resource("data/trips/plans.json", {}, {
            query: { method: "GET", isArray: true }
        });
    }
}
