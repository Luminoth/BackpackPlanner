///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IGearSystemsScope extends IAppScope {
        orderBy: string;
    }

    export class GearSystemsCtrl {
        constructor($scope: IGearSystemsScope) {
            $scope.orderBy = "Name";
        }
    }
    
    GearSystemsCtrl.$inject = ["$scope"];
}
