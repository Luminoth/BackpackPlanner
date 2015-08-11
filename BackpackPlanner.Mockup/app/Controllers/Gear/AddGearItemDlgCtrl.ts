///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IAddGearItemDlgScope extends ng.IScope {
        gearSystem: Models.Gear.GearSystem;
        orderBy: string;

        getGearItems: () => Models.Gear.GearItem[];
        close: () => void;
        isSelected: (gearItem: Models.Gear.GearItem) => void;
        toggle: (gearItem: Models.Gear.GearItem) => void;
    }

    export class AddGearItemDlgCtrl {
        constructor($scope: IAddGearItemDlgScope, $mdDialog: ng.material.IDialogService, gearSystem: Models.Gear.GearSystem) {
            $scope.gearSystem = gearSystem;
            $scope.orderBy = "Name";

            $scope.getGearItems = () => {
                return AppState.getInstance().getGearState().getGearItems();
            }

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.isSelected = (gearItem) => {
                return $scope.gearSystem.getGearItemEntryIndexById(gearItem.Id) >= 0;
            }

            $scope.toggle = (gearItem) => {
                var idx = $scope.gearSystem.getGearItemEntryIndexById(gearItem.Id);
                if(idx < 0) {
                    $scope.gearSystem.GearItems.push(new Models.Gear.GearItemEntry(gearItem.Id));
                } else {
                    $scope.gearSystem.GearItems.splice(idx, 1);
                } 
            };
        }
    }
}
