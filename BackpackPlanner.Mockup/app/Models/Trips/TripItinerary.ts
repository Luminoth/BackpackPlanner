module BackpackPlanner.Mockup.Models.Trips {
    "use strict";

    export interface ITripItinerary {
        Id: number;
    }

    export class TripItinerary implements ITripItinerary {
        public Id = -1;
    }

    export interface ITripItineraryEntry {
    }

    export class TripItineraryEntry implements ITripItineraryEntry {
    }
}
