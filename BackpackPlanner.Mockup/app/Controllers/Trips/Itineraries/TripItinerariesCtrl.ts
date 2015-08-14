///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Itineraries {
    "use strict";

    export interface ITripItinerariesScope extends IAppScope {
        orderBy: string;

        showWhatIsTripItinerary: (event: MouseEvent) => void;
        showDeleteAllConfirm: (event: MouseEvent) => void;
    }

    export class TripItinerariesCtrl {
        constructor($scope: ITripItinerariesScope, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderBy = "Name";

            $scope.showWhatIsTripItinerary = (event) => {
                $mdDialog.show({
                    controller: WhatIsTripItineraryDlgCtrl,
                    templateUrl: "content/partials/trips/itineraries/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }

            $scope.showDeleteAllConfirm = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Trip Itineraries")
                    .content("Are you sure you wish to delete all trip itineraries?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All trip itineraries deleted!")
                    .content("All trip itineraries have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all trip itineraries")
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content("Restored all trip itineraries")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getTripState().deleteAllTripItineraries();

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
    
    TripItinerariesCtrl.$inject = ["$scope", "$mdDialog", "$mdToast"];
}
