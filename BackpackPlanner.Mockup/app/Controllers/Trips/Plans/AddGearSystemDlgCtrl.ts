///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddGearSystemDlgScope extends ng.IScope {
        tripPlan: Models.Trips.TripPlan;

        orderBy: string;

        close: () => void;

        getGearSystems: () => Models.Gear.GearSystem[];
        isGearSystemSelected: (gearSystem: Models.Gear.GearSystem) => void;
        toggleGearSystemSelected: (gearSystem: Models.Gear.GearSystem) => void;
    }

    export class AddGearSystemDlgCtrl {
        constructor($scope: IAddGearSystemDlgScope, $mdDialog: ng.material.IDialogService, tripPlan: Models.Trips.TripPlan) {
            $scope.tripPlan = tripPlan;
            $scope.orderBy = "Name";

            $scope.getGearSystems = () => {
                return AppState.getInstance().getGearState().getGearSystems();
            }

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.isGearSystemSelected = (gearSystem) => {
                return $scope.tripPlan.containsGearSystem(gearSystem);
            }

            $scope.toggleGearSystemSelected = (gearSystem) => {
                if(!$scope.tripPlan.containsGearSystem(gearSystem)) {
                    $scope.tripPlan.addGearSystem(gearSystem);
                } else {
                    $scope.tripPlan.removeGearSystem(gearSystem);
                } 
            };
        }
    }
}
