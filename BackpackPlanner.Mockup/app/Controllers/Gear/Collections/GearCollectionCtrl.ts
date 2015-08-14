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

        showAddGearSystem: (event: MouseEvent) => void;
        showAddGearItem: (event: MouseEvent) => void;
        showDeleteConfirm: (event: MouseEvent) => void;
    }

    export interface IGearCollectionRouteParams extends ng.route.IRouteParamsService {
        gearCollectionId: number;
    }

    export class GearCollectionCtrl {
        constructor($scope: IGearCollectionScope, $routeParams: IGearCollectionRouteParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.orderGearSystemsBy = "getName()";
            $scope.orderGearItemsBy = "getName()";
        
            $scope.gearCollection = AppState.getInstance().getGearState().getGearCollectionById($routeParams.gearCollectionId);
            if(null == $scope.gearCollection) {
                alert("The gear collection does not exist!");
                $location.path("/gear/collections");
                return;
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

            $scope.showDeleteConfirm = (event) => {
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
                    .content(`Deleted gear collection: ${$scope.gearCollection.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content(`Restored gear collection: ${$scope.gearCollection.Name}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        if(!AppState.getInstance().getGearState().deleteGearCollection($scope.gearCollection)) {
                            alert("Couldn't find the gear collection to delete!");
                            return;
                        }

                        $location.path("/gear/collections");
                        $mdToast.show(deleteToast).then((response: string) => {
                            if("ok" == response) {
                                // TODO: this does *not* restore the collection to its containers
                                // and it should probably do so... but how?
                                AppState.getInstance().getGearState().addGearCollection($scope.gearCollection);
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
