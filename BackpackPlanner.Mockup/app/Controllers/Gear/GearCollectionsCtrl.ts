///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IGearCollectionsScope extends IAppScope {
        orderBy: string;

        getWeightInOunces: (gearCollection: Models.Gear.IGearCollection) => number;
        getCostInUSD: (gearCollection: Models.Gear.IGearCollection) => number;
    }

    export class GearCollectionsCtrl {
        constructor($scope: IGearCollectionsScope) {
            $scope.orderBy = "Name";

            $scope.getWeightInOunces = (gearCollection) => {
                return Models.Gear.getGearCollectionWeightInOunces(gearCollection, $scope.gearSystems, $scope.gearItems);
            };

            $scope.getCostInUSD = (gearCollection) => {
                return Models.Gear.getGearCollectionCostInUSD(gearCollection, $scope.gearSystems, $scope.gearItems);
            };
        }
    }
    
    GearCollectionsCtrl.$inject = ["$scope"];
}
