///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddGearItemDlgScope extends ng.IScope {
        tripPlan: Models.Trips.TripPlan;
        orderBy: string;

        getGearItems: () => Models.Gear.GearItem[];
        close: () => void;
        isSelected: (gearItem: Models.Gear.GearItem) => void;
        toggle: (gearItem: Models.Gear.GearItem) => void;
    }

    export class AddGearItemDlgCtrl {
        constructor($scope: IAddGearItemDlgScope, $mdDialog: ng.material.IDialogService, tripPlan: Models.Trips.TripPlan) {
            $scope.tripPlan = tripPlan;
            $scope.orderBy = "Name";

            $scope.getGearItems = () => {
                return AppState.getInstance().getGearState().getGearItems();
            }

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.isSelected = (gearItem) => {
                return $scope.tripPlan.containsGearItem(gearItem);
            }

            $scope.toggle = (gearItem) => {
                if(!$scope.tripPlan.containsGearItem(gearItem)) {
                    $scope.tripPlan.addGearItem(gearItem);
                } else {
                    $scope.tripPlan.removeGearItem(gearItem);
                } 
            };
        }
    }
}
