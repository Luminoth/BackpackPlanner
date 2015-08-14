module BackpackPlanner.Mockup.Models {
    "use strict";

    export interface IEntry {
        Count: number;
        IsPacked: boolean;

        getName: () => string;
        getCostInCurrency: () => number;
        getWeightInUnits: () => number;
    }
}