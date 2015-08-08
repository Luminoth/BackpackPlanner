///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export interface IGearSystem extends ng.resource.IResource<IGearSystem> {
        Id: number;
        Name: string;
        GearItems: number[];
        Note: string;
    }

    export interface IGearSystemResource extends ng.resource.IResourceClass<IGearSystem> {
        query(): Array<IGearSystem>;
    }

    export function gearSystemResourceFactory($resource: ng.resource.IResourceService) : IGearSystemResource {
        const queryAction: ng.resource.IActionDescriptor = {
            method: "GET",
            isArray: true
        };

        return <IGearSystemResource> $resource("data/gear/systems.json", {}, {
            query: queryAction
        });
    }

    export function newGearSystem() : IGearSystem {
        return <IGearSystem> {
            Id: -1,
            Name: "",
            GearItems: <Array<number>>[],
            Note: ""
        };
    }

    export function getNextGearSystemId() : number {
        // TODO: write this
        return -1;
    }

    export function getGearSystemIndexById(gearSystems: IGearSystem[], gearSystemId: number) : number {
        for(let i=0; i<gearSystems.length; ++i) {
            const gearSystem = gearSystems[i];
            if(gearSystem.Id == gearSystemId) {
                return i;
            }
        }
        return -1;
    }

    export function getGearSystemById(gearSystems: IGearSystem[], gearSystemId: number) : IGearSystem {
        const idx = getGearSystemIndexById(gearSystems, gearSystemId);
        return idx < 0 ? null : gearSystems[idx];
    }

    export function deleteGearSystem(gearSystems: IGearSystem[], gearCollections: IGearCollection[], gearSystem: IGearSystem) : boolean {
        const idx = getGearSystemIndexById(gearSystems, gearSystem.Id);
        if(idx < 0) {
            return false;
        }
        gearSystems.splice(idx, 1);

        // TODO: remove the system from the collections, and trip plans it belongs to

        return true;
    }

    export function getGearSystemWeightInOunces(gearSystem: IGearSystem, gearItems: IGearItem[]) {
        let weightInOunces = 0;
        for(let i=0; i<gearSystem.GearItems.length; ++i) {
            const gearItem = getGearItemById(gearItems, gearSystem.GearItems[i]);
            if(null == gearItem) {
                continue;
            }
            weightInOunces += gearItem.WeightInOunces;
        }
        return weightInOunces;
    }

    export function getGearSystemCostInUSD(gearSystem: IGearSystem, gearItems: IGearItem[]) {
        let costInUSD = 0;
        for(let i=0; i<gearSystem.GearItems.length; ++i) {
            const gearItem = getGearItemById(gearItems, gearSystem.GearItems[i]);
            if(null == gearItem) {
                continue;
            }
            costInUSD += gearItem.CostInUSD;
        }
        return costInUSD;
    }
}
