///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../Models/AppSettings.ts" />

module BackpackPlanner.Mockup.Resources {
    "use strict";

    export interface IAppSettings {
        Units: string;
        Currency: string;

        UltralightMaxWeightInGrams: number;
        LightweightMaxWeightInGrams: number;
    }

    export interface IAppSettingsResource extends IAppSettings, ng.resource.IResource<IAppSettings> {
    }
}
