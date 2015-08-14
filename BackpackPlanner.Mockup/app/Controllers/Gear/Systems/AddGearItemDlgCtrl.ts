///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Systems {
    "use strict";

    export interface IAddGearItemDlgScope extends ng.IScope {
        gearSystem: Models.Gear.GearSystem;

        orderBy: string;

        close: () => void;

        getGearItems: () => Models.Gear.GearItem[];
        isGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
        toggleGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
    }

    export class AddGearItemDlgCtrl {
        constructor($scope: IAddGearItemDlgScope, $mdDialog: ng.material.IDialogService, gearSystem: Models.Gear.GearSystem) {
            $scope.gearSystem = gearSystem;
            $scope.orderBy = "Name";

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.getGearItems = () => {
                return AppState.getInstance().getGearState().getGearItems();
            }

            $scope.isGearItemSelected = (gearItem) => {
                return $scope.gearSystem.containsGearItem(gearItem);
            }

            $scope.toggleGearItemSelected = (gearItem) => {
                if(!$scope.gearSystem.containsGearItem(gearItem)) {
                    $scope.gearSystem.addGearItem(gearItem);
                } else {
                    $scope.gearSystem.removeGearItem(gearItem);
                } 
            };
        }
    }
}
