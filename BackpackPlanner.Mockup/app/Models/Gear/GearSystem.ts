///<reference path="../../AppManager.ts" />

///<reference path="../../Resources/Gear/GearSystemResource.ts"/>

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

        public GearItems = <Array<IGearItemEntry>>[];

        constructor(gearSystemResource?: Resources.Gear.IGearSystemResource) {
            if(gearSystemResource) {
                this.Id = gearSystemResource.Id;
                this.Name = gearSystemResource.Name;
                this.Note = gearSystemResource.Note;

                this.GearItems = gearSystemResource.GearItems;
            }
        }

        public getNumberOfItems() {
            let count = 0;
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                count += gearItemEntry.Count;
            }
            return count;
        }

        public getWeightInOunces() {
            let weightInOunces = 0;
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                const gearItem = AppManager.getInstance().getGearItemById(gearItemEntry.GearItemId);
                if(null == gearItem) {
                    continue;
                }
                weightInOunces += gearItemEntry.Count * gearItem.WeightInOunces;
            }
            return weightInOunces;
        }

        public getCostInUSD() {
            let costInUSD = 0;
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                const gearItem = AppManager.getInstance().getGearItemById(gearItemEntry.GearItemId);
                if(null == gearItem) {
                    continue;
                }
                costInUSD += gearItemEntry.Count * gearItem.CostInUSD;
            }
            return costInUSD;
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

        public getGearItemEntryById(gearItemId: number) : IGearItemEntry {
            const idx = this.getGearItemEntryIndexById(gearItemId);
            return idx < 0 ? null : this.GearItems[idx];
        }
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

        constructor(gearSystemId: number) {
            this.GearSystemId = gearSystemId;
        }
    }
}
