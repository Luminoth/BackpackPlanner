///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Items {
    "use strict";

    export interface IGearItemsScope extends IAppScope {
        orderBy: string;

        showWhatIsGearItem: (event: MouseEvent) => void;
    }

    export class GearItemsCtrl {
        constructor($scope: IGearItemsScope, $mdDialog: ng.material.IDialogService) {
            $scope.orderBy = "Name";

            $scope.showWhatIsGearItem = (event) => {
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
