///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IAddGearSystemScope extends IAppScope {
        gearSystem: Models.Gear.IGearSystem;

        getGearItem: (gearItemId: number) => Models.Gear.IGearItem;
        showAddGearItem: (event: MouseEvent) => void;
        addSystem: (gearSystem: Models.Gear.IGearSystem) => void;
    }

    export class AddGearSystemCtrl {
        constructor($scope: IAddGearSystemScope, $location: ng.ILocationService, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.gearSystem = Models.Gear.newGearSystem();

            $scope.getGearItem = (gearItemId) => {
                return Models.Gear.getGearItemById($scope.gearItems, gearItemId);
            };

            $scope.showAddGearItem = (event) => {
                $mdDialog.show({
                    controller: AddGearItemDlgCtrl,
                    templateUrl: "content/partials/gear/systems/add-item.html",
                    parent: angular.element(document.body),
                    targetEvent: event,
                    locals: {
                        gearItems: $scope.gearItems,
                        gearSystem: $scope.gearSystem
                    }
                });
            }

            $scope.addSystem = (gearSystem) => {
                $scope.gearSystem = angular.copy(gearSystem);
                $scope.gearSystem.Id = Models.Gear.getNextGearSystemId();
                $scope.gearSystems.push($scope.gearSystem);

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
                    Models.Gear.deleteGearSystem($scope.gearSystems, $scope.gearCollections, $scope.gearSystem);
                    $mdToast.show(undoAddToast);
                });
            }
        }
    }

    AddGearSystemCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
