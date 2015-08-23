///<reference path="../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../Resources/AppSettingsResource.ts"/>

module BackpackPlanner.Mockup.Models {
    "use strict";

    export interface IAppSettings {
        Units: string;
        Currency: string;

        UltralightMaxWeightInGrams: number;
        LightweightMaxWeightInGrams: number;
    }

    export class AppSettings implements IAppSettings {
        public Units = "Metric";
        public Currency = "USD";
        public UltralightMaxWeightInGrams = 4500;
        public LightweightMaxWeightInGrams = 9000;

        public resetToDefaults() {
            this.Units = "Metric";
            this.Currency = "USD";
            this.UltralightMaxWeightInGrams = 4500;
            this.LightweightMaxWeightInGrams = 9000;
        }

        public getWeightClass(weightInGrams: number) {
            if(weightInGrams < this.UltralightMaxWeightInGrams) {
                return "Ultralight";
            } else if(weightInGrams < this.LightweightMaxWeightInGrams) {
                return "Lightweight";
            }
            return "Traditional";
        }

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

        public ultralightMaxWeightInUnits(weight?: number) : number {
            return arguments.length
                ? (this.UltralightMaxWeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().Units))
                : parseFloat(convertGramsToUnits(this.UltralightMaxWeightInGrams, AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public lightweightMaxWeightInUnits(weight?: number) : number {
            return arguments.length
                ? (this.LightweightMaxWeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().Units))
                : parseFloat(convertGramsToUnits(this.LightweightMaxWeightInGrams, AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        /* Load/Save */

        public update(appSettings: AppSettings) {
            this.Units = appSettings.Units;
            this.Currency = appSettings.Currency;
            this.UltralightMaxWeightInGrams = appSettings.UltralightMaxWeightInGrams;
            this.LightweightMaxWeightInGrams = appSettings.LightweightMaxWeightInGrams;
        }

        public loadFromDevice($q: ng.IQService, appSettingsResource: Resources.IAppSettingsResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this.Units = appSettingsResource.Units;
            this.Currency = appSettingsResource.Currency;
            this.UltralightMaxWeightInGrams = appSettingsResource.UltralightMaxWeightInGrams;
            this.LightweightMaxWeightInGrams = appSettingsResource.LightweightMaxWeightInGrams;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("AppSettings.saveToDevice");
            return $q.defer().promise;
        }
    }
}
