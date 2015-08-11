///<reference path="Models/Meals/Meal.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export class MealState {
        /* Meals */

        private _meals: Models.Meals.Meal[];

        public getMeals() : Models.Meals.Meal[] {
            return this._meals;
        }

        public loadMeals(mealResource: any[]) {
            if(this._meals) {
                throw new Error("Meals already loaded!");
            }

            this._meals = <Array<Models.Meals.Meal>>[];
            for(let i=0; i<mealResource.length; ++i) {
                //this._meals.push(new Models.Meal(mealResource[i]));
            }
        }

        private getNextMealId() : number {
            // TODO: write this
            return -1;
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

        /* Load/Save */

        public loadFromDevice() {
            // TODO: load from the resources here and return a promise
        }

        public saveToDevice() {
            // TODO: don't do anything here, just return a promise
        }
    }
}