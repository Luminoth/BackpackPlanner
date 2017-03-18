/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />

module BackpackPlanner.Mockup.Controllers.Personal {
    "use strict";

    export interface IWhatIsPersonalDlgScope extends ng.IScope {
        close: () => void;
    }

    export class WhatIsPersonalDlgCtrl {
        constructor($scope: IWhatIsPersonalDlgScope, $mdDialog: ng.material.IDialogService) {
            $scope.close = () => {
                $mdDialog.hide();
            };
        }
    }
}
