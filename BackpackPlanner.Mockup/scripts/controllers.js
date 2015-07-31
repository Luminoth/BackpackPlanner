var mockupControllers = angular.module("mockupControllers", []);

mockupControllers.controller("AppCtrl", ["$scope", "$location", "$mdSidenav",
    function($scope, $location, $mdSidenav) {
        $scope.isActive = function(viewLocation) {
            // set the nav item as active when we're looking at its location
            return $location.path() === viewLocation;
        }

        $scope.toggleSidenav = function() {
            $mdSidenav("left").toggle();
        }
    }
]);
