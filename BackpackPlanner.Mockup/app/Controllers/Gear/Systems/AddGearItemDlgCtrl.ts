/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />

/// <reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Systems {
    "use strict";

    export interface IAddGearItemDlgScope extends ng.IScope {
        gearSystem: Models.Gear.GearSystem;

        filterName: string;
        orderBy: string;

        close: () => void;

        filterGearItem: (gearItem: Models.Gear.GearItem) => boolean;
        getGearItems: () => Models.Gear.GearItem[];
        isGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
        toggleGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
    }

    export class AddGearItemDlgCtrl {
        constructor($scope: IAddGearItemDlgScope, $mdDialog: ng.material.IDialogService, gearSystem: Models.Gear.GearSystem) {
            $scope.gearSystem = gearSystem;
            $scope.filterName = "";
            $scope.orderBy = "name()";

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.filterGearItem = (gearItem) => {
                if($scope.filterName) {
                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                }
                return true;
            }

            $scope.getGearItems = () => {
                return AppState.getInstance().getGearState().getGearItems();
            }

            $scope.isGearItemSelected = (gearItem) => {
                return $scope.gearSystem.containsGearItemById(gearItem.Id);
            }

            $scope.toggleGearItemSelected = (gearItem) => {
                if(!$scope.gearSystem.containsGearItemById(gearItem.Id)) {
                    try {
                        $scope.gearSystem.addGearItem(gearItem);
                    } catch(error) {
                        alert(error);
                    }
                } else {
                    if(!$scope.gearSystem.removeGearItemById(gearItem.Id)) {
                        alert("Cannot remove the item, it may no longer exist.");
                    }
                } 
            };
        }
    }
}
