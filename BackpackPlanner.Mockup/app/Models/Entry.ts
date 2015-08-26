module BackpackPlanner.Mockup.Models {
    "use strict";

    export interface IEntry {
        count: (count?: number) => number;

        getName: () => string;
        getTotalWeightInUnits: (/*units: string*/) => number;
        getCostInCurrency: (/*currency: string*/) => number;
    }
}
