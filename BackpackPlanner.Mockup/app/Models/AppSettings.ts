///<reference path="../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../Resources/AppSettingsResource.ts"/>

module BackpackPlanner.Mockup.Models {
    "use strict";

    export interface IAppSettings {
        Units: string;
        Currency: string;
    }

    export class AppSettings implements IAppSettings {
        public Units = "Metric";
        public Currency = "USD";

        /* Load/Save */

        public loadFromDevice($q: ng.IQService, appSettingsResource: Resources.IAppSettingsResource) : ng.IPromise<any> {
            this.Units = appSettingsResource.Units;
            this.Currency = appSettingsResource.Currency;

            return $q.defer().promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("AppSettings.saveToDevice");
            return $q.defer().promise;
        }
    }
}
