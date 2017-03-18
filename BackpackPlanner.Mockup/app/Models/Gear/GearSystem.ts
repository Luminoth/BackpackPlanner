/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/underscore/underscore.d.ts" />

/// <reference path="../../Resources/Gear/GearSystemResource.ts"/>

/// <reference path="../../AppState.ts"/>

/// <reference path="../Entry.ts"/>
/// <reference path="GearItem.ts"/>

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export class GearSystem {
        private _id = -1;
        private _name = "";
        private _note = "";

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
            return undefined != _.find(this._gearItems, (gearSystemEntry) => {
                    return gearSystemEntry.getGearItemId() == gearItemId;
                }
            );
        }

        public addGearItem(gearItem: GearItem) {
            if(this.containsGearItemById(gearItem.Id)) {
                throw "The system already contains this item!";
            }

            this._gearItems.push(new GearItemEntry(gearItem.Id));
        }

        private addGearItemEntry(gearItemId: number, count: number) {
            if(this.containsGearItemById(gearItemId)) {
                throw "The system already contains this item!";
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
            return parseFloat(convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getBaseWeightInGrams(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let weightInGrams = 0;
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
            return parseFloat(convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getPackWeightInGrams(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let weightInGrams = 0;
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
            return parseFloat(convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getSkinOutWeightInGrams(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            const packWeightInGrams = this.getPackWeightInGrams(visitedGearItems);

            let weightInGrams = 0;
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
            return packWeightInGrams + weightInGrams;
        }

        public getSkinOutWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getCostInUSDP(visitedGearItems: number[]) {
            if(!visitedGearItems) {
                visitedGearItems = <Array<number>>[];
            }

            let costInUSDP = 0;
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

        public update(gearSystem: GearSystem) {
            this._name = gearSystem._name;
            this._note = gearSystem._note;

            this._gearItems = <Array<GearItemEntry>>[];
            for(let i=0; i<gearSystem._gearItems.length; ++i) {
                const gearItemEntry = gearSystem._gearItems[i];
                try {
                    this.addGearItemEntry(gearItemEntry.getGearItemId(), gearItemEntry.count());
                } catch(error) {
                }
            }
        }

        public loadFromDevice($q: ng.IQService, gearSystemResource: Resources.Gear.IGearSystemResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this._id = gearSystemResource.Id;
            this._name = gearSystemResource.Name;
            this._note = gearSystemResource.Note;

            for(let i=0; i<gearSystemResource.GearItems.length; ++i) {
                const gearItemEntry = gearSystemResource.GearItems[i];
                try {
                    this.addGearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count);
                } catch(error) {
                }
            }

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("GearSystem.saveToDevice");
            return $q.defer().promise;
        }
    }

    export class GearSystemEntry implements IEntry {
        private _gearSystemId = -1;
        private _count = 1;

        constructor(gearSystemId: number, count?: number) {
            this._gearSystemId = gearSystemId;
            
            if(count) {
                this._count = count;
            }
        }

        public getGearSystemId() {
            return this._gearSystemId;
        }

        public count(count?: number) {
            return arguments.length
                ? (this._count = count)
                : this._count;
        }

        public getName() {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
            if(!gearSystem) {
                return "";
            }
            return gearSystem.name();
        }

        public getGearItemCount(visitedGearItems: number[]) {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this._count * gearSystem.getGearItemCount(visitedGearItems);
        }

        public getTotalWeightInGrams(visitedGearItems: number[]) {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this._count * gearSystem.getTotalWeightInGrams(visitedGearItems);
        }

        public getTotalWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getBaseWeightInGrams(visitedGearItems: number[]) {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this._count * gearSystem.getBaseWeightInGrams(visitedGearItems);
        }

        public getBaseWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getPackWeightInGrams(visitedGearItems: number[]) {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this._count * gearSystem.getPackWeightInGrams(visitedGearItems);
        }

        public getPackWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getSkinOutWeightInGrams(visitedGearItems: number[]) {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this._count * gearSystem.getSkinOutWeightInGrams(visitedGearItems);
        }

        public getSkinOutWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getCostInUSDP(visitedGearItems: number[]) {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this._count * gearSystem.getCostInUSDP(visitedGearItems);
        }

        public getCostInCurrency(/*currency: string*/) {
            return convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/AppState.getInstance().getAppSettings().currency());
        }
    }
}
