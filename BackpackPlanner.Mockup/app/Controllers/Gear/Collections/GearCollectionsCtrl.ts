///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IGearCollectionsScope extends IAppScope {
        orderBy: string;
    }

    export class GearCollectionsCtrl {
        constructor($scope: IGearCollectionsScope) {
            $scope.orderBy = "Name";
        }
    }
    
    GearCollectionsCtrl.$inject = ["$scope"];
}
