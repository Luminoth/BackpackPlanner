var mockupControllers = angular.module("mockupControllers", []);

mockupControllers.controller("AppCtrl", ["$scope", "$location", "$mdSidenav", "UserInfo",
    function($scope, $location, $mdSidenav, UserInfo) {
        // TODO: this keeps giving an error, I dunno what to do to fix it
        var userInfo = UserInfo.get();
        userInfo.BirthDate = new Date(userInfo.BirthDate);
        $scope.userInfo = userInfo;

        $scope.isActive = function(viewLocation) {
            // set the nav item as active when we're looking at its location
            return $location.path() === viewLocation;
        }

        $scope.toggleSidenav = function() {
            $mdSidenav("left").toggle();
        }
    }
]);

mockupControllers.controller("GearItemsCtrl", ["$scope", "GearItem",
    function ($scope, GearItem) {
        $scope.gearItems = GearItem.query();
    }
]);

mockupControllers.controller("GearItemCtrl", ["$scope", "$routeParams", "GearItem",
    function ($scope, $routeParams, GearItem) {
        $scope.gearItem = GearItem.get({ gearItemId: $routeParams.gearItemId });
    }
]);
