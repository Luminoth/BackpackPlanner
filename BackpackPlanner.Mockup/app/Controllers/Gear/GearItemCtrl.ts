///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../scripts/typings/angularjs/angular-route.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IGearItemScope extends IAppScope {
        gearItem: Models.Gear.GearItem;

        showDeleteConfirm: (event: MouseEvent) => void;
    }

    export interface IGearItemRouteParams extends ng.route.IRouteParamsService {
        gearItemId: number;
    }

    export class GearItemCtrl {
        constructor($scope: IGearItemScope, $routeParams: IGearItemRouteParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
        
            $scope.gearItem = AppManager.getInstance().getGearItemById($routeParams.gearItemId);
            if(null == $scope.gearItem) {
                alert("The gear item does not exist!");
                $location.path("/gear/items");
                return;
            }

            $scope.showDeleteConfirm = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Gear Item")
                    .content("Are you sure you wish to delete this gear item?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Gear item deleted!")
                    .content("The gear item has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content(`Deleted gear item: ${$scope.gearItem.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content(`Restored gear item: ${$scope.gearItem.Name}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        if(!AppManager.getInstance().deleteGearItem($scope.gearItem)) {
                            alert("Couldn't find the gear item to delete!");
                            return;
                        }

                        $location.path("/gear/items");
                        $mdToast.show(deleteToast).then(() => {
                            // TODO: this does *not* restore the item to its containers
                            // and it should probably do so... but how?
                            AppManager.getInstance().getGearItems().push($scope.gearItem);
                            $mdToast.show(undoDeleteToast);
                            $location.path(`/gear/items/${$scope.gearItem.Id}`);
                        });
                    });
                });
            }
        }
    }

    GearItemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}
