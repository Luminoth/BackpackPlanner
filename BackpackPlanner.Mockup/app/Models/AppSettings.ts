/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />

/// <reference path="../Resources/AppSettingsResource.ts"/>

module BackpackPlanner.Mockup.Models {
    "use strict";

    export class AppSettings {
        private _units = "Metric";
        private _currency = "USD";

        // weight classes
        private _ultralightClassMaxWeightInGrams = 4500;
        private _lightweightClassMaxWeightInGrams = 9000;

        // weight categories
        private _ultralightCategoryMaxWeightInGrams = 225;
        private _lightCategoryMaxWeightInGrams = 450;
        private _mediumCategoryMaxWeightInGrams = 1360;
        private _heavyCategoryMaxWeightInGrams = 2270;

        public units(units?: string) {
            return arguments.length
                ? (this._units = units)
                : this._units;
        }

        public currency(currency?: string) {
            return arguments.length
                ? (this._currency = currency)
                : this._currency;
        }

        public getUltralightMaxWeightInGrams() {
            return this._ultralightClassMaxWeightInGrams;
        }

        public ultralightMaxWeightInUnits(weight?: number) {
            return arguments.length
                ? (this._ultralightClassMaxWeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().units()))
                : parseFloat(convertGramsToUnits(this._ultralightClassMaxWeightInGrams, AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getLightweightMaxWeightInGrams() {
            return this._lightweightClassMaxWeightInGrams;
        }

        public lightweightMaxWeightInUnits(weight?: number) {
            return arguments.length
                ? (this._lightweightClassMaxWeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().units()))
                : parseFloat(convertGramsToUnits(this._lightweightClassMaxWeightInGrams, AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public resetToDefaults() {
            this._units = "Metric";
            this._currency = "USD";

            this._ultralightClassMaxWeightInGrams = 4500;
            this._lightweightClassMaxWeightInGrams = 9000;

            this._ultralightCategoryMaxWeightInGrams = 225;
            this._lightCategoryMaxWeightInGrams = 450;
            this._mediumCategoryMaxWeightInGrams = 1360;
            this._heavyCategoryMaxWeightInGrams = 2270;
        }

        public getWeightClass(weightInGrams: number) {
            if(weightInGrams < this._ultralightClassMaxWeightInGrams) {
                return "Ultralight";
            } else if(weightInGrams < this._lightweightClassMaxWeightInGrams) {
                return "Lightweight";
            }
            return "Traditional";
        }

        public getWeightCategory(weightInGrams: number) {
            if(weightInGrams <= 0) {
                return "None";
            } else if(weightInGrams < this._ultralightCategoryMaxWeightInGrams) {
                return "Ultralight";
            } else if(weightInGrams < this._lightCategoryMaxWeightInGrams) {
                return "Light";
            } else if(weightInGrams < this._mediumCategoryMaxWeightInGrams) {
                return "Medium";
            } else if(weightInGrams < this._heavyCategoryMaxWeightInGrams) {
                return "Heavy";
            }
            return "ExtraHeavy";
        }

        /* Load/Save */

        public update(appSettings: AppSettings) {
            this._units = appSettings._units;
            this._currency = appSettings._currency;

            this._ultralightClassMaxWeightInGrams = appSettings._ultralightClassMaxWeightInGrams;
            this._lightweightClassMaxWeightInGrams = appSettings._lightweightClassMaxWeightInGrams;

            this._ultralightCategoryMaxWeightInGrams = appSettings._ultralightCategoryMaxWeightInGrams;
            this._lightCategoryMaxWeightInGrams = appSettings._lightCategoryMaxWeightInGrams;
            this._mediumCategoryMaxWeightInGrams = appSettings._mediumCategoryMaxWeightInGrams;
            this._heavyCategoryMaxWeightInGrams = appSettings._heavyCategoryMaxWeightInGrams;
        }

        public loadFromDevice($q: ng.IQService, appSettingsResource: Resources.IAppSettingsResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this._units = appSettingsResource.Units;
            this._currency = appSettingsResource.Currency;

            this._ultralightClassMaxWeightInGrams = appSettingsResource.UltralightClassMaxWeightInGrams;
            this._lightweightClassMaxWeightInGrams = appSettingsResource.LightweightClassMaxWeightInGrams;

            this._ultralightCategoryMaxWeightInGrams = appSettingsResource.UltralightCategoryMaxWeightInGrams;
            this._lightCategoryMaxWeightInGrams = appSettingsResource.LightCategoryMaxWeightInGrams;
            this._mediumCategoryMaxWeightInGrams = appSettingsResource.MediumCategoryMaxWeightInGrams;
            this._heavyCategoryMaxWeightInGrams = appSettingsResource.HeavyCategoryMaxWeightInGrams;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("AppSettings.saveToDevice");
            return $q.defer().promise;
        }
    }
}
