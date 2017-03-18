/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />

/// <reference path="../../../AppState.ts" />

module BackpackPlanner.Mockup.Controllers.Trips.Plans {
    "use strict";

    export interface IAddGearCollectionDlgScope extends ng.IScope {
        tripPlan: Models.Trips.TripPlan;

        filterName: string;
        orderBy: string;

        close: () => void;

        filterGearCollection: (gearCollection: Models.Gear.GearCollection) => boolean;
        getGearCollections: () => Models.Gear.GearCollection[];
        isGearCollectionSelected: (gearCollection: Models.Gear.GearCollection) => void;
        toggleGearCollectionSelected: (gearCollection: Models.Gear.GearCollection) => void;
    }

    export class AddGearCollectionDlgCtrl {
        constructor($scope: IAddGearCollectionDlgScope, $mdDialog: ng.material.IDialogService, tripPlan: Models.Trips.TripPlan) {
            $scope.tripPlan = tripPlan;
            $scope.filterName = "";
            $scope.orderBy = "name()";

            $scope.close = () => {
                $mdDialog.hide();
            };

            $scope.filterGearCollection = (gearCollection) => {
                if($scope.filterName) {
                    return gearCollection.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                }
                return true;
            }

            $scope.getGearCollections = () => {
                return AppState.getInstance().getGearState().getGearCollections();
            }

            $scope.isGearCollectionSelected = (gearCollection) => {
                return $scope.tripPlan.containsGearCollectionById(gearCollection.Id);
            }

            $scope.toggleGearCollectionSelected = (gearCollection) => {
                if(!$scope.tripPlan.containsGearCollectionById(gearCollection.Id)) {
                    try {
                        $scope.tripPlan.addGearCollection(gearCollection);
                    } catch(error) {
                        alert(error);
                    }
                } else {
                    if(!$scope.tripPlan.removeGearCollectionById(gearCollection.Id)) {
                        alert("Cannot remove the collection, it may no longer exist.");
                    }
                } 
            };
        }
    }
}
