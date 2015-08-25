///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Itineraries {
    "use strict";

    export interface ITripItineraryScope extends IAppScope {
        tripItinerary: Models.Trips.TripItinerary;

        saveTripItinerary: () => void;
        resetTripItinerary: () => void;
        deleteTripItinerary: (event: MouseEvent) => void;
    }

    export interface ITripItineraryParams extends ng.route.IRouteParamsService {
        tripItineraryId: number;
    }

    export class TripItineraryCtrl {
        constructor($scope: ITripItineraryScope, $routeParams: ITripItineraryParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
        
            const tripItinerary = AppState.getInstance().getTripState().getTripItineraryById($routeParams.tripItineraryId);
            if(null == tripItinerary) {
                alert("The trip itinerary does not exist!");
                $location.path("/trips/itineraries");
                return;
            }
            $scope.tripItinerary = angular.copy(tripItinerary);

            $scope.saveTripItinerary = () => {
                var tripItinerary = AppState.getInstance().getTripState().getTripItineraryById($scope.tripItinerary.Id);
                if(null == tripItinerary) {
                    alert("The trip itinerary no longer exists!");
                    $location.path("/trips/itineraries");
                    return;
                }
                tripItinerary.update($scope.tripItinerary);

                $location.path("/trips/itineraries");
                // TODO: toast!
            }

            $scope.resetTripItinerary = () => {
                var tripItinerary = AppState.getInstance().getTripState().getTripItineraryById($scope.tripItinerary.Id);
                if(null == tripItinerary) {
                    alert("The trip itinerary no longer exists!");
                    $location.path("/trips/itineraries");
                    return;
                }
                $scope.tripItinerary = angular.copy(tripItinerary);
            }

            $scope.deleteTripItinerary = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Trip Itinerary")
                    .content("Are you sure you wish to delete this trip itinerary?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Trip itinerary deleted!")
                    .content("The trip itinerary has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content(`Deleted trip itinerary: ${$scope.tripItinerary.name()}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content(`Restored trip itinerary: ${$scope.tripItinerary.name()}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        if(!AppState.getInstance().getTripState().deleteTripItinerary($scope.tripItinerary)) {
                            alert("Couldn't find the trip itinerary to delete!");
                            return;
                        }

                        $location.path("/trips/itineraries");
                        $mdToast.show(deleteToast).then((response: string) => {
                            if("ok" == response) {
                                // TODO: this does *not* restore the itinerary to its containers
                                // and it should probably do so... but how?
                                AppState.getInstance().getTripState().addTripItinerary($scope.tripItinerary);
                                $mdToast.show(undoDeleteToast);
                                $location.path(`/trips/itineraries/${$scope.tripItinerary.Id}`);
                            }
                        });
                    });
                });
            }
        }
    }

    TripItineraryCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}
