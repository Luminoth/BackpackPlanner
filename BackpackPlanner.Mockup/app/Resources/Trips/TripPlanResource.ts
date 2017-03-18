/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

/// <reference path="../../Models/Trips/TripPlan.ts" />

module BackpackPlanner.Mockup.Resources.Trips {
    "use strict";

    export interface ITripPlan {
        Id: number;
        Name: string;
        StartDate: string;
        EndDate: string;
        TripItineraryId: number;
        Note: string;

        GearCollections: Resources.Gear.IGearCollectionEntry[];
        GearSystems: Resources.Gear.IGearSystemEntry[];
        GearItems: Resources.Gear.IGearItemEntry[];

        Meals: Meals.IMealEntry[];
    }

    export interface ITripPlanResource extends ITripPlan, ng.resource.IResource<ITripPlan> {
    }
}
