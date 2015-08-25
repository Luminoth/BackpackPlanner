///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IAddGearSystemDlgScope extends ng.IScope {
        gearCollection: Models.Gear.GearCollection;

        filterName: string;
        orderBy: string;

        close: () => void;

        filterGearSystem: (gearSystem: Models.Gear.GearSystem) => boolean;
        getGearSystems: () => Models.Gear.GearSystem[];
        isGearSystemSelected: (gearSystem: Models.Gear.GearSystem) => void;
        toggleGearSystemSelected: (gearSystem: Models.Gear.GearSystem) => void;
    }

    export class AddGearSystemDlgCtrl {
        constructor($scope: IAddGearSystemDlgScope, $mdDialog: ng.material.IDialogService, gearCollection: Models.Gear.GearCollection) {
            $scope.gearCollection = gearCollection;
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
                return $scope.gearCollection.containsGearSystemById(gearSystem.Id);
            }

            $scope.toggleGearSystemSelected = (gearSystem) => {
                if(!$scope.gearCollection.containsGearSystemById(gearSystem.Id)) {
                    $scope.gearCollection.addGearSystem(gearSystem);
                } else {
                    $scope.gearCollection.removeGearSystemById(gearSystem.Id);
                } 
            };
        }
    }
}
