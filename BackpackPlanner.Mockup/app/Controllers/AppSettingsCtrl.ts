///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../Models/AppSettings.ts" />

module BackpackPlanner.Mockup.Controllers {
    "use strict";

    export interface IAppSettingsScope extends ng.IScope {
        appSettings: Models.AppSettings;

        showAdvancedSettings: boolean;

        toggleAdvancedSettings: () => void;

        saveAppSettings: () => void;
        resetAppSettings: () => void;
        defaultAppSettings: () => void;

        deleteAllGearItems: (event: MouseEvent) => void;
        deleteAllGearSystems: (event: MouseEvent) => void;
        deleteAllGearCollections: (event: MouseEvent) => void;

        deleteAllMeals: (event: MouseEvent) => void;

        deleteAllTripItineraries: (event: MouseEvent) => void;
        deleteAllTripPlans: (event: MouseEvent) => void;

        deleteAllData: (event: MouseEvent) => void;
    }

    export class AppSettingsCtrl {
        constructor($scope: IAppSettingsScope, $location: ng.ILocationService, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
            $scope.appSettings = angular.copy(AppState.getInstance().getAppSettings());

            $scope.showAdvancedSettings = false;

            $scope.toggleAdvancedSettings = () => {
                $scope.showAdvancedSettings = !$scope.showAdvancedSettings;
            }

            $scope.saveAppSettings = () => {
                AppState.getInstance().getAppSettings().update($scope.appSettings);

                var updateToast = $mdToast.simple()
                    .content("Updated application settings!")
                    .action("OK")
                    .position("bottom left");

                $location.path("/settings");
                $mdToast.show(updateToast);
            }

            $scope.resetAppSettings = () => {
                $scope.appSettings = angular.copy(AppState.getInstance().getAppSettings());
            }

            $scope.defaultAppSettings = () => {
                $scope.appSettings.resetToDefaults();
            }

            $scope.deleteAllGearItems = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Gear Items")
                    .content("Are you sure you wish to delete all gear items? This action cannot be undone.")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All gear items deleted!")
                    .content("All gear items have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all gear items")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getGearState().deleteAllGearItems();

                        $mdToast.show(deleteToast);
                    });
                });
            }

            $scope.deleteAllGearSystems = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Gear Systems")
                    .content("Are you sure you wish to delete all gear systems? This action cannot be undone.")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All gear systems deleted!")
                    .content("All gear systems have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all gear systems")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getGearState().deleteAllGearSystems();

                        $mdToast.show(deleteToast);
                    });
                });
            }

            $scope.deleteAllGearCollections = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Gear Collections")
                    .content("Are you sure you wish to delete all gear collections? This action cannot be undone.")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All gear collections deleted!")
                    .content("All gear collections have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all gear collections")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getGearState().deleteAllGearCollections();

                        $mdToast.show(deleteToast);
                    });
                });
            }

            $scope.deleteAllMeals = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Meals")
                    .content("Are you sure you wish to delete all meals? This action cannot be undone.")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All meals deleted!")
                    .content("All meals have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all meals")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getMealState().deleteAllMeals();

                        $mdToast.show(deleteToast);
                    });
                });
            }

            $scope.deleteAllTripItineraries = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Trip Itineraries")
                    .content("Are you sure you wish to delete all trip itineraries? This action cannot be undone.")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All trip itineraries deleted!")
                    .content("All trip itineraries have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all trip itineraries")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getTripState().deleteAllTripItineraries();

                        $mdToast.show(deleteToast);
                    });
                });
            }

            $scope.deleteAllTripPlans = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Trip Plans")
                    .content("Are you sure you wish to delete all trip plans? This action cannot be undone.")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All trip plans deleted!")
                    .content("All trip plans have been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all trip plans")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().getTripState().deleteAllTripPlans();

                        $mdToast.show(deleteToast);
                    });
                });
            }

            $scope.deleteAllData = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete All Data")
                    .content("Are you sure you wish to delete all data? This action cannot be undone.")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("All data deleted!")
                    .content("All data has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content("Deleted all data")
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        AppState.getInstance().deleteAllData();

                        $mdToast.show(deleteToast);
                    });
                });
            }
        }
    }

    AppSettingsCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
}
