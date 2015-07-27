var mockupApp = angular.module("mockupApp", [
    "ngRoute",
    "ngMaterial",
    "ui.bootstrap"
]);

mockupApp.config(["$routeProvider",
    function($routeProvider) {
        $routeProvider.when("/index", {
            templateUrl: "partials/main.html"
        })
        .otherwise({
            redirectTo: "/index"
        });
    }
]);
