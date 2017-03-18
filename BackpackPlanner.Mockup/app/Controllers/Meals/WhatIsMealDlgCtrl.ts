/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />

module BackpackPlanner.Mockup.Controllers.Meals {
    "use strict";

    export interface IWhatIsMealDlgScope extends ng.IScope {
        close: () => void;
    }

    export class WhatIsMealDlgCtrl {
        constructor($scope: IWhatIsMealDlgScope, $mdDialog: ng.material.IDialogService) {
            $scope.close = () => {
                $mdDialog.hide();
            };
        }
    }
}
