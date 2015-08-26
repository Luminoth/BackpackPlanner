///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />

///<reference path="../../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear.Collections {
    "use strict";

    export interface IGearCollectionScope extends IAppScope {
        gearCollection: Models.Gear.GearCollection;

        orderGearSystemsBy: string;
        orderGearItemsBy: string;

        showAddGearSystemDlg: (event: MouseEvent) => void;
        showAddGearItemDlg: (event: MouseEvent) => void;

        saveGearCollection: () => void;
        resetGearCollection: () => void;
        deleteGearCollection: (event: MouseEvent) => void;
    }

    export interface IGearCollectionRouteParams extends ng.route.IRouteParamsService {
        gearCollectionId: number;
    }

    export class GearCollectionCtrl {
        constructor($scope: IGearCollectionScope, $routeParams: IGearCollectionRouteParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderGearSystemsBy = "getName()";
            $scope.orderGearItemsBy = "getName()";
        
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById($routeParams.gearCollectionId);
            if(null == gearCollection) {
                alert("The gear collection does not exist!");
                $location.path("/gear/collections");
                return;
            }
            $scope.gearCollection = angular.copy(gearCollection);

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

            $scope.saveGearCollection = () => {
                var gearCollection = AppState.getInstance().getGearState().getGearCollectionById($scope.gearCollection.Id);
                if(null == gearCollection) {
                    alert("The gear collection no longer exists!");
                    $location.path("/gear/collections");
                    return;
                }
                gearCollection.update($scope.gearCollection);

                $location.path("/gear/collections");
                // TODO: toast!
            }

            $scope.resetGearCollection = () => {
                var gearCollection = AppState.getInstance().getGearState().getGearCollectionById($scope.gearCollection.Id);
                if(null == gearCollection) {
                    alert("The gear collection no longer exists!");
                    $location.path("/gear/collections");
                    return;
                }
                $scope.gearCollection = angular.copy(gearCollection);
            }

            $scope.deleteGearCollection = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Gear Collection")
                    .content("Are you sure you wish to delete this gear collection?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Gear collection deleted!")
                    .content("The gear collection has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content(`Deleted gear collection: ${$scope.gearCollection.name()}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content(`Restored gear collection: ${$scope.gearCollection.name()}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        const action = new Actions.Gear.Collections.DeleteGearCollectionAction();
                        action.GearCollection = $scope.gearCollection;
                        AppState.getInstance().executeAction(action);

                        $location.path("/gear/collections");
                        $mdToast.show(deleteToast).then((response: string) => {
                            if("ok" == response) {
                                AppState.getInstance().undoAction();
                                $mdToast.show(undoDeleteToast);
                                $location.path(`/gear/collections/${$scope.gearCollection.Id}`);
                            }
                        });
                    });
                });
            }
        }
    }

    GearCollectionCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}
