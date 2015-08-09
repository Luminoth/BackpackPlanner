///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../Models/AppSettings.ts" />

module BackpackPlanner.Mockup.Resources {
    "use strict";

    export interface IAppSettingsResource extends Models.IAppSettings, ng.resource.IResource<Models.IAppSettings> {
    }
}