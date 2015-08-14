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
///<reference path="../Services/Meals/MealService.ts"/>
///<reference path="../Services/Trips/TripItineraryService.ts"/>
///<reference path="../Services/Trips/TripPlanService.ts"/>
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

        getGearSystems: () => Models.Gear.GearSystem[];
        getGearSystemById: (gearSystemId: number) => Models.Gear.GearSystem;

        getGearCollections: () => Models.Gear.GearCollection[];
        getGearCollectionById: (gearCollectionId: number) => Models.Gear.GearCollection;

        getMeals: () => Models.Meals.Meal[];
        getMealById: (mealId: number) => Models.Meals.Meal;

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
        constructor($scope: IAppScope, $q: ng.IQService, $location: ng.ILocationService, $mdSidenav: ng.material.ISidenavService, $mdDialog: ng.material.IDialogService, $mdToast: ng.material.IToastService,
            appSettingsService: Services.IAppSettingsService, userInformationService: Services.Personal.IUserInformationService,
            gearItemService: Services.Gear.IGearItemService, gearSystemService: Services.Gear.IGearSystemService, gearCollectionService: Services.Gear.IGearCollectionService,
            mealService: Services.Meals.IMealService, tripItineraryService: Services.Trips.ITripItineraryService, tripPlanService: Services.Trips.ITripPlanService) {

            $scope.appStateLoading = true;
            AppState.getInstance().loadFromDevice($q, appSettingsService, userInformationService,
                gearItemService, gearSystemService, gearCollectionService,
                mealService, tripItineraryService, tripPlanService).then(
                () => {
                    $scope.appStateLoading = false;
                }
            );

            // user information
            $scope.getUserInfo = () => {
                return AppState.getInstance().getUserInformation();
            }

            // gear items
            $scope.getGearItems = () => {
                return AppState.getInstance().getGearState().getGearItems();
            }

            $scope.getGearItemById = (gearItemId: number) => {
                return AppState.getInstance().getGearState().getGearItemById(gearItemId);
            }

            // gear systems
            $scope.getGearSystems = () => {
                return AppState.getInstance().getGearState().getGearSystems();
            }

            $scope.getGearSystemById = (gearSystemId: number) => {
                return AppState.getInstance().getGearState().getGearSystemById(gearSystemId);
            }

            // gear collections
            $scope.getGearCollections = () => {
                return AppState.getInstance().getGearState().getGearCollections();
            }

            $scope.getGearCollectionById = (gearCollectionId: number) => {
                return AppState.getInstance().getGearState().getGearCollectionById(gearCollectionId);
            }

            // meals
            $scope.getMeals = () => {
                return AppState.getInstance().getMealState().getMeals();
            }

            $scope.getMealById = (mealId: number) => {
                return AppState.getInstance().getMealState().getMealById(mealId);
            }

            // trip itineraries
            $scope.getTripItineraries = () => {
                return AppState.getInstance().getTripState().getTripItineraries();
            }

            $scope.getTripItineraryById = (tripItineraryId: number) => {
                return AppState.getInstance().getTripState().getTripItineraryById(tripItineraryId);
            }

            // trip plans
            $scope.getTripPlans = () => {
                return AppState.getInstance().getTripState().getTripPlans();
            }

            $scope.getTripPlanById = (tripPlanId: number) => {
                return AppState.getInstance().getTripState().getTripPlanById(tripPlanId);
            }

            // unit utilities
            $scope.getUnitsWeightString = () => {
                return getUnitsWeightString(AppState.getInstance().getAppSettings().Units);
            }

            $scope.getUnitsLengthString = () => {
                return getUnitsLengthString(AppState.getInstance().getAppSettings().Units);
            }

            $scope.getCurrencyString = () => {
                return getCurrencyString(AppState.getInstance().getAppSettings().Currency);
            }

            // view utilities
            $scope.isActive = (viewLocation: string) => {
                // set the nav item as active when we're looking at its location
                return $location.path() === viewLocation;
            }

            $scope.toggleSidenav = () => {
                $mdSidenav("left").toggle();
            }
        }
    }

    AppCtrl.$inject = ["$scope", "$q", "$location", "$mdSidenav", "$mdDialog", "$mdToast",
        "AppSettingsService", "UserInformationService",
        "GearItemService", "GearSystemService", "GearCollectionService",
        "MealService", "TripItineraryService", "TripPlanService"];
}
