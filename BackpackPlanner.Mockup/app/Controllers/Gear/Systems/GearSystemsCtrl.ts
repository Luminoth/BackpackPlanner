///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Systems {
    "use strict";

    export interface IGearSystemsScope extends IAppScope {
        orderBy: string;

        showWhatIsGearSystemDlg: (event: MouseEvent) => void;
    }

    export class GearSystemsCtrl {
        constructor($scope: IGearSystemsScope, $mdDialog: ng.material.IDialogService) {
            $scope.orderBy = "Name";

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
