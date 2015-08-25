module BackpackPlanner.Mockup.Actions {
    "use strict";

    /*
    Commands to think about:
        Add*
        Delete*
        DeleteAll*

        Idea is to hold the "last" command and allow for undoing commands
            e.g. Deleting a GearItem needs to remove it from everything holding it
                and undoing that delete needs to add it back to everything that was holding it
    */

    export interface ICommand {
    }
}
