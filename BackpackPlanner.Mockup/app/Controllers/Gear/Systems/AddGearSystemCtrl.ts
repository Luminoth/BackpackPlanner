﻿///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Systems {
    "use strict";

    export interface IAddGearSystemScope extends IAppScope {
        gearSystem: Models.Gear.GearSystem;

        orderGearItemsBy: string;

        showAddGearItemDlg: (event: MouseEvent) => void;

        addGearSystem: () => void;
        resetGearSystem: () => void;
    }

    export class AddGearSystemCtrl {
        constructor($scope: IAddGearSystemScope, $location: ng.ILocationService, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderGearItemsBy = "getName()";

            $scope.gearSystem = new Models.Gear.GearSystem();

            $scope.showAddGearItemDlg = (event) => {
                $mdDialog.show({
                    controller: AddGearItemDlgCtrl,
                    templateUrl: "content/partials/gear/systems/add-item.html",
                    parent: angular.element(document.body),
                    targetEvent: event,
                    locals: {
                        gearSystem: $scope.gearSystem
                    }
                });
            }

            $scope.addGearSystem = () => {
                AppState.getInstance().getGearState().addGearSystem($scope.gearSystem);

                var addToast = $mdToast.simple()
                    .content(`Added gear system: ${$scope.gearSystem.name()}`)
                    .action("Undo")
                    .position("bottom left");

                var undoAddToast = $mdToast.simple()
                    .content(`Removed gear system: ${$scope.gearSystem.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/systems");
                $mdToast.show(addToast).then((response: string) => {
                    if("ok" == response) {
                        AppState.getInstance().getGearState().deleteGearSystem($scope.gearSystem);
                        $mdToast.show(undoAddToast);
                    }
                });
            }

            $scope.resetGearSystem = () => {
                $scope.gearSystem = new Models.Gear.GearSystem();
            }
        }
    }

    AddGearSystemCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
