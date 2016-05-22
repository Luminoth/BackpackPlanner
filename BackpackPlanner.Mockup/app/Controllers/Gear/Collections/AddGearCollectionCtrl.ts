///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IAddGearCollectionScope extends IAppScope {
        gearCollection: Models.Gear.GearCollection;

        orderGearItemsBy: string;
        orderGearSystemsBy: string;

        showAddGearItemDlg: (event: MouseEvent) => void;
        showAddGearSystemDlg: (event: MouseEvent) => void;

        addGearCollection: () => void;
        resetGearCollection: () => void;
    }

    export class AddGearCollectionCtrl {
        constructor($scope: IAddGearCollectionScope, $location: ng.ILocationService, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderGearItemsBy = "getName()";
            $scope.orderGearSystemsBy = "getName()";

            $scope.gearCollection = new Models.Gear.GearCollection();

            $scope.showAddGearItemDlg = (event) => {
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

            $scope.showAddGearSystemDlg = (event) => {
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

            $scope.addGearCollection = () => {
                AppState.getInstance().getGearState().addGearCollection($scope.gearCollection);

                var addToast = $mdToast.simple()
                    .textContent(`Added gear collection: ${$scope.gearCollection.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/collections");
                $mdToast.show(addToast);
            }

            $scope.resetGearCollection = () => {
                $scope.gearCollection = new Models.Gear.GearCollection();
            }
        }
    }

    AddGearCollectionCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
