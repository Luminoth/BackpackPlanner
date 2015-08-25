///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Itineraries {
    "use strict";

    export interface ITripItinerariesScope extends IAppScope {
        filterName: string;
        orderBy: string;

        filterTripItinerary: (tripItinerary: Models.Trips.TripItinerary) => boolean;
        showWhatIsTripItineraryDlg: (event: MouseEvent) => void;
    }

    export class TripItinerariesCtrl {
        constructor($scope: ITripItinerariesScope, $mdDialog: ng.material.IDialogService) {
            $scope.filterName = "";
            $scope.orderBy = "name()";

            $scope.filterTripItinerary = (tripItinerary) => {
                if($scope.filterName) {
                    return tripItinerary.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                }
                return true;
            }

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
