///<reference path="../../Resources/Gear/GearSystemResource.ts"/>

///<reference path="../../AppState.ts"/>

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

        constructor(gearSystemResource?: Resources.Gear.IGearSystemResource) {
            if(gearSystemResource) {
                this.Id = gearSystemResource.Id;
                this.Name = gearSystemResource.Name;
                this.Note = gearSystemResource.Note;

                for(let i=0; i<gearSystemResource.GearItems.length; ++i) {
                    const gearItemEntry = gearSystemResource.GearItems[i];
                    this.GearItems.push(new GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
                }
            }
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

    export interface IGearSystemEntry {
        GearSystemId: number;
        Count: number;
        IsPacked: boolean;
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

        public getGearItemCount() : number {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this.Count * gearSystem.getGearItemCount();
        }

        public getWeightInGrams() {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this.Count * gearSystem.getWeightInGrams();
        }

        public getCostInUSDP() {
            const gearSystem = AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
            if(!gearSystem) {
                return 0;
            }
            return this.Count * gearSystem.getCostInUSDP();
        }
    }
}
