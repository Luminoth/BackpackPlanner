///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Models/Gear/GearCollection.ts" />

module BackpackPlanner.Mockup.Resources.Gear {
    "use strict";

    export interface IGearCollectionResource extends Models.Gear.IGearCollection, ng.resource.IResource<Models.Gear.IGearCollection> {
    }
}