module BackpackPlanner.Mockup.Actions {
    "use strict";

    export interface ICommand {
        doAction: () => void;
        undoAction: () => void;
    }
}
