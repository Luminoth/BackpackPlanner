///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IAddGearItemDlgScope extends ng.IScope {
        gearCollection: Models.Gear.GearCollection;
        orderBy: string;

        getGearItems: () => Models.Gear.GearItem[];
        close: () => void;
        isSelected: (gearItem: Models.Gear.GearItem) => void;
        toggle: (gearItem: Models.Gear.GearItem) => void;
    }

    export class AddGearItemDlgCtrl {
        constructor($scope: IAddGearItemDlgScope, $mdDialog: ng.material.IDialogService, gearCollection: Models.Gear.GearCollection) {
            $scope.gearCollection = gearCollection;
            $scope.orderBy = "Name";

            $scope.getGearItems = () => {
                return AppState.getInstance().getGearState().getGearItems();
            }

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.isSelected = (gearItem) => {
                return $scope.gearCollection.getGearItemEntryIndexById(gearItem.Id) >= 0;
            }

            $scope.toggle = (gearItem) => {
                var idx = $scope.gearCollection.getGearItemEntryIndexById(gearItem.Id);
                if(idx < 0) {
                    $scope.gearCollection.GearItems.push(new Models.Gear.GearItemEntry(gearItem.Id));
                } else {
                    $scope.gearCollection.GearItems.splice(idx, 1);
                } 
            };
        }
    }
}
