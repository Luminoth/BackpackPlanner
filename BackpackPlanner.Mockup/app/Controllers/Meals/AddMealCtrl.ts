///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../AppCtrl.ts" />

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
                    .content(`Added meal: ${$scope.meal.name()}`)
                    .action("Undo")
                    .position("bottom left");

                var undoAddToast = $mdToast.simple()
                    .content(`Removed meal: ${$scope.meal.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/meals");
                $mdToast.show(addToast).then((response: string) => {
                    if("ok" == response) {
                        AppState.getInstance().getMealState().deleteMeal($scope.meal);
                        $mdToast.show(undoAddToast);
                    }
                });
            }

            $scope.resetMeal = () => {
                $scope.meal = new Models.Meals.Meal();
            }
        }
    }

    AddMealCtrl.$inject = ["$scope", "$location", "$mdToast"];
}
