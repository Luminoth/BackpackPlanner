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

        private _gearItems = <Array<Models.Gear.GearItem>>[];

        // TODO: this should be a read-only collection
        public getGearItems() {
            return this._gearItems;
        }

        public getGearItemIndexById(gearItemId: number) {
            for(let i=0; i<this._gearItems.length; ++i) {
                const gearItem = this._gearItems[i];
                if(gearItem.Id == gearItemId) {
                    return i;
                }
            }
            return -1;
        }

        public getGearItemById(gearItemId: number) {
            const idx = this.getGearItemIndexById(gearItemId);
            return idx < 0 ? null : this._gearItems[idx];
        }

        private static _lastGearItemId = 0;

        private getNextGearItemId() {
            return ++GearState._lastGearItemId;
        }

        public addGearItem(gearItem: Models.Gear.GearItem) {
            if(gearItem.Id < 0) {
                gearItem.Id = this.getNextGearItemId();
            } else if(gearItem.Id > GearState._lastGearItemId) {
                GearState._lastGearItemId = gearItem.Id;
            }

            this._gearItems.push(gearItem);
            return gearItem.Id;
        }

        public deleteGearItem(gearItem: Models.Gear.GearItem) {
            const idx = this.getGearItemIndexById(gearItem.Id);
            if(idx < 0) {
                return false;
            }
            this._gearItems.splice(idx, 1);
            return true;
        }

        public deleteAllGearItems() {
            this._gearItems = <Array<Models.Gear.GearItem>>[];
        }

        /* Gear Systems */

        private _gearSystems = <Array<Models.Gear.GearSystem>>[];

        // TODO: this should be a read-only collection
        public getGearSystems() {
            return this._gearSystems;
        }

        public getGearSystemIndexById(gearSystemId: number) {
            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystem = this._gearSystems[i];
                if(gearSystem.Id == gearSystemId) {
                    return i;
                }
            }
            return -1;
        }

        public getGearSystemById(gearSystemId: number) {
            const idx = this.getGearSystemIndexById(gearSystemId);
            return idx < 0 ? null : this._gearSystems[idx];
        }

        private static _lastGearSystemId = 0;

        private getNextGearSystemId() {
            return ++GearState._lastGearSystemId;
        }

        public addGearSystem(gearSystem: Models.Gear.GearSystem) {
            if(gearSystem.Id < 0) {
                gearSystem.Id = this.getNextGearSystemId();
            } else if(gearSystem.Id > GearState._lastGearSystemId) {
                GearState._lastGearSystemId = gearSystem.Id;
            }

            this._gearSystems.push(gearSystem);
            return gearSystem.Id;
        }

        public removeGearItemFromSystems(gearItem: Models.Gear.GearItem) {
            const gearSystems = <Array<Models.Gear.GearSystem>>[];
            for(let i=0; i<this._gearSystems.length; ++i ) {
                const gearSystem = this._gearSystems[i];
                if(gearSystem.containsGearItemById(gearItem.Id)) {
                    gearSystem.removeGearItemById(gearItem.Id);
                    gearSystems.push(gearSystem);
                }
            }
            return gearSystems;
        }

        public deleteGearSystem(gearSystem: Models.Gear.GearSystem) {
            const idx = this.getGearSystemIndexById(gearSystem.Id);
            if(idx < 0) {
                return false;
            }
            this._gearSystems.splice(idx, 1);
            return true;
        }

        public deleteAllGearSystems() {
            this._gearSystems = <Array<Models.Gear.GearSystem>>[];
        }

        /* Gear Collections */

        private _gearCollections = <Array<Models.Gear.GearCollection>>[];

        // TODO: this should be a read-only collection
        public getGearCollections() {
            return this._gearCollections;
        }

        public getGearCollectionIndexById(gearCollectionId: number) {
            for(let i=0; i<this._gearCollections.length; ++i) {
                const gearCollection = this._gearCollections[i];
                if(gearCollection.Id == gearCollectionId) {
                    return i;
                }
            }
            return -1;
        }

        public getGearCollectionById(gearCollectionId: number) {
            const idx = this.getGearCollectionIndexById(gearCollectionId);
            return idx < 0 ? null : this._gearCollections[idx];
        }

        private static _lastGearCollectionId = 0;

        private getNextGearCollectionId() {
            return ++GearState._lastGearCollectionId;
        }

        public addGearCollection(gearCollection: Models.Gear.GearCollection) {
            if(gearCollection.Id < 0) {
                gearCollection.Id = this.getNextGearCollectionId();
            } else if(gearCollection.Id > GearState._lastGearCollectionId) {
                GearState._lastGearCollectionId = gearCollection.Id;
            }

            this._gearCollections.push(gearCollection);
            return gearCollection.Id;
        }

        public removeGearSystemFromCollections(gearSystem: Models.Gear.GearSystem) {
            const gearCollections = <Array<Models.Gear.GearCollection>>[];
            for(let i=0; i<this._gearCollections.length; ++i ) {
                const gearCollection = this._gearCollections[i];
                if(gearCollection.containsGearSystemById(gearSystem.Id)) {
                    gearCollection.removeGearSystemById(gearSystem.Id);
                    gearCollections.push(gearCollection);
                }
            }
            return gearCollections;
        }

        public removeGearItemFromCollections(gearItem: Models.Gear.GearItem) {
            const gearCollections = <Array<Models.Gear.GearCollection>>[];
            for(let i=0; i<this._gearCollections.length; ++i ) {
                const gearCollection = this._gearCollections[i];
                if(gearCollection.containsGearItemById(gearItem.Id)) {
                    gearCollection.removeGearItemById(gearItem.Id);
                    gearCollections.push(gearCollection);
                }
            }
            return gearCollections;
        }

        public deleteGearCollection(gearCollection: Models.Gear.GearCollection) {
            const idx = this.getGearCollectionIndexById(gearCollection.Id);
            if(idx < 0) {
                return false;
            }
            this._gearCollections.splice(idx, 1);
            return true;
        }

        public deleteAllGearCollections() {
            this._gearCollections = <Array<Models.Gear.GearCollection>>[];
        }

        /* Utilities */

        public deleteAllData() {
            this.deleteAllGearCollections();
            this.deleteAllGearSystems();
            this.deleteAllGearItems();
        }

        /* Load/Save */

        private loadGearItems($q: ng.IQService, gearItemResources: Resources.Gear.IGearItemResource[]) {
            const promises = <Array<ng.IPromise<any>>>[];
            this._gearItems = <Array<Models.Gear.GearItem>>[];
            for(let i=0; i<gearItemResources.length; ++i) {
                const gearItem = new Models.Gear.GearItem();
                promises.push(gearItem.loadFromDevice($q, gearItemResources[i]).then(
                    (loadedGearItem) => {
                        this.addGearItem(loadedGearItem);
                    }
                ));
            }
            return $q.all(promises);
        }

        private loadGearSystems($q: ng.IQService, gearSystemResources: Resources.Gear.IGearSystemResource[]) {
            const promises = <Array<ng.IPromise<any>>>[];
            this._gearSystems = <Array<Models.Gear.GearSystem>>[];
            for(let i=0; i<gearSystemResources.length; ++i) {
                const gearSystem = new Models.Gear.GearSystem();
                promises.push(gearSystem.loadFromDevice($q, gearSystemResources[i]).then(
                    (loadedGearSystem) => {
                        this.addGearSystem(loadedGearSystem);
                    }
                ));
            }
            return $q.all(promises);
        }

        private loadGearCollections($q: ng.IQService, gearCollectionResources: Resources.Gear.IGearCollectionResource[]) {
            const promises = <Array<ng.IPromise<any>>>[];
            this._gearCollections = <Array<Models.Gear.GearCollection>>[];
            for(let i=0; i<gearCollectionResources.length; ++i) {
                const gearCollection = new Models.Gear.GearCollection();
                promises.push(gearCollection.loadFromDevice($q, gearCollectionResources[i]).then(
                    (loadedGearCollection) => {
                        this.addGearCollection(loadedGearCollection);
                    }
                ));
            }
            return $q.all(promises);
        }

        public loadFromDevice($q: ng.IQService, gearItemService: Services.Gear.IGearItemService, gearSystemService: Services.Gear.IGearSystemService, gearCollectionService: Services.Gear.IGearCollectionService) : ng.IPromise<any[]> {
            const promises = <Array<ng.IPromise<any>>>[];

            promises.push(gearItemService.query().$promise.then(
                (gearItemResources: Resources.Gear.IGearItemResource[]) => {
                    this.loadGearItems($q, gearItemResources).then(
                        () => {
                        }
                    );
                }
            ));

            promises.push(gearSystemService.query().$promise.then(
                (gearSystemResources: Resources.Gear.IGearSystemResource[]) => {
                    this.loadGearSystems($q, gearSystemResources).then(
                        () => {
                        }
                    );
                }
            ));

            promises.push(gearCollectionService.query().$promise.then(
                (gearCollectionResources: Resources.Gear.IGearCollectionResource[]) => {
                    this.loadGearCollections($q, gearCollectionResources).then(
                        () => {
                        }
                    );
                }
            ));

            return $q.all(promises);
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any[]> {
            alert("GearState.saveToDevice");
            return $q.defer().promise;
        }
    }
}