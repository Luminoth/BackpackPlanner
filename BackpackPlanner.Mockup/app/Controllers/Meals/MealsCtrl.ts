///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Meals {
    "use strict";

    export interface IMealsScope extends IAppScope {
        orderBy: string;

        showWhatIsMealDlg: (event: MouseEvent) => void;
    }

    export class MealsCtrl {
        constructor($scope: IMealsScope, $mdDialog: ng.material.IDialogService) {
            $scope.orderBy = "Name";

            $scope.showWhatIsMealDlg = (event) => {
                $mdDialog.show({
                    controller: WhatIsMealDlgCtrl,
                    templateUrl: "content/partials/meals/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }
        }
    }
    
    MealsCtrl.$inject = ["$scope", "$mdDialog"];
}
