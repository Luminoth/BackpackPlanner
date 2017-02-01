///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface ITripPlanScope extends IAppScope {
        tripPlan: Models.Trips.TripPlan;

        orderGearCollectionsBy: string;
        orderGearSystemsBy: string;
        orderGearItemsBy: string;
        orderMealsBy: string;

        showAddGearCollectionDlg: (event: MouseEvent) => void;
        showAddGearSystemDlg: (event: MouseEvent) => void;
        showAddGearItemDlg: (event: MouseEvent) => void;
        showAddMealDlg: (event: MouseEvent) => void;

        saveTripPlan: () => void;
        resetTripPlan: () => void;
        deleteTripPlan: (event: MouseEvent) => void;
    }

    export interface ITripPlanParams extends ng.route.IRouteParamsService {
        tripPlanId: number;
    }

    export class TripPlanCtrl {
        constructor($scope: ITripPlanScope, $routeParams: ITripPlanParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderGearCollectionsBy = "getName()";
            $scope.orderGearSystemsBy = "getName()";
            $scope.orderGearItemsBy = "getName()";
            $scope.orderMealsBy = "getName()";
        
            const tripPlan = AppState.getInstance().getTripState().getTripPlanById($routeParams.tripPlanId);
            if(null == tripPlan) {
                alert("The trip plan does not exist!");
                $location.path("/trips/plans");
                return;
            }
            $scope.tripPlan = angular.copy(tripPlan);

            $scope.showAddGearCollectionDlg = (event) => {
                $mdDialog.show({
                    controller: AddGearCollectionDlgCtrl,
                    templateUrl: "content/partials/trips/plans/add-collection.html",
                    parent: angular.element(document.body),
                    targetEvent: event,
                    locals: {
                        tripPlan: $scope.tripPlan
                    }
                });
            }

            $scope.showAddGearSystemDlg = (event) => {
                $mdDialog.show({
                    controller: AddGearSystemDlgCtrl,
                    templateUrl: "content/partials/trips/plans/add-system.html",
                    parent: angular.element(document.body),
                    targetEvent: event,
                    locals: {
                        tripPlan: $scope.tripPlan
                    }
                });
            }

            $scope.showAddGearItemDlg = (event) => {
                $mdDialog.show({
                    controller: AddGearItemDlgCtrl,
                    templateUrl: "content/partials/trips/plans/add-item.html",
                    parent: angular.element(document.body),
                    targetEvent: event,
                    locals: {
                        tripPlan: $scope.tripPlan
                    }
                });
            }

            $scope.showAddMealDlg = (event) => {
                $mdDialog.show({
                    controller: AddMealDlgCtrl,
                    templateUrl: "content/partials/trips/plans/add-meal.html",
                    parent: angular.element(document.body),
                    targetEvent: event,
                    locals: {
                        tripPlan: $scope.tripPlan
                    }
                });
            }

            $scope.saveTripPlan = () => {
                var tripPlan = AppState.getInstance().getTripState().getTripPlanById($scope.tripPlan.Id);
                if(null == tripPlan) {
                    alert("The trip plan no longer exists!");
                    $location.path("/trips/plans");
                    return;
                }
                tripPlan.update($scope.tripPlan);

                var updateToast = $mdToast.simple()
                    .textContent(`Updated trip plan: ${$scope.tripPlan.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/trips/plans");
                $mdToast.show(updateToast);
            }

            $scope.resetTripPlan = () => {
                var tripPlan = AppState.getInstance().getTripState().getTripPlanById($scope.tripPlan.Id);
                if(null == tripPlan) {
                    alert("The trip plan no longer exists!");
                    $location.path("/trips/plans");
                    return;
                }
                $scope.tripPlan = angular.copy(tripPlan);
            }

            $scope.deleteTripPlan = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Trip Plan")
                    .textContent("Are you sure you wish to delete this trip plan?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Trip plan deleted!")
                    .textContent("The trip plan has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .textContent(`Deleted trip plan: ${$scope.tripPlan.name()}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .textContent(`Restored trip plan: ${$scope.tripPlan.name()}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        const action = new Actions.Trips.Plans.DeleteTripPlanAction();
                        action.TripPlan = $scope.tripPlan;
                        AppState.getInstance().executeAction(action);

                        $location.path("/trips/plans");
                        $mdToast.show(deleteToast).then((response: string) => {
                            if("ok" == response) {
                                AppState.getInstance().undoAction();
                                $mdToast.show(undoDeleteToast);
                                $location.path(`/trips/plans/${$scope.tripPlan.Id}`);
                            }
                        });
                    });
                });
            }
        }
    }

    TripPlanCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}
