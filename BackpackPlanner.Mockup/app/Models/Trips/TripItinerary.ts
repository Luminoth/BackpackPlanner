module BackpackPlanner.Mockup.Models.Trips {
    "use strict";

    export interface IRouteDescription {
        Id: number;
        Description: string;
    }

    export class RouteDescription implements IRouteDescription {
        public Id = -1;
        public Description = "";
    }

    export interface IPointOfInterest {
        Id: number;
        Name: string;
        GpsCoordinate: string;
    }

    export class PointOfInterest implements IPointOfInterest {
        public Id = -1;
        public Name = "";
        public GpsCoordinate = "";
    }

    export interface ITripItinerary {
        Id: number;
        Name: string;
        Note: string;

        RouteDescriptions: IRouteDescription[];
        PointsOfInterest: IPointOfInterest[];
    }

    export class TripItinerary implements ITripItinerary {
        public Id = -1;
        public Name = "";
        public Note = "";

        public RouteDescriptions = <Array<RouteDescription>>[];
        public PointsOfInterest = <Array<PointOfInterest>>[];
    }
}
