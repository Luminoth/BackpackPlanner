///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface ITripPlanScope extends IAppScope {
        tripPlan: Models.Trips.TripPlan;

        showDeleteConfirm: (event: MouseEvent) => void;
    }

    export interface ITripPlanParams extends ng.route.IRouteParamsService {
        tripPlanId: number;
    }

    export class TripPlanCtrl {
        constructor($scope: ITripPlanScope, $routeParams: ITripPlanParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
        
            $scope.tripPlan = AppState.getInstance().getTripState().getTripPlanById($routeParams.tripPlanId);
            if(null == $scope.tripPlan) {
                alert("The trip plan does not exist!");
                $location.path("/trips/plans");
                return;
            }

            $scope.showDeleteConfirm = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Trip Plan")
                    .content("Are you sure you wish to delete this trip plan?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Trip plan deleted!")
                    .content("The trip plan has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content(`Deleted trip plan: ${$scope.tripPlan.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content(`Restored trip plan: ${$scope.tripPlan.Name}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        if(!AppState.getInstance().getTripState().deleteTripPlan($scope.tripPlan)) {
                            alert("Couldn't find the trip plan to delete!");
                            return;
                        }

                        $location.path("/trips/plans");
                        $mdToast.show(deleteToast).then(() => {
                            AppState.getInstance().getTripState().addTripPlan($scope.tripPlan);
                            $mdToast.show(undoDeleteToast);
                            $location.path(`/trips/plans/${$scope.tripPlan.Id}`);
                        });
                    });
                });
            }
        }
    }

    TripPlanCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}
