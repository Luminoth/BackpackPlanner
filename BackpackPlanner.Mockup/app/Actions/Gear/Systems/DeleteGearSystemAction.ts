/// <reference path="../../../Models/Gear/GearSystem.ts"/>

/// <reference path="../../Command.ts"/>

/// <reference path="../../../AppState.ts"/>

module BackpackPlanner.Mockup.Actions.Gear.Systems {
    "use strict";

    export class DeleteGearSystemAction implements ICommand {
        public GearSystem: Models.Gear.GearSystem;

        private _gearCollections = <Array<Models.Gear.GearCollection>>[];
        private _tripPlans = <Array<Models.Trips.TripPlan>>[];

        public doAction() {
            this._tripPlans = AppState.getInstance().getTripState().removeGearSystemFromPlans(this.GearSystem);
            this._gearCollections = AppState.getInstance().getGearState().removeGearSystemFromCollections(this.GearSystem);

            AppState.getInstance().getGearState().deleteGearSystem(this.GearSystem);
        }

        public undoAction() {
            AppState.getInstance().getGearState().addGearSystem(this.GearSystem);

            for(let i=0; i<this._gearCollections.length; ++i) {
                const gearCollection = this._gearCollections[i];
                gearCollection.addGearSystem(this.GearSystem);
            }

            for(let i=0; i<this._tripPlans.length; ++i) {
                const tripPlan = this._tripPlans[i];
                tripPlan.addGearSystem(this.GearSystem);
            }
        }
    }
}
