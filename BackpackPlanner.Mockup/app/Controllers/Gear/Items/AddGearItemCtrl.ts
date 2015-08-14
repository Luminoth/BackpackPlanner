﻿///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
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
                    .content(`Added gear item: ${$scope.gearItem.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoAddToast = $mdToast.simple()
                    .content(`Removed gear item: ${$scope.gearItem.Name}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/items");
                $mdToast.show(addToast).then((response: string) => {
                    if("ok" == response) {
                        AppState.getInstance().getGearState().deleteGearItem($scope.gearItem);
                        $mdToast.show(undoAddToast);
                    }
                });
            }

            $scope.resetGearItem = () => {
                $scope.gearItem = new Models.Gear.GearItem();
            }
        }
    }

    AddGearItemCtrl.$inject = ["$scope", "$location", "$mdToast"];
}
