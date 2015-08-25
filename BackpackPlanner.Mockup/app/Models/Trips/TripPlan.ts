///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/underscore/underscore.d.ts" />

//<reference path="../../Resources/Trips/TripPlanResource.ts"/>

///<reference path="../../AppState.ts"/>

///<reference path="../Gear/GearCollection.ts"/>
///<reference path="../Gear/GearItem.ts"/>
///<reference path="../Gear/GearSystem.ts"/>

///<reference path="../Meals/Meal.ts"/>

///<reference path="TripItinerary.ts"/>

module BackpackPlanner.Mockup.Models.Trips {
    "use strict";

    export class TripPlan {
        private _id = -1;
        private _name = "";
        private _startDate = new Date();
        private _endDate = new Date();
        private _tripItineraryId = -1;
        private _note = "";

        private _gearCollections = <Array<Models.Gear.GearCollectionEntry>>[];
        private _gearSystems = <Array<Models.Gear.GearSystemEntry>>[];
        private _gearItems = <Array<Models.Gear.GearItemEntry>>[];

        private _meals = <Array<Models.Meals.MealEntry>>[];

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

        public startDate(startDate?: Date) {
            return arguments.length
                ? (this._startDate = startDate)
                : this._startDate;
        }

        public endDate(endDate?: Date) {
            return arguments.length
                ? (this._endDate = endDate)
                : this._endDate;
        }

        public tripItineraryId(tripItineraryId?: number) {
            return arguments.length
                ? (this._tripItineraryId = tripItineraryId)
                : this._tripItineraryId;
        }

        public note(note?: string) {
            return arguments.length
                ? (this._note = note)
                : this._note;
        }

        public getTotalGearItemCount() {
            const visitedGearItems = <Array<number>>[];

            let count = 0;
            for(let i=0; i<this._gearCollections.length; ++i) {
                const gearCollectionEntry = this._gearCollections[i];
                count += gearCollectionEntry.getGearItemCount(visitedGearItems);
            }

            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystemEntry = this._gearSystems[i];
                count += gearSystemEntry.getGearItemCount(visitedGearItems);
            }

            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItemEntry = this._gearItems[i];
                if(_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                    continue;
                }

                visitedGearItems.push(gearItemEntry.getGearItemId());
                count += gearItemEntry.count();
            }
            return count;
        }

        public getTotalCalories() {
            const visitedMeals = <Array<number>>[];

            let calories = 0;
            for(let i=0; i<this._meals.length; ++i) {
                const mealEntry = this._meals[i];
                if(_.contains(visitedMeals, mealEntry.getMealId())) {
                    continue;
                }

                visitedMeals.push(mealEntry.getMealId());
                calories += mealEntry.getCalories();
            }
            return calories;
        }

        /* Gear Collections */

        public getGearCollections() {
            return this._gearCollections;
        }

        public getGearCollectionCount(visitedGearCollections: number[]) {
            if(!visitedGearCollections) {
                visitedGearCollections = <Array<number>>[];
            }

            let count = 0;
            for(let i=0; i<this._gearCollections.length; ++i) {
                const gearCollectionEntry = this._gearCollections[i];
                if(_.contains(visitedGearCollections, gearCollectionEntry.getGearCollectionId())) {
                    continue;
                }

                visitedGearCollections.push(gearCollectionEntry.getGearCollectionId());
                count += gearCollectionEntry.count();
            }
            return count;
        }

        private getGearCollectionEntryIndexById(gearCollectionId: number) {
            return _.findIndex(this._gearCollections, (gearCollectionEntry) => {
                    return gearCollectionEntry.getGearCollectionId() == gearCollectionId;
                }
            );
        }

        public containsGearCollectionById(gearCollectionId: number) {
            return undefined != _.find(this._gearCollections, (gearCollectionEntry) => {
                    return gearCollectionEntry.getGearCollectionId() == gearCollectionId;
                }
            );
        }

        public containsGearCollectionSystems(gearCollection: Models.Gear.GearCollection) {
            const gearSystems = gearCollection.getGearSystems();
            for(let i=0; i<gearSystems.length; ++i) {
                const gearSystemEntry = gearSystems[i];
                if(this.containsGearSystemById(gearSystemEntry.getGearSystemId())) {
                    return true;
                }

                const gearSystem = AppState.getInstance().getGearState().getGearSystemById(gearSystemEntry.getGearSystemId());
                if(gearSystem && this.containsGearSystemItems(gearSystem)) {
                    return true;
                }
            }
            return false;
        }

        public containsGearCollectionItems(gearCollection: Models.Gear.GearCollection) {
            const gearItems = gearCollection.getGearItems();
            for(let i=0; i<gearItems.length; ++i) {
                const gearItemEntry = gearItems[i];
                if(this.containsGearItemById(gearItemEntry.getGearItemId())) {
                    return true;
                }
            }
            return false;
        }

        public addGearCollection(gearCollection: Models.Gear.GearCollection) {
            if(this.containsGearCollectionById(gearCollection.Id)) {
                return false;
            }

            if(this.containsGearCollectionSystems(gearCollection)) {
                return false;
            }

            if(this.containsGearCollectionItems(gearCollection)) {
                return false;
            }

            this._gearCollections.push(new Models.Gear.GearCollectionEntry(gearCollection.Id));
            return true;
        }

        private addGearCollectionEntry(gearCollectionId: number, count: number) {
            if(this.containsGearCollectionById(gearCollectionId)) {
                return false;
            }

            /*const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(gearCollectionId);
            if(!gearCollection) {
                return false;
            }

            if(this.containsGearCollectionItems(gearCollection)) {
                return false;
            }*/
            
            this._gearCollections.push(new Models.Gear.GearCollectionEntry(gearCollectionId, count));
            return true;
        }

        public removeGearCollectionById(gearCollectionId: number) {
            const idx = this.getGearCollectionEntryIndexById(gearCollectionId);
            if(idx < 0) {
                return false;
            }

            this._gearCollections.splice(idx, 1);
            return true;
        }

        public removeAllGearCollections() {
            this._gearCollections = <Array<Models.Gear.GearCollectionEntry>>[];
        }
        
        /* Gear Systems */

        public getGearSystems() {
            return this._gearSystems;
        }

        public getGearSystemCount(visitedGearSystems: number[]) {
            if(!visitedGearSystems) {
                visitedGearSystems = <Array<number>>[];
            }

            let count = 0;
            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystemEntry = this._gearSystems[i];
                if(_.contains(visitedGearSystems, gearSystemEntry.getGearSystemId())) {
                    continue;
                }

                visitedGearSystems.push(gearSystemEntry.getGearSystemId());
                count += gearSystemEntry.count();
            }
            return count;
        }

        private getGearSystemEntryIndexById(gearSystemId: number) {
            return _.findIndex(this._gearSystems, (gearSystemEntry) => {
                    return gearSystemEntry.getGearSystemId() == gearSystemId;
                }
            );
        }

        public containsGearSystemById(gearSystemId: number) {
            if(_.find(this._gearCollections, (gearCollectionEntry) => {
                    const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(gearCollectionEntry.getGearCollectionId());
                    if(!gearCollection) {
                        return false;
                    }
                    return gearCollection.containsGearSystemById(gearSystemId);
                }
            )) {
                return true;
            }

            return undefined != _.find(this._gearSystems, (gearSystemEntry) => {
                    return gearSystemEntry.getGearSystemId() == gearSystemId;
                }
            );
        }

        public containsGearSystemItems(gearSystem: Models.Gear.GearSystem) {
            const gearItems = gearSystem.getGearItems();
            for(let i=0; i<gearItems.length; ++i) {
                const gearItemEntry = gearItems[i];
                if(this.containsGearItemById(gearItemEntry.getGearItemId())) {
                    return true;
                }
            }
            return false;
        }

        public addGearSystem(gearSystem: Models.Gear.GearSystem) {
            if(this.containsGearSystemById(gearSystem.Id)) {
                return false;
            }

            if(this.containsGearSystemItems(gearSystem)) {
                return false;
            }

            this._gearSystems.push(new Models.Gear.GearSystemEntry(gearSystem.Id));
            return true;
        }

        private addGearSystemEntry(gearSystemId: number, count: number) {
            if(this.containsGearSystemById(gearSystemId)) {
                return false;
            }

            /*const gearSystem = AppState.getInstance().getGearState().getGearSystemById(gearSystemId);
            if(!gearSystem) {
                return false;
            }

            if(this.containsGearSystemItems(gearSystem)) {
                return false;
            }*/
            
            this._gearSystems.push(new Models.Gear.GearSystemEntry(gearSystemId, count));
            return true;
        }

        public removeGearSystemById(gearSystemId: number) {
            const idx = this.getGearSystemEntryIndexById(gearSystemId);
            if(idx < 0) {
                return false;
            }

            this._gearSystems.splice(idx, 1);
            return true;
        }

        public removeAllGearSystems() {
            this._gearSystems = <Array<Models.Gear.GearSystemEntry>>[];
        }

        /* Gear Items */

        public getGearItems() {
            return this._gearItems;
        }

        public getGearItemCount(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let count = 0;
            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItemEntry = this._gearItems[i];
                if(_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                    continue;
                }

                visitedGearItems.push(gearItemEntry.getGearItemId());
                count += gearItemEntry.count();
            }
            return count;
        }

        private getGearItemEntryIndexById(gearItemId: number) {
            return _.findIndex(this._gearItems, (gearItemEntry) => {
                    return gearItemEntry.getGearItemId() == gearItemId;
                }
            );
        }

        public containsGearItemById(gearItemId: number) {
            if(_.find(this._gearCollections, (gearCollectionEntry) => {
                    const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(gearCollectionEntry.getGearCollectionId());
                    if(!gearCollection) {
                        return false;
                    }
                    return gearCollection.containsGearItemById(gearItemId);
                }
            )) {
                return true;
            }

            if(_.find(this._gearSystems, (gearSystemEntry) => {
                    const gearSystem = AppState.getInstance().getGearState().getGearSystemById(gearSystemEntry.getGearSystemId());
                    if(!gearSystem) {
                        return false;
                    }
                    return gearSystem.containsGearItemById(gearItemId);
                }
            )) {
                return true;
            }

            return undefined != _.find(this._gearItems, (gearItemEntry) => {
                    return gearItemEntry.getGearItemId() == gearItemId;
                }
            );
        }

        public addGearItem(gearItem: Models.Gear.GearItem) {
            if(this.containsGearItemById(gearItem.Id)) {
                return false;
            }

            this._gearItems.push(new Models.Gear.GearItemEntry(gearItem.Id));
            return true;
        }

        private addGearItemEntry(gearItemId: number, count: number) {
            if(this.containsGearItemById(gearItemId)) {
                return false;
            }

            this._gearItems.push(new Models.Gear.GearItemEntry(gearItemId, count));
            return true;
        }

        public removeGearItemById(gearItemId: number) {
            const idx = this.getGearItemEntryIndexById(gearItemId);
            if(idx < 0) {
                return false;
            }

            this._gearItems.splice(idx, 1);
            return true;
        }

        public removeAllGearItems() {
            this._gearItems = <Array<Models.Gear.GearItemEntry>>[];
        }

        /* Meals */

        public getMeals() {
            return this._meals;
        }

        public getMealCount() {
            const visitedMeals = <Array<number>>[];

            let count = 0;
            for(let i=0; i<this._meals.length; ++i) {
                const mealEntry = this._meals[i];
                if(_.contains(visitedMeals, mealEntry.getMealId())) {
                    continue;
                }

                visitedMeals.push(mealEntry.getMealId());
                count += mealEntry.count();
            }
            return count;
        }

        private getMealEntryIndexById(mealId: number) {
            return _.findIndex(this._meals, (mealEntry) => {
                    return mealEntry.getMealId() == mealId;
                }
            );
        }

        public containsMealById(mealId: number) {
            return undefined != _.find(this._meals, (mealEntry) => {
                    return mealEntry.getMealId() == mealId;
                }
            );
        }

        public addMeal(meal: Models.Meals.Meal) {
            if(this.containsMealById(meal.Id)) {
                return false;
            }

            this._meals.push(new Models.Meals.MealEntry(meal.Id));
            return true;
        }

        private addMealEntry(mealId: number, count: number) {
            if(this.containsMealById(mealId)) {
                return false;
            }

            this._meals.push(new Models.Meals.MealEntry(mealId, count));
            return true;
        }

        public removeMealById(mealId: number) {
            const idx = this.getMealEntryIndexById(mealId);
            if(idx < 0) {
                return false;
            }

            this._meals.splice(idx, 1);
            return true;
        }

        public removeAllMeals() {
            this._meals = <Array<Models.Meals.MealEntry>>[];
        }

        /* Weight/Cost */

        public getWeightClass() {
            return AppState.getInstance().getAppSettings().getWeightClass(this.getWeightInGrams([], []));
        }

        public getWeightInGrams(visitedGearItems: number[], visitedMeals: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let weightInGrams = 0;
            for(let i=0; i<this._gearCollections.length; ++i) {
                const gearCollectionEntry = this._gearCollections[i];
                weightInGrams += gearCollectionEntry.getWeightInGrams(visitedGearItems);
            }

            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystemEntry = this._gearSystems[i];
                weightInGrams += gearSystemEntry.getWeightInGrams(visitedGearItems);
            }

            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItemEntry = this._gearItems[i];
                if(_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                    continue;
                }

                visitedGearItems.push(gearItemEntry.getGearItemId());
                weightInGrams += gearItemEntry.getWeightInGrams();
            }

            for(let i=0; i<this._meals.length; ++i) {
                const mealEntry = this._meals[i];
                if(_.contains(visitedMeals, mealEntry.getMealId())) {
                    continue;
                }

                visitedMeals.push(mealEntry.getMealId());
                weightInGrams += mealEntry.getWeightInGrams();
            }
            return weightInGrams;
        }

        public getWeightInUnits(/*units: string*/) {
            return convertGramsToUnits(this.getWeightInGrams([], []), /*units*/AppState.getInstance().getAppSettings().units());
        }

        public getCostInUSDP(visitedGearItems: number[], visitedMeals: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let costInUSDP = 0;
            for(let i=0; i<this._gearCollections.length; ++i) {
                const gearCollectionEntry = this._gearCollections[i];
                costInUSDP += gearCollectionEntry.getCostInUSDP(visitedGearItems);
            }

            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystemEntry = this._gearSystems[i];
                costInUSDP += gearSystemEntry.getCostInUSDP(visitedGearItems);
            }

            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItemEntry = this._gearItems[i];
                if(_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                    continue;
                }

                visitedGearItems.push(gearItemEntry.getGearItemId());
                costInUSDP += gearItemEntry.getCostInUSDP();
            }

            for(let i=0; i<this._meals.length; ++i) {
                const mealEntry = this._meals[i];
                if(_.contains(visitedMeals, mealEntry.getMealId())) {
                    continue;
                }

                visitedMeals.push(mealEntry.getMealId());
                costInUSDP += mealEntry.getCostInUSDP();
            }
            return costInUSDP;
        }

        public getCostInCurrency(/*currency: string*/) {
            return convertUSDPToCurrency(this.getCostInUSDP([], []), /*currency*/AppState.getInstance().getAppSettings().currency());
        }

        public getCostPerUnitInCurrency() {
            const weightInUnits = convertGramsToUnits(this.getWeightInGrams([], []), AppState.getInstance().getAppSettings().units());
            const costInCurrency = convertUSDPToCurrency(this.getCostInUSDP([], []), AppState.getInstance().getAppSettings().currency());

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        /* Load/Save */

        public update(tripPlan: TripPlan) {
            this._name = tripPlan._name;
            this._startDate = this._startDate;
            this._endDate = this._endDate;
            this._tripItineraryId = tripPlan._tripItineraryId;
            this._note = tripPlan._note;

            this._gearCollections = <Array<Models.Gear.GearCollectionEntry>>[];
            for(let i=0; i<tripPlan._gearCollections.length; ++i) {
                const gearCollectionEntry = tripPlan._gearCollections[i];
                this.addGearCollectionEntry(gearCollectionEntry.getGearCollectionId(), gearCollectionEntry.count());
            }

            this._gearSystems = <Array<Models.Gear.GearSystemEntry>>[];
            for(let i=0; i<tripPlan._gearSystems.length; ++i) {
                const gearSystemEntry = tripPlan._gearSystems[i];
                this.addGearSystemEntry(gearSystemEntry.getGearSystemId(), gearSystemEntry.count());
            }

            this._gearItems = <Array<Models.Gear.GearItemEntry>>[];
            for(let i=0; i<tripPlan._gearItems.length; ++i) {
                const gearItemEntry = tripPlan._gearItems[i];
                this.addGearItemEntry(gearItemEntry.getGearItemId(), gearItemEntry.count());
            }

            this._meals = <Array<Models.Meals.MealEntry>>[];
            for(let i=0; i<tripPlan._meals.length; ++i) {
                const mealEntry = tripPlan._meals[i];
                this.addMealEntry(mealEntry.getMealId(), mealEntry.count());
            }
        }

        public loadFromDevice($q: ng.IQService, tripPlanResource: Resources.Trips.ITripPlanResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this._id = tripPlanResource.Id;
            this._name = tripPlanResource.Name;
            this._startDate = new Date(tripPlanResource.StartDate);
            this._endDate = new Date(tripPlanResource.EndDate);
            this._tripItineraryId = tripPlanResource.TripItineraryId;
            this._note = tripPlanResource.Note;

            for(let i=0; i<tripPlanResource.GearCollections.length; ++i) {
                const gearCollectionEntry = tripPlanResource.GearCollections[i];
                this.addGearCollectionEntry(gearCollectionEntry.GearCollectionId, gearCollectionEntry.Count);
            }

            for(let i=0; i<tripPlanResource.GearSystems.length; ++i) {
                const gearSystemEntry = tripPlanResource.GearSystems[i];
                this.addGearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count);
            }

            for(let i=0; i<tripPlanResource.GearItems.length; ++i) {
                const gearItemEntry = tripPlanResource.GearItems[i];
                this.addGearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count);
            }

            for(let i=0; i<tripPlanResource.Meals.length; ++i) {
                const mealEntry = tripPlanResource.Meals[i];
                this.addMealEntry(mealEntry.MealId, mealEntry.Count);
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
