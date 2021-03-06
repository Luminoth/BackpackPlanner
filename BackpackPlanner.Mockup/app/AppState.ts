﻿/// <reference path="../scripts/typings/angularjs/angular.d.ts" />

/// <reference path="Actions/Command.ts"/>

/// <reference path="Models/Personal/UserInformation.ts" />
/// <reference path="Models/AppSettings.ts" />

/// <reference path="Resources/Personal/UserInformationResource.ts" />
/// <reference path="Resources/AppSettingsResource.ts" />

/// <reference path="Services/Gear/GearCollectionService.ts"/>
/// <reference path="Services/Gear/GearItemService.ts"/>
/// <reference path="Services/Gear/GearSystemService.ts"/>
/// <reference path="Services/Meals/MealService.ts"/>
/// <reference path="Services/Trips/TripItineraryService.ts"/>
/// <reference path="Services/Trips/TripPlanService.ts"/>
/// <reference path="Services/Personal/UserInformationService.ts"/>
/// <reference path="Services/AppSettingsService.ts"/>

/// <reference path="GearState.ts" />
/// <reference path="MealState.ts" />
/// <reference path="TripState.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export class AppState {
        /* Singleton */

        private static _instance = new AppState();

        public static getInstance() {
            return AppState._instance;
        }

        constructor() {
            if(AppState._instance) {
                throw new Error("Error: AppState already instantiated!");
            }
        }

        /* Actions */

        private _lastAction: Actions.ICommand;

        public executeAction(action: Actions.ICommand) {
            this._lastAction = action;
            this._lastAction.doAction();
        }

        public undoAction() {
            if(!this._lastAction) {
                return;
            }

            this._lastAction.undoAction();
            this._lastAction = undefined;
        }

        /* App Settings */

        private _appSettings = new Models.AppSettings();

        public getAppSettings() {
            return this._appSettings;
        }

        /* User Information */

        private _userInformation = new Models.Personal.UserInformation();

        public getUserInformation() {
            return this._userInformation;
        }

        /* Gear State */

        private _gearState = new GearState();

        public getGearState() {
            return this._gearState;
        }

        /* Meal State */

        private _mealState = new MealState();

        public getMealState() {
            return this._mealState;
        }

        /* Trip State */

        private _tripState = new TripState();

        public getTripState() {
            return this._tripState;
        }

        /* Utilities */
        public deleteAllData() {
            this._gearState.deleteAllData();
            this._mealState.deleteAllData();
            this._tripState.deleteAllData();
        }

        /* Load/Save */

        public loadFromDevice($q: ng.IQService,
            appSettingsService: Services.IAppSettingsService, userInformationService: Services.Personal.IUserInformationService,
            gearItemService: Services.Gear.IGearItemService, gearSystemService: Services.Gear.IGearSystemService, gearCollectionService: Services.Gear.IGearCollectionService,
            mealService: Services.Meals.IMealService,
            tripItineraryService: Services.Trips.ITripItineraryService, tripPlanService: Services.Trips.ITripPlanService) : ng.IPromise<any[]> {

            const promises = <Array<ng.IPromise<any>>>[];

            // load the application settings
            promises.push(appSettingsService.get().$promise.then(
                (appSettingsResource: Resources.IAppSettingsResource) => {
                    this._appSettings.loadFromDevice($q, appSettingsResource).then(
                        () => {
                        }
                    );
                }
            ));

            // load the user's personal information
            promises.push(userInformationService.get().$promise.then(
                (userInfoResource: Resources.Personal.IUserInformationResource) => {
                    this._userInformation.loadFromDevice($q, userInfoResource).then(
                        () => {
                        }
                    );
                }
            ));

            promises.push(this._gearState.loadFromDevice($q, gearItemService, gearSystemService, gearCollectionService));
            promises.push(this._mealState.loadFromDevice($q, mealService));
            promises.push(this._tripState.loadFromDevice($q, tripItineraryService, tripPlanService));

            return $q.all(promises);
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any[]> {
            alert("AppState.saveToDevice");
            return $q.defer().promise;
        }

        /* Import/Export */

        public importFromCloudStorage($q: ng.IQService, cloudStorage: string) : ng.IPromise<any> {
            // mockup does nothing here
            return $q.defer().promise;
        }

        public exportToCloudStorage($q: ng.IQService, cloudStorage: string) : ng.IPromise<any> {
            // mockup does nothing here
            return $q.defer().promise;
        }
    }
}
