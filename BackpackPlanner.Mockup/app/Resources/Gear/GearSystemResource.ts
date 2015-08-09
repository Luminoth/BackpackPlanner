///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Models/Gear/GearSystem.ts" />

module BackpackPlanner.Mockup.Resources.Gear {
    "use strict";

    export interface IGearSystemResource extends Models.Gear.IGearSystem, ng.resource.IResource<Models.Gear.IGearSystem> {
    }
}