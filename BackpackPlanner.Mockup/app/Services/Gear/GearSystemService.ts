///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Resources/Gear/GearSystemResource.ts" />

module BackpackPlanner.Mockup.Services.Gear {
    "use strict";

    export interface IGearSystemService extends ng.resource.IResourceClass<Resources.Gear.IGearSystemResource> {
        query: ng.resource.IResourceArrayMethod<Resources.Gear.IGearSystemResource>;
    }

    export function gearSystemServiceFactory($resource: ng.resource.IResourceService) {
        return <IGearSystemService> $resource("data/gear/systems.json", {}, {
            query: { method: "GET", isArray: true }
        });
    }
}
