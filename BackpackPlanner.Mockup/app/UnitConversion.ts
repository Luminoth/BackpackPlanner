/// <reference path="Models/AppSettings.ts"/>

module BackpackPlanner.Mockup {
    "use strict";

    export function getUnitsWeightString(units: string) {
        switch(units)
        {
        case "Imperial":
            return "ounce";
        case "Metric":
            return "gram";
        }
        throw new Error(`Invalid units: ${units}`);
    }

    export function convertGramsToOunces(grams: number) {
        return grams * 0.035274;
    }

    export function convertOuncesToGrams(ounces: number) {
        return ounces * 28.3495;
    }

    export function convertGramsToUnits(grams: number, units: string) {
        switch(units)
        {
        case "Metric":
            return grams;
        case "Imperial":
            return convertGramsToOunces(grams);
        }
        throw new Error(`Invalid units: ${units}`);
    }

    export function convertUnitsToGrams(value: number, units: string) {
        switch(units)
        {
        case "Metric":
            return value;
        case "Imperial":
            return convertOuncesToGrams(value);
        }
        throw new Error(`Invalid units: ${units}`);
    }

    export function getUnitsLengthString(units: string) {
        switch(units)
        {
        case "Imperial":
            return "inches";
        case "Metric":
            return "centimeters";
        }
        throw new Error(`Invalid units: ${units}`);
    }

    export function convertCentimetersToInches(centimeters: number) {
        return centimeters * 0.393701;
    }

    export function convertInchesToCentimeters(inches: number) {
        return inches * 2.54;
    }

    export function convertCentimetersToUnits(centimeters: number, units: string) {
        switch(units)
        {
        case "Metric":
            return centimeters;
        case "Imperial":
            return convertCentimetersToInches(centimeters);
        }
        throw new Error(`Invalid units: ${units}`);
    }

    export function convertUnitsToCentimeters(value: number, units: string) {
        switch(units)
        {
        case "Metric":
            return value;
        case "Imperial":
            return convertInchesToCentimeters(value);
        }
        throw new Error(`Invalid units: ${units}`);
    }

    export function getCurrencyString(currency: string) {
        switch(currency)
        {
        case "USD":
            return "USD";
        }
        throw new Error(`Invalid currency: ${currency}`);
    }

    export function convertUSDPToUSD(usdp: number) {
        return usdp * 0.01;
    }

    export function convertUSDToUSDP(usd: number) {
        return usd * 100.0;
    }

    export function convertUSDPToCurrency(usdp: number, currency: string) {
        switch(currency)
        {
        case "USD":
            return convertUSDPToUSD(usdp);
        }
        throw new Error(`Invalid currency: ${currency}`);
    }

    export function convertCurrencyToUSDP(value: number, currency: string) {
        switch(currency)
        {
        case "USD":
            return convertUSDToUSDP(value);
        }
        throw new Error(`Invalid currency: ${currency}`);
    }
}
