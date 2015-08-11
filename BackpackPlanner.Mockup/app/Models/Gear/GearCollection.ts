///<reference path="../../Resources/Gear/GearCollectionResource.ts"/>

///<reference path="../../AppState.ts"/>

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

        constructor(gearCollectionResource?: Resources.Gear.IGearCollectionResource) {
            if(gearCollectionResource) {
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
            }
        }

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

        public getGearSystemCount() {
            let count = 0;
            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearItems[i];
                count += gearSystemEntry.Count;
            }
            return count;
        }

        public getGearItemCount() {
            let count = 0;
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                count += gearItemEntry.Count;
            }
            return count;
        }

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

        public getCostPerGramInUSDP() {
            const costInUSDP = this.getCostInUSDP();
            const weightInGrams = this.getWeightInGrams();

            return 0 == weightInGrams
                ? costInUSDP
                : costInUSDP / weightInGrams;
        }

        public getCostPerUnitInCurrency() {
            const costInCurrency = convertUSDPToCurrency(this.getCostInUSDP(), AppState.getInstance().getAppSettings().Currency);
            const weightInUnits = convertGramsToUnits(this.getWeightInGrams(), AppState.getInstance().getAppSettings().Units);

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        public getGearSystemEntryIndexById(gearSystemId: number) : number {
            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearSystems[i];
                if(gearSystemEntry.GearSystemId == gearSystemId) {
                    return i;
                }
            }
            return -1;
        }

        public getGearSystemEntryById(gearSystemId: number) : GearSystemEntry {
            const idx = this.getGearSystemEntryIndexById(gearSystemId);
            return idx < 0 ? null : this.GearSystems[idx];
        }

        // TODO: add/remove system entries

        public getGearItemEntryIndexById(gearItemId: number) : number {
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                if(gearItemEntry.GearItemId == gearItemId) {
                    return i;
                }
            }
            return -1;
        }

        public getGearItemEntryById(gearItemId: number) : GearItemEntry {
            const idx = this.getGearItemEntryIndexById(gearItemId);
            return idx < 0 ? null : this.GearItems[idx];
        }

        // TODO: add/remove item entries
    }

    export interface IGearCollectionEntry {
        GearCollectionId: number;
        Count: number;
        IsPacked: boolean;
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

        public getCostInUSDP() {
            const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
            if(!gearCollection) {
                return 0;
            }
            return this.Count * gearCollection.getCostInUSDP();
        }
    }
}
