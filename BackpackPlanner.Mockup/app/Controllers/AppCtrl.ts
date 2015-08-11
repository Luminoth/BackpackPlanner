﻿///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../Models/AppSettings.ts" />
///<reference path="../Models/UserInformation.ts" />
///<reference path="../Models/Gear/GearItem.ts" />
///<reference path="../Models/Gear/GearSystem.ts" />
///<reference path="../Models/Gear/GearCollection.ts" />

///<reference path="../Services/AppSettingsService.ts"/>
///<reference path="../Services/UserInformationService.ts"/>
///<reference path="../Services/gear/GearCollectionService.ts"/>
///<reference path="../Services/gear/GearItemService.ts"/>
///<reference path="../Services/gear/GearSystemService.ts"/>

///<reference path="../UnitConversion.ts"/>

module BackpackPlanner.Mockup.Controllers {
    "use strict";

    export interface IAppScope extends ng.IScope {
        appStateLoading: boolean;

        getAppSettings: () => Models.AppSettings;
        getUserInfo: () => Models.UserInformation;

        getGearItems: () => Models.Gear.GearItem[];
        getGearItemById: (gearItemId: number) => Models.Gear.GearItem;

        getGearSystems: () => Models.Gear.GearSystem[];
        getGearSystemById: (gearSystemId: number) => Models.Gear.GearSystem;

        getGearCollections: () => Models.Gear.GearCollection[];
        getGearCollectionById: (gearCollectionId: number) => Models.Gear.GearCollection;

        getMeals: () => Models.Meal[];
        getMealById: (mealId: number) => Models.Meal;

        getTripItineraries: () => Models.Trips.TripItinerary[];
        getTripItineraryById: (tripItineraryId: number) => Models.Trips.TripItinerary;

        getTripPlans: () => Models.Trips.TripPlan[];
        getTripPlanById: (tripPlanId: number) => Models.Trips.TripPlan;

        getUnitsWeightString: () => string;
        getUnitsLengthString: () => string;
        getCurrencyString: () => string;

        isActive: (viewLocation: string) => boolean;
        toggleSidenav: () => void;
    }

    export class AppCtrl {
        constructor($scope: IAppScope, $location: ng.ILocationService, $mdSidenav: ng.material.ISidenavService,
            appSettingsService: Services.IAppSettingsService, userInformationService: Services.IUserInformationService,
            gearItemService: Services.Gear.IGearItemService, gearSystemService: Services.Gear.IGearSystemService, gearCollectionService: Services.Gear.IGearCollectionService
            ) {

            $scope.appStateLoading = true;
            AppState.getInstance().loadFromDevice()/*.$promise.then(
                $scope.appStateLoading = false;
            )*/;

/* TODO: move all of this into the AppState.loadFromDevice() call */

            // load the application settings
            appSettingsService.get().$promise.then(
                (appSettingsResource: Resources.IAppSettingsResource) => {
                    AppState.getInstance().loadAppSettings(appSettingsResource);
                }
            );

            $scope.getAppSettings = () => {
                return AppState.getInstance().getAppSettings();
            }

            // load the user's personal information
            userInformationService.get().$promise.then(
                (userInfoResource: Resources.IUserInformationResource) => {
                    AppState.getInstance().loadUserInformation(userInfoResource);
                }
            );

            $scope.getUserInfo = () => {
                return AppState.getInstance().getUserInformation();
            }

            // load the gear items
            gearItemService.query().$promise.then(
                (gearItemsResource: Resources.Gear.IGearItemResource[]) => {
                    AppState.getInstance().getGearState().loadGearItems(gearItemsResource);
                }
            );

            $scope.getGearItems = () => {
                return AppState.getInstance().getGearState().getGearItems();
            }

            $scope.getGearItemById = (gearItemId: number) => {
                return AppState.getInstance().getGearState().getGearItemById(gearItemId);
            }

            // load the gear systems
            gearSystemService.query().$promise.then(
                (gearSystemsResource: Resources.Gear.IGearSystemResource[]) => {
                    AppState.getInstance().getGearState().loadGearSystems(gearSystemsResource);
                }
            );

            $scope.getGearSystems = () => {
                return AppState.getInstance().getGearState().getGearSystems();
            }

            $scope.getGearSystemById = (gearSystemId: number) => {
                return AppState.getInstance().getGearState().getGearSystemById(gearSystemId);
            }

            // load the gear collections
            gearCollectionService.query().$promise.then(
                (gearCollectionsResource: Resources.Gear.IGearCollectionResource[]) => {
                    AppState.getInstance().getGearState().loadGearCollections(gearCollectionsResource);
                }
            );

            $scope.getGearCollections = () => {
                return AppState.getInstance().getGearState().getGearCollections();
            }

            $scope.getGearCollectionById = (gearCollectionId: number) => {
                return AppState.getInstance().getGearState().getGearCollectionById(gearCollectionId);
            }

            // load the meals
            $scope.getMeals = () => {
                return <Array<Models.Meal>>[];
            }
            $scope.getMealById = (mealId: number) => {
                return <Models.Meal> null;
            }

            // load the trip itineraries
            $scope.getTripItineraries = () => {
                return <Array<Models.Trips.TripItinerary>>[];
            }
            $scope.getTripItineraryById = (tripItineraryId: number) => {
                return <Models.Trips.TripItinerary> null;
            }

            // load the trip plans
            $scope.getTripPlans = () => {
                return <Array<Models.Trips.TripPlan>>[];
            }
            $scope.getTripPlanById = (tripPlanId: number) => {
                return <Models.Trips.TripPlan> null;
            }

/* TODO: end move all of this into the AppState.loadFromDevice() call */

            $scope.getUnitsWeightString = () => {
                return getUnitsWeightString(AppState.getInstance().getAppSettings().Units);
            }

            $scope.getUnitsLengthString = () => {
                return getUnitsLengthString(AppState.getInstance().getAppSettings().Units);
            }

            $scope.getCurrencyString = () => {
                return getCurrencyString(AppState.getInstance().getAppSettings().Currency);
            }

            $scope.isActive = (viewLocation: string) => {
                // set the nav item as active when we're looking at its location
                return $location.path() === viewLocation;
            }

            $scope.toggleSidenav = () => {
                $mdSidenav("left").toggle();
            }
        }
    }

    AppCtrl.$inject = ["$scope", "$location", "$mdSidenav", "AppSettingsService", "UserInformationService",
        "GearItemService", "GearSystemService", "GearCollectionService"];
}
