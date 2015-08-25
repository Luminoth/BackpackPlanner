///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="GearSystemResource.ts" />
///<reference path="GearItemResource.ts" />

module BackpackPlanner.Mockup.Resources.Gear {
    "use strict";

    export interface IGearCollection {
        Id: number;
        Name: string;
        Note: string;

        GearSystems: IGearSystemEntry[];
        GearItems: IGearItemEntry[];
    }

    export interface IGearCollectionEntry {
        GearCollectionId: number;
        Count: number;
    }

    export interface IGearCollectionResource extends IGearCollection, ng.resource.IResource<IGearCollection> {
    }
}
