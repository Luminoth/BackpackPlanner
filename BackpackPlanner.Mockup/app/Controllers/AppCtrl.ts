///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../Models/AppSettings.ts" />
///<reference path="../Models/UserInformation.ts" />
///<reference path="../Models/Gear/GearItem.ts" />
///<reference path="../Models/Gear/GearSystem.ts" />
///<reference path="../Models/Gear/GearCollection.ts" />

module BackpackPlanner.Mockup.Controllers {
    "use strict";

    export interface IAppScope extends ng.IScope {
        appSettingsLoading: boolean;
        appSettings: Models.IAppSettings;

        userInfoLoading: boolean;
        userInfo: Models.IUserInformation;

        gearItemsLoading: boolean;
        gearItems: Models.Gear.IGearItem[];

        gearSystemsLoading: boolean;
        gearSystems: Models.Gear.IGearSystem[];

        gearCollectionsLoading: boolean;
        gearCollections: Models.Gear.IGearCollection[];

        mealsLoading: boolean;
        meals: any[];

        tripItinerariesLoading: boolean;
        tripItineraries: any[];

        tripPlansLoading: boolean;
        tripPlans: any[];

        isActive: (viewLocation: string) => boolean;
        toggleSidenav: () => void;
    }

    export class AppCtrl {
        constructor($scope: IAppScope, $location: ng.ILocationService, $mdSidenav: ng.material.ISidenavService,
            appSettingsResource: Models.IAppSettingsResource, userInformationResource: Models.IUserInformationResource,
            gearItemResource: Models.Gear.IGearItemResource, gearSystemResource: Models.Gear.IGearSystemResource, gearCollectionResource: Models.Gear.IGearCollectionResource) {

            // load the application settings
            $scope.appSettingsLoading = true;
            appSettingsResource.get().$promise.then(
                (appSettings: Models.IAppSettings) => {
                    $scope.appSettings = appSettings;
                    $scope.appSettingsLoading = false;
                }
            );

            // load the user's personal information
            $scope.userInfoLoading = true;
            userInformationResource.get().$promise.then(
                (userInformation: Models.IUserInformation) => {
                    $scope.userInfo = userInformation;
                    $scope.userInfoLoading = false;
                }
            );

            // load the gear items
            $scope.gearItemsLoading = true;
            gearItemResource.query().$promise.then(
                (gearItems: Models.Gear.IGearItem[]) => {
                    $scope.gearItems = gearItems;
                    $scope.gearItemsLoading = false;
                }
            );

            // load the gear systems
            $scope.gearSystemsLoading = true;
            gearSystemResource.query().$promise.then(
                (gearSystems: Models.Gear.IGearSystem[]) => {
                    $scope.gearSystems = gearSystems;
                    $scope.gearSystemsLoading = false;
                }
            );

            // load the gear collections
            $scope.gearCollectionsLoading = true;
            gearCollectionResource.query().$promise.then(
                (gearCollections: Models.Gear.IGearCollection[]) => {
                    $scope.gearCollections = gearCollections;
                    $scope.gearCollectionsLoading = false;
                }
            );

            // load the meals
            $scope.mealsLoading = true;
            $scope.meals = [];
                        $scope.mealsLoading = false;

            // load the trip itineraries
            $scope.tripItinerariesLoading = true;
            $scope.tripItineraries = [];
                        $scope.tripItinerariesLoading = false;

            // load the trip plans
            $scope.tripPlansLoading = true;
            $scope.tripPlans = [];
                        $scope.tripPlansLoading = false;

            $scope.isActive = (viewLocation: string) => {
                // set the nav item as active when we're looking at its location
                return $location.path() === viewLocation;
            }

            $scope.toggleSidenav = () => {
                $mdSidenav("left").toggle();
            }
        }

        public getGearItemById(): Models.Gear.IGearItem {
            return null;
        }
    }

    AppCtrl.$inject = ["$scope", "$location", "$mdSidenav", "AppSettingsResource", "UserInformationResource",
        "GearItemResource", "GearSystemResource", "GearCollectionResource"];
}
