///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../Resources/AppSettingsResource.ts" />

module BackpackPlanner.Mockup.Services {
    "use strict";

    export interface IAppSettingsService extends ng.resource.IResourceClass<Resources.IAppSettingsResource> {
        get(): Resources.IAppSettingsResource;
    }

    export function appSettingsServiceFactory($resource: ng.resource.IResourceService) : IAppSettingsService {
        return <IAppSettingsService> $resource("data/settings.json", {}, {
            get: { method: "GET", isArray: false }
        });
    }
}
