///<reference path="../../AppManager.ts" />

///<reference path="../../Resources/Gear/GearCollectionResource.ts"/>

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

        public GearSystems = <Array<IGearSystemEntry>>[];
        public GearItems = <Array<IGearItemEntry>>[];

        constructor(gearCollectionResource?: Resources.Gear.IGearCollectionResource) {
            if(gearCollectionResource) {
                this.Id = gearCollectionResource.Id;
                this.Name = gearCollectionResource.Name;
                this.Note = gearCollectionResource.Note;

                this.GearSystems = gearCollectionResource.GearSystems;
                this.GearItems = gearCollectionResource.GearItems;
            }
        }

        public getTotalNumberOfItems() {
            let count = 0;
            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearSystems[i];
                const gearSystem = AppManager.getInstance().getGearSystemById(gearSystemEntry.GearSystemId);
                count += gearSystemEntry.Count * gearSystem.getNumberOfItems();
            }
            for(let i=0; i<this.GearItems.length; ++i) {
                const gearItemEntry = this.GearItems[i];
                count += gearItemEntry.Count;
            }
            return count;
        }

        public getNumberOfSystems() {
            let count = 0;
            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearItems[i];
                count += gearSystemEntry.Count;
            }
            return count;
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
            // TODO: calculate this
            return weightInOunces;
        }

        public getGearCollectionCostInUSD() {
            let costInUSD = 0;
            // TODO: calculate this
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

        public getGearSystemEntryIndexById(gearSystemId: number) : number {
            for(let i=0; i<this.GearSystems.length; ++i) {
                const gearSystemEntry = this.GearSystems[i];
                if(gearSystemEntry.GearSystemId == gearSystemId) {
                    return i;
                }
            }
            return -1;
        }

        public getGearSystemmEntryById(gearSystemId: number) : IGearSystemEntry {
            const idx = this.getGearSystemEntryIndexById(gearSystemId);
            return idx < 0 ? null : this.GearSystems[idx];
        }
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

        constructor(gearCollectionId: number) {
            this.GearCollectionId = gearCollectionId;
        }
    }
}
