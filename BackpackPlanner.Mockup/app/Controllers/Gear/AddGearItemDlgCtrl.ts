///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IAddGearItemDlgScope extends ng.IScope {
        gearItems: Models.Gear.IGearItem[];
        gearSystem: Models.Gear.IGearSystem;
        orderBy: string;

        close: () => void;
        isSelected: (gearItem: Models.Gear.IGearItem) => void;
        toggle: (gearItem: Models.Gear.IGearItem) => void;
    }

    export class AddGearItemDlgCtrl {
        constructor($scope: IAddGearItemDlgScope, $mdDialog: ng.material.IDialogService, gearItems: Models.Gear.IGearItem[], gearSystem: Models.Gear.IGearSystem) {
            $scope.gearItems = gearItems;
            $scope.gearSystem = gearSystem;
            $scope.orderBy = "Name";

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.isSelected = (gearItem) => {
                return $scope.gearSystem.GearItems.indexOf(gearItem.Id) >= 0;
            }

            $scope.toggle = (gearItem) => {
                var idx = $scope.gearSystem.GearItems.indexOf(gearItem.Id);
                if(idx < 0) {
                    $scope.gearSystem.GearItems.push(gearItem.Id);
                } else {
                    $scope.gearSystem.GearItems.splice(idx, 1);
                } 
            };
        }
    }
}