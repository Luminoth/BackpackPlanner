/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

/// <reference path="../../Resources/Meals/MealResource.ts" />

module BackpackPlanner.Mockup.Services.Meals {
    "use strict";

    export interface IMealService extends ng.resource.IResourceClass<Resources.Meals.IMealResource> {
        query: ng.resource.IResourceArrayMethod<Resources.Meals.IMealResource>;
    }

    export function mealServiceFactory($resource: ng.resource.IResourceService) {
        return <IMealService> $resource("data/meals/meals.json", {}, {
            query: { method: "GET", isArray: true }
        });
    }
}
