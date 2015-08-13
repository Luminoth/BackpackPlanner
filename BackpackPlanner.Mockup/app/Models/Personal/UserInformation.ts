﻿///<reference path="../../Resources/Personal/UserInformationResource.ts"/>

module BackpackPlanner.Mockup.Models.Personal {
    "use strict";

    export interface IUserInformation {
        FirstName: string;
        LastName: string;
        BirthDate: string;
        Sex: string;
        HeightInCm: number;
        WeightInGrams: number;
    }

    export class UserInformation implements IUserInformation {
        public FirstName = "";
        public LastName = "";
        public BirthDate = "";
        public Sex = "NotSpecified";
        public HeightInCm = 0;
        public WeightInGrams = 0;

        public BirthDateAsDate = new Date();

        constructor(userInfoResource?: Resources.Personal.IUserInformationResource) {
            if(userInfoResource) {
                this.FirstName = userInfoResource.FirstName;
                this.LastName = userInfoResource.LastName;
                this.BirthDate = userInfoResource.BirthDate;
                this.Sex = userInfoResource.Sex;
                this.HeightInCm = userInfoResource.HeightInCm;
                this.WeightInGrams = userInfoResource.WeightInGrams;

                this.BirthDateAsDate = new Date(this.BirthDate);
            }
        }

        public heightInUnits(height: number) : number {
            return arguments.length
                ? (this.HeightInCm = convertUnitsToCentimeters(height, AppState.getInstance().getAppSettings().Units))
                : parseFloat(convertCentimetersToUnits(this.HeightInCm, AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public weightInUnits(weight: number) : number {
            return arguments.length
                ? (this.WeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().Units))
                : parseFloat(convertGramsToUnits(this.WeightInGrams, AppState.getInstance().getAppSettings().Units).toFixed(2));
        }
    }
}