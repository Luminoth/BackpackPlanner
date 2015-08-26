///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/underscore/underscore.d.ts" />

///<reference path="../../Resources/Gear/GearCollectionResource.ts"/>

///<reference path="../../AppState.ts"/>

///<reference path="../Entry.ts"/>
///<reference path="GearItem.ts"/>
///<reference path="GearSystem.ts"/>

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export class GearCollection {
        private _id = -1;
        private _name = "";
        private _note = "";

        private _gearSystems = <Array<GearSystemEntry>>[];
        private _gearItems = <Array<GearItemEntry>>[];

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

        public note(note?: string) {
            return arguments.length
                ? (this._note = note)
                : this._note;
        }

        public getTotalGearItemCount() {
            const visitedGearItems = <Array<number>>[];

            let count = 0;
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
            return undefined != _.find(this._gearSystems, (gearSystemEntry) => {
                    return gearSystemEntry.getGearSystemId() == gearSystemId;
                }
            );
        }

        public containsGearSystemItems(gearSystem: GearSystem) {
            const gearItems = gearSystem.getGearItems();
            for(let i=0; i<gearItems.length; ++i) {
                const gearItemEntry = gearItems[i];
                if(this.containsGearItemById(gearItemEntry.getGearItemId())) {
                    return true;
                }
            }
            return false;
        }

        public addGearSystem(gearSystem: GearSystem) {
            if(this.containsGearSystemById(gearSystem.Id)) {
                throw "The collection already contains this system!";
            }

            if(this.containsGearSystemItems(gearSystem)) {
                throw "The collection already contains items from this system!";
            }

            this._gearSystems.push(new GearSystemEntry(gearSystem.Id));
        }

        private addGearSystemEntry(gearSystemId: number, count: number) {
            if(this.containsGearSystemById(gearSystemId)) {
                throw "The collection already contains this system!";
            }

            // TODO: prevent duplicates here
            /*const gearSystem = AppState.getInstance().getGearState().getGearSystemById(gearSystemId);
            if(!gearSystem) {
                throw "The system does not exist!";
            }

            if(this.containsGearSystemItems(gearSystem)) {
                throw "The collection already contains items from this system!";
            }*/
            
            this._gearSystems.push(new GearSystemEntry(gearSystemId, count));
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
            this._gearSystems = <Array<GearSystemEntry>>[];
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

        public addGearItem(gearItem: GearItem) {
            if(this.containsGearItemById(gearItem.Id)) {
                throw "The collection already contains this item!";
            }

            this._gearItems.push(new GearItemEntry(gearItem.Id));
        }

        private addGearItemEntry(gearItemId: number, count: number) {
            if(this.containsGearItemById(gearItemId)) {
                throw "The collection already contains this item!";
            }

            this._gearItems.push(new GearItemEntry(gearItemId, count));
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
            this._gearItems = <Array<GearItemEntry>>[];
        }

        /* Weight/Cost */

        public getTotalWeightInGrams(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let weightInGrams = 0;
            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystemEntry = this._gearSystems[i];
                weightInGrams += gearSystemEntry.getTotalWeightInGrams(visitedGearItems);
            }

            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItemEntry = this._gearItems[i];
                if(_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                    continue;
                }

                visitedGearItems.push(gearItemEntry.getGearItemId());
                weightInGrams += gearItemEntry.getTotalWeightInGrams();
            }
            return weightInGrams;
        }

        public getTotalWeightInUnits(/*units: string*/) {
            return convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units());
        }

        public getBaseWeightInGrams(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let weightInGrams = 0;
            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystemEntry = this._gearSystems[i];
                weightInGrams += gearSystemEntry.getBaseWeightInGrams(visitedGearItems);
            }

            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItemEntry = this._gearItems[i];
                if(_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                    continue;
                }

                // carried but not worn or consumable
                if(gearItemEntry.isCarried() && !gearItemEntry.isWorn() && !gearItemEntry.isConsumable()) {
                    visitedGearItems.push(gearItemEntry.getGearItemId());
                    weightInGrams += gearItemEntry.getTotalWeightInGrams();
                }
            }
            return weightInGrams;
        }

        public getBaseWeightInUnits(/*units: string*/) {
            return convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units());
        }

        public getPackWeightInGrams(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let weightInGrams = 0;
            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystemEntry = this._gearSystems[i];
                weightInGrams += gearSystemEntry.getPackWeightInGrams(visitedGearItems);
            }

            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItemEntry = this._gearItems[i];
                if(_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                    continue;
                }

                // carried or consumable but not worn
                if(gearItemEntry.isCarried() && !gearItemEntry.isWorn() || gearItemEntry.isConsumable()) {
                    visitedGearItems.push(gearItemEntry.getGearItemId());
                    weightInGrams += gearItemEntry.getTotalWeightInGrams();
                }
            }
            return weightInGrams;
        }

        public getPackWeightInUnits(/*units: string*/) {
            return convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units());
        }

        public getSkinOutWeightInGrams(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let weightInGrams = 0;
            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystemEntry = this._gearSystems[i];
                weightInGrams += gearSystemEntry.getSkinOutWeightInGrams(visitedGearItems);
            }

            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItemEntry = this._gearItems[i];
                if(_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                    continue;
                }

                // carried, worn, and consumable gear items
                if(gearItemEntry.isCarried()) {
                    visitedGearItems.push(gearItemEntry.getGearItemId());
                    weightInGrams += gearItemEntry.getTotalWeightInGrams();
                }
            }
            return weightInGrams;
        }

        public getSkinOutWeightInUnits(/*units: string*/) {
            return convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units());
        }

        public getCostInUSDP(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let costInUSDP = 0;
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
            return costInUSDP;
        }

        public getCostInCurrency(/*currency: string*/) {
            return convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/AppState.getInstance().getAppSettings().currency());
        }

        public getCostPerUnitInCurrency(/*units: string, currency: string*/) {
            const weightInUnits = convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units());
            const costInCurrency = convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/AppState.getInstance().getAppSettings().currency());

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        /* Load/Save */

        public update(gearCollection: GearCollection) {
            this._name = gearCollection._name;
            this._note = gearCollection._note;

            this._gearSystems = <Array<GearSystemEntry>>[];
            for(let i=0; i<gearCollection._gearSystems.length; ++i) {
                const gearSystemEntry = gearCollection._gearSystems[i];
                try {
                    this.addGearSystemEntry(gearSystemEntry.getGearSystemId(), gearSystemEntry.count());
                } catch(error) {
                }
            }

            this._gearItems = <Array<GearItemEntry>>[];
            for(let i=0; i<gearCollection._gearItems.length; ++i) {
                const gearItemEntry = gearCollection._gearItems[i];
                try {
                    this.addGearItemEntry(gearItemEntry.getGearItemId(), gearItemEntry.count());
                } catch(error) {
                }
            }
        }

        public loadFromDevice($q: ng.IQService, gearCollectionResource: Resources.Gear.IGearCollectionResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this._id = gearCollectionResource.Id;
            this._name = gearCollectionResource.Name;
            this._note = gearCollectionResource.Note;

            for(let i=0; i<gearCollectionResource.GearSystems.length; ++i) {
                const gearSystemEntry = gearCollectionResource.GearSystems[i];
                try {
                    this.addGearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count);
                } catch(error) {
                }
            }

            for(let i=0; i<gearCollectionResource.GearItems.length; ++i) {
                const gearItemEntry = gearCollectionResource.GearItems[i];
                try {
                    this.addGearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count);
                } catch(error) {
                }
            }

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("GearCollection.saveToDevice");
            return $q.defer().promise;
        }
    }

    export class GearCollectionEntry implements IEntry {
        private _gearCollectionId = -1;
        private _count = 1;

        constructor(gearCollectionId: number, count?: number) {
            this._gearCollectionId = gearCollectionId;

            if(count) {
                this._count = count;
            }
        }

        public getGearCollectionId() {
            return this._gearCollectionId;
        }

        public count(count?: number) {
            return arguments.length
                ? (this._count = count)
                : this._count;
        }

        public getName() {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
            if(!gearCollection) {
                return "";
            }
            return gearCollection.name();
        }

        public getTotalGearItemCount() {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this._count * gearCollection.getTotalGearItemCount();
        }

        public getGearSystemCount(visitedGearSystems: number[]) {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this._count * gearCollection.getGearSystemCount(visitedGearSystems);
        }

        public getGearItemCount(visitedGearItems: number[]) {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this._count * gearCollection.getGearItemCount(visitedGearItems);
        }

        public getTotalWeightInGrams(visitedGearItems: number[]) {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this._count * gearCollection.getTotalWeightInGrams(visitedGearItems);
        }

        public getTotalWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getBaseWeightInGrams(visitedGearItems: number[]) {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this._count * gearCollection.getBaseWeightInGrams(visitedGearItems);
        }

        public getBaseWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getPackWeightInGrams(visitedGearItems: number[]) {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this._count * gearCollection.getPackWeightInGrams(visitedGearItems);
        }

        public getPackWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getSkinOutWeightInGrams(visitedGearItems: number[]) {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this._count * gearCollection.getSkinOutWeightInGrams(visitedGearItems);
        }

        public getSkinOutWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getCostInUSDP(visitedGearItems: number[]) {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this._count * gearCollection.getCostInUSDP(visitedGearItems);
        }

        public getCostInCurrency(/*currency: string*/) {
            return convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/AppState.getInstance().getAppSettings().currency());
        }
    }
}
