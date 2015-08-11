///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Models/Personal/UserInformation.ts" />

module BackpackPlanner.Mockup.Resources.Personal {
    "use strict";

    export interface IUserInformationResource extends Models.Personal.IUserInformation, ng.resource.IResource<Models.Personal.IUserInformation> {
    }
}