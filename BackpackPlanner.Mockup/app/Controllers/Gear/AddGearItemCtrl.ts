///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IAddGearItemScope extends IAppScope {
        gearItem: Models.Gear.IGearItem;

        addItem: (gearItem: Models.Gear.IGearItem) => void;
    }

    export class AddGearItemCtrl {
        constructor($scope: IAddGearItemScope, $location: ng.ILocationService, $mdToast: ng.material.IToastService) {
            $scope.gearItem = Models.Gear.newGearItem();

            $scope.addItem = (gearItem) => {
                $scope.gearItem = angular.copy(gearItem);
                $scope.gearItem.Id = Models.Gear.getNextGearItemId();
                $scope.gearItems.push($scope.gearItem);

                var addToast = $mdToast.simple()
                    .content(`Added gear item: ${$scope.gearItem.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoAddToast = $mdToast.simple()
                    .content(`Removed gear item: ${$scope.gearItem.Name}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/items");
                $mdToast.show(addToast).then(() => {
                    Models.Gear.deleteGearItem($scope.gearItems, $scope.gearSystems, $scope.gearCollections, $scope.gearItem);
                    $mdToast.show(undoAddToast);
                });
            }
        }
    }

    AddGearItemCtrl.$inject = ["$scope", "$location", "$mdToast"];
}
