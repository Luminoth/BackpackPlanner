﻿///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />

//<reference path="../../Resources/Trips/TripItineraryResource.ts"/>

module BackpackPlanner.Mockup.Models.Trips {
    import GearItemEntry = BackpackPlanner.Mockup.Models.Gear.GearItemEntry;
    "use strict";

    export interface IRouteDescription {
        Id: number;
        Description: string;
    }

    export class RouteDescription implements IRouteDescription {
        public Id = -1;
        public Description = "";

        constructor(id: number, description: string) {
            this.Id = id;
            this.Description = description;
        }
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

        constructor(id: number, name: string, gpsCoordinate: string) {
            this.Id = id;
            this.Name = name;
            this.GpsCoordinate = gpsCoordinate;
        }
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

        /* Load/Save */

        public update(tripItinerary: TripItinerary) {
            this.Name = tripItinerary.Name;
            this.Note = tripItinerary.Note;

            this.RouteDescriptions = <Array<RouteDescription>>[];
            for(let i=0; i<tripItinerary.RouteDescriptions.length; ++i) {
                const routeDescription = tripItinerary.RouteDescriptions[i];
                this.RouteDescriptions.push(new RouteDescription(routeDescription.Id, routeDescription.Description));
            }

            this.PointsOfInterest = <Array<PointOfInterest>>[];
            for(let i=0; i<tripItinerary.PointsOfInterest.length; ++i) {
                const pointOfInterest = tripItinerary.PointsOfInterest[i];
                this.PointsOfInterest.push(new PointOfInterest(pointOfInterest.Id, pointOfInterest.Name, pointOfInterest.GpsCoordinate));
            }
        }

        public loadFromDevice($q: ng.IQService, tripItineraryResource: Resources.Trips.ITripItineraryResource) : ng.IPromise<any> {
            const deferred = $q.defer();

            this.Id = tripItineraryResource.Id;
            this.Name = tripItineraryResource.Name;
            this.Note = tripItineraryResource.Note;

            for(let i=0; i<tripItineraryResource.RouteDescriptions.length; ++i) {
                const routeDescription = tripItineraryResource.RouteDescriptions[i];
                this.RouteDescriptions.push(new RouteDescription(routeDescription.Id, routeDescription.Description));
            }

            for(let i=0; i<tripItineraryResource.PointsOfInterest.length; ++i) {
                const pointOfInterest = tripItineraryResource.PointsOfInterest[i];
                this.PointsOfInterest.push(new PointOfInterest(pointOfInterest.Id, pointOfInterest.Name, pointOfInterest.GpsCoordinate));
            }

            deferred.resolve(this);
            return deferred.promise;
        }

        public saveToDevice($q: ng.IQService) : ng.IPromise<any> {
            alert("TripItinerary.saveToDevice");
            return $q.defer().promise;
        }
    }
}
