///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export enum GearCarried {
        NotCarried,
        Carried,
        Worn
    }

    export interface IGearItem extends ng.resource.IResource<IGearItem> {
        Id: number;
        Name: string;
        Url: string;
        Make: string;
        Model: string;
        Carried: GearCarried;
        WeightInOunces: number;
        CostInUSD: number;
        IsConsumable: boolean;
        ConsumedPerDay: number;
        Note: string;
    }

    export interface IGearItemResource extends ng.resource.IResourceClass<IGearItem> {
        query(): Array<IGearItem>;
    }

    export function gearItemResourceFactory($resource: ng.resource.IResourceService) : IGearItemResource {
        const queryAction: ng.resource.IActionDescriptor = {
            method: "GET",
            isArray: true
        };

        return <IGearItemResource> $resource("data/gear/items.json", {}, {
            query: queryAction
        });
    }

    export function newGearItem() : IGearItem {
        return <IGearItem> {
            Id: -1,
            Name: "",
            Url: "",
            Make: "",
            Model: "",
            Carried: GearCarried.Carried,
            WeightInOunces: 0,
            CostInUSD: 0,
            IsConsumable: false,
            ConsumedPerDay: 0,
            Note: ""
        };
    }

    export function getNextGearItemId() : number {
        // TODO: write this
        return -1;
    }

    export function getGearItemIndexById(gearItems: IGearItem[], gearItemId: number) : number {
        for(let i=0; i<gearItems.length; ++i) {
            const gearItem = gearItems[i];
            if(gearItem.Id == gearItemId) {
                return i;
            }
        }
        return -1;
    }

    export function getGearItemById(gearItems: IGearItem[], gearItemId: number) : IGearItem {
        const idx = getGearItemIndexById(gearItems, gearItemId);
        return idx < 0 ? null : gearItems[idx];
    }

    export function deleteGearItem(gearItems: IGearItem[], gearSystems: IGearSystem[], gearCollections: IGearCollection[], gearItem: IGearItem) : boolean {
        const idx = getGearItemIndexById(gearItems, gearItem.Id);
        if(idx < 0) {
            return false;
        }
        gearItems.splice(idx, 1);

        // TODO: remove the item from the systems, collections, and trip plans it belongs to

        return true;
    }
}
