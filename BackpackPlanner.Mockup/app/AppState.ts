///<reference path="Models/AppSettings.ts" />
///<reference path="Models/UserInformation.ts" />

///<reference path="Resources/AppSettingsResource.ts" />
///<reference path="Resources/UserInformationResource.ts" />

///<reference path="GearState.ts" />
///<reference path="MealState.ts" />
///<reference path="TripState.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export class AppState {
        /* Singleton */

        private static _instance = new AppState();

        public static getInstance() : AppState {
            return AppState._instance;
        }

        constructor() {
            if(AppState._instance) {
                throw new Error("Error: AppState already instantiated!");
            }
        }

        /* App Settings */

        private _appSettings: Models.AppSettings;

        public getAppSettings() : Models.AppSettings {
            return this._appSettings;
        }

        public loadAppSettings(appSettingsResource: Resources.IAppSettingsResource) {
            if(this._appSettings) {
                throw new Error("Application settings already loaded!");
            }
            this._appSettings = new Models.AppSettings(appSettingsResource);
        }

        /* User Information */

        private _userInformation: Models.UserInformation;

        public getUserInformation() : Models.UserInformation {
            return this._userInformation;
        }

        public loadUserInformation(userInfoResource: Resources.IUserInformationResource) {
            if(this._userInformation) {
                throw new Error("User information already loaded!");
            }
            this._userInformation = new Models.UserInformation(userInfoResource);
        }

        /* Gear State */

        private _gearState = new GearState();

        public getGearState() : GearState {
            return this._gearState;
        }

        /* Meal State */

        private _mealState = new MealState();

        public getMealState() : MealState {
            return this._mealState;
        }

        /* Trip State */

        private _tripState = new TripState();

        public getTripState() : TripState {
            return this._tripState;
        }

        /* Load/Save */

        public loadFromDevice() {
            // TODO: load from the resources here and return a promise

            this._gearState.loadFromDevice();
            this._mealState.loadFromDevice();
            this._tripState.loadFromDevice();
        }

        public saveToDevice() {
            // TODO: don't do anything here, just return a promise

            this._gearState.saveToDevice();
            this._mealState.saveToDevice();
            this._tripState.saveToDevice();
        }

        /* Import/Export */

        public importFromCloudStorage(cloudStorage: string) {
            // TODO: don't do anything here, just return a promise
        }

        public exportToCloudStorage(cloudStorage: string) {
            // TODO: don't do anything here, just return a promise
        }
    }
}
