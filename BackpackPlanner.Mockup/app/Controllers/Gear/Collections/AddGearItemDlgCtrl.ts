/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />

/// <reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IAddGearItemDlgScope extends ng.IScope {
        gearCollection: Models.Gear.GearCollection;

        filterName: string;
        orderBy: string;

        close: () => void;

        filterGearItem: (gearItem: Models.Gear.GearItem) => boolean;
        getGearItems: () => Models.Gear.GearItem[];
        isGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
        toggleGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
    }

    export class AddGearItemDlgCtrl {
        constructor($scope: IAddGearItemDlgScope, $mdDialog: ng.material.IDialogService, gearCollection: Models.Gear.GearCollection) {
            $scope.gearCollection = gearCollection;
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
                return $scope.gearCollection.containsGearItemById(gearItem.Id);
            }

            $scope.toggleGearItemSelected = (gearItem) => {
                if(!$scope.gearCollection.containsGearItemById(gearItem.Id)) {
                    try {
                        $scope.gearCollection.addGearItem(gearItem);
                    } catch(error) {
                        alert(error);
                    }
                } else {
                    if(!$scope.gearCollection.removeGearItemById(gearItem.Id)) {
                        alert("Cannot remove the item, it may be included by a system or no longer exists.");
                    }
                } 
            };
        }
    }
}
