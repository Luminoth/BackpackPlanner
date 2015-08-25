///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Models/Personal/UserInformation.ts" />

module BackpackPlanner.Mockup.Resources.Personal {
    "use strict";

    export interface IUserInformation {
        FirstName: string;
        LastName: string;
        BirthDate: string;
        Sex: string;
        HeightInCm: number;
        WeightInGrams: number;
    }

    export interface IUserInformationResource extends IUserInformation, ng.resource.IResource<IUserInformation> {
    }
}
