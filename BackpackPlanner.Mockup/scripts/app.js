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
    "ui.bootstrap",

    "mockupControllers",
    "mockupServices"
]);

mockupApp.run(["$rootScope",
    function($rootScope) {
        $rootScope.$on("$routeChangeSuccess",
            function(event, currentRoute, previousRoute) {
                $rootScope.title = currentRoute.title;
            }
        );
    }
]);

mockupApp.config(["$routeProvider",
    function($routeProvider) {
        $routeProvider.when("/", {
            redirectTo: "/index"
        })
        .when("/index", {
            templateUrl: "content/partials/main.html",
            title: "Backpacking Planner"
        })

        // gear items
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

        // gear systems
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

        // gear collections
        .when("/gear/collections", {
            templateUrl: "content/partials/gear/collections/collections.html",
            controller: "GearCollectionsCtrl",
            title: "Gear Collections"
        })
        .when("/gear/collections/add", {
            templateUrl: "content/partials/gear/collections/add.html",
            controller: "AddGearCollectionCtrl",
            title: "Add a Gear Collection"
        })
        .when("/gear/collections/:gearCollectionId", {
            templateUrl: "content/partials/gear/collections/collection.html",
            controller: "GearCollectionCtrl",
            title: "Gear Collection"
        })

        // meals
        .when("/meals", {
            templateUrl: "content/partials/meals.html",
            title: "Meals"
        })

        // trip itineraries
        .when("/trip/itineraries", {
            templateUrl: "content/partials/trip/itineraries/itineraries.html",
            title: "Trip Itineraries"
        })

        // trip plans
        .when("/trip/plans", {
            templateUrl: "content/partials/trip/plans/plans.html",
            title: "Trip Plans"
        })

        // personal information and settings
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
]);

mockupApp.config(["$mdThemingProvider",
    function($mdThemingProvider) {
        var primaryPalette = $mdThemingProvider.extendPalette("green", {
            "500": "668000",
            "A100": "501616",
            "contrastDefaultColor": "light"
        });

        var backgroundPalette = $mdThemingProvider.extendPalette("brown", {
            "500": "decd87"
        });

        var accentPalette = $mdThemingProvider.extendPalette("blue-grey", {
            //"500": "ffffff"
        });

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
]);
