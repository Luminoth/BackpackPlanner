var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Actions;
        (function (Actions) {
            "use strict";
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Resources;
        (function (Resources) {
            var Gear;
            (function (Gear) {
                "use strict";
            })(Gear = Resources.Gear || (Resources.Gear = {}));
        })(Resources = Mockup.Resources || (Mockup.Resources = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="GearItemResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Resources;
        (function (Resources) {
            var Gear;
            (function (Gear) {
                "use strict";
            })(Gear = Resources.Gear || (Resources.Gear = {}));
        })(Resources = Mockup.Resources || (Mockup.Resources = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="GearSystemResource.ts" />
/// <reference path="GearItemResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Resources;
        (function (Resources) {
            var Gear;
            (function (Gear) {
                "use strict";
            })(Gear = Resources.Gear || (Resources.Gear = {}));
        })(Resources = Mockup.Resources || (Mockup.Resources = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Models/Personal/UserInformation.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Resources;
        (function (Resources) {
            var Personal;
            (function (Personal) {
                "use strict";
            })(Personal = Resources.Personal || (Resources.Personal = {}));
        })(Resources = Mockup.Resources || (Mockup.Resources = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../Resources/Personal/UserInformationResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Personal;
            (function (Personal) {
                "use strict";
                class UserInformation {
                    constructor() {
                        this._firstName = "";
                        this._lastName = "";
                        this._birthDate = new Date();
                        this._sex = "NotSpecified";
                        this._heightInCm = 0;
                        this._weightInGrams = 0;
                    }
                    firstName(firstName) {
                        return arguments.length
                            ? (this._firstName = firstName)
                            : this._firstName;
                    }
                    lastName(lastName) {
                        return arguments.length
                            ? (this._lastName = lastName)
                            : this._lastName;
                    }
                    birthDate(birthDate) {
                        return arguments.length
                            ? (this._birthDate = birthDate)
                            : this._birthDate;
                    }
                    sex(sex) {
                        return arguments.length
                            ? (this._sex = sex)
                            : this._sex;
                    }
                    /* Height/Weight */
                    heightInUnits(height) {
                        return arguments.length
                            ? (this._heightInCm = Mockup.convertUnitsToCentimeters(height, Mockup.AppState.getInstance().getAppSettings().units()))
                            : parseFloat(Mockup.convertCentimetersToUnits(this._heightInCm, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    weightInUnits(weight) {
                        return arguments.length
                            ? (this._weightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                            : parseFloat(Mockup.convertGramsToUnits(this._weightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    /* Load/Save */
                    update(userInformation) {
                        this._firstName = userInformation._firstName;
                        this._lastName = userInformation._lastName;
                        this._birthDate = userInformation._birthDate;
                        this._sex = userInformation._sex;
                        this._heightInCm = userInformation._heightInCm;
                        this._weightInGrams = userInformation._weightInGrams;
                    }
                    loadFromDevice($q, userInfoResource) {
                        const deferred = $q.defer();
                        this._firstName = userInfoResource.FirstName;
                        this._lastName = userInfoResource.LastName;
                        this._birthDate = new Date(userInfoResource.BirthDate);
                        this._sex = userInfoResource.Sex;
                        this._heightInCm = userInfoResource.HeightInCm;
                        this._weightInGrams = userInfoResource.WeightInGrams;
                        deferred.resolve(this);
                        return deferred.promise;
                    }
                    saveToDevice($q) {
                        alert("UserInformation.saveToDevice");
                        return $q.defer().promise;
                    }
                }
                Personal.UserInformation = UserInformation;
            })(Personal = Models.Personal || (Models.Personal = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../Models/AppSettings.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Resources;
        (function (Resources) {
            "use strict";
        })(Resources = Mockup.Resources || (Mockup.Resources = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../Resources/AppSettingsResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            "use strict";
            class AppSettings {
                constructor() {
                    this._units = "Metric";
                    this._currency = "USD";
                    // weight classes
                    this._ultralightClassMaxWeightInGrams = 4500;
                    this._lightweightClassMaxWeightInGrams = 9000;
                    // weight categories
                    this._ultralightCategoryMaxWeightInGrams = 225;
                    this._lightCategoryMaxWeightInGrams = 450;
                    this._mediumCategoryMaxWeightInGrams = 1360;
                    this._heavyCategoryMaxWeightInGrams = 2270;
                }
                units(units) {
                    return arguments.length
                        ? (this._units = units)
                        : this._units;
                }
                currency(currency) {
                    return arguments.length
                        ? (this._currency = currency)
                        : this._currency;
                }
                getUltralightMaxWeightInGrams() {
                    return this._ultralightClassMaxWeightInGrams;
                }
                ultralightMaxWeightInUnits(weight) {
                    return arguments.length
                        ? (this._ultralightClassMaxWeightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                        : parseFloat(Mockup.convertGramsToUnits(this._ultralightClassMaxWeightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                }
                getLightweightMaxWeightInGrams() {
                    return this._lightweightClassMaxWeightInGrams;
                }
                lightweightMaxWeightInUnits(weight) {
                    return arguments.length
                        ? (this._lightweightClassMaxWeightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                        : parseFloat(Mockup.convertGramsToUnits(this._lightweightClassMaxWeightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                }
                resetToDefaults() {
                    this._units = "Metric";
                    this._currency = "USD";
                    this._ultralightClassMaxWeightInGrams = 4500;
                    this._lightweightClassMaxWeightInGrams = 9000;
                    this._ultralightCategoryMaxWeightInGrams = 225;
                    this._lightCategoryMaxWeightInGrams = 450;
                    this._mediumCategoryMaxWeightInGrams = 1360;
                    this._heavyCategoryMaxWeightInGrams = 2270;
                }
                getWeightClass(weightInGrams) {
                    if (weightInGrams < this._ultralightClassMaxWeightInGrams) {
                        return "Ultralight";
                    }
                    else if (weightInGrams < this._lightweightClassMaxWeightInGrams) {
                        return "Lightweight";
                    }
                    return "Traditional";
                }
                getWeightCategory(weightInGrams) {
                    if (weightInGrams <= 0) {
                        return "None";
                    }
                    else if (weightInGrams < this._ultralightCategoryMaxWeightInGrams) {
                        return "Ultralight";
                    }
                    else if (weightInGrams < this._lightCategoryMaxWeightInGrams) {
                        return "Light";
                    }
                    else if (weightInGrams < this._mediumCategoryMaxWeightInGrams) {
                        return "Medium";
                    }
                    else if (weightInGrams < this._heavyCategoryMaxWeightInGrams) {
                        return "Heavy";
                    }
                    return "ExtraHeavy";
                }
                /* Load/Save */
                update(appSettings) {
                    this._units = appSettings._units;
                    this._currency = appSettings._currency;
                    this._ultralightClassMaxWeightInGrams = appSettings._ultralightClassMaxWeightInGrams;
                    this._lightweightClassMaxWeightInGrams = appSettings._lightweightClassMaxWeightInGrams;
                    this._ultralightCategoryMaxWeightInGrams = appSettings._ultralightCategoryMaxWeightInGrams;
                    this._lightCategoryMaxWeightInGrams = appSettings._lightCategoryMaxWeightInGrams;
                    this._mediumCategoryMaxWeightInGrams = appSettings._mediumCategoryMaxWeightInGrams;
                    this._heavyCategoryMaxWeightInGrams = appSettings._heavyCategoryMaxWeightInGrams;
                }
                loadFromDevice($q, appSettingsResource) {
                    const deferred = $q.defer();
                    this._units = appSettingsResource.Units;
                    this._currency = appSettingsResource.Currency;
                    this._ultralightClassMaxWeightInGrams = appSettingsResource.UltralightClassMaxWeightInGrams;
                    this._lightweightClassMaxWeightInGrams = appSettingsResource.LightweightClassMaxWeightInGrams;
                    this._ultralightCategoryMaxWeightInGrams = appSettingsResource.UltralightCategoryMaxWeightInGrams;
                    this._lightCategoryMaxWeightInGrams = appSettingsResource.LightCategoryMaxWeightInGrams;
                    this._mediumCategoryMaxWeightInGrams = appSettingsResource.MediumCategoryMaxWeightInGrams;
                    this._heavyCategoryMaxWeightInGrams = appSettingsResource.HeavyCategoryMaxWeightInGrams;
                    deferred.resolve(this);
                    return deferred.promise;
                }
                saveToDevice($q) {
                    alert("AppSettings.saveToDevice");
                    return $q.defer().promise;
                }
            }
            Models.AppSettings = AppSettings;
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Resources/Gear/GearCollectionResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Services;
        (function (Services) {
            var Gear;
            (function (Gear) {
                "use strict";
                function gearCollectionServiceFactory($resource) {
                    return $resource("data/gear/collections.json", {}, {
                        query: { method: "GET", isArray: true }
                    });
                }
                Gear.gearCollectionServiceFactory = gearCollectionServiceFactory;
            })(Gear = Services.Gear || (Services.Gear = {}));
        })(Services = Mockup.Services || (Mockup.Services = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Resources/Gear/GearItemResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Services;
        (function (Services) {
            var Gear;
            (function (Gear) {
                "use strict";
                function gearItemServiceFactory($resource) {
                    return $resource("data/gear/items.json", {}, {
                        query: { method: "GET", isArray: true }
                    });
                }
                Gear.gearItemServiceFactory = gearItemServiceFactory;
            })(Gear = Services.Gear || (Services.Gear = {}));
        })(Services = Mockup.Services || (Mockup.Services = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Resources/Gear/GearSystemResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Services;
        (function (Services) {
            var Gear;
            (function (Gear) {
                "use strict";
                function gearSystemServiceFactory($resource) {
                    return $resource("data/gear/systems.json", {}, {
                        query: { method: "GET", isArray: true }
                    });
                }
                Gear.gearSystemServiceFactory = gearSystemServiceFactory;
            })(Gear = Services.Gear || (Services.Gear = {}));
        })(Services = Mockup.Services || (Mockup.Services = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            "use strict";
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../Resources/Meals/MealResource.ts"/>
/// <reference path="../../AppState.ts"/>
/// <reference path="../Entry.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Meals;
            (function (Meals) {
                "use strict";
                class Meal {
                    constructor() {
                        this._id = -1;
                        this._name = "";
                        this._url = "";
                        this._meal = "Other";
                        this._servingCount = 1;
                        this._weightInGrams = 0;
                        this._costInUSDP = 0;
                        this._calories = 0;
                        this._proteinInGrams = 0;
                        this._fiberInGrams = 0;
                        this._note = "";
                    }
                    get Id() {
                        return this._id;
                    }
                    set Id(id) {
                        this._id = id;
                    }
                    name(name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    }
                    url(url) {
                        return arguments.length
                            ? (this._url = url)
                            : this._url;
                    }
                    meal(meal) {
                        return arguments.length
                            ? (this._meal = meal)
                            : this._meal;
                    }
                    servingCount(servingCount) {
                        return arguments.length
                            ? (this._servingCount = servingCount)
                            : this._servingCount;
                    }
                    calories(calories) {
                        return arguments.length
                            ? (this._calories = calories)
                            : this._calories;
                    }
                    getCaloriesPerWeightUnit() {
                        return 0 == this._calories ? 0 : this._calories / this.weightInUnits();
                    }
                    proteinInGrams(proteinInGrams) {
                        return arguments.length
                            ? (this._proteinInGrams = proteinInGrams)
                            : this._proteinInGrams;
                    }
                    fiberInGrams(fiberInGrams) {
                        return arguments.length
                            ? (this._fiberInGrams = fiberInGrams)
                            : this._fiberInGrams;
                    }
                    note(note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    }
                    /* Weight/Cost */
                    getWeightInGrams() {
                        return this._weightInGrams;
                    }
                    weightInUnits(weight) {
                        return arguments.length
                            ? (this._weightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                            : parseFloat(Mockup.convertGramsToUnits(this._weightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getCostInUSDP() {
                        return this._costInUSDP;
                    }
                    costInCurrency(cost) {
                        return arguments.length
                            ? (this._costInUSDP = Mockup.convertCurrencyToUSDP(cost, Mockup.AppState.getInstance().getAppSettings().currency()))
                            : Mockup.convertUSDPToCurrency(this._costInUSDP, Mockup.AppState.getInstance().getAppSettings().currency());
                    }
                    getCostPerUnitInCurrency() {
                        const weightInUnits = Mockup.convertGramsToUnits(this._weightInGrams, /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                        const costInCurrency = Mockup.convertUSDPToCurrency(this._costInUSDP, /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    }
                    /* Load/Save */
                    update(meal) {
                        this._name = meal._name;
                        this._url = meal._url;
                        this._meal = meal._meal;
                        this._servingCount = meal._servingCount;
                        this._weightInGrams = meal._weightInGrams;
                        this._costInUSDP = meal._costInUSDP;
                        this._calories = meal._calories;
                        this._proteinInGrams = meal._proteinInGrams;
                        this._fiberInGrams = meal._fiberInGrams;
                        this._note = meal._note;
                    }
                    loadFromDevice($q, mealResource) {
                        const deferred = $q.defer();
                        this._id = mealResource.Id;
                        this._name = mealResource.Name;
                        this._url = mealResource.Url;
                        this._meal = mealResource.Meal;
                        this._servingCount = mealResource.ServingCount;
                        this._weightInGrams = mealResource.WeightInGrams;
                        this._costInUSDP = mealResource.CostInUSDP;
                        this._calories = mealResource.Calories;
                        this._proteinInGrams = mealResource.ProteinInGrams;
                        this._fiberInGrams = mealResource.FiberInGrams;
                        this._note = mealResource.Note;
                        deferred.resolve(this);
                        return deferred.promise;
                    }
                    saveToDevice($q) {
                        alert("Meal.saveToDevice");
                        return $q.defer().promise;
                    }
                }
                Meals.Meal = Meal;
                class MealEntry {
                    constructor(mealId, count) {
                        this._mealId = -1;
                        this._count = 1;
                        this._mealId = mealId;
                        if (count) {
                            this._count = count;
                        }
                    }
                    getMealId() {
                        return this._mealId;
                    }
                    count(count) {
                        return arguments.length
                            ? (this._count = count)
                            : this._count;
                    }
                    getName() {
                        const meal = Mockup.AppState.getInstance().getMealState().getMealById(this._mealId);
                        if (!meal) {
                            return "";
                        }
                        return meal.name();
                    }
                    getCalories() {
                        const meal = Mockup.AppState.getInstance().getMealState().getMealById(this._mealId);
                        if (!meal) {
                            return 0;
                        }
                        return this._count * meal.calories();
                    }
                    getTotalWeightInGrams() {
                        const meal = Mockup.AppState.getInstance().getMealState().getMealById(this._mealId);
                        if (!meal) {
                            return 0;
                        }
                        return this._count * meal.getWeightInGrams();
                    }
                    getTotalWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams(), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getCostInUSDP() {
                        const meal = Mockup.AppState.getInstance().getMealState().getMealById(this._mealId);
                        if (!meal) {
                            return 0;
                        }
                        return this._count * meal.getCostInUSDP();
                    }
                    getCostInCurrency() {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    }
                }
                Meals.MealEntry = MealEntry;
            })(Meals = Models.Meals || (Models.Meals = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Models/Meals/Meal.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Resources;
        (function (Resources) {
            var Meals;
            (function (Meals) {
                "use strict";
            })(Meals = Resources.Meals || (Resources.Meals = {}));
        })(Resources = Mockup.Resources || (Mockup.Resources = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Resources/Meals/MealResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Services;
        (function (Services) {
            var Meals;
            (function (Meals) {
                "use strict";
                function mealServiceFactory($resource) {
                    return $resource("data/meals/meals.json", {}, {
                        query: { method: "GET", isArray: true }
                    });
                }
                Meals.mealServiceFactory = mealServiceFactory;
            })(Meals = Services.Meals || (Services.Meals = {}));
        })(Services = Mockup.Services || (Mockup.Services = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../Resources/Trips/TripItineraryResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Trips;
            (function (Trips) {
                "use strict";
                class TripItinerary {
                    constructor() {
                        this._id = -1;
                        this._name = "";
                        this._note = "";
                    }
                    get Id() {
                        return this._id;
                    }
                    set Id(id) {
                        this._id = id;
                    }
                    name(name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    }
                    note(note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    }
                    /* Load/Save */
                    update(tripItinerary) {
                        this._name = tripItinerary._name;
                        this._note = tripItinerary._note;
                    }
                    loadFromDevice($q, tripItineraryResource) {
                        const deferred = $q.defer();
                        this._id = tripItineraryResource.Id;
                        this._name = tripItineraryResource.Name;
                        this._note = tripItineraryResource.Note;
                        deferred.resolve(this);
                        return deferred.promise;
                    }
                    saveToDevice($q) {
                        alert("TripItinerary.saveToDevice");
                        return $q.defer().promise;
                    }
                }
                Trips.TripItinerary = TripItinerary;
            })(Trips = Models.Trips || (Models.Trips = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Models/Trips/TripItinerary.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Resources;
        (function (Resources) {
            var Trips;
            (function (Trips) {
                "use strict";
            })(Trips = Resources.Trips || (Resources.Trips = {}));
        })(Resources = Mockup.Resources || (Mockup.Resources = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Resources/Trips/TripItineraryResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Services;
        (function (Services) {
            var Trips;
            (function (Trips) {
                "use strict";
                function tripItineraryServiceFactory($resource) {
                    return $resource("data/trips/itineraries.json", {}, {
                        query: { method: "GET", isArray: true }
                    });
                }
                Trips.tripItineraryServiceFactory = tripItineraryServiceFactory;
            })(Trips = Services.Trips || (Services.Trips = {}));
        })(Services = Mockup.Services || (Mockup.Services = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../Resources/Gear/GearItemResource.ts"/>
/// <reference path="../../AppState.ts"/>
/// <reference path="../Entry.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                class GearItem {
                    constructor() {
                        this._id = -1;
                        this._name = "";
                        this._url = "";
                        this._make = "";
                        this._model = "";
                        this._carried = "Carried";
                        this._weightInGrams = 0;
                        this._costInUSDP = 0;
                        this._isConsumable = false;
                        this._consumedPerDay = 1;
                        this._note = "";
                    }
                    get Id() {
                        return this._id;
                    }
                    set Id(id) {
                        this._id = id;
                    }
                    name(name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    }
                    url(url) {
                        return arguments.length
                            ? (this._url = url)
                            : this._url;
                    }
                    make(make) {
                        return arguments.length
                            ? (this._make = make)
                            : this._make;
                    }
                    model(model) {
                        return arguments.length
                            ? (this._model = model)
                            : this._model;
                    }
                    carried(carried) {
                        return arguments.length
                            ? (this._carried = carried)
                            : this._carried;
                    }
                    isCarried() {
                        return "NotCarried" != this._carried;
                    }
                    isWorn() {
                        return "Worn" == this._carried;
                    }
                    isConsumable(isConsumable) {
                        return arguments.length
                            ? (this._isConsumable = isConsumable)
                            : this._isConsumable;
                    }
                    consumedPerDay(consumedPerDay) {
                        return arguments.length
                            ? (this._consumedPerDay = consumedPerDay)
                            : this._consumedPerDay;
                    }
                    note(note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    }
                    /* Weight/Cost */
                    getWeightCategory() {
                        if (!this.isCarried()) {
                            return "None";
                        }
                        return Mockup.AppState.getInstance().getAppSettings().getWeightCategory(this._weightInGrams);
                    }
                    getWeightInGrams() {
                        return this._weightInGrams;
                    }
                    weightInUnits(weight) {
                        return arguments.length
                            ? (this._weightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                            : parseFloat(Mockup.convertGramsToUnits(this._weightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getCostInUSDP() {
                        return this._costInUSDP;
                    }
                    costInCurrency(cost) {
                        return arguments.length
                            ? (this._costInUSDP = Mockup.convertCurrencyToUSDP(cost, Mockup.AppState.getInstance().getAppSettings().currency()))
                            : Mockup.convertUSDPToCurrency(this._costInUSDP, Mockup.AppState.getInstance().getAppSettings().currency());
                    }
                    getCostPerUnitInCurrency() {
                        const weightInUnits = Mockup.convertGramsToUnits(this._weightInGrams, /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                        const costInCurrency = Mockup.convertUSDPToCurrency(this._costInUSDP, /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    }
                    /* Load/Save */
                    update(gearItem) {
                        this._name = gearItem._name;
                        this._url = gearItem._url;
                        this._make = gearItem._make;
                        this._model = gearItem._model;
                        this._carried = gearItem._carried;
                        this._weightInGrams = gearItem._weightInGrams;
                        this._costInUSDP = gearItem._costInUSDP;
                        this._isConsumable = gearItem._isConsumable;
                        this._consumedPerDay = gearItem._consumedPerDay;
                        this._note = gearItem._note;
                    }
                    loadFromDevice($q, gearItemResource) {
                        const deferred = $q.defer();
                        this._id = gearItemResource.Id;
                        this._name = gearItemResource.Name;
                        this._url = gearItemResource.Url;
                        this._make = gearItemResource.Make;
                        this._model = gearItemResource.Model;
                        this._carried = gearItemResource.Carried;
                        this._weightInGrams = gearItemResource.WeightInGrams;
                        this._costInUSDP = gearItemResource.CostInUSDP;
                        this._isConsumable = gearItemResource.IsConsumable;
                        this._consumedPerDay = gearItemResource.ConsumedPerDay;
                        this._note = gearItemResource.Note;
                        deferred.resolve(this);
                        return deferred.promise;
                    }
                    saveToDevice($q) {
                        alert("GearItem.saveToDevice");
                        return $q.defer().promise;
                    }
                }
                Gear.GearItem = GearItem;
                class GearItemEntry {
                    constructor(gearItemId, count) {
                        this._gearItemId = -1;
                        this._count = 1;
                        this._gearItemId = gearItemId;
                        if (count) {
                            this._count = count;
                        }
                    }
                    getGearItemId() {
                        return this._gearItemId;
                    }
                    count(count) {
                        return arguments.length
                            ? (this._count = count)
                            : this._count;
                    }
                    getName() {
                        const gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return "";
                        }
                        return gearItem.name();
                    }
                    isCarried() {
                        const gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return false;
                        }
                        return gearItem.isCarried();
                    }
                    isWorn() {
                        const gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return false;
                        }
                        return gearItem.isWorn();
                    }
                    isConsumable() {
                        const gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return false;
                        }
                        return gearItem.isConsumable();
                    }
                    getTotalWeightInGrams() {
                        const gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return 0;
                        }
                        return this._count * gearItem.getWeightInGrams();
                    }
                    getTotalWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams(), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getCostInUSDP() {
                        const gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return 0;
                        }
                        return this._count * gearItem.getCostInUSDP();
                    }
                    getCostInCurrency() {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    }
                }
                Gear.GearItemEntry = GearItemEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/underscore/underscore.d.ts" />
/// <reference path="../../Resources/Gear/GearSystemResource.ts"/>
/// <reference path="../../AppState.ts"/>
/// <reference path="../Entry.ts"/>
/// <reference path="GearItem.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                class GearSystem {
                    constructor() {
                        this._id = -1;
                        this._name = "";
                        this._note = "";
                        this._gearItems = [];
                    }
                    get Id() {
                        return this._id;
                    }
                    set Id(id) {
                        this._id = id;
                    }
                    name(name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    }
                    note(note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    }
                    /* Gear Items */
                    getGearItems() {
                        return this._gearItems;
                    }
                    getGearItemCount(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let count = 0;
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    }
                    getGearItemEntryIndexById(gearItemId) {
                        return _.findIndex(this._gearItems, (gearItemEntry) => {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    }
                    containsGearItemById(gearItemId) {
                        return undefined != _.find(this._gearItems, (gearSystemEntry) => {
                            return gearSystemEntry.getGearItemId() == gearItemId;
                        });
                    }
                    addGearItem(gearItem) {
                        if (this.containsGearItemById(gearItem.Id)) {
                            throw "The system already contains this item!";
                        }
                        this._gearItems.push(new Gear.GearItemEntry(gearItem.Id));
                    }
                    addGearItemEntry(gearItemId, count) {
                        if (this.containsGearItemById(gearItemId)) {
                            throw "The system already contains this item!";
                        }
                        this._gearItems.push(new Gear.GearItemEntry(gearItemId, count));
                    }
                    removeGearItemById(gearItemId) {
                        const idx = this.getGearItemEntryIndexById(gearItemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearItems.splice(idx, 1);
                        return true;
                    }
                    removeAllGearItems() {
                        this._gearItems = [];
                    }
                    /* Weight/Cost */
                    getTotalWeightInGrams(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            weightInGrams += gearItemEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    }
                    getTotalWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getBaseWeightInGrams(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried but not worn or consumable
                            if (gearItemEntry.isCarried() && !gearItemEntry.isWorn() && !gearItemEntry.isConsumable()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        return weightInGrams;
                    }
                    getBaseWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getPackWeightInGrams(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried or consumable but not worn
                            if (gearItemEntry.isCarried() && !gearItemEntry.isWorn() || gearItemEntry.isConsumable()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        return weightInGrams;
                    }
                    getPackWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getSkinOutWeightInGrams(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        const packWeightInGrams = this.getPackWeightInGrams(visitedGearItems);
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried, worn, and consumable gear items
                            if (gearItemEntry.isCarried()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        return packWeightInGrams + weightInGrams;
                    }
                    getSkinOutWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getCostInUSDP(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let costInUSDP = 0;
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            costInUSDP += gearItemEntry.getCostInUSDP();
                        }
                        return costInUSDP;
                    }
                    getCostInCurrency() {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    }
                    getCostPerUnitInCurrency() {
                        const weightInUnits = Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                        const costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    }
                    /* Load/Save */
                    update(gearSystem) {
                        this._name = gearSystem._name;
                        this._note = gearSystem._note;
                        this._gearItems = [];
                        for (let i = 0; i < gearSystem._gearItems.length; ++i) {
                            const gearItemEntry = gearSystem._gearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.getGearItemId(), gearItemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                    }
                    loadFromDevice($q, gearSystemResource) {
                        const deferred = $q.defer();
                        this._id = gearSystemResource.Id;
                        this._name = gearSystemResource.Name;
                        this._note = gearSystemResource.Note;
                        for (let i = 0; i < gearSystemResource.GearItems.length; ++i) {
                            const gearItemEntry = gearSystemResource.GearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    }
                    saveToDevice($q) {
                        alert("GearSystem.saveToDevice");
                        return $q.defer().promise;
                    }
                }
                Gear.GearSystem = GearSystem;
                class GearSystemEntry {
                    constructor(gearSystemId, count) {
                        this._gearSystemId = -1;
                        this._count = 1;
                        this._gearSystemId = gearSystemId;
                        if (count) {
                            this._count = count;
                        }
                    }
                    getGearSystemId() {
                        return this._gearSystemId;
                    }
                    count(count) {
                        return arguments.length
                            ? (this._count = count)
                            : this._count;
                    }
                    getName() {
                        const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return "";
                        }
                        return gearSystem.name();
                    }
                    getGearItemCount(visitedGearItems) {
                        const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getGearItemCount(visitedGearItems);
                    }
                    getTotalWeightInGrams(visitedGearItems) {
                        const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getTotalWeightInGrams(visitedGearItems);
                    }
                    getTotalWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getBaseWeightInGrams(visitedGearItems) {
                        const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getBaseWeightInGrams(visitedGearItems);
                    }
                    getBaseWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getPackWeightInGrams(visitedGearItems) {
                        const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getPackWeightInGrams(visitedGearItems);
                    }
                    getPackWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getSkinOutWeightInGrams(visitedGearItems) {
                        const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getSkinOutWeightInGrams(visitedGearItems);
                    }
                    getSkinOutWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getCostInUSDP(visitedGearItems) {
                        const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getCostInUSDP(visitedGearItems);
                    }
                    getCostInCurrency() {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    }
                }
                Gear.GearSystemEntry = GearSystemEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/underscore/underscore.d.ts" />
/// <reference path="../../Resources/Trips/TripPlanResource.ts"/>
/// <reference path="../../AppState.ts"/>
/// <reference path="../Gear/GearCollection.ts"/>
/// <reference path="../Gear/GearItem.ts"/>
/// <reference path="../Gear/GearSystem.ts"/>
/// <reference path="../Meals/Meal.ts"/>
/// <reference path="TripItinerary.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Trips;
            (function (Trips) {
                "use strict";
                class TripPlan {
                    constructor() {
                        this._id = -1;
                        this._name = "";
                        this._startDate = new Date();
                        this._endDate = new Date();
                        this._tripItineraryId = -1;
                        this._note = "";
                        this._gearCollections = [];
                        this._gearSystems = [];
                        this._gearItems = [];
                        this._meals = [];
                    }
                    get Id() {
                        return this._id;
                    }
                    set Id(id) {
                        this._id = id;
                    }
                    name(name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    }
                    startDate(startDate) {
                        return arguments.length
                            ? (this._startDate = startDate)
                            : this._startDate;
                    }
                    endDate(endDate) {
                        return arguments.length
                            ? (this._endDate = endDate)
                            : this._endDate;
                    }
                    getTripItineraryName() {
                        if (this._tripItineraryId < 1) {
                            return "No trip itinerary";
                        }
                        const tripItinerary = Mockup.AppState.getInstance().getTripState().getTripItineraryById(this._tripItineraryId);
                        if (!tripItinerary) {
                            return "Could not find trip itinerary";
                        }
                        return tripItinerary.name();
                    }
                    tripItineraryId(tripItineraryId) {
                        return arguments.length
                            ? (this._tripItineraryId = tripItineraryId)
                            : this._tripItineraryId;
                    }
                    note(note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    }
                    getTotalGearItemCount() {
                        const visitedGearItems = [];
                        let count = 0;
                        for (let i = 0; i < this._gearCollections.length; ++i) {
                            const gearCollectionEntry = this._gearCollections[i];
                            count += gearCollectionEntry.getGearItemCount(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            count += gearSystemEntry.getGearItemCount(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    }
                    getTotalCalories() {
                        const visitedMeals = [];
                        let calories = 0;
                        for (let i = 0; i < this._meals.length; ++i) {
                            const mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            calories += mealEntry.getCalories();
                        }
                        return calories;
                    }
                    /* Gear Collections */
                    getGearCollections() {
                        return this._gearCollections;
                    }
                    getGearCollectionCount(visitedGearCollections) {
                        if (!visitedGearCollections) {
                            visitedGearCollections = [];
                        }
                        let count = 0;
                        for (let i = 0; i < this._gearCollections.length; ++i) {
                            const gearCollectionEntry = this._gearCollections[i];
                            if (_.contains(visitedGearCollections, gearCollectionEntry.getGearCollectionId())) {
                                continue;
                            }
                            visitedGearCollections.push(gearCollectionEntry.getGearCollectionId());
                            count += gearCollectionEntry.count();
                        }
                        return count;
                    }
                    getGearCollectionEntryIndexById(gearCollectionId) {
                        return _.findIndex(this._gearCollections, (gearCollectionEntry) => {
                            return gearCollectionEntry.getGearCollectionId() == gearCollectionId;
                        });
                    }
                    containsGearCollectionById(gearCollectionId) {
                        return undefined != _.find(this._gearCollections, (gearCollectionEntry) => {
                            return gearCollectionEntry.getGearCollectionId() == gearCollectionId;
                        });
                    }
                    containsGearCollectionSystems(gearCollection) {
                        const gearSystems = gearCollection.getGearSystems();
                        for (let i = 0; i < gearSystems.length; ++i) {
                            const gearSystemEntry = gearSystems[i];
                            if (this.containsGearSystemById(gearSystemEntry.getGearSystemId())) {
                                return true;
                            }
                            const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(gearSystemEntry.getGearSystemId());
                            if (gearSystem && this.containsGearSystemItems(gearSystem)) {
                                return true;
                            }
                        }
                        return false;
                    }
                    containsGearCollectionItems(gearCollection) {
                        const gearItems = gearCollection.getGearItems();
                        for (let i = 0; i < gearItems.length; ++i) {
                            const gearItemEntry = gearItems[i];
                            if (this.containsGearItemById(gearItemEntry.getGearItemId())) {
                                return true;
                            }
                        }
                        return false;
                    }
                    addGearCollection(gearCollection) {
                        if (this.containsGearCollectionById(gearCollection.Id)) {
                            throw "The plan already contains this collection!";
                        }
                        if (this.containsGearCollectionSystems(gearCollection)) {
                            throw "The plan already contains systems from this collection!";
                        }
                        if (this.containsGearCollectionItems(gearCollection)) {
                            throw "The plan already contains items from this collection!";
                        }
                        this._gearCollections.push(new Models.Gear.GearCollectionEntry(gearCollection.Id));
                    }
                    addGearCollectionEntry(gearCollectionId, count) {
                        if (this.containsGearCollectionById(gearCollectionId)) {
                            throw "The plan already contains this collection!";
                        }
                        // TODO: prevent duplicates here
                        /*const gearCollection = AppState.getInstance().getGearState().getGearCollectionById(gearCollectionId);
                        if(!gearCollection) {
                            throw "The collection does not exist!";
                        }
            
                        if(this.containsGearCollectionSystems(gearCollection)) {
                            throw "The plan already contains systems from this collection!";
                        }
            
                        if(this.containsGearCollectionItems(gearCollection)) {
                            throw "The plan already contains items from this collection!";
                        }*/
                        this._gearCollections.push(new Models.Gear.GearCollectionEntry(gearCollectionId, count));
                    }
                    removeGearCollectionById(gearCollectionId) {
                        const idx = this.getGearCollectionEntryIndexById(gearCollectionId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearCollections.splice(idx, 1);
                        return true;
                    }
                    removeAllGearCollections() {
                        this._gearCollections = [];
                    }
                    /* Gear Systems */
                    getGearSystems() {
                        return this._gearSystems;
                    }
                    getGearSystemCount(visitedGearSystems) {
                        if (!visitedGearSystems) {
                            visitedGearSystems = [];
                        }
                        let count = 0;
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            if (_.contains(visitedGearSystems, gearSystemEntry.getGearSystemId())) {
                                continue;
                            }
                            visitedGearSystems.push(gearSystemEntry.getGearSystemId());
                            count += gearSystemEntry.count();
                        }
                        return count;
                    }
                    getGearSystemEntryIndexById(gearSystemId) {
                        return _.findIndex(this._gearSystems, (gearSystemEntry) => {
                            return gearSystemEntry.getGearSystemId() == gearSystemId;
                        });
                    }
                    containsGearSystemById(gearSystemId) {
                        if (_.find(this._gearCollections, (gearCollectionEntry) => {
                            const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(gearCollectionEntry.getGearCollectionId());
                            if (!gearCollection) {
                                return false;
                            }
                            return gearCollection.containsGearSystemById(gearSystemId);
                        })) {
                            return true;
                        }
                        return undefined != _.find(this._gearSystems, (gearSystemEntry) => {
                            return gearSystemEntry.getGearSystemId() == gearSystemId;
                        });
                    }
                    containsGearSystemItems(gearSystem) {
                        const gearItems = gearSystem.getGearItems();
                        for (let i = 0; i < gearItems.length; ++i) {
                            const gearItemEntry = gearItems[i];
                            if (this.containsGearItemById(gearItemEntry.getGearItemId())) {
                                return true;
                            }
                        }
                        return false;
                    }
                    addGearSystem(gearSystem) {
                        if (this.containsGearSystemById(gearSystem.Id)) {
                            throw "The plan already contains this system!";
                        }
                        if (this.containsGearSystemItems(gearSystem)) {
                            throw "The plan already contains items from this system!";
                        }
                        this._gearSystems.push(new Models.Gear.GearSystemEntry(gearSystem.Id));
                    }
                    addGearSystemEntry(gearSystemId, count) {
                        if (this.containsGearSystemById(gearSystemId)) {
                            throw "The plan already contains this system!";
                        }
                        /*const gearSystem = AppState.getInstance().getGearState().getGearSystemById(gearSystemId);
                        if(!gearSystem) {
                            throw "The system does not exist!";
                        }
            
                        if(this.containsGearSystemItems(gearSystem)) {
                            throw "The plan already contains items from this system!";
                        }*/
                        this._gearSystems.push(new Models.Gear.GearSystemEntry(gearSystemId, count));
                    }
                    removeGearSystemById(gearSystemId) {
                        const idx = this.getGearSystemEntryIndexById(gearSystemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearSystems.splice(idx, 1);
                        return true;
                    }
                    removeAllGearSystems() {
                        this._gearSystems = [];
                    }
                    /* Gear Items */
                    getGearItems() {
                        return this._gearItems;
                    }
                    getGearItemCount(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let count = 0;
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    }
                    getGearItemEntryIndexById(gearItemId) {
                        return _.findIndex(this._gearItems, (gearItemEntry) => {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    }
                    containsGearItemById(gearItemId) {
                        if (_.find(this._gearCollections, (gearCollectionEntry) => {
                            const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(gearCollectionEntry.getGearCollectionId());
                            if (!gearCollection) {
                                return false;
                            }
                            return gearCollection.containsGearItemById(gearItemId);
                        })) {
                            return true;
                        }
                        if (_.find(this._gearSystems, (gearSystemEntry) => {
                            const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(gearSystemEntry.getGearSystemId());
                            if (!gearSystem) {
                                return false;
                            }
                            return gearSystem.containsGearItemById(gearItemId);
                        })) {
                            return true;
                        }
                        return undefined != _.find(this._gearItems, (gearItemEntry) => {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    }
                    addGearItem(gearItem) {
                        if (this.containsGearItemById(gearItem.Id)) {
                            throw "The plan already contains this item!";
                        }
                        this._gearItems.push(new Models.Gear.GearItemEntry(gearItem.Id));
                    }
                    addGearItemEntry(gearItemId, count) {
                        if (this.containsGearItemById(gearItemId)) {
                            throw "The plan already contains this item!";
                        }
                        this._gearItems.push(new Models.Gear.GearItemEntry(gearItemId, count));
                    }
                    removeGearItemById(gearItemId) {
                        const idx = this.getGearItemEntryIndexById(gearItemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearItems.splice(idx, 1);
                        return true;
                    }
                    removeAllGearItems() {
                        this._gearItems = [];
                    }
                    /* Meals */
                    getMeals() {
                        return this._meals;
                    }
                    getMealCount() {
                        const visitedMeals = [];
                        let count = 0;
                        for (let i = 0; i < this._meals.length; ++i) {
                            const mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            count += mealEntry.count();
                        }
                        return count;
                    }
                    getMealEntryIndexById(mealId) {
                        return _.findIndex(this._meals, (mealEntry) => {
                            return mealEntry.getMealId() == mealId;
                        });
                    }
                    containsMealById(mealId) {
                        return undefined != _.find(this._meals, (mealEntry) => {
                            return mealEntry.getMealId() == mealId;
                        });
                    }
                    addMeal(meal) {
                        if (this.containsMealById(meal.Id)) {
                            return false;
                        }
                        this._meals.push(new Models.Meals.MealEntry(meal.Id));
                        return true;
                    }
                    addMealEntry(mealId, count) {
                        if (this.containsMealById(mealId)) {
                            throw "The plan already contains this meal!";
                        }
                        this._meals.push(new Models.Meals.MealEntry(mealId, count));
                    }
                    removeMealById(mealId) {
                        const idx = this.getMealEntryIndexById(mealId);
                        if (idx < 0) {
                            throw "The plan already contains this meal!";
                        }
                        this._meals.splice(idx, 1);
                    }
                    removeAllMeals() {
                        this._meals = [];
                    }
                    /* Weight/Cost */
                    getWeightClass() {
                        return Mockup.AppState.getInstance().getAppSettings().getWeightClass(this.getBaseWeightInGrams([]));
                    }
                    getTotalWeightInGrams(visitedGearItems, visitedMeals) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        if (!visitedMeals) {
                            visitedMeals = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearCollections.length; ++i) {
                            const gearCollectionEntry = this._gearCollections[i];
                            weightInGrams += gearCollectionEntry.getTotalWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getTotalWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            weightInGrams += gearItemEntry.getTotalWeightInGrams();
                        }
                        for (let i = 0; i < this._meals.length; ++i) {
                            const mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            weightInGrams += mealEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    }
                    getTotalWeightInUnits() {
                        return Mockup.convertGramsToUnits(this.getTotalWeightInGrams([], []), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    }
                    getBaseWeightInGrams(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearCollections.length; ++i) {
                            const gearCollectionEntry = this._gearCollections[i];
                            weightInGrams += gearCollectionEntry.getBaseWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getBaseWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried but not worn or consumable
                            if (gearItemEntry.isCarried() && !gearItemEntry.isWorn() && !gearItemEntry.isConsumable()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        return weightInGrams;
                    }
                    getBaseWeightInUnits() {
                        return Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    }
                    getPackWeightInGrams(visitedGearItems, visitedMeals) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        if (!visitedMeals) {
                            visitedMeals = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearCollections.length; ++i) {
                            const gearCollectionEntry = this._gearCollections[i];
                            weightInGrams += gearCollectionEntry.getPackWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getPackWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried or consumable but not worn
                            if (gearItemEntry.isCarried() && !gearItemEntry.isWorn() || gearItemEntry.isConsumable()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        for (let i = 0; i < this._meals.length; ++i) {
                            const mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            weightInGrams += mealEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    }
                    getPackWeightInUnits() {
                        return Mockup.convertGramsToUnits(this.getPackWeightInGrams([], []), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    }
                    getSkinOutWeightInGrams(visitedGearItems, visitedMeals) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        if (!visitedMeals) {
                            visitedMeals = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearCollections.length; ++i) {
                            const gearCollectionEntry = this._gearCollections[i];
                            weightInGrams += gearCollectionEntry.getSkinOutWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getSkinOutWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried, worn, and consumable gear items
                            if (gearItemEntry.isCarried()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        for (let i = 0; i < this._meals.length; ++i) {
                            const mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            weightInGrams += mealEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    }
                    getSkinOutWeightInUnits() {
                        return Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([], []), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    }
                    getCostInUSDP(visitedGearItems, visitedMeals) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let costInUSDP = 0;
                        for (let i = 0; i < this._gearCollections.length; ++i) {
                            const gearCollectionEntry = this._gearCollections[i];
                            costInUSDP += gearCollectionEntry.getCostInUSDP(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            costInUSDP += gearSystemEntry.getCostInUSDP(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            costInUSDP += gearItemEntry.getCostInUSDP();
                        }
                        for (let i = 0; i < this._meals.length; ++i) {
                            const mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            costInUSDP += mealEntry.getCostInUSDP();
                        }
                        return costInUSDP;
                    }
                    getCostInCurrency() {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([], []), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    }
                    getCostPerUnitInCurrency() {
                        const weightInUnits = Mockup.convertGramsToUnits(this.getTotalWeightInGrams([], []), Mockup.AppState.getInstance().getAppSettings().units());
                        const costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP([], []), Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    }
                    /* Load/Save */
                    update(tripPlan) {
                        this._name = tripPlan._name;
                        this._startDate = this._startDate;
                        this._endDate = this._endDate;
                        this._tripItineraryId = tripPlan._tripItineraryId;
                        this._note = tripPlan._note;
                        this._gearCollections = [];
                        for (let i = 0; i < tripPlan._gearCollections.length; ++i) {
                            const gearCollectionEntry = tripPlan._gearCollections[i];
                            try {
                                this.addGearCollectionEntry(gearCollectionEntry.getGearCollectionId(), gearCollectionEntry.count());
                            }
                            catch (error) {
                            }
                        }
                        this._gearSystems = [];
                        for (let i = 0; i < tripPlan._gearSystems.length; ++i) {
                            const gearSystemEntry = tripPlan._gearSystems[i];
                            try {
                                this.addGearSystemEntry(gearSystemEntry.getGearSystemId(), gearSystemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                        this._gearItems = [];
                        for (let i = 0; i < tripPlan._gearItems.length; ++i) {
                            const gearItemEntry = tripPlan._gearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.getGearItemId(), gearItemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                        this._meals = [];
                        for (let i = 0; i < tripPlan._meals.length; ++i) {
                            const mealEntry = tripPlan._meals[i];
                            try {
                                this.addMealEntry(mealEntry.getMealId(), mealEntry.count());
                            }
                            catch (error) {
                            }
                        }
                    }
                    loadFromDevice($q, tripPlanResource) {
                        const deferred = $q.defer();
                        this._id = tripPlanResource.Id;
                        this._name = tripPlanResource.Name;
                        this._startDate = new Date(tripPlanResource.StartDate);
                        this._endDate = new Date(tripPlanResource.EndDate);
                        this._tripItineraryId = tripPlanResource.TripItineraryId;
                        this._note = tripPlanResource.Note;
                        for (let i = 0; i < tripPlanResource.GearCollections.length; ++i) {
                            const gearCollectionEntry = tripPlanResource.GearCollections[i];
                            try {
                                this.addGearCollectionEntry(gearCollectionEntry.GearCollectionId, gearCollectionEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        for (let i = 0; i < tripPlanResource.GearSystems.length; ++i) {
                            const gearSystemEntry = tripPlanResource.GearSystems[i];
                            try {
                                this.addGearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        for (let i = 0; i < tripPlanResource.GearItems.length; ++i) {
                            const gearItemEntry = tripPlanResource.GearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        for (let i = 0; i < tripPlanResource.Meals.length; ++i) {
                            const mealEntry = tripPlanResource.Meals[i];
                            try {
                                this.addMealEntry(mealEntry.MealId, mealEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    }
                    saveToDevice($q) {
                        alert("TripPlan.saveToDevice");
                        return $q.defer().promise;
                    }
                }
                Trips.TripPlan = TripPlan;
            })(Trips = Models.Trips || (Models.Trips = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Models/Trips/TripPlan.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Resources;
        (function (Resources) {
            var Trips;
            (function (Trips) {
                "use strict";
            })(Trips = Resources.Trips || (Resources.Trips = {}));
        })(Resources = Mockup.Resources || (Mockup.Resources = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Resources/Trips/TripPlanResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Services;
        (function (Services) {
            var Trips;
            (function (Trips) {
                "use strict";
                function tripPlanServiceFactory($resource) {
                    return $resource("data/trips/plans.json", {}, {
                        query: { method: "GET", isArray: true }
                    });
                }
                Trips.tripPlanServiceFactory = tripPlanServiceFactory;
            })(Trips = Services.Trips || (Services.Trips = {}));
        })(Services = Mockup.Services || (Mockup.Services = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../../Resources/Personal/UserInformationResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Services;
        (function (Services) {
            var Personal;
            (function (Personal) {
                "use strict";
                function userInformationServiceFactory($resource) {
                    return $resource("data/user.json", {}, {
                        get: { method: "GET", isArray: false }
                    });
                }
                Personal.userInformationServiceFactory = userInformationServiceFactory;
            })(Personal = Services.Personal || (Services.Personal = {}));
        })(Services = Mockup.Services || (Mockup.Services = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
/// <reference path="../Resources/AppSettingsResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Services;
        (function (Services) {
            "use strict";
            function appSettingsServiceFactory($resource) {
                return $resource("data/settings.json", {}, {
                    get: { method: "GET", isArray: false }
                });
            }
            Services.appSettingsServiceFactory = appSettingsServiceFactory;
        })(Services = Mockup.Services || (Mockup.Services = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="Models/Gear/GearCollection.ts" />
/// <reference path="Models/Gear/GearItem.ts" />
/// <reference path="Models/Gear/GearSystem.ts" />
/// <reference path="Resources/Gear/GearCollectionResource.ts" />
/// <reference path="Resources/Gear/GearItemResource.ts" />
/// <reference path="Resources/Gear/GearSystemResource.ts" />
/// <reference path="Services/Gear/GearCollectionService.ts"/>
/// <reference path="Services/Gear/GearItemService.ts"/>
/// <reference path="Services/Gear/GearSystemService.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        class GearState {
            constructor() {
                /* Gear Items */
                this._gearItems = [];
                /* Gear Systems */
                this._gearSystems = [];
                /* Gear Collections */
                this._gearCollections = [];
            }
            // TODO: this should be a read-only collection
            getGearItems() {
                return this._gearItems;
            }
            getGearItemIndexById(gearItemId) {
                for (let i = 0; i < this._gearItems.length; ++i) {
                    const gearItem = this._gearItems[i];
                    if (gearItem.Id == gearItemId) {
                        return i;
                    }
                }
                return -1;
            }
            getGearItemById(gearItemId) {
                const idx = this.getGearItemIndexById(gearItemId);
                return idx < 0 ? null : this._gearItems[idx];
            }
            getNextGearItemId() {
                return ++GearState._lastGearItemId;
            }
            addGearItem(gearItem) {
                if (gearItem.Id < 0) {
                    gearItem.Id = this.getNextGearItemId();
                }
                else if (gearItem.Id > GearState._lastGearItemId) {
                    GearState._lastGearItemId = gearItem.Id;
                }
                this._gearItems.push(gearItem);
                return gearItem.Id;
            }
            deleteGearItem(gearItem) {
                const idx = this.getGearItemIndexById(gearItem.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearItems.splice(idx, 1);
                return true;
            }
            deleteAllGearItems() {
                this._gearItems = [];
            }
            // TODO: this should be a read-only collection
            getGearSystems() {
                return this._gearSystems;
            }
            getGearSystemIndexById(gearSystemId) {
                for (let i = 0; i < this._gearSystems.length; ++i) {
                    const gearSystem = this._gearSystems[i];
                    if (gearSystem.Id == gearSystemId) {
                        return i;
                    }
                }
                return -1;
            }
            getGearSystemById(gearSystemId) {
                const idx = this.getGearSystemIndexById(gearSystemId);
                return idx < 0 ? null : this._gearSystems[idx];
            }
            getNextGearSystemId() {
                return ++GearState._lastGearSystemId;
            }
            addGearSystem(gearSystem) {
                if (gearSystem.Id < 0) {
                    gearSystem.Id = this.getNextGearSystemId();
                }
                else if (gearSystem.Id > GearState._lastGearSystemId) {
                    GearState._lastGearSystemId = gearSystem.Id;
                }
                this._gearSystems.push(gearSystem);
                return gearSystem.Id;
            }
            removeGearItemFromSystems(gearItem) {
                const gearSystems = [];
                for (let i = 0; i < this._gearSystems.length; ++i) {
                    const gearSystem = this._gearSystems[i];
                    if (gearSystem.containsGearItemById(gearItem.Id)) {
                        gearSystem.removeGearItemById(gearItem.Id);
                        gearSystems.push(gearSystem);
                    }
                }
                return gearSystems;
            }
            deleteGearSystem(gearSystem) {
                const idx = this.getGearSystemIndexById(gearSystem.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearSystems.splice(idx, 1);
                return true;
            }
            deleteAllGearSystems() {
                this._gearSystems = [];
            }
            // TODO: this should be a read-only collection
            getGearCollections() {
                return this._gearCollections;
            }
            getGearCollectionIndexById(gearCollectionId) {
                for (let i = 0; i < this._gearCollections.length; ++i) {
                    const gearCollection = this._gearCollections[i];
                    if (gearCollection.Id == gearCollectionId) {
                        return i;
                    }
                }
                return -1;
            }
            getGearCollectionById(gearCollectionId) {
                const idx = this.getGearCollectionIndexById(gearCollectionId);
                return idx < 0 ? null : this._gearCollections[idx];
            }
            getNextGearCollectionId() {
                return ++GearState._lastGearCollectionId;
            }
            addGearCollection(gearCollection) {
                if (gearCollection.Id < 0) {
                    gearCollection.Id = this.getNextGearCollectionId();
                }
                else if (gearCollection.Id > GearState._lastGearCollectionId) {
                    GearState._lastGearCollectionId = gearCollection.Id;
                }
                this._gearCollections.push(gearCollection);
                return gearCollection.Id;
            }
            removeGearSystemFromCollections(gearSystem) {
                const gearCollections = [];
                for (let i = 0; i < this._gearCollections.length; ++i) {
                    const gearCollection = this._gearCollections[i];
                    if (gearCollection.containsGearSystemById(gearSystem.Id)) {
                        gearCollection.removeGearSystemById(gearSystem.Id);
                        gearCollections.push(gearCollection);
                    }
                }
                return gearCollections;
            }
            removeGearItemFromCollections(gearItem) {
                const gearCollections = [];
                for (let i = 0; i < this._gearCollections.length; ++i) {
                    const gearCollection = this._gearCollections[i];
                    if (gearCollection.containsGearItemById(gearItem.Id)) {
                        gearCollection.removeGearItemById(gearItem.Id);
                        gearCollections.push(gearCollection);
                    }
                }
                return gearCollections;
            }
            deleteGearCollection(gearCollection) {
                const idx = this.getGearCollectionIndexById(gearCollection.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearCollections.splice(idx, 1);
                return true;
            }
            deleteAllGearCollections() {
                this._gearCollections = [];
            }
            /* Utilities */
            deleteAllData() {
                this.deleteAllGearCollections();
                this.deleteAllGearSystems();
                this.deleteAllGearItems();
            }
            /* Load/Save */
            loadGearItems($q, gearItemResources) {
                const promises = [];
                this._gearItems = [];
                for (let i = 0; i < gearItemResources.length; ++i) {
                    const gearItem = new Mockup.Models.Gear.GearItem();
                    promises.push(gearItem.loadFromDevice($q, gearItemResources[i]).then((loadedGearItem) => {
                        this.addGearItem(loadedGearItem);
                    }));
                }
                return $q.all(promises);
            }
            loadGearSystems($q, gearSystemResources) {
                const promises = [];
                this._gearSystems = [];
                for (let i = 0; i < gearSystemResources.length; ++i) {
                    const gearSystem = new Mockup.Models.Gear.GearSystem();
                    promises.push(gearSystem.loadFromDevice($q, gearSystemResources[i]).then((loadedGearSystem) => {
                        this.addGearSystem(loadedGearSystem);
                    }));
                }
                return $q.all(promises);
            }
            loadGearCollections($q, gearCollectionResources) {
                const promises = [];
                this._gearCollections = [];
                for (let i = 0; i < gearCollectionResources.length; ++i) {
                    const gearCollection = new Mockup.Models.Gear.GearCollection();
                    promises.push(gearCollection.loadFromDevice($q, gearCollectionResources[i]).then((loadedGearCollection) => {
                        this.addGearCollection(loadedGearCollection);
                    }));
                }
                return $q.all(promises);
            }
            loadFromDevice($q, gearItemService, gearSystemService, gearCollectionService) {
                const promises = [];
                promises.push(gearItemService.query().$promise.then((gearItemResources) => {
                    this.loadGearItems($q, gearItemResources).then(() => {
                    });
                }));
                promises.push(gearSystemService.query().$promise.then((gearSystemResources) => {
                    this.loadGearSystems($q, gearSystemResources).then(() => {
                    });
                }));
                promises.push(gearCollectionService.query().$promise.then((gearCollectionResources) => {
                    this.loadGearCollections($q, gearCollectionResources).then(() => {
                    });
                }));
                return $q.all(promises);
            }
            saveToDevice($q) {
                alert("GearState.saveToDevice");
                return $q.defer().promise;
            }
        }
        GearState._lastGearItemId = 0;
        GearState._lastGearSystemId = 0;
        GearState._lastGearCollectionId = 0;
        Mockup.GearState = GearState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="Models/Meals/Meal.ts" />
/// <reference path="Resources/Meals/MealResource.ts" />
/// <reference path="Services/Meals/MealService.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        class MealState {
            constructor() {
                /* Meals */
                this._meals = [];
            }
            // TODO: this should be a read-only collection
            getMeals() {
                return this._meals;
            }
            getMealIndexById(mealId) {
                for (let i = 0; i < this._meals.length; ++i) {
                    const meal = this._meals[i];
                    if (meal.Id == mealId) {
                        return i;
                    }
                }
                return -1;
            }
            getMealById(mealId) {
                const idx = this.getMealIndexById(mealId);
                return idx < 0 ? null : this._meals[idx];
            }
            getNextMealId() {
                return ++MealState._lastMealId;
            }
            addMeal(meal) {
                if (meal.Id < 0) {
                    meal.Id = this.getNextMealId();
                }
                else if (meal.Id > MealState._lastMealId) {
                    MealState._lastMealId = meal.Id;
                }
                this._meals.push(meal);
                return meal.Id;
            }
            deleteMeal(meal) {
                const idx = this.getMealIndexById(meal.Id);
                if (idx < 0) {
                    return false;
                }
                this._meals.splice(idx, 1);
                return true;
            }
            deleteAllMeals() {
                this._meals = [];
            }
            /* Utilities */
            deleteAllData() {
                this.deleteAllMeals();
            }
            /* Load/Save */
            loadMeals($q, mealResources) {
                const promises = [];
                this._meals = [];
                for (let i = 0; i < mealResources.length; ++i) {
                    const meal = new Mockup.Models.Meals.Meal();
                    promises.push(meal.loadFromDevice($q, mealResources[i]).then((loadedMeal) => {
                        this.addMeal(loadedMeal);
                    }));
                }
                return $q.all(promises);
            }
            loadFromDevice($q, mealService) {
                const promises = [];
                promises.push(mealService.query().$promise.then((mealResources) => {
                    this.loadMeals($q, mealResources).then(() => {
                    });
                }));
                return $q.all(promises);
            }
            saveToDevice($q) {
                alert("MealState.saveToDevice");
                return $q.defer().promise;
            }
        }
        MealState._lastMealId = 0;
        Mockup.MealState = MealState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="Models/Trips/TripItinerary.ts" />
/// <reference path="Models/Trips/TripPlan.ts" />
/// <reference path="Resources/Trips/TripItineraryResource.ts" />
/// <reference path="Resources/Trips/TripPlanResource.ts" />
/// <reference path="Services/Trips/TripItineraryService.ts" />
/// <reference path="Services/Trips/TripPlanService.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        class TripState {
            constructor() {
                /* Trip Itineraries */
                this._tripItineraries = [];
                /* Trip Plans */
                this._tripPlans = [];
            }
            // TODO: this should be a read-only collection
            getTripItineraries() {
                return this._tripItineraries;
            }
            getTripItineraryIndexById(tripItineraryId) {
                for (let i = 0; i < this._tripItineraries.length; ++i) {
                    const tripItinerary = this._tripItineraries[i];
                    if (tripItinerary.Id == tripItineraryId) {
                        return i;
                    }
                }
                return -1;
            }
            getTripItineraryById(tripItineraryId) {
                const idx = this.getTripItineraryIndexById(tripItineraryId);
                return idx < 0 ? null : this._tripItineraries[idx];
            }
            getNextTripItineraryId() {
                return ++TripState._lastTripItineraryId;
            }
            addTripItinerary(tripItinerary) {
                if (tripItinerary.Id < 0) {
                    tripItinerary.Id = this.getNextTripItineraryId();
                }
                else if (tripItinerary.Id > TripState._lastTripItineraryId) {
                    TripState._lastTripItineraryId = tripItinerary.Id;
                }
                this._tripItineraries.push(tripItinerary);
                return tripItinerary.Id;
            }
            deleteTripItinerary(tripItinerary) {
                const idx = this.getTripItineraryIndexById(tripItinerary.Id);
                if (idx < 0) {
                    return false;
                }
                this._tripItineraries.splice(idx, 1);
                return true;
            }
            deleteAllTripItineraries() {
                this._tripItineraries = [];
            }
            // TODO: this should be a read-only collection
            getTripPlans() {
                return this._tripPlans;
            }
            getTripPlanIndexById(tripPlanId) {
                for (let i = 0; i < this._tripPlans.length; ++i) {
                    const tripPlan = this._tripPlans[i];
                    if (tripPlan.Id == tripPlanId) {
                        return i;
                    }
                }
                return -1;
            }
            getTripPlanById(tripPlanId) {
                const idx = this.getTripPlanIndexById(tripPlanId);
                return idx < 0 ? null : this._tripPlans[idx];
            }
            getNextTripPlanId() {
                return ++TripState._lastTripPlanId;
            }
            addTripPlan(tripPlan) {
                if (tripPlan.Id < 0) {
                    tripPlan.Id = this.getNextTripPlanId();
                }
                else if (tripPlan.Id > TripState._lastTripPlanId) {
                    TripState._lastTripPlanId = tripPlan.Id;
                }
                this._tripPlans.push(tripPlan);
                return tripPlan.Id;
            }
            removeGearCollectionFromPlans(gearCollection) {
                const tripPlans = [];
                for (let i = 0; i < this._tripPlans.length; ++i) {
                    const tripPlan = this._tripPlans[i];
                    if (tripPlan.containsGearCollectionById(tripPlan.Id)) {
                        tripPlan.removeGearCollectionById(tripPlan.Id);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            }
            removeGearSystemFromPlans(gearSystem) {
                const tripPlans = [];
                for (let i = 0; i < this._tripPlans.length; ++i) {
                    const tripPlan = this._tripPlans[i];
                    if (tripPlan.containsGearSystemById(gearSystem.Id)) {
                        tripPlan.removeGearSystemById(gearSystem.Id);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            }
            removeGearItemFromPlans(gearItem) {
                const tripPlans = [];
                for (let i = 0; i < this._tripPlans.length; ++i) {
                    const tripPlan = this._tripPlans[i];
                    if (tripPlan.containsGearItemById(gearItem.Id)) {
                        tripPlan.removeGearItemById(gearItem.Id);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            }
            removeMealFromPlans(meal) {
                const tripPlans = [];
                for (let i = 0; i < this._tripPlans.length; ++i) {
                    const tripPlan = this._tripPlans[i];
                    if (tripPlan.containsMealById(tripPlan.Id)) {
                        tripPlan.removeMealById(tripPlan.Id);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            }
            removeTripItineraryFromPlans(tripItinerary) {
                const tripPlans = [];
                for (let i = 0; i < this._tripPlans.length; ++i) {
                    const tripPlan = this._tripPlans[i];
                    if (tripPlan.tripItineraryId() == tripItinerary.Id) {
                        tripPlan.tripItineraryId(-1);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            }
            deleteTripPlan(tripPlan) {
                const idx = this.getTripPlanIndexById(tripPlan.Id);
                if (idx < 0) {
                    return false;
                }
                this._tripPlans.splice(idx, 1);
                return true;
            }
            deleteAllTripPlans() {
                this._tripPlans = [];
            }
            /* Utilities */
            deleteAllData() {
                this.deleteAllTripItineraries();
                this.deleteAllTripPlans();
            }
            /* Load/Save */
            loadTripItineraries($q, tripItineraryResources) {
                const promises = [];
                this._tripItineraries = [];
                for (let i = 0; i < tripItineraryResources.length; ++i) {
                    const tripItinerary = new Mockup.Models.Trips.TripItinerary();
                    promises.push(tripItinerary.loadFromDevice($q, tripItineraryResources[i]).then((loadedTripItinerary) => {
                        this.addTripItinerary(loadedTripItinerary);
                    }));
                }
                return $q.all(promises);
            }
            loadTripPlans($q, tripPlanResources) {
                const promises = [];
                this._tripPlans = [];
                for (let i = 0; i < tripPlanResources.length; ++i) {
                    const tripPlan = new Mockup.Models.Trips.TripPlan();
                    promises.push(tripPlan.loadFromDevice($q, tripPlanResources[i]).then((loadedTripPlan) => {
                        this.addTripPlan(loadedTripPlan);
                    }));
                }
                return $q.all(promises);
            }
            loadFromDevice($q, tripItineraryService, tripPlanService) {
                const promises = [];
                promises.push(tripItineraryService.query().$promise.then((tripItineraryResources) => {
                    this.loadTripItineraries($q, tripItineraryResources).then(() => {
                    });
                }));
                promises.push(tripPlanService.query().$promise.then((tripPlanResources) => {
                    this.loadTripPlans($q, tripPlanResources).then(() => {
                    });
                }));
                return $q.all(promises);
            }
            saveToDevice($q) {
                alert("TripState.saveToDevice");
                return $q.defer().promise;
            }
        }
        TripState._lastTripItineraryId = 0;
        TripState._lastTripPlanId = 0;
        Mockup.TripState = TripState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="Actions/Command.ts"/>
/// <reference path="Models/Personal/UserInformation.ts" />
/// <reference path="Models/AppSettings.ts" />
/// <reference path="Resources/Personal/UserInformationResource.ts" />
/// <reference path="Resources/AppSettingsResource.ts" />
/// <reference path="Services/Gear/GearCollectionService.ts"/>
/// <reference path="Services/Gear/GearItemService.ts"/>
/// <reference path="Services/Gear/GearSystemService.ts"/>
/// <reference path="Services/Meals/MealService.ts"/>
/// <reference path="Services/Trips/TripItineraryService.ts"/>
/// <reference path="Services/Trips/TripPlanService.ts"/>
/// <reference path="Services/Personal/UserInformationService.ts"/>
/// <reference path="Services/AppSettingsService.ts"/>
/// <reference path="GearState.ts" />
/// <reference path="MealState.ts" />
/// <reference path="TripState.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        class AppState {
            constructor() {
                /* App Settings */
                this._appSettings = new Mockup.Models.AppSettings();
                /* User Information */
                this._userInformation = new Mockup.Models.Personal.UserInformation();
                /* Gear State */
                this._gearState = new Mockup.GearState();
                /* Meal State */
                this._mealState = new Mockup.MealState();
                /* Trip State */
                this._tripState = new Mockup.TripState();
                if (AppState._instance) {
                    throw new Error("Error: AppState already instantiated!");
                }
            }
            static getInstance() {
                return AppState._instance;
            }
            executeAction(action) {
                this._lastAction = action;
                this._lastAction.doAction();
            }
            undoAction() {
                if (!this._lastAction) {
                    return;
                }
                this._lastAction.undoAction();
                this._lastAction = undefined;
            }
            getAppSettings() {
                return this._appSettings;
            }
            getUserInformation() {
                return this._userInformation;
            }
            getGearState() {
                return this._gearState;
            }
            getMealState() {
                return this._mealState;
            }
            getTripState() {
                return this._tripState;
            }
            /* Utilities */
            deleteAllData() {
                this._gearState.deleteAllData();
                this._mealState.deleteAllData();
                this._tripState.deleteAllData();
            }
            /* Load/Save */
            loadFromDevice($q, appSettingsService, userInformationService, gearItemService, gearSystemService, gearCollectionService, mealService, tripItineraryService, tripPlanService) {
                const promises = [];
                // load the application settings
                promises.push(appSettingsService.get().$promise.then((appSettingsResource) => {
                    this._appSettings.loadFromDevice($q, appSettingsResource).then(() => {
                    });
                }));
                // load the user's personal information
                promises.push(userInformationService.get().$promise.then((userInfoResource) => {
                    this._userInformation.loadFromDevice($q, userInfoResource).then(() => {
                    });
                }));
                promises.push(this._gearState.loadFromDevice($q, gearItemService, gearSystemService, gearCollectionService));
                promises.push(this._mealState.loadFromDevice($q, mealService));
                promises.push(this._tripState.loadFromDevice($q, tripItineraryService, tripPlanService));
                return $q.all(promises);
            }
            saveToDevice($q) {
                alert("AppState.saveToDevice");
                return $q.defer().promise;
            }
            /* Import/Export */
            importFromCloudStorage($q, cloudStorage) {
                // mockup does nothing here
                return $q.defer().promise;
            }
            exportToCloudStorage($q, cloudStorage) {
                // mockup does nothing here
                return $q.defer().promise;
            }
        }
        /* Singleton */
        AppState._instance = new AppState();
        Mockup.AppState = AppState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/underscore/underscore.d.ts" />
/// <reference path="../../Resources/Gear/GearCollectionResource.ts"/>
/// <reference path="../../AppState.ts"/>
/// <reference path="../Entry.ts"/>
/// <reference path="GearItem.ts"/>
/// <reference path="GearSystem.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                class GearCollection {
                    constructor() {
                        this._id = -1;
                        this._name = "";
                        this._note = "";
                        this._gearSystems = [];
                        this._gearItems = [];
                    }
                    get Id() {
                        return this._id;
                    }
                    set Id(id) {
                        this._id = id;
                    }
                    name(name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    }
                    note(note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    }
                    getTotalGearItemCount() {
                        const visitedGearItems = [];
                        let count = 0;
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            count += gearSystemEntry.getGearItemCount(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    }
                    /* Gear Systems */
                    getGearSystems() {
                        return this._gearSystems;
                    }
                    getGearSystemCount(visitedGearSystems) {
                        if (!visitedGearSystems) {
                            visitedGearSystems = [];
                        }
                        let count = 0;
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            if (_.contains(visitedGearSystems, gearSystemEntry.getGearSystemId())) {
                                continue;
                            }
                            visitedGearSystems.push(gearSystemEntry.getGearSystemId());
                            count += gearSystemEntry.count();
                        }
                        return count;
                    }
                    getGearSystemEntryIndexById(gearSystemId) {
                        return _.findIndex(this._gearSystems, (gearSystemEntry) => {
                            return gearSystemEntry.getGearSystemId() == gearSystemId;
                        });
                    }
                    containsGearSystemById(gearSystemId) {
                        return undefined != _.find(this._gearSystems, (gearSystemEntry) => {
                            return gearSystemEntry.getGearSystemId() == gearSystemId;
                        });
                    }
                    containsGearSystemItems(gearSystem) {
                        const gearItems = gearSystem.getGearItems();
                        for (let i = 0; i < gearItems.length; ++i) {
                            const gearItemEntry = gearItems[i];
                            if (this.containsGearItemById(gearItemEntry.getGearItemId())) {
                                return true;
                            }
                        }
                        return false;
                    }
                    addGearSystem(gearSystem) {
                        if (this.containsGearSystemById(gearSystem.Id)) {
                            throw "The collection already contains this system!";
                        }
                        if (this.containsGearSystemItems(gearSystem)) {
                            throw "The collection already contains items from this system!";
                        }
                        this._gearSystems.push(new Gear.GearSystemEntry(gearSystem.Id));
                    }
                    addGearSystemEntry(gearSystemId, count) {
                        if (this.containsGearSystemById(gearSystemId)) {
                            throw "The collection already contains this system!";
                        }
                        // TODO: prevent duplicates here
                        /*const gearSystem = AppState.getInstance().getGearState().getGearSystemById(gearSystemId);
                        if(!gearSystem) {
                            throw "The system does not exist!";
                        }
            
                        if(this.containsGearSystemItems(gearSystem)) {
                            throw "The collection already contains items from this system!";
                        }*/
                        this._gearSystems.push(new Gear.GearSystemEntry(gearSystemId, count));
                    }
                    removeGearSystemById(gearSystemId) {
                        const idx = this.getGearSystemEntryIndexById(gearSystemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearSystems.splice(idx, 1);
                        return true;
                    }
                    removeAllGearSystems() {
                        this._gearSystems = [];
                    }
                    /* Gear Items */
                    getGearItems() {
                        return this._gearItems;
                    }
                    getGearItemCount(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let count = 0;
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    }
                    getGearItemEntryIndexById(gearItemId) {
                        return _.findIndex(this._gearItems, (gearItemEntry) => {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    }
                    containsGearItemById(gearItemId) {
                        if (_.find(this._gearSystems, (gearSystemEntry) => {
                            const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(gearSystemEntry.getGearSystemId());
                            if (!gearSystem) {
                                return false;
                            }
                            return gearSystem.containsGearItemById(gearItemId);
                        })) {
                            return true;
                        }
                        return undefined != _.find(this._gearItems, (gearItemEntry) => {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    }
                    addGearItem(gearItem) {
                        if (this.containsGearItemById(gearItem.Id)) {
                            throw "The collection already contains this item!";
                        }
                        this._gearItems.push(new Gear.GearItemEntry(gearItem.Id));
                    }
                    addGearItemEntry(gearItemId, count) {
                        if (this.containsGearItemById(gearItemId)) {
                            throw "The collection already contains this item!";
                        }
                        this._gearItems.push(new Gear.GearItemEntry(gearItemId, count));
                    }
                    removeGearItemById(gearItemId) {
                        const idx = this.getGearItemEntryIndexById(gearItemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearItems.splice(idx, 1);
                        return true;
                    }
                    removeAllGearItems() {
                        this._gearItems = [];
                    }
                    /* Weight/Cost */
                    getTotalWeightInGrams(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getTotalWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            weightInGrams += gearItemEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    }
                    getTotalWeightInUnits() {
                        return Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    }
                    getBaseWeightInGrams(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getBaseWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried but not worn or consumable
                            if (gearItemEntry.isCarried() && !gearItemEntry.isWorn() && !gearItemEntry.isConsumable()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        return weightInGrams;
                    }
                    getBaseWeightInUnits() {
                        return Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    }
                    getPackWeightInGrams(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getPackWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried or consumable but not worn
                            if (gearItemEntry.isCarried() && !gearItemEntry.isWorn() || gearItemEntry.isConsumable()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        return weightInGrams;
                    }
                    getPackWeightInUnits() {
                        return Mockup.convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    }
                    getSkinOutWeightInGrams(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let weightInGrams = 0;
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getSkinOutWeightInGrams(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried, worn, and consumable gear items
                            if (gearItemEntry.isCarried()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        return weightInGrams;
                    }
                    getSkinOutWeightInUnits() {
                        return Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    }
                    getCostInUSDP(visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        let costInUSDP = 0;
                        for (let i = 0; i < this._gearSystems.length; ++i) {
                            const gearSystemEntry = this._gearSystems[i];
                            costInUSDP += gearSystemEntry.getCostInUSDP(visitedGearItems);
                        }
                        for (let i = 0; i < this._gearItems.length; ++i) {
                            const gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            costInUSDP += gearItemEntry.getCostInUSDP();
                        }
                        return costInUSDP;
                    }
                    getCostInCurrency() {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    }
                    getCostPerUnitInCurrency() {
                        const weightInUnits = Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                        const costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    }
                    /* Load/Save */
                    update(gearCollection) {
                        this._name = gearCollection._name;
                        this._note = gearCollection._note;
                        this._gearSystems = [];
                        for (let i = 0; i < gearCollection._gearSystems.length; ++i) {
                            const gearSystemEntry = gearCollection._gearSystems[i];
                            try {
                                this.addGearSystemEntry(gearSystemEntry.getGearSystemId(), gearSystemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                        this._gearItems = [];
                        for (let i = 0; i < gearCollection._gearItems.length; ++i) {
                            const gearItemEntry = gearCollection._gearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.getGearItemId(), gearItemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                    }
                    loadFromDevice($q, gearCollectionResource) {
                        const deferred = $q.defer();
                        this._id = gearCollectionResource.Id;
                        this._name = gearCollectionResource.Name;
                        this._note = gearCollectionResource.Note;
                        for (let i = 0; i < gearCollectionResource.GearSystems.length; ++i) {
                            const gearSystemEntry = gearCollectionResource.GearSystems[i];
                            try {
                                this.addGearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        for (let i = 0; i < gearCollectionResource.GearItems.length; ++i) {
                            const gearItemEntry = gearCollectionResource.GearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    }
                    saveToDevice($q) {
                        alert("GearCollection.saveToDevice");
                        return $q.defer().promise;
                    }
                }
                Gear.GearCollection = GearCollection;
                class GearCollectionEntry {
                    constructor(gearCollectionId, count) {
                        this._gearCollectionId = -1;
                        this._count = 1;
                        this._gearCollectionId = gearCollectionId;
                        if (count) {
                            this._count = count;
                        }
                    }
                    getGearCollectionId() {
                        return this._gearCollectionId;
                    }
                    count(count) {
                        return arguments.length
                            ? (this._count = count)
                            : this._count;
                    }
                    getName() {
                        const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return "";
                        }
                        return gearCollection.name();
                    }
                    getTotalGearItemCount() {
                        const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getTotalGearItemCount();
                    }
                    getGearSystemCount(visitedGearSystems) {
                        const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getGearSystemCount(visitedGearSystems);
                    }
                    getGearItemCount(visitedGearItems) {
                        const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getGearItemCount(visitedGearItems);
                    }
                    getTotalWeightInGrams(visitedGearItems) {
                        const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getTotalWeightInGrams(visitedGearItems);
                    }
                    getTotalWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getBaseWeightInGrams(visitedGearItems) {
                        const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getBaseWeightInGrams(visitedGearItems);
                    }
                    getBaseWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getPackWeightInGrams(visitedGearItems) {
                        const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getPackWeightInGrams(visitedGearItems);
                    }
                    getPackWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getSkinOutWeightInGrams(visitedGearItems) {
                        const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getSkinOutWeightInGrams(visitedGearItems);
                    }
                    getSkinOutWeightInUnits() {
                        return parseFloat(Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    }
                    getCostInUSDP(visitedGearItems) {
                        const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getCostInUSDP(visitedGearItems);
                    }
                    getCostInCurrency() {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    }
                }
                Gear.GearCollectionEntry = GearCollectionEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../Models/Gear/GearCollection.ts"/>
/// <reference path="../../Command.ts"/>
/// <reference path="../../../AppState.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Actions;
        (function (Actions) {
            var Gear;
            (function (Gear) {
                var Collections;
                (function (Collections) {
                    "use strict";
                    class DeleteGearCollectionAction {
                        constructor() {
                            this._tripPlans = [];
                        }
                        doAction() {
                            this._tripPlans = Mockup.AppState.getInstance().getTripState().removeGearCollectionFromPlans(this.GearCollection);
                            Mockup.AppState.getInstance().getGearState().deleteGearCollection(this.GearCollection);
                        }
                        undoAction() {
                            Mockup.AppState.getInstance().getGearState().addGearCollection(this.GearCollection);
                            for (let i = 0; i < this._tripPlans.length; ++i) {
                                const tripPlan = this._tripPlans[i];
                                tripPlan.addGearCollection(this.GearCollection);
                            }
                        }
                    }
                    Collections.DeleteGearCollectionAction = DeleteGearCollectionAction;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Actions.Gear || (Actions.Gear = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../Models/Gear/GearItem.ts"/>
/// <reference path="../../Command.ts"/>
/// <reference path="../../../AppState.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Actions;
        (function (Actions) {
            var Gear;
            (function (Gear) {
                var Items;
                (function (Items) {
                    "use strict";
                    class DeleteGearItemAction {
                        constructor() {
                            this._gearSystems = [];
                            this._gearCollections = [];
                            this._tripPlans = [];
                        }
                        doAction() {
                            this._tripPlans = Mockup.AppState.getInstance().getTripState().removeGearItemFromPlans(this.GearItem);
                            this._gearCollections = Mockup.AppState.getInstance().getGearState().removeGearItemFromCollections(this.GearItem);
                            this._gearSystems = Mockup.AppState.getInstance().getGearState().removeGearItemFromSystems(this.GearItem);
                            Mockup.AppState.getInstance().getGearState().deleteGearItem(this.GearItem);
                        }
                        undoAction() {
                            Mockup.AppState.getInstance().getGearState().addGearItem(this.GearItem);
                            for (let i = 0; i < this._gearSystems.length; ++i) {
                                const gearSystem = this._gearSystems[i];
                                gearSystem.addGearItem(this.GearItem);
                            }
                            for (let i = 0; i < this._gearCollections.length; ++i) {
                                const gearCollection = this._gearCollections[i];
                                gearCollection.addGearItem(this.GearItem);
                            }
                            for (let i = 0; i < this._tripPlans.length; ++i) {
                                const tripPlan = this._tripPlans[i];
                                tripPlan.addGearItem(this.GearItem);
                            }
                        }
                    }
                    Items.DeleteGearItemAction = DeleteGearItemAction;
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Actions.Gear || (Actions.Gear = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../Models/Gear/GearSystem.ts"/>
/// <reference path="../../Command.ts"/>
/// <reference path="../../../AppState.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Actions;
        (function (Actions) {
            var Gear;
            (function (Gear) {
                var Systems;
                (function (Systems) {
                    "use strict";
                    class DeleteGearSystemAction {
                        constructor() {
                            this._gearCollections = [];
                            this._tripPlans = [];
                        }
                        doAction() {
                            this._tripPlans = Mockup.AppState.getInstance().getTripState().removeGearSystemFromPlans(this.GearSystem);
                            this._gearCollections = Mockup.AppState.getInstance().getGearState().removeGearSystemFromCollections(this.GearSystem);
                            Mockup.AppState.getInstance().getGearState().deleteGearSystem(this.GearSystem);
                        }
                        undoAction() {
                            Mockup.AppState.getInstance().getGearState().addGearSystem(this.GearSystem);
                            for (let i = 0; i < this._gearCollections.length; ++i) {
                                const gearCollection = this._gearCollections[i];
                                gearCollection.addGearSystem(this.GearSystem);
                            }
                            for (let i = 0; i < this._tripPlans.length; ++i) {
                                const tripPlan = this._tripPlans[i];
                                tripPlan.addGearSystem(this.GearSystem);
                            }
                        }
                    }
                    Systems.DeleteGearSystemAction = DeleteGearSystemAction;
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Actions.Gear || (Actions.Gear = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../Models/Meals/Meal.ts"/>
/// <reference path="../Command.ts"/>
/// <reference path="../../AppState.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Actions;
        (function (Actions) {
            var Meals;
            (function (Meals) {
                "use strict";
                class DeleteMealAction {
                    constructor() {
                        this._tripPlans = [];
                    }
                    doAction() {
                        this._tripPlans = Mockup.AppState.getInstance().getTripState().removeMealFromPlans(this.Meal);
                        Mockup.AppState.getInstance().getMealState().deleteMeal(this.Meal);
                    }
                    undoAction() {
                        Mockup.AppState.getInstance().getMealState().addMeal(this.Meal);
                        for (let i = 0; i < this._tripPlans.length; ++i) {
                            const tripPlan = this._tripPlans[i];
                            tripPlan.addMeal(this.Meal);
                        }
                    }
                }
                Meals.DeleteMealAction = DeleteMealAction;
            })(Meals = Actions.Meals || (Actions.Meals = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../Models/Trips/TripItinerary.ts"/>
/// <reference path="../../Command.ts"/>
/// <reference path="../../../AppState.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Actions;
        (function (Actions) {
            var Trips;
            (function (Trips) {
                var Itineraries;
                (function (Itineraries) {
                    "use strict";
                    class DeleteTripItineraryAction {
                        constructor() {
                            this._tripPlans = [];
                        }
                        doAction() {
                            this._tripPlans = Mockup.AppState.getInstance().getTripState().removeTripItineraryFromPlans(this.TripItinerary);
                            Mockup.AppState.getInstance().getTripState().deleteTripItinerary(this.TripItinerary);
                        }
                        undoAction() {
                            Mockup.AppState.getInstance().getTripState().addTripItinerary(this.TripItinerary);
                            for (let i = 0; i < this._tripPlans.length; ++i) {
                                const tripPlan = this._tripPlans[i];
                                tripPlan.tripItineraryId(this.TripItinerary.Id);
                            }
                        }
                    }
                    Itineraries.DeleteTripItineraryAction = DeleteTripItineraryAction;
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Actions.Trips || (Actions.Trips = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../Models/Trips/TripPlan.ts"/>
/// <reference path="../../Command.ts"/>
/// <reference path="../../../AppState.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Actions;
        (function (Actions) {
            var Trips;
            (function (Trips) {
                var Plans;
                (function (Plans) {
                    "use strict";
                    class DeleteTripPlanAction {
                        doAction() {
                            Mockup.AppState.getInstance().getTripState().deleteTripPlan(this.TripPlan);
                        }
                        undoAction() {
                            Mockup.AppState.getInstance().getTripState().addTripPlan(this.TripPlan);
                        }
                    }
                    Plans.DeleteTripPlanAction = DeleteTripPlanAction;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Actions.Trips || (Actions.Trips = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="Models/AppSettings.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        function getUnitsWeightString(units) {
            switch (units) {
                case "Imperial":
                    return "ounce";
                case "Metric":
                    return "gram";
            }
            throw new Error(`Invalid units: ${units}`);
        }
        Mockup.getUnitsWeightString = getUnitsWeightString;
        function convertGramsToOunces(grams) {
            return grams * 0.035274;
        }
        Mockup.convertGramsToOunces = convertGramsToOunces;
        function convertOuncesToGrams(ounces) {
            return ounces * 28.3495;
        }
        Mockup.convertOuncesToGrams = convertOuncesToGrams;
        function convertGramsToUnits(grams, units) {
            switch (units) {
                case "Metric":
                    return grams;
                case "Imperial":
                    return convertGramsToOunces(grams);
            }
            throw new Error(`Invalid units: ${units}`);
        }
        Mockup.convertGramsToUnits = convertGramsToUnits;
        function convertUnitsToGrams(value, units) {
            switch (units) {
                case "Metric":
                    return value;
                case "Imperial":
                    return convertOuncesToGrams(value);
            }
            throw new Error(`Invalid units: ${units}`);
        }
        Mockup.convertUnitsToGrams = convertUnitsToGrams;
        function getUnitsLengthString(units) {
            switch (units) {
                case "Imperial":
                    return "inches";
                case "Metric":
                    return "centimeters";
            }
            throw new Error(`Invalid units: ${units}`);
        }
        Mockup.getUnitsLengthString = getUnitsLengthString;
        function convertCentimetersToInches(centimeters) {
            return centimeters * 0.393701;
        }
        Mockup.convertCentimetersToInches = convertCentimetersToInches;
        function convertInchesToCentimeters(inches) {
            return inches * 2.54;
        }
        Mockup.convertInchesToCentimeters = convertInchesToCentimeters;
        function convertCentimetersToUnits(centimeters, units) {
            switch (units) {
                case "Metric":
                    return centimeters;
                case "Imperial":
                    return convertCentimetersToInches(centimeters);
            }
            throw new Error(`Invalid units: ${units}`);
        }
        Mockup.convertCentimetersToUnits = convertCentimetersToUnits;
        function convertUnitsToCentimeters(value, units) {
            switch (units) {
                case "Metric":
                    return value;
                case "Imperial":
                    return convertInchesToCentimeters(value);
            }
            throw new Error(`Invalid units: ${units}`);
        }
        Mockup.convertUnitsToCentimeters = convertUnitsToCentimeters;
        function getCurrencyString(currency) {
            switch (currency) {
                case "USD":
                    return "USD";
            }
            throw new Error(`Invalid currency: ${currency}`);
        }
        Mockup.getCurrencyString = getCurrencyString;
        function convertUSDPToUSD(usdp) {
            return usdp * 0.01;
        }
        Mockup.convertUSDPToUSD = convertUSDPToUSD;
        function convertUSDToUSDP(usd) {
            return usd * 100.0;
        }
        Mockup.convertUSDToUSDP = convertUSDToUSDP;
        function convertUSDPToCurrency(usdp, currency) {
            switch (currency) {
                case "USD":
                    return convertUSDPToUSD(usdp);
            }
            throw new Error(`Invalid currency: ${currency}`);
        }
        Mockup.convertUSDPToCurrency = convertUSDPToCurrency;
        function convertCurrencyToUSDP(value, currency) {
            switch (currency) {
                case "USD":
                    return convertUSDToUSDP(value);
            }
            throw new Error(`Invalid currency: ${currency}`);
        }
        Mockup.convertCurrencyToUSDP = convertCurrencyToUSDP;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../Models/Gear/GearItem.ts" />
/// <reference path="../Models/Gear/GearSystem.ts" />
/// <reference path="../Models/Gear/GearCollection.ts" />
/// <reference path="../Models/Meals/Meal.ts" />
/// <reference path="../Models/Trips/TripItinerary.ts" />
/// <reference path="../Models/Trips/TripPlan.ts" />
/// <reference path="../Models/Personal/UserInformation.ts" />
/// <reference path="../Models/AppSettings.ts" />
/// <reference path="../Services/Gear/GearCollectionService.ts"/>
/// <reference path="../Services/Gear/GearItemService.ts"/>
/// <reference path="../Services/Gear/GearSystemService.ts"/>
/// <reference path="../Services/Meals/MealService.ts"/>
/// <reference path="../Services/Trips/TripItineraryService.ts"/>
/// <reference path="../Services/Trips/TripPlanService.ts"/>
/// <reference path="../Services/Personal/UserInformationService.ts"/>
/// <reference path="../Services/AppSettingsService.ts"/>
/// <reference path="../UnitConversion.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            "use strict";
            class AppCtrl {
                constructor($scope, $q, $location, $anchorScroll, $mdSidenav, $mdDialog, $mdToast, appSettingsService, userInformationService, gearItemService, gearSystemService, gearCollectionService, mealService, tripItineraryService, tripPlanService) {
                    $scope.appStateLoading = true;
                    Mockup.AppState.getInstance().loadFromDevice($q, appSettingsService, userInformationService, gearItemService, gearSystemService, gearCollectionService, mealService, tripItineraryService, tripPlanService).then(() => {
                        $scope.appStateLoading = false;
                    });
                    // user information
                    $scope.getUserInfo = () => {
                        return Mockup.AppState.getInstance().getUserInformation();
                    };
                    // gear items
                    $scope.getGearItems = () => {
                        return Mockup.AppState.getInstance().getGearState().getGearItems();
                    };
                    $scope.getGearItemById = (gearItemId) => {
                        return Mockup.AppState.getInstance().getGearState().getGearItemById(gearItemId);
                    };
                    // gear systems
                    $scope.getGearSystems = () => {
                        return Mockup.AppState.getInstance().getGearState().getGearSystems();
                    };
                    $scope.getGearSystemById = (gearSystemId) => {
                        return Mockup.AppState.getInstance().getGearState().getGearSystemById(gearSystemId);
                    };
                    // gear collections
                    $scope.getGearCollections = () => {
                        return Mockup.AppState.getInstance().getGearState().getGearCollections();
                    };
                    $scope.getGearCollectionById = (gearCollectionId) => {
                        return Mockup.AppState.getInstance().getGearState().getGearCollectionById(gearCollectionId);
                    };
                    // meals
                    $scope.getMeals = () => {
                        return Mockup.AppState.getInstance().getMealState().getMeals();
                    };
                    $scope.getMealById = (mealId) => {
                        return Mockup.AppState.getInstance().getMealState().getMealById(mealId);
                    };
                    // trip itineraries
                    $scope.getTripItineraries = () => {
                        return Mockup.AppState.getInstance().getTripState().getTripItineraries();
                    };
                    $scope.getTripItineraryById = (tripItineraryId) => {
                        return Mockup.AppState.getInstance().getTripState().getTripItineraryById(tripItineraryId);
                    };
                    // trip plans
                    $scope.getTripPlans = () => {
                        return Mockup.AppState.getInstance().getTripState().getTripPlans();
                    };
                    $scope.getTripPlanById = (tripPlanId) => {
                        return Mockup.AppState.getInstance().getTripState().getTripPlanById(tripPlanId);
                    };
                    // unit utilities
                    $scope.getUnitsWeightString = () => {
                        return Mockup.getUnitsWeightString(Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    $scope.getUnitsLengthString = () => {
                        return Mockup.getUnitsLengthString(Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    $scope.getCurrencyString = () => {
                        return Mockup.getCurrencyString(Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    $scope.getDaysBetween = (startDate, endDate) => {
                        const oneDayInMs = 86400000;
                        const startDateInMs = startDate.getTime();
                        const endDateInMs = endDate.getTime();
                        const daysBetweenInMs = endDateInMs - startDateInMs;
                        return Math.round(daysBetweenInMs / oneDayInMs);
                    };
                    // view utilities
                    $scope.scrollToTop = () => {
                        $location.hash("top");
                        $anchorScroll();
                    };
                    $scope.isActive = (viewLocation) => {
                        // set the nav item as active when we're looking at its location
                        return $location.path() === viewLocation;
                    };
                    $scope.toggleSidenav = () => {
                        $mdSidenav("left").toggle();
                    };
                }
            }
            Controllers.AppCtrl = AppCtrl;
            AppCtrl.$inject = ["$scope", "$q", "$location", "$anchorScroll",
                "$mdSidenav", "$mdDialog", "$mdToast",
                "AppSettingsService", "UserInformationService",
                "GearItemService", "GearSystemService", "GearCollectionService",
                "MealService", "TripItineraryService", "TripPlanService"];
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Collections;
                (function (Collections) {
                    "use strict";
                    class AddGearCollectionCtrl {
                        constructor($scope, $location, $mdDialog, $mdToast) {
                            $scope.orderGearItemsBy = "getName()";
                            $scope.orderGearSystemsBy = "getName()";
                            $scope.gearCollection = new Mockup.Models.Gear.GearCollection();
                            $scope.showAddGearItemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Collections.AddGearItemDlgCtrl,
                                    templateUrl: "content/partials/gear/collections/add-item.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        gearCollection: $scope.gearCollection
                                    }
                                });
                            };
                            $scope.showAddGearSystemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Collections.AddGearSystemDlgCtrl,
                                    templateUrl: "content/partials/gear/collections/add-system.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        gearCollection: $scope.gearCollection
                                    }
                                });
                            };
                            $scope.addGearCollection = () => {
                                Mockup.AppState.getInstance().getGearState().addGearCollection($scope.gearCollection);
                                var addToast = $mdToast.simple()
                                    .textContent(`Added gear collection: ${$scope.gearCollection.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/collections");
                                $mdToast.show(addToast);
                            };
                            $scope.resetGearCollection = () => {
                                $scope.gearCollection = new Mockup.Models.Gear.GearCollection();
                            };
                        }
                    }
                    Collections.AddGearCollectionCtrl = AddGearCollectionCtrl;
                    AddGearCollectionCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Collections;
                (function (Collections) {
                    "use strict";
                    class GearCollectionCtrl {
                        constructor($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            $scope.orderGearSystemsBy = "getName()";
                            $scope.orderGearItemsBy = "getName()";
                            const gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById($routeParams.gearCollectionId);
                            if (null == gearCollection) {
                                alert("The gear collection does not exist!");
                                $location.path("/gear/collections");
                                return;
                            }
                            $scope.gearCollection = angular.copy(gearCollection);
                            $scope.showAddGearSystemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Collections.AddGearSystemDlgCtrl,
                                    templateUrl: "content/partials/gear/collections/add-system.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        gearCollection: $scope.gearCollection
                                    }
                                });
                            };
                            $scope.showAddGearItemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Collections.AddGearItemDlgCtrl,
                                    templateUrl: "content/partials/gear/collections/add-item.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        gearCollection: $scope.gearCollection
                                    }
                                });
                            };
                            $scope.saveGearCollection = () => {
                                var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById($scope.gearCollection.Id);
                                if (null == gearCollection) {
                                    alert("The gear collection no longer exists!");
                                    $location.path("/gear/collections");
                                    return;
                                }
                                gearCollection.update($scope.gearCollection);
                                var updateToast = $mdToast.simple()
                                    .textContent(`Updated gear collection: ${$scope.gearCollection.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/collections");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetGearCollection = () => {
                                var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById($scope.gearCollection.Id);
                                if (null == gearCollection) {
                                    alert("The gear collection no longer exists!");
                                    $location.path("/gear/collections");
                                    return;
                                }
                                $scope.gearCollection = angular.copy(gearCollection);
                            };
                            $scope.deleteGearCollection = (event) => {
                                var confirm = $mdDialog.confirm()
                                    .parent(angular.element(document.body))
                                    .title("Delete Gear Collection")
                                    .textContent("Are you sure you wish to delete this gear collection?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Gear collection deleted!")
                                    .textContent("The gear collection has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .textContent(`Deleted gear collection: ${$scope.gearCollection.name()}`)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent(`Restored gear collection: ${$scope.gearCollection.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(() => {
                                    $mdDialog.show(receipt).then(() => {
                                        const action = new Mockup.Actions.Gear.Collections.DeleteGearCollectionAction();
                                        action.GearCollection = $scope.gearCollection;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/gear/collections");
                                        $mdToast.show(deleteToast).then((response) => {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path(`/gear/collections/${$scope.gearCollection.Id}`);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                    }
                    Collections.GearCollectionCtrl = GearCollectionCtrl;
                    GearCollectionCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Collections;
                (function (Collections) {
                    "use strict";
                    class GearCollectionsCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterGearCollection = (gearCollection) => {
                                if ($scope.filterName) {
                                    return gearCollection.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsGearCollectionDlg = (event) => {
                                $mdDialog.show({
                                    controller: Collections.WhatIsGearCollectionDlgCtrl,
                                    templateUrl: "content/partials/gear/collections/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                    }
                    Collections.GearCollectionsCtrl = GearCollectionsCtrl;
                    GearCollectionsCtrl.$inject = ["$scope", "$mdDialog"];
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Items;
                (function (Items) {
                    "use strict";
                    class AddGearItemCtrl {
                        constructor($scope, $location, $mdToast) {
                            $scope.gearItem = new Mockup.Models.Gear.GearItem();
                            $scope.addGearItem = () => {
                                Mockup.AppState.getInstance().getGearState().addGearItem($scope.gearItem);
                                var addToast = $mdToast.simple()
                                    .textContent(`Added gear item: ${$scope.gearItem.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/items");
                                $mdToast.show(addToast);
                            };
                            $scope.resetGearItem = () => {
                                $scope.gearItem = new Mockup.Models.Gear.GearItem();
                            };
                        }
                    }
                    Items.AddGearItemCtrl = AddGearItemCtrl;
                    AddGearItemCtrl.$inject = ["$scope", "$location", "$mdToast"];
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Items;
                (function (Items) {
                    "use strict";
                    class GearItemCtrl {
                        constructor($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            const gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById($routeParams.gearItemId);
                            if (null == gearItem) {
                                alert("The gear item does not exist!");
                                $location.path("/gear/items");
                                return;
                            }
                            $scope.gearItem = angular.copy(gearItem);
                            $scope.saveGearItem = () => {
                                var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById($scope.gearItem.Id);
                                if (null == gearItem) {
                                    alert("The gear item no longer exists!");
                                    $location.path("/gear/items");
                                    return;
                                }
                                gearItem.update($scope.gearItem);
                                var updateToast = $mdToast.simple()
                                    .textContent(`Updated gear item: ${$scope.gearItem.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/items");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetGearItem = () => {
                                var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById($scope.gearItem.Id);
                                if (null == gearItem) {
                                    alert("The gear item no longer exists!");
                                    $location.path("/gear/items");
                                    return;
                                }
                                $scope.gearItem = angular.copy(gearItem);
                            };
                            $scope.deleteGearItem = (event) => {
                                var confirm = $mdDialog.confirm()
                                    .parent(angular.element(document.body))
                                    .title("Delete Gear Item")
                                    .textContent("Are you sure you wish to delete this gear item?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Gear item deleted!")
                                    .textContent("The gear item has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .textContent(`Deleted gear item: ${$scope.gearItem.name()}`)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent(`Restored gear item: ${$scope.gearItem.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(() => {
                                    $mdDialog.show(receipt).then(() => {
                                        const action = new Mockup.Actions.Gear.Items.DeleteGearItemAction();
                                        action.GearItem = $scope.gearItem;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/gear/items");
                                        $mdToast.show(deleteToast).then((response) => {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path(`/gear/items/${$scope.gearItem.Id}`);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                    }
                    Items.GearItemCtrl = GearItemCtrl;
                    GearItemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Items;
                (function (Items) {
                    "use strict";
                    class GearItemsCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterGearItem = (gearItem) => {
                                if ($scope.filterName) {
                                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsGearItemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Items.WhatIsGearItemDlgCtrl,
                                    templateUrl: "content/partials/gear/items/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                    }
                    Items.GearItemsCtrl = GearItemsCtrl;
                    GearItemsCtrl.$inject = ["$scope", "$mdDialog"];
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Systems;
                (function (Systems) {
                    "use strict";
                    class AddGearSystemCtrl {
                        constructor($scope, $location, $mdDialog, $mdToast) {
                            $scope.orderGearItemsBy = "getName()";
                            $scope.gearSystem = new Mockup.Models.Gear.GearSystem();
                            $scope.showAddGearItemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Systems.AddGearItemDlgCtrl,
                                    templateUrl: "content/partials/gear/systems/add-item.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        gearSystem: $scope.gearSystem
                                    }
                                });
                            };
                            $scope.addGearSystem = () => {
                                Mockup.AppState.getInstance().getGearState().addGearSystem($scope.gearSystem);
                                var addToast = $mdToast.simple()
                                    .textContent(`Added gear system: ${$scope.gearSystem.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/systems");
                                $mdToast.show(addToast);
                            };
                            $scope.resetGearSystem = () => {
                                $scope.gearSystem = new Mockup.Models.Gear.GearSystem();
                            };
                        }
                    }
                    Systems.AddGearSystemCtrl = AddGearSystemCtrl;
                    AddGearSystemCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Systems;
                (function (Systems) {
                    "use strict";
                    class GearSystemCtrl {
                        constructor($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            $scope.orderGearItemsBy = "getName()";
                            const gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById($routeParams.gearSystemId);
                            if (null == gearSystem) {
                                alert("The gear system does not exist!");
                                $location.path("/gear/systems");
                                return;
                            }
                            $scope.gearSystem = angular.copy(gearSystem);
                            $scope.showAddGearItemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Systems.AddGearItemDlgCtrl,
                                    templateUrl: "content/partials/gear/systems/add-item.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        gearSystem: $scope.gearSystem
                                    }
                                });
                            };
                            $scope.saveGearSystem = () => {
                                var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById($scope.gearSystem.Id);
                                if (null == gearSystem) {
                                    alert("The gear system no longer exists!");
                                    $location.path("/gear/systems");
                                    return;
                                }
                                gearSystem.update($scope.gearSystem);
                                var updateToast = $mdToast.simple()
                                    .textContent(`Updated gear system: ${$scope.gearSystem.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/systems");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetGearSystem = () => {
                                var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById($scope.gearSystem.Id);
                                if (null == gearSystem) {
                                    alert("The gear system no longer exists!");
                                    $location.path("/gear/systems");
                                    return;
                                }
                                $scope.gearSystem = angular.copy(gearSystem);
                            };
                            $scope.deleteGearSystem = (event) => {
                                var confirm = $mdDialog.confirm()
                                    .parent(angular.element(document.body))
                                    .title("Delete Gear System")
                                    .textContent("Are you sure you wish to delete this gear system?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Gear system deleted!")
                                    .textContent("The gear system has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .textContent(`Deleted gear system: ${$scope.gearSystem.name()}`)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent(`Restored gear system: ${$scope.gearSystem.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(() => {
                                    $mdDialog.show(receipt).then(() => {
                                        const action = new Mockup.Actions.Gear.Systems.DeleteGearSystemAction();
                                        action.GearSystem = $scope.gearSystem;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/gear/systems");
                                        $mdToast.show(deleteToast).then((response) => {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path(`/gear/systems/${$scope.gearSystem.Id}`);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                    }
                    Systems.GearSystemCtrl = GearSystemCtrl;
                    GearSystemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Systems;
                (function (Systems) {
                    "use strict";
                    class GearSystemsCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterGearSystem = (gearSystem) => {
                                if ($scope.filterName) {
                                    return gearSystem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsGearSystemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Systems.WhatIsGearSystemDlgCtrl,
                                    templateUrl: "content/partials/gear/systems/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                    }
                    Systems.GearSystemsCtrl = GearSystemsCtrl;
                    GearSystemsCtrl.$inject = ["$scope", "$mdDialog"];
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Meals;
            (function (Meals) {
                "use strict";
                class MealsCtrl {
                    constructor($scope, $mdDialog) {
                        $scope.filterName = "";
                        $scope.orderBy = "name()";
                        $scope.filterMeal = (meal) => {
                            if ($scope.filterName) {
                                return meal.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                            }
                            return true;
                        };
                        $scope.showWhatIsMealDlg = (event) => {
                            $mdDialog.show({
                                controller: Meals.WhatIsMealDlgCtrl,
                                templateUrl: "content/partials/meals/what.html",
                                parent: angular.element(document.body),
                                targetEvent: event
                            });
                        };
                    }
                }
                Meals.MealsCtrl = MealsCtrl;
                MealsCtrl.$inject = ["$scope", "$mdDialog"];
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Meals;
            (function (Meals) {
                "use strict";
                class MealCtrl {
                    constructor($scope, $routeParams, $location, $mdDialog, $mdToast) {
                        const meal = Mockup.AppState.getInstance().getMealState().getMealById($routeParams.mealId);
                        if (null == meal) {
                            alert("The meal does not exist!");
                            $location.path("/meals");
                            return;
                        }
                        $scope.meal = angular.copy(meal);
                        $scope.saveMeal = () => {
                            var meal = Mockup.AppState.getInstance().getMealState().getMealById($scope.meal.Id);
                            if (null == meal) {
                                alert("The meal no longer exists!");
                                $location.path("/meals");
                                return;
                            }
                            meal.update($scope.meal);
                            var updateToast = $mdToast.simple()
                                .textContent(`Updated meal: ${$scope.meal.name()}`)
                                .action("OK")
                                .position("bottom left");
                            $location.path("/meals");
                            $mdToast.show(updateToast);
                        };
                        $scope.resetMeal = () => {
                            var meal = Mockup.AppState.getInstance().getMealState().getMealById($scope.meal.Id);
                            if (null == meal) {
                                alert("The meal no longer exists!");
                                $location.path("/meals");
                                return;
                            }
                            $scope.meal = angular.copy(meal);
                        };
                        $scope.deleteMeal = (event) => {
                            var confirm = $mdDialog.confirm()
                                .parent(angular.element(document.body))
                                .title("Delete Meal")
                                .textContent("Are you sure you wish to delete this meal?")
                                .ok("Yes")
                                .cancel("No")
                                .targetEvent(event);
                            var receipt = $mdDialog.alert()
                                .parent(angular.element(document.body))
                                .title("Meal deleted!")
                                .textContent("The meal has been deleted.")
                                .ok("OK")
                                .targetEvent(event);
                            var deleteToast = $mdToast.simple()
                                .textContent(`Deleted meal: ${$scope.meal.name()}`)
                                .action("Undo")
                                .position("bottom left");
                            var undoDeleteToast = $mdToast.simple()
                                .textContent(`Restored meal: ${$scope.meal.name()}`)
                                .action("OK")
                                .position("bottom left");
                            $mdDialog.show(confirm).then(() => {
                                $mdDialog.show(receipt).then(() => {
                                    const action = new Mockup.Actions.Meals.DeleteMealAction();
                                    action.Meal = $scope.meal;
                                    Mockup.AppState.getInstance().executeAction(action);
                                    $location.path("/meals");
                                    $mdToast.show(deleteToast).then((response) => {
                                        if ("ok" == response) {
                                            Mockup.AppState.getInstance().undoAction();
                                            $mdToast.show(undoDeleteToast);
                                            $location.path(`/meals/${$scope.meal.Id}`);
                                        }
                                    });
                                });
                            });
                        };
                    }
                }
                Meals.MealCtrl = MealCtrl;
                MealCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Meals;
            (function (Meals) {
                "use strict";
                class AddMealCtrl {
                    constructor($scope, $location, $mdToast) {
                        $scope.meal = new Mockup.Models.Meals.Meal();
                        $scope.addMeal = () => {
                            Mockup.AppState.getInstance().getMealState().addMeal($scope.meal);
                            var addToast = $mdToast.simple()
                                .textContent(`Added meal: ${$scope.meal.name()}`)
                                .action("OK")
                                .position("bottom left");
                            $location.path("/meals");
                            $mdToast.show(addToast);
                        };
                        $scope.resetMeal = () => {
                            $scope.meal = new Mockup.Models.Meals.Meal();
                        };
                    }
                }
                Meals.AddMealCtrl = AddMealCtrl;
                AddMealCtrl.$inject = ["$scope", "$location", "$mdToast"];
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Itineraries;
                (function (Itineraries) {
                    "use strict";
                    class TripItinerariesCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterTripItinerary = (tripItinerary) => {
                                if ($scope.filterName) {
                                    return tripItinerary.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsTripItineraryDlg = (event) => {
                                $mdDialog.show({
                                    controller: Itineraries.WhatIsTripItineraryDlgCtrl,
                                    templateUrl: "content/partials/trips/itineraries/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                    }
                    Itineraries.TripItinerariesCtrl = TripItinerariesCtrl;
                    TripItinerariesCtrl.$inject = ["$scope", "$mdDialog"];
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Itineraries;
                (function (Itineraries) {
                    "use strict";
                    class TripItineraryCtrl {
                        constructor($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            const tripItinerary = Mockup.AppState.getInstance().getTripState().getTripItineraryById($routeParams.tripItineraryId);
                            if (null == tripItinerary) {
                                alert("The trip itinerary does not exist!");
                                $location.path("/trips/itineraries");
                                return;
                            }
                            $scope.tripItinerary = angular.copy(tripItinerary);
                            $scope.saveTripItinerary = () => {
                                var tripItinerary = Mockup.AppState.getInstance().getTripState().getTripItineraryById($scope.tripItinerary.Id);
                                if (null == tripItinerary) {
                                    alert("The trip itinerary no longer exists!");
                                    $location.path("/trips/itineraries");
                                    return;
                                }
                                tripItinerary.update($scope.tripItinerary);
                                var updateToast = $mdToast.simple()
                                    .textContent(`Updated gear collection: ${$scope.tripItinerary.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/itineraries");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetTripItinerary = () => {
                                var tripItinerary = Mockup.AppState.getInstance().getTripState().getTripItineraryById($scope.tripItinerary.Id);
                                if (null == tripItinerary) {
                                    alert("The trip itinerary no longer exists!");
                                    $location.path("/trips/itineraries");
                                    return;
                                }
                                $scope.tripItinerary = angular.copy(tripItinerary);
                            };
                            $scope.deleteTripItinerary = (event) => {
                                var confirm = $mdDialog.confirm()
                                    .parent(angular.element(document.body))
                                    .title("Delete Trip Itinerary")
                                    .textContent("Are you sure you wish to delete this trip itinerary?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Trip itinerary deleted!")
                                    .textContent("The trip itinerary has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .textContent(`Deleted trip itinerary: ${$scope.tripItinerary.name()}`)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent(`Restored trip itinerary: ${$scope.tripItinerary.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(() => {
                                    $mdDialog.show(receipt).then(() => {
                                        const action = new Mockup.Actions.Trips.Itineraries.DeleteTripItineraryAction();
                                        action.TripItinerary = $scope.tripItinerary;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/trips/itineraries");
                                        $mdToast.show(deleteToast).then((response) => {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path(`/trips/itineraries/${$scope.tripItinerary.Id}`);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                    }
                    Itineraries.TripItineraryCtrl = TripItineraryCtrl;
                    TripItineraryCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Itineraries;
                (function (Itineraries) {
                    "use strict";
                    class AddTripItineraryCtrl {
                        constructor($scope, $location, $mdToast) {
                            $scope.tripItinerary = new Mockup.Models.Trips.TripItinerary();
                            $scope.addTripItinerary = () => {
                                Mockup.AppState.getInstance().getTripState().addTripItinerary($scope.tripItinerary);
                                var addToast = $mdToast.simple()
                                    .textContent(`Added trip itinerary: ${$scope.tripItinerary.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/itineraries");
                                $mdToast.show(addToast);
                            };
                            $scope.resetTripItinerary = () => {
                                $scope.tripItinerary = new Mockup.Models.Trips.TripItinerary();
                            };
                        }
                    }
                    Itineraries.AddTripItineraryCtrl = AddTripItineraryCtrl;
                    AddTripItineraryCtrl.$inject = ["$scope", "$location", "$mdToast"];
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Plans;
                (function (Plans) {
                    "use strict";
                    class TripPlansCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterTripPlan = (tripPlan) => {
                                if ($scope.filterName) {
                                    return tripPlan.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsTripPlan = (event) => {
                                $mdDialog.show({
                                    controller: Plans.WhatIsTripPlanDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                    }
                    Plans.TripPlansCtrl = TripPlansCtrl;
                    TripPlansCtrl.$inject = ["$scope", "$mdDialog"];
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Plans;
                (function (Plans) {
                    "use strict";
                    class TripPlanCtrl {
                        constructor($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            $scope.orderGearCollectionsBy = "getName()";
                            $scope.orderGearSystemsBy = "getName()";
                            $scope.orderGearItemsBy = "getName()";
                            $scope.orderMealsBy = "getName()";
                            const tripPlan = Mockup.AppState.getInstance().getTripState().getTripPlanById($routeParams.tripPlanId);
                            if (null == tripPlan) {
                                alert("The trip plan does not exist!");
                                $location.path("/trips/plans");
                                return;
                            }
                            $scope.tripPlan = angular.copy(tripPlan);
                            $scope.showAddGearCollectionDlg = (event) => {
                                $mdDialog.show({
                                    controller: Plans.AddGearCollectionDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/add-collection.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        tripPlan: $scope.tripPlan
                                    }
                                });
                            };
                            $scope.showAddGearSystemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Plans.AddGearSystemDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/add-system.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        tripPlan: $scope.tripPlan
                                    }
                                });
                            };
                            $scope.showAddGearItemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Plans.AddGearItemDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/add-item.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        tripPlan: $scope.tripPlan
                                    }
                                });
                            };
                            $scope.showAddMealDlg = (event) => {
                                $mdDialog.show({
                                    controller: Plans.AddMealDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/add-meal.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        tripPlan: $scope.tripPlan
                                    }
                                });
                            };
                            $scope.saveTripPlan = () => {
                                var tripPlan = Mockup.AppState.getInstance().getTripState().getTripPlanById($scope.tripPlan.Id);
                                if (null == tripPlan) {
                                    alert("The trip plan no longer exists!");
                                    $location.path("/trips/plans");
                                    return;
                                }
                                tripPlan.update($scope.tripPlan);
                                var updateToast = $mdToast.simple()
                                    .textContent(`Updated trip plan: ${$scope.tripPlan.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/plans");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetTripPlan = () => {
                                var tripPlan = Mockup.AppState.getInstance().getTripState().getTripPlanById($scope.tripPlan.Id);
                                if (null == tripPlan) {
                                    alert("The trip plan no longer exists!");
                                    $location.path("/trips/plans");
                                    return;
                                }
                                $scope.tripPlan = angular.copy(tripPlan);
                            };
                            $scope.deleteTripPlan = (event) => {
                                var confirm = $mdDialog.confirm()
                                    .parent(angular.element(document.body))
                                    .title("Delete Trip Plan")
                                    .textContent("Are you sure you wish to delete this trip plan?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Trip plan deleted!")
                                    .textContent("The trip plan has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .textContent(`Deleted trip plan: ${$scope.tripPlan.name()}`)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent(`Restored trip plan: ${$scope.tripPlan.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(() => {
                                    $mdDialog.show(receipt).then(() => {
                                        const action = new Mockup.Actions.Trips.Plans.DeleteTripPlanAction();
                                        action.TripPlan = $scope.tripPlan;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/trips/plans");
                                        $mdToast.show(deleteToast).then((response) => {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path(`/trips/plans/${$scope.tripPlan.Id}`);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                    }
                    Plans.TripPlanCtrl = TripPlanCtrl;
                    TripPlanCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Plans;
                (function (Plans) {
                    "use strict";
                    class AddTripPlanCtrl {
                        constructor($scope, $location, $mdDialog, $mdToast) {
                            $scope.orderGearCollectionsBy = "getName()";
                            $scope.orderGearSystemsBy = "getName()";
                            $scope.orderGearItemsBy = "getName()";
                            $scope.orderMealsBy = "getName()";
                            $scope.tripPlan = new Mockup.Models.Trips.TripPlan();
                            $scope.showAddGearCollectionDlg = (event) => {
                                $mdDialog.show({
                                    controller: Plans.AddGearCollectionDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/add-collection.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        tripPlan: $scope.tripPlan
                                    }
                                });
                            };
                            $scope.showAddGearSystemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Plans.AddGearSystemDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/add-system.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        tripPlan: $scope.tripPlan
                                    }
                                });
                            };
                            $scope.showAddGearItemDlg = (event) => {
                                $mdDialog.show({
                                    controller: Plans.AddGearItemDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/add-item.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        tripPlan: $scope.tripPlan
                                    }
                                });
                            };
                            $scope.showAddMealDlg = (event) => {
                                $mdDialog.show({
                                    controller: Plans.AddMealDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/add-meal.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event,
                                    locals: {
                                        tripPlan: $scope.tripPlan
                                    }
                                });
                            };
                            $scope.addTripPlan = () => {
                                Mockup.AppState.getInstance().getTripState().addTripPlan($scope.tripPlan);
                                var addToast = $mdToast.simple()
                                    .textContent(`Added trip plan: ${$scope.tripPlan.name()}`)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/plans");
                                $mdToast.show(addToast);
                            };
                            $scope.resetTripPlan = () => {
                                $scope.tripPlan = new Mockup.Models.Trips.TripPlan();
                            };
                        }
                    }
                    Plans.AddTripPlanCtrl = AddTripPlanCtrl;
                    AddTripPlanCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../Models/AppSettings.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Personal;
            (function (Personal) {
                "use strict";
                class UserInformationCtrl {
                    constructor($scope, $location, $mdDialog, $mdToast) {
                        $scope.userInfo = angular.copy(Mockup.AppState.getInstance().getUserInformation());
                        $scope.showWhatIsPersonalDlg = (event) => {
                            $mdDialog.show({
                                controller: Personal.WhatIsPersonalDlgCtrl,
                                templateUrl: "content/partials/personal/what.html",
                                parent: angular.element(document.body),
                                targetEvent: event
                            });
                        };
                        $scope.saveUserInformation = () => {
                            Mockup.AppState.getInstance().getUserInformation().update($scope.userInfo);
                            var updateToast = $mdToast.simple()
                                .textContent("Updated personal information!")
                                .action("OK")
                                .position("bottom left");
                            $location.path("/personal");
                            $mdToast.show(updateToast);
                        };
                        $scope.resetUserInformation = () => {
                            $scope.userInfo = angular.copy(Mockup.AppState.getInstance().getUserInformation());
                        };
                    }
                }
                Personal.UserInformationCtrl = UserInformationCtrl;
                UserInformationCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
            })(Personal = Controllers.Personal || (Controllers.Personal = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../Models/AppSettings.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            "use strict";
            class AppSettingsCtrl {
                constructor($scope, $location, $mdDialog, $mdToast) {
                    $scope.appSettings = angular.copy(Mockup.AppState.getInstance().getAppSettings());
                    $scope.showAdvancedSettings = false;
                    $scope.toggleAdvancedSettings = () => {
                        $scope.showAdvancedSettings = !$scope.showAdvancedSettings;
                    };
                    $scope.saveAppSettings = () => {
                        Mockup.AppState.getInstance().getAppSettings().update($scope.appSettings);
                        var updateToast = $mdToast.simple()
                            .textContent("Updated application settings!")
                            .action("OK")
                            .position("bottom left");
                        $location.path("/settings");
                        $mdToast.show(updateToast);
                    };
                    $scope.resetAppSettings = () => {
                        $scope.appSettings = angular.copy(Mockup.AppState.getInstance().getAppSettings());
                    };
                    $scope.defaultAppSettings = () => {
                        $scope.appSettings.resetToDefaults();
                    };
                    $scope.deleteAllGearItems = (event) => {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Gear Items")
                            .textContent("Are you sure you wish to delete all gear items? This action cannot be undone.")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All gear items deleted!")
                            .textContent("All gear items have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .textContent("Deleted all gear items")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(() => {
                            $mdDialog.show(receipt).then(() => {
                                Mockup.AppState.getInstance().getGearState().deleteAllGearItems();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllGearSystems = (event) => {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Gear Systems")
                            .textContent("Are you sure you wish to delete all gear systems? This action cannot be undone.")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All gear systems deleted!")
                            .textContent("All gear systems have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .textContent("Deleted all gear systems")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(() => {
                            $mdDialog.show(receipt).then(() => {
                                Mockup.AppState.getInstance().getGearState().deleteAllGearSystems();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllGearCollections = (event) => {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Gear Collections")
                            .textContent("Are you sure you wish to delete all gear collections? This action cannot be undone.")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All gear collections deleted!")
                            .textContent("All gear collections have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .textContent("Deleted all gear collections")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(() => {
                            $mdDialog.show(receipt).then(() => {
                                Mockup.AppState.getInstance().getGearState().deleteAllGearCollections();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllMeals = (event) => {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Meals")
                            .textContent("Are you sure you wish to delete all meals? This action cannot be undone.")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All meals deleted!")
                            .textContent("All meals have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .textContent("Deleted all meals")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(() => {
                            $mdDialog.show(receipt).then(() => {
                                Mockup.AppState.getInstance().getMealState().deleteAllMeals();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllTripItineraries = (event) => {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Trip Itineraries")
                            .textContent("Are you sure you wish to delete all trip itineraries? This action cannot be undone.")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All trip itineraries deleted!")
                            .textContent("All trip itineraries have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .textContent("Deleted all trip itineraries")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(() => {
                            $mdDialog.show(receipt).then(() => {
                                Mockup.AppState.getInstance().getTripState().deleteAllTripItineraries();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllTripPlans = (event) => {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Trip Plans")
                            .textContent("Are you sure you wish to delete all trip plans? This action cannot be undone.")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All trip plans deleted!")
                            .textContent("All trip plans have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .textContent("Deleted all trip plans")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(() => {
                            $mdDialog.show(receipt).then(() => {
                                Mockup.AppState.getInstance().getTripState().deleteAllTripPlans();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllData = (event) => {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Data")
                            .textContent("Are you sure you wish to delete all data? This action cannot be undone.")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All data deleted!")
                            .textContent("All data has been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .textContent("Deleted all data")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(() => {
                            $mdDialog.show(receipt).then(() => {
                                Mockup.AppState.getInstance().deleteAllData();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                }
            }
            Controllers.AppSettingsCtrl = AppSettingsCtrl;
            AppSettingsCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../scripts/typings/angularjs/angular-route.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        class RootScopeConfig {
            constructor($rootScope) {
                $rootScope.$on("$routeChangeSuccess", (event, currentRoute, previousRoute) => {
                    // change the app menu title when the route changes
                    $rootScope.title = currentRoute.title;
                });
            }
        }
        Mockup.RootScopeConfig = RootScopeConfig;
        ;
        RootScopeConfig.$inject = ["$rootScope"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../scripts/typings/angularjs/angular-route.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        class CustomRouteConfig {
        }
        class RouteConfig {
            constructor($routeProvider) {
                $routeProvider.when("/", {
                    redirectTo: "/gear/items"
                });
                // gear items
                this.addRoute($routeProvider, "/gear/items", {
                    templateUrl: "content/partials/gear/items/items.html",
                    controller: "GearItemsCtrl",
                    title: "Gear Items"
                });
                this.addRoute($routeProvider, "/gear/items/add", {
                    templateUrl: "content/partials/gear/items/add.html",
                    controller: "AddGearItemCtrl",
                    title: "Add a Gear Item"
                });
                this.addRoute($routeProvider, "/gear/items/:gearItemId", {
                    templateUrl: "content/partials/gear/items/item.html",
                    controller: "GearItemCtrl",
                    title: "Gear Item"
                });
                // gear system
                this.addRoute($routeProvider, "/gear/systems", {
                    templateUrl: "content/partials/gear/systems/systems.html",
                    controller: "GearSystemsCtrl",
                    title: "Gear Systems"
                });
                this.addRoute($routeProvider, "/gear/systems/add", {
                    templateUrl: "content/partials/gear/systems/add.html",
                    controller: "AddGearSystemCtrl",
                    title: "Add a Gear System"
                });
                this.addRoute($routeProvider, "/gear/systems/:gearSystemId", {
                    templateUrl: "content/partials/gear/systems/system.html",
                    controller: "GearSystemCtrl",
                    title: "Gear System"
                });
                // gear collections
                this.addRoute($routeProvider, "/gear/collections", {
                    templateUrl: "content/partials/gear/collections/collections.html",
                    controller: "GearCollectionsCtrl",
                    title: "Gear Collections"
                });
                this.addRoute($routeProvider, "/gear/collections/add", {
                    templateUrl: "content/partials/gear/collections/add.html",
                    controller: "AddGearCollectionCtrl",
                    title: "Add a Gear Collection"
                });
                this.addRoute($routeProvider, "/gear/collections/:gearCollectionId", {
                    templateUrl: "content/partials/gear/collections/collection.html",
                    controller: "GearCollectionCtrl",
                    title: "Gear Collection"
                });
                // meals
                this.addRoute($routeProvider, "/meals", {
                    templateUrl: "content/partials/meals/meals.html",
                    controller: "MealsCtrl",
                    title: "Meals"
                });
                this.addRoute($routeProvider, "/meals/add", {
                    templateUrl: "content/partials/meals/add.html",
                    controller: "AddMealCtrl",
                    title: "Add a Meal"
                });
                this.addRoute($routeProvider, "/meals/:mealId", {
                    templateUrl: "content/partials/meals/meal.html",
                    controller: "MealCtrl",
                    title: "Meal"
                });
                // trip itineraries
                /*this.addRoute($routeProvider, "/trips/itineraries", {
                    templateUrl: "content/partials/trips/itineraries/itineraries.html",
                    controller: "TripItinerariesCtrl",
                    title: "Trip Itineraries"
                });
                this.addRoute($routeProvider, "/trips/itineraries/add", {
                    templateUrl: "content/partials/trips/itineraries/add.html",
                    controller: "AddTripItineraryCtrl",
                    title: "Add a Trip Itinerary"
                });
                this.addRoute($routeProvider, "/trips/itineraries/:tripItineraryId", {
                    templateUrl: "content/partials/trips/itineraries/itinerary.html",
                    controller: "TripItineraryCtrl",
                    title: "Trip Itinerary"
                });*/
                // trip plans
                this.addRoute($routeProvider, "/trips/plans", {
                    templateUrl: "content/partials/trips/plans/plans.html",
                    controller: "TripPlansCtrl",
                    title: "Trip Plans"
                });
                this.addRoute($routeProvider, "/trips/plans/add", {
                    templateUrl: "content/partials/trips/plans/add.html",
                    controller: "AddTripPlanCtrl",
                    title: "Add a Trip Plan"
                });
                this.addRoute($routeProvider, "/trips/plans/:tripPlanId", {
                    templateUrl: "content/partials/trips/plans/plan.html",
                    controller: "TripPlanCtrl",
                    title: "Trip Plan"
                });
                // personal information and settings
                this.addRoute($routeProvider, "/personal", {
                    templateUrl: "content/partials/personal/personal.html",
                    controller: "UserInformationCtrl",
                    title: "Personal Information"
                });
                this.addRoute($routeProvider, "/settings", {
                    templateUrl: "content/partials/settings.html",
                    controller: "AppSettingsCtrl",
                    title: "Settings"
                });
                this.addRoute($routeProvider, "/help", {
                    templateUrl: "content/partials/help.html",
                    title: "Help"
                });
                // error codes
                this.addRoute($routeProvider, "/404", {
                    templateUrl: "content/partials/404.html",
                    title: "Backpacking Planner"
                });
                this.addRoute($routeProvider, "/500", {
                    templateUrl: "content/partials/500.html",
                    title: "Backpacking Planner"
                });
                // 404 at the bottom
                $routeProvider.otherwise({
                    redirectTo: "/404"
                });
            }
            addRoute($routeProvider, url, routeConfig) {
                $routeProvider.when(url, routeConfig);
            }
        }
        Mockup.RouteConfig = RouteConfig;
        ;
        RouteConfig.$inject = ["$routeProvider"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../Scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        class ThemeConfig {
            constructor($mdThemingProvider) {
                const primaryPalette = $mdThemingProvider.extendPalette("green", {
                    "500": "668000",
                    "A100": "501616",
                    "contrastDefaultColor": "light"
                });
                const backgroundPalette = $mdThemingProvider.extendPalette("brown", {
                    "500": "decd87"
                });
                const accentPalette = $mdThemingProvider.extendPalette("blue-grey", {});
                $mdThemingProvider.definePalette("mockupPrimaryPalette", primaryPalette);
                $mdThemingProvider.definePalette("mockupBackgroundPalette", backgroundPalette);
                $mdThemingProvider.definePalette("mockupAccentPalette", accentPalette);
                $mdThemingProvider.theme("default")
                    .primaryPalette("mockupPrimaryPalette", {
                    "default": "500",
                    "hue-1": "A100"
                })
                    .backgroundPalette("mockupBackgroundPalette", {
                    "default": "500"
                })
                    .accentPalette("mockupAccentPalette", {
                    "default": "500"
                });
            }
        }
        Mockup.ThemeConfig = ThemeConfig;
        ;
        ThemeConfig.$inject = ["$mdThemingProvider"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="Controllers/Gear/Collections/AddGearCollectionCtrl.ts" />
/// <reference path="Controllers/Gear/Collections/GearCollectionCtrl.ts" />
/// <reference path="Controllers/Gear/Collections/GearCollectionsCtrl.ts" />
/// <reference path="Controllers/Gear/Items/AddGearItemCtrl.ts" />
/// <reference path="Controllers/Gear/Items/GearItemCtrl.ts" />
/// <reference path="Controllers/Gear/Items/GearItemsCtrl.ts" />
/// <reference path="Controllers/Gear/Systems/AddGearSystemCtrl.ts" />
/// <reference path="Controllers/Gear/Systems/GearSystemCtrl.ts" />
/// <reference path="Controllers/Gear/Systems/GearSystemsCtrl.ts" />
/// <reference path="Controllers/Meals/MealsCtrl.ts" />
/// <reference path="Controllers/Meals/MealCtrl.ts" />
/// <reference path="Controllers/Meals/AddMealCtrl.ts" />
/// <reference path="Controllers/Trips/Itineraries/TripItinerariesCtrl.ts" />
/// <reference path="Controllers/Trips/Itineraries/TripItineraryCtrl.ts" />
/// <reference path="Controllers/Trips/Itineraries/AddTripItineraryCtrl.ts" />
/// <reference path="Controllers/Trips/Plans/TripPlansCtrl.ts" />
/// <reference path="Controllers/Trips/Plans/TripPlanCtrl.ts" />
/// <reference path="Controllers/Trips/Plans/AddTripPlanCtrl.ts" />
/// <reference path="Controllers/Personal/UserInformationCtrl.ts" />
/// <reference path="Controllers/AppCtrl.ts" />
/// <reference path="Controllers/AppSettingsCtrl.ts" />
/// <reference path="RootScopeConfig.ts" />
/// <reference path="RouteConfig.ts" />
/// <reference path="ThemeConfig.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var mockupApp = angular.module("mockupApp", [
            "ngAnimate",
            "ngAria",
            "ngMessages",
            "ngResource",
            "ngRoute",
            "ngSanitize",
            "ngTouch",
            "ngMaterial",
            "ngMdIcons",
            "ui.bootstrap"
        ]);
        // configure the root scope
        mockupApp.run(Mockup.RootScopeConfig);
        // configure routing
        mockupApp.config(Mockup.RouteConfig);
        // configure the material design theme
        mockupApp.config(Mockup.ThemeConfig);
        // inject services
        mockupApp.factory("AppSettingsService", ["$resource", Mockup.Services.appSettingsServiceFactory]);
        mockupApp.factory("UserInformationService", ["$resource", Mockup.Services.Personal.userInformationServiceFactory]);
        mockupApp.factory("GearItemService", ["$resource", Mockup.Services.Gear.gearItemServiceFactory]);
        mockupApp.factory("GearSystemService", ["$resource", Mockup.Services.Gear.gearSystemServiceFactory]);
        mockupApp.factory("GearCollectionService", ["$resource", Mockup.Services.Gear.gearCollectionServiceFactory]);
        mockupApp.factory("MealService", ["$resource", Mockup.Services.Meals.mealServiceFactory]);
        mockupApp.factory("TripItineraryService", ["$resource", Mockup.Services.Trips.tripItineraryServiceFactory]);
        mockupApp.factory("TripPlanService", ["$resource", Mockup.Services.Trips.tripPlanServiceFactory]);
        // inject controllers
        mockupApp.controller("AppCtrl", Mockup.Controllers.AppCtrl);
        mockupApp.controller("AppSettingsCtrl", Mockup.Controllers.AppSettingsCtrl);
        mockupApp.controller("UserInformationCtrl", Mockup.Controllers.Personal.UserInformationCtrl);
        mockupApp.controller("AddGearCollectionCtrl", Mockup.Controllers.Gear.Collections.AddGearCollectionCtrl);
        mockupApp.controller("GearCollectionCtrl", Mockup.Controllers.Gear.Collections.GearCollectionCtrl);
        mockupApp.controller("GearCollectionsCtrl", Mockup.Controllers.Gear.Collections.GearCollectionsCtrl);
        mockupApp.controller("GearItemCtrl", Mockup.Controllers.Gear.Items.GearItemCtrl);
        mockupApp.controller("GearItemsCtrl", Mockup.Controllers.Gear.Items.GearItemsCtrl);
        mockupApp.controller("AddGearItemCtrl", Mockup.Controllers.Gear.Items.AddGearItemCtrl);
        mockupApp.controller("GearSystemCtrl", Mockup.Controllers.Gear.Systems.GearSystemCtrl);
        mockupApp.controller("GearSystemsCtrl", Mockup.Controllers.Gear.Systems.GearSystemsCtrl);
        mockupApp.controller("AddGearSystemCtrl", Mockup.Controllers.Gear.Systems.AddGearSystemCtrl);
        mockupApp.controller("MealsCtrl", Mockup.Controllers.Meals.MealsCtrl);
        mockupApp.controller("MealCtrl", Mockup.Controllers.Meals.MealCtrl);
        mockupApp.controller("AddMealCtrl", Mockup.Controllers.Meals.AddMealCtrl);
        mockupApp.controller("TripItinerariesCtrl", Mockup.Controllers.Trips.Itineraries.TripItinerariesCtrl);
        mockupApp.controller("TripItineraryCtrl", Mockup.Controllers.Trips.Itineraries.TripItineraryCtrl);
        mockupApp.controller("AddTripItineraryCtrl", Mockup.Controllers.Trips.Itineraries.AddTripItineraryCtrl);
        mockupApp.controller("TripPlansCtrl", Mockup.Controllers.Trips.Plans.TripPlansCtrl);
        mockupApp.controller("TripPlanCtrl", Mockup.Controllers.Trips.Plans.TripPlanCtrl);
        mockupApp.controller("AddTripPlanCtrl", Mockup.Controllers.Trips.Plans.AddTripPlanCtrl);
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../AppState.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Collections;
                (function (Collections) {
                    "use strict";
                    class AddGearItemDlgCtrl {
                        constructor($scope, $mdDialog, gearCollection) {
                            $scope.gearCollection = gearCollection;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                            $scope.filterGearItem = (gearItem) => {
                                if ($scope.filterName) {
                                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearItems = () => {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.isGearItemSelected = (gearItem) => {
                                return $scope.gearCollection.containsGearItemById(gearItem.Id);
                            };
                            $scope.toggleGearItemSelected = (gearItem) => {
                                if (!$scope.gearCollection.containsGearItemById(gearItem.Id)) {
                                    try {
                                        $scope.gearCollection.addGearItem(gearItem);
                                    }
                                    catch (error) {
                                        alert(error);
                                    }
                                }
                                else {
                                    if (!$scope.gearCollection.removeGearItemById(gearItem.Id)) {
                                        alert("Cannot remove the item, it may be included by a system or no longer exists.");
                                    }
                                }
                            };
                        }
                    }
                    Collections.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../AppState.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Collections;
                (function (Collections) {
                    "use strict";
                    class AddGearSystemDlgCtrl {
                        constructor($scope, $mdDialog, gearCollection) {
                            $scope.gearCollection = gearCollection;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                            $scope.filterGearSystem = (gearSystem) => {
                                if ($scope.filterName) {
                                    return gearSystem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearSystems = () => {
                                return Mockup.AppState.getInstance().getGearState().getGearSystems();
                            };
                            $scope.isGearSystemSelected = (gearSystem) => {
                                return $scope.gearCollection.containsGearSystemById(gearSystem.Id);
                            };
                            $scope.toggleGearSystemSelected = (gearSystem) => {
                                if (!$scope.gearCollection.containsGearSystemById(gearSystem.Id)) {
                                    try {
                                        $scope.gearCollection.addGearSystem(gearSystem);
                                    }
                                    catch (error) {
                                        alert(error);
                                    }
                                }
                                else {
                                    if (!$scope.gearCollection.removeGearSystemById(gearSystem.Id)) {
                                        alert("Cannot remove the system, it may no longer exist.");
                                    }
                                }
                            };
                        }
                    }
                    Collections.AddGearSystemDlgCtrl = AddGearSystemDlgCtrl;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Collections;
                (function (Collections) {
                    "use strict";
                    class WhatIsGearCollectionDlgCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                        }
                    }
                    Collections.WhatIsGearCollectionDlgCtrl = WhatIsGearCollectionDlgCtrl;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Items;
                (function (Items) {
                    "use strict";
                    class WhatIsGearItemDlgCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                        }
                    }
                    Items.WhatIsGearItemDlgCtrl = WhatIsGearItemDlgCtrl;
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../AppState.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Systems;
                (function (Systems) {
                    "use strict";
                    class AddGearItemDlgCtrl {
                        constructor($scope, $mdDialog, gearSystem) {
                            $scope.gearSystem = gearSystem;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                            $scope.filterGearItem = (gearItem) => {
                                if ($scope.filterName) {
                                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearItems = () => {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.isGearItemSelected = (gearItem) => {
                                return $scope.gearSystem.containsGearItemById(gearItem.Id);
                            };
                            $scope.toggleGearItemSelected = (gearItem) => {
                                if (!$scope.gearSystem.containsGearItemById(gearItem.Id)) {
                                    try {
                                        $scope.gearSystem.addGearItem(gearItem);
                                    }
                                    catch (error) {
                                        alert(error);
                                    }
                                }
                                else {
                                    if (!$scope.gearSystem.removeGearItemById(gearItem.Id)) {
                                        alert("Cannot remove the item, it may no longer exist.");
                                    }
                                }
                            };
                        }
                    }
                    Systems.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                var Systems;
                (function (Systems) {
                    "use strict";
                    class WhatIsGearSystemDlgCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                        }
                    }
                    Systems.WhatIsGearSystemDlgCtrl = WhatIsGearSystemDlgCtrl;
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Meals;
            (function (Meals) {
                "use strict";
                class WhatIsMealDlgCtrl {
                    constructor($scope, $mdDialog) {
                        $scope.close = () => {
                            $mdDialog.hide();
                        };
                    }
                }
                Meals.WhatIsMealDlgCtrl = WhatIsMealDlgCtrl;
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Personal;
            (function (Personal) {
                "use strict";
                class WhatIsPersonalDlgCtrl {
                    constructor($scope, $mdDialog) {
                        $scope.close = () => {
                            $mdDialog.hide();
                        };
                    }
                }
                Personal.WhatIsPersonalDlgCtrl = WhatIsPersonalDlgCtrl;
            })(Personal = Controllers.Personal || (Controllers.Personal = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Itineraries;
                (function (Itineraries) {
                    "use strict";
                    class WhatIsTripItineraryDlgCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                        }
                    }
                    Itineraries.WhatIsTripItineraryDlgCtrl = WhatIsTripItineraryDlgCtrl;
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../AppState.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Plans;
                (function (Plans) {
                    "use strict";
                    class AddGearCollectionDlgCtrl {
                        constructor($scope, $mdDialog, tripPlan) {
                            $scope.tripPlan = tripPlan;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                            $scope.filterGearCollection = (gearCollection) => {
                                if ($scope.filterName) {
                                    return gearCollection.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearCollections = () => {
                                return Mockup.AppState.getInstance().getGearState().getGearCollections();
                            };
                            $scope.isGearCollectionSelected = (gearCollection) => {
                                return $scope.tripPlan.containsGearCollectionById(gearCollection.Id);
                            };
                            $scope.toggleGearCollectionSelected = (gearCollection) => {
                                if (!$scope.tripPlan.containsGearCollectionById(gearCollection.Id)) {
                                    try {
                                        $scope.tripPlan.addGearCollection(gearCollection);
                                    }
                                    catch (error) {
                                        alert(error);
                                    }
                                }
                                else {
                                    if (!$scope.tripPlan.removeGearCollectionById(gearCollection.Id)) {
                                        alert("Cannot remove the collection, it may no longer exist.");
                                    }
                                }
                            };
                        }
                    }
                    Plans.AddGearCollectionDlgCtrl = AddGearCollectionDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../AppState.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Plans;
                (function (Plans) {
                    "use strict";
                    class AddGearItemDlgCtrl {
                        constructor($scope, $mdDialog, tripPlan) {
                            $scope.tripPlan = tripPlan;
                            $scope.orderBy = "name()";
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                            $scope.filterGearItem = (gearItem) => {
                                if ($scope.filterName) {
                                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearItems = () => {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.isGearItemSelected = (gearItem) => {
                                return $scope.tripPlan.containsGearItemById(gearItem.Id);
                            };
                            $scope.toggleGearItemSelected = (gearItem) => {
                                if (!$scope.tripPlan.containsGearItemById(gearItem.Id)) {
                                    try {
                                        $scope.tripPlan.addGearItem(gearItem);
                                    }
                                    catch (error) {
                                        alert(error);
                                    }
                                }
                                else {
                                    if (!$scope.tripPlan.removeGearItemById(gearItem.Id)) {
                                        alert("Cannot remove the item, it may be included by a collection or system or no longer exists.");
                                    }
                                }
                            };
                        }
                    }
                    Plans.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../AppState.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Plans;
                (function (Plans) {
                    "use strict";
                    class AddGearSystemDlgCtrl {
                        constructor($scope, $mdDialog, tripPlan) {
                            $scope.tripPlan = tripPlan;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                            $scope.filterGearSystem = (gearSystem) => {
                                if ($scope.filterName) {
                                    return gearSystem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearSystems = () => {
                                return Mockup.AppState.getInstance().getGearState().getGearSystems();
                            };
                            $scope.isGearSystemSelected = (gearSystem) => {
                                return $scope.tripPlan.containsGearSystemById(gearSystem.Id);
                            };
                            $scope.toggleGearSystemSelected = (gearSystem) => {
                                if (!$scope.tripPlan.containsGearSystemById(gearSystem.Id)) {
                                    try {
                                        $scope.tripPlan.addGearSystem(gearSystem);
                                    }
                                    catch (error) {
                                        alert(error);
                                    }
                                }
                                else {
                                    if (!$scope.tripPlan.removeGearSystemById(gearSystem.Id)) {
                                        alert("Cannot remove the system, it may be included by a collection or no longer exists.");
                                    }
                                }
                            };
                        }
                    }
                    Plans.AddGearSystemDlgCtrl = AddGearSystemDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
/// <reference path="../../../AppState.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Plans;
                (function (Plans) {
                    "use strict";
                    class AddMealDlgCtrl {
                        constructor($scope, $mdDialog, tripPlan) {
                            $scope.tripPlan = tripPlan;
                            $scope.orderBy = "name()";
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                            $scope.filterMeal = (meal) => {
                                if ($scope.filterName) {
                                    return meal.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getMeals = () => {
                                return Mockup.AppState.getInstance().getMealState().getMeals();
                            };
                            $scope.isMealSelected = (meal) => {
                                return $scope.tripPlan.containsMealById(meal.Id);
                            };
                            $scope.toggleMealSelected = (meal) => {
                                if (!$scope.tripPlan.containsMealById(meal.Id)) {
                                    try {
                                        $scope.tripPlan.addMeal(meal);
                                    }
                                    catch (error) {
                                        alert(error);
                                    }
                                }
                                else {
                                    if (!$scope.tripPlan.removeMealById(meal.Id)) {
                                        alert("Cannot remove the meal, it may no longer exist.");
                                    }
                                }
                            };
                        }
                    }
                    Plans.AddMealDlgCtrl = AddMealDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Trips;
            (function (Trips) {
                var Plans;
                (function (Plans) {
                    "use strict";
                    class WhatIsTripPlanDlgCtrl {
                        constructor($scope, $mdDialog) {
                            $scope.close = () => {
                                $mdDialog.hide();
                            };
                        }
                    }
                    Plans.WhatIsTripPlanDlgCtrl = WhatIsTripPlanDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
//# sourceMappingURL=mockup.js.map