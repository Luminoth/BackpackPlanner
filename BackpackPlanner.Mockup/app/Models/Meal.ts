﻿module BackpackPlanner.Mockup.Models {
    "use strict";

    export interface IMeal {
        Id: number;
    }

    export class Meal implements IMeal {
        public Id = -1;
    }

    export interface IMealEntry {
    }

    export class MealEntry implements IMealEntry {
    }
}
