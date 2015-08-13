///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../../scripts/typings/angularjs/angular-route.d.ts" />

///<reference path="../AppCtrl.ts" />

module BackpackPlanner.Mockup.Controllers.Meals {
    "use strict";

    export interface IMealScope extends IAppScope {
        meal: Models.Meals.Meal;

        showDeleteConfirm: (event: MouseEvent) => void;
    }

    export interface IMealRouteParams extends ng.route.IRouteParamsService {
        mealId: number;
    }

    export class MealCtrl {
        constructor($scope: IMealScope, $routeParams: IMealRouteParams, $location: ng.ILocationService,
            $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService) {
        
            $scope.meal = AppState.getInstance().getMealState().getMealById($routeParams.mealId);
            if(null == $scope.meal) {
                alert("The meal does not exist!");
                $location.path("/meals");
                return;
            }

            $scope.showDeleteConfirm = (event) => {
                var confirm = $mdDialog.confirm()
                    .parent(angular.element(document.body))
                    .title("Delete Meal")
                    .content("Are you sure you wish to delete this meal?")
                    .ok("Yes")
                    .cancel("No")
                    .targetEvent(event);

                var receipt = $mdDialog.alert()
                    .parent(angular.element(document.body))
                    .title("Meal deleted!")
                    .content("The meal has been deleted.")
                    .ok("OK")
                    .targetEvent(event);

                var deleteToast = $mdToast.simple()
                    .content(`Deleted meal: ${$scope.meal.Name}`)
                    .action("Undo")
                    .position("bottom left");

                var undoDeleteToast = $mdToast.simple()
                    .content(`Restored meal: ${$scope.meal.Name}`)
                    .action("OK")
                    .position("bottom left");

                $mdDialog.show(confirm).then(() => {
                    $mdDialog.show(receipt).then(() => {
                        if(!AppState.getInstance().getMealState().deleteMeal($scope.meal)) {
                            alert("Couldn't find the meal to delete!");
                            return;
                        }

                        $location.path("/meals");
                        $mdToast.show(deleteToast).then(() => {
                            // TODO: this does *not* restore the meal to its containers
                            // and it should probably do so... but how?
                            AppState.getInstance().getMealState().addMeal($scope.meal);
                            $mdToast.show(undoDeleteToast);
                            $location.path(`/meals/${$scope.meal.Id}`);
                        });
                    });
                });
            }
        }
    }

    MealCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
}