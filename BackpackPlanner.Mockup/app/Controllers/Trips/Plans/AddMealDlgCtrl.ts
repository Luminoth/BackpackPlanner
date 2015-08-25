///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddMealDlgScope extends ng.IScope {
        tripPlan: Models.Trips.TripPlan;

        filterName: string;
        orderBy: string;

        close: () => void;

        filterMeal: (meal: Models.Meals.Meal) => boolean;
        getMeals: () => Models.Meals.Meal[];
        isMealSelected: (gearItem: Models.Meals.Meal) => void;
        toggleMealSelected: (gearItem: Models.Meals.Meal) => void;
    }

    export class AddMealDlgCtrl {
        constructor($scope: IAddMealDlgScope, $mdDialog: ng.material.IDialogService, tripPlan: Models.Trips.TripPlan) {
            $scope.tripPlan = tripPlan;
            $scope.orderBy = "name()";

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.filterMeal = (meal) => {
                if($scope.filterName) {
                    return meal.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                }
                return true;
            }

            $scope.getMeals = () => {
                return AppState.getInstance().getMealState().getMeals();
            }

            $scope.isMealSelected = (meal) => {
                return $scope.tripPlan.containsMealById(meal.Id);
            }

            $scope.toggleMealSelected = (meal) => {
                if(!$scope.tripPlan.containsMealById(meal.Id)) {
                    $scope.tripPlan.addMeal(meal);
                } else {
                    $scope.tripPlan.removeMealById(meal.Id);
                } 
            };
        }
    }
}
