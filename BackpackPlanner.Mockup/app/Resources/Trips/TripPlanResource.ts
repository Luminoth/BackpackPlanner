///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Models/Trips/TripPlan.ts" />

module BackpackPlanner.Mockup.Resources.Trips {
    "use strict";

    export interface ITripPlanResource extends Models.Trips.ITripPlan, ng.resource.IResource<Models.Trips.ITripPlan> {
    }
}
