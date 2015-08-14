///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Meals {
    "use strict";

    export interface IMealsScope extends IAppScope {
        orderBy: string;

        showWhatIsMeal: (event: MouseEvent) => void;
        showDeleteAllConfirm: (event: MouseEvent) => void;
    }

    export class MealsCtrl {
        constructor($scope: IMealsScope, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderBy = "Name";

            $scope.showWhatIsMeal = (event) => {
                $mdDialog.show({
                    controller: WhatIsMealDlgCtrl,
                    templateUrl: "content/partials/meals/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }

            $scope.showDeleteAllConfirm = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Meals")
                    .content("Are you sure you wish to delete all meals?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All meals deleted!")
                    .content("All meals have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all meals")
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content("Restored all meals")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getMealState().deleteAllMeals();

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
    
    MealsCtrl.$inject = ["$scope", "$mdDialog", "$mdToast"];
}
