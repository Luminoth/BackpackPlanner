///<reference path="../../Resources/Personal/UserInformationResource.ts"/>

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

        /* Height/Weight */

        public heightInUnits(height?: number) : number {
            return arguments.length
                ? (this.HeightInCm = convertUnitsToCentimeters(height, AppState.getInstance().getAppSettings().Units))
                : parseFloat(convertCentimetersToUnits(this.HeightInCm, AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public weightInUnits(weight?: number) : number {
            return arguments.length
                ? (this.WeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().Units))
                : parseFloat(convertGramsToUnits(this.WeightInGrams, AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        /* Load/Save */

        public update(userInformation: UserInformation) {
            this.FirstName = userInformation.FirstName;
            this.LastName = userInformation.LastName;
            this.BirthDateAsDate = userInformation.BirthDateAsDate;
            this.BirthDate = this.BirthDateAsDate.toString();
            this.Sex = userInformation.Sex;
            this.HeightInCm = userInformation.HeightInCm;
            this.WeightInGrams = userInformation.WeightInGrams;
        }

        public loadFromDevice($q: ng.IQService, userInfoResource: Resources.Personal.IUserInformationResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this.FirstName = userInfoResource.FirstName;
            this.LastName = userInfoResource.LastName;
            this.BirthDate = userInfoResource.BirthDate;
            this.BirthDateAsDate = new Date(this.BirthDate);
            this.Sex = userInfoResource.Sex;
            this.HeightInCm = userInfoResource.HeightInCm;
            this.WeightInGrams = userInfoResource.WeightInGrams;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("UserInformation.saveToDevice");
            return $q.defer().promise;
        }
    }
}
