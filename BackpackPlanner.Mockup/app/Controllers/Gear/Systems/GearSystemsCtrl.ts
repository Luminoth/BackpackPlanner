///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Systems {
    "use strict";

    export interface IGearSystemsScope extends IAppScope {
        filterName: string;
        orderBy: string;

        filterGearSystem: (gearSystem: Models.Gear.GearSystem) => boolean;
        showWhatIsGearSystemDlg: (event: MouseEvent) => void;
    }

    export class GearSystemsCtrl {
        constructor($scope: IGearSystemsScope, $mdDialog: ng.material.IDialogService) {
            $scope.filterName = "";
            $scope.orderBy = "name()";

            $scope.filterGearSystem = (gearSystem) => {
                if($scope.filterName) {
                    return gearSystem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                }
                return true;
            }

            $scope.showWhatIsGearSystemDlg = (event) => {
                $mdDialog.show({
                    controller: WhatIsGearSystemDlgCtrl,
                    templateUrl: "content/partials/gear/systems/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }
        }
    }
    
    GearSystemsCtrl.$inject = ["$scope", "$mdDialog"];
}
