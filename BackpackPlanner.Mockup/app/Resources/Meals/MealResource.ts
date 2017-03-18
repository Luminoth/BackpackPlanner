/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />

/// <reference path="../../Models/Meals/Meal.ts" />

module BackpackPlanner.Mockup.Resources.Meals {
    "use strict";

    export interface IMeal {
        Id: number;
        Name: string;
        Url: string;
        Meal: string;
        ServingCount: number;
        WeightInGrams: number;
        CostInUSDP: number;
        Calories: number;
        ProteinInGrams: number;
        FiberInGrams: number;
        Note: string;
    }

    export interface IMealEntry {
        MealId: number;
        Count: number;
    }

    export interface IMealResource extends IMeal, ng.resource.IResource<IMeal> {
    }
}
