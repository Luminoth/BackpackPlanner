///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IAddGearSystemScope extends IAppScope {
        gearSystem: Models.Gear.GearSystem;

        showAddGearItem: (event: MouseEvent) => void;
        addSystem: (gearSystem: Models.Gear.GearSystem) => void;
    }

    export class AddGearSystemCtrl {
        constructor($scope: IAddGearSystemScope, $location: ng.ILocationService, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.gearSystem = new Models.Gear.GearSystem();

            $scope.showAddGearItem = (event) => {
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

            $scope.addSystem = (gearSystem) => {
                $scope.gearSystem = angular.copy(gearSystem);
                $scope.gearSystem.Id = AppManager.getInstance().getNextGearSystemId();
                AppManager.getInstance().getGearSystems().push($scope.gearSystem);

                var addToast = $mdToast.simple()
                    .content(`Added gear system: ${$scope.gearSystem.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoAddToast = $mdToast.simple()
                    .content(`Removed gear system: ${$scope.gearSystem.Name}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/systems");
                $mdToast.show(addToast).then(() => {
                    AppManager.getInstance().deleteGearSystem($scope.gearSystem);
                    $mdToast.show(undoAddToast);
                });
            }
        }
    }

    AddGearSystemCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
