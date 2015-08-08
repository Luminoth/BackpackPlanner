///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export interface IGearCollection extends ng.resource.IResource<IGearCollection> {
        Id: number;
    }

    export interface IGearCollectionResource extends ng.resource.IResourceClass<IGearCollection> {
        query(): Array<IGearCollection>;
    }

    export function gearCollectionResourceFactory($resource: ng.resource.IResourceService) : IGearCollectionResource {
        const queryAction: ng.resource.IActionDescriptor = {
            method: "GET",
            isArray: true
        };

        return <IGearCollectionResource> $resource("data/gear/collections.json", {}, {
            query: queryAction
        });
    }

    export function newGearCollection() : IGearCollection {
        return <IGearCollection> {
            Id: -1
        };
    }

    export function getNextGearCollectionId() : number {
        // TODO: write this
        return -1;
    }

    export function getGearCollectionIndexById(gearCollections: IGearCollection[], gearCollectionId: number) : number {
        for(let i=0; i<gearCollections.length; ++i) {
            const gearCollection = gearCollections[i];
            if(gearCollection.Id == gearCollectionId) {
                return i;
            }
        }
        return -1;
    }

    export function getGearCollectionById(gearCollections: IGearCollection[], gearCollectionId: number) : IGearCollection {
        const idx = getGearCollectionIndexById(gearCollections, gearCollectionId);
        return idx < 0 ? null : gearCollections[idx];
    }

    export function deleteGearCollection(gearCollections: IGearCollection[], gearCollection: IGearCollection) : boolean {
        const idx = gearCollections.indexOf(gearCollection);
        if(idx < 0) {
            return false;
        }
        gearCollections.splice(idx, 1);

        // TODO: remove the collection from the trip plans it belongs to

        return true;
    }

    export function getGearCollectionWeightInOunces(gearCollection: IGearCollection, gearSystems: IGearSystem[], gearItems: IGearItem[]) {
        let weightInOunces = 0;
        // TODO: calculate this
        return weightInOunces;
    }

    export function getGearCollectionCostInUSD(gearCollection: IGearCollection, gearSystems: IGearSystem[], gearItems: IGearItem[]) {
        let costInUSD = 0;
        // TODO: calculate this
        return costInUSD;
    }
}
