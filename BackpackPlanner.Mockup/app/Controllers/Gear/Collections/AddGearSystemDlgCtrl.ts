///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IAddGearSystemDlgScope extends ng.IScope {
        gearCollection: Models.Gear.GearCollection;
        orderBy: string;

        getGearSystems: () => Models.Gear.GearSystem[];
        close: () => void;
        isSelected: (gearSystem: Models.Gear.GearSystem) => void;
        toggle: (gearSystem: Models.Gear.GearSystem) => void;
    }

    export class AddGearSystemDlgCtrl {
        constructor($scope: IAddGearSystemDlgScope, $mdDialog: ng.material.IDialogService, gearCollection: Models.Gear.GearCollection) {
            $scope.gearCollection = gearCollection;
            $scope.orderBy = "Name";

            $scope.getGearSystems = () => {
                return AppState.getInstance().getGearState().getGearSystems();
            }

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.isSelected = (gearSystem) => {
                return $scope.gearCollection.containsGearSystem(gearSystem);
            }

            $scope.toggle = (gearSystem) => {
                if(!$scope.gearCollection.containsGearSystem(gearSystem)) {
                    $scope.gearCollection.addGearSystem(gearSystem);
                } else {
                    $scope.gearCollection.removeGearSystem(gearSystem);
                } 
            };
        }
    }
}