var mockupControllers = angular.module("mockupControllers", []);

mockupControllers.controller("AppCtrl", ["$scope", "$location", "$mdSidenav", "AppSettings", "UserInfo", "GearItem", "GearSystem",
    function($scope, $location, $mdSidenav, AppSettings, UserInfo, GearItem, GearSystem) {
        $scope.appSettings = AppSettings.get();

        // TODO: this keeps giving an error, I dunno what to do to fix it
        var userInfo = UserInfo.get();
        userInfo.BirthDate = new Date(userInfo.BirthDate);
        $scope.userInfo = userInfo;

        // load the data globally to better simulate the application working
        $scope.gearItems = GearItem.query();
        $scope.gearSystems = GearSystem.query();

        $scope.isActive = function(viewLocation) {
            // set the nav item as active when we're looking at its location
            return $location.path() === viewLocation;
        }

        $scope.toggleSidenav = function() {
            $mdSidenav("left").toggle();
        }
    }
]);

/* gear items */

mockupControllers.controller("GearItemsCtrl", ["$scope",
    function ($scope) {
        $scope.orderBy = "Name";
    }
]);

mockupControllers.controller("GearItemCtrl", ["$scope", "$routeParams", "$location", "$mdDialog",
    function ($scope, $routeParams, $location, $mdDialog) {

        $scope.showDeleteConfirm = function (event) {
            var confirm = $mdDialog.confirm()
                .parent(angular.element(document.body))
                .title("Are you sure you wish to delete this gear item?")
                .content("Deleting a gear item may not be undone.")
                .ok("Yes")
                .cancel("No")
                .targetEvent(event);

            var receipt = $mdDialog.alert()
                .parent(angular.element(document.body))
                .title("Gear item deleted!")
                .content("The gear item has been deleted.")
                .ok("OK")
                .targetEvent(Event);

            $mdDialog.show(confirm).then(
                function () {
                    $mdDialog.show(receipt).then(
                        function () {
                            var idx = $scope.gearItems.indexOf($scope.gearItem);
                            if(idx > -1) {
                                $scope.gearItems.splice(idx, 1);
                            } else {
                                alert("Couldn't find item to delete!");
                            }
                            // TODO: remove the item from the systems, collections, and trip plans it belongs to
                            $location.path("/gear/items");
                        });
                });
        }
    }
]);

mockupControllers.controller("AddGearItemCtrl", ["$scope", "$location",
    function ($scope, $location) {
        $scope.gearItem = {
            Carried: "Carried",
            ConsumedPerDay: 0,
            WeightInOunces: 0,
            CostInUSD: 0
        }

        $scope.addItem = function(gearItem) {
            $scope.gearItem = angular.copy(gearItem);
            //TODO: $scope.gearItem.Id = ???
            $scope.gearItems.push($scope.gearItem);
            $location.path("/gear/items");
        }
    }
]);

/* gear systems */

mockupControllers.controller("GearSystemsCtrl", ["$scope",
    function ($scope) {
        $scope.orderBy = "Name";
    }
]);

mockupControllers.controller("GearSystemCtrl", ["$scope", "$routeParams", "$location", "$mdDialog",
    function ($scope, $routeParams, $location, $mdDialog) {

        $scope.showDeleteConfirm = function (event) {
            var confirm = $mdDialog.confirm()
                .parent(angular.element(document.body))
                .title("Are you sure you wish to delete this gear system?")
                .content("Deleting a gear system may not be undone.")
                .ok("Yes")
                .cancel("No")
                .targetEvent(event);

            var receipt = $mdDialog.alert()
                .parent(angular.element(document.body))
                .title("Gear system deleted!")
                .content("The gear system has been deleted.")
                .ok("OK")
                .targetEvent(Event);

            $mdDialog.show(confirm).then(
                function () {
                    $mdDialog.show(receipt).then(
                        function () {
                            var idx = $scope.gearSystems.indexOf($scope.gearSystem);
                            if(idx > -1) {
                                $scope.gearSystems.splice(idx, 1);
                            } else {
                                alert("Couldn't find item to delete!");
                            }
                            // TODO: remove the system from the collections, and trip plans it belongs to
                            $location.path("/gear/systems");
                        });
                });
        }

        function addGearItemDlgCtrl($scope, $mdDialog, gearSystem, gearItems) {
            $scope.gearSystem = gearSystem;
            $scope.gearItems = gearItems;
            $scope.orderBy = "Name";

            $scope.cancel = function() {
                $mdDialog.cancel();
            };

            $scope.isSelected = function (gearItem) {
                return $scope.gearSystem.GearItems.indexOf(gearItem) > -1;
            }

            $scope.toggle = function (gearItem) {
                var idx = $scope.gearSystem.GearItems.indexOf(gearItem);
                if(idx > -1) {
                    $scope.gearSystem.GearItems.splice(idx, 1);
                } else {
                    $scope.gearSystem.GearItems.push(gearItem);
                }
            };

            $scope.addGearItems = function() {
                $mdDialog.hide();
            };
        }

        $scope.showAddGearItem = function (event) {
            $mdDialog.show({
                controller: addGearItemDlgCtrl,
                templateUrl: "/partials/gear/systems/add-item.html",
                parent: angular.element(document.body),
                targetEvent: event,
                locals: {
                    gearSystem: $scope.gearSystem,
                    gearItems: $scope.gearItems
                }
            });
        }
    }
]);

mockupControllers.controller("AddGearSystemCtrl", ["$scope", "$location",
    function ($scope, $location) {
        $scope.gearSystem = {
            GearItems: []
        }

        $scope.addSystem = function(gearSystem) {
            $scope.gearSystem = angular.copy(gearSystem);
            //TODO: $scope.gearSystem.Id = ???
            $scope.gearSystems.push($scope.gearSystem);
            $location.path("/gear/systems");
        }
    }
]);
