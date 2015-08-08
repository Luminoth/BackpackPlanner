///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

module BackpackPlanner.Mockup.Models {
    "use strict";

    export enum Sex {
        NotSpecified,
        Male,
        Female
    }

    export interface IUserInformation extends ng.resource.IResource<IUserInformation> {
        FirstName: string;
        LastName: string;
        BirthDate: Date;
        Sex: Sex;
        HeightInInches: number;
        WeightInOunces: number;
    }

    export interface IUserInformationResource extends ng.resource.IResourceClass<IUserInformation> {
        get(): IUserInformation;
    }

    export function userInformationResourceFactory($resource: ng.resource.IResourceService) : IUserInformationResource {
        const queryAction: ng.resource.IActionDescriptor = {
            method: "GET",
            isArray: false
        };

        return <IUserInformationResource> $resource("data/user.json", {}, {
            get: queryAction
        });
    }
}
