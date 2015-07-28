var mockupControllers = angular.module("mockupControllers", []);

mockupControllers.controller("AppCtrl", ["$scope", "$mdSidenav",
    function($scope, $mdSidenav) {
        $scope.toggleSidenav = function() { $mdSidenav("left").toggle(); }
    }
]);
