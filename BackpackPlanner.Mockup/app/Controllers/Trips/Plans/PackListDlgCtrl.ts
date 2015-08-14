///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IPackListDlgScope extends ng.IScope {
        tripPlan: Models.Trips.TripPlan;

        orderBy: string;

        close: () => void;

        getGearItems: () => Models.Gear.GearItem[];
        getMeals: () => Models.Meals.Meal[];
    }

    export class PackListDlgCtrl {
        constructor($scope: IPackListDlgScope, $mdDialog: ng.material.IDialogService, tripPlan: Models.Trips.TripPlan) {
            $scope.tripPlan = tripPlan;
            $scope.orderBy = "getName()";

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.getGearItems = () => {
                return AppState.getInstance().getGearState().getGearItems();
            }

            $scope.getMeals = () => {
                return AppState.getInstance().getMealState().getMeals();
            }
        }
    }
}
