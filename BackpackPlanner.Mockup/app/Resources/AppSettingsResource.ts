/// <reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

/// <reference path="../Models/AppSettings.ts" />

module BackpackPlanner.Mockup.Resources {
    "use strict";

    export interface IAppSettings {
        Units: string;
        Currency: string;

        UltralightClassMaxWeightInGrams: number;
        LightweightClassMaxWeightInGrams: number;

        UltralightCategoryMaxWeightInGrams: number;
        LightCategoryMaxWeightInGrams: number;
        MediumCategoryMaxWeightInGrams: number;
        HeavyCategoryMaxWeightInGrams: number;
    }

    export interface IAppSettingsResource extends IAppSettings, ng.resource.IResource<IAppSettings> {
    }
}
