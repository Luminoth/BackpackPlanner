///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface ITripPlansScope extends IAppScope {
        orderBy: string;

        showWhatIsTripPlan: (event: MouseEvent) => void;
        showDeleteAllConfirm: (event: MouseEvent) => void;
    }

    export class TripPlansCtrl {
        constructor($scope: ITripPlansScope, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderBy = "Name";

            $scope.showWhatIsTripPlan = (event) => {
                $mdDialog.show({
                    controller: WhatIsTripPlanDlgCtrl,
                    templateUrl: "content/partials/trips/plans/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }

            $scope.showDeleteAllConfirm = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Trip Plans")
                    .content("Are you sure you wish to delete all trip plans?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All trip plans deleted!")
                    .content("All trip plans have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all trip plans")
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content("Restored all trip plans")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getTripState().deleteAllTripPlans();

                        $mdToast.show(deleteToast).then((response: string) => {
                            if("ok" == response) {
                                // TODO: this does *not* restore anything
                                // and it should probably do so... but how?
                                $mdToast.show(undoDeleteToast);
                            }
                        });
                    });
                });
            }
        }
    }
    
    TripPlansCtrl.$inject = ["$scope", "$mdDialog", "$mdToast"];
}
