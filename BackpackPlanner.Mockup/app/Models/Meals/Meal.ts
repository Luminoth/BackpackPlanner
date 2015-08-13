//<reference path="../../Resources/Meals/MealResource.ts"/>

///<reference path="../../AppState.ts"/>

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
        public ServingCount = 0;
        public WeightInGrams = 0;
        public CostInUSDP = 0;
        public Calories = 0;
        public ProteinInGrams = 0;
        public FiberInGrams = 0;
        public Note = "";

        constructor(mealResource?: Resources.Meals.IMealResource) {
            if(mealResource) {
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
            }
        }

        public getCostPerUnitInCurrency() {
            const costInCurrency = convertUSDPToCurrency(this.CostInUSDP, AppState.getInstance().getAppSettings().Currency);
            const weightInUnits = convertGramsToUnits(this.WeightInGrams, AppState.getInstance().getAppSettings().Units);

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        public weightInUnits(weight: number) : number {
            return arguments.length
                ? (this.WeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().Units))
                : parseFloat(convertGramsToUnits(this.WeightInGrams, AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public costInCurrency(cost: number) : number {
            return arguments.length
                ? (this.CostInUSDP = convertCurrencyToUSDP(cost, AppState.getInstance().getAppSettings().Currency))
                : convertUSDPToCurrency(this.CostInUSDP, AppState.getInstance().getAppSettings().Currency);
        }
    }

    export interface IMealEntry {
        MealId: number;
        Count: number;
        IsPacked: boolean;
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
