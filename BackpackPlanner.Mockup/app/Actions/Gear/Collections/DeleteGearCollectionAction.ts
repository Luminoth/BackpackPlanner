///<reference path="../../../Models/Gear/GearCollection.ts"/>

///<reference path="../../Command.ts"/>

///<reference path="../../../AppState.ts"/>

module BackpackPlanner.Mockup.Actions.Gear.Collections {
    "use strict";

    export class DeleteGearCollectionAction implements ICommand {
        public GearCollection: Models.Gear.GearCollection;

        private _tripPlans = <Array<Models.Trips.TripPlan>>[];

        public doAction() {
            this._tripPlans = AppState.getInstance().getTripState().removeGearCollectionFromPlans(this.GearCollection);

            AppState.getInstance().getGearState().deleteGearCollection(this.GearCollection);
        }

        public undoAction() {
            AppState.getInstance().getGearState().addGearCollection(this.GearCollection);

            for(let i=0; i<this._tripPlans.length; ++i) {
                const tripPlan = this._tripPlans[i];
                tripPlan.addGearCollection(this.GearCollection);
            }
        }
    }
}
