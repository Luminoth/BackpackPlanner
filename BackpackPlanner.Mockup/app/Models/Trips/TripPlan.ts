///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

//<reference path="../../Resources/Trips/TripPlanResource.ts"/>

///<reference path="../Gear/GearCollection.ts"/>
///<reference path="../Gear/GearItem.ts"/>
///<reference path="../Gear/GearSystem.ts"/>

///<reference path="../Meals/Meal.ts"/>

///<reference path="TripItinerary.ts"/>

module BackpackPlanner.Mockup.Models.Trips {
    "use strict";

    export interface ITripPlan {
        Id: number;
        Name: string;
        StartDate: string;
        EndDate: string;
        TripItineraryId: number;
        Note: string;

        GearCollections: Models.Gear.IGearCollectionEntry[];
        GearSystems: Models.Gear.IGearSystemEntry[];
        GearItems: Models.Gear.IGearItemEntry[];

        Meals: Models.Meals.IMealEntry[];
    }

    export class TripPlan implements ITripPlan {
        public Id = -1;
        public Name = "";
        public StartDate = "";
        public EndDate = "";
        public TripItineraryId = -1;
        public Note = "";

        public GearCollections = <Array<Models.Gear.GearCollectionEntry>>[];
        public GearSystems = <Array<Models.Gear.GearSystemEntry>>[];
        public GearItems = <Array<Models.Gear.GearItemEntry>>[];

        public Meals = <Array<Models.Meals.MealEntry>>[];

        public StartDateAsDate = new Date();
        public EndDateAsDate = new Date();

        public getTotalGearItemCount() {
            let count = 0;
            for(let i=0; i<this.GearCollections.length; ++i) {
                const gearCollectionEntry = this.GearCollections[i];
                count += gearCollectionEntry.getGearItemCount();
            }

            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearSystems[i];
                count += gearSystemEntry.getGearItemCount();
            }

            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                count += gearItemEntry.Count;
            }
            return count;
        }

        /* Gear Collections */

        public getGearCollectionCount() {
            let count = 0;
            for(let i=0; i<this.GearCollections.length; ++i) {
                const gearCollectionEntry = this.GearCollections[i];
                count += gearCollectionEntry.Count;
            }
            return count;
        }

        private getGearCollectionEntryIndexById(gearCollectionId: number) : number {
            for(let i=0; i<this.GearCollections.length; ++i) {
                const gearCollectionEntry = this.GearCollections[i];
                if(gearCollectionEntry.GearCollectionId == gearCollectionId) {
                    return i;
                }
            }
            return -1;
        }

        public containsGearCollection(gearCollection: Models.Gear.GearCollection) {
            return this.getGearCollectionEntryIndexById(gearCollection.Id) >= 0;
        }

        public addGearCollection(gearCollection: Models.Gear.GearCollection) {
            if(this.containsGearCollection(gearCollection)) {
                return;
            }
            this.GearCollections.push(new Models.Gear.GearCollectionEntry(gearCollection.Id));
        }

        public removeGearCollection(gearCollection: Models.Gear.GearCollection) {
            const idx = this.getGearCollectionEntryIndexById(gearCollection.Id);
            if(idx < 0) {
                return;
            }
            this.GearCollections.splice(idx, 1);
        }

        public removeAllGearCollections() {
            this.GearCollections = <Array<Models.Gear.GearCollectionEntry>>[];
        }
        
        /* Gear Systems */

        public getGearSystemCount() {
            let count = 0;
            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearSystems[i];
                count += gearSystemEntry.Count;
            }
            return count;
        }

        private getGearSystemEntryIndexById(gearSystemId: number) : number {
            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearSystems[i];
                if(gearSystemEntry.GearSystemId == gearSystemId) {
                    return i;
                }
            }
            return -1;
        }

        public containsGearSystem(gearSystem: Models.Gear.GearSystem) {
            return this.getGearSystemEntryIndexById(gearSystem.Id) >= 0;
        }

        public addGearSystem(gearSystem: Models.Gear.GearSystem) {
            if(this.containsGearSystem(gearSystem)) {
                return;
            }
            this.GearSystems.push(new Models.Gear.GearSystemEntry(gearSystem.Id));
        }

        public removeGearSystem(gearSystem: Models.Gear.GearSystem) {
            const idx = this.getGearSystemEntryIndexById(gearSystem.Id);
            if(idx < 0) {
                return;
            }
            this.GearSystems.splice(idx, 1);
        }

        public removeAllGearSystems() {
            this.GearSystems = <Array<Models.Gear.GearSystemEntry>>[];
        }

        /* Gear Items */

        public getGearItemCount() {
            let count = 0;
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                count += gearItemEntry.Count;
            }
            return count;
        }

        private getGearItemEntryIndexById(gearItemId: number) : number {
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                if(gearItemEntry.GearItemId == gearItemId) {
                    return i;
                }
            }
            return -1;
        }

        public containsGearItem(gearItem: Models.Gear.GearItem) {
            return this.getGearItemEntryIndexById(gearItem.Id) >= 0;
        }

        public addGearItem(gearItem: Models.Gear.GearItem) {
            if(this.containsGearItem(gearItem)) {
                return;
            }
            this.GearItems.push(new Models.Gear.GearItemEntry(gearItem.Id));
        }

        public removeGearItem(gearItem: Models.Gear.GearItem) {
            const idx = this.getGearItemEntryIndexById(gearItem.Id);
            if(idx < 0) {
                return;
            }
            this.GearItems.splice(idx, 1);
        }

        public removeAllGearItems() {
            this.GearItems = <Array<Models.Gear.GearItemEntry>>[];
        }

        /* Meals */

        public getMealCount() {
            let count = 0;
            for(let i=0; i<this.Meals.length; ++i) {
                const mealEntry = this.Meals[i];
                count += mealEntry.Count;
            }
            return count;
        }

        private getMealEntryIndexById(mealId: number) : number {
            for(let i=0; i<this.Meals.length; ++i) {
                const mealEntry = this.Meals[i];
                if(mealEntry.MealId == mealId) {
                    return i;
                }
            }
            return -1;
        }

        public containsMeal(meal: Models.Meals.Meal) {
            return this.getMealEntryIndexById(meal.Id) >= 0;
        }

        public addMeal(meal: Models.Meals.Meal) {
            if(this.containsMeal(meal)) {
                return;
            }
            this.Meals.push(new Models.Meals.MealEntry(meal.Id));
        }

        public removeMeal(meal: Models.Meals.Meal) {
            const idx = this.getMealEntryIndexById(meal.Id);
            if(idx < 0) {
                return;
            }
            this.Meals.splice(idx, 1);
        }

        public removeAllMeals() {
            this.Meals = <Array<Models.Meals.MealEntry>>[];
        }

        /* Pack List */

        public getPackedGearItemCount() {
            let count = 0;
            for(let i=0; i<this.GearCollections.length; ++i) {
                const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this.GearCollections[i].GearCollectionId);
                count += gearCollection.getPackedGearItemCount();
            }

            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystems[i].GearSystemId);
                count += gearSystem.getPackedGearItemCount();
            }

            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                if(gearItemEntry.IsPacked) {
                    ++count;
                }
            }
            return count;
        }

        public getPackedMealCount() {
            let count = 0;
            for(let i=0; i<this.Meals.length; ++i) {
                const mealEntry = this.Meals[i];
                if(mealEntry.IsPacked) {
                    ++count;
                }
            }
            return count;
        }

        public getPackList() {
            let entries = <Array<IEntry>>[];
            for(let i=0; i<this.GearCollections.length; ++i) {
                const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this.GearCollections[i].GearCollectionId);
                entries = entries.concat(gearCollection.getPackList());
            }

            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystems[i].GearSystemId);
                entries = entries.concat(gearSystem.getPackList());
            }

            for(let i=0; i<this.GearItems.length; ++i) {
                entries.push(this.GearItems[i]);
            }

            for(let i=0; i<this.Meals.length; ++i) {
                entries.push(this.Meals[i]);
            }
            return entries;
        }

        /* Weight/Cost */

        public getTotalCalories() {
            let calories = 0;
            for(let i=0; i<this.Meals.length; ++i) {
                const mealEntry = this.Meals[i];
                calories += mealEntry.getCalories();
            }
            return calories;
        }

        //// TODO: MOVE THIS INTO A UTILITY CLASS OR SOMETHING
        //// AND MAKE THE CLASSES CONFIGURABLE
        public getWeightClass() {
            const weightInGrams = this.getWeightInGrams();
            if(weightInGrams < 4500) {
                return "Ultralight";
            } else if(weightInGrams < 9000) {
                return "Lightweight";
            }
            return "Traditional";
        }

        public getWeightInGrams() {
            let weightInGrams = 0;
            for(let i=0; i<this.GearCollections.length; ++i) {
                const gearCollectionEntry = this.GearCollections[i];
                weightInGrams += gearCollectionEntry.getWeightInGrams();
            }

            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearSystems[i];
                weightInGrams += gearSystemEntry.getWeightInGrams();
            }

            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                weightInGrams += gearItemEntry.getWeightInGrams();
            }

            for(let i=0; i<this.Meals.length; ++i) {
                const mealEntry = this.Meals[i];
                weightInGrams += mealEntry.getWeightInGrams();
            }
            return weightInGrams;
        }

        public getWeightInUnits() {
            return convertGramsToUnits(this.getWeightInGrams(), AppState.getInstance().getAppSettings().Units);
        }

        public getCostInUSDP() {
            let costInUSDP = 0;
            for(let i=0; i<this.GearCollections.length; ++i) {
                const gearCollectionEntry = this.GearCollections[i];
                costInUSDP += gearCollectionEntry.getCostInUSDP();
            }

            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearSystems[i];
                costInUSDP += gearSystemEntry.getCostInUSDP();
            }

            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                costInUSDP += gearItemEntry.getCostInUSDP();
            }

            for(let i=0; i<this.Meals.length; ++i) {
                const mealEntry = this.Meals[i];
                costInUSDP += mealEntry.getCostInUSDP();
            }
            return costInUSDP;
        }

        public getCostInCurrency() {
            return convertUSDPToCurrency(this.getCostInUSDP(), AppState.getInstance().getAppSettings().Currency);
        }

        public getCostPerUnitInCurrency() {
            const costInCurrency = convertUSDPToCurrency(this.getCostInUSDP(), AppState.getInstance().getAppSettings().Currency);
            const weightInUnits = convertGramsToUnits(this.getWeightInGrams(), AppState.getInstance().getAppSettings().Units);

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        /* Load/Save */

        public update(tripPlan: TripPlan) {
            this.Name = tripPlan.Name;
            this.StartDateAsDate = this.StartDateAsDate;
            this.StartDate = tripPlan.StartDateAsDate.toString();
            this.EndDateAsDate = this.EndDateAsDate;
            this.EndDate = tripPlan.EndDateAsDate.toString();
            this.TripItineraryId = tripPlan.TripItineraryId;
            this.Note = tripPlan.Note;

            this.GearCollections = <Array<Models.Gear.GearCollectionEntry>>[];
            for(let i=0; i<tripPlan.GearCollections.length; ++i) {
                const gearCollectionEntry = tripPlan.GearCollections[i];
                this.GearCollections.push(new Models.Gear.GearCollectionEntry(gearCollectionEntry.GearCollectionId, gearCollectionEntry.Count, gearCollectionEntry.IsPacked));
            }

            this.GearSystems = <Array<Models.Gear.GearSystemEntry>>[];
            for(let i=0; i<tripPlan.GearSystems.length; ++i) {
                const gearSystemEntry = tripPlan.GearSystems[i];
                this.GearSystems.push(new Models.Gear.GearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count, gearSystemEntry.IsPacked));
            }

            this.GearItems = <Array<Models.Gear.GearItemEntry>>[];
            for(let i=0; i<tripPlan.GearItems.length; ++i) {
                const gearItemEntry = tripPlan.GearItems[i];
                this.GearItems.push(new Models.Gear.GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
            }

            this.Meals = <Array<Models.Meals.MealEntry>>[];
            for(let i=0; i<tripPlan.Meals.length; ++i) {
                const mealEntry = tripPlan.Meals[i];
                this.Meals.push(new Models.Meals.MealEntry(mealEntry.MealId, mealEntry.Count, mealEntry.IsPacked));
            }
        }

        public loadFromDevice($q: ng.IQService, tripPlanResource: Resources.Trips.ITripPlanResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this.Id = tripPlanResource.Id;
            this.Name = tripPlanResource.Name;
            this.StartDate = tripPlanResource.StartDate;
            this.StartDateAsDate = new Date(this.StartDate);
            this.EndDate = tripPlanResource.EndDate;
            this.EndDateAsDate = new Date(this.EndDate);
            this.TripItineraryId = tripPlanResource.TripItineraryId;
            this.Note = tripPlanResource.Note;

            for(let i=0; i<tripPlanResource.GearCollections.length; ++i) {
                const gearCollectionEntry = tripPlanResource.GearCollections[i];
                this.GearCollections.push(new Models.Gear.GearCollectionEntry(gearCollectionEntry.GearCollectionId, gearCollectionEntry.Count, gearCollectionEntry.IsPacked));
            }

            for(let i=0; i<tripPlanResource.GearSystems.length; ++i) {
                const gearSystemEntry = tripPlanResource.GearSystems[i];
                this.GearSystems.push(new Models.Gear.GearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count, gearSystemEntry.IsPacked));
            }

            for(let i=0; i<tripPlanResource.GearItems.length; ++i) {
                const gearItemEntry = tripPlanResource.GearItems[i];
                this.GearItems.push(new Models.Gear.GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
            }

            for(let i=0; i<tripPlanResource.Meals.length; ++i) {
                const mealEntry = tripPlanResource.Meals[i];
                this.Meals.push(new Models.Meals.MealEntry(mealEntry.MealId, mealEntry.Count, mealEntry.IsPacked));
            }

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("TripPlan.saveToDevice");
            return $q.defer().promise;
        }
    }
}
