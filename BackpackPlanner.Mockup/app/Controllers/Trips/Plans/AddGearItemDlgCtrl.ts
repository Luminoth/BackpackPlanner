/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />

/// <reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddGearItemDlgScope extends ng.IScope {
        tripPlan: Models.Trips.TripPlan;

        filterName: string;
        orderBy: string;

        close: () => void;

        filterGearItem: (gearItem: Models.Gear.GearItem) => boolean;
        getGearItems: () => Models.Gear.GearItem[];
        isGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
        toggleGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
    }

    export class AddGearItemDlgCtrl {
        constructor($scope: IAddGearItemDlgScope, $mdDialog: ng.material.IDialogService, tripPlan: Models.Trips.TripPlan) {
            $scope.tripPlan = tripPlan;
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
                return $scope.tripPlan.containsGearItemById(gearItem.Id);
            }

            $scope.toggleGearItemSelected = (gearItem) => {
                if(!$scope.tripPlan.containsGearItemById(gearItem.Id)) {
                    try {
                        $scope.tripPlan.addGearItem(gearItem);
                    } catch(error) {
                        alert(error);
                    }
                } else {
                    if(!$scope.tripPlan.removeGearItemById(gearItem.Id)) {
                        alert("Cannot remove the item, it may be included by a collection or system or no longer exists.");
                    }
                } 
            };
        }
    }
}
