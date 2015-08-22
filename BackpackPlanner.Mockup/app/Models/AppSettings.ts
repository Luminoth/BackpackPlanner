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

        public update(appSettings: AppSettings) {
            this.Units = appSettings.Units;
            this.Currency = appSettings.Currency;
        }

        public loadFromDevice($q: ng.IQService, appSettingsResource: Resources.IAppSettingsResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this.Units = appSettingsResource.Units;
            this.Currency = appSettingsResource.Currency;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("AppSettings.saveToDevice");
            return $q.defer().promise;
        }
    }
}
