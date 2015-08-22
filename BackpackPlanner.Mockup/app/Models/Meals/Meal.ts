///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

//<reference path="../../Resources/Meals/MealResource.ts"/>

///<reference path="../../AppState.ts"/>

///<reference path="../Entry.ts"/>

module BackpackPlanner.Mockup.Models.Meals {
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

    export class Meal implements IMeal {
        public Id = -1;
        public Name = "";
        public Url = "";
        public Meal = "Other";
        public ServingCount = 1;
        public WeightInGrams = 0;
        public CostInUSDP = 0;
        public Calories = 0;
        public ProteinInGrams = 0;
        public FiberInGrams = 0;
        public Note = "";

        /* Weight/Cost */

        public getCaloriesPerUnit() {
            return 0 == this.Calories ? 0 : this.Calories / this.weightInUnits();
        }

        public weightInUnits(weight?: number) : number {
            return arguments.length
                ? (this.WeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().Units))
                : parseFloat(convertGramsToUnits(this.WeightInGrams, AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public costInCurrency(cost?: number) : number {
            return arguments.length
                ? (this.CostInUSDP = convertCurrencyToUSDP(cost, AppState.getInstance().getAppSettings().Currency))
                : convertUSDPToCurrency(this.CostInUSDP, AppState.getInstance().getAppSettings().Currency);
        }

        public getCostPerUnitInCurrency() {
            const costInCurrency = convertUSDPToCurrency(this.CostInUSDP, AppState.getInstance().getAppSettings().Currency);
            const weightInUnits = convertGramsToUnits(this.WeightInGrams, AppState.getInstance().getAppSettings().Units);

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        /* Load/Save */

        public update(meal: Meal) {
            this.Name = meal.Name;
            this.Url = meal.Url;
            this.Meal = meal.Meal;
            this.ServingCount = meal.ServingCount;
            this.WeightInGrams = meal.WeightInGrams;
            this.CostInUSDP = meal.CostInUSDP;
            this.Calories = meal.Calories;
            this.ProteinInGrams = meal.ProteinInGrams;
            this.FiberInGrams = meal.FiberInGrams;
            this.Note = meal.Note;
        }

        public loadFromDevice($q: ng.IQService, mealResource: Resources.Meals.IMealResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this.Id = mealResource.Id;
            this.Name = mealResource.Name;
            this.Url = mealResource.Url;
            this.Meal = mealResource.Meal;
            this.ServingCount = mealResource.ServingCount;
            this.WeightInGrams = mealResource.WeightInGrams;
            this.CostInUSDP = mealResource.CostInUSDP;
            this.Calories = mealResource.Calories;
            this.ProteinInGrams = mealResource.ProteinInGrams;
            this.FiberInGrams = mealResource.FiberInGrams;
            this.Note = mealResource.Note;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("Meal.saveToDevice");
            return $q.defer().promise;
        }
    }

    export interface IMealEntry extends IEntry {
        MealId: number;
    }

    export class MealEntry implements IMealEntry {
        public MealId = -1;
        public Count = 1;
        public IsPacked = false;

        constructor(mealId: number, count?: number, isPacked?: boolean) {
            this.MealId = mealId;

            if(count) {
                this.Count = count;
            }

            if(isPacked) {
                this.IsPacked = isPacked;
            }
        }

        public getName() : string {
            const meal = AppState.getInstance().getMealState().getMealById(this.MealId);
            if(!meal) {
                return "";
            }
            return meal.Name;
        }

        public getCalories() : number {
            const meal = AppState.getInstance().getMealState().getMealById(this.MealId);
            if(!meal) {
                return 0;
            }
            return this.Count * meal.Calories;
        }

        public getWeightInGrams() : number {
            const meal = AppState.getInstance().getMealState().getMealById(this.MealId);
            if(!meal) {
                return 0;
            }
            return this.Count * meal.WeightInGrams;
        }

        public getWeightInUnits() : number {
            return parseFloat(convertGramsToUnits(this.getWeightInGrams(), AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public getCostInUSDP() {
            const meal = AppState.getInstance().getMealState().getMealById(this.MealId);
            if(!meal) {
                return 0;
            }
            return this.Count * meal.CostInUSDP;
        }

        public getCostInCurrency() {
            return convertUSDPToCurrency(this.getCostInUSDP(), AppState.getInstance().getAppSettings().Currency);
        }
    }
}
