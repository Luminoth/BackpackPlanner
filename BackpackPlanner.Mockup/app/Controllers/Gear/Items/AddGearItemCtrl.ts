///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Items {
    "use strict";

    export interface IAddGearItemScope extends IAppScope {
        gearItem: Models.Gear.GearItem;

        addGearItem: () => void;
        resetGearItem: () => void;
    }

    export class AddGearItemCtrl {
        constructor($scope: IAddGearItemScope, $location: ng.ILocationService, $mdToast: ng.material.IToastService) {
            $scope.gearItem = new Models.Gear.GearItem();

            $scope.addGearItem = () => {
                AppState.getInstance().getGearState().addGearItem($scope.gearItem);

                var addToast = $mdToast.simple()
                    .content(`Added gear item: ${$scope.gearItem.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/items");
                $mdToast.show(addToast);
            }

            $scope.resetGearItem = () => {
                $scope.gearItem = new Models.Gear.GearItem();
            }
        }
    }

    AddGearItemCtrl.$inject = ["$scope", "$location", "$mdToast"];
}
