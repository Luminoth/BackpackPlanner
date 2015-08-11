module BackpackPlanner.Mockup.Models.Trips {
    "use strict";

    export interface ITripPlan {
        Id: number;
    }

    export class TripPlan implements ITripPlan {
        public Id = -1;
    }

    export interface ITripPlanEntry {
    }

    export class TripPlanEntry implements ITripPlanEntry {
    }
}
