/// <reference path="../scripts/typings/angularjs/angular.d.ts" />

/// <reference path="Controllers/Gear/Collections/AddGearCollectionCtrl.ts" />
/// <reference path="Controllers/Gear/Collections/GearCollectionCtrl.ts" />
/// <reference path="Controllers/Gear/Collections/GearCollectionsCtrl.ts" />

/// <reference path="Controllers/Gear/Items/AddGearItemCtrl.ts" />
/// <reference path="Controllers/Gear/Items/GearItemCtrl.ts" />
/// <reference path="Controllers/Gear/Items/GearItemsCtrl.ts" />

/// <reference path="Controllers/Gear/Systems/AddGearSystemCtrl.ts" />
/// <reference path="Controllers/Gear/Systems/GearSystemCtrl.ts" />
/// <reference path="Controllers/Gear/Systems/GearSystemsCtrl.ts" />

/// <reference path="Controllers/Meals/MealsCtrl.ts" />
/// <reference path="Controllers/Meals/MealCtrl.ts" />
/// <reference path="Controllers/Meals/AddMealCtrl.ts" />

/// <reference path="Controllers/Trips/Itineraries/TripItinerariesCtrl.ts" />
/// <reference path="Controllers/Trips/Itineraries/TripItineraryCtrl.ts" />
/// <reference path="Controllers/Trips/Itineraries/AddTripItineraryCtrl.ts" />

/// <reference path="Controllers/Trips/Plans/TripPlansCtrl.ts" />
/// <reference path="Controllers/Trips/Plans/TripPlanCtrl.ts" />
/// <reference path="Controllers/Trips/Plans/AddTripPlanCtrl.ts" />

/// <reference path="Controllers/Personal/UserInformationCtrl.ts" />

/// <reference path="Controllers/AppCtrl.ts" />
/// <reference path="Controllers/AppSettingsCtrl.ts" />

/// <reference path="RootScopeConfig.ts" />
/// <reference path="RouteConfig.ts" />
/// <reference path="ThemeConfig.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    var mockupApp = angular.module("mockupApp", [
        "ngAnimate",
        "ngAria",
        "ngMessages",
        "ngResource",
        "ngRoute",
        "ngSanitize",
        "ngTouch",
        "ngMaterial",
        "ngMdIcons",
        "ui.bootstrap"
    ]);

    // configure the root scope
    mockupApp.run(RootScopeConfig);

    // configure routing
    mockupApp.config(RouteConfig);

    // configure the material design theme
    mockupApp.config(ThemeConfig);

    // inject services
    mockupApp.factory("AppSettingsService", ["$resource", Services.appSettingsServiceFactory]);
    mockupApp.factory("UserInformationService", ["$resource", Services.Personal.userInformationServiceFactory]);
    mockupApp.factory("GearItemService", ["$resource", Services.Gear.gearItemServiceFactory]);
    mockupApp.factory("GearSystemService", ["$resource", Services.Gear.gearSystemServiceFactory]);
    mockupApp.factory("GearCollectionService", ["$resource", Services.Gear.gearCollectionServiceFactory]);
    mockupApp.factory("MealService", ["$resource", Services.Meals.mealServiceFactory]);
    mockupApp.factory("TripItineraryService", ["$resource", Services.Trips.tripItineraryServiceFactory]);
    mockupApp.factory("TripPlanService", ["$resource", Services.Trips.tripPlanServiceFactory]);

    // inject controllers
    mockupApp.controller("AppCtrl", Controllers.AppCtrl);
    mockupApp.controller("AppSettingsCtrl", Controllers.AppSettingsCtrl);

    mockupApp.controller("UserInformationCtrl", Controllers.Personal.UserInformationCtrl);

    mockupApp.controller("AddGearCollectionCtrl", Controllers.Gear.Collections.AddGearCollectionCtrl);
    mockupApp.controller("GearCollectionCtrl", Controllers.Gear.Collections.GearCollectionCtrl);
    mockupApp.controller("GearCollectionsCtrl", Controllers.Gear.Collections.GearCollectionsCtrl);

    mockupApp.controller("GearItemCtrl", Controllers.Gear.Items.GearItemCtrl);
    mockupApp.controller("GearItemsCtrl", Controllers.Gear.Items.GearItemsCtrl);
    mockupApp.controller("AddGearItemCtrl", Controllers.Gear.Items.AddGearItemCtrl);

    mockupApp.controller("GearSystemCtrl", Controllers.Gear.Systems.GearSystemCtrl);
    mockupApp.controller("GearSystemsCtrl", Controllers.Gear.Systems.GearSystemsCtrl);
    mockupApp.controller("AddGearSystemCtrl", Controllers.Gear.Systems.AddGearSystemCtrl);

    mockupApp.controller("MealsCtrl", Controllers.Meals.MealsCtrl);
    mockupApp.controller("MealCtrl", Controllers.Meals.MealCtrl);
    mockupApp.controller("AddMealCtrl", Controllers.Meals.AddMealCtrl);

    mockupApp.controller("TripItinerariesCtrl", Controllers.Trips.Itineraries.TripItinerariesCtrl);
    mockupApp.controller("TripItineraryCtrl", Controllers.Trips.Itineraries.TripItineraryCtrl);
    mockupApp.controller("AddTripItineraryCtrl", Controllers.Trips.Itineraries.AddTripItineraryCtrl);

    mockupApp.controller("TripPlansCtrl", Controllers.Trips.Plans.TripPlansCtrl);
    mockupApp.controller("TripPlanCtrl", Controllers.Trips.Plans.TripPlanCtrl);
    mockupApp.controller("AddTripPlanCtrl", Controllers.Trips.Plans.AddTripPlanCtrl);
}
