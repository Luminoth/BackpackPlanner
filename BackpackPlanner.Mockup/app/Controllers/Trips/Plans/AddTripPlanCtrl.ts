///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddTripPlanScope extends IAppScope {
        tripPlan: Models.Trips.TripPlan;

        orderGearCollectionsBy: string;
        orderGearSystemsBy: string;
        orderGearItemsBy: string;
        orderMealsBy: string;

        showAddGearCollectionDlg: (event: MouseEvent) => void;
        showAddGearSystemDlg: (event: MouseEvent) => void;
        showAddGearItemDlg: (event: MouseEvent) => void;
        showAddMealDlg: (event: MouseEvent) => void;

        addTripPlan: () => void;
        resetTripPlan: () => void;
    }

    export class AddTripPlanCtrl {
        constructor($scope: IAddTripPlanScope, $location: ng.ILocationService, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderGearCollectionsBy = "getName()";
            $scope.orderGearSystemsBy = "getName()";
            $scope.orderGearItemsBy = "getName()";
            $scope.orderMealsBy = "getName()";

            $scope.tripPlan = new Models.Trips.TripPlan();

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

            $scope.addTripPlan = () => {
                AppState.getInstance().getTripState().addTripPlan($scope.tripPlan);

                var addToast = $mdToast.simple()
                    .textContent(`Added trip plan: ${$scope.tripPlan.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/trips/plans");
                $mdToast.show(addToast);
            }

            $scope.resetTripPlan = () => {
                $scope.tripPlan = new Models.Trips.TripPlan();
            }
        }
    }

    AddTripPlanCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
