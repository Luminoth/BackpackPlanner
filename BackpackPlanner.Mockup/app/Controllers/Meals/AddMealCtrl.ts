/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />

/// <reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Meals {
    "use strict";

    export interface IAddMealScope extends IAppScope {
        meal: Models.Meals.Meal;

        addMeal: () => void;
        resetMeal: () => void;
    }

    export class AddMealCtrl {
        constructor($scope: IAddMealScope, $location: ng.ILocationService, $mdToast: ng.material.IToastService) {
            $scope.meal = new Models.Meals.Meal();

            $scope.addMeal = () => {
                AppState.getInstance().getMealState().addMeal($scope.meal);

                var addToast = $mdToast.simple()
                    .textContent(`Added meal: ${$scope.meal.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/meals");
                $mdToast.show(addToast);
            }

            $scope.resetMeal = () => {
                $scope.meal = new Models.Meals.Meal();
            }
        }
    }

    AddMealCtrl.$inject = ["$scope", "$location", "$mdToast"];
}
