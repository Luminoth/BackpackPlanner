///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Items {
    "use strict";

    export interface IGearItemsScope extends IAppScope {
        orderBy: string;
    }

    export class GearItemsCtrl {
        constructor($scope: IGearItemsScope) {
            $scope.orderBy = "Name";
        }
    }
    
    GearItemsCtrl.$inject = ["$scope"];
}
