///<reference path="../scripts/typings/angularjs/angular-route.d.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export class RouteConfig {
        constructor($routeProvider: ng.route.IRouteProvider) {
            $routeProvider.when("/", {
                redirectTo: "/gear/items"
            })

            // gear items
            .when("/gear/items", {
                templateUrl: "content/partials/gear/items/items.html",
                controller: "GearItemsCtrl",
                title: "Gear Items"
            })
            .when("/gear/items/add", {
                templateUrl: "content/partials/gear/items/add.html",
                controller: "AddGearItemCtrl",
                title: "Add a Gear Item"
            })
            .when("/gear/items/:gearItemId", {
                templateUrl: "content/partials/gear/items/item.html",
                controller: "GearItemCtrl",
                title: "Gear Item"
            })

            // gear systems
            .when("/gear/systems", {
                templateUrl: "content/partials/gear/systems/systems.html",
                controller: "GearSystemsCtrl",
                title: "Gear Systems"
            })
            .when("/gear/systems/add", {
                templateUrl: "content/partials/gear/systems/add.html",
                controller: "AddGearSystemCtrl",
                title: "Add a Gear System"
            })
            .when("/gear/systems/:gearSystemId", {
                templateUrl: "content/partials/gear/systems/system.html",
                controller: "GearSystemCtrl",
                title: "Gear System"
            })

            // gear collections
            .when("/gear/collections", {
                templateUrl: "content/partials/gear/collections/collections.html",
                controller: "GearCollectionsCtrl",
                title: "Gear Collections"
            })
            .when("/gear/collections/add", {
                templateUrl: "content/partials/gear/collections/add.html",
                controller: "AddGearCollectionCtrl",
                title: "Add a Gear Collection"
            })
            .when("/gear/collections/:gearCollectionId", {
                templateUrl: "content/partials/gear/collections/collection.html",
                controller: "GearCollectionCtrl",
                title: "Gear Collection"
            })

            // meals
            .when("/meals", {
                templateUrl: "content/partials/meals/meals.html",
                controller: "MealsCtrl",
                title: "Meals"
            })
            .when("/meals/add", {
                templateUrl: "content/partials/meals/add.html",
                controller: "AddMealCtrl",
                title: "Add a Meal"
            })
            .when("/meals/:mealId", {
                templateUrl: "content/partials/meals/meal.html",
                controller: "MealCtrl",
                title: "Meal"
            })

            // trip itineraries
            /*.when("/trips/itineraries", {
                templateUrl: "content/partials/trips/itineraries/itineraries.html",
                controller: "TripItinerariesCtrl",
                title: "Trip Itineraries"
            })
            .when("/trips/itineraries/add", {
                templateUrl: "content/partials/trips/itineraries/add.html",
                controller: "AddTripItineraryCtrl",
                title: "Add a Trip Itinerary"
            })
            .when("/trips/itineraries/:tripItineraryId", {
                templateUrl: "content/partials/trips/itineraries/itinerary.html",
                controller: "TripItineraryCtrl",
                title: "Trip Itinerary"
            })*/

            // trip plans
            .when("/trips/plans", {
                templateUrl: "content/partials/trips/plans/plans.html",
                controller: "TripPlansCtrl",
                title: "Trip Plans"
            })
            .when("/trips/plans/add", {
                templateUrl: "content/partials/trips/plans/add.html",
                controller: "AddTripPlanCtrl",
                title: "Add a Trip Plan"
            })
            .when("/trips/plans/:tripPlanId", {
                templateUrl: "content/partials/trips/plans/plan.html",
                controller: "TripPlanCtrl",
                title: "Trip Plan"
            })

            // personal information and settings
            .when("/personal", {
                templateUrl: "content/partials/personal/personal.html",
                controller: "UserInformationCtrl",
                title: "Personal Information"
            })
            .when("/settings", {
                templateUrl: "content/partials/settings.html",
                controller: "AppSettingsCtrl",
                title: "Settings"
            })
            .when("/help", {
                templateUrl: "content/partials/help.html",
                title: "Help"
            })

            .when("/404", {
                templateUrl: "content/partials/404.html",
                title: "Backpacking Planner"
            }).when("/500", {
                templateUrl: "content/partials/500.html",
                title: "Backpacking Planner"
            })
            .otherwise({
                redirectTo: "/404"
            });
        }
    };

    RouteConfig.$inject = ["$routeProvider"];
}
