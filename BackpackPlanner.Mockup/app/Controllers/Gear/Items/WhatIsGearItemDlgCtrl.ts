/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Items {
    "use strict";

    export interface IWhatIsGearItemDlgScope extends ng.IScope {
        close: () => void;
    }

    export class WhatIsGearItemDlgCtrl {
        constructor($scope: IWhatIsGearItemDlgScope, $mdDialog: ng.material.IDialogService) {
            $scope.close = () => {
                $mdDialog.hide();
            };
        }
    }
}
