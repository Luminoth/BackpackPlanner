///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

//<reference path="../../Resources/Trips/TripPlanResource.ts"/>

///<reference path="../Gear/GearCollection.ts"/>
///<reference path="../Gear/GearItem.ts"/>
///<reference path="../Gear/GearSystem.ts"/>

///<reference path="../Meals/Meal.ts"/>

///<reference path="TripItinerary.ts"/>

module BackpackPlanner.Mockup.Models.Trips {
    "use strict";

    export interface ITripPlan {
        Id: number;
        Name: string;
        StartDate: string;
        EndDate: string;
        TripItineraryId: number;
        Note: string;

        GearCollections: Models.Gear.IGearCollectionEntry[];
        GearSystems: Models.Gear.IGearSystemEntry[];
        GearItems: Models.Gear.IGearItemEntry[];

        Meals: Models.Meals.IMealEntry[];
    }

    export class TripPlan implements ITripPlan {
        public Id = -1;
        public Name = "";
        public StartDate = "";
        public EndDate = "";
        public TripItineraryId = -1;
        public Note = "";

        public GearCollections = <Array<Models.Gear.GearCollectionEntry>>[];
        public GearSystems = <Array<Models.Gear.GearSystemEntry>>[];
        public GearItems = <Array<Models.Gear.GearItemEntry>>[];

        public Meals = <Array<Models.Meals.MealEntry>>[];

        public StartDateAsDate = new Date();
        public EndDateAsDate = new Date();

        /* Load/Save */

        public loadFromDevice($q: ng.IQService, tripPlanResource: Resources.Trips.ITripPlanResource) : ng.IPromise<any> {
            this.Id = tripPlanResource.Id;
            this.Name = tripPlanResource.Name;
            this.StartDate = tripPlanResource.StartDate;
            this.EndDate = tripPlanResource.EndDate;
            this.TripItineraryId = tripPlanResource.TripItineraryId;
            this.Note = tripPlanResource.Note;

            // TODO: gear/meals

            this.StartDateAsDate = new Date(this.StartDate);
            this.EndDateAsDate = new Date(this.EndDate);

            return $q.defer().promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            // mockup does nothing here
            return $q.defer().promise;
        }
    }
}
