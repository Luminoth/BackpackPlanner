///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddTripPlanScope extends IAppScope {
        tripPlan: Models.Trips.TripPlan;

        addTripPlan: (tripPlan: Models.Trips.TripPlan) => void;
    }

    export class AddTripPlanCtrl {
        constructor($scope: IAddTripPlanScope, $location: ng.ILocationService, $mdToast: ng.material.IToastService) {
            $scope.tripPlan = new Models.Trips.TripPlan();

            $scope.addTripPlan = (tripPlan) => {
                $scope.tripPlan = angular.copy(tripPlan);
                $scope.tripPlan.Id = AppState.getInstance().getTripState().addTripPlan($scope.tripPlan);

                var addToast = $mdToast.simple()
                    .content(`Added trip plan: ${$scope.tripPlan.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoAddToast = $mdToast.simple()
                    .content(`Removed trip plan: ${$scope.tripPlan.Name}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/trips/plans");
                $mdToast.show(addToast).then(() => {
                    AppState.getInstance().getTripState().deleteTripPlan($scope.tripPlan);
                    $mdToast.show(undoAddToast);
                });
            }
        }
    }

    AddTripPlanCtrl.$inject = ["$scope", "$location", "$mdToast"];
}
