///<reference path="../../../Models/Gear/GearItem.ts"/>

///<reference path="../../Command.ts"/>

///<reference path="../../../AppState.ts"/>

module BackpackPlanner.Mockup.Actions.Gear.Items {
    "use strict";

    export class DeleteGearItemAction implements ICommand {
        public GearItem: Models.Gear.GearItem;

        private _gearSystems = <Array<Models.Gear.GearSystem>>[];
        private _gearCollections = <Array<Models.Gear.GearCollection>>[];
        private _tripPlans = <Array<Models.Trips.TripPlan>>[];

        public doAction() {
            this._tripPlans = AppState.getInstance().getTripState().removeGearItemFromPlans(this.GearItem);
            this._gearCollections = AppState.getInstance().getGearState().removeGearItemFromCollections(this.GearItem);
            this._gearSystems = AppState.getInstance().getGearState().removeGearItemFromSystems(this.GearItem);

            AppState.getInstance().getGearState().deleteGearItem(this.GearItem);
        }

        public undoAction() {
            AppState.getInstance().getGearState().addGearItem(this.GearItem);

            for(let i=0; i<this._gearSystems.length; ++i) {
                const gearSystem = this._gearSystems[i];
                gearSystem.addGearItem(this.GearItem);
            }

            for(let i=0; i<this._gearCollections.length; ++i) {
                const gearCollection = this._gearCollections[i];
                gearCollection.addGearItem(this.GearItem);
            }

            for(let i=0; i<this._tripPlans.length; ++i) {
                const tripPlan = this._tripPlans[i];
                tripPlan.addGearItem(this.GearItem);
            }
        }
    }
}
