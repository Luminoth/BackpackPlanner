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

        /* Weight/Cost */

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

        public loadFromDevice($q: ng.IQService, tripPlanResource: Resources.Trips.ITripPlanResource) : ng.IPromise<any> {
            this.Id = tripPlanResource.Id;
            this.Name = tripPlanResource.Name;
            this.StartDate = tripPlanResource.StartDate;
            this.EndDate = tripPlanResource.EndDate;
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

            this.StartDateAsDate = new Date(this.StartDate);
            this.EndDateAsDate = new Date(this.EndDate);

            return $q.defer().promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            // mockup does nothing here
            return $q.defer().promise;
        }
    }
}
