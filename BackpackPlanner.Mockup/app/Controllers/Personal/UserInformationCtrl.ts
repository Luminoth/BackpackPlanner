///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../Models/AppSettings.ts" />

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

                $location.path("/");
                // TODO: toast!
            }

            $scope.resetUserInformation = () => {
                $scope.userInfo = angular.copy(AppState.getInstance().getUserInformation());
            }
        }
    }

    UserInformationCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
