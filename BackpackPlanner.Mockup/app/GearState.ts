///<reference path="../scripts/typings/angularjs/angular.d.ts" />

///<reference path="Models/Gear/GearCollection.ts" />
///<reference path="Models/Gear/GearItem.ts" />
///<reference path="Models/Gear/GearSystem.ts" />

///<reference path="Resources/Gear/GearCollectionResource.ts" />
///<reference path="Resources/Gear/GearItemResource.ts" />
///<reference path="Resources/Gear/GearSystemResource.ts" />

///<reference path="Services/Gear/GearCollectionService.ts"/>
///<reference path="Services/Gear/GearItemService.ts"/>
///<reference path="Services/Gear/GearSystemService.ts"/>

module BackpackPlanner.Mockup {
    "use strict";

    export class GearState {
        /* Gear Items */

        private _gearItems: Models.Gear.GearItem[];

        // TODO: this should be a read-only collection
        public getGearItems() : Models.Gear.GearItem[] {
            return this._gearItems;
        }

        public getGearItemIndexById(gearItemId: number) : number {
            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItem = this._gearItems[i];
                if(gearItem.Id == gearItemId) {
                    return i;
                }
            }
            return -1;
        }

        public getGearItemById(gearItemId: number) : Models.Gear.GearItem {
            const idx = this.getGearItemIndexById(gearItemId);
            return idx < 0 ? null : this._gearItems[idx];
        }

        private getNextGearItemId() : number {
            // TODO: write this
            return -1;
        }

        public addGearItem(gearItem: Models.Gear.GearItem) : number {
            if(gearItem.Id < 0) {
                gearItem.Id = this.getNextGearItemId();
            }
            this._gearItems.push(gearItem);
            return gearItem.Id;
        }

        public deleteGearItem(gearItem: Models.Gear.GearItem) : boolean {
            const idx = this.getGearItemIndexById(gearItem.Id);
            if(idx < 0) {
                return false;
            }
            this._gearItems.splice(idx, 1);

            // TODO: remove the item from the systems, collections, and trip plans it belongs to

            return true;
        }

        public deleteAllGearItems() : void {
            this._gearItems = <Array<Models.Gear.GearItem>>[];
        }

        /* Gear Systems */

        private _gearSystems: Models.Gear.GearSystem[];

        // TODO: this should be a read-only collection
        public getGearSystems() : Models.Gear.GearSystem[] {
            return this._gearSystems;
        }

        public getGearSystemIndexById(gearSystemId: number) : number {
            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystem = this._gearSystems[i];
                if(gearSystem.Id == gearSystemId) {
                    return i;
                }
            }
            return -1;
        }

        public getGearSystemById(gearSystemId: number) : Models.Gear.GearSystem {
            const idx = this.getGearSystemIndexById(gearSystemId);
            return idx < 0 ? null : this._gearSystems[idx];
        }

        private getNextGearSystemId() : number {
            // TODO: write this
            return -1;
        }

        public addGearSystem(gearSystem: Models.Gear.GearSystem) : number {
            if(gearSystem.Id < 0) {
                gearSystem.Id = this.getNextGearSystemId();
            }
            this._gearSystems.push(gearSystem);
            return gearSystem.Id;
        }

        public deleteGearSystem(gearSystem: Models.Gear.GearSystem) : boolean {
            const idx = this.getGearSystemIndexById(gearSystem.Id);
            if(idx < 0) {
                return false;
            }
            this._gearSystems.splice(idx, 1);

            // TODO: remove the system from the collections, and trip plans it belongs to

            return true;
        }

        public deleteAllGearSystems() : void {
            this._gearSystems = <Array<Models.Gear.GearSystem>>[];
        }

        /* Gear Collections */

        private _gearCollections: Models.Gear.GearCollection[];

        // TODO: this should be a read-only collection
        public getGearCollections() : Models.Gear.GearCollection[] {
            return this._gearCollections;
        }

        public getGearCollectionIndexById(gearCollectionId: number) : number {
            for(let i=0; i<this._gearCollections.length; ++i) {
                const gearCollection = this._gearCollections[i];
                if(gearCollection.Id == gearCollectionId) {
                    return i;
                }
            }
            return -1;
        }

        public getGearCollectionById(gearCollectionId: number) : Models.Gear.GearCollection {
            const idx = this.getGearCollectionIndexById(gearCollectionId);
            return idx < 0 ? null : this._gearCollections[idx];
        }

        private getNextGearCollectionId() : number {
            // TODO: write this
            return -1;
        }

        public addGearCollection(gearCollection: Models.Gear.GearCollection) : number {
            if(gearCollection.Id < 0) {
                gearCollection.Id = this.getNextGearCollectionId();
            }
            this._gearCollections.push(gearCollection);
            return gearCollection.Id;
        }

        public deleteGearCollection(gearCollection: Models.Gear.GearCollection) : boolean {
            const idx = this.getGearCollectionIndexById(gearCollection.Id);
            if(idx < 0) {
                return false;
            }
            this._gearCollections.splice(idx, 1);

            // TODO: remove the collection from the trip plans it belongs to

            return true;
        }

        public deleteAllGearCollections() : void {
            this._gearCollections = <Array<Models.Gear.GearCollection>>[];
        }

        /* Load/Save */

        private loadGearItems(gearItemResources: Resources.Gear.IGearItemResource[]) {
            if(this._gearItems) {
                throw new Error("Gear items already loaded!");
            }

            this._gearItems = <Array<Models.Gear.GearItem>>[];
            for(let i=0; i<gearItemResources.length; ++i) {
                this._gearItems.push(new Models.Gear.GearItem(gearItemResources[i]));
            }
        }

        private loadGearSystems(gearSystemResources: Resources.Gear.IGearSystemResource[]) {
            if(this._gearSystems) {
                throw new Error("Gear systems already loaded!");
            }

            this._gearSystems = <Array<Models.Gear.GearSystem>>[];
            for(let i=0; i<gearSystemResources.length; ++i) {
                this._gearSystems.push(new Models.Gear.GearSystem(gearSystemResources[i]));
            }
        }

        private loadGearCollections(gearCollectionResources: Resources.Gear.IGearCollectionResource[]) {
            if(this._gearCollections) {
                throw new Error("Gear collections already loaded!");
            }

            this._gearCollections = <Array<Models.Gear.GearCollection>>[];
            for(let i=0; i<gearCollectionResources.length; ++i) {
                this._gearCollections.push(new Models.Gear.GearCollection(gearCollectionResources[i]));
            }
        }

        public loadFromDevice($q: ng.IQService, gearItemService: Services.Gear.IGearItemService, gearSystemService: Services.Gear.IGearSystemService, gearCollectionService: Services.Gear.IGearCollectionService) : ng.IPromise<any[]> {
            const promises = <Array<ng.IPromise<any>>>[];

            promises.push(gearItemService.query().$promise.then(
                (gearItemResources: Resources.Gear.IGearItemResource[]) => {
                    this.loadGearItems(gearItemResources);
                }
            ));

            promises.push(gearSystemService.query().$promise.then(
                (gearSystemResources: Resources.Gear.IGearSystemResource[]) => {
                    this.loadGearSystems(gearSystemResources);
                }
            ));

            promises.push(gearCollectionService.query().$promise.then(
                (gearCollectionResources: Resources.Gear.IGearCollectionResource[]) => {
                    this.loadGearCollections(gearCollectionResources);
                }
            ));

            return $q.all(promises);
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            // mockup does nothing here
            return $q.defer().promise;
        }
    }
}