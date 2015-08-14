///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Systems {
    "use strict";

    export interface IGearSystemsScope extends IAppScope {
        orderBy: string;

        showWhatIsGearSystem: (event: MouseEvent) => void;
        showDeleteAllConfirm: (event: MouseEvent) => void;
    }

    export class GearSystemsCtrl {
        constructor($scope: IGearSystemsScope, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderBy = "Name";

            $scope.showWhatIsGearSystem = (event) => {
                $mdDialog.show({
                    controller: WhatIsGearSystemDlgCtrl,
                    templateUrl: "content/partials/gear/systems/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }

            $scope.showDeleteAllConfirm = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Gear Systems")
                    .content("Are you sure you wish to delete all gear systems?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All gear systems deleted!")
                    .content("All gear systems have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all gear systems")
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content("Restored all gear systems")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getGearState().deleteAllGearSystems();

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
    
    GearSystemsCtrl.$inject = ["$scope", "$mdDialog", "$mdToast"];
}
