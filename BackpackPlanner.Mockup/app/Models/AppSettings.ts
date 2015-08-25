///<reference path="../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../Resources/AppSettingsResource.ts"/>

module BackpackPlanner.Mockup.Models {
    "use strict";

    export class AppSettings {
        private _units = "Metric";
        private _currency = "USD";
        private _ultralightMaxWeightInGrams = 4500;
        private _lightweightMaxWeightInGrams = 9000;

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
            return this._ultralightMaxWeightInGrams;
        }

        public ultralightMaxWeightInUnits(weight?: number) {
            return arguments.length
                ? (this._ultralightMaxWeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().units()))
                : parseFloat(convertGramsToUnits(this._ultralightMaxWeightInGrams, AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getLightweightMaxWeightInGrams() {
            return this._lightweightMaxWeightInGrams;
        }

        public lightweightMaxWeightInUnits(weight?: number) {
            return arguments.length
                ? (this._lightweightMaxWeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().units()))
                : parseFloat(convertGramsToUnits(this._lightweightMaxWeightInGrams, AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public resetToDefaults() {
            this._units = "Metric";
            this._currency = "USD";
            this._ultralightMaxWeightInGrams = 4500;
            this._lightweightMaxWeightInGrams = 9000;
        }

        public getWeightClass(weightInGrams: number) {
            if(weightInGrams < this._ultralightMaxWeightInGrams) {
                return "Ultralight";
            } else if(weightInGrams < this._lightweightMaxWeightInGrams) {
                return "Lightweight";
            }
            return "Traditional";
        }

        // TODO: make these values configurable
        public getWeightCategory(weightInGrams: number) {
            if(weightInGrams <= 0) {
                return "None";
            } else if(weightInGrams < 225) {
                return "Ultralight";
            } else if(weightInGrams < 450) {
                return "Light";
            } else if(weightInGrams < 1360) {
                return "Medium";
            } else if(weightInGrams < 2270) {
                return "Heavy";
            }
            return "ExtraHeavy";
        }

        /* Load/Save */

        public update(appSettings: AppSettings) {
            this._units = appSettings._units;
            this._currency = appSettings._currency;
            this._ultralightMaxWeightInGrams = appSettings._ultralightMaxWeightInGrams;
            this._lightweightMaxWeightInGrams = appSettings._lightweightMaxWeightInGrams;
        }

        public loadFromDevice($q: ng.IQService, appSettingsResource: Resources.IAppSettingsResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this._units = appSettingsResource.Units;
            this._currency = appSettingsResource.Currency;
            this._ultralightMaxWeightInGrams = appSettingsResource.UltralightMaxWeightInGrams;
            this._lightweightMaxWeightInGrams = appSettingsResource.LightweightMaxWeightInGrams;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("AppSettings.saveToDevice");
            return $q.defer().promise;
        }
    }
}
