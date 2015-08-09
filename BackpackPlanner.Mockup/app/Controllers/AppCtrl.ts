///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
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

module BackpackPlanner.Mockup.Controllers {
    "use strict";

    export interface IAppScope extends ng.IScope {
        appSettingsLoading: boolean;
        getAppSettings: () => Models.AppSettings;

        userInfoLoading: boolean;
        getUserInfo: () => Models.UserInformation;

        gearItemsLoading: boolean;
        getGearItems: () => Models.Gear.GearItem[];
        getGearItemById: (gearItemId: number) => Models.Gear.GearItem;

        gearSystemsLoading: boolean;
        getGearSystems: () => Models.Gear.GearSystem[];
        getGearSystemById: (gearSystemId: number) => Models.Gear.GearSystem;

        gearCollectionsLoading: boolean;
        getGearCollections: () => Models.Gear.GearCollection[];
        getGearCollectionById: (gearCollectionId: number) => Models.Gear.GearCollection;

        mealsLoading: boolean;
        getMeals: () => any[];
        getMealById: (mealId: number) => any;

        tripItinerariesLoading: boolean;
        getTripItineraries: () => any[];
        getTripItineraryById: (tripItineraryId: number) => any;

        tripPlansLoading: boolean;
        getTripPlans: () => any[];
        getTripPlanById: (tripPlanId: number) => any;

        isActive: (viewLocation: string) => boolean;
        toggleSidenav: () => void;
    }

    export class AppCtrl {
        constructor($scope: IAppScope, $location: ng.ILocationService, $mdSidenav: ng.material.ISidenavService,
            appSettingsService: Services.IAppSettingsService, userInformationService: Services.IUserInformationService,
            gearItemService: Services.Gear.IGearItemService, gearSystemService: Services.Gear.IGearSystemService, gearCollectionService: Services.Gear.IGearCollectionService) {

            // load the application settings
            $scope.appSettingsLoading = true;
            appSettingsService.get().$promise.then(
                (appSettingsResource: Resources.IAppSettingsResource) => {
                    AppManager.getInstance().setAppSettings(appSettingsResource);
                    $scope.appSettingsLoading = false;
                }
            );

            $scope.getAppSettings = () => {
                return AppManager.getInstance().getAppSettings();
            }

            // load the user's personal information
            $scope.userInfoLoading = true;
            userInformationService.get().$promise.then(
                (userInfoResource: Resources.IUserInformationResource) => {
                    AppManager.getInstance().setUserInformation(userInfoResource);
                    $scope.userInfoLoading = false;
                }
            );

            $scope.getUserInfo = () => {
                return AppManager.getInstance().getUserInformation();
            }

            // load the gear items
            $scope.gearItemsLoading = true;
            gearItemService.query().$promise.then(
                (gearItemsResource: Resources.Gear.IGearItemResource[]) => {
                    AppManager.getInstance().setGearItems(gearItemsResource);
                    $scope.gearItemsLoading = false;
                }
            );

            $scope.getGearItems = () => {
                return AppManager.getInstance().getGearItems();
            }

            $scope.getGearItemById = (gearItemId: number) => {
                return AppManager.getInstance().getGearItemById(gearItemId);
            }

            // load the gear systems
            $scope.gearSystemsLoading = true;
            gearSystemService.query().$promise.then(
                (gearSystemsResource: Resources.Gear.IGearSystemResource[]) => {
                    AppManager.getInstance().setGearSystems(gearSystemsResource);
                    $scope.gearSystemsLoading = false;
                }
            );

            $scope.getGearSystems = () => {
                return AppManager.getInstance().getGearSystems();
            }

            $scope.getGearSystemById = (gearSystemId: number) => {
                return AppManager.getInstance().getGearSystemById(gearSystemId);
            }

            // load the gear collections
            $scope.gearCollectionsLoading = true;
            gearCollectionService.query().$promise.then(
                (gearCollectionsResource: Resources.Gear.IGearCollectionResource[]) => {
                    AppManager.getInstance().setGearCollections(gearCollectionsResource);
                    $scope.gearCollectionsLoading = false;
                }
            );

            $scope.getGearCollections = () => {
                return AppManager.getInstance().getGearCollections();
            }

            $scope.getGearCollectionById = (gearCollectionId: number) => {
                return AppManager.getInstance().getGearCollectionById(gearCollectionId);
            }

            // load the meals
            $scope.mealsLoading = true;
            $scope.getMeals = () => {
                return <Array<any>>[];
            }
            $scope.getMealById = (mealId: number) => {
                return <any> null;
            }
            $scope.mealsLoading = false;

            // load the trip itineraries
            $scope.tripItinerariesLoading = true;
            $scope.getTripItineraries = () => {
                return <Array<any>>[];
            }
            $scope.getTripItineraryById = (tripItineraryId: number) => {
                return <any> null;
            }
            $scope.tripItinerariesLoading = false;

            // load the trip plans
            $scope.tripPlansLoading = true;
            $scope.getTripPlans = () => {
                return <Array<any>>[];
            }
            $scope.getTripPlanById = (tripPlanId: number) => {
                return <any> null;
            }
            $scope.tripPlansLoading = false;

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
