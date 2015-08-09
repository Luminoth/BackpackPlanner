///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../Resources/UserInformationResource.ts" />

module BackpackPlanner.Mockup.Services {
    "use strict";

    export interface IUserInformationService extends ng.resource.IResourceClass<Resources.IUserInformationResource> {
        get(): Resources.IUserInformationResource;
    }

    export function userInformationServiceFactory($resource: ng.resource.IResourceService) : IUserInformationService {
        return <IUserInformationService> $resource("data/user.json", {}, {
            get: { method: "GET", isArray: false }
        });
    }
}
