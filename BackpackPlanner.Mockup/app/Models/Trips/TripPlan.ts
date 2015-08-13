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
        Note: string;

        GearCollections: Models.Gear.IGearCollectionEntry[];
        GearSystems: Models.Gear.IGearSystemEntry[];
        GearItems: Models.Gear.IGearItemEntry[];

        Meals: Models.Meals.IMealEntry[];

        TripItinerary: ITripItinerary;
    }

    export class TripPlan implements ITripPlan {
        public Id = -1;
        public Name = "";
        public StartDate = "";
        public EndDate = "";
        public Note = "";

        public GearCollections = <Array<Models.Gear.GearCollectionEntry>>[];
        public GearSystems = <Array<Models.Gear.GearSystemEntry>>[];
        public GearItems = <Array<Models.Gear.GearItemEntry>>[];

        public Meals = <Array<Models.Meals.MealEntry>>[];

        public TripItinerary = new TripItinerary();

        public StartDateAsDate = new Date();
        public EndDateAsDate = new Date();
    }
}
