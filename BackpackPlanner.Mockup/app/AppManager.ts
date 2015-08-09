///<reference path="Models/AppSettings.ts" />
///<reference path="Models/UserInformation.ts" />
///<reference path="Models/Gear/GearCollection.ts" />
///<reference path="Models/Gear/GearItem.ts" />
///<reference path="Models/Gear/GearSystem.ts" />

///<reference path="Resources/AppSettingsResource.ts" />
///<reference path="Resources/UserInformationResource.ts" />
///<reference path="Resources/Gear/GearCollectionResource.ts" />
///<reference path="Resources/Gear/GearItemResource.ts" />
///<reference path="Resources/Gear/GearSystemResource.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export class AppManager {
        private static _instance = new AppManager();

        public static getInstance() : AppManager {
            return AppManager._instance;
        }

        private _appSettings: Models.AppSettings;

        public getAppSettings() : Models.AppSettings {
            return this._appSettings;
        }

        public setAppSettings(appSettingsResource: Resources.IAppSettingsResource) {
            if(this._appSettings) {
                throw new Error("Application settings already set!");
            }
            this._appSettings = new Models.AppSettings(appSettingsResource);
        }

        private _userInformation: Models.UserInformation;

        public getUserInformation() : Models.UserInformation {
            return this._userInformation;
        }

        public setUserInformation(userInfoResource: Resources.IUserInformationResource) {
            if(this._userInformation) {
                throw new Error("User information already set!");
            }
            this._userInformation = new Models.UserInformation(userInfoResource);
        }

        private _gearItems: Models.Gear.GearItem[];

        public getGearItems() : Models.Gear.GearItem[] {
            return this._gearItems;
        }

        public setGearItems(gearItemsResource: Resources.Gear.IGearItemResource[]) {
            if(this._gearItems) {
                throw new Error("Gear items already set!");
            }

            this._gearItems = <Array<Models.Gear.GearItem>>[];
            for(let i=0; i<gearItemsResource.length; ++i) {
                this._gearItems.push(new Models.Gear.GearItem(gearItemsResource[i]));
            }
        }

        public getNextGearItemId() : number {
            // TODO: write this
            return -1;
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

        public deleteGearItem(gearItem: Models.Gear.GearItem) : boolean {
            const idx = this.getGearItemIndexById(gearItem.Id);
            if(idx < 0) {
                return false;
            }
            this._gearItems.splice(idx, 1);

            // TODO: remove the item from the systems, collections, and trip plans it belongs to

            return true;
        }

        private _gearSystems: Models.Gear.GearSystem[];

        public getGearSystems() : Models.Gear.GearSystem[] {
            return this._gearSystems;
        }

        public setGearSystems(gearSystemsResource: Resources.Gear.IGearSystemResource[]) {
            if(this._gearSystems) {
                throw new Error("Gear systems already set!");
            }

            this._gearSystems = <Array<Models.Gear.GearSystem>>[];
            for(let i=0; i<gearSystemsResource.length; ++i) {
                this._gearSystems.push(new Models.Gear.GearSystem(gearSystemsResource[i]));
            }
        }

        public getNextGearSystemId() : number {
            // TODO: write this
            return -1;
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

        public deleteGearSystem(gearSystem: Models.Gear.GearSystem) : boolean {
            const idx = this.getGearSystemIndexById(gearSystem.Id);
            if(idx < 0) {
                return false;
            }
            this._gearSystems.splice(idx, 1);

            // TODO: remove the system from the collections, and trip plans it belongs to

            return true;
        }

        private _gearCollections: Models.Gear.GearCollection[];

        public getGearCollections() : Models.Gear.GearCollection[] {
            return this._gearCollections;
        }

        public setGearCollections(gearCollectionsResource: Resources.Gear.IGearCollectionResource[]) {
            if(this._gearCollections) {
                throw new Error("Gear collections already set!");
            }

            this._gearCollections = <Array<Models.Gear.GearCollection>>[];
            for(let i=0; i<gearCollectionsResource.length; ++i) {
                this._gearCollections.push(new Models.Gear.GearCollection(gearCollectionsResource[i]));
            }
        }

        public getNextGearCollectionId() : number {
            // TODO: write this
            return -1;
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

        public deleteGearCollection(gearCollection: Models.Gear.GearCollection) : boolean {
            const idx = this.getGearCollectionIndexById(gearCollection.Id);
            if(idx < 0) {
                return false;
            }
            this._gearCollections.splice(idx, 1);

            // TODO: remove the collection from the trip plans it belongs to

            return true;
        }

        constructor() {
            if(AppManager._instance) {
                throw new Error("Error: AppManager already instantiated!");
            }
        }
    }
}
