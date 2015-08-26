///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

//<reference path="../../Resources/Meals/MealResource.ts"/>

///<reference path="../../AppState.ts"/>

///<reference path="../Entry.ts"/>

module BackpackPlanner.Mockup.Models.Meals {
    "use strict";

    export class Meal {
        private _id = -1;
        private _name = "";
        private _url = "";
        private _meal = "Other";
        private _servingCount = 1;
        private _weightInGrams = 0;
        private _costInUSDP = 0;
        private _calories = 0;
        private _proteinInGrams = 0;
        private _fiberInGrams = 0;
        private _note = "";

        public get Id() {
            return this._id;
        }

        public set Id(id: number) {
            this._id = id;
        }

        public name(name?: string) {
            return arguments.length
                ? (this._name = name)
                : this._name;
        }
        
        public url(url?: string) {
            return arguments.length
                ? (this._url = url)
                : this._url;
        }

        public meal(meal?: string) {
            return arguments.length
                ? (this._meal = meal)
                : this._meal;
        }

        public servingCount(servingCount?: number) {
            return arguments.length
                ? (this._servingCount = servingCount)
                : this._servingCount;
        }

        public calories(calories?: number) {
            return arguments.length
                ? (this._calories = calories)
                : this._calories;
        }

        public getCaloriesPerWeightUnit() {
            return 0 == this._calories ? 0 : this._calories / this.weightInUnits();
        }

        public proteinInGrams(proteinInGrams?: number) {
            return arguments.length
                ? (this._proteinInGrams = proteinInGrams)
                : this._proteinInGrams;
        }

        public fiberInGrams(fiberInGrams?: number) {
            return arguments.length
                ? (this._fiberInGrams = fiberInGrams)
                : this._fiberInGrams;
        }

        public note(note?: string) {
            return arguments.length
                ? (this._note = note)
                : this._note;
        }

        /* Weight/Cost */

        public getWeightInGrams() {
            return this._weightInGrams;
        }

        public weightInUnits(weight?: number) {
            return arguments.length
                ? (this._weightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().units()))
                : parseFloat(convertGramsToUnits(this._weightInGrams, AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getCostInUSDP() {
            return this._costInUSDP;
        }

        public costInCurrency(cost?: number) {
            return arguments.length
                ? (this._costInUSDP = convertCurrencyToUSDP(cost, AppState.getInstance().getAppSettings().currency()))
                : convertUSDPToCurrency(this._costInUSDP, AppState.getInstance().getAppSettings().currency());
        }

        public getCostPerUnitInCurrency(/*units: string, currency: string*/) {
            const weightInUnits = convertGramsToUnits(this._weightInGrams, /*units*/AppState.getInstance().getAppSettings().units());
            const costInCurrency = convertUSDPToCurrency(this._costInUSDP, /*currency*/AppState.getInstance().getAppSettings().currency());

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        /* Load/Save */

        public update(meal: Meal) {
            this._name = meal._name;
            this._url = meal._url;
            this._meal = meal._meal;
            this._servingCount = meal._servingCount;
            this._weightInGrams = meal._weightInGrams;
            this._costInUSDP = meal._costInUSDP;
            this._calories = meal._calories;
            this._proteinInGrams = meal._proteinInGrams;
            this._fiberInGrams = meal._fiberInGrams;
            this._note = meal._note;
        }

        public loadFromDevice($q: ng.IQService, mealResource: Resources.Meals.IMealResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this._id = mealResource.Id;
            this._name = mealResource.Name;
            this._url = mealResource.Url;
            this._meal = mealResource.Meal;
            this._servingCount = mealResource.ServingCount;
            this._weightInGrams = mealResource.WeightInGrams;
            this._costInUSDP = mealResource.CostInUSDP;
            this._calories = mealResource.Calories;
            this._proteinInGrams = mealResource.ProteinInGrams;
            this._fiberInGrams = mealResource.FiberInGrams;
            this._note = mealResource.Note;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("Meal.saveToDevice");
            return $q.defer().promise;
        }
    }

    export class MealEntry implements IEntry {
        private _mealId = -1;
        private _count = 1;

        constructor(mealId: number, count?: number) {
            this._mealId = mealId;

            if(count) {
                this._count = count;
            }
        }

        public getMealId() {
            return this._mealId;
        }

        public count(count?: number) {
            return arguments.length
                ? (this._count = count)
                : this._count;
        }

        public getName() {
            const meal = AppState.getInstance().getMealState().getMealById(this._mealId);
            if(!meal) {
                return "";
            }
            return meal.name();
        }

        public getCalories() {
            const meal = AppState.getInstance().getMealState().getMealById(this._mealId);
            if(!meal) {
                return 0;
            }
            return this._count * meal.calories();
        }

        public getTotalWeightInGrams() {
            const meal = AppState.getInstance().getMealState().getMealById(this._mealId);
            if(!meal) {
                return 0;
            }
            return this._count * meal.getWeightInGrams();
        }

        public getTotalWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getTotalWeightInGrams(), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getCostInUSDP() {
            const meal = AppState.getInstance().getMealState().getMealById(this._mealId);
            if(!meal) {
                return 0;
            }
            return this._count * meal.getCostInUSDP();
        }

        public getCostInCurrency(/*currency: string*/) {
            return convertUSDPToCurrency(this.getCostInUSDP(), /*currency*/AppState.getInstance().getAppSettings().currency());
        }
    }
}
