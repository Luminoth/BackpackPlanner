///<reference path="../scripts/typings/angularjs/angular-route.d.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    interface ICustomRouteConfig extends ng.route.IRoute {
        title: string;
    }

    class CustomRouteConfig implements ICustomRouteConfig {
        public templateUrl: string;
        public controller: string;
        public title: string;
    }

    export class RouteConfig {
        constructor($routeProvider: ng.route.IRouteProvider) {
            $routeProvider.when("/", {
                redirectTo: "/gear/items"
            });

            // gear items
            this.addRoute($routeProvider, "/gear/items", {
                templateUrl: "content/partials/gear/items/items.html",
                controller: "GearItemsCtrl",
                title: "Gear Items"
            });
            this.addRoute($routeProvider, "/gear/items/add", {
                templateUrl: "content/partials/gear/items/add.html",
                controller: "AddGearItemCtrl",
                title: "Add a Gear Item"
            });
            this.addRoute($routeProvider, "/gear/items/:gearItemId", {
                templateUrl: "content/partials/gear/items/item.html",
                controller: "GearItemCtrl",
                title: "Gear Item"
            });

            // gear system
            this.addRoute($routeProvider, "/gear/systems", {
                templateUrl: "content/partials/gear/systems/systems.html",
                controller: "GearSystemsCtrl",
                title: "Gear Systems"
            });
            this.addRoute($routeProvider, "/gear/systems/add", {
                templateUrl: "content/partials/gear/systems/add.html",
                controller: "AddGearSystemCtrl",
                title: "Add a Gear System"
            });
            this.addRoute($routeProvider, "/gear/systems/:gearSystemId", {
                templateUrl: "content/partials/gear/systems/system.html",
                controller: "GearSystemCtrl",
                title: "Gear System"
            });

            // gear collections
            this.addRoute($routeProvider, "/gear/collections", {
                templateUrl: "content/partials/gear/collections/collections.html",
                controller: "GearCollectionsCtrl",
                title: "Gear Collections"
            });
            this.addRoute($routeProvider, "/gear/collections/add", {
                templateUrl: "content/partials/gear/collections/add.html",
                controller: "AddGearCollectionCtrl",
                title: "Add a Gear Collection"
            });
            this.addRoute($routeProvider, "/gear/collections/:gearCollectionId", {
                templateUrl: "content/partials/gear/collections/collection.html",
                controller: "GearCollectionCtrl",
                title: "Gear Collection"
            });

            // meals
            this.addRoute($routeProvider, "/meals", {
                templateUrl: "content/partials/meals/meals.html",
                controller: "MealsCtrl",
                title: "Meals"
            });
            this.addRoute($routeProvider, "/meals/add", {
                templateUrl: "content/partials/meals/add.html",
                controller: "AddMealCtrl",
                title: "Add a Meal"
            });
            this.addRoute($routeProvider, "/meals/:mealId", {
                templateUrl: "content/partials/meals/meal.html",
                controller: "MealCtrl",
                title: "Meal"
            });

            // trip itineraries
            /*this.addRoute($routeProvider, "/trips/itineraries", {
                templateUrl: "content/partials/trips/itineraries/itineraries.html",
                controller: "TripItinerariesCtrl",
                title: "Trip Itineraries"
            });
            this.addRoute($routeProvider, "/trips/itineraries/add", {
                templateUrl: "content/partials/trips/itineraries/add.html",
                controller: "AddTripItineraryCtrl",
                title: "Add a Trip Itinerary"
            });
            this.addRoute($routeProvider, "/trips/itineraries/:tripItineraryId", {
                templateUrl: "content/partials/trips/itineraries/itinerary.html",
                controller: "TripItineraryCtrl",
                title: "Trip Itinerary"
            });*/

            // trip plans
            this.addRoute($routeProvider, "/trips/plans", {
                templateUrl: "content/partials/trips/plans/plans.html",
                controller: "TripPlansCtrl",
                title: "Trip Plans"
            });
            this.addRoute($routeProvider, "/trips/plans/add", {
                templateUrl: "content/partials/trips/plans/add.html",
                controller: "AddTripPlanCtrl",
                title: "Add a Trip Plan"
            });
            this.addRoute($routeProvider, "/trips/plans/:tripPlanId", {
                templateUrl: "content/partials/trips/plans/plan.html",
                controller: "TripPlanCtrl",
                title: "Trip Plan"
            });

            // personal information and settings
            this.addRoute($routeProvider, "/personal", {
                templateUrl: "content/partials/personal/personal.html",
                controller: "UserInformationCtrl",
                title: "Personal Information"
            });
            this.addRoute($routeProvider, "/settings", {
                templateUrl: "content/partials/settings.html",
                controller: "AppSettingsCtrl",
                title: "Settings"
            });
            this.addRoute($routeProvider, "/help", {
                templateUrl: "content/partials/help.html",
                title: "Help"
            });

            // error codes
            this.addRoute($routeProvider, "/404", {
                templateUrl: "content/partials/404.html",
                title: "Backpacking Planner"
            });
            this.addRoute($routeProvider, "/500", {
                templateUrl: "content/partials/500.html",
                title: "Backpacking Planner"
            });

            // 404 at the bottom
            $routeProvider.otherwise({
                redirectTo: "/404"
            });
        }

        private addRoute($routeProvider: ng.route.IRouteProvider, url: string, routeConfig: ICustomRouteConfig) {
            $routeProvider.when(url, routeConfig);
        }
    };

    RouteConfig.$inject = ["$routeProvider"];
}
