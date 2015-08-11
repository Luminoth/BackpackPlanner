///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../Models/AppSettings.ts" />

module BackpackPlanner.Mockup.Controllers.Personal {
    "use strict";

    export interface IUserInformationScope extends ng.IScope {
        showWhatIsPersonal: (event: MouseEvent) => void;
    }

    export class UserInformationCtrl {
        constructor($scope: IUserInformationScope, $mdDialog: ng.material.IDialogService) {
            $scope.showWhatIsPersonal = (event) => {
                $mdDialog.show({
                    controller: WhatIsPersonalDlgCtrl,
                    templateUrl: "content/partials/personal/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }
        }
    }

    UserInformationCtrl.$inject = ["$scope", "$mdDialog"];
}
