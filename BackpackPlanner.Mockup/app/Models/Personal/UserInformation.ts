///<reference path="../../Resources/Personal/UserInformationResource.ts"/>

module BackpackPlanner.Mockup.Models.Personal {
    "use strict";

    export class UserInformation {
        private _firstName = "";
        private _lastName = "";
        private _birthDate = new Date();
        private _sex = "NotSpecified";
        private _heightInCm = 0;
        private _weightInGrams = 0;

        public firstName(firstName?: string) {
            return arguments.length
                ? (this._firstName = firstName)
                : this._firstName;
        }

        public lastName(lastName?: string) {
            return arguments.length
                ? (this._lastName = lastName)
                : this._lastName;
        }

        public birthDate(birthDate?: Date) {
            return arguments.length
                ? (this._birthDate = birthDate)
                : this._birthDate;
        }

        public sex(sex?: string) {
            return arguments.length
                ? (this._sex = sex)
                : this._sex;
        }

        /* Height/Weight */

        public heightInUnits(height?: number) {
            return arguments.length
                ? (this._heightInCm = convertUnitsToCentimeters(height, AppState.getInstance().getAppSettings().units()))
                : parseFloat(convertCentimetersToUnits(this._heightInCm, AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public weightInUnits(weight?: number) {
            return arguments.length
                ? (this._weightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().units()))
                : parseFloat(convertGramsToUnits(this._weightInGrams, AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        /* Load/Save */

        public update(userInformation: UserInformation) {
            this._firstName = userInformation._firstName;
            this._lastName = userInformation._lastName;
            this._birthDate = userInformation._birthDate;
            this._sex = userInformation._sex;
            this._heightInCm = userInformation._heightInCm;
            this._weightInGrams = userInformation._weightInGrams;
        }

        public loadFromDevice($q: ng.IQService, userInfoResource: Resources.Personal.IUserInformationResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this._firstName = userInfoResource.FirstName;
            this._lastName = userInfoResource.LastName;
            this._birthDate = new Date(userInfoResource.BirthDate);
            this._sex = userInfoResource.Sex;
            this._heightInCm = userInfoResource.HeightInCm;
            this._weightInGrams = userInfoResource.WeightInGrams;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("UserInformation.saveToDevice");
            return $q.defer().promise;
        }
    }
}
