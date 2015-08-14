///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Itineraries {
    "use strict";

    export interface ITripItinerariesScope extends IAppScope {
        orderBy: string;

        showWhatIsTripItineraryDlg: (event: MouseEvent) => void;
    }

    export class TripItinerariesCtrl {
        constructor($scope: ITripItinerariesScope, $mdDialog: ng.material.IDialogService) {
            $scope.orderBy = "Name";

            $scope.showWhatIsTripItineraryDlg = (event) => {
                $mdDialog.show({
                    controller: WhatIsTripItineraryDlgCtrl,
                    templateUrl: "content/partials/trips/itineraries/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }
        }
    }
    
    TripItinerariesCtrl.$inject = ["$scope", "$mdDialog"];
}
