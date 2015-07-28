var mockupApp = angular.module("mockupApp", [
    "ngRoute",
    "ngMaterial",
    "ngMdIcons",
    "ngTouch",
    "ui.bootstrap",

    "mockupControllers"
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

        // gear
        .when("/gear/items", {
            templateUrl: "partials/gear/items.html",
            title: "Gear Items"
        })
        .when("/gear/systems", {
            templateUrl: "partials/gear/systems.html",
            title: "Gear Systems"
        })
        .when("/gear/collections", {
            templateUrl: "partials/gear/collections.html",
            title: "Gear Collections"
        })

        // trips
        .when("/trip/itineraries", {
            templateUrl: "partials/trip/itineraries.html",
            title: "Trip Itineraries"
        })
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

        .otherwise({
            redirectTo: "/index"
        });
    }
]);

mockupApp.config(["$mdThemingProvider",
    function($mdThemingProvider) {
        var primaryPalette = $mdThemingProvider.extendPalette("green", {
            "500": "668000",
            "A100": "501616"
        });

        var backgroundPalette = $mdThemingProvider.extendPalette("brown", {
            "A100": "decd87"
        });

        var accentPalette = $mdThemingProvider.extendPalette("grey", {
        });

        $mdThemingProvider.definePalette("mockupPrimaryPalette", primaryPalette);
        $mdThemingProvider.definePalette("mockupBackgroundPalette", backgroundPalette);
        $mdThemingProvider.definePalette("mockupAccentPalette", accentPalette);

        $mdThemingProvider.theme("default")
            .primaryPalette("mockupPrimaryPalette", {
                "hue-1": "100"
            }).dark()
            .backgroundPalette("mockupBackgroundPalette", {
                "hue-1": "A100"
            })
            .accentPalette("mockupAccentPalette");
    }
]);
