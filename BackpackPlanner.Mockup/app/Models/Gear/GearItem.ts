///<reference path="../../Resources/Gear/GearItemResource.ts"/>

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export enum GearCarried {
        NotCarried,
        Carried,
        Worn
    }

    export interface IGearItem {
        Id: number;
        Name: string;
        Url: string;
        Make: string;
        Model: string;
        Carried: GearCarried;
        WeightInOunces: number;
        CostInUSD: number;
        IsConsumable: boolean;
        ConsumedPerDay: number;
        Note: string;
    }

    export class GearItem implements IGearItem {
        public Id = -1;
        public Name = "";
        public Url = "";
        public Make = "";
        public Model = "";
        public Carried = GearCarried.Carried;
        public WeightInOunces = 0;
        public CostInUSD = 0;
        public IsConsumable = false;
        public ConsumedPerDay = 0;
        public Note = "";

        constructor(gearItemResource?: Resources.Gear.IGearItemResource) {
            if(gearItemResource) {
                this.Id = gearItemResource.Id;
                this.Name = gearItemResource.Name;
                this.Url = gearItemResource.Url;
                this.Make = gearItemResource.Make;
                this.Model = gearItemResource.Model;
                this.Carried = gearItemResource.Carried;
                this.WeightInOunces = gearItemResource.WeightInOunces;
                this.CostInUSD = gearItemResource.CostInUSD;
                this.IsConsumable = gearItemResource.IsConsumable;
                this.ConsumedPerDay = gearItemResource.ConsumedPerDay;
                this.Note = gearItemResource.Note;
            }
        }

        public CarriedAsString() : string {
            return GearCarried[this.Carried];
        }
    }

    export interface IGearItemEntry {
        GearItemId: number;
        Count: number;
        IsPacked: boolean;
    }

    export class GearItemEntry implements IGearItemEntry {
        public GearItemId = -1;
        public Count = 1;
        public IsPacked = false;

        constructor(gearItemId: number) {
            this.GearItemId = gearItemId;
        }
    }
}
