var mockupApp = angular.module("mockupApp", [
    "ngRoute",
    "ngMaterial",
    "ngMdIcons",
    "ui.bootstrap"
]);

mockupApp.config(["$routeProvider",
    function($routeProvider) {
        $routeProvider.when("/index", {
            templateUrl: "partials/main.html",
            title: "Backpacking Planner"
        })
        .when("/gear/manage", {
            templateUrl: "partials/gear/manage.html",
            title: "Manage Gear"
        })
        .when("/gear/items", {
            templateUrl: "partials/gear/items.html",
            title: "Gear Items"
        })
        .otherwise({
            redirectTo: "/index"
        });
    }
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
