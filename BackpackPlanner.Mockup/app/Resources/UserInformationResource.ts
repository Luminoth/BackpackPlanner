///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../Models/UserInformation.ts" />

module BackpackPlanner.Mockup.Resources {
    "use strict";

    export interface IUserInformationResource extends Models.IUserInformation, ng.resource.IResource<Models.IUserInformation> {
    }
}