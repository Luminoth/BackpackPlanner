///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../../Resources/Gear/GearCollectionResource.ts"/>

///<reference path="../../AppState.ts"/>

///<reference path="../Entry.ts"/>
///<reference path="GearItem.ts"/>
///<reference path="GearSystem.ts"/>

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export interface IGearCollection {
        Id: number;
        Name: string;
        Note: string;

        GearSystems: IGearSystemEntry[];
        GearItems: IGearItemEntry[];
    }

    export class GearCollection implements IGearCollection {
        public Id = -1;
        public Name = "";
        public Note = "";

        public GearSystems = <Array<GearSystemEntry>>[];
        public GearItems = <Array<GearItemEntry>>[];

        public getTotalGearItemCount() {
            let count = 0;
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

        public containsGearSystem(gearSystem: GearSystem) {
            return this.getGearSystemEntryIndexById(gearSystem.Id) >= 0;
        }

        public addGearSystem(gearSystem: GearSystem) {
            if(this.containsGearSystem(gearSystem)) {
                return;
            }
            this.GearSystems.push(new GearSystemEntry(gearSystem.Id));
        }

        public removeGearSystem(gearSystem: GearSystem) {
            const idx = this.getGearSystemEntryIndexById(gearSystem.Id);
            if(idx < 0) {
                return;
            }
            this.GearSystems.splice(idx, 1);
        }

        public removeAllGearSystems() {
            this.GearSystems = <Array<GearSystemEntry>>[];
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

        public containsGearItem(gearItem: GearItem) {
            return this.getGearItemEntryIndexById(gearItem.Id) >= 0;
        }

        public addGearItem(gearItem: GearItem) {
            if(this.containsGearItem(gearItem)) {
                return;
            }
            this.GearItems.push(new GearItemEntry(gearItem.Id));
        }

        public removeGearItem(gearItem: GearItem) {
            const idx = this.getGearItemEntryIndexById(gearItem.Id);
            if(idx < 0) {
                return;
            }
            this.GearItems.splice(idx, 1);
        }

        public removeAllGearItems() {
            this.GearItems = <Array<GearItemEntry>>[];
        }

        /* Pack List */

        public getPackedGearItemCount() {
            let count = 0;
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

        public getPackList() {
            let entries = <Array<GearItemEntry>>[];
            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystems[i].GearSystemId);
                entries = entries.concat(gearSystem.getPackList());
            }

            for(let i=0; i<this.GearItems.length; ++i) {
                entries.push(this.GearItems[i]);
            }
            return entries;
        }

        /* Weight/Cost */

        public getWeightInGrams() {
            let weightInGrams = 0;
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

        public update(gearCollection: GearCollection) {
            this.Name = gearCollection.Name;
            this.Note = gearCollection.Note;

            this.GearSystems = <Array<GearSystemEntry>>[];
            for(let i=0; i<gearCollection.GearSystems.length; ++i) {
                const gearSystemEntry = gearCollection.GearSystems[i];
                this.GearSystems.push(new GearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count, gearSystemEntry.IsPacked));
            }

            this.GearItems = <Array<GearItemEntry>>[];
            for(let i=0; i<gearCollection.GearItems.length; ++i) {
                const gearItemEntry = gearCollection.GearItems[i];
                this.GearItems.push(new GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
            }
        }

        public loadFromDevice($q: ng.IQService, gearCollectionResource: Resources.Gear.IGearCollectionResource) : ng.IPromise<any> {
            this.Id = gearCollectionResource.Id;
            this.Name = gearCollectionResource.Name;
            this.Note = gearCollectionResource.Note;

            for(let i=0; i<gearCollectionResource.GearSystems.length; ++i) {
                const gearSystemEntry = gearCollectionResource.GearSystems[i];
                this.GearSystems.push(new GearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count, gearSystemEntry.IsPacked));
            }

            for(let i=0; i<gearCollectionResource.GearItems.length; ++i) {
                const gearItemEntry = gearCollectionResource.GearItems[i];
                this.GearItems.push(new GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
            }

            return $q.defer().promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("GearCollection.saveToDevice");
            return $q.defer().promise;
        }
    }

    export interface IGearCollectionEntry extends IEntry {
        GearCollectionId: number;
    }

    export class GearCollectionEntry implements IGearCollectionEntry {
        public GearCollectionId = -1;
        public Count = 1;
        public IsPacked = false;

        constructor(gearCollectionId: number, count?: number, isPacked?: boolean) {
            this.GearCollectionId = gearCollectionId;

            if(count) {
                this.Count = count;
            }

            if(isPacked) {
                this.IsPacked = isPacked;
            }
        }

        public getName() : string {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
            if(!gearCollection) {
                return "";
            }
            return gearCollection.Name;
        }

        public getTotalGearItemCount() : number {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this.Count * gearCollection.getTotalGearItemCount();
        }

        public getGearSystemCount() : number {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this.Count * gearCollection.getGearSystemCount();
        }

        public getGearItemCount() : number {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this.Count * gearCollection.getGearItemCount();
        }

        public getWeightInGrams() {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this.Count * gearCollection.getWeightInGrams();
        }

        public getWeightInUnits() : number {
            return parseFloat(convertGramsToUnits(this.getWeightInGrams(), AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public getCostInUSDP() {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this.Count * gearCollection.getCostInUSDP();
        }

        public getCostInCurrency() {
            return convertUSDPToCurrency(this.getCostInUSDP(), AppState.getInstance().getAppSettings().Currency);
        }
    }
}
