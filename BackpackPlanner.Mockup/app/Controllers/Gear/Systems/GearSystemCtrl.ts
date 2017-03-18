/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />

/// <reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Systems {
    "use strict";

    export interface IGearSystemScope extends IAppScope {
        gearSystem: Models.Gear.GearSystem;

        orderGearItemsBy: string;

        showAddGearItemDlg: (event: MouseEvent) => void;

        saveGearSystem: () => void;
        resetGearSystem: () => void;
        deleteGearSystem: (event: MouseEvent) => void;
    }

    export interface IGearSystemRouteParams extends ng.route.IRouteParamsService {
        gearSystemId: number;
    }

    export class GearSystemCtrl {
        constructor($scope: IGearSystemScope, $routeParams: IGearSystemRouteParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderGearItemsBy = "getName()";
        
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById($routeParams.gearSystemId);
            if(null == gearSystem) {
                alert("The gear system does not exist!");
                $location.path("/gear/systems");
                return;
            }
            $scope.gearSystem = angular.copy(gearSystem);

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

            $scope.saveGearSystem = () => {
                var gearSystem = AppState.getInstance().getGearState().getGearSystemById($scope.gearSystem.Id);
                if(null == gearSystem) {
                    alert("The gear system no longer exists!");
                    $location.path("/gear/systems");
                    return;
                }
                gearSystem.update($scope.gearSystem);

                var updateToast = $mdToast.simple()
                    .textContent(`Updated gear system: ${$scope.gearSystem.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/systems");
                $mdToast.show(updateToast);
            }

            $scope.resetGearSystem = () => {
                var gearSystem = AppState.getInstance().getGearState().getGearSystemById($scope.gearSystem.Id);
                if(null == gearSystem) {
                    alert("The gear system no longer exists!");
                    $location.path("/gear/systems");
                    return;
                }
                $scope.gearSystem = angular.copy(gearSystem);
            }

            $scope.deleteGearSystem = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Gear System")
                    .textContent("Are you sure you wish to delete this gear system?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Gear system deleted!")
                    .textContent("The gear system has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .textContent(`Deleted gear system: ${$scope.gearSystem.name()}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .textContent(`Restored gear system: ${$scope.gearSystem.name()}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        const action = new Actions.Gear.Systems.DeleteGearSystemAction();
                        action.GearSystem = $scope.gearSystem;
                        AppState.getInstance().executeAction(action);

                        $location.path("/gear/systems");
                        $mdToast.show(deleteToast).then((response: string) => {
                            if("ok" == response) {
                                AppState.getInstance().undoAction();
                                $mdToast.show(undoDeleteToast);
                                $location.path(`/gear/systems/${$scope.gearSystem.Id}`);
                            }
                        });
                    });
                });
            }
        }
    }

    GearSystemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}
