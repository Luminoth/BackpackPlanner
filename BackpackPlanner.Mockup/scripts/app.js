var mockupApp = angular.module("mockupApp", [
    "ngRoute",
    "ngAria",
    "ngMaterial",
    "ngMdIcons",
    "ngMessages",
    //"ngTouch",        // this breaks md-button ng-click operation
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
        $routeProvider.when("/index", {
            templateUrl: "partials/main.html",
            title: "Backpacking Planner"
        })

        // gear items
        .when("/gear/items", {
            templateUrl: "partials/gear/items.html",
            controller: "GearItemsCtrl",
            title: "Gear Items"
        })
        .when("/gear/items/add", {
            templateUrl: "partials/gear/add.html",
            title: "Add a Gear Item"
        })
        .when("/gear/items/:gearItemId", {
            templateUrl: "partials/gear/item.html",
            controller: "GearItemCtrl",
            title: "Gear Item"
        })

        // gear systems
        .when("/gear/systems", {
            templateUrl: "partials/gear/systems.html",
            title: "Gear Systems"
        })

        // gear collections
        .when("/gear/collections", {
            templateUrl: "partials/gear/collections.html",
            title: "Gear Collections"
        })

        // trip itineraries
        .when("/trip/itineraries", {
            templateUrl: "partials/trip/itineraries.html",
            title: "Trip Itineraries"
        })

        // trip plans
        .when("/trip/plans", {
            templateUrl: "partials/trip/plans.html",
            title: "Trip Plans"
        })

        // personal information and settings
        .when("/personal", {
            templateUrl: "partials/personal.html",
            title: "Personal Information"
        })
        .when("/settings", {
            templateUrl: "partials/settings.html",
            title: "Settings"
        })
        .when("/help", {
            templateUrl: "partials/help.html",
            title: "Help"
        })

        .otherwise({
            redirectTo: "/index"
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
