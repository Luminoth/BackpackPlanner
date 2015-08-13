///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddMealDlgScope extends ng.IScope {
        tripPlan: Models.Trips.TripPlan;
        orderBy: string;

        getMeals: () => Models.Meals.Meal[];
        close: () => void;
        isSelected: (gearItem: Models.Meals.Meal) => void;
        toggle: (gearItem: Models.Meals.Meal) => void;
    }

    export class AddMealDlgCtrl {
        constructor($scope: IAddMealDlgScope, $mdDialog: ng.material.IDialogService, tripPlan: Models.Trips.TripPlan) {
            $scope.tripPlan = tripPlan;
            $scope.orderBy = "Name";

            $scope.getMeals = () => {
                return AppState.getInstance().getMealState().getMeals();
            }

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.isSelected = (meal) => {
                return $scope.tripPlan.containsMeal(meal);
            }

            $scope.toggle = (meal) => {
                if(!$scope.tripPlan.containsMeal(meal)) {
                    $scope.tripPlan.addMeal(meal);
                } else {
                    $scope.tripPlan.removeMeal(meal);
                } 
            };
        }
    }
}
