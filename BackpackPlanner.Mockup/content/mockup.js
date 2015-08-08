///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            "use strict";
            (function (Units) {
                Units[Units["Imperial"] = 0] = "Imperial";
                Units[Units["Metric"] = 1] = "Metric";
            })(Models.Units || (Models.Units = {}));
            var Units = Models.Units;
            function appSettingsResourceFactory($resource) {
                var queryAction = {
                    method: "GET",
                    isArray: false
                };
                return $resource("data/settings.json", {}, {
                    get: queryAction
                });
            }
            Models.appSettingsResourceFactory = appSettingsResourceFactory;
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            "use strict";
            (function (Sex) {
                Sex[Sex["NotSpecified"] = 0] = "NotSpecified";
                Sex[Sex["Male"] = 1] = "Male";
                Sex[Sex["Female"] = 2] = "Female";
            })(Models.Sex || (Models.Sex = {}));
            var Sex = Models.Sex;
            function userInformationResourceFactory($resource) {
                var queryAction = {
                    method: "GET",
                    isArray: false
                };
                return $resource("data/user.json", {}, {
                    get: queryAction
                });
            }
            Models.userInformationResourceFactory = userInformationResourceFactory;
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                (function (GearCarried) {
                    GearCarried[GearCarried["NotCarried"] = 0] = "NotCarried";
                    GearCarried[GearCarried["Carried"] = 1] = "Carried";
                    GearCarried[GearCarried["Worn"] = 2] = "Worn";
                })(Gear.GearCarried || (Gear.GearCarried = {}));
                var GearCarried = Gear.GearCarried;
                function gearItemResourceFactory($resource) {
                    var queryAction = {
                        method: "GET",
                        isArray: true
                    };
                    return $resource("data/gear/items.json", {}, {
                        query: queryAction
                    });
                }
                Gear.gearItemResourceFactory = gearItemResourceFactory;
                function newGearItem() {
                    return {
                        Id: -1,
                        Name: "",
                        Url: "",
                        Make: "",
                        Model: "",
                        Carried: GearCarried.Carried,
                        WeightInOunces: 0,
                        CostInUSD: 0,
                        IsConsumable: false,
                        ConsumedPerDay: 0,
                        Note: ""
                    };
                }
                Gear.newGearItem = newGearItem;
                function getNextGearItemId() {
                    // TODO: write this
                    return -1;
                }
                Gear.getNextGearItemId = getNextGearItemId;
                function getGearItemIndexById(gearItems, gearItemId) {
                    for (var i = 0; i < gearItems.length; ++i) {
                        var gearItem = gearItems[i];
                        if (gearItem.Id == gearItemId) {
                            return i;
                        }
                    }
                    return -1;
                }
                Gear.getGearItemIndexById = getGearItemIndexById;
                function getGearItemById(gearItems, gearItemId) {
                    var idx = getGearItemIndexById(gearItems, gearItemId);
                    return idx < 0 ? null : gearItems[idx];
                }
                Gear.getGearItemById = getGearItemById;
                function deleteGearItem(gearItems, gearSystems, gearCollections, gearItem) {
                    var idx = getGearItemIndexById(gearItems, gearItem.Id);
                    if (idx < 0) {
                        return false;
                    }
                    gearItems.splice(idx, 1);
                    // TODO: remove the item from the systems, collections, and trip plans it belongs to
                    return true;
                }
                Gear.deleteGearItem = deleteGearItem;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                function gearSystemResourceFactory($resource) {
                    var queryAction = {
                        method: "GET",
                        isArray: true
                    };
                    return $resource("data/gear/systems.json", {}, {
                        query: queryAction
                    });
                }
                Gear.gearSystemResourceFactory = gearSystemResourceFactory;
                function newGearSystem() {
                    return {
                        Id: -1,
                        Name: "",
                        GearItems: [],
                        Note: ""
                    };
                }
                Gear.newGearSystem = newGearSystem;
                function getNextGearSystemId() {
                    // TODO: write this
                    return -1;
                }
                Gear.getNextGearSystemId = getNextGearSystemId;
                function getGearSystemIndexById(gearSystems, gearSystemId) {
                    for (var i = 0; i < gearSystems.length; ++i) {
                        var gearSystem = gearSystems[i];
                        if (gearSystem.Id == gearSystemId) {
                            return i;
                        }
                    }
                    return -1;
                }
                Gear.getGearSystemIndexById = getGearSystemIndexById;
                function getGearSystemById(gearSystems, gearSystemId) {
                    var idx = getGearSystemIndexById(gearSystems, gearSystemId);
                    return idx < 0 ? null : gearSystems[idx];
                }
                Gear.getGearSystemById = getGearSystemById;
                function deleteGearSystem(gearSystems, gearCollections, gearSystem) {
                    var idx = getGearSystemIndexById(gearSystems, gearSystem.Id);
                    if (idx < 0) {
                        return false;
                    }
                    gearSystems.splice(idx, 1);
                    // TODO: remove the system from the collections, and trip plans it belongs to
                    return true;
                }
                Gear.deleteGearSystem = deleteGearSystem;
                function getGearSystemWeightInOunces(gearSystem, gearItems) {
                    var weightInOunces = 0;
                    for (var i = 0; i < gearSystem.GearItems.length; ++i) {
                        var gearItem = Gear.getGearItemById(gearItems, gearSystem.GearItems[i]);
                        if (null == gearItem) {
                            continue;
                        }
                        weightInOunces += gearItem.WeightInOunces;
                    }
                    return weightInOunces;
                }
                Gear.getGearSystemWeightInOunces = getGearSystemWeightInOunces;
                function getGearSystemCostInUSD(gearSystem, gearItems) {
                    var costInUSD = 0;
                    for (var i = 0; i < gearSystem.GearItems.length; ++i) {
                        var gearItem = Gear.getGearItemById(gearItems, gearSystem.GearItems[i]);
                        if (null == gearItem) {
                            continue;
                        }
                        costInUSD += gearItem.CostInUSD;
                    }
                    return costInUSD;
                }
                Gear.getGearSystemCostInUSD = getGearSystemCostInUSD;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                function gearCollectionResourceFactory($resource) {
                    var queryAction = {
                        method: "GET",
                        isArray: true
                    };
                    return $resource("data/gear/collections.json", {}, {
                        query: queryAction
                    });
                }
                Gear.gearCollectionResourceFactory = gearCollectionResourceFactory;
                function newGearCollection() {
                    return {
                        Id: -1
                    };
                }
                Gear.newGearCollection = newGearCollection;
                function getNextGearCollectionId() {
                    // TODO: write this
                    return -1;
                }
                Gear.getNextGearCollectionId = getNextGearCollectionId;
                function getGearCollectionIndexById(gearCollections, gearCollectionId) {
                    for (var i = 0; i < gearCollections.length; ++i) {
                        var gearCollection = gearCollections[i];
                        if (gearCollection.Id == gearCollectionId) {
                            return i;
                        }
                    }
                    return -1;
                }
                Gear.getGearCollectionIndexById = getGearCollectionIndexById;
                function getGearCollectionById(gearCollections, gearCollectionId) {
                    var idx = getGearCollectionIndexById(gearCollections, gearCollectionId);
                    return idx < 0 ? null : gearCollections[idx];
                }
                Gear.getGearCollectionById = getGearCollectionById;
                function deleteGearCollection(gearCollections, gearCollection) {
                    var idx = gearCollections.indexOf(gearCollection);
                    if (idx < 0) {
                        return false;
                    }
                    gearCollections.splice(idx, 1);
                    // TODO: remove the collection from the trip plans it belongs to
                    return true;
                }
                Gear.deleteGearCollection = deleteGearCollection;
                function getGearCollectionWeightInOunces(gearCollection, gearSystems, gearItems) {
                    var weightInOunces = 0;
                    // TODO: calculate this
                    return weightInOunces;
                }
                Gear.getGearCollectionWeightInOunces = getGearCollectionWeightInOunces;
                function getGearCollectionCostInUSD(gearCollection, gearSystems, gearItems) {
                    var costInUSD = 0;
                    // TODO: calculate this
                    return costInUSD;
                }
                Gear.getGearCollectionCostInUSD = getGearCollectionCostInUSD;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../Models/AppSettings.ts" />
///<reference path="../Models/UserInformation.ts" />
///<reference path="../Models/Gear/GearItem.ts" />
///<reference path="../Models/Gear/GearSystem.ts" />
///<reference path="../Models/Gear/GearCollection.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            "use strict";
            var AppCtrl = (function () {
                function AppCtrl($scope, $location, $mdSidenav, appSettingsResource, userInformationResource, gearItemResource, gearSystemResource, gearCollectionResource) {
                    // load the application settings
                    $scope.appSettingsLoading = true;
                    appSettingsResource.get().$promise.then(function (appSettings) {
                        $scope.appSettings = appSettings;
                        $scope.appSettingsLoading = false;
                    });
                    // load the user's personal information
                    $scope.userInfoLoading = true;
                    userInformationResource.get().$promise.then(function (userInformation) {
                        $scope.userInfo = userInformation;
                        $scope.userInfoLoading = false;
                    });
                    // load the gear items
                    $scope.gearItemsLoading = true;
                    gearItemResource.query().$promise.then(function (gearItems) {
                        $scope.gearItems = gearItems;
                        $scope.gearItemsLoading = false;
                    });
                    // load the gear systems
                    $scope.gearSystemsLoading = true;
                    gearSystemResource.query().$promise.then(function (gearSystems) {
                        $scope.gearSystems = gearSystems;
                        $scope.gearSystemsLoading = false;
                    });
                    // load the gear collections
                    $scope.gearCollectionsLoading = true;
                    gearCollectionResource.query().$promise.then(function (gearCollections) {
                        $scope.gearCollections = gearCollections;
                        $scope.gearCollectionsLoading = false;
                    });
                    // load the meals
                    $scope.mealsLoading = true;
                    $scope.meals = [];
                    $scope.mealsLoading = false;
                    // load the trip itineraries
                    $scope.tripItinerariesLoading = true;
                    $scope.tripItineraries = [];
                    $scope.tripItinerariesLoading = false;
                    // load the trip plans
                    $scope.tripPlansLoading = true;
                    $scope.tripPlans = [];
                    $scope.tripPlansLoading = false;
                    $scope.isActive = function (viewLocation) {
                        // set the nav item as active when we're looking at its location
                        return $location.path() === viewLocation;
                    };
                    $scope.toggleSidenav = function () {
                        $mdSidenav("left").toggle();
                    };
                }
                AppCtrl.prototype.getGearItemById = function () {
                    return null;
                };
                return AppCtrl;
            })();
            Controllers.AppCtrl = AppCtrl;
            AppCtrl.$inject = ["$scope", "$location", "$mdSidenav", "AppSettingsResource", "UserInformationResource",
                "GearItemResource", "GearSystemResource", "GearCollectionResource"];
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var AddGearItemCtrl = (function () {
                    function AddGearItemCtrl($scope, $location, $mdToast) {
                        $scope.gearItem = Mockup.Models.Gear.newGearItem();
                        $scope.addItem = function (gearItem) {
                            $scope.gearItem = angular.copy(gearItem);
                            $scope.gearItem.Id = Mockup.Models.Gear.getNextGearItemId();
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
                            $mdToast.show(addToast).then(function () {
                                Mockup.Models.Gear.deleteGearItem($scope.gearItems, $scope.gearSystems, $scope.gearCollections, $scope.gearItem);
                                $mdToast.show(undoAddToast);
                            });
                        };
                    }
                    return AddGearItemCtrl;
                })();
                Gear.AddGearItemCtrl = AddGearItemCtrl;
                AddGearItemCtrl.$inject = ["$scope", "$location", "$mdToast"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../scripts/typings/angularjs/angular-route.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearItemCtrl = (function () {
                    function GearItemCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                        $scope.gearItem = Mockup.Models.Gear.getGearItemById($scope.gearItems, $routeParams.gearItemId);
                        if (null == $scope.gearItem) {
                            alert("The gear item does not exist!");
                            $location.path("/gear/items");
                            return;
                        }
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
                                .targetEvent(event);
                            var deleteToast = $mdToast.simple()
                                .content("Deleted gear item: " + $scope.gearItem.Name)
                                .action("Undo")
                                .position("bottom left");
                            var undoDeleteToast = $mdToast.simple()
                                .content("Restored gear item: " + $scope.gearItem.Name)
                                .action("OK")
                                .position("bottom left");
                            $mdDialog.show(confirm).then(function () {
                                $mdDialog.show(receipt).then(function () {
                                    if (!Mockup.Models.Gear.deleteGearItem($scope.gearItems, $scope.gearSystems, $scope.gearCollections, $scope.gearItem)) {
                                        alert("Couldn't find the gear item to delete!");
                                        return;
                                    }
                                    $location.path("/gear/items");
                                    $mdToast.show(deleteToast).then(function () {
                                        // TODO: this does *not* restore the item to its containers
                                        // and it should probably do so... but how?
                                        $scope.gearItems.push($scope.gearItem);
                                        $mdToast.show(undoDeleteToast);
                                        $location.path("/gear/items/" + $scope.gearItem.Id);
                                    });
                                });
                            });
                        };
                    }
                    return GearItemCtrl;
                })();
                Gear.GearItemCtrl = GearItemCtrl;
                GearItemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearItemsCtrl = (function () {
                    function GearItemsCtrl($scope) {
                        $scope.orderBy = "Name";
                    }
                    return GearItemsCtrl;
                })();
                Gear.GearItemsCtrl = GearItemsCtrl;
                GearItemsCtrl.$inject = ["$scope"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var AddGearSystemCtrl = (function () {
                    function AddGearSystemCtrl($scope, $location, $mdDialog, $mdToast) {
                        $scope.gearSystem = Mockup.Models.Gear.newGearSystem();
                        $scope.getGearItem = function (gearItemId) {
                            return Mockup.Models.Gear.getGearItemById($scope.gearItems, gearItemId);
                        };
                        $scope.showAddGearItem = function (event) {
                            $mdDialog.show({
                                controller: Gear.AddGearItemDlgCtrl,
                                templateUrl: "content/partials/gear/systems/add-item.html",
                                parent: angular.element(document.body),
                                targetEvent: event,
                                locals: {
                                    gearItems: $scope.gearItems,
                                    gearSystem: $scope.gearSystem
                                }
                            });
                        };
                        $scope.addSystem = function (gearSystem) {
                            $scope.gearSystem = angular.copy(gearSystem);
                            $scope.gearSystem.Id = Mockup.Models.Gear.getNextGearSystemId();
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
                            $mdToast.show(addToast).then(function () {
                                Mockup.Models.Gear.deleteGearSystem($scope.gearSystems, $scope.gearCollections, $scope.gearSystem);
                                $mdToast.show(undoAddToast);
                            });
                        };
                    }
                    return AddGearSystemCtrl;
                })();
                Gear.AddGearSystemCtrl = AddGearSystemCtrl;
                AddGearSystemCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../scripts/typings/angularjs/angular-route.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearSystemCtrl = (function () {
                    function GearSystemCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                        $scope.gearSystem = Mockup.Models.Gear.getGearSystemById($scope.gearSystems, $routeParams.gearSystemId);
                        if (null == $scope.gearSystem) {
                            alert("The gear system does not exist!");
                            $location.path("/gear/system");
                            return;
                        }
                        $scope.getGearItem = function (gearItemId) {
                            return Mockup.Models.Gear.getGearItemById($scope.gearItems, gearItemId);
                        };
                        $scope.showAddGearItem = function (event) {
                            $mdDialog.show({
                                controller: Gear.AddGearItemDlgCtrl,
                                templateUrl: "content/partials/gear/systems/add-item.html",
                                parent: angular.element(document.body),
                                targetEvent: event,
                                locals: {
                                    gearItems: $scope.gearItems,
                                    gearSystem: $scope.gearSystem
                                }
                            });
                        };
                        $scope.showDeleteConfirm = function (event) {
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
                                .content("Deleted gear system: " + $scope.gearSystem.Name)
                                .action("Undo")
                                .position("bottom left");
                            var undoDeleteToast = $mdToast.simple()
                                .content("Restored gear system: " + $scope.gearSystem.Name)
                                .action("OK")
                                .position("bottom left");
                            $mdDialog.show(confirm).then(function () {
                                $mdDialog.show(receipt).then(function () {
                                    if (!Mockup.Models.Gear.deleteGearSystem($scope.gearSystems, $scope.gearCollections, $scope.gearSystem)) {
                                        alert("Couldn't find the gear system to delete!");
                                        return;
                                    }
                                    $location.path("/gear/systems");
                                    $mdToast.show(deleteToast).then(function () {
                                        // TODO: this does *not* restore the system to its containers
                                        // and it should probably do so... but how?
                                        $scope.gearSystems.push($scope.gearSystem);
                                        $mdToast.show(undoDeleteToast);
                                        $location.path("/gear/systems/" + $scope.gearSystem.Id);
                                    });
                                });
                            });
                        };
                    }
                    return GearSystemCtrl;
                })();
                Gear.GearSystemCtrl = GearSystemCtrl;
                GearSystemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearSystemsCtrl = (function () {
                    function GearSystemsCtrl($scope) {
                        $scope.orderBy = "Name";
                        $scope.getWeightInOunces = function (gearSystem) {
                            return Mockup.Models.Gear.getGearSystemWeightInOunces(gearSystem, $scope.gearItems);
                        };
                        $scope.getCostInUSD = function (gearSystem) {
                            return Mockup.Models.Gear.getGearSystemCostInUSD(gearSystem, $scope.gearItems);
                        };
                    }
                    return GearSystemsCtrl;
                })();
                Gear.GearSystemsCtrl = GearSystemsCtrl;
                GearSystemsCtrl.$inject = ["$scope"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearCollectionsCtrl = (function () {
                    function GearCollectionsCtrl($scope) {
                        $scope.orderBy = "Name";
                        $scope.getWeightInOunces = function (gearCollection) {
                            return Mockup.Models.Gear.getGearCollectionWeightInOunces(gearCollection, $scope.gearSystems, $scope.gearItems);
                        };
                        $scope.getCostInUSD = function (gearCollection) {
                            return Mockup.Models.Gear.getGearCollectionCostInUSD(gearCollection, $scope.gearSystems, $scope.gearItems);
                        };
                    }
                    return GearCollectionsCtrl;
                })();
                Gear.GearCollectionsCtrl = GearCollectionsCtrl;
                GearCollectionsCtrl.$inject = ["$scope"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../scripts/typings/angularjs/angular-route.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var RootScopeConfig = (function () {
            function RootScopeConfig($rootScope) {
                $rootScope.$on("$routeChangeSuccess", function (event, currentRoute, previousRoute) {
                    // change the app menu title when the route changes
                    $rootScope.title = currentRoute.title;
                });
            }
            return RootScopeConfig;
        })();
        Mockup.RootScopeConfig = RootScopeConfig;
        ;
        RootScopeConfig.$inject = ["$rootScope"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular-route.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var RouteConfig = (function () {
            function RouteConfig($routeProvider) {
                $routeProvider.when("/", {
                    redirectTo: "/index"
                })
                    .when("/index", {
                    templateUrl: "content/partials/main.html",
                    title: "Backpacking Planner"
                })
                    .when("/gear/items", {
                    templateUrl: "content/partials/gear/items/items.html",
                    controller: "GearItemsCtrl",
                    title: "Gear Items"
                })
                    .when("/gear/items/add", {
                    templateUrl: "content/partials/gear/items/add.html",
                    controller: "AddGearItemCtrl",
                    title: "Add a Gear Item"
                })
                    .when("/gear/items/:gearItemId", {
                    templateUrl: "content/partials/gear/items/item.html",
                    controller: "GearItemCtrl",
                    title: "Gear Item"
                })
                    .when("/gear/systems", {
                    templateUrl: "content/partials/gear/systems/systems.html",
                    controller: "GearSystemsCtrl",
                    title: "Gear Systems"
                })
                    .when("/gear/systems/add", {
                    templateUrl: "content/partials/gear/systems/add.html",
                    controller: "AddGearSystemCtrl",
                    title: "Add a Gear System"
                })
                    .when("/gear/systems/:gearSystemId", {
                    templateUrl: "content/partials/gear/systems/system.html",
                    controller: "GearSystemCtrl",
                    title: "Gear System"
                })
                    .when("/gear/collections", {
                    templateUrl: "content/partials/gear/collections/collections.html",
                    controller: "GearCollectionsCtrl",
                    title: "Gear Collections"
                })
                    .when("/meals", {
                    templateUrl: "content/partials/meals.html",
                    title: "Meals"
                })
                    .when("/trip/itineraries", {
                    templateUrl: "content/partials/trip/itineraries/itineraries.html",
                    title: "Trip Itineraries"
                })
                    .when("/trip/plans", {
                    templateUrl: "content/partials/trip/plans/plans.html",
                    title: "Trip Plans"
                })
                    .when("/personal", {
                    templateUrl: "content/partials/personal.html",
                    title: "Personal Information"
                })
                    .when("/settings", {
                    templateUrl: "content/partials/settings.html",
                    title: "Settings"
                })
                    .when("/help", {
                    templateUrl: "content/partials/help.html",
                    title: "Help"
                })
                    .when("/404", {
                    templateUrl: "content/partials/404.html",
                    title: "Backpacking Planner"
                }).when("/500", {
                    templateUrl: "content/partials/500.html",
                    title: "Backpacking Planner"
                })
                    .otherwise({
                    redirectTo: "/404"
                });
            }
            return RouteConfig;
        })();
        Mockup.RouteConfig = RouteConfig;
        ;
        RouteConfig.$inject = ["$routeProvider"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../Scripts/typings/angular-material/angular-material.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var ThemeConfig = (function () {
            function ThemeConfig($mdThemingProvider) {
                var primaryPalette = $mdThemingProvider.extendPalette("green", {
                    "500": "668000",
                    "A100": "501616",
                    "contrastDefaultColor": "light"
                });
                var backgroundPalette = $mdThemingProvider.extendPalette("brown", {
                    "500": "decd87"
                });
                var accentPalette = $mdThemingProvider.extendPalette("blue-grey", {});
                $mdThemingProvider.definePalette("mockupPrimaryPalette", primaryPalette);
                $mdThemingProvider.definePalette("mockupBackgroundPalette", backgroundPalette);
                $mdThemingProvider.definePalette("mockupAccentPalette", accentPalette);
                $mdThemingProvider.theme("default")
                    .primaryPalette("mockupPrimaryPalette", {
                    "default": "500",
                    "hue-1": "A100"
                })
                    .backgroundPalette("mockupBackgroundPalette", {
                    "default": "500"
                })
                    .accentPalette("mockupAccentPalette", {
                    "default": "500"
                });
            }
            return ThemeConfig;
        })();
        Mockup.ThemeConfig = ThemeConfig;
        ;
        ThemeConfig.$inject = ["$mdThemingProvider"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular.d.ts" />
///<reference path="Controllers/AppCtrl.ts" />
///<reference path="Controllers/Gear/AddGearItemCtrl.ts" />
///<reference path="Controllers/Gear/GearItemCtrl.ts" />
///<reference path="Controllers/Gear/GearItemsCtrl.ts" />
///<reference path="Controllers/Gear/AddGearSystemCtrl.ts" />
///<reference path="Controllers/Gear/GearSystemCtrl.ts" />
///<reference path="Controllers/Gear/GearSystemsCtrl.ts" />
///<reference path="Controllers/Gear/GearCollectionsCtrl.ts" />
///<reference path="RootScopeConfig.ts" />
///<reference path="RouteConfig.ts" />
///<reference path="ThemeConfig.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var mockupApp = angular.module("mockupApp", [
            "ngAnimate",
            "ngAria",
            "ngMessages",
            "ngResource",
            "ngRoute",
            "ngSanitize",
            //"ngTouch",        // this breaks md-button ng-click operation
            "ngMaterial",
            "ngMdIcons",
            "ui.bootstrap"
        ]);
        // configure the root scope
        mockupApp.run(Mockup.RootScopeConfig);
        // configure routing
        mockupApp.config(Mockup.RouteConfig);
        // configure the material design theme
        mockupApp.config(Mockup.ThemeConfig);
        // inject services
        mockupApp.factory("AppSettingsResource", ["$resource", Mockup.Models.appSettingsResourceFactory]);
        mockupApp.factory("UserInformationResource", ["$resource", Mockup.Models.userInformationResourceFactory]);
        mockupApp.factory("GearItemResource", ["$resource", Mockup.Models.Gear.gearItemResourceFactory]);
        mockupApp.factory("GearSystemResource", ["$resource", Mockup.Models.Gear.gearSystemResourceFactory]);
        mockupApp.factory("GearCollectionResource", ["$resource", Mockup.Models.Gear.gearCollectionResourceFactory]);
        // inject controllers
        mockupApp.controller("AppCtrl", Mockup.Controllers.AppCtrl);
        mockupApp.controller("GearItemCtrl", Mockup.Controllers.Gear.GearItemCtrl);
        mockupApp.controller("GearItemsCtrl", Mockup.Controllers.Gear.GearItemsCtrl);
        mockupApp.controller("AddGearItemCtrl", Mockup.Controllers.Gear.AddGearItemCtrl);
        mockupApp.controller("GearSystemCtrl", Mockup.Controllers.Gear.GearSystemCtrl);
        mockupApp.controller("GearSystemsCtrl", Mockup.Controllers.Gear.GearSystemsCtrl);
        mockupApp.controller("AddGearSystemCtrl", Mockup.Controllers.Gear.AddGearSystemCtrl);
        mockupApp.controller("GearCollectionsCtrl", Mockup.Controllers.Gear.GearCollectionsCtrl);
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var AddGearItemDlgCtrl = (function () {
                    function AddGearItemDlgCtrl($scope, $mdDialog, gearItems, gearSystem) {
                        $scope.gearItems = gearItems;
                        $scope.gearSystem = gearSystem;
                        $scope.orderBy = "Name";
                        $scope.close = function () {
                            $mdDialog.hide();
                        };
                        $scope.isSelected = function (gearItem) {
                            return $scope.gearSystem.GearItems.indexOf(gearItem.Id) >= 0;
                        };
                        $scope.toggle = function (gearItem) {
                            var idx = $scope.gearSystem.GearItems.indexOf(gearItem.Id);
                            if (idx < 0) {
                                $scope.gearSystem.GearItems.push(gearItem.Id);
                            }
                            else {
                                $scope.gearSystem.GearItems.splice(idx, 1);
                            }
                        };
                    }
                    return AddGearItemDlgCtrl;
                })();
                Gear.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
//# sourceMappingURL=mockup.js.map