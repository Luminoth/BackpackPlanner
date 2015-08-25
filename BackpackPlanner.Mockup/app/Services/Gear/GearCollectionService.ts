///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Resources/Gear/GearCollectionResource.ts" />

module BackpackPlanner.Mockup.Services.Gear {
    "use strict";

    export interface IGearCollectionService extends ng.resource.IResourceClass<Resources.Gear.IGearCollectionResource> {
        query(): Resources.Gear.IGearCollectionResource[];
    }

    export function gearCollectionServiceFactory($resource: ng.resource.IResourceService) {
        return <IGearCollectionService> $resource("data/gear/collections.json", {}, {
            query: { method: "GET", isArray: true }
        });
    }
}
