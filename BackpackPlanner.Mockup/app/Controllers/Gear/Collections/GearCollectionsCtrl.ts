///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IGearCollectionsScope extends IAppScope {
        orderBy: string;

        showWhatIsGearCollectionDlg: (event: MouseEvent) => void;
    }

    export class GearCollectionsCtrl {
        constructor($scope: IGearCollectionsScope, $mdDialog: ng.material.IDialogService) {
            $scope.orderBy = "Name";

            $scope.showWhatIsGearCollectionDlg = (event) => {
                $mdDialog.show({
                    controller: WhatIsGearCollectionDlgCtrl,
                    templateUrl: "content/partials/gear/collections/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }
        }
    }
    
    GearCollectionsCtrl.$inject = ["$scope", "$mdDialog"];
}
