///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Itineraries {
    "use strict";

    export interface IWhatIsTripItineraryDlgScope extends ng.IScope {
        close: () => void;
    }

    export class WhatIsTripItineraryDlgCtrl {
        constructor($scope: IWhatIsTripItineraryDlgScope, $mdDialog: ng.material.IDialogService) {
            $scope.close = () => {
                $mdDialog.hide();
            };
        }
    }
}
