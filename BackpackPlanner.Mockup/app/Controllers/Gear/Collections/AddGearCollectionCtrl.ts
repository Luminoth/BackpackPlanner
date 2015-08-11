///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IAddGearCollectionScope extends IAppScope {
        gearCollection: Models.Gear.GearCollection;

        showAddGearItem: (event: MouseEvent) => void;
        showAddGearSystem: (event: MouseEvent) => void;
        addCollection: (gearCollection: Models.Gear.GearCollection) => void;
    }

    export class AddGearCollectionCtrl {
        constructor($scope: IAddGearCollectionScope, $location: ng.ILocationService, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.gearCollection = new Models.Gear.GearCollection();

            $scope.showAddGearItem = (event) => {
                $mdDialog.show({
                    controller: AddGearItemDlgCtrl,
                    templateUrl: "content/partials/gear/collections/add-item.html",
                    parent: angular.element(document.body),
                    targetEvent: event,
                    locals: {
                        gearCollection: $scope.gearCollection
                    }
                });
            }

            $scope.showAddGearSystem = (event) => {
                $mdDialog.show({
                    controller: AddGearSystemDlgCtrl,
                    templateUrl: "content/partials/gear/collections/add-system.html",
                    parent: angular.element(document.body),
                    targetEvent: event,
                    locals: {
                        gearCollection: $scope.gearCollection
                    }
                });
            }

            $scope.addCollection = (gearCollection) => {
                $scope.gearCollection = angular.copy(gearCollection);
                $scope.gearCollection.Id = AppState.getInstance().getGearState().addGearCollection($scope.gearCollection);

                var addToast = $mdToast.simple()
                    .content(`Added gear collection: ${$scope.gearCollection.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoAddToast = $mdToast.simple()
                    .content(`Removed gear collection: ${$scope.gearCollection.Name}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/collections");
                $mdToast.show(addToast).then(() => {
                    AppState.getInstance().getGearState().deleteGearCollection($scope.gearCollection);
                    $mdToast.show(undoAddToast);
                });
            }
        }
    }

    AddGearCollectionCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
