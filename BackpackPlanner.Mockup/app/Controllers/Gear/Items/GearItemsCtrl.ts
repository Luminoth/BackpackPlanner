///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Items {
    "use strict";

    export interface IGearItemsScope extends IAppScope {
        filterName: string;
        orderBy: string;

        filterGearItem: (gearItem: Models.Gear.GearItem) => boolean;
        showWhatIsGearItemDlg: (event: MouseEvent) => void;
    }

    export class GearItemsCtrl {
        constructor($scope: IGearItemsScope, $mdDialog: ng.material.IDialogService) {
            $scope.filterName = "";
            $scope.orderBy = "name()";

            $scope.filterGearItem = (gearItem) => {
                if($scope.filterName) {
                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                }
                return true;
            }

            $scope.showWhatIsGearItemDlg = (event) => {
                $mdDialog.show({
                    controller: WhatIsGearItemDlgCtrl,
                    templateUrl: "content/partials/gear/items/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }
        }
    }
    
    GearItemsCtrl.$inject = ["$scope", "$mdDialog"];
}
