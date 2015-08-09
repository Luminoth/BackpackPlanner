///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Models/Gear/GearItem.ts" />

module BackpackPlanner.Mockup.Resources.Gear {
    "use strict";

    export interface IGearItemResource extends Models.Gear.IGearItem, ng.resource.IResource<Models.Gear.IGearItem> {
    }
}