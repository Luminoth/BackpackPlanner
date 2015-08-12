///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/angular-material.d.ts" />

///<reference path="../Models/Gear/GearItem.ts" />
///<reference path="../Models/Gear/GearSystem.ts" />
///<reference path="../Models/Gear/GearCollection.ts" />
///<reference path="../Models/Meals/Meal.ts" />
///<reference path="../Models/Trips/TripItinerary.ts" />
///<reference path="../Models/Trips/TripPlan.ts" />
///<reference path="../Models/Personal/UserInformation.ts" />
///<reference path="../Models/AppSettings.ts" />

///<reference path="../Services/Gear/GearCollectionService.ts"/>
///<reference path="../Services/Gear/GearItemService.ts"/>
///<reference path="../Services/Gear/GearSystemService.ts"/>
///<reference path="../Services/Personal/UserInformationService.ts"/>
///<reference path="../Services/AppSettingsService.ts"/>

///<reference path="../UnitConversion.ts"/>

module BackpackPlanner.Mockup.Controllers {
    "use strict";

    export interface IAppScope extends ng.IScope {
        appStateLoading: boolean;

        getUserInfo: () => Models.Personal.UserInformation;

        getGearItems: () => Models.Gear.GearItem[];
        getGearItemById: (gearItemId: number) => Models.Gear.GearItem;
        deleteAllGearItems: () => void;

        getGearSystems: () => Models.Gear.GearSystem[];
        getGearSystemById: (gearSystemId: number) => Models.Gear.GearSystem;
        deleteAllGearSystems: () => void;

        getGearCollections: () => Models.Gear.GearCollection[];
        getGearCollectionById: (gearCollectionId: number) => Models.Gear.GearCollection;
        deleteAllGearCollections: () => void;

        getMeals: () => Models.Meals.Meal[];
        getMealById: (mealId: number) => Models.Meals.Meal;
        deleteAllMeals: () => void;

        getTripItineraries: () => Models.Trips.TripItinerary[];
        getTripItineraryById: (tripItineraryId: number) => Models.Trips.TripItinerary;
        deleteAllTripItineraries: () => void;

        getTripPlans: () => Models.Trips.TripPlan[];
        getTripPlanById: (tripPlanId: number) => Models.Trips.TripPlan;
        deleteAllTripPlans: () => void;

        getUnitsWeightString: () => string;
        getUnitsLengthString: () => string;
        getCurrencyString: () => string;

        isActive: (viewLocation: string) => boolean;
        toggleSidenav: () => void;
    }

    export class AppCtrl {
        constructor($scope: IAppScope, $location: ng.ILocationService, $mdSidenav: ng.material.ISidenavService,
            appSettingsService: Services.IAppSettingsService, userInformationService: Services.Personal.IUserInformationService,
            gearItemService: Services.Gear.IGearItemService, gearSystemService: Services.Gear.IGearSystemService, gearCollectionService: Services.Gear.IGearCollectionService
            /*mealsService: Services.Meals.IMealService,
            tripItinerariesService: Services.Trips.ITripItineraryService, tripPlansService: Services.Trips.ITripPlansService*/) {

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

            // load the user's personal information
            userInformationService.get().$promise.then(
                (userInfoResource: Resources.Personal.IUserInformationResource) => {
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

            $scope.deleteAllGearItems = () => {
                // TODO: md alert verify this!
                AppState.getInstance().getGearState().deleteAllGearItems();
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

            $scope.deleteAllGearSystems = () => {
                // TODO: md alert verify this!
                AppState.getInstance().getGearState().deleteAllGearSystems();
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

            $scope.deleteAllGearCollections = () => {
                // TODO: md alert verify this!
                AppState.getInstance().getGearState().deleteAllGearCollections();
            }

            // load the meals
            $scope.getMeals = () => {
                return <Array<Models.Meals.Meal>>[];
            }

            $scope.getMealById = (mealId: number) => {
                return <Models.Meals.Meal> null;
            }

            $scope.deleteAllMeals = () => {
                // TODO: md alert verify this!
                AppState.getInstance().getMealState().deleteAllMeals();
            }

            // load the trip itineraries
            $scope.getTripItineraries = () => {
                return <Array<Models.Trips.TripItinerary>>[];
            }

            $scope.getTripItineraryById = (tripItineraryId: number) => {
                return <Models.Trips.TripItinerary> null;
            }

            $scope.deleteAllTripItineraries = () => {
                // TODO: md alert verify this!
                AppState.getInstance().getTripState().deleteAllTripItineraries();
            }

            // load the trip plans
            $scope.getTripPlans = () => {
                return <Array<Models.Trips.TripPlan>>[];
            }
            $scope.getTripPlanById = (tripPlanId: number) => {
                return <Models.Trips.TripPlan> null;
            }

            $scope.deleteAllTripPlans = () => {
                // TODO: md alert verify this!
                AppState.getInstance().getTripState().deleteAllTripPlans();
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

    AppCtrl.$inject = ["$scope", "$location", "$mdSidenav",
        "AppSettingsService", "UserInformationService",
        "GearItemService", "GearSystemService", "GearCollectionService"/*
        MealService,
        TripItineraryService, TripPlanService*/];
}
