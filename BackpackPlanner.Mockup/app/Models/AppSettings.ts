///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

module BackpackPlanner.Mockup.Models {
    "use strict";

    export enum Units {
        Imperial,
        Metric
    }

    export interface IAppSettings extends ng.resource.IResource<IAppSettings> {
        units: Units;
    }

    export interface IAppSettingsResource extends ng.resource.IResourceClass<IAppSettings> {
        get(): IAppSettings;
    }

    export function appSettingsResourceFactory($resource: ng.resource.IResourceService) : IAppSettingsResource {
        const queryAction: ng.resource.IActionDescriptor = {
            method: "GET",
            isArray: false
        };

        return <IAppSettingsResource> $resource("data/settings.json", {}, {
            get: queryAction
        });
    }
}
