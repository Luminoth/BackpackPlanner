///<reference path="../Resources/AppSettingsResource.ts"/>

module BackpackPlanner.Mockup.Models {
    "use strict";

    export enum Units {
        Imperial,
        Metric
    }

    export interface IAppSettings {
        Units: Units;
    }

    export class AppSettings implements IAppSettings {
        public Units = Units.Imperial;

        constructor(appSettingsResource?: Resources.IAppSettingsResource) {
            if(appSettingsResource) {
                this.Units = appSettingsResource.Units;
            }
        }
    }
}
