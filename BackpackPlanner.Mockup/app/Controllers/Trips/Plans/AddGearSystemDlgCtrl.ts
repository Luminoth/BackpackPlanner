///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddGearSystemDlgScope extends ng.IScope {
        tripPlan: Models.Trips.TripPlan;

        filterName: string;
        orderBy: string;

        close: () => void;

        filterGearSystem: (gearSystem: Models.Gear.GearSystem) => boolean;
        getGearSystems: () => Models.Gear.GearSystem[];
        isGearSystemSelected: (gearSystem: Models.Gear.GearSystem) => void;
        toggleGearSystemSelected: (gearSystem: Models.Gear.GearSystem) => void;
    }

    export class AddGearSystemDlgCtrl {
        constructor($scope: IAddGearSystemDlgScope, $mdDialog: ng.material.IDialogService, tripPlan: Models.Trips.TripPlan) {
            $scope.tripPlan = tripPlan;
            $scope.filterName = "";
            $scope.orderBy = "name()";

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.filterGearSystem = (gearSystem) => {
                if($scope.filterName) {
                    return gearSystem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                }
                return true;
            }

            $scope.getGearSystems = () => {
                return AppState.getInstance().getGearState().getGearSystems();
            }

            $scope.isGearSystemSelected = (gearSystem) => {
                return $scope.tripPlan.containsGearSystemById(gearSystem.Id);
            }

            $scope.toggleGearSystemSelected = (gearSystem) => {
                if(!$scope.tripPlan.containsGearSystemById(gearSystem.Id)) {
                    $scope.tripPlan.addGearSystem(gearSystem);
                } else {
                    $scope.tripPlan.removeGearSystemById(gearSystem.Id);
                } 
            };
        }
    }
}
