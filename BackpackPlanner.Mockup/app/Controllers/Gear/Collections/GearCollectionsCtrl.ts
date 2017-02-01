///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IGearCollectionsScope extends IAppScope {
        filterName: string;
        orderBy: string;

        filterGearCollection: (gearCollection: Models.Gear.GearCollection) => boolean;
        showWhatIsGearCollectionDlg: (event: MouseEvent) => void;
    }

    export class GearCollectionsCtrl {
        constructor($scope: IGearCollectionsScope, $mdDialog: ng.material.IDialogService) {
            $scope.filterName = "";
            $scope.orderBy = "name()";

            $scope.filterGearCollection = (gearCollection) => {
                if($scope.filterName) {
                    return gearCollection.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                }
                return true;
            }

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
