///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Resources/Personal/UserInformationResource.ts" />

module BackpackPlanner.Mockup.Services.Personal {
    "use strict";

    export interface IUserInformationService extends ng.resource.IResourceClass<Resources.Personal.IUserInformationResource> {
        get(): Resources.Personal.IUserInformationResource;
    }

    export function userInformationServiceFactory($resource: ng.resource.IResourceService) {
        return <IUserInformationService> $resource("data/user.json", {}, {
            get: { method: "GET", isArray: false }
        });
    }
}
