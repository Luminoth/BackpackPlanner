///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../scripts/typings/angularjs/angular-route.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Gear {
    "use strict";

    export interface IGearSystemScope extends IAppScope {
        gearSystem: Models.Gear.GearSystem;

        showAddGearItem: (event: MouseEvent) => void;
        showDeleteConfirm: (event: MouseEvent) => void;
    }

    export interface IGearSystemRouteParams extends ng.route.IRouteParamsService {
        gearSystemId: number;
    }

    export class GearSystemCtrl {
        constructor($scope: IGearSystemScope, $routeParams: IGearSystemRouteParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
        
            $scope.gearSystem = AppState.getInstance().getGearState().getGearSystemById($routeParams.gearSystemId);
            if(null == $scope.gearSystem) {
                alert("The gear system does not exist!");
                $location.path("/gear/system");
                return;
            }

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

            $scope.showDeleteConfirm = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Gear System")
                    .content("Are you sure you wish to delete this gear system?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Gear system deleted!")
                    .content("The gear system has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content(`Deleted gear system: ${$scope.gearSystem.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content(`Restored gear system: ${$scope.gearSystem.Name}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        if(!AppState.getInstance().getGearState().deleteGearSystem($scope.gearSystem)) {
                            alert("Couldn't find the gear system to delete!");
                            return;
                        }

                        $location.path("/gear/systems");
                        $mdToast.show(deleteToast).then(() => {
                            // TODO: this does *not* restore the system to its containers
                            // and it should probably do so... but how?
                            AppState.getInstance().getGearState().addGearSystem($scope.gearSystem);
                            $mdToast.show(undoDeleteToast);
                            $location.path(`/gear/systems/${$scope.gearSystem.Id}`);
                        });
                    });
                });
            }
        }
    }

    GearSystemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}
