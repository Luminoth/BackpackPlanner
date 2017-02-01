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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="GearItemResource.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="GearSystemResource.ts" />
///<reference path="GearItemResource.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Models/Personal/UserInformation.ts" />
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
///<reference path="../../Resources/Personal/UserInformationResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Personal;
            (function (Personal) {
                "use strict";
                var UserInformation = (function () {
                    function UserInformation() {
                        this._firstName = "";
                        this._lastName = "";
                        this._birthDate = new Date();
                        this._sex = "NotSpecified";
                        this._heightInCm = 0;
                        this._weightInGrams = 0;
                    }
                    UserInformation.prototype.firstName = function (firstName) {
                        return arguments.length
                            ? (this._firstName = firstName)
                            : this._firstName;
                    };
                    UserInformation.prototype.lastName = function (lastName) {
                        return arguments.length
                            ? (this._lastName = lastName)
                            : this._lastName;
                    };
                    UserInformation.prototype.birthDate = function (birthDate) {
                        return arguments.length
                            ? (this._birthDate = birthDate)
                            : this._birthDate;
                    };
                    UserInformation.prototype.sex = function (sex) {
                        return arguments.length
                            ? (this._sex = sex)
                            : this._sex;
                    };
                    /* Height/Weight */
                    UserInformation.prototype.heightInUnits = function (height) {
                        return arguments.length
                            ? (this._heightInCm = Mockup.convertUnitsToCentimeters(height, Mockup.AppState.getInstance().getAppSettings().units()))
                            : parseFloat(Mockup.convertCentimetersToUnits(this._heightInCm, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    UserInformation.prototype.weightInUnits = function (weight) {
                        return arguments.length
                            ? (this._weightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                            : parseFloat(Mockup.convertGramsToUnits(this._weightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    /* Load/Save */
                    UserInformation.prototype.update = function (userInformation) {
                        this._firstName = userInformation._firstName;
                        this._lastName = userInformation._lastName;
                        this._birthDate = userInformation._birthDate;
                        this._sex = userInformation._sex;
                        this._heightInCm = userInformation._heightInCm;
                        this._weightInGrams = userInformation._weightInGrams;
                    };
                    UserInformation.prototype.loadFromDevice = function ($q, userInfoResource) {
                        var deferred = $q.defer();
                        this._firstName = userInfoResource.FirstName;
                        this._lastName = userInfoResource.LastName;
                        this._birthDate = new Date(userInfoResource.BirthDate);
                        this._sex = userInfoResource.Sex;
                        this._heightInCm = userInfoResource.HeightInCm;
                        this._weightInGrams = userInfoResource.WeightInGrams;
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    UserInformation.prototype.saveToDevice = function ($q) {
                        alert("UserInformation.saveToDevice");
                        return $q.defer().promise;
                    };
                    return UserInformation;
                }());
                Personal.UserInformation = UserInformation;
            })(Personal = Models.Personal || (Models.Personal = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../Models/AppSettings.ts" />
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
///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../Resources/AppSettingsResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            "use strict";
            var AppSettings = (function () {
                function AppSettings() {
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
                AppSettings.prototype.units = function (units) {
                    return arguments.length
                        ? (this._units = units)
                        : this._units;
                };
                AppSettings.prototype.currency = function (currency) {
                    return arguments.length
                        ? (this._currency = currency)
                        : this._currency;
                };
                AppSettings.prototype.getUltralightMaxWeightInGrams = function () {
                    return this._ultralightClassMaxWeightInGrams;
                };
                AppSettings.prototype.ultralightMaxWeightInUnits = function (weight) {
                    return arguments.length
                        ? (this._ultralightClassMaxWeightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                        : parseFloat(Mockup.convertGramsToUnits(this._ultralightClassMaxWeightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                };
                AppSettings.prototype.getLightweightMaxWeightInGrams = function () {
                    return this._lightweightClassMaxWeightInGrams;
                };
                AppSettings.prototype.lightweightMaxWeightInUnits = function (weight) {
                    return arguments.length
                        ? (this._lightweightClassMaxWeightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                        : parseFloat(Mockup.convertGramsToUnits(this._lightweightClassMaxWeightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                };
                AppSettings.prototype.resetToDefaults = function () {
                    this._units = "Metric";
                    this._currency = "USD";
                    this._ultralightClassMaxWeightInGrams = 4500;
                    this._lightweightClassMaxWeightInGrams = 9000;
                    this._ultralightCategoryMaxWeightInGrams = 225;
                    this._lightCategoryMaxWeightInGrams = 450;
                    this._mediumCategoryMaxWeightInGrams = 1360;
                    this._heavyCategoryMaxWeightInGrams = 2270;
                };
                AppSettings.prototype.getWeightClass = function (weightInGrams) {
                    if (weightInGrams < this._ultralightClassMaxWeightInGrams) {
                        return "Ultralight";
                    }
                    else if (weightInGrams < this._lightweightClassMaxWeightInGrams) {
                        return "Lightweight";
                    }
                    return "Traditional";
                };
                AppSettings.prototype.getWeightCategory = function (weightInGrams) {
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
                };
                /* Load/Save */
                AppSettings.prototype.update = function (appSettings) {
                    this._units = appSettings._units;
                    this._currency = appSettings._currency;
                    this._ultralightClassMaxWeightInGrams = appSettings._ultralightClassMaxWeightInGrams;
                    this._lightweightClassMaxWeightInGrams = appSettings._lightweightClassMaxWeightInGrams;
                    this._ultralightCategoryMaxWeightInGrams = appSettings._ultralightCategoryMaxWeightInGrams;
                    this._lightCategoryMaxWeightInGrams = appSettings._lightCategoryMaxWeightInGrams;
                    this._mediumCategoryMaxWeightInGrams = appSettings._mediumCategoryMaxWeightInGrams;
                    this._heavyCategoryMaxWeightInGrams = appSettings._heavyCategoryMaxWeightInGrams;
                };
                AppSettings.prototype.loadFromDevice = function ($q, appSettingsResource) {
                    var deferred = $q.defer();
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
                };
                AppSettings.prototype.saveToDevice = function ($q) {
                    alert("AppSettings.saveToDevice");
                    return $q.defer().promise;
                };
                return AppSettings;
            }());
            Models.AppSettings = AppSettings;
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Resources/Gear/GearCollectionResource.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Resources/Gear/GearItemResource.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Resources/Gear/GearSystemResource.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
//<reference path="../../Resources/Meals/MealResource.ts"/>
///<reference path="../../AppState.ts"/>
///<reference path="../Entry.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Meals;
            (function (Meals) {
                "use strict";
                var Meal = (function () {
                    function Meal() {
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
                    Object.defineProperty(Meal.prototype, "Id", {
                        get: function () {
                            return this._id;
                        },
                        set: function (id) {
                            this._id = id;
                        },
                        enumerable: true,
                        configurable: true
                    });
                    Meal.prototype.name = function (name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    };
                    Meal.prototype.url = function (url) {
                        return arguments.length
                            ? (this._url = url)
                            : this._url;
                    };
                    Meal.prototype.meal = function (meal) {
                        return arguments.length
                            ? (this._meal = meal)
                            : this._meal;
                    };
                    Meal.prototype.servingCount = function (servingCount) {
                        return arguments.length
                            ? (this._servingCount = servingCount)
                            : this._servingCount;
                    };
                    Meal.prototype.calories = function (calories) {
                        return arguments.length
                            ? (this._calories = calories)
                            : this._calories;
                    };
                    Meal.prototype.getCaloriesPerWeightUnit = function () {
                        return 0 == this._calories ? 0 : this._calories / this.weightInUnits();
                    };
                    Meal.prototype.proteinInGrams = function (proteinInGrams) {
                        return arguments.length
                            ? (this._proteinInGrams = proteinInGrams)
                            : this._proteinInGrams;
                    };
                    Meal.prototype.fiberInGrams = function (fiberInGrams) {
                        return arguments.length
                            ? (this._fiberInGrams = fiberInGrams)
                            : this._fiberInGrams;
                    };
                    Meal.prototype.note = function (note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    };
                    /* Weight/Cost */
                    Meal.prototype.getWeightInGrams = function () {
                        return this._weightInGrams;
                    };
                    Meal.prototype.weightInUnits = function (weight) {
                        return arguments.length
                            ? (this._weightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                            : parseFloat(Mockup.convertGramsToUnits(this._weightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    Meal.prototype.getCostInUSDP = function () {
                        return this._costInUSDP;
                    };
                    Meal.prototype.costInCurrency = function (cost) {
                        return arguments.length
                            ? (this._costInUSDP = Mockup.convertCurrencyToUSDP(cost, Mockup.AppState.getInstance().getAppSettings().currency()))
                            : Mockup.convertUSDPToCurrency(this._costInUSDP, Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    Meal.prototype.getCostPerUnitInCurrency = function () {
                        var weightInUnits = Mockup.convertGramsToUnits(this._weightInGrams, /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                        var costInCurrency = Mockup.convertUSDPToCurrency(this._costInUSDP, /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    Meal.prototype.update = function (meal) {
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
                    };
                    Meal.prototype.loadFromDevice = function ($q, mealResource) {
                        var deferred = $q.defer();
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
                    };
                    Meal.prototype.saveToDevice = function ($q) {
                        alert("Meal.saveToDevice");
                        return $q.defer().promise;
                    };
                    return Meal;
                }());
                Meals.Meal = Meal;
                var MealEntry = (function () {
                    function MealEntry(mealId, count) {
                        this._mealId = -1;
                        this._count = 1;
                        this._mealId = mealId;
                        if (count) {
                            this._count = count;
                        }
                    }
                    MealEntry.prototype.getMealId = function () {
                        return this._mealId;
                    };
                    MealEntry.prototype.count = function (count) {
                        return arguments.length
                            ? (this._count = count)
                            : this._count;
                    };
                    MealEntry.prototype.getName = function () {
                        var meal = Mockup.AppState.getInstance().getMealState().getMealById(this._mealId);
                        if (!meal) {
                            return "";
                        }
                        return meal.name();
                    };
                    MealEntry.prototype.getCalories = function () {
                        var meal = Mockup.AppState.getInstance().getMealState().getMealById(this._mealId);
                        if (!meal) {
                            return 0;
                        }
                        return this._count * meal.calories();
                    };
                    MealEntry.prototype.getTotalWeightInGrams = function () {
                        var meal = Mockup.AppState.getInstance().getMealState().getMealById(this._mealId);
                        if (!meal) {
                            return 0;
                        }
                        return this._count * meal.getWeightInGrams();
                    };
                    MealEntry.prototype.getTotalWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams(), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    MealEntry.prototype.getCostInUSDP = function () {
                        var meal = Mockup.AppState.getInstance().getMealState().getMealById(this._mealId);
                        if (!meal) {
                            return 0;
                        }
                        return this._count * meal.getCostInUSDP();
                    };
                    MealEntry.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    return MealEntry;
                }());
                Meals.MealEntry = MealEntry;
            })(Meals = Models.Meals || (Models.Meals = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Models/Meals/Meal.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Resources/Meals/MealResource.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
//<reference path="../../Resources/Trips/TripItineraryResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Trips;
            (function (Trips) {
                "use strict";
                var TripItinerary = (function () {
                    function TripItinerary() {
                        this._id = -1;
                        this._name = "";
                        this._note = "";
                    }
                    Object.defineProperty(TripItinerary.prototype, "Id", {
                        get: function () {
                            return this._id;
                        },
                        set: function (id) {
                            this._id = id;
                        },
                        enumerable: true,
                        configurable: true
                    });
                    TripItinerary.prototype.name = function (name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    };
                    TripItinerary.prototype.note = function (note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    };
                    /* Load/Save */
                    TripItinerary.prototype.update = function (tripItinerary) {
                        this._name = tripItinerary._name;
                        this._note = tripItinerary._note;
                    };
                    TripItinerary.prototype.loadFromDevice = function ($q, tripItineraryResource) {
                        var deferred = $q.defer();
                        this._id = tripItineraryResource.Id;
                        this._name = tripItineraryResource.Name;
                        this._note = tripItineraryResource.Note;
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    TripItinerary.prototype.saveToDevice = function ($q) {
                        alert("TripItinerary.saveToDevice");
                        return $q.defer().promise;
                    };
                    return TripItinerary;
                }());
                Trips.TripItinerary = TripItinerary;
            })(Trips = Models.Trips || (Models.Trips = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Models/Trips/TripItinerary.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Resources/Trips/TripItineraryResource.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../Resources/Gear/GearItemResource.ts"/>
///<reference path="../../AppState.ts"/>
///<reference path="../Entry.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearItem = (function () {
                    function GearItem() {
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
                    Object.defineProperty(GearItem.prototype, "Id", {
                        get: function () {
                            return this._id;
                        },
                        set: function (id) {
                            this._id = id;
                        },
                        enumerable: true,
                        configurable: true
                    });
                    GearItem.prototype.name = function (name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    };
                    GearItem.prototype.url = function (url) {
                        return arguments.length
                            ? (this._url = url)
                            : this._url;
                    };
                    GearItem.prototype.make = function (make) {
                        return arguments.length
                            ? (this._make = make)
                            : this._make;
                    };
                    GearItem.prototype.model = function (model) {
                        return arguments.length
                            ? (this._model = model)
                            : this._model;
                    };
                    GearItem.prototype.carried = function (carried) {
                        return arguments.length
                            ? (this._carried = carried)
                            : this._carried;
                    };
                    GearItem.prototype.isCarried = function () {
                        return "NotCarried" != this._carried;
                    };
                    GearItem.prototype.isWorn = function () {
                        return "Worn" == this._carried;
                    };
                    GearItem.prototype.isConsumable = function (isConsumable) {
                        return arguments.length
                            ? (this._isConsumable = isConsumable)
                            : this._isConsumable;
                    };
                    GearItem.prototype.consumedPerDay = function (consumedPerDay) {
                        return arguments.length
                            ? (this._consumedPerDay = consumedPerDay)
                            : this._consumedPerDay;
                    };
                    GearItem.prototype.note = function (note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    };
                    /* Weight/Cost */
                    GearItem.prototype.getWeightCategory = function () {
                        if (!this.isCarried()) {
                            return "None";
                        }
                        return Mockup.AppState.getInstance().getAppSettings().getWeightCategory(this._weightInGrams);
                    };
                    GearItem.prototype.getWeightInGrams = function () {
                        return this._weightInGrams;
                    };
                    GearItem.prototype.weightInUnits = function (weight) {
                        return arguments.length
                            ? (this._weightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().units()))
                            : parseFloat(Mockup.convertGramsToUnits(this._weightInGrams, Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearItem.prototype.getCostInUSDP = function () {
                        return this._costInUSDP;
                    };
                    GearItem.prototype.costInCurrency = function (cost) {
                        return arguments.length
                            ? (this._costInUSDP = Mockup.convertCurrencyToUSDP(cost, Mockup.AppState.getInstance().getAppSettings().currency()))
                            : Mockup.convertUSDPToCurrency(this._costInUSDP, Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    GearItem.prototype.getCostPerUnitInCurrency = function () {
                        var weightInUnits = Mockup.convertGramsToUnits(this._weightInGrams, /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                        var costInCurrency = Mockup.convertUSDPToCurrency(this._costInUSDP, /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    GearItem.prototype.update = function (gearItem) {
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
                    };
                    GearItem.prototype.loadFromDevice = function ($q, gearItemResource) {
                        var deferred = $q.defer();
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
                    };
                    GearItem.prototype.saveToDevice = function ($q) {
                        alert("GearItem.saveToDevice");
                        return $q.defer().promise;
                    };
                    return GearItem;
                }());
                Gear.GearItem = GearItem;
                var GearItemEntry = (function () {
                    function GearItemEntry(gearItemId, count) {
                        this._gearItemId = -1;
                        this._count = 1;
                        this._gearItemId = gearItemId;
                        if (count) {
                            this._count = count;
                        }
                    }
                    GearItemEntry.prototype.getGearItemId = function () {
                        return this._gearItemId;
                    };
                    GearItemEntry.prototype.count = function (count) {
                        return arguments.length
                            ? (this._count = count)
                            : this._count;
                    };
                    GearItemEntry.prototype.getName = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return "";
                        }
                        return gearItem.name();
                    };
                    GearItemEntry.prototype.isCarried = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return false;
                        }
                        return gearItem.isCarried();
                    };
                    GearItemEntry.prototype.isWorn = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return false;
                        }
                        return gearItem.isWorn();
                    };
                    GearItemEntry.prototype.isConsumable = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return false;
                        }
                        return gearItem.isConsumable();
                    };
                    GearItemEntry.prototype.getTotalWeightInGrams = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return 0;
                        }
                        return this._count * gearItem.getWeightInGrams();
                    };
                    GearItemEntry.prototype.getTotalWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams(), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearItemEntry.prototype.getCostInUSDP = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this._gearItemId);
                        if (!gearItem) {
                            return 0;
                        }
                        return this._count * gearItem.getCostInUSDP();
                    };
                    GearItemEntry.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    return GearItemEntry;
                }());
                Gear.GearItemEntry = GearItemEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/underscore/underscore.d.ts" />
///<reference path="../../Resources/Gear/GearSystemResource.ts"/>
///<reference path="../../AppState.ts"/>
///<reference path="../Entry.ts"/>
///<reference path="GearItem.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearSystem = (function () {
                    function GearSystem() {
                        this._id = -1;
                        this._name = "";
                        this._note = "";
                        this._gearItems = [];
                    }
                    Object.defineProperty(GearSystem.prototype, "Id", {
                        get: function () {
                            return this._id;
                        },
                        set: function (id) {
                            this._id = id;
                        },
                        enumerable: true,
                        configurable: true
                    });
                    GearSystem.prototype.name = function (name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    };
                    GearSystem.prototype.note = function (note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    };
                    /* Gear Items */
                    GearSystem.prototype.getGearItems = function () {
                        return this._gearItems;
                    };
                    GearSystem.prototype.getGearItemCount = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var count = 0;
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    };
                    GearSystem.prototype.getGearItemEntryIndexById = function (gearItemId) {
                        return _.findIndex(this._gearItems, function (gearItemEntry) {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    };
                    GearSystem.prototype.containsGearItemById = function (gearItemId) {
                        return undefined != _.find(this._gearItems, function (gearSystemEntry) {
                            return gearSystemEntry.getGearItemId() == gearItemId;
                        });
                    };
                    GearSystem.prototype.addGearItem = function (gearItem) {
                        if (this.containsGearItemById(gearItem.Id)) {
                            throw "The system already contains this item!";
                        }
                        this._gearItems.push(new Gear.GearItemEntry(gearItem.Id));
                    };
                    GearSystem.prototype.addGearItemEntry = function (gearItemId, count) {
                        if (this.containsGearItemById(gearItemId)) {
                            throw "The system already contains this item!";
                        }
                        this._gearItems.push(new Gear.GearItemEntry(gearItemId, count));
                    };
                    GearSystem.prototype.removeGearItemById = function (gearItemId) {
                        var idx = this.getGearItemEntryIndexById(gearItemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearItems.splice(idx, 1);
                        return true;
                    };
                    GearSystem.prototype.removeAllGearItems = function () {
                        this._gearItems = [];
                    };
                    /* Weight/Cost */
                    GearSystem.prototype.getTotalWeightInGrams = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            weightInGrams += gearItemEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    };
                    GearSystem.prototype.getTotalWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearSystem.prototype.getBaseWeightInGrams = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
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
                    };
                    GearSystem.prototype.getBaseWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearSystem.prototype.getPackWeightInGrams = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
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
                    };
                    GearSystem.prototype.getPackWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearSystem.prototype.getSkinOutWeightInGrams = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var packWeightInGrams = this.getPackWeightInGrams(visitedGearItems);
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
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
                    };
                    GearSystem.prototype.getSkinOutWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearSystem.prototype.getCostInUSDP = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var costInUSDP = 0;
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            costInUSDP += gearItemEntry.getCostInUSDP();
                        }
                        return costInUSDP;
                    };
                    GearSystem.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    GearSystem.prototype.getCostPerUnitInCurrency = function () {
                        var weightInUnits = Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    GearSystem.prototype.update = function (gearSystem) {
                        this._name = gearSystem._name;
                        this._note = gearSystem._note;
                        this._gearItems = [];
                        for (var i = 0; i < gearSystem._gearItems.length; ++i) {
                            var gearItemEntry = gearSystem._gearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.getGearItemId(), gearItemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                    };
                    GearSystem.prototype.loadFromDevice = function ($q, gearSystemResource) {
                        var deferred = $q.defer();
                        this._id = gearSystemResource.Id;
                        this._name = gearSystemResource.Name;
                        this._note = gearSystemResource.Note;
                        for (var i = 0; i < gearSystemResource.GearItems.length; ++i) {
                            var gearItemEntry = gearSystemResource.GearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    GearSystem.prototype.saveToDevice = function ($q) {
                        alert("GearSystem.saveToDevice");
                        return $q.defer().promise;
                    };
                    return GearSystem;
                }());
                Gear.GearSystem = GearSystem;
                var GearSystemEntry = (function () {
                    function GearSystemEntry(gearSystemId, count) {
                        this._gearSystemId = -1;
                        this._count = 1;
                        this._gearSystemId = gearSystemId;
                        if (count) {
                            this._count = count;
                        }
                    }
                    GearSystemEntry.prototype.getGearSystemId = function () {
                        return this._gearSystemId;
                    };
                    GearSystemEntry.prototype.count = function (count) {
                        return arguments.length
                            ? (this._count = count)
                            : this._count;
                    };
                    GearSystemEntry.prototype.getName = function () {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return "";
                        }
                        return gearSystem.name();
                    };
                    GearSystemEntry.prototype.getGearItemCount = function (visitedGearItems) {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getGearItemCount(visitedGearItems);
                    };
                    GearSystemEntry.prototype.getTotalWeightInGrams = function (visitedGearItems) {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getTotalWeightInGrams(visitedGearItems);
                    };
                    GearSystemEntry.prototype.getTotalWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearSystemEntry.prototype.getBaseWeightInGrams = function (visitedGearItems) {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getBaseWeightInGrams(visitedGearItems);
                    };
                    GearSystemEntry.prototype.getBaseWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearSystemEntry.prototype.getPackWeightInGrams = function (visitedGearItems) {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getPackWeightInGrams(visitedGearItems);
                    };
                    GearSystemEntry.prototype.getPackWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearSystemEntry.prototype.getSkinOutWeightInGrams = function (visitedGearItems) {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getSkinOutWeightInGrams(visitedGearItems);
                    };
                    GearSystemEntry.prototype.getSkinOutWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearSystemEntry.prototype.getCostInUSDP = function (visitedGearItems) {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this._gearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this._count * gearSystem.getCostInUSDP(visitedGearItems);
                    };
                    GearSystemEntry.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    return GearSystemEntry;
                }());
                Gear.GearSystemEntry = GearSystemEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/underscore/underscore.d.ts" />
//<reference path="../../Resources/Trips/TripPlanResource.ts"/>
///<reference path="../../AppState.ts"/>
///<reference path="../Gear/GearCollection.ts"/>
///<reference path="../Gear/GearItem.ts"/>
///<reference path="../Gear/GearSystem.ts"/>
///<reference path="../Meals/Meal.ts"/>
///<reference path="TripItinerary.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Trips;
            (function (Trips) {
                "use strict";
                var TripPlan = (function () {
                    function TripPlan() {
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
                    Object.defineProperty(TripPlan.prototype, "Id", {
                        get: function () {
                            return this._id;
                        },
                        set: function (id) {
                            this._id = id;
                        },
                        enumerable: true,
                        configurable: true
                    });
                    TripPlan.prototype.name = function (name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    };
                    TripPlan.prototype.startDate = function (startDate) {
                        return arguments.length
                            ? (this._startDate = startDate)
                            : this._startDate;
                    };
                    TripPlan.prototype.endDate = function (endDate) {
                        return arguments.length
                            ? (this._endDate = endDate)
                            : this._endDate;
                    };
                    TripPlan.prototype.getTripItineraryName = function () {
                        if (this._tripItineraryId < 1) {
                            return "No trip itinerary";
                        }
                        var tripItinerary = Mockup.AppState.getInstance().getTripState().getTripItineraryById(this._tripItineraryId);
                        if (!tripItinerary) {
                            return "Could not find trip itinerary";
                        }
                        return tripItinerary.name();
                    };
                    TripPlan.prototype.tripItineraryId = function (tripItineraryId) {
                        return arguments.length
                            ? (this._tripItineraryId = tripItineraryId)
                            : this._tripItineraryId;
                    };
                    TripPlan.prototype.note = function (note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    };
                    TripPlan.prototype.getTotalGearItemCount = function () {
                        var visitedGearItems = [];
                        var count = 0;
                        for (var i = 0; i < this._gearCollections.length; ++i) {
                            var gearCollectionEntry = this._gearCollections[i];
                            count += gearCollectionEntry.getGearItemCount(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            count += gearSystemEntry.getGearItemCount(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    };
                    TripPlan.prototype.getTotalCalories = function () {
                        var visitedMeals = [];
                        var calories = 0;
                        for (var i = 0; i < this._meals.length; ++i) {
                            var mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            calories += mealEntry.getCalories();
                        }
                        return calories;
                    };
                    /* Gear Collections */
                    TripPlan.prototype.getGearCollections = function () {
                        return this._gearCollections;
                    };
                    TripPlan.prototype.getGearCollectionCount = function (visitedGearCollections) {
                        if (!visitedGearCollections) {
                            visitedGearCollections = [];
                        }
                        var count = 0;
                        for (var i = 0; i < this._gearCollections.length; ++i) {
                            var gearCollectionEntry = this._gearCollections[i];
                            if (_.contains(visitedGearCollections, gearCollectionEntry.getGearCollectionId())) {
                                continue;
                            }
                            visitedGearCollections.push(gearCollectionEntry.getGearCollectionId());
                            count += gearCollectionEntry.count();
                        }
                        return count;
                    };
                    TripPlan.prototype.getGearCollectionEntryIndexById = function (gearCollectionId) {
                        return _.findIndex(this._gearCollections, function (gearCollectionEntry) {
                            return gearCollectionEntry.getGearCollectionId() == gearCollectionId;
                        });
                    };
                    TripPlan.prototype.containsGearCollectionById = function (gearCollectionId) {
                        return undefined != _.find(this._gearCollections, function (gearCollectionEntry) {
                            return gearCollectionEntry.getGearCollectionId() == gearCollectionId;
                        });
                    };
                    TripPlan.prototype.containsGearCollectionSystems = function (gearCollection) {
                        var gearSystems = gearCollection.getGearSystems();
                        for (var i = 0; i < gearSystems.length; ++i) {
                            var gearSystemEntry = gearSystems[i];
                            if (this.containsGearSystemById(gearSystemEntry.getGearSystemId())) {
                                return true;
                            }
                            var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(gearSystemEntry.getGearSystemId());
                            if (gearSystem && this.containsGearSystemItems(gearSystem)) {
                                return true;
                            }
                        }
                        return false;
                    };
                    TripPlan.prototype.containsGearCollectionItems = function (gearCollection) {
                        var gearItems = gearCollection.getGearItems();
                        for (var i = 0; i < gearItems.length; ++i) {
                            var gearItemEntry = gearItems[i];
                            if (this.containsGearItemById(gearItemEntry.getGearItemId())) {
                                return true;
                            }
                        }
                        return false;
                    };
                    TripPlan.prototype.addGearCollection = function (gearCollection) {
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
                    };
                    TripPlan.prototype.addGearCollectionEntry = function (gearCollectionId, count) {
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
                    };
                    TripPlan.prototype.removeGearCollectionById = function (gearCollectionId) {
                        var idx = this.getGearCollectionEntryIndexById(gearCollectionId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearCollections.splice(idx, 1);
                        return true;
                    };
                    TripPlan.prototype.removeAllGearCollections = function () {
                        this._gearCollections = [];
                    };
                    /* Gear Systems */
                    TripPlan.prototype.getGearSystems = function () {
                        return this._gearSystems;
                    };
                    TripPlan.prototype.getGearSystemCount = function (visitedGearSystems) {
                        if (!visitedGearSystems) {
                            visitedGearSystems = [];
                        }
                        var count = 0;
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            if (_.contains(visitedGearSystems, gearSystemEntry.getGearSystemId())) {
                                continue;
                            }
                            visitedGearSystems.push(gearSystemEntry.getGearSystemId());
                            count += gearSystemEntry.count();
                        }
                        return count;
                    };
                    TripPlan.prototype.getGearSystemEntryIndexById = function (gearSystemId) {
                        return _.findIndex(this._gearSystems, function (gearSystemEntry) {
                            return gearSystemEntry.getGearSystemId() == gearSystemId;
                        });
                    };
                    TripPlan.prototype.containsGearSystemById = function (gearSystemId) {
                        if (_.find(this._gearCollections, function (gearCollectionEntry) {
                            var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(gearCollectionEntry.getGearCollectionId());
                            if (!gearCollection) {
                                return false;
                            }
                            return gearCollection.containsGearSystemById(gearSystemId);
                        })) {
                            return true;
                        }
                        return undefined != _.find(this._gearSystems, function (gearSystemEntry) {
                            return gearSystemEntry.getGearSystemId() == gearSystemId;
                        });
                    };
                    TripPlan.prototype.containsGearSystemItems = function (gearSystem) {
                        var gearItems = gearSystem.getGearItems();
                        for (var i = 0; i < gearItems.length; ++i) {
                            var gearItemEntry = gearItems[i];
                            if (this.containsGearItemById(gearItemEntry.getGearItemId())) {
                                return true;
                            }
                        }
                        return false;
                    };
                    TripPlan.prototype.addGearSystem = function (gearSystem) {
                        if (this.containsGearSystemById(gearSystem.Id)) {
                            throw "The plan already contains this system!";
                        }
                        if (this.containsGearSystemItems(gearSystem)) {
                            throw "The plan already contains items from this system!";
                        }
                        this._gearSystems.push(new Models.Gear.GearSystemEntry(gearSystem.Id));
                    };
                    TripPlan.prototype.addGearSystemEntry = function (gearSystemId, count) {
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
                    };
                    TripPlan.prototype.removeGearSystemById = function (gearSystemId) {
                        var idx = this.getGearSystemEntryIndexById(gearSystemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearSystems.splice(idx, 1);
                        return true;
                    };
                    TripPlan.prototype.removeAllGearSystems = function () {
                        this._gearSystems = [];
                    };
                    /* Gear Items */
                    TripPlan.prototype.getGearItems = function () {
                        return this._gearItems;
                    };
                    TripPlan.prototype.getGearItemCount = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var count = 0;
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    };
                    TripPlan.prototype.getGearItemEntryIndexById = function (gearItemId) {
                        return _.findIndex(this._gearItems, function (gearItemEntry) {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    };
                    TripPlan.prototype.containsGearItemById = function (gearItemId) {
                        if (_.find(this._gearCollections, function (gearCollectionEntry) {
                            var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(gearCollectionEntry.getGearCollectionId());
                            if (!gearCollection) {
                                return false;
                            }
                            return gearCollection.containsGearItemById(gearItemId);
                        })) {
                            return true;
                        }
                        if (_.find(this._gearSystems, function (gearSystemEntry) {
                            var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(gearSystemEntry.getGearSystemId());
                            if (!gearSystem) {
                                return false;
                            }
                            return gearSystem.containsGearItemById(gearItemId);
                        })) {
                            return true;
                        }
                        return undefined != _.find(this._gearItems, function (gearItemEntry) {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    };
                    TripPlan.prototype.addGearItem = function (gearItem) {
                        if (this.containsGearItemById(gearItem.Id)) {
                            throw "The plan already contains this item!";
                        }
                        this._gearItems.push(new Models.Gear.GearItemEntry(gearItem.Id));
                    };
                    TripPlan.prototype.addGearItemEntry = function (gearItemId, count) {
                        if (this.containsGearItemById(gearItemId)) {
                            throw "The plan already contains this item!";
                        }
                        this._gearItems.push(new Models.Gear.GearItemEntry(gearItemId, count));
                    };
                    TripPlan.prototype.removeGearItemById = function (gearItemId) {
                        var idx = this.getGearItemEntryIndexById(gearItemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearItems.splice(idx, 1);
                        return true;
                    };
                    TripPlan.prototype.removeAllGearItems = function () {
                        this._gearItems = [];
                    };
                    /* Meals */
                    TripPlan.prototype.getMeals = function () {
                        return this._meals;
                    };
                    TripPlan.prototype.getMealCount = function () {
                        var visitedMeals = [];
                        var count = 0;
                        for (var i = 0; i < this._meals.length; ++i) {
                            var mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            count += mealEntry.count();
                        }
                        return count;
                    };
                    TripPlan.prototype.getMealEntryIndexById = function (mealId) {
                        return _.findIndex(this._meals, function (mealEntry) {
                            return mealEntry.getMealId() == mealId;
                        });
                    };
                    TripPlan.prototype.containsMealById = function (mealId) {
                        return undefined != _.find(this._meals, function (mealEntry) {
                            return mealEntry.getMealId() == mealId;
                        });
                    };
                    TripPlan.prototype.addMeal = function (meal) {
                        if (this.containsMealById(meal.Id)) {
                            return false;
                        }
                        this._meals.push(new Models.Meals.MealEntry(meal.Id));
                        return true;
                    };
                    TripPlan.prototype.addMealEntry = function (mealId, count) {
                        if (this.containsMealById(mealId)) {
                            throw "The plan already contains this meal!";
                        }
                        this._meals.push(new Models.Meals.MealEntry(mealId, count));
                    };
                    TripPlan.prototype.removeMealById = function (mealId) {
                        var idx = this.getMealEntryIndexById(mealId);
                        if (idx < 0) {
                            throw "The plan already contains this meal!";
                        }
                        this._meals.splice(idx, 1);
                    };
                    TripPlan.prototype.removeAllMeals = function () {
                        this._meals = [];
                    };
                    /* Weight/Cost */
                    TripPlan.prototype.getWeightClass = function () {
                        return Mockup.AppState.getInstance().getAppSettings().getWeightClass(this.getBaseWeightInGrams([]));
                    };
                    TripPlan.prototype.getTotalWeightInGrams = function (visitedGearItems, visitedMeals) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        if (!visitedMeals) {
                            visitedMeals = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearCollections.length; ++i) {
                            var gearCollectionEntry = this._gearCollections[i];
                            weightInGrams += gearCollectionEntry.getTotalWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getTotalWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            weightInGrams += gearItemEntry.getTotalWeightInGrams();
                        }
                        for (var i = 0; i < this._meals.length; ++i) {
                            var mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            weightInGrams += mealEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    };
                    TripPlan.prototype.getTotalWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getTotalWeightInGrams([], []), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    TripPlan.prototype.getBaseWeightInGrams = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearCollections.length; ++i) {
                            var gearCollectionEntry = this._gearCollections[i];
                            weightInGrams += gearCollectionEntry.getBaseWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getBaseWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
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
                    };
                    TripPlan.prototype.getBaseWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    TripPlan.prototype.getPackWeightInGrams = function (visitedGearItems, visitedMeals) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        if (!visitedMeals) {
                            visitedMeals = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearCollections.length; ++i) {
                            var gearCollectionEntry = this._gearCollections[i];
                            weightInGrams += gearCollectionEntry.getPackWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getPackWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried or consumable but not worn
                            if (gearItemEntry.isCarried() && !gearItemEntry.isWorn() || gearItemEntry.isConsumable()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        for (var i = 0; i < this._meals.length; ++i) {
                            var mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            weightInGrams += mealEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    };
                    TripPlan.prototype.getPackWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getPackWeightInGrams([], []), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    TripPlan.prototype.getSkinOutWeightInGrams = function (visitedGearItems, visitedMeals) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        if (!visitedMeals) {
                            visitedMeals = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearCollections.length; ++i) {
                            var gearCollectionEntry = this._gearCollections[i];
                            weightInGrams += gearCollectionEntry.getSkinOutWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getSkinOutWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            // carried, worn, and consumable gear items
                            if (gearItemEntry.isCarried()) {
                                visitedGearItems.push(gearItemEntry.getGearItemId());
                                weightInGrams += gearItemEntry.getTotalWeightInGrams();
                            }
                        }
                        for (var i = 0; i < this._meals.length; ++i) {
                            var mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            weightInGrams += mealEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    };
                    TripPlan.prototype.getSkinOutWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([], []), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    TripPlan.prototype.getCostInUSDP = function (visitedGearItems, visitedMeals) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var costInUSDP = 0;
                        for (var i = 0; i < this._gearCollections.length; ++i) {
                            var gearCollectionEntry = this._gearCollections[i];
                            costInUSDP += gearCollectionEntry.getCostInUSDP(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            costInUSDP += gearSystemEntry.getCostInUSDP(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            costInUSDP += gearItemEntry.getCostInUSDP();
                        }
                        for (var i = 0; i < this._meals.length; ++i) {
                            var mealEntry = this._meals[i];
                            if (_.contains(visitedMeals, mealEntry.getMealId())) {
                                continue;
                            }
                            visitedMeals.push(mealEntry.getMealId());
                            costInUSDP += mealEntry.getCostInUSDP();
                        }
                        return costInUSDP;
                    };
                    TripPlan.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([], []), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    TripPlan.prototype.getCostPerUnitInCurrency = function () {
                        var weightInUnits = Mockup.convertGramsToUnits(this.getTotalWeightInGrams([], []), Mockup.AppState.getInstance().getAppSettings().units());
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP([], []), Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    TripPlan.prototype.update = function (tripPlan) {
                        this._name = tripPlan._name;
                        this._startDate = this._startDate;
                        this._endDate = this._endDate;
                        this._tripItineraryId = tripPlan._tripItineraryId;
                        this._note = tripPlan._note;
                        this._gearCollections = [];
                        for (var i = 0; i < tripPlan._gearCollections.length; ++i) {
                            var gearCollectionEntry = tripPlan._gearCollections[i];
                            try {
                                this.addGearCollectionEntry(gearCollectionEntry.getGearCollectionId(), gearCollectionEntry.count());
                            }
                            catch (error) {
                            }
                        }
                        this._gearSystems = [];
                        for (var i = 0; i < tripPlan._gearSystems.length; ++i) {
                            var gearSystemEntry = tripPlan._gearSystems[i];
                            try {
                                this.addGearSystemEntry(gearSystemEntry.getGearSystemId(), gearSystemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                        this._gearItems = [];
                        for (var i = 0; i < tripPlan._gearItems.length; ++i) {
                            var gearItemEntry = tripPlan._gearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.getGearItemId(), gearItemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                        this._meals = [];
                        for (var i = 0; i < tripPlan._meals.length; ++i) {
                            var mealEntry = tripPlan._meals[i];
                            try {
                                this.addMealEntry(mealEntry.getMealId(), mealEntry.count());
                            }
                            catch (error) {
                            }
                        }
                    };
                    TripPlan.prototype.loadFromDevice = function ($q, tripPlanResource) {
                        var deferred = $q.defer();
                        this._id = tripPlanResource.Id;
                        this._name = tripPlanResource.Name;
                        this._startDate = new Date(tripPlanResource.StartDate);
                        this._endDate = new Date(tripPlanResource.EndDate);
                        this._tripItineraryId = tripPlanResource.TripItineraryId;
                        this._note = tripPlanResource.Note;
                        for (var i = 0; i < tripPlanResource.GearCollections.length; ++i) {
                            var gearCollectionEntry = tripPlanResource.GearCollections[i];
                            try {
                                this.addGearCollectionEntry(gearCollectionEntry.GearCollectionId, gearCollectionEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        for (var i = 0; i < tripPlanResource.GearSystems.length; ++i) {
                            var gearSystemEntry = tripPlanResource.GearSystems[i];
                            try {
                                this.addGearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        for (var i = 0; i < tripPlanResource.GearItems.length; ++i) {
                            var gearItemEntry = tripPlanResource.GearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        for (var i = 0; i < tripPlanResource.Meals.length; ++i) {
                            var mealEntry = tripPlanResource.Meals[i];
                            try {
                                this.addMealEntry(mealEntry.MealId, mealEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    TripPlan.prototype.saveToDevice = function ($q) {
                        alert("TripPlan.saveToDevice");
                        return $q.defer().promise;
                    };
                    return TripPlan;
                }());
                Trips.TripPlan = TripPlan;
            })(Trips = Models.Trips || (Models.Trips = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Models/Trips/TripPlan.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Resources/Trips/TripPlanResource.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Resources/Personal/UserInformationResource.ts" />
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
///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../Resources/AppSettingsResource.ts" />
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
///<reference path="../scripts/typings/angularjs/angular.d.ts" />
///<reference path="Models/Gear/GearCollection.ts" />
///<reference path="Models/Gear/GearItem.ts" />
///<reference path="Models/Gear/GearSystem.ts" />
///<reference path="Resources/Gear/GearCollectionResource.ts" />
///<reference path="Resources/Gear/GearItemResource.ts" />
///<reference path="Resources/Gear/GearSystemResource.ts" />
///<reference path="Services/Gear/GearCollectionService.ts"/>
///<reference path="Services/Gear/GearItemService.ts"/>
///<reference path="Services/Gear/GearSystemService.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var GearState = (function () {
            function GearState() {
                /* Gear Items */
                this._gearItems = [];
                /* Gear Systems */
                this._gearSystems = [];
                /* Gear Collections */
                this._gearCollections = [];
            }
            // TODO: this should be a read-only collection
            GearState.prototype.getGearItems = function () {
                return this._gearItems;
            };
            GearState.prototype.getGearItemIndexById = function (gearItemId) {
                for (var i = 0; i < this._gearItems.length; ++i) {
                    var gearItem = this._gearItems[i];
                    if (gearItem.Id == gearItemId) {
                        return i;
                    }
                }
                return -1;
            };
            GearState.prototype.getGearItemById = function (gearItemId) {
                var idx = this.getGearItemIndexById(gearItemId);
                return idx < 0 ? null : this._gearItems[idx];
            };
            GearState.prototype.getNextGearItemId = function () {
                return ++GearState._lastGearItemId;
            };
            GearState.prototype.addGearItem = function (gearItem) {
                if (gearItem.Id < 0) {
                    gearItem.Id = this.getNextGearItemId();
                }
                else if (gearItem.Id > GearState._lastGearItemId) {
                    GearState._lastGearItemId = gearItem.Id;
                }
                this._gearItems.push(gearItem);
                return gearItem.Id;
            };
            GearState.prototype.deleteGearItem = function (gearItem) {
                var idx = this.getGearItemIndexById(gearItem.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearItems.splice(idx, 1);
                return true;
            };
            GearState.prototype.deleteAllGearItems = function () {
                this._gearItems = [];
            };
            // TODO: this should be a read-only collection
            GearState.prototype.getGearSystems = function () {
                return this._gearSystems;
            };
            GearState.prototype.getGearSystemIndexById = function (gearSystemId) {
                for (var i = 0; i < this._gearSystems.length; ++i) {
                    var gearSystem = this._gearSystems[i];
                    if (gearSystem.Id == gearSystemId) {
                        return i;
                    }
                }
                return -1;
            };
            GearState.prototype.getGearSystemById = function (gearSystemId) {
                var idx = this.getGearSystemIndexById(gearSystemId);
                return idx < 0 ? null : this._gearSystems[idx];
            };
            GearState.prototype.getNextGearSystemId = function () {
                return ++GearState._lastGearSystemId;
            };
            GearState.prototype.addGearSystem = function (gearSystem) {
                if (gearSystem.Id < 0) {
                    gearSystem.Id = this.getNextGearSystemId();
                }
                else if (gearSystem.Id > GearState._lastGearSystemId) {
                    GearState._lastGearSystemId = gearSystem.Id;
                }
                this._gearSystems.push(gearSystem);
                return gearSystem.Id;
            };
            GearState.prototype.removeGearItemFromSystems = function (gearItem) {
                var gearSystems = [];
                for (var i = 0; i < this._gearSystems.length; ++i) {
                    var gearSystem = this._gearSystems[i];
                    if (gearSystem.containsGearItemById(gearItem.Id)) {
                        gearSystem.removeGearItemById(gearItem.Id);
                        gearSystems.push(gearSystem);
                    }
                }
                return gearSystems;
            };
            GearState.prototype.deleteGearSystem = function (gearSystem) {
                var idx = this.getGearSystemIndexById(gearSystem.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearSystems.splice(idx, 1);
                return true;
            };
            GearState.prototype.deleteAllGearSystems = function () {
                this._gearSystems = [];
            };
            // TODO: this should be a read-only collection
            GearState.prototype.getGearCollections = function () {
                return this._gearCollections;
            };
            GearState.prototype.getGearCollectionIndexById = function (gearCollectionId) {
                for (var i = 0; i < this._gearCollections.length; ++i) {
                    var gearCollection = this._gearCollections[i];
                    if (gearCollection.Id == gearCollectionId) {
                        return i;
                    }
                }
                return -1;
            };
            GearState.prototype.getGearCollectionById = function (gearCollectionId) {
                var idx = this.getGearCollectionIndexById(gearCollectionId);
                return idx < 0 ? null : this._gearCollections[idx];
            };
            GearState.prototype.getNextGearCollectionId = function () {
                return ++GearState._lastGearCollectionId;
            };
            GearState.prototype.addGearCollection = function (gearCollection) {
                if (gearCollection.Id < 0) {
                    gearCollection.Id = this.getNextGearCollectionId();
                }
                else if (gearCollection.Id > GearState._lastGearCollectionId) {
                    GearState._lastGearCollectionId = gearCollection.Id;
                }
                this._gearCollections.push(gearCollection);
                return gearCollection.Id;
            };
            GearState.prototype.removeGearSystemFromCollections = function (gearSystem) {
                var gearCollections = [];
                for (var i = 0; i < this._gearCollections.length; ++i) {
                    var gearCollection = this._gearCollections[i];
                    if (gearCollection.containsGearSystemById(gearSystem.Id)) {
                        gearCollection.removeGearSystemById(gearSystem.Id);
                        gearCollections.push(gearCollection);
                    }
                }
                return gearCollections;
            };
            GearState.prototype.removeGearItemFromCollections = function (gearItem) {
                var gearCollections = [];
                for (var i = 0; i < this._gearCollections.length; ++i) {
                    var gearCollection = this._gearCollections[i];
                    if (gearCollection.containsGearItemById(gearItem.Id)) {
                        gearCollection.removeGearItemById(gearItem.Id);
                        gearCollections.push(gearCollection);
                    }
                }
                return gearCollections;
            };
            GearState.prototype.deleteGearCollection = function (gearCollection) {
                var idx = this.getGearCollectionIndexById(gearCollection.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearCollections.splice(idx, 1);
                return true;
            };
            GearState.prototype.deleteAllGearCollections = function () {
                this._gearCollections = [];
            };
            /* Utilities */
            GearState.prototype.deleteAllData = function () {
                this.deleteAllGearCollections();
                this.deleteAllGearSystems();
                this.deleteAllGearItems();
            };
            /* Load/Save */
            GearState.prototype.loadGearItems = function ($q, gearItemResources) {
                var _this = this;
                var promises = [];
                this._gearItems = [];
                for (var i = 0; i < gearItemResources.length; ++i) {
                    var gearItem = new Mockup.Models.Gear.GearItem();
                    promises.push(gearItem.loadFromDevice($q, gearItemResources[i]).then(function (loadedGearItem) {
                        _this.addGearItem(loadedGearItem);
                    }));
                }
                return $q.all(promises);
            };
            GearState.prototype.loadGearSystems = function ($q, gearSystemResources) {
                var _this = this;
                var promises = [];
                this._gearSystems = [];
                for (var i = 0; i < gearSystemResources.length; ++i) {
                    var gearSystem = new Mockup.Models.Gear.GearSystem();
                    promises.push(gearSystem.loadFromDevice($q, gearSystemResources[i]).then(function (loadedGearSystem) {
                        _this.addGearSystem(loadedGearSystem);
                    }));
                }
                return $q.all(promises);
            };
            GearState.prototype.loadGearCollections = function ($q, gearCollectionResources) {
                var _this = this;
                var promises = [];
                this._gearCollections = [];
                for (var i = 0; i < gearCollectionResources.length; ++i) {
                    var gearCollection = new Mockup.Models.Gear.GearCollection();
                    promises.push(gearCollection.loadFromDevice($q, gearCollectionResources[i]).then(function (loadedGearCollection) {
                        _this.addGearCollection(loadedGearCollection);
                    }));
                }
                return $q.all(promises);
            };
            GearState.prototype.loadFromDevice = function ($q, gearItemService, gearSystemService, gearCollectionService) {
                var _this = this;
                var promises = [];
                promises.push(gearItemService.query().$promise.then(function (gearItemResources) {
                    _this.loadGearItems($q, gearItemResources).then(function () {
                    });
                }));
                promises.push(gearSystemService.query().$promise.then(function (gearSystemResources) {
                    _this.loadGearSystems($q, gearSystemResources).then(function () {
                    });
                }));
                promises.push(gearCollectionService.query().$promise.then(function (gearCollectionResources) {
                    _this.loadGearCollections($q, gearCollectionResources).then(function () {
                    });
                }));
                return $q.all(promises);
            };
            GearState.prototype.saveToDevice = function ($q) {
                alert("GearState.saveToDevice");
                return $q.defer().promise;
            };
            GearState._lastGearItemId = 0;
            GearState._lastGearSystemId = 0;
            GearState._lastGearCollectionId = 0;
            return GearState;
        }());
        Mockup.GearState = GearState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular.d.ts" />
///<reference path="Models/Meals/Meal.ts" />
///<reference path="Resources/Meals/MealResource.ts" />
///<reference path="Services/Meals/MealService.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var MealState = (function () {
            function MealState() {
                /* Meals */
                this._meals = [];
            }
            // TODO: this should be a read-only collection
            MealState.prototype.getMeals = function () {
                return this._meals;
            };
            MealState.prototype.getMealIndexById = function (mealId) {
                for (var i = 0; i < this._meals.length; ++i) {
                    var meal = this._meals[i];
                    if (meal.Id == mealId) {
                        return i;
                    }
                }
                return -1;
            };
            MealState.prototype.getMealById = function (mealId) {
                var idx = this.getMealIndexById(mealId);
                return idx < 0 ? null : this._meals[idx];
            };
            MealState.prototype.getNextMealId = function () {
                return ++MealState._lastMealId;
            };
            MealState.prototype.addMeal = function (meal) {
                if (meal.Id < 0) {
                    meal.Id = this.getNextMealId();
                }
                else if (meal.Id > MealState._lastMealId) {
                    MealState._lastMealId = meal.Id;
                }
                this._meals.push(meal);
                return meal.Id;
            };
            MealState.prototype.deleteMeal = function (meal) {
                var idx = this.getMealIndexById(meal.Id);
                if (idx < 0) {
                    return false;
                }
                this._meals.splice(idx, 1);
                return true;
            };
            MealState.prototype.deleteAllMeals = function () {
                this._meals = [];
            };
            /* Utilities */
            MealState.prototype.deleteAllData = function () {
                this.deleteAllMeals();
            };
            /* Load/Save */
            MealState.prototype.loadMeals = function ($q, mealResources) {
                var _this = this;
                var promises = [];
                this._meals = [];
                for (var i = 0; i < mealResources.length; ++i) {
                    var meal = new Mockup.Models.Meals.Meal();
                    promises.push(meal.loadFromDevice($q, mealResources[i]).then(function (loadedMeal) {
                        _this.addMeal(loadedMeal);
                    }));
                }
                return $q.all(promises);
            };
            MealState.prototype.loadFromDevice = function ($q, mealService) {
                var _this = this;
                var promises = [];
                promises.push(mealService.query().$promise.then(function (mealResources) {
                    _this.loadMeals($q, mealResources).then(function () {
                    });
                }));
                return $q.all(promises);
            };
            MealState.prototype.saveToDevice = function ($q) {
                alert("MealState.saveToDevice");
                return $q.defer().promise;
            };
            MealState._lastMealId = 0;
            return MealState;
        }());
        Mockup.MealState = MealState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular.d.ts" />
///<reference path="Models/Trips/TripItinerary.ts" />
///<reference path="Models/Trips/TripPlan.ts" />
///<reference path="Resources/Trips/TripItineraryResource.ts" />
///<reference path="Resources/Trips/TripPlanResource.ts" />
///<reference path="Services/Trips/TripItineraryService.ts" />
///<reference path="Services/Trips/TripPlanService.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var TripState = (function () {
            function TripState() {
                /* Trip Itineraries */
                this._tripItineraries = [];
                /* Trip Plans */
                this._tripPlans = [];
            }
            // TODO: this should be a read-only collection
            TripState.prototype.getTripItineraries = function () {
                return this._tripItineraries;
            };
            TripState.prototype.getTripItineraryIndexById = function (tripItineraryId) {
                for (var i = 0; i < this._tripItineraries.length; ++i) {
                    var tripItinerary = this._tripItineraries[i];
                    if (tripItinerary.Id == tripItineraryId) {
                        return i;
                    }
                }
                return -1;
            };
            TripState.prototype.getTripItineraryById = function (tripItineraryId) {
                var idx = this.getTripItineraryIndexById(tripItineraryId);
                return idx < 0 ? null : this._tripItineraries[idx];
            };
            TripState.prototype.getNextTripItineraryId = function () {
                return ++TripState._lastTripItineraryId;
            };
            TripState.prototype.addTripItinerary = function (tripItinerary) {
                if (tripItinerary.Id < 0) {
                    tripItinerary.Id = this.getNextTripItineraryId();
                }
                else if (tripItinerary.Id > TripState._lastTripItineraryId) {
                    TripState._lastTripItineraryId = tripItinerary.Id;
                }
                this._tripItineraries.push(tripItinerary);
                return tripItinerary.Id;
            };
            TripState.prototype.deleteTripItinerary = function (tripItinerary) {
                var idx = this.getTripItineraryIndexById(tripItinerary.Id);
                if (idx < 0) {
                    return false;
                }
                this._tripItineraries.splice(idx, 1);
                return true;
            };
            TripState.prototype.deleteAllTripItineraries = function () {
                this._tripItineraries = [];
            };
            // TODO: this should be a read-only collection
            TripState.prototype.getTripPlans = function () {
                return this._tripPlans;
            };
            TripState.prototype.getTripPlanIndexById = function (tripPlanId) {
                for (var i = 0; i < this._tripPlans.length; ++i) {
                    var tripPlan = this._tripPlans[i];
                    if (tripPlan.Id == tripPlanId) {
                        return i;
                    }
                }
                return -1;
            };
            TripState.prototype.getTripPlanById = function (tripPlanId) {
                var idx = this.getTripPlanIndexById(tripPlanId);
                return idx < 0 ? null : this._tripPlans[idx];
            };
            TripState.prototype.getNextTripPlanId = function () {
                return ++TripState._lastTripPlanId;
            };
            TripState.prototype.addTripPlan = function (tripPlan) {
                if (tripPlan.Id < 0) {
                    tripPlan.Id = this.getNextTripPlanId();
                }
                else if (tripPlan.Id > TripState._lastTripPlanId) {
                    TripState._lastTripPlanId = tripPlan.Id;
                }
                this._tripPlans.push(tripPlan);
                return tripPlan.Id;
            };
            TripState.prototype.removeGearCollectionFromPlans = function (gearCollection) {
                var tripPlans = [];
                for (var i = 0; i < this._tripPlans.length; ++i) {
                    var tripPlan = this._tripPlans[i];
                    if (tripPlan.containsGearCollectionById(tripPlan.Id)) {
                        tripPlan.removeGearCollectionById(tripPlan.Id);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            };
            TripState.prototype.removeGearSystemFromPlans = function (gearSystem) {
                var tripPlans = [];
                for (var i = 0; i < this._tripPlans.length; ++i) {
                    var tripPlan = this._tripPlans[i];
                    if (tripPlan.containsGearSystemById(gearSystem.Id)) {
                        tripPlan.removeGearSystemById(gearSystem.Id);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            };
            TripState.prototype.removeGearItemFromPlans = function (gearItem) {
                var tripPlans = [];
                for (var i = 0; i < this._tripPlans.length; ++i) {
                    var tripPlan = this._tripPlans[i];
                    if (tripPlan.containsGearItemById(gearItem.Id)) {
                        tripPlan.removeGearItemById(gearItem.Id);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            };
            TripState.prototype.removeMealFromPlans = function (meal) {
                var tripPlans = [];
                for (var i = 0; i < this._tripPlans.length; ++i) {
                    var tripPlan = this._tripPlans[i];
                    if (tripPlan.containsMealById(tripPlan.Id)) {
                        tripPlan.removeMealById(tripPlan.Id);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            };
            TripState.prototype.removeTripItineraryFromPlans = function (tripItinerary) {
                var tripPlans = [];
                for (var i = 0; i < this._tripPlans.length; ++i) {
                    var tripPlan = this._tripPlans[i];
                    if (tripPlan.tripItineraryId() == tripItinerary.Id) {
                        tripPlan.tripItineraryId(-1);
                        tripPlans.push(tripPlan);
                    }
                }
                return tripPlans;
            };
            TripState.prototype.deleteTripPlan = function (tripPlan) {
                var idx = this.getTripPlanIndexById(tripPlan.Id);
                if (idx < 0) {
                    return false;
                }
                this._tripPlans.splice(idx, 1);
                return true;
            };
            TripState.prototype.deleteAllTripPlans = function () {
                this._tripPlans = [];
            };
            /* Utilities */
            TripState.prototype.deleteAllData = function () {
                this.deleteAllTripItineraries();
                this.deleteAllTripPlans();
            };
            /* Load/Save */
            TripState.prototype.loadTripItineraries = function ($q, tripItineraryResources) {
                var _this = this;
                var promises = [];
                this._tripItineraries = [];
                for (var i = 0; i < tripItineraryResources.length; ++i) {
                    var tripItinerary = new Mockup.Models.Trips.TripItinerary();
                    promises.push(tripItinerary.loadFromDevice($q, tripItineraryResources[i]).then(function (loadedTripItinerary) {
                        _this.addTripItinerary(loadedTripItinerary);
                    }));
                }
                return $q.all(promises);
            };
            TripState.prototype.loadTripPlans = function ($q, tripPlanResources) {
                var _this = this;
                var promises = [];
                this._tripPlans = [];
                for (var i = 0; i < tripPlanResources.length; ++i) {
                    var tripPlan = new Mockup.Models.Trips.TripPlan();
                    promises.push(tripPlan.loadFromDevice($q, tripPlanResources[i]).then(function (loadedTripPlan) {
                        _this.addTripPlan(loadedTripPlan);
                    }));
                }
                return $q.all(promises);
            };
            TripState.prototype.loadFromDevice = function ($q, tripItineraryService, tripPlanService) {
                var _this = this;
                var promises = [];
                promises.push(tripItineraryService.query().$promise.then(function (tripItineraryResources) {
                    _this.loadTripItineraries($q, tripItineraryResources).then(function () {
                    });
                }));
                promises.push(tripPlanService.query().$promise.then(function (tripPlanResources) {
                    _this.loadTripPlans($q, tripPlanResources).then(function () {
                    });
                }));
                return $q.all(promises);
            };
            TripState.prototype.saveToDevice = function ($q) {
                alert("TripState.saveToDevice");
                return $q.defer().promise;
            };
            TripState._lastTripItineraryId = 0;
            TripState._lastTripPlanId = 0;
            return TripState;
        }());
        Mockup.TripState = TripState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular.d.ts" />
///<reference path="Actions/Command.ts"/>
///<reference path="Models/Personal/UserInformation.ts" />
///<reference path="Models/AppSettings.ts" />
///<reference path="Resources/Personal/UserInformationResource.ts" />
///<reference path="Resources/AppSettingsResource.ts" />
///<reference path="Services/Gear/GearCollectionService.ts"/>
///<reference path="Services/Gear/GearItemService.ts"/>
///<reference path="Services/Gear/GearSystemService.ts"/>
///<reference path="Services/Meals/MealService.ts"/>
///<reference path="Services/Trips/TripItineraryService.ts"/>
///<reference path="Services/Trips/TripPlanService.ts"/>
///<reference path="Services/Personal/UserInformationService.ts"/>
///<reference path="Services/AppSettingsService.ts"/>
///<reference path="GearState.ts" />
///<reference path="MealState.ts" />
///<reference path="TripState.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var AppState = (function () {
            function AppState() {
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
            AppState.getInstance = function () {
                return AppState._instance;
            };
            AppState.prototype.executeAction = function (action) {
                this._lastAction = action;
                this._lastAction.doAction();
            };
            AppState.prototype.undoAction = function () {
                if (!this._lastAction) {
                    return;
                }
                this._lastAction.undoAction();
                this._lastAction = undefined;
            };
            AppState.prototype.getAppSettings = function () {
                return this._appSettings;
            };
            AppState.prototype.getUserInformation = function () {
                return this._userInformation;
            };
            AppState.prototype.getGearState = function () {
                return this._gearState;
            };
            AppState.prototype.getMealState = function () {
                return this._mealState;
            };
            AppState.prototype.getTripState = function () {
                return this._tripState;
            };
            /* Utilities */
            AppState.prototype.deleteAllData = function () {
                this._gearState.deleteAllData();
                this._mealState.deleteAllData();
                this._tripState.deleteAllData();
            };
            /* Load/Save */
            AppState.prototype.loadFromDevice = function ($q, appSettingsService, userInformationService, gearItemService, gearSystemService, gearCollectionService, mealService, tripItineraryService, tripPlanService) {
                var _this = this;
                var promises = [];
                // load the application settings
                promises.push(appSettingsService.get().$promise.then(function (appSettingsResource) {
                    _this._appSettings.loadFromDevice($q, appSettingsResource).then(function () {
                    });
                }));
                // load the user's personal information
                promises.push(userInformationService.get().$promise.then(function (userInfoResource) {
                    _this._userInformation.loadFromDevice($q, userInfoResource).then(function () {
                    });
                }));
                promises.push(this._gearState.loadFromDevice($q, gearItemService, gearSystemService, gearCollectionService));
                promises.push(this._mealState.loadFromDevice($q, mealService));
                promises.push(this._tripState.loadFromDevice($q, tripItineraryService, tripPlanService));
                return $q.all(promises);
            };
            AppState.prototype.saveToDevice = function ($q) {
                alert("AppState.saveToDevice");
                return $q.defer().promise;
            };
            /* Import/Export */
            AppState.prototype.importFromCloudStorage = function ($q, cloudStorage) {
                // mockup does nothing here
                return $q.defer().promise;
            };
            AppState.prototype.exportToCloudStorage = function ($q, cloudStorage) {
                // mockup does nothing here
                return $q.defer().promise;
            };
            /* Singleton */
            AppState._instance = new AppState();
            return AppState;
        }());
        Mockup.AppState = AppState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/underscore/underscore.d.ts" />
///<reference path="../../Resources/Gear/GearCollectionResource.ts"/>
///<reference path="../../AppState.ts"/>
///<reference path="../Entry.ts"/>
///<reference path="GearItem.ts"/>
///<reference path="GearSystem.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearCollection = (function () {
                    function GearCollection() {
                        this._id = -1;
                        this._name = "";
                        this._note = "";
                        this._gearSystems = [];
                        this._gearItems = [];
                    }
                    Object.defineProperty(GearCollection.prototype, "Id", {
                        get: function () {
                            return this._id;
                        },
                        set: function (id) {
                            this._id = id;
                        },
                        enumerable: true,
                        configurable: true
                    });
                    GearCollection.prototype.name = function (name) {
                        return arguments.length
                            ? (this._name = name)
                            : this._name;
                    };
                    GearCollection.prototype.note = function (note) {
                        return arguments.length
                            ? (this._note = note)
                            : this._note;
                    };
                    GearCollection.prototype.getTotalGearItemCount = function () {
                        var visitedGearItems = [];
                        var count = 0;
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            count += gearSystemEntry.getGearItemCount(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    };
                    /* Gear Systems */
                    GearCollection.prototype.getGearSystems = function () {
                        return this._gearSystems;
                    };
                    GearCollection.prototype.getGearSystemCount = function (visitedGearSystems) {
                        if (!visitedGearSystems) {
                            visitedGearSystems = [];
                        }
                        var count = 0;
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            if (_.contains(visitedGearSystems, gearSystemEntry.getGearSystemId())) {
                                continue;
                            }
                            visitedGearSystems.push(gearSystemEntry.getGearSystemId());
                            count += gearSystemEntry.count();
                        }
                        return count;
                    };
                    GearCollection.prototype.getGearSystemEntryIndexById = function (gearSystemId) {
                        return _.findIndex(this._gearSystems, function (gearSystemEntry) {
                            return gearSystemEntry.getGearSystemId() == gearSystemId;
                        });
                    };
                    GearCollection.prototype.containsGearSystemById = function (gearSystemId) {
                        return undefined != _.find(this._gearSystems, function (gearSystemEntry) {
                            return gearSystemEntry.getGearSystemId() == gearSystemId;
                        });
                    };
                    GearCollection.prototype.containsGearSystemItems = function (gearSystem) {
                        var gearItems = gearSystem.getGearItems();
                        for (var i = 0; i < gearItems.length; ++i) {
                            var gearItemEntry = gearItems[i];
                            if (this.containsGearItemById(gearItemEntry.getGearItemId())) {
                                return true;
                            }
                        }
                        return false;
                    };
                    GearCollection.prototype.addGearSystem = function (gearSystem) {
                        if (this.containsGearSystemById(gearSystem.Id)) {
                            throw "The collection already contains this system!";
                        }
                        if (this.containsGearSystemItems(gearSystem)) {
                            throw "The collection already contains items from this system!";
                        }
                        this._gearSystems.push(new Gear.GearSystemEntry(gearSystem.Id));
                    };
                    GearCollection.prototype.addGearSystemEntry = function (gearSystemId, count) {
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
                    };
                    GearCollection.prototype.removeGearSystemById = function (gearSystemId) {
                        var idx = this.getGearSystemEntryIndexById(gearSystemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearSystems.splice(idx, 1);
                        return true;
                    };
                    GearCollection.prototype.removeAllGearSystems = function () {
                        this._gearSystems = [];
                    };
                    /* Gear Items */
                    GearCollection.prototype.getGearItems = function () {
                        return this._gearItems;
                    };
                    GearCollection.prototype.getGearItemCount = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var count = 0;
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            count += gearItemEntry.count();
                        }
                        return count;
                    };
                    GearCollection.prototype.getGearItemEntryIndexById = function (gearItemId) {
                        return _.findIndex(this._gearItems, function (gearItemEntry) {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    };
                    GearCollection.prototype.containsGearItemById = function (gearItemId) {
                        if (_.find(this._gearSystems, function (gearSystemEntry) {
                            var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(gearSystemEntry.getGearSystemId());
                            if (!gearSystem) {
                                return false;
                            }
                            return gearSystem.containsGearItemById(gearItemId);
                        })) {
                            return true;
                        }
                        return undefined != _.find(this._gearItems, function (gearItemEntry) {
                            return gearItemEntry.getGearItemId() == gearItemId;
                        });
                    };
                    GearCollection.prototype.addGearItem = function (gearItem) {
                        if (this.containsGearItemById(gearItem.Id)) {
                            throw "The collection already contains this item!";
                        }
                        this._gearItems.push(new Gear.GearItemEntry(gearItem.Id));
                    };
                    GearCollection.prototype.addGearItemEntry = function (gearItemId, count) {
                        if (this.containsGearItemById(gearItemId)) {
                            throw "The collection already contains this item!";
                        }
                        this._gearItems.push(new Gear.GearItemEntry(gearItemId, count));
                    };
                    GearCollection.prototype.removeGearItemById = function (gearItemId) {
                        var idx = this.getGearItemEntryIndexById(gearItemId);
                        if (idx < 0) {
                            return false;
                        }
                        this._gearItems.splice(idx, 1);
                        return true;
                    };
                    GearCollection.prototype.removeAllGearItems = function () {
                        this._gearItems = [];
                    };
                    /* Weight/Cost */
                    GearCollection.prototype.getTotalWeightInGrams = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getTotalWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            weightInGrams += gearItemEntry.getTotalWeightInGrams();
                        }
                        return weightInGrams;
                    };
                    GearCollection.prototype.getTotalWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    GearCollection.prototype.getBaseWeightInGrams = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getBaseWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
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
                    };
                    GearCollection.prototype.getBaseWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    GearCollection.prototype.getPackWeightInGrams = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getPackWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
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
                    };
                    GearCollection.prototype.getPackWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    GearCollection.prototype.getSkinOutWeightInGrams = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var weightInGrams = 0;
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            weightInGrams += gearSystemEntry.getSkinOutWeightInGrams(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
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
                    };
                    GearCollection.prototype.getSkinOutWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    GearCollection.prototype.getCostInUSDP = function (visitedGearItems) {
                        if (!visitedGearItems) {
                            visitedGearItems = [];
                        }
                        var costInUSDP = 0;
                        for (var i = 0; i < this._gearSystems.length; ++i) {
                            var gearSystemEntry = this._gearSystems[i];
                            costInUSDP += gearSystemEntry.getCostInUSDP(visitedGearItems);
                        }
                        for (var i = 0; i < this._gearItems.length; ++i) {
                            var gearItemEntry = this._gearItems[i];
                            if (_.contains(visitedGearItems, gearItemEntry.getGearItemId())) {
                                continue;
                            }
                            visitedGearItems.push(gearItemEntry.getGearItemId());
                            costInUSDP += gearItemEntry.getCostInUSDP();
                        }
                        return costInUSDP;
                    };
                    GearCollection.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    GearCollection.prototype.getCostPerUnitInCurrency = function () {
                        var weightInUnits = Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units());
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    GearCollection.prototype.update = function (gearCollection) {
                        this._name = gearCollection._name;
                        this._note = gearCollection._note;
                        this._gearSystems = [];
                        for (var i = 0; i < gearCollection._gearSystems.length; ++i) {
                            var gearSystemEntry = gearCollection._gearSystems[i];
                            try {
                                this.addGearSystemEntry(gearSystemEntry.getGearSystemId(), gearSystemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                        this._gearItems = [];
                        for (var i = 0; i < gearCollection._gearItems.length; ++i) {
                            var gearItemEntry = gearCollection._gearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.getGearItemId(), gearItemEntry.count());
                            }
                            catch (error) {
                            }
                        }
                    };
                    GearCollection.prototype.loadFromDevice = function ($q, gearCollectionResource) {
                        var deferred = $q.defer();
                        this._id = gearCollectionResource.Id;
                        this._name = gearCollectionResource.Name;
                        this._note = gearCollectionResource.Note;
                        for (var i = 0; i < gearCollectionResource.GearSystems.length; ++i) {
                            var gearSystemEntry = gearCollectionResource.GearSystems[i];
                            try {
                                this.addGearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        for (var i = 0; i < gearCollectionResource.GearItems.length; ++i) {
                            var gearItemEntry = gearCollectionResource.GearItems[i];
                            try {
                                this.addGearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count);
                            }
                            catch (error) {
                            }
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    GearCollection.prototype.saveToDevice = function ($q) {
                        alert("GearCollection.saveToDevice");
                        return $q.defer().promise;
                    };
                    return GearCollection;
                }());
                Gear.GearCollection = GearCollection;
                var GearCollectionEntry = (function () {
                    function GearCollectionEntry(gearCollectionId, count) {
                        this._gearCollectionId = -1;
                        this._count = 1;
                        this._gearCollectionId = gearCollectionId;
                        if (count) {
                            this._count = count;
                        }
                    }
                    GearCollectionEntry.prototype.getGearCollectionId = function () {
                        return this._gearCollectionId;
                    };
                    GearCollectionEntry.prototype.count = function (count) {
                        return arguments.length
                            ? (this._count = count)
                            : this._count;
                    };
                    GearCollectionEntry.prototype.getName = function () {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return "";
                        }
                        return gearCollection.name();
                    };
                    GearCollectionEntry.prototype.getTotalGearItemCount = function () {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getTotalGearItemCount();
                    };
                    GearCollectionEntry.prototype.getGearSystemCount = function (visitedGearSystems) {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getGearSystemCount(visitedGearSystems);
                    };
                    GearCollectionEntry.prototype.getGearItemCount = function (visitedGearItems) {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getGearItemCount(visitedGearItems);
                    };
                    GearCollectionEntry.prototype.getTotalWeightInGrams = function (visitedGearItems) {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getTotalWeightInGrams(visitedGearItems);
                    };
                    GearCollectionEntry.prototype.getTotalWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getTotalWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearCollectionEntry.prototype.getBaseWeightInGrams = function (visitedGearItems) {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getBaseWeightInGrams(visitedGearItems);
                    };
                    GearCollectionEntry.prototype.getBaseWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getBaseWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearCollectionEntry.prototype.getPackWeightInGrams = function (visitedGearItems) {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getPackWeightInGrams(visitedGearItems);
                    };
                    GearCollectionEntry.prototype.getPackWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getPackWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearCollectionEntry.prototype.getSkinOutWeightInGrams = function (visitedGearItems) {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getSkinOutWeightInGrams(visitedGearItems);
                    };
                    GearCollectionEntry.prototype.getSkinOutWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getSkinOutWeightInGrams([]), /*units*/ Mockup.AppState.getInstance().getAppSettings().units()).toFixed(2));
                    };
                    GearCollectionEntry.prototype.getCostInUSDP = function (visitedGearItems) {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this._gearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this._count * gearCollection.getCostInUSDP(visitedGearItems);
                    };
                    GearCollectionEntry.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP([]), /*currency*/ Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    return GearCollectionEntry;
                }());
                Gear.GearCollectionEntry = GearCollectionEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../Models/Gear/GearCollection.ts"/>
///<reference path="../../Command.ts"/>
///<reference path="../../../AppState.ts"/>
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
                    var DeleteGearCollectionAction = (function () {
                        function DeleteGearCollectionAction() {
                            this._tripPlans = [];
                        }
                        DeleteGearCollectionAction.prototype.doAction = function () {
                            this._tripPlans = Mockup.AppState.getInstance().getTripState().removeGearCollectionFromPlans(this.GearCollection);
                            Mockup.AppState.getInstance().getGearState().deleteGearCollection(this.GearCollection);
                        };
                        DeleteGearCollectionAction.prototype.undoAction = function () {
                            Mockup.AppState.getInstance().getGearState().addGearCollection(this.GearCollection);
                            for (var i = 0; i < this._tripPlans.length; ++i) {
                                var tripPlan = this._tripPlans[i];
                                tripPlan.addGearCollection(this.GearCollection);
                            }
                        };
                        return DeleteGearCollectionAction;
                    }());
                    Collections.DeleteGearCollectionAction = DeleteGearCollectionAction;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Actions.Gear || (Actions.Gear = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../Models/Gear/GearItem.ts"/>
///<reference path="../../Command.ts"/>
///<reference path="../../../AppState.ts"/>
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
                    var DeleteGearItemAction = (function () {
                        function DeleteGearItemAction() {
                            this._gearSystems = [];
                            this._gearCollections = [];
                            this._tripPlans = [];
                        }
                        DeleteGearItemAction.prototype.doAction = function () {
                            this._tripPlans = Mockup.AppState.getInstance().getTripState().removeGearItemFromPlans(this.GearItem);
                            this._gearCollections = Mockup.AppState.getInstance().getGearState().removeGearItemFromCollections(this.GearItem);
                            this._gearSystems = Mockup.AppState.getInstance().getGearState().removeGearItemFromSystems(this.GearItem);
                            Mockup.AppState.getInstance().getGearState().deleteGearItem(this.GearItem);
                        };
                        DeleteGearItemAction.prototype.undoAction = function () {
                            Mockup.AppState.getInstance().getGearState().addGearItem(this.GearItem);
                            for (var i = 0; i < this._gearSystems.length; ++i) {
                                var gearSystem = this._gearSystems[i];
                                gearSystem.addGearItem(this.GearItem);
                            }
                            for (var i = 0; i < this._gearCollections.length; ++i) {
                                var gearCollection = this._gearCollections[i];
                                gearCollection.addGearItem(this.GearItem);
                            }
                            for (var i = 0; i < this._tripPlans.length; ++i) {
                                var tripPlan = this._tripPlans[i];
                                tripPlan.addGearItem(this.GearItem);
                            }
                        };
                        return DeleteGearItemAction;
                    }());
                    Items.DeleteGearItemAction = DeleteGearItemAction;
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Actions.Gear || (Actions.Gear = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../Models/Gear/GearSystem.ts"/>
///<reference path="../../Command.ts"/>
///<reference path="../../../AppState.ts"/>
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
                    var DeleteGearSystemAction = (function () {
                        function DeleteGearSystemAction() {
                            this._gearCollections = [];
                            this._tripPlans = [];
                        }
                        DeleteGearSystemAction.prototype.doAction = function () {
                            this._tripPlans = Mockup.AppState.getInstance().getTripState().removeGearSystemFromPlans(this.GearSystem);
                            this._gearCollections = Mockup.AppState.getInstance().getGearState().removeGearSystemFromCollections(this.GearSystem);
                            Mockup.AppState.getInstance().getGearState().deleteGearSystem(this.GearSystem);
                        };
                        DeleteGearSystemAction.prototype.undoAction = function () {
                            Mockup.AppState.getInstance().getGearState().addGearSystem(this.GearSystem);
                            for (var i = 0; i < this._gearCollections.length; ++i) {
                                var gearCollection = this._gearCollections[i];
                                gearCollection.addGearSystem(this.GearSystem);
                            }
                            for (var i = 0; i < this._tripPlans.length; ++i) {
                                var tripPlan = this._tripPlans[i];
                                tripPlan.addGearSystem(this.GearSystem);
                            }
                        };
                        return DeleteGearSystemAction;
                    }());
                    Systems.DeleteGearSystemAction = DeleteGearSystemAction;
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Actions.Gear || (Actions.Gear = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../Models/Meals/Meal.ts"/>
///<reference path="../Command.ts"/>
///<reference path="../../AppState.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Actions;
        (function (Actions) {
            var Meals;
            (function (Meals) {
                "use strict";
                var DeleteMealAction = (function () {
                    function DeleteMealAction() {
                        this._tripPlans = [];
                    }
                    DeleteMealAction.prototype.doAction = function () {
                        this._tripPlans = Mockup.AppState.getInstance().getTripState().removeMealFromPlans(this.Meal);
                        Mockup.AppState.getInstance().getMealState().deleteMeal(this.Meal);
                    };
                    DeleteMealAction.prototype.undoAction = function () {
                        Mockup.AppState.getInstance().getMealState().addMeal(this.Meal);
                        for (var i = 0; i < this._tripPlans.length; ++i) {
                            var tripPlan = this._tripPlans[i];
                            tripPlan.addMeal(this.Meal);
                        }
                    };
                    return DeleteMealAction;
                }());
                Meals.DeleteMealAction = DeleteMealAction;
            })(Meals = Actions.Meals || (Actions.Meals = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../Models/Trips/TripItinerary.ts"/>
///<reference path="../../Command.ts"/>
///<reference path="../../../AppState.ts"/>
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
                    var DeleteTripItineraryAction = (function () {
                        function DeleteTripItineraryAction() {
                            this._tripPlans = [];
                        }
                        DeleteTripItineraryAction.prototype.doAction = function () {
                            this._tripPlans = Mockup.AppState.getInstance().getTripState().removeTripItineraryFromPlans(this.TripItinerary);
                            Mockup.AppState.getInstance().getTripState().deleteTripItinerary(this.TripItinerary);
                        };
                        DeleteTripItineraryAction.prototype.undoAction = function () {
                            Mockup.AppState.getInstance().getTripState().addTripItinerary(this.TripItinerary);
                            for (var i = 0; i < this._tripPlans.length; ++i) {
                                var tripPlan = this._tripPlans[i];
                                tripPlan.tripItineraryId(this.TripItinerary.Id);
                            }
                        };
                        return DeleteTripItineraryAction;
                    }());
                    Itineraries.DeleteTripItineraryAction = DeleteTripItineraryAction;
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Actions.Trips || (Actions.Trips = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../Models/Trips/TripPlan.ts"/>
///<reference path="../../Command.ts"/>
///<reference path="../../../AppState.ts"/>
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
                    var DeleteTripPlanAction = (function () {
                        function DeleteTripPlanAction() {
                        }
                        DeleteTripPlanAction.prototype.doAction = function () {
                            Mockup.AppState.getInstance().getTripState().deleteTripPlan(this.TripPlan);
                        };
                        DeleteTripPlanAction.prototype.undoAction = function () {
                            Mockup.AppState.getInstance().getTripState().addTripPlan(this.TripPlan);
                        };
                        return DeleteTripPlanAction;
                    }());
                    Plans.DeleteTripPlanAction = DeleteTripPlanAction;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Actions.Trips || (Actions.Trips = {}));
        })(Actions = Mockup.Actions || (Mockup.Actions = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="Models/AppSettings.ts"/>
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
            throw new Error("Invalid units: " + units);
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
            throw new Error("Invalid units: " + units);
        }
        Mockup.convertGramsToUnits = convertGramsToUnits;
        function convertUnitsToGrams(value, units) {
            switch (units) {
                case "Metric":
                    return value;
                case "Imperial":
                    return convertOuncesToGrams(value);
            }
            throw new Error("Invalid units: " + units);
        }
        Mockup.convertUnitsToGrams = convertUnitsToGrams;
        function getUnitsLengthString(units) {
            switch (units) {
                case "Imperial":
                    return "inches";
                case "Metric":
                    return "centimeters";
            }
            throw new Error("Invalid units: " + units);
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
            throw new Error("Invalid units: " + units);
        }
        Mockup.convertCentimetersToUnits = convertCentimetersToUnits;
        function convertUnitsToCentimeters(value, units) {
            switch (units) {
                case "Metric":
                    return value;
                case "Imperial":
                    return convertInchesToCentimeters(value);
            }
            throw new Error("Invalid units: " + units);
        }
        Mockup.convertUnitsToCentimeters = convertUnitsToCentimeters;
        function getCurrencyString(currency) {
            switch (currency) {
                case "USD":
                    return "USD";
            }
            throw new Error("Invalid currency: " + currency);
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
            throw new Error("Invalid currency: " + currency);
        }
        Mockup.convertUSDPToCurrency = convertUSDPToCurrency;
        function convertCurrencyToUSDP(value, currency) {
            switch (currency) {
                case "USD":
                    return convertUSDToUSDP(value);
            }
            throw new Error("Invalid currency: " + currency);
        }
        Mockup.convertCurrencyToUSDP = convertCurrencyToUSDP;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../Models/Gear/GearItem.ts" />
///<reference path="../Models/Gear/GearSystem.ts" />
///<reference path="../Models/Gear/GearCollection.ts" />
///<reference path="../Models/Meals/Meal.ts" />
///<reference path="../Models/Trips/TripItinerary.ts" />
///<reference path="../Models/Trips/TripPlan.ts" />
///<reference path="../Models/Personal/UserInformation.ts" />
///<reference path="../Models/AppSettings.ts" />
///<reference path="../Services/Gear/GearCollectionService.ts"/>
///<reference path="../Services/Gear/GearItemService.ts"/>
///<reference path="../Services/Gear/GearSystemService.ts"/>
///<reference path="../Services/Meals/MealService.ts"/>
///<reference path="../Services/Trips/TripItineraryService.ts"/>
///<reference path="../Services/Trips/TripPlanService.ts"/>
///<reference path="../Services/Personal/UserInformationService.ts"/>
///<reference path="../Services/AppSettingsService.ts"/>
///<reference path="../UnitConversion.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            "use strict";
            var AppCtrl = (function () {
                function AppCtrl($scope, $q, $location, $anchorScroll, $mdSidenav, $mdDialog, $mdToast, appSettingsService, userInformationService, gearItemService, gearSystemService, gearCollectionService, mealService, tripItineraryService, tripPlanService) {
                    $scope.appStateLoading = true;
                    Mockup.AppState.getInstance().loadFromDevice($q, appSettingsService, userInformationService, gearItemService, gearSystemService, gearCollectionService, mealService, tripItineraryService, tripPlanService).then(function () {
                        $scope.appStateLoading = false;
                    });
                    // user information
                    $scope.getUserInfo = function () {
                        return Mockup.AppState.getInstance().getUserInformation();
                    };
                    // gear items
                    $scope.getGearItems = function () {
                        return Mockup.AppState.getInstance().getGearState().getGearItems();
                    };
                    $scope.getGearItemById = function (gearItemId) {
                        return Mockup.AppState.getInstance().getGearState().getGearItemById(gearItemId);
                    };
                    // gear systems
                    $scope.getGearSystems = function () {
                        return Mockup.AppState.getInstance().getGearState().getGearSystems();
                    };
                    $scope.getGearSystemById = function (gearSystemId) {
                        return Mockup.AppState.getInstance().getGearState().getGearSystemById(gearSystemId);
                    };
                    // gear collections
                    $scope.getGearCollections = function () {
                        return Mockup.AppState.getInstance().getGearState().getGearCollections();
                    };
                    $scope.getGearCollectionById = function (gearCollectionId) {
                        return Mockup.AppState.getInstance().getGearState().getGearCollectionById(gearCollectionId);
                    };
                    // meals
                    $scope.getMeals = function () {
                        return Mockup.AppState.getInstance().getMealState().getMeals();
                    };
                    $scope.getMealById = function (mealId) {
                        return Mockup.AppState.getInstance().getMealState().getMealById(mealId);
                    };
                    // trip itineraries
                    $scope.getTripItineraries = function () {
                        return Mockup.AppState.getInstance().getTripState().getTripItineraries();
                    };
                    $scope.getTripItineraryById = function (tripItineraryId) {
                        return Mockup.AppState.getInstance().getTripState().getTripItineraryById(tripItineraryId);
                    };
                    // trip plans
                    $scope.getTripPlans = function () {
                        return Mockup.AppState.getInstance().getTripState().getTripPlans();
                    };
                    $scope.getTripPlanById = function (tripPlanId) {
                        return Mockup.AppState.getInstance().getTripState().getTripPlanById(tripPlanId);
                    };
                    // unit utilities
                    $scope.getUnitsWeightString = function () {
                        return Mockup.getUnitsWeightString(Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    $scope.getUnitsLengthString = function () {
                        return Mockup.getUnitsLengthString(Mockup.AppState.getInstance().getAppSettings().units());
                    };
                    $scope.getCurrencyString = function () {
                        return Mockup.getCurrencyString(Mockup.AppState.getInstance().getAppSettings().currency());
                    };
                    $scope.getDaysBetween = function (startDate, endDate) {
                        var oneDayInMs = 86400000;
                        var startDateInMs = startDate.getTime();
                        var endDateInMs = endDate.getTime();
                        var daysBetweenInMs = endDateInMs - startDateInMs;
                        return Math.round(daysBetweenInMs / oneDayInMs);
                    };
                    // view utilities
                    $scope.scrollToTop = function () {
                        $location.hash("top");
                        $anchorScroll();
                    };
                    $scope.isActive = function (viewLocation) {
                        // set the nav item as active when we're looking at its location
                        return $location.path() === viewLocation;
                    };
                    $scope.toggleSidenav = function () {
                        $mdSidenav("left").toggle();
                    };
                }
                return AppCtrl;
            }());
            Controllers.AppCtrl = AppCtrl;
            AppCtrl.$inject = ["$scope", "$q", "$location", "$anchorScroll",
                "$mdSidenav", "$mdDialog", "$mdToast",
                "AppSettingsService", "UserInformationService",
                "GearItemService", "GearSystemService", "GearCollectionService",
                "MealService", "TripItineraryService", "TripPlanService"];
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var AddGearCollectionCtrl = (function () {
                        function AddGearCollectionCtrl($scope, $location, $mdDialog, $mdToast) {
                            $scope.orderGearItemsBy = "getName()";
                            $scope.orderGearSystemsBy = "getName()";
                            $scope.gearCollection = new Mockup.Models.Gear.GearCollection();
                            $scope.showAddGearItemDlg = function (event) {
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
                            $scope.showAddGearSystemDlg = function (event) {
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
                            $scope.addGearCollection = function () {
                                Mockup.AppState.getInstance().getGearState().addGearCollection($scope.gearCollection);
                                var addToast = $mdToast.simple()
                                    .textContent("Added gear collection: " + $scope.gearCollection.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/collections");
                                $mdToast.show(addToast);
                            };
                            $scope.resetGearCollection = function () {
                                $scope.gearCollection = new Mockup.Models.Gear.GearCollection();
                            };
                        }
                        return AddGearCollectionCtrl;
                    }());
                    Collections.AddGearCollectionCtrl = AddGearCollectionCtrl;
                    AddGearCollectionCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var GearCollectionCtrl = (function () {
                        function GearCollectionCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            $scope.orderGearSystemsBy = "getName()";
                            $scope.orderGearItemsBy = "getName()";
                            var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById($routeParams.gearCollectionId);
                            if (null == gearCollection) {
                                alert("The gear collection does not exist!");
                                $location.path("/gear/collections");
                                return;
                            }
                            $scope.gearCollection = angular.copy(gearCollection);
                            $scope.showAddGearSystemDlg = function (event) {
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
                            $scope.showAddGearItemDlg = function (event) {
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
                            $scope.saveGearCollection = function () {
                                var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById($scope.gearCollection.Id);
                                if (null == gearCollection) {
                                    alert("The gear collection no longer exists!");
                                    $location.path("/gear/collections");
                                    return;
                                }
                                gearCollection.update($scope.gearCollection);
                                var updateToast = $mdToast.simple()
                                    .textContent("Updated gear collection: " + $scope.gearCollection.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/collections");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetGearCollection = function () {
                                var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById($scope.gearCollection.Id);
                                if (null == gearCollection) {
                                    alert("The gear collection no longer exists!");
                                    $location.path("/gear/collections");
                                    return;
                                }
                                $scope.gearCollection = angular.copy(gearCollection);
                            };
                            $scope.deleteGearCollection = function (event) {
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
                                    .textContent("Deleted gear collection: " + $scope.gearCollection.name())
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent("Restored gear collection: " + $scope.gearCollection.name())
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        var action = new Mockup.Actions.Gear.Collections.DeleteGearCollectionAction();
                                        action.GearCollection = $scope.gearCollection;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/gear/collections");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/gear/collections/" + $scope.gearCollection.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return GearCollectionCtrl;
                    }());
                    Collections.GearCollectionCtrl = GearCollectionCtrl;
                    GearCollectionCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var GearCollectionsCtrl = (function () {
                        function GearCollectionsCtrl($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterGearCollection = function (gearCollection) {
                                if ($scope.filterName) {
                                    return gearCollection.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsGearCollectionDlg = function (event) {
                                $mdDialog.show({
                                    controller: Collections.WhatIsGearCollectionDlgCtrl,
                                    templateUrl: "content/partials/gear/collections/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                        return GearCollectionsCtrl;
                    }());
                    Collections.GearCollectionsCtrl = GearCollectionsCtrl;
                    GearCollectionsCtrl.$inject = ["$scope", "$mdDialog"];
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var AddGearItemCtrl = (function () {
                        function AddGearItemCtrl($scope, $location, $mdToast) {
                            $scope.gearItem = new Mockup.Models.Gear.GearItem();
                            $scope.addGearItem = function () {
                                Mockup.AppState.getInstance().getGearState().addGearItem($scope.gearItem);
                                var addToast = $mdToast.simple()
                                    .textContent("Added gear item: " + $scope.gearItem.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/items");
                                $mdToast.show(addToast);
                            };
                            $scope.resetGearItem = function () {
                                $scope.gearItem = new Mockup.Models.Gear.GearItem();
                            };
                        }
                        return AddGearItemCtrl;
                    }());
                    Items.AddGearItemCtrl = AddGearItemCtrl;
                    AddGearItemCtrl.$inject = ["$scope", "$location", "$mdToast"];
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var GearItemCtrl = (function () {
                        function GearItemCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById($routeParams.gearItemId);
                            if (null == gearItem) {
                                alert("The gear item does not exist!");
                                $location.path("/gear/items");
                                return;
                            }
                            $scope.gearItem = angular.copy(gearItem);
                            $scope.saveGearItem = function () {
                                var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById($scope.gearItem.Id);
                                if (null == gearItem) {
                                    alert("The gear item no longer exists!");
                                    $location.path("/gear/items");
                                    return;
                                }
                                gearItem.update($scope.gearItem);
                                var updateToast = $mdToast.simple()
                                    .textContent("Updated gear item: " + $scope.gearItem.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/items");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetGearItem = function () {
                                var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById($scope.gearItem.Id);
                                if (null == gearItem) {
                                    alert("The gear item no longer exists!");
                                    $location.path("/gear/items");
                                    return;
                                }
                                $scope.gearItem = angular.copy(gearItem);
                            };
                            $scope.deleteGearItem = function (event) {
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
                                    .textContent("Deleted gear item: " + $scope.gearItem.name())
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent("Restored gear item: " + $scope.gearItem.name())
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        var action = new Mockup.Actions.Gear.Items.DeleteGearItemAction();
                                        action.GearItem = $scope.gearItem;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/gear/items");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/gear/items/" + $scope.gearItem.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return GearItemCtrl;
                    }());
                    Items.GearItemCtrl = GearItemCtrl;
                    GearItemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var GearItemsCtrl = (function () {
                        function GearItemsCtrl($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterGearItem = function (gearItem) {
                                if ($scope.filterName) {
                                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsGearItemDlg = function (event) {
                                $mdDialog.show({
                                    controller: Items.WhatIsGearItemDlgCtrl,
                                    templateUrl: "content/partials/gear/items/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                        return GearItemsCtrl;
                    }());
                    Items.GearItemsCtrl = GearItemsCtrl;
                    GearItemsCtrl.$inject = ["$scope", "$mdDialog"];
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var AddGearSystemCtrl = (function () {
                        function AddGearSystemCtrl($scope, $location, $mdDialog, $mdToast) {
                            $scope.orderGearItemsBy = "getName()";
                            $scope.gearSystem = new Mockup.Models.Gear.GearSystem();
                            $scope.showAddGearItemDlg = function (event) {
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
                            $scope.addGearSystem = function () {
                                Mockup.AppState.getInstance().getGearState().addGearSystem($scope.gearSystem);
                                var addToast = $mdToast.simple()
                                    .textContent("Added gear system: " + $scope.gearSystem.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/systems");
                                $mdToast.show(addToast);
                            };
                            $scope.resetGearSystem = function () {
                                $scope.gearSystem = new Mockup.Models.Gear.GearSystem();
                            };
                        }
                        return AddGearSystemCtrl;
                    }());
                    Systems.AddGearSystemCtrl = AddGearSystemCtrl;
                    AddGearSystemCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var GearSystemCtrl = (function () {
                        function GearSystemCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            $scope.orderGearItemsBy = "getName()";
                            var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById($routeParams.gearSystemId);
                            if (null == gearSystem) {
                                alert("The gear system does not exist!");
                                $location.path("/gear/systems");
                                return;
                            }
                            $scope.gearSystem = angular.copy(gearSystem);
                            $scope.showAddGearItemDlg = function (event) {
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
                            $scope.saveGearSystem = function () {
                                var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById($scope.gearSystem.Id);
                                if (null == gearSystem) {
                                    alert("The gear system no longer exists!");
                                    $location.path("/gear/systems");
                                    return;
                                }
                                gearSystem.update($scope.gearSystem);
                                var updateToast = $mdToast.simple()
                                    .textContent("Updated gear system: " + $scope.gearSystem.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/systems");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetGearSystem = function () {
                                var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById($scope.gearSystem.Id);
                                if (null == gearSystem) {
                                    alert("The gear system no longer exists!");
                                    $location.path("/gear/systems");
                                    return;
                                }
                                $scope.gearSystem = angular.copy(gearSystem);
                            };
                            $scope.deleteGearSystem = function (event) {
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
                                    .textContent("Deleted gear system: " + $scope.gearSystem.name())
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent("Restored gear system: " + $scope.gearSystem.name())
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        var action = new Mockup.Actions.Gear.Systems.DeleteGearSystemAction();
                                        action.GearSystem = $scope.gearSystem;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/gear/systems");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/gear/systems/" + $scope.gearSystem.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return GearSystemCtrl;
                    }());
                    Systems.GearSystemCtrl = GearSystemCtrl;
                    GearSystemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var GearSystemsCtrl = (function () {
                        function GearSystemsCtrl($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterGearSystem = function (gearSystem) {
                                if ($scope.filterName) {
                                    return gearSystem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsGearSystemDlg = function (event) {
                                $mdDialog.show({
                                    controller: Systems.WhatIsGearSystemDlgCtrl,
                                    templateUrl: "content/partials/gear/systems/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                        return GearSystemsCtrl;
                    }());
                    Systems.GearSystemsCtrl = GearSystemsCtrl;
                    GearSystemsCtrl.$inject = ["$scope", "$mdDialog"];
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Meals;
            (function (Meals) {
                "use strict";
                var MealsCtrl = (function () {
                    function MealsCtrl($scope, $mdDialog) {
                        $scope.filterName = "";
                        $scope.orderBy = "name()";
                        $scope.filterMeal = function (meal) {
                            if ($scope.filterName) {
                                return meal.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                            }
                            return true;
                        };
                        $scope.showWhatIsMealDlg = function (event) {
                            $mdDialog.show({
                                controller: Meals.WhatIsMealDlgCtrl,
                                templateUrl: "content/partials/meals/what.html",
                                parent: angular.element(document.body),
                                targetEvent: event
                            });
                        };
                    }
                    return MealsCtrl;
                }());
                Meals.MealsCtrl = MealsCtrl;
                MealsCtrl.$inject = ["$scope", "$mdDialog"];
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../scripts/typings/angularjs/angular-route.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Meals;
            (function (Meals) {
                "use strict";
                var MealCtrl = (function () {
                    function MealCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                        var meal = Mockup.AppState.getInstance().getMealState().getMealById($routeParams.mealId);
                        if (null == meal) {
                            alert("The meal does not exist!");
                            $location.path("/meals");
                            return;
                        }
                        $scope.meal = angular.copy(meal);
                        $scope.saveMeal = function () {
                            var meal = Mockup.AppState.getInstance().getMealState().getMealById($scope.meal.Id);
                            if (null == meal) {
                                alert("The meal no longer exists!");
                                $location.path("/meals");
                                return;
                            }
                            meal.update($scope.meal);
                            var updateToast = $mdToast.simple()
                                .textContent("Updated meal: " + $scope.meal.name())
                                .action("OK")
                                .position("bottom left");
                            $location.path("/meals");
                            $mdToast.show(updateToast);
                        };
                        $scope.resetMeal = function () {
                            var meal = Mockup.AppState.getInstance().getMealState().getMealById($scope.meal.Id);
                            if (null == meal) {
                                alert("The meal no longer exists!");
                                $location.path("/meals");
                                return;
                            }
                            $scope.meal = angular.copy(meal);
                        };
                        $scope.deleteMeal = function (event) {
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
                                .textContent("Deleted meal: " + $scope.meal.name())
                                .action("Undo")
                                .position("bottom left");
                            var undoDeleteToast = $mdToast.simple()
                                .textContent("Restored meal: " + $scope.meal.name())
                                .action("OK")
                                .position("bottom left");
                            $mdDialog.show(confirm).then(function () {
                                $mdDialog.show(receipt).then(function () {
                                    var action = new Mockup.Actions.Meals.DeleteMealAction();
                                    action.Meal = $scope.meal;
                                    Mockup.AppState.getInstance().executeAction(action);
                                    $location.path("/meals");
                                    $mdToast.show(deleteToast).then(function (response) {
                                        if ("ok" == response) {
                                            Mockup.AppState.getInstance().undoAction();
                                            $mdToast.show(undoDeleteToast);
                                            $location.path("/meals/" + $scope.meal.Id);
                                        }
                                    });
                                });
                            });
                        };
                    }
                    return MealCtrl;
                }());
                Meals.MealCtrl = MealCtrl;
                MealCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Meals;
            (function (Meals) {
                "use strict";
                var AddMealCtrl = (function () {
                    function AddMealCtrl($scope, $location, $mdToast) {
                        $scope.meal = new Mockup.Models.Meals.Meal();
                        $scope.addMeal = function () {
                            Mockup.AppState.getInstance().getMealState().addMeal($scope.meal);
                            var addToast = $mdToast.simple()
                                .textContent("Added meal: " + $scope.meal.name())
                                .action("OK")
                                .position("bottom left");
                            $location.path("/meals");
                            $mdToast.show(addToast);
                        };
                        $scope.resetMeal = function () {
                            $scope.meal = new Mockup.Models.Meals.Meal();
                        };
                    }
                    return AddMealCtrl;
                }());
                Meals.AddMealCtrl = AddMealCtrl;
                AddMealCtrl.$inject = ["$scope", "$location", "$mdToast"];
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var TripItinerariesCtrl = (function () {
                        function TripItinerariesCtrl($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterTripItinerary = function (tripItinerary) {
                                if ($scope.filterName) {
                                    return tripItinerary.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsTripItineraryDlg = function (event) {
                                $mdDialog.show({
                                    controller: Itineraries.WhatIsTripItineraryDlgCtrl,
                                    templateUrl: "content/partials/trips/itineraries/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                        return TripItinerariesCtrl;
                    }());
                    Itineraries.TripItinerariesCtrl = TripItinerariesCtrl;
                    TripItinerariesCtrl.$inject = ["$scope", "$mdDialog"];
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var TripItineraryCtrl = (function () {
                        function TripItineraryCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            var tripItinerary = Mockup.AppState.getInstance().getTripState().getTripItineraryById($routeParams.tripItineraryId);
                            if (null == tripItinerary) {
                                alert("The trip itinerary does not exist!");
                                $location.path("/trips/itineraries");
                                return;
                            }
                            $scope.tripItinerary = angular.copy(tripItinerary);
                            $scope.saveTripItinerary = function () {
                                var tripItinerary = Mockup.AppState.getInstance().getTripState().getTripItineraryById($scope.tripItinerary.Id);
                                if (null == tripItinerary) {
                                    alert("The trip itinerary no longer exists!");
                                    $location.path("/trips/itineraries");
                                    return;
                                }
                                tripItinerary.update($scope.tripItinerary);
                                var updateToast = $mdToast.simple()
                                    .textContent("Updated gear collection: " + $scope.tripItinerary.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/itineraries");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetTripItinerary = function () {
                                var tripItinerary = Mockup.AppState.getInstance().getTripState().getTripItineraryById($scope.tripItinerary.Id);
                                if (null == tripItinerary) {
                                    alert("The trip itinerary no longer exists!");
                                    $location.path("/trips/itineraries");
                                    return;
                                }
                                $scope.tripItinerary = angular.copy(tripItinerary);
                            };
                            $scope.deleteTripItinerary = function (event) {
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
                                    .textContent("Deleted trip itinerary: " + $scope.tripItinerary.name())
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent("Restored trip itinerary: " + $scope.tripItinerary.name())
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        var action = new Mockup.Actions.Trips.Itineraries.DeleteTripItineraryAction();
                                        action.TripItinerary = $scope.tripItinerary;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/trips/itineraries");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/trips/itineraries/" + $scope.tripItinerary.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return TripItineraryCtrl;
                    }());
                    Itineraries.TripItineraryCtrl = TripItineraryCtrl;
                    TripItineraryCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var AddTripItineraryCtrl = (function () {
                        function AddTripItineraryCtrl($scope, $location, $mdToast) {
                            $scope.tripItinerary = new Mockup.Models.Trips.TripItinerary();
                            $scope.addTripItinerary = function () {
                                Mockup.AppState.getInstance().getTripState().addTripItinerary($scope.tripItinerary);
                                var addToast = $mdToast.simple()
                                    .textContent("Added trip itinerary: " + $scope.tripItinerary.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/itineraries");
                                $mdToast.show(addToast);
                            };
                            $scope.resetTripItinerary = function () {
                                $scope.tripItinerary = new Mockup.Models.Trips.TripItinerary();
                            };
                        }
                        return AddTripItineraryCtrl;
                    }());
                    Itineraries.AddTripItineraryCtrl = AddTripItineraryCtrl;
                    AddTripItineraryCtrl.$inject = ["$scope", "$location", "$mdToast"];
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var TripPlansCtrl = (function () {
                        function TripPlansCtrl($scope, $mdDialog) {
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.filterTripPlan = function (tripPlan) {
                                if ($scope.filterName) {
                                    return tripPlan.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.showWhatIsTripPlan = function (event) {
                                $mdDialog.show({
                                    controller: Plans.WhatIsTripPlanDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/what.html",
                                    parent: angular.element(document.body),
                                    targetEvent: event
                                });
                            };
                        }
                        return TripPlansCtrl;
                    }());
                    Plans.TripPlansCtrl = TripPlansCtrl;
                    TripPlansCtrl.$inject = ["$scope", "$mdDialog"];
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../../scripts/typings/angularjs/angular-route.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var TripPlanCtrl = (function () {
                        function TripPlanCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                            $scope.orderGearCollectionsBy = "getName()";
                            $scope.orderGearSystemsBy = "getName()";
                            $scope.orderGearItemsBy = "getName()";
                            $scope.orderMealsBy = "getName()";
                            var tripPlan = Mockup.AppState.getInstance().getTripState().getTripPlanById($routeParams.tripPlanId);
                            if (null == tripPlan) {
                                alert("The trip plan does not exist!");
                                $location.path("/trips/plans");
                                return;
                            }
                            $scope.tripPlan = angular.copy(tripPlan);
                            $scope.showAddGearCollectionDlg = function (event) {
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
                            $scope.showAddGearSystemDlg = function (event) {
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
                            $scope.showAddGearItemDlg = function (event) {
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
                            $scope.showAddMealDlg = function (event) {
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
                            $scope.saveTripPlan = function () {
                                var tripPlan = Mockup.AppState.getInstance().getTripState().getTripPlanById($scope.tripPlan.Id);
                                if (null == tripPlan) {
                                    alert("The trip plan no longer exists!");
                                    $location.path("/trips/plans");
                                    return;
                                }
                                tripPlan.update($scope.tripPlan);
                                var updateToast = $mdToast.simple()
                                    .textContent("Updated trip plan: " + $scope.tripPlan.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/plans");
                                $mdToast.show(updateToast);
                            };
                            $scope.resetTripPlan = function () {
                                var tripPlan = Mockup.AppState.getInstance().getTripState().getTripPlanById($scope.tripPlan.Id);
                                if (null == tripPlan) {
                                    alert("The trip plan no longer exists!");
                                    $location.path("/trips/plans");
                                    return;
                                }
                                $scope.tripPlan = angular.copy(tripPlan);
                            };
                            $scope.deleteTripPlan = function (event) {
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
                                    .textContent("Deleted trip plan: " + $scope.tripPlan.name())
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .textContent("Restored trip plan: " + $scope.tripPlan.name())
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        var action = new Mockup.Actions.Trips.Plans.DeleteTripPlanAction();
                                        action.TripPlan = $scope.tripPlan;
                                        Mockup.AppState.getInstance().executeAction(action);
                                        $location.path("/trips/plans");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().undoAction();
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/trips/plans/" + $scope.tripPlan.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return TripPlanCtrl;
                    }());
                    Plans.TripPlanCtrl = TripPlanCtrl;
                    TripPlanCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../AppCtrl.ts" />
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
                    var AddTripPlanCtrl = (function () {
                        function AddTripPlanCtrl($scope, $location, $mdDialog, $mdToast) {
                            $scope.orderGearCollectionsBy = "getName()";
                            $scope.orderGearSystemsBy = "getName()";
                            $scope.orderGearItemsBy = "getName()";
                            $scope.orderMealsBy = "getName()";
                            $scope.tripPlan = new Mockup.Models.Trips.TripPlan();
                            $scope.showAddGearCollectionDlg = function (event) {
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
                            $scope.showAddGearSystemDlg = function (event) {
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
                            $scope.showAddGearItemDlg = function (event) {
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
                            $scope.showAddMealDlg = function (event) {
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
                            $scope.addTripPlan = function () {
                                Mockup.AppState.getInstance().getTripState().addTripPlan($scope.tripPlan);
                                var addToast = $mdToast.simple()
                                    .textContent("Added trip plan: " + $scope.tripPlan.name())
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/plans");
                                $mdToast.show(addToast);
                            };
                            $scope.resetTripPlan = function () {
                                $scope.tripPlan = new Mockup.Models.Trips.TripPlan();
                            };
                        }
                        return AddTripPlanCtrl;
                    }());
                    Plans.AddTripPlanCtrl = AddTripPlanCtrl;
                    AddTripPlanCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../Models/AppSettings.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Personal;
            (function (Personal) {
                "use strict";
                var UserInformationCtrl = (function () {
                    function UserInformationCtrl($scope, $location, $mdDialog, $mdToast) {
                        $scope.userInfo = angular.copy(Mockup.AppState.getInstance().getUserInformation());
                        $scope.showWhatIsPersonalDlg = function (event) {
                            $mdDialog.show({
                                controller: Personal.WhatIsPersonalDlgCtrl,
                                templateUrl: "content/partials/personal/what.html",
                                parent: angular.element(document.body),
                                targetEvent: event
                            });
                        };
                        $scope.saveUserInformation = function () {
                            Mockup.AppState.getInstance().getUserInformation().update($scope.userInfo);
                            var updateToast = $mdToast.simple()
                                .textContent("Updated personal information!")
                                .action("OK")
                                .position("bottom left");
                            $location.path("/personal");
                            $mdToast.show(updateToast);
                        };
                        $scope.resetUserInformation = function () {
                            $scope.userInfo = angular.copy(Mockup.AppState.getInstance().getUserInformation());
                        };
                    }
                    return UserInformationCtrl;
                }());
                Personal.UserInformationCtrl = UserInformationCtrl;
                UserInformationCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
            })(Personal = Controllers.Personal || (Controllers.Personal = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../Models/AppSettings.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            "use strict";
            var AppSettingsCtrl = (function () {
                function AppSettingsCtrl($scope, $location, $mdDialog, $mdToast) {
                    $scope.appSettings = angular.copy(Mockup.AppState.getInstance().getAppSettings());
                    $scope.showAdvancedSettings = false;
                    $scope.toggleAdvancedSettings = function () {
                        $scope.showAdvancedSettings = !$scope.showAdvancedSettings;
                    };
                    $scope.saveAppSettings = function () {
                        Mockup.AppState.getInstance().getAppSettings().update($scope.appSettings);
                        var updateToast = $mdToast.simple()
                            .textContent("Updated application settings!")
                            .action("OK")
                            .position("bottom left");
                        $location.path("/settings");
                        $mdToast.show(updateToast);
                    };
                    $scope.resetAppSettings = function () {
                        $scope.appSettings = angular.copy(Mockup.AppState.getInstance().getAppSettings());
                    };
                    $scope.defaultAppSettings = function () {
                        $scope.appSettings.resetToDefaults();
                    };
                    $scope.deleteAllGearItems = function (event) {
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
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getGearState().deleteAllGearItems();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllGearSystems = function (event) {
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
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getGearState().deleteAllGearSystems();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllGearCollections = function (event) {
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
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getGearState().deleteAllGearCollections();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllMeals = function (event) {
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
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getMealState().deleteAllMeals();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllTripItineraries = function (event) {
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
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getTripState().deleteAllTripItineraries();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllTripPlans = function (event) {
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
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getTripState().deleteAllTripPlans();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                    $scope.deleteAllData = function (event) {
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
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().deleteAllData();
                                $mdToast.show(deleteToast);
                            });
                        });
                    };
                }
                return AppSettingsCtrl;
            }());
            Controllers.AppSettingsCtrl = AppSettingsCtrl;
            AppSettingsCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../scripts/typings/angularjs/angular-route.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var RootScopeConfig = (function () {
            function RootScopeConfig($rootScope) {
                $rootScope.$on("$routeChangeSuccess", function (event, currentRoute, previousRoute) {
                    // change the app menu title when the route changes
                    $rootScope.title = currentRoute.title;
                });
            }
            return RootScopeConfig;
        }());
        Mockup.RootScopeConfig = RootScopeConfig;
        ;
        RootScopeConfig.$inject = ["$rootScope"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular-route.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var CustomRouteConfig = (function () {
            function CustomRouteConfig() {
            }
            return CustomRouteConfig;
        }());
        var RouteConfig = (function () {
            function RouteConfig($routeProvider) {
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
            RouteConfig.prototype.addRoute = function ($routeProvider, url, routeConfig) {
                $routeProvider.when(url, routeConfig);
            };
            return RouteConfig;
        }());
        Mockup.RouteConfig = RouteConfig;
        ;
        RouteConfig.$inject = ["$routeProvider"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../Scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var ThemeConfig = (function () {
            function ThemeConfig($mdThemingProvider) {
                var primaryPalette = $mdThemingProvider.extendPalette("green", {
                    "500": "668000",
                    "A100": "501616",
                    "contrastDefaultColor": "light"
                });
                var backgroundPalette = $mdThemingProvider.extendPalette("brown", {
                    "500": "decd87"
                });
                var accentPalette = $mdThemingProvider.extendPalette("blue-grey", {});
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
            return ThemeConfig;
        }());
        Mockup.ThemeConfig = ThemeConfig;
        ;
        ThemeConfig.$inject = ["$mdThemingProvider"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular.d.ts" />
///<reference path="Controllers/Gear/Collections/AddGearCollectionCtrl.ts" />
///<reference path="Controllers/Gear/Collections/GearCollectionCtrl.ts" />
///<reference path="Controllers/Gear/Collections/GearCollectionsCtrl.ts" />
///<reference path="Controllers/Gear/Items/AddGearItemCtrl.ts" />
///<reference path="Controllers/Gear/Items/GearItemCtrl.ts" />
///<reference path="Controllers/Gear/Items/GearItemsCtrl.ts" />
///<reference path="Controllers/Gear/Systems/AddGearSystemCtrl.ts" />
///<reference path="Controllers/Gear/Systems/GearSystemCtrl.ts" />
///<reference path="Controllers/Gear/Systems/GearSystemsCtrl.ts" />
///<reference path="Controllers/Meals/MealsCtrl.ts" />
///<reference path="Controllers/Meals/MealCtrl.ts" />
///<reference path="Controllers/Meals/AddMealCtrl.ts" />
///<reference path="Controllers/Trips/Itineraries/TripItinerariesCtrl.ts" />
///<reference path="Controllers/Trips/Itineraries/TripItineraryCtrl.ts" />
///<reference path="Controllers/Trips/Itineraries/AddTripItineraryCtrl.ts" />
///<reference path="Controllers/Trips/Plans/TripPlansCtrl.ts" />
///<reference path="Controllers/Trips/Plans/TripPlanCtrl.ts" />
///<reference path="Controllers/Trips/Plans/AddTripPlanCtrl.ts" />
///<reference path="Controllers/Personal/UserInformationCtrl.ts" />
///<reference path="Controllers/AppCtrl.ts" />
///<reference path="Controllers/AppSettingsCtrl.ts" />
///<reference path="RootScopeConfig.ts" />
///<reference path="RouteConfig.ts" />
///<reference path="ThemeConfig.ts" />
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
            //"ngTouch",        // this breaks md-button ng-click operation
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
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../AppState.ts" />
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
                    var AddGearItemDlgCtrl = (function () {
                        function AddGearItemDlgCtrl($scope, $mdDialog, gearCollection) {
                            $scope.gearCollection = gearCollection;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.filterGearItem = function (gearItem) {
                                if ($scope.filterName) {
                                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearItems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.isGearItemSelected = function (gearItem) {
                                return $scope.gearCollection.containsGearItemById(gearItem.Id);
                            };
                            $scope.toggleGearItemSelected = function (gearItem) {
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
                        return AddGearItemDlgCtrl;
                    }());
                    Collections.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../AppState.ts" />
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
                    var AddGearSystemDlgCtrl = (function () {
                        function AddGearSystemDlgCtrl($scope, $mdDialog, gearCollection) {
                            $scope.gearCollection = gearCollection;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.filterGearSystem = function (gearSystem) {
                                if ($scope.filterName) {
                                    return gearSystem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearSystems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearSystems();
                            };
                            $scope.isGearSystemSelected = function (gearSystem) {
                                return $scope.gearCollection.containsGearSystemById(gearSystem.Id);
                            };
                            $scope.toggleGearSystemSelected = function (gearSystem) {
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
                        return AddGearSystemDlgCtrl;
                    }());
                    Collections.AddGearSystemDlgCtrl = AddGearSystemDlgCtrl;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
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
                    var WhatIsGearCollectionDlgCtrl = (function () {
                        function WhatIsGearCollectionDlgCtrl($scope, $mdDialog) {
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                        }
                        return WhatIsGearCollectionDlgCtrl;
                    }());
                    Collections.WhatIsGearCollectionDlgCtrl = WhatIsGearCollectionDlgCtrl;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
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
                    var WhatIsGearItemDlgCtrl = (function () {
                        function WhatIsGearItemDlgCtrl($scope, $mdDialog) {
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                        }
                        return WhatIsGearItemDlgCtrl;
                    }());
                    Items.WhatIsGearItemDlgCtrl = WhatIsGearItemDlgCtrl;
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../AppState.ts" />
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
                    var AddGearItemDlgCtrl = (function () {
                        function AddGearItemDlgCtrl($scope, $mdDialog, gearSystem) {
                            $scope.gearSystem = gearSystem;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.filterGearItem = function (gearItem) {
                                if ($scope.filterName) {
                                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearItems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.isGearItemSelected = function (gearItem) {
                                return $scope.gearSystem.containsGearItemById(gearItem.Id);
                            };
                            $scope.toggleGearItemSelected = function (gearItem) {
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
                        return AddGearItemDlgCtrl;
                    }());
                    Systems.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
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
                    var WhatIsGearSystemDlgCtrl = (function () {
                        function WhatIsGearSystemDlgCtrl($scope, $mdDialog) {
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                        }
                        return WhatIsGearSystemDlgCtrl;
                    }());
                    Systems.WhatIsGearSystemDlgCtrl = WhatIsGearSystemDlgCtrl;
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Meals;
            (function (Meals) {
                "use strict";
                var WhatIsMealDlgCtrl = (function () {
                    function WhatIsMealDlgCtrl($scope, $mdDialog) {
                        $scope.close = function () {
                            $mdDialog.hide();
                        };
                    }
                    return WhatIsMealDlgCtrl;
                }());
                Meals.WhatIsMealDlgCtrl = WhatIsMealDlgCtrl;
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/index.d.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Personal;
            (function (Personal) {
                "use strict";
                var WhatIsPersonalDlgCtrl = (function () {
                    function WhatIsPersonalDlgCtrl($scope, $mdDialog) {
                        $scope.close = function () {
                            $mdDialog.hide();
                        };
                    }
                    return WhatIsPersonalDlgCtrl;
                }());
                Personal.WhatIsPersonalDlgCtrl = WhatIsPersonalDlgCtrl;
            })(Personal = Controllers.Personal || (Controllers.Personal = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
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
                    var WhatIsTripItineraryDlgCtrl = (function () {
                        function WhatIsTripItineraryDlgCtrl($scope, $mdDialog) {
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                        }
                        return WhatIsTripItineraryDlgCtrl;
                    }());
                    Itineraries.WhatIsTripItineraryDlgCtrl = WhatIsTripItineraryDlgCtrl;
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../AppState.ts" />
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
                    var AddGearCollectionDlgCtrl = (function () {
                        function AddGearCollectionDlgCtrl($scope, $mdDialog, tripPlan) {
                            $scope.tripPlan = tripPlan;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.filterGearCollection = function (gearCollection) {
                                if ($scope.filterName) {
                                    return gearCollection.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearCollections = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearCollections();
                            };
                            $scope.isGearCollectionSelected = function (gearCollection) {
                                return $scope.tripPlan.containsGearCollectionById(gearCollection.Id);
                            };
                            $scope.toggleGearCollectionSelected = function (gearCollection) {
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
                        return AddGearCollectionDlgCtrl;
                    }());
                    Plans.AddGearCollectionDlgCtrl = AddGearCollectionDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../AppState.ts" />
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
                    var AddGearItemDlgCtrl = (function () {
                        function AddGearItemDlgCtrl($scope, $mdDialog, tripPlan) {
                            $scope.tripPlan = tripPlan;
                            $scope.orderBy = "name()";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.filterGearItem = function (gearItem) {
                                if ($scope.filterName) {
                                    return gearItem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearItems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.isGearItemSelected = function (gearItem) {
                                return $scope.tripPlan.containsGearItemById(gearItem.Id);
                            };
                            $scope.toggleGearItemSelected = function (gearItem) {
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
                        return AddGearItemDlgCtrl;
                    }());
                    Plans.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../AppState.ts" />
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
                    var AddGearSystemDlgCtrl = (function () {
                        function AddGearSystemDlgCtrl($scope, $mdDialog, tripPlan) {
                            $scope.tripPlan = tripPlan;
                            $scope.filterName = "";
                            $scope.orderBy = "name()";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.filterGearSystem = function (gearSystem) {
                                if ($scope.filterName) {
                                    return gearSystem.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getGearSystems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearSystems();
                            };
                            $scope.isGearSystemSelected = function (gearSystem) {
                                return $scope.tripPlan.containsGearSystemById(gearSystem.Id);
                            };
                            $scope.toggleGearSystemSelected = function (gearSystem) {
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
                        return AddGearSystemDlgCtrl;
                    }());
                    Plans.AddGearSystemDlgCtrl = AddGearSystemDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
///<reference path="../../../AppState.ts" />
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
                    var AddMealDlgCtrl = (function () {
                        function AddMealDlgCtrl($scope, $mdDialog, tripPlan) {
                            $scope.tripPlan = tripPlan;
                            $scope.orderBy = "name()";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.filterMeal = function (meal) {
                                if ($scope.filterName) {
                                    return meal.name().toLowerCase().indexOf($scope.filterName.toLowerCase()) >= 0;
                                }
                                return true;
                            };
                            $scope.getMeals = function () {
                                return Mockup.AppState.getInstance().getMealState().getMeals();
                            };
                            $scope.isMealSelected = function (meal) {
                                return $scope.tripPlan.containsMealById(meal.Id);
                            };
                            $scope.toggleMealSelected = function (meal) {
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
                        return AddMealDlgCtrl;
                    }());
                    Plans.AddMealDlgCtrl = AddMealDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/index.d.ts" />
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
                    var WhatIsTripPlanDlgCtrl = (function () {
                        function WhatIsTripPlanDlgCtrl($scope, $mdDialog) {
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                        }
                        return WhatIsTripPlanDlgCtrl;
                    }());
                    Plans.WhatIsTripPlanDlgCtrl = WhatIsTripPlanDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
//# sourceMappingURL=mockup.js.map