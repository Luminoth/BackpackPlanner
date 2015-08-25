///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="GearItemResource.ts" />

module BackpackPlanner.Mockup.Resources.Gear {
    "use strict";

    export interface IGearSystem {
        Id: number;
        Name: string;
        Note: string;

        GearItems: IGearItemEntry[];
    }

    export interface IGearSystemEntry {
        GearSystemId: number;
        Count: number;
    }

    export interface IGearSystemResource extends IGearSystem, ng.resource.IResource<IGearSystem> {
    }
}
