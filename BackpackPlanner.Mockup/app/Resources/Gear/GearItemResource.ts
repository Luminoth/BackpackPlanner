///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

module BackpackPlanner.Mockup.Resources.Gear {
    "use strict";

    export interface IGearItem {
        Id: number;
        Name: string;
        Url: string;
        Make: string;
        Model: string;
        Carried: string;
        WeightInGrams: number;
        CostInUSDP: number;
        IsConsumable: boolean;
        ConsumedPerDay: number;
        Note: string;
    }

    export interface IGearItemEntry {
        GearItemId: number;
        Count: number;
    }

    export interface IGearItemResource extends IGearItem, ng.resource.IResource<IGearItem> {
    }
}
