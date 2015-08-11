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

        constructor(appSettingsResource?: Resources.IAppSettingsResource) {
            if(appSettingsResource) {
                this.Units = appSettingsResource.Units;
                this.Currency = appSettingsResource.Currency;
            }
        }
    }
}
