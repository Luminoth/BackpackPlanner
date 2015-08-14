///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IGearCollectionsScope extends IAppScope {
        orderBy: string;

        showWhatIsGearCollection: (event: MouseEvent) => void;
        showDeleteAllConfirm: (event: MouseEvent) => void;
    }

    export class GearCollectionsCtrl {
        constructor($scope: IGearCollectionsScope, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderBy = "Name";

            $scope.showWhatIsGearCollection = (event) => {
                $mdDialog.show({
                    controller: WhatIsGearCollectionDlgCtrl,
                    templateUrl: "content/partials/gear/collections/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }

            $scope.showDeleteAllConfirm = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Gear Collections")
                    .content("Are you sure you wish to delete all gear collections?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All gear collections deleted!")
                    .content("All gear collections have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all gear collections")
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content("Restored all gear collections")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getGearState().deleteAllGearCollections();

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
    
    GearCollectionsCtrl.$inject = ["$scope", "$mdDialog", "$mdToast"];
}
