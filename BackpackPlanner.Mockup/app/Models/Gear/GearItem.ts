///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

///<reference path="../../Resources/Gear/GearItemResource.ts"/>

///<reference path="../../AppState.ts"/>

///<reference path="../Entry.ts"/>

module BackpackPlanner.Mockup.Models.Gear {
    "use strict";

    export class GearItem {
        private _id = -1;
        private _name = "";
        private _url = "";
        private _make = "";
        private _model = "";
        private _carried = "Carried";
        private _weightInGrams = 0;
        private _costInUSDP = 0;
        private _isConsumable = false;
        private _consumedPerDay = 1;
        private _note = "";

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

        public url(url?: string) {
            return arguments.length
                ? (this._url = url)
                : this._url;
        }

        public make(make?: string) {
            return arguments.length
                ? (this._make = make)
                : this._make;
        }

        public model(model?: string) {
            return arguments.length
                ? (this._model = model)
                : this._model;
        }

        public carried(carried?: string) {
            return arguments.length
                ? (this._carried = carried)
                : this._carried;
        }

        public isCarried() {
            return "NotCarried" != this._carried;
        }

        public isWorn() {
            return "Worn" == this._carried;
        }

        public isConsumable(isConsumable?: boolean) {
            return arguments.length
                ? (this._isConsumable = isConsumable)
                : this._isConsumable;
        }

        public consumedPerDay(consumedPerDay?: number) {
            return arguments.length
                ? (this._consumedPerDay = consumedPerDay)
                : this._consumedPerDay;
        }

        public note(note?: string) {
            return arguments.length
                ? (this._note = note)
                : this._note;
        }

        /* Weight/Cost */

        public getWeightCategory() {
            if(!this.isCarried()) {
                return "None";
            }
            return AppState.getInstance().getAppSettings().getWeightCategory(this._weightInGrams);
        }

        public getWeightInGrams() {
            return this._weightInGrams;
        }

        public weightInUnits(weight?: number) {
            return arguments.length
                ? (this._weightInGrams = convertUnitsToGrams(weight, AppState.getInstance().getAppSettings().units()))
                : parseFloat(convertGramsToUnits(this._weightInGrams, AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getCostInUSDP() {
            return this._costInUSDP;
        }

        public costInCurrency(cost?: number) {
            return arguments.length
                ? (this._costInUSDP = convertCurrencyToUSDP(cost, AppState.getInstance().getAppSettings().currency()))
                : convertUSDPToCurrency(this._costInUSDP, AppState.getInstance().getAppSettings().currency());
        }

        public getCostPerUnitInCurrency(/*units: string, currency: string*/) {
            const weightInUnits = convertGramsToUnits(this._weightInGrams, /*units*/AppState.getInstance().getAppSettings().units());
            const costInCurrency = convertUSDPToCurrency(this._costInUSDP, /*currency*/AppState.getInstance().getAppSettings().currency());

            return 0 == weightInUnits
                ? costInCurrency
                : costInCurrency / weightInUnits;
        }

        /* Load/Save */

        public update(gearItem: GearItem) {
            this._name = gearItem._name;
            this._url = gearItem._url;
            this._make = gearItem._make;
            this._model = gearItem._model;
            this._carried = gearItem._carried;
            this._weightInGrams = gearItem._weightInGrams;
            this._costInUSDP = gearItem._costInUSDP;
            this._isConsumable = gearItem._isConsumable;
            this._consumedPerDay = gearItem._consumedPerDay;
            this._note = gearItem._note;
        }

        public loadFromDevice($q: ng.IQService, gearItemResource: Resources.Gear.IGearItemResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this._id = gearItemResource.Id;
            this._name = gearItemResource.Name;
            this._url = gearItemResource.Url;
            this._make = gearItemResource.Make;
            this._model = gearItemResource.Model;
            this._carried = gearItemResource.Carried;
            this._weightInGrams = gearItemResource.WeightInGrams;
            this._costInUSDP = gearItemResource.CostInUSDP;
            this._isConsumable = gearItemResource.IsConsumable;
            this._consumedPerDay = gearItemResource.ConsumedPerDay;
            this._note = gearItemResource.Note;

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("GearItem.saveToDevice");
            return $q.defer().promise;
        }
    }

    export class GearItemEntry implements IEntry {
        private _gearItemId = -1;
        private _count = 1;

        constructor(gearItemId: number, count?: number) {
            this._gearItemId = gearItemId;

            if(count) {
                this._count = count;
            }
        }

        public getGearItemId() {
            return this._gearItemId;
        }

        public count(count?: number) {
            return arguments.length
                ? (this._count = count)
                : this._count;
        }

        public getName() {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
            if(!gearItem) {
                return "";
            }
            return gearItem.name();
        }

        public isCarried() {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
            if(!gearItem) {
                return false;
            }
            return gearItem.isCarried();
        }

        public isWorn() {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
            if(!gearItem) {
                return false;
            }
            return gearItem.isWorn();
        }

        public isConsumable() {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
            if(!gearItem) {
                return false;
            }
            return gearItem.isConsumable();
        }

        public getTotalWeightInGrams() {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
            if(!gearItem) {
                return 0;
            }
            return this._count * gearItem.getWeightInGrams();
        }

        public getTotalWeightInUnits(/*units: string*/) {
            return parseFloat(convertGramsToUnits(this.getTotalWeightInGrams(), /*units*/AppState.getInstance().getAppSettings().units()).toFixed(2));
        }

        public getCostInUSDP() {
            const gearItem = AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
            if(!gearItem) {
                return 0;
            }
            return this._count * gearItem.getCostInUSDP();
        }

        public getCostInCurrency(/*currency: string*/) {
            return convertUSDPToCurrency(this.getCostInUSDP(), /*currency*/AppState.getInstance().getAppSettings().currency());
        }
    }
}
