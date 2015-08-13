///<reference path="../scripts/typings/angularjs/angular.d.ts" />

///<reference path="Models/Meals/Meal.ts" />

///<reference path="Resources/Meals/MealResource.ts" />

///<reference path="Services/Meals/MealService.ts"/>

module BackpackPlanner.Mockup {
    "use strict";

    export class MealState {
        /* Meals */

        private _meals: Models.Meals.Meal[];

        // TODO: this should be a read-only collection
        public getMeals() : Models.Meals.Meal[] {
            return this._meals;
        }

        public getMealIndexById(mealId: number) : number {
            for(let i=0; i<this._meals.length; ++i) {
                const meal = this._meals[i];
                if(meal.Id == mealId) {
                    return i;
                }
            }
            return -1;
        }

        public getMealById(mealId: number) : Models.Meals.Meal {
            const idx = this.getMealIndexById(mealId);
            return idx < 0 ? null : this._meals[idx];
        }

        private getNextMealId() : number {
            // TODO: write this
            return -1;
        }

        public addMeal(meal: Models.Meals.Meal) : number {
            if(meal.Id < 0) {
                meal.Id = this.getNextMealId();
            }
            this._meals.push(meal);
            return meal.Id;
        }

        public deleteMeal(meal: Models.Meals.Meal) : boolean {
            const idx = this.getMealIndexById(meal.Id);
            if(idx < 0) {
                return false;
            }
            this._meals.splice(idx, 1);

            // TODO: remove the meal from the trip plans it belongs to

            return true;
        }

        public deleteAllMeals() : void {
            this._meals = <Array<Models.Meals.Meal>>[];
        }

        /* Load/Save */

        private loadMeals(mealResources: Resources.Meals.IMealResource[]) {
            if(this._meals) {
                throw new Error("Meals already loaded!");
            }

            this._meals = <Array<Models.Meals.Meal>>[];
            for(let i=0; i<mealResources.length; ++i) {
                this._meals.push(new Models.Meals.Meal(mealResources[i]));
            }
        }

        public loadFromDevice($q: ng.IQService, mealService: Services.Meals.IMealService) : ng.IPromise<any[]> {
            const promises = <Array<ng.IPromise<any>>>[];

            promises.push(mealService.query().$promise.then(
                (mealResources: Resources.Meals.IMealResource[]) => {
                    this.loadMeals(mealResources);
                }
            ));

            return $q.all(promises);
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            // mockup does nothing here
            return $q.defer().promise;
        }
    }
}