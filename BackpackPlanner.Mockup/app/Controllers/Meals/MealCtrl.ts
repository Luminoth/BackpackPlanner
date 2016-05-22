///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../scripts/typings/angularjs/angular-route.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Meals {
    "use strict";

    export interface IMealScope extends IAppScope {
        meal: Models.Meals.Meal;

        saveMeal: () => void;
        resetMeal: () => void;
        deleteMeal: (event: MouseEvent) => void;
    }

    export interface IMealRouteParams extends ng.route.IRouteParamsService {
        mealId: number;
    }

    export class MealCtrl {
        constructor($scope: IMealScope, $routeParams: IMealRouteParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
        
            const meal = AppState.getInstance().getMealState().getMealById($routeParams.mealId);
            if(null == meal) {
                alert("The meal does not exist!");
                $location.path("/meals");
                return;
            }
            $scope.meal = angular.copy(meal);

            $scope.saveMeal = () => {
                var meal = AppState.getInstance().getMealState().getMealById($scope.meal.Id);
                if(null == meal) {
                    alert("The meal no longer exists!");
                    $location.path("/meals");
                    return;
                }
                meal.update($scope.meal);

                var updateToast = $mdToast.simple()
                    .textContent(`Updated meal: ${$scope.meal.name()}`)
                    .action("OK")
                    .position("bottom left");

                $location.path("/meals");
                $mdToast.show(updateToast);
            }

            $scope.resetMeal = () => {
                var meal = AppState.getInstance().getMealState().getMealById($scope.meal.Id);
                if(null == meal) {
                    alert("The meal no longer exists!");
                    $location.path("/meals");
                    return;
                }
                $scope.meal = angular.copy(meal);
            }

            $scope.deleteMeal = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Meal")
                    .textContent("Are you sure you wish to delete this meal?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Meal deleted!")
                    .textContent("The meal has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .textContent(`Deleted meal: ${$scope.meal.name()}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .textContent(`Restored meal: ${$scope.meal.name()}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        const action = new Actions.Meals.DeleteMealAction();
                        action.Meal = $scope.meal;
                        AppState.getInstance().executeAction(action);

                        $location.path("/meals");
                        $mdToast.show(deleteToast).then((response: string) => {
                            if("ok" == response) {
                                AppState.getInstance().undoAction();
                                $mdToast.show(undoDeleteToast);
                                $location.path(`/meals/${$scope.meal.Id}`);
                            }
                        });
                    });
                });
            }
        }
    }

    MealCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}
