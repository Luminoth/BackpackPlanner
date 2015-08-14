///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../../Resources/Gear/GearItemResource.ts"/>

///<reference path="../../AppState.ts"/>

///<reference path="../Entry.ts"/>

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
        public ConsumedPerDay = 1;
        public Note = "";

        /* Weight/Cost */

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

        public getCostPerUnitInCurrency() {
            const costInCurrency = convertUSDPToCurrency(this.CostInUSDP, AppState.getInstance().getAppSettings().Currency);
            const weightInUnits = convertGramsToUnits(this.WeightInGrams, AppState.getInstance().getAppSettings().Units);

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        /* Load/Save */

        public update(gearItem: GearItem) {
            this.Name = gearItem.Name;
            this.Url = gearItem.Url;
            this.Make = gearItem.Make;
            this.Model = gearItem.Model;
            this.Carried = gearItem.Carried;
            this.WeightInGrams = gearItem.WeightInGrams;
            this.CostInUSDP = gearItem.CostInUSDP;
            this.IsConsumable = gearItem.IsConsumable;
            this.ConsumedPerDay = gearItem.ConsumedPerDay;
            this.Note = gearItem.Note;
        }

        public loadFromDevice($q: ng.IQService, gearItemResource: Resources.Gear.IGearItemResource) : ng.IPromise<any> {
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

            return $q.defer().promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("GearItem.saveToDevice");
            return $q.defer().promise;
        }
    }

    export interface IGearItemEntry extends IEntry {
        GearItemId: number;
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

        public getName() : string {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
            if(!gearItem) {
                return "";
            }
            return gearItem.Name;
        }

        public getWeightInGrams() : number {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
            if(!gearItem) {
                return 0;
            }
            return this.Count * gearItem.WeightInGrams;
        }

        public getWeightInUnits() : number {
            return parseFloat(convertGramsToUnits(this.getWeightInGrams(), AppState.getInstance().getAppSettings().Units).toFixed(2));
        }

        public getCostInUSDP() {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
            if(!gearItem) {
                return 0;
            }
            return this.Count * gearItem.CostInUSDP;
        }

        public getCostInCurrency() {
            return convertUSDPToCurrency(this.getCostInUSDP(), AppState.getInstance().getAppSettings().Currency);
        }
    }
}
