///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Items {
    "use strict";

    export interface IGearItemScope extends IAppScope {
        gearItem: Models.Gear.GearItem;

        saveGearItem: () => void;
        resetGearItem: () => void;
        deleteGearItem: (event: MouseEvent) => void;
    }

    export interface IGearItemRouteParams extends ng.route.IRouteParamsService {
        gearItemId: number;
    }

    export class GearItemCtrl {
        constructor($scope: IGearItemScope, $routeParams: IGearItemRouteParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
        
            const gearItem = AppState.getInstance().getGearState().getGearItemById($routeParams.gearItemId);
            if(null == gearItem) {
                alert("The gear item does not exist!");
                $location.path("/gear/items");
                return;
            }
            $scope.gearItem = angular.copy(gearItem);

            $scope.saveGearItem = () => {
                var gearItem = AppState.getInstance().getGearState().getGearItemById($scope.gearItem.Id);
                if(null == gearItem) {
                    alert("The gear item no longer exists!");
                    $location.path("/gear/items");
                    return;
                }
                gearItem.update($scope.gearItem);

                var updateToast = $mdToast.simple()
                    .textContent(`Updated gear item: ${$scope.gearItem.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/gear/items");
                $mdToast.show(updateToast);
            }

            $scope.resetGearItem = () => {
                var gearItem = AppState.getInstance().getGearState().getGearItemById($scope.gearItem.Id);
                if(null == gearItem) {
                    alert("The gear item no longer exists!");
                    $location.path("/gear/items");
                    return;
                }
                $scope.gearItem = angular.copy(gearItem);
            }

            $scope.deleteGearItem = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Gear Item")
                    .textContent("Are you sure you wish to delete this gear item?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Gear item deleted!")
                    .textContent("The gear item has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .textContent(`Deleted gear item: ${$scope.gearItem.name()}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .textContent(`Restored gear item: ${$scope.gearItem.name()}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        const action = new Actions.Gear.Items.DeleteGearItemAction();
                        action.GearItem = $scope.gearItem;
                        AppState.getInstance().executeAction(action);

                        $location.path("/gear/items");
                        $mdToast.show(deleteToast).then((response: string) => {
                            if("ok" == response) {
                                AppState.getInstance().undoAction();
                                $mdToast.show(undoDeleteToast);
                                $location.path(`/gear/items/${$scope.gearItem.Id}`);
                            }
                        });
                    });
                });
            }
        }
    }

    GearItemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}
