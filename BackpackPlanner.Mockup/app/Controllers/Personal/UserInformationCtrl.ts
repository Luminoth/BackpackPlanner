/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />

/// <reference path="../../Models/AppSettings.ts" />

module BackpackPlanner.Mockup.Controllers.Personal {
    "use strict";

    export interface IUserInformationScope extends ng.IScope {
        userInfo: Models.Personal.UserInformation;

        showWhatIsPersonalDlg: (event: MouseEvent) => void;

        saveUserInformation: () => void;
        resetUserInformation: () => void;
    }

    export class UserInformationCtrl {
        constructor($scope: IUserInformationScope, $location: ng.ILocationService, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.userInfo = angular.copy(AppState.getInstance().getUserInformation());

            $scope.showWhatIsPersonalDlg = (event) => {
                $mdDialog.show({
                    controller: WhatIsPersonalDlgCtrl,
                    templateUrl: "content/partials/personal/what.html",
                    parent: angular.element(document.body),
                    targetEvent: event
                });
            }

            $scope.saveUserInformation = () => {
                AppState.getInstance().getUserInformation().update($scope.userInfo);

                var updateToast = $mdToast.simple()
                    .textContent("Updated personal information!")
                    .action("OK")
                    .position("bottom left");

                $location.path("/personal");
                $mdToast.show(updateToast);
            }

            $scope.resetUserInformation = () => {
                $scope.userInfo = angular.copy(AppState.getInstance().getUserInformation());
            }
        }
    }

    UserInformationCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
