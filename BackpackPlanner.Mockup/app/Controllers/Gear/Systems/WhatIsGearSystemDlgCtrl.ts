///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Systems {
    "use strict";

    export interface IWhatIsGearSystemDlgScope extends ng.IScope {
        close: () => void;
    }

    export class WhatIsGearSystemDlgCtrl {
        constructor($scope: IWhatIsGearSystemDlgScope, $mdDialog: ng.material.IDialogService) {
            $scope.close = () => {
                $mdDialog.hide();
            };
        }
    }
}
