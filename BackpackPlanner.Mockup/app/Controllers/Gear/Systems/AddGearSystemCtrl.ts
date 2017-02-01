///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />

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
                    .textContent(`Added gear system: ${$scope.gearSystem.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/systems");
                $mdToast.show(addToast);
            }

            $scope.resetGearSystem = () => {
                $scope.gearSystem = new Models.Gear.GearSystem();
            }
        }
    }

    AddGearSystemCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
