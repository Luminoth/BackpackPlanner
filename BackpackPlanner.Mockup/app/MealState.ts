/// <reference path="../scripts/typings/angularjs/angular.d.ts" />

/// <reference path="Models/Meals/Meal.ts" />

/// <reference path="Resources/Meals/MealResource.ts" />

/// <reference path="Services/Meals/MealService.ts"/>

module BackpackPlanner.Mockup {
    "use strict";

    export class MealState {
        /* Meals */

        private _meals = <Array<Models.Meals.Meal>>[];

        // TODO: this should be a read-only collection
        public getMeals() {
            return this._meals;
        }

        public getMealIndexById(mealId: number) {
            for(let i=0; i<this._meals.length; ++i) {
                const meal = this._meals[i];
                if(meal.Id == mealId) {
                    return i;
                }
            }
            return -1;
        }

        public getMealById(mealId: number) {
            const idx = this.getMealIndexById(mealId);
            return idx < 0 ? null : this._meals[idx];
        }

        private static _lastMealId = 0;

        private getNextMealId() {
            return ++MealState._lastMealId;
        }

        public addMeal(meal: Models.Meals.Meal) {
            if(meal.Id < 0) {
                meal.Id = this.getNextMealId();
            } else if(meal.Id > MealState._lastMealId) {
                MealState._lastMealId = meal.Id;
            }

            this._meals.push(meal);
            return meal.Id;
        }

        public deleteMeal(meal: Models.Meals.Meal) {
            const idx = this.getMealIndexById(meal.Id);
            if(idx < 0) {
                return false;
            }
            this._meals.splice(idx, 1);
            return true;
        }

        public deleteAllMeals() {
            this._meals = <Array<Models.Meals.Meal>>[];
        }

        /* Utilities */

        public deleteAllData() {
            this.deleteAllMeals();
        }

        /* Load/Save */

        private loadMeals($q: ng.IQService, mealResources: Resources.Meals.IMealResource[]) {
            const promises = <Array<ng.IPromise<any>>>[];
            this._meals = <Array<Models.Meals.Meal>>[];
            for(let i=0; i<mealResources.length; ++i) {
                const meal = new Models.Meals.Meal();
                promises.push(meal.loadFromDevice($q, mealResources[i]).then(
                    (loadedMeal) => {
                        this.addMeal(loadedMeal);
                    }
                ));
            }
            return $q.all(promises);
        }

        public loadFromDevice($q: ng.IQService, mealService: Services.Meals.IMealService) : ng.IPromise<any[]> {
            const promises = <Array<ng.IPromise<any>>>[];

            promises.push(mealService.query().$promise.then(
                (mealResources: Resources.Meals.IMealResource[]) => {
                    this.loadMeals($q, mealResources).then(
                        () => {
                        }
                    );
                }
            ));

            return $q.all(promises);
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any[]> {
            alert("MealState.saveToDevice");
            return $q.defer().promise;
        }
    }
}