/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IWhatIsGearCollectionDlgScope extends ng.IScope {
        close: () => void;
    }

    export class WhatIsGearCollectionDlgCtrl {
        constructor($scope: IWhatIsGearCollectionDlgScope, $mdDialog: ng.material.IDialogService) {
            $scope.close = () => {
                $mdDialog.hide();
            };
        }
    }
}
