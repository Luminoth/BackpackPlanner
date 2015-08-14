///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IAddGearItemDlgScope extends ng.IScope {
        gearCollection: Models.Gear.GearCollection;

        orderBy: string;

        close: () => void;

        getGearItems: () => Models.Gear.GearItem[];
        isGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
        toggleGearItemSelected: (gearItem: Models.Gear.GearItem) => void;
    }

    export class AddGearItemDlgCtrl {
        constructor($scope: IAddGearItemDlgScope, $mdDialog: ng.material.IDialogService, gearCollection: Models.Gear.GearCollection) {
            $scope.gearCollection = gearCollection;
            $scope.orderBy = "Name";

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.getGearItems = () => {
                return AppState.getInstance().getGearState().getGearItems();
            }

            $scope.isGearItemSelected = (gearItem) => {
                return $scope.gearCollection.containsGearItem(gearItem);
            }

            $scope.toggleGearItemSelected = (gearItem) => {
                if(!$scope.gearCollection.containsGearItem(gearItem)) {
                    $scope.gearCollection.addGearItem(gearItem);
                } else {
                    $scope.gearCollection.removeGearItem(gearItem);
                } 
            };
        }
    }
}
