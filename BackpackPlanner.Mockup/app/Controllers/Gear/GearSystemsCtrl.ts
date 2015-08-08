///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IGearSystemsScope extends IAppScope {
        orderBy: string;

        getWeightInOunces: (gearSystem: Models.Gear.IGearSystem) => number;
        getCostInUSD: (gearSystem: Models.Gear.IGearSystem) => number;
    }

    export class GearSystemsCtrl {
        constructor($scope: IGearSystemsScope) {
            $scope.orderBy = "Name";

            $scope.getWeightInOunces = (gearSystem) => {
                return Models.Gear.getGearSystemWeightInOunces(gearSystem, $scope.gearItems);
            };

            $scope.getCostInUSD = (gearSystem) => {
                return Models.Gear.getGearSystemCostInUSD(gearSystem, $scope.gearItems);
            };
        }
    }
    
    GearSystemsCtrl.$inject = ["$scope"];
}
