/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />

/// <reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface ITripPlansScope extends IAppScope {
        filterName: string;
        orderBy: string;

        filterTripPlan: (tripPlan: Models.Trips.TripPlan) => boolean;
        showWhatIsTripPlan: (event: MouseEvent) => void;
    }

    export class TripPlansCtrl {
        constructor($scope: ITripPlansScope, $mdDialog: ng.material.IDialogService) {
            $scope.filterName = "";
            $scope.orderBy = "name()";

            $scope.filterTripPlan = (tripPlan) => {
                if($scope.filterName) {
                    return tripPlan.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                }
                return true;
            }

            $scope.showWhatIsTripPlan = (event) => {
                $mdDialog.show({
                    controller: WhatIsTripPlanDlgCtrl,
                    templateUrl: "content/partials/trips/plans/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }
        }
    }
    
    TripPlansCtrl.$inject = ["$scope", "$mdDialog"];
}
