///<reference path="../../Resources/Gear/GearItemResource.ts"/>

///<reference path="../../AppState.ts"/>

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export interface IGearItem {
        Id: number;
        Name: string;
        Url: string;
        Make: string;
        Model: string;
        Carried: string;
        WeightInGrams: number;
        CostInUSDP: number;
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
        public Carried = "Carried";
        public WeightInGrams = 0;
        public CostInUSDP = 0;
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
                this.WeightInGrams = gearItemResource.WeightInGrams;
                this.CostInUSDP = gearItemResource.CostInUSDP;
                this.IsConsumable = gearItemResource.IsConsumable;
                this.ConsumedPerDay = gearItemResource.ConsumedPerDay;
                this.Note = gearItemResource.Note;
            }
        }

        public getCostPerGramInUSDP() {
            return 0 == this.WeightInGrams
                ? this.CostInUSDP
                : this.CostInUSDP / this.WeightInGrams;
        }

        public getCostPerUnitInCurrency() {
            const costInCurrency = convertUSDPToCurrency(this.CostInUSDP, AppState.getInstance().getAppSettings().Currency);
            const weightInUnits = convertGramsToUnits(this.WeightInGrams, AppState.getInstance().getAppSettings().Units);

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        public weightInUnits(weight: number) : number {
            return arguments.length
                ? (this.WeightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().Units))
                : parseFloat(convertGramsToUnits(this.WeightInGrams, AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public costInCurrency(cost: number) : number {
            return arguments.length
                ? (this.CostInUSDP = convertCurrencyToUSDP(cost, AppState.getInstance().getAppSettings().Currency))
                : convertUSDPToCurrency(this.CostInUSDP, AppState.getInstance().getAppSettings().Currency);
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

        constructor(gearItemId: number, count?: number, isPacked?: boolean) {
            this.GearItemId = gearItemId;

            if(count) {
                this.Count = count;
            }

            if(isPacked) {
                this.IsPacked = isPacked;
            }
        }

        public getWeightInGrams() {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
            if(!gearItem) {
                return 0;
            }
            return this.Count * gearItem.WeightInGrams;
        }

        public getCostInUSDP() {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
            if(!gearItem) {
                return 0;
            }
            return this.Count * gearItem.CostInUSDP;
        }
    }
}
