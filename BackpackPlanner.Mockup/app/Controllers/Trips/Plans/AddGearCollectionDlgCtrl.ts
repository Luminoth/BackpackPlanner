///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddGearCollectionDlgScope extends ng.IScope {
        tripPlan: Models.Trips.TripPlan;
        orderBy: string;

        getGearCollections: () => Models.Gear.GearCollection[];
        close: () => void;
        isSelected: (gearCollection: Models.Gear.GearCollection) => void;
        toggle: (gearCollection: Models.Gear.GearCollection) => void;
    }

    export class AddGearCollectionDlgCtrl {
        constructor($scope: IAddGearCollectionDlgScope, $mdDialog: ng.material.IDialogService, tripPlan: Models.Trips.TripPlan) {
            $scope.tripPlan = tripPlan;
            $scope.orderBy = "Name";

            $scope.getGearCollections = () => {
                return AppState.getInstance().getGearState().getGearCollections();
            }

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.isSelected = (gearCollection) => {
                return $scope.tripPlan.containsGearCollection(gearCollection);
            }

            $scope.toggle = (gearCollection) => {
                if(!$scope.tripPlan.containsGearCollection(gearCollection)) {
                    $scope.tripPlan.addGearCollection(gearCollection);
                } else {
                    $scope.tripPlan.removeGearCollection(gearCollection);
                } 
            };
        }
    }
}
