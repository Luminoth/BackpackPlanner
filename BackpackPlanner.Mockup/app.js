var mockupApp = angular.module("mockupApp", [
    "ngRoute",
    "ngMaterial",
    "ngMdIcons",
    "ngTouch",
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
        .when("/gear/items/item", {
            templateUrl: "partials/gear/items-item.html",
            title: "Gear Item"
        })
        .when("/gear/items/add", {
            templateUrl: "partials/gear/items-add.html",
            title: "Add Gear Item"
        })
        .when("/gear/items/delete", {
            templateUrl: "partials/gear/items-delete.html",
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

mockupApp.controller("GearItemCtrl", ["$scope",
    function($scope) {
        $scope.onClick = function(event) {
            window.location.replace("/#/gear/items/item");
        }

        $scope.onSwipeRight = function(event) {
            window.location.replace("/#/gear/items/delete");
        };

        $scope.onSwipeLeft = function(event) {
            window.location.replace("/#/gear/items");
        };
    }
]);
