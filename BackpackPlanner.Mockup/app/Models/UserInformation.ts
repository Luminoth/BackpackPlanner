///<reference path="../Resources/UserInformationResource.ts"/>

module BackpackPlanner.Mockup.Models {
    "use strict";

    export enum Sex {
        NotSpecified,
        Male,
        Female
    }

    export interface IUserInformation {
        FirstName: string;
        LastName: string;
        BirthDate: Date;
        Sex: Sex;
        HeightInInches: number;
        WeightInOunces: number;
    }

    export class UserInformation implements IUserInformation {
        public FirstName = "";
        public LastName = "";
        public BirthDate = new Date();
        public Sex = Sex.NotSpecified;
        public HeightInInches = 0;
        public WeightInOunces = 0;

        constructor(userInfoResource?: Resources.IUserInformationResource) {
            if(userInfoResource) {
                this.FirstName = userInfoResource.FirstName;
                this.LastName = userInfoResource.LastName;
                this.BirthDate = userInfoResource.BirthDate;
                this.Sex = userInfoResource.Sex;
                this.HeightInInches = userInfoResource.HeightInInches;
                this.WeightInOunces = userInfoResource.WeightInOunces;
            }
        }
    }
}
