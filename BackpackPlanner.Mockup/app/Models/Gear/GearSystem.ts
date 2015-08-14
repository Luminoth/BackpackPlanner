///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../../Resources/Gear/GearSystemResource.ts"/>

///<reference path="../../AppState.ts"/>

///<reference path="../Entry.ts"/>
///<reference path="GearItem.ts"/>

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export interface IGearSystem {
        Id: number;
        Name: string;
        Note: string;

        GearItems: IGearItemEntry[];
    }

    export class GearSystem implements IGearSystem {
        public Id = -1;
        public Name = "";
        public Note = "";

        public GearItems = <Array<GearItemEntry>>[];

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

        /* Pack List */

        public getPackedGearItemCount() {
            let count = 0;
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                if(gearItemEntry.IsPacked) {
                    ++count;
                }
            }
            return count;
        }

        public getPackList() {
            const entries = <Array<GearItemEntry>>[];
            for(let i=0; i<this.GearItems.length; ++i) {
                entries.push(this.GearItems[i]);
            }
            return entries;
        }

        /* Weight/Cost */

        public getWeightInGrams() {
            let weightInGrams = 0;
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                weightInGrams += gearItemEntry.getWeightInGrams();
            }
            return weightInGrams;
        }

        public getWeightInUnits() {
            return parseFloat(convertGramsToUnits(this.getWeightInGrams(), AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public getCostInUSDP() {
            let costInUSDP = 0;
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

        public loadFromDevice($q: ng.IQService, gearSystemResource: Resources.Gear.IGearSystemResource) : ng.IPromise<any> {
            this.Id = gearSystemResource.Id;
            this.Name = gearSystemResource.Name;
            this.Note = gearSystemResource.Note;

            for(let i=0; i<gearSystemResource.GearItems.length; ++i) {
                const gearItemEntry = gearSystemResource.GearItems[i];
                this.GearItems.push(new GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
            }

            return $q.defer().promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("GearSystem.saveToDevice");
            return $q.defer().promise;
        }
    }

    export interface IGearSystemEntry extends IEntry {
        GearSystemId: number;
    }

    export class GearSystemEntry implements IGearSystemEntry {
        public GearSystemId = -1;
        public Count = 1;
        public IsPacked = false;

        constructor(gearSystemId: number, count?: number, isPacked?: boolean) {
            this.GearSystemId = gearSystemId;
            
            if(count) {
                this.Count = count;
            }

            if(isPacked) {
                this.IsPacked = isPacked;
            }
        }

        public getName() : string {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
            if(!gearSystem) {
                return "";
            }
            return gearSystem.Name;
        }

        public getGearItemCount() : number {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this.Count * gearSystem.getGearItemCount();
        }

        public getWeightInGrams() : number {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this.Count * gearSystem.getWeightInGrams();
        }

        public getWeightInUnits() : number {
            return parseFloat(convertGramsToUnits(this.getWeightInGrams(), AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public getCostInUSDP() {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this.Count * gearSystem.getCostInUSDP();
        }

        public getCostInCurrency() {
            return convertUSDPToCurrency(this.getCostInUSDP(), AppState.getInstance().getAppSettings().Currency);
        }
    }
}
