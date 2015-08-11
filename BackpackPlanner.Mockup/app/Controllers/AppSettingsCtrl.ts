///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../Models/AppSettings.ts" />

module BackpackPlanner.Mockup.Controllers {
    "use strict";

    export interface IAppSettingsScope extends ng.IScope {
        getAppSettings: () => Models.AppSettings;
    }

    export class AppSettingsCtrl {
        constructor($scope: IAppSettingsScope) {
            $scope.getAppSettings = () => {
                return AppState.getInstance().getAppSettings();
            }
        }
    }

    AppSettingsCtrl.$inject = ["$scope"];
}
