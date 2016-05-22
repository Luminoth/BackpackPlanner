///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Resources/Gear/GearItemResource.ts" />

module BackpackPlanner.Mockup.Services.Gear {
    "use strict";

    export interface IGearItemService extends ng.resource.IResourceClass<Resources.Gear.IGearItemResource> {
        query: ng.resource.IResourceArrayMethod<Resources.Gear.IGearItemResource>;
    }

    export function gearItemServiceFactory($resource: ng.resource.IResourceService) {
        return <IGearItemService> $resource("data/gear/items.json", {}, {
            query: { method: "GET", isArray: true }
        });
    }
}
