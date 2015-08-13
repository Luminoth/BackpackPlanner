﻿///<reference path="../scripts/typings/angularjs/angular.d.ts" />

///<reference path="Models/Personal/UserInformation.ts" />
///<reference path="Models/AppSettings.ts" />

///<reference path="Resources/Personal/UserInformationResource.ts" />
///<reference path="Resources/AppSettingsResource.ts" />

///<reference path="Services/Gear/GearCollectionService.ts"/>
///<reference path="Services/Gear/GearItemService.ts"/>
///<reference path="Services/Gear/GearSystemService.ts"/>
///<reference path="Services/Meals/MealService.ts"/>
///<reference path="Services/Trips/TripItineraryService.ts"/>
///<reference path="Services/Trips/TripPlanService.ts"/>
///<reference path="Services/Personal/UserInformationService.ts"/>
///<reference path="Services/AppSettingsService.ts"/>

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

        /* User Information */

        private _userInformation: Models.Personal.UserInformation;

        public getUserInformation() : Models.Personal.UserInformation {
            return this._userInformation;
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

        private loadAppSettings(appSettingsResource: Resources.IAppSettingsResource) {
            if(this._appSettings) {
                throw new Error("Application settings already loaded!");
            }
            this._appSettings = new Models.AppSettings(appSettingsResource);
        }

        private loadUserInformation(userInfoResource: Resources.Personal.IUserInformationResource) {
            if(this._userInformation) {
                throw new Error("User information already loaded!");
            }
            this._userInformation = new Models.Personal.UserInformation(userInfoResource);
        }

        public loadFromDevice($q: ng.IQService,
            appSettingsService: Services.IAppSettingsService, userInformationService: Services.Personal.IUserInformationService,
            gearItemService: Services.Gear.IGearItemService, gearSystemService: Services.Gear.IGearSystemService, gearCollectionService: Services.Gear.IGearCollectionService,
            mealService: Services.Meals.IMealService,
            tripItineraryService: Services.Trips.ITripItineraryService, tripPlanService: Services.Trips.ITripPlanService) : ng.IPromise<any[]> {
            const promises = <Array<ng.IPromise<any>>>[];

            // load the application settings
            promises.push(appSettingsService.get().$promise.then(
                (appSettingsResource: Resources.IAppSettingsResource) => {
                    this.loadAppSettings(appSettingsResource);
                }
            ));

            // load the user's personal information
            promises.push(userInformationService.get().$promise.then(
                (userInfoResource: Resources.Personal.IUserInformationResource) => {
                    this.loadUserInformation(userInfoResource);
                }
            ));

            promises.push(this._gearState.loadFromDevice($q, gearItemService, gearSystemService, gearCollectionService));
            promises.push(this._mealState.loadFromDevice($q, mealService));
            promises.push(this._tripState.loadFromDevice($q, tripItineraryService, tripPlanService));

            return $q.all(promises);
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            const promises = <Array<ng.IPromise<any>>>[];

            promises.push(this._gearState.saveToDevice($q));
            promises.push(this._mealState.saveToDevice($q));
            promises.push(this._tripState.saveToDevice($q));

            return $q.all(promises);
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