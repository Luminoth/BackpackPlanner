///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Itineraries {
    "use strict";

    export interface IAddTripItineraryScope extends IAppScope {
        tripItinerary: Models.Trips.TripItinerary;

        addTripItinerary: () => void;
        resetTripItinerary: () => void;
    }

    export class AddTripItineraryCtrl {
        constructor($scope: IAddTripItineraryScope, $location: ng.ILocationService, $mdToast: ng.material.IToastService) {
            $scope.tripItinerary = new Models.Trips.TripItinerary();

            $scope.addTripItinerary = () => {
                AppState.getInstance().getTripState().addTripItinerary($scope.tripItinerary);

                var addToast = $mdToast.simple()
                    .content(`Added trip itinerary: ${$scope.tripItinerary.name()}`)
                    .action("Undo")
                    .position("bottom left");

                var undoAddToast = $mdToast.simple()
                    .content(`Removed trip itinerary: ${$scope.tripItinerary.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/trips/itineraries");
                $mdToast.show(addToast).then((response: string) => {
                    if("ok" == response) {
                        AppState.getInstance().getTripState().deleteTripItinerary($scope.tripItinerary);
                        $mdToast.show(undoAddToast);
                    }
                });
            }

            $scope.resetTripItinerary = () => {
                $scope.tripItinerary = new Models.Trips.TripItinerary();
            }
        }
    }

    AddTripItineraryCtrl.$inject = ["$scope", "$location", "$mdToast"];
}
