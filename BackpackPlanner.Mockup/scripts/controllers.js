function findGearItemById($filter, gearItems, gearItemId) {
    var foundItems = $filter("filter")(gearItems, { Id: parseInt(gearItemId) }, true);
    return foundItems.length > 0 ? foundItems[0] : null;
}

function deleteGearItem(gearItems, gearSystems, gearItem) {
    var idx = gearItems.indexOf(gearItem);
    if(idx < 0) {
        return false;
    }
    gearItems.splice(idx, 1);

    // TODO: remove the item from the systems, collections, and trip plans it belongs to

    return true;
}

function findGearSystemById($filter, gearSystems, gearSystemId) {
    var foundSystems = $filter("filter")(gearSystems, { Id: parseInt(gearSystemId) }, true);
    return foundSystems.length > 0 ? foundSystems[0] : null;
}

function deleteGearSystem(gearSystems, gearSystem) {
    var idx = gearSystems.indexOf(gearSystem);
    if(idx < 0) {
        return false;
    }
    gearSystems.splice(idx, 1);

    // TODO: remove the system from the collections, and trip plans it belongs to

    return true;
}

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

mockupControllers.controller("GearItemCtrl", ["$scope", "$routeParams", "$location", "$filter", "$mdDialog", "$mdToast",
    function ($scope, $routeParams, $location, $filter, $mdDialog, $mdToast) {
        var gearItem = findGearItemById($filter, $scope.gearItems, $routeParams.gearItemId);
        if(null == gearItem) {
            alert("The gear item does not exist!");
            $location.path("/gear/items");
            return;
        }
        $scope.gearItem = gearItem;

        $scope.showDeleteConfirm = function (event) {
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
                .targetEvent(Event);

            var deleteToast = $mdToast.simple()
                .content("Deleted gear item: " + $scope.gearItem.Name)
                .action("Undo")
                .position("bottom left");

            var undoDeleteToast = $mdToast.simple()
                .content("Restored gear item: " + $scope.gearItem.Name)
                .action("OK")
                .position("bottom left");

            $mdDialog.show(confirm).then(
                function () {
                    $mdDialog.show(receipt).then(
                        function () {
                            if(!deleteGearItem($scope.gearItems, $scope.gearSystems, $scope.gearItem)) {
                                alert("Couldn't find the gear item to delete!");
                                return;
                            }

                            $location.path("/gear/items");
                            $mdToast.show(deleteToast).then(function() {
                                // TODO: this does *not* restore the item to its containers
                                // and it should probably do so... but how?
                                $scope.gearItems.push($scope.gearItem);
                                $mdToast.show(undoDeleteToast);
                                $location.path("/gear/items/" + $scope.gearItem.Id);
                            });
                        });
                });
        }
    }
]);

mockupControllers.controller("AddGearItemCtrl", ["$scope", "$location", "$mdToast",
    function ($scope, $location, $mdToast) {
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

            var addToast = $mdToast.simple()
                .content("Added gear item: " + $scope.gearItem.Name)
                .action("Undo")
                .position("bottom left");

            var undoAddToast = $mdToast.simple()
                .content("Removed gear item: " + $scope.gearItem.Name)
                .action("OK")
                .position("bottom left");

            $location.path("/gear/items");
            $mdToast.show(addToast).then(function() {
                deleteGearItem($scope.gearItems, $scope.gearSystems, $scope.gearItem);
                $mdToast.show(undoAddToast);
            });
        }
    }
]);

/* gear systems */

mockupControllers.controller("GearSystemsCtrl", ["$scope",
    function ($scope) {
        $scope.orderBy = "Name";
    }
]);

mockupControllers.controller("GearSystemCtrl", ["$scope", "$routeParams", "$location", "$filter", "$mdDialog", "$mdToast",
    function ($scope, $routeParams, $location, $filter, $mdDialog, $mdToast) {
        var gearSystem = findGearSystemById($filter, $scope.gearSystems, $routeParams.gearSystemId);
        if(null == gearSystem) {
            alert("The gear system does not exist!");
            $location.path("/gear/system");
            return;
        }
        $scope.gearSystem = gearSystem;

        $scope.showDeleteConfirm = function (event) {
            var confirm = $mdDialog.confirm()
                .parent(angular.element(document.body))
                .title("Delete Gear System")
                .content("Are you sure you wish to delete this gear item?")
                .ok("Yes")
                .cancel("No")
                .targetEvent(event);

            var receipt = $mdDialog.alert()
                .parent(angular.element(document.body))
                .title("Gear system deleted!")
                .content("The gear system has been deleted.")
                .ok("OK")
                .targetEvent(Event);

            var deleteToast = $mdToast.simple()
                .content("Deleted gear system: " + $scope.gearSystem.Name)
                .action("Undo")
                .position("bottom left");

            var undoDeleteToast = $mdToast.simple()
                .content("Restored gear system: " + $scope.gearSystem.Name)
                .action("OK")
                .position("bottom left");

            $mdDialog.show(confirm).then(
                function () {
                    $mdDialog.show(receipt).then(
                        function () {
                            if(!deleteGearSystem($scope.gearSystems, $scope.gearSystem)) {
                                alert("Couldn't find the gear system to delete!");
                                return;
                            }

                            $location.path("/gear/systems");
                            $mdToast.show(deleteToast).then(function() {
                                // TODO: this does *not* restore the system to its containers
                                // and it should probably do so... but how?
                                $scope.gearSystems.push($scope.gearSystem);
                                $mdToast.show(undoDeleteToast);
                                $location.path("/gear/systems/" + $scope.gearSystem.Id);
                            });
                        });
                });
        }

        function addGearItemDlgCtrl($scope, $mdDialog, gearSystem, gearItems) {
            $scope.gearSystem = gearSystem;
            $scope.gearItems = gearItems;
            $scope.orderBy = "Name";

            $scope.close = function() {
                $mdDialog.hide();
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

mockupControllers.controller("AddGearSystemCtrl", ["$scope", "$location", "$mdToast",
    function ($scope, $location, $mdToast) {
        $scope.gearSystem = {
            GearItems: []
        }

        $scope.addSystem = function(gearSystem) {
            $scope.gearSystem = angular.copy(gearSystem);
            //TODO: $scope.gearSystem.Id = ???
            $scope.gearSystems.push($scope.gearSystem);

            var addToast = $mdToast.simple()
                .content("Added gear system: " + $scope.gearSystem.Name)
                .action("Undo")
                .position("bottom left");

            var undoAddToast = $mdToast.simple()
                .content("Removed gear system: " + $scope.gearSystem.Name)
                .action("OK")
                .position("bottom left");

            $location.path("/gear/systems");
            $mdToast.show(addToast).then(function() {
                deleteGearSystem($scope.gearSystems, $scope.gearSystem);
                $mdToast.show(undoAddToast);
            });
        }
    }
]);
