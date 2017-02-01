///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IWhatIsTripPlanDlgScope extends ng.IScope {
        close: () => void;
    }

    export class WhatIsTripPlanDlgCtrl {
        constructor($scope: IWhatIsTripPlanDlgScope, $mdDialog: ng.material.IDialogService) {
            $scope.close = () => {
                $mdDialog.hide();
            };
        }
    }
}
