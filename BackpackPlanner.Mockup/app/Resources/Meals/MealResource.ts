///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

///<reference path="../../Models/Meals/Meal.ts" />

module BackpackPlanner.Mockup.Resources.Meals {
    "use strict";

    export interface IMealResource extends Models.Meals.IMeal, ng.resource.IResource<Models.Meals.IMeal> {
    }
}