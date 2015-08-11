///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface ITripPlansScope extends IAppScope {
        orderBy: string;

        showWhatIsTripPlan: (event: MouseEvent) => void;
    }

    export class TripPlansCtrl {
        constructor($scope: ITripPlansScope, $mdDialog: ng.material.IDialogService) {
            $scope.orderBy = "Name";

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
