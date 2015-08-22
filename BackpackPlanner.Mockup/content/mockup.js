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
                        this.FirstName = "";
                        this.LastName = "";
                        this.BirthDate = "";
                        this.Sex = "NotSpecified";
                        this.HeightInCm = 0;
                        this.WeightInGrams = 0;
                        this.BirthDateAsDate = new Date();
                    }
                    /* Height/Weight */
                    UserInformation.prototype.heightInUnits = function (height) {
                        return arguments.length
                            ? (this.HeightInCm = Mockup.convertUnitsToCentimeters(height, Mockup.AppState.getInstance().getAppSettings().Units))
                            : parseFloat(Mockup.convertCentimetersToUnits(this.HeightInCm, Mockup.AppState.getInstance().getAppSettings().Units).toFixed(2));
                    };
                    UserInformation.prototype.weightInUnits = function (weight) {
                        return arguments.length
                            ? (this.WeightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().Units))
                            : parseFloat(Mockup.convertGramsToUnits(this.WeightInGrams, Mockup.AppState.getInstance().getAppSettings().Units).toFixed(2));
                    };
                    /* Load/Save */
                    UserInformation.prototype.update = function (userInformation) {
                        this.FirstName = userInformation.FirstName;
                        this.LastName = userInformation.LastName;
                        this.BirthDateAsDate = userInformation.BirthDateAsDate;
                        this.BirthDate = this.BirthDateAsDate.toString();
                        this.Sex = userInformation.Sex;
                        this.HeightInCm = userInformation.HeightInCm;
                        this.WeightInGrams = userInformation.WeightInGrams;
                    };
                    UserInformation.prototype.loadFromDevice = function ($q, userInfoResource) {
                        var deferred = $q.defer();
                        this.FirstName = userInfoResource.FirstName;
                        this.LastName = userInfoResource.LastName;
                        this.BirthDate = userInfoResource.BirthDate;
                        this.BirthDateAsDate = new Date(this.BirthDate);
                        this.Sex = userInfoResource.Sex;
                        this.HeightInCm = userInfoResource.HeightInCm;
                        this.WeightInGrams = userInfoResource.WeightInGrams;
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    UserInformation.prototype.saveToDevice = function ($q) {
                        alert("UserInformation.saveToDevice");
                        return $q.defer().promise;
                    };
                    return UserInformation;
                })();
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
                    this.Units = "Metric";
                    this.Currency = "USD";
                }
                /* Load/Save */
                AppSettings.prototype.update = function (appSettings) {
                    this.Units = appSettings.Units;
                    this.Currency = appSettings.Currency;
                };
                AppSettings.prototype.loadFromDevice = function ($q, appSettingsResource) {
                    var deferred = $q.defer();
                    this.Units = appSettingsResource.Units;
                    this.Currency = appSettingsResource.Currency;
                    deferred.resolve(this);
                    return deferred.promise;
                };
                AppSettings.prototype.saveToDevice = function ($q) {
                    alert("AppSettings.saveToDevice");
                    return $q.defer().promise;
                };
                return AppSettings;
            })();
            Models.AppSettings = AppSettings;
        })(Models = Mockup.Models || (Mockup.Models = {}));
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
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Models/Gear/GearSystem.ts" />
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
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
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
                        this.Id = -1;
                        this.Name = "";
                        this.Note = "";
                        this.GearItems = [];
                    }
                    /* Gear Items */
                    GearSystem.prototype.getGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
                    GearSystem.prototype.getGearItemEntryIndexById = function (gearItemId) {
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            if (gearItemEntry.GearItemId == gearItemId) {
                                return i;
                            }
                        }
                        return -1;
                    };
                    GearSystem.prototype.containsGearItem = function (gearItem) {
                        return this.getGearItemEntryIndexById(gearItem.Id) >= 0;
                    };
                    GearSystem.prototype.addGearItem = function (gearItem) {
                        if (this.containsGearItem(gearItem)) {
                            return;
                        }
                        this.GearItems.push(new Gear.GearItemEntry(gearItem.Id));
                    };
                    GearSystem.prototype.removeGearItem = function (gearItem) {
                        var idx = this.getGearItemEntryIndexById(gearItem.Id);
                        if (idx < 0) {
                            return;
                        }
                        this.GearItems.splice(idx, 1);
                    };
                    GearSystem.prototype.removeAllGearItems = function () {
                        this.GearItems = [];
                    };
                    /* Pack List */
                    GearSystem.prototype.getPackedGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            if (gearItemEntry.IsPacked) {
                                ++count;
                            }
                        }
                        return count;
                    };
                    GearSystem.prototype.getPackList = function () {
                        var entries = [];
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            entries.push(this.GearItems[i]);
                        }
                        return entries;
                    };
                    /* Weight/Cost */
                    GearSystem.prototype.getWeightInGrams = function () {
                        var weightInGrams = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            weightInGrams += gearItemEntry.getWeightInGrams();
                        }
                        return weightInGrams;
                    };
                    GearSystem.prototype.getWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units).toFixed(2));
                    };
                    GearSystem.prototype.getCostInUSDP = function () {
                        var costInUSDP = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            costInUSDP += gearItemEntry.getCostInUSDP();
                        }
                        return costInUSDP;
                    };
                    GearSystem.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    GearSystem.prototype.getCostPerUnitInCurrency = function () {
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                        var weightInUnits = Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units);
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    GearSystem.prototype.update = function (gearSystem) {
                        this.Name = gearSystem.Name;
                        this.Note = gearSystem.Note;
                        this.GearItems = [];
                        for (var i = 0; i < gearSystem.GearItems.length; ++i) {
                            var gearItemEntry = gearSystem.GearItems[i];
                            this.GearItems.push(new Gear.GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
                        }
                    };
                    GearSystem.prototype.loadFromDevice = function ($q, gearSystemResource) {
                        var deferred = $q.defer();
                        this.Id = gearSystemResource.Id;
                        this.Name = gearSystemResource.Name;
                        this.Note = gearSystemResource.Note;
                        for (var i = 0; i < gearSystemResource.GearItems.length; ++i) {
                            var gearItemEntry = gearSystemResource.GearItems[i];
                            this.GearItems.push(new Gear.GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    GearSystem.prototype.saveToDevice = function ($q) {
                        alert("GearSystem.saveToDevice");
                        return $q.defer().promise;
                    };
                    return GearSystem;
                })();
                Gear.GearSystem = GearSystem;
                var GearSystemEntry = (function () {
                    function GearSystemEntry(gearSystemId, count, isPacked) {
                        this.GearSystemId = -1;
                        this.Count = 1;
                        this.IsPacked = false;
                        this.GearSystemId = gearSystemId;
                        if (count) {
                            this.Count = count;
                        }
                        if (isPacked) {
                            this.IsPacked = isPacked;
                        }
                    }
                    GearSystemEntry.prototype.getName = function () {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
                        if (!gearSystem) {
                            return "";
                        }
                        return gearSystem.Name;
                    };
                    GearSystemEntry.prototype.getGearItemCount = function () {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this.Count * gearSystem.getGearItemCount();
                    };
                    GearSystemEntry.prototype.getWeightInGrams = function () {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this.Count * gearSystem.getWeightInGrams();
                    };
                    GearSystemEntry.prototype.getWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units).toFixed(2));
                    };
                    GearSystemEntry.prototype.getCostInUSDP = function () {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this.Count * gearSystem.getCostInUSDP();
                    };
                    GearSystemEntry.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    return GearSystemEntry;
                })();
                Gear.GearSystemEntry = GearSystemEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
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
                        this.Id = -1;
                        this.Name = "";
                        this.Note = "";
                        this.GearSystems = [];
                        this.GearItems = [];
                    }
                    GearCollection.prototype.getTotalGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            count += gearSystemEntry.getGearItemCount();
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
                    /* Gear Systems */
                    GearCollection.prototype.getGearSystemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            count += gearSystemEntry.Count;
                        }
                        return count;
                    };
                    GearCollection.prototype.getGearSystemEntryIndexById = function (gearSystemId) {
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            if (gearSystemEntry.GearSystemId == gearSystemId) {
                                return i;
                            }
                        }
                        return -1;
                    };
                    GearCollection.prototype.containsGearSystem = function (gearSystem) {
                        return this.getGearSystemEntryIndexById(gearSystem.Id) >= 0;
                    };
                    GearCollection.prototype.addGearSystem = function (gearSystem) {
                        if (this.containsGearSystem(gearSystem)) {
                            return;
                        }
                        this.GearSystems.push(new Gear.GearSystemEntry(gearSystem.Id));
                    };
                    GearCollection.prototype.removeGearSystem = function (gearSystem) {
                        var idx = this.getGearSystemEntryIndexById(gearSystem.Id);
                        if (idx < 0) {
                            return;
                        }
                        this.GearSystems.splice(idx, 1);
                    };
                    GearCollection.prototype.removeAllGearSystems = function () {
                        this.GearSystems = [];
                    };
                    /* Gear Items */
                    GearCollection.prototype.getGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
                    GearCollection.prototype.getGearItemEntryIndexById = function (gearItemId) {
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            if (gearItemEntry.GearItemId == gearItemId) {
                                return i;
                            }
                        }
                        return -1;
                    };
                    GearCollection.prototype.containsGearItem = function (gearItem) {
                        return this.getGearItemEntryIndexById(gearItem.Id) >= 0;
                    };
                    GearCollection.prototype.addGearItem = function (gearItem) {
                        if (this.containsGearItem(gearItem)) {
                            return;
                        }
                        this.GearItems.push(new Gear.GearItemEntry(gearItem.Id));
                    };
                    GearCollection.prototype.removeGearItem = function (gearItem) {
                        var idx = this.getGearItemEntryIndexById(gearItem.Id);
                        if (idx < 0) {
                            return;
                        }
                        this.GearItems.splice(idx, 1);
                    };
                    GearCollection.prototype.removeAllGearItems = function () {
                        this.GearItems = [];
                    };
                    /* Pack List */
                    GearCollection.prototype.getPackedGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this.GearSystems[i].GearSystemId);
                            count += gearSystem.getPackedGearItemCount();
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            if (gearItemEntry.IsPacked) {
                                ++count;
                            }
                        }
                        return count;
                    };
                    GearCollection.prototype.getPackList = function () {
                        var entries = [];
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this.GearSystems[i].GearSystemId);
                            entries = entries.concat(gearSystem.getPackList());
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            entries.push(this.GearItems[i]);
                        }
                        return entries;
                    };
                    /* Weight/Cost */
                    GearCollection.prototype.getWeightInGrams = function () {
                        var weightInGrams = 0;
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            weightInGrams += gearSystemEntry.getWeightInGrams();
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            weightInGrams += gearItemEntry.getWeightInGrams();
                        }
                        return weightInGrams;
                    };
                    GearCollection.prototype.getWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units);
                    };
                    GearCollection.prototype.getCostInUSDP = function () {
                        var costInUSDP = 0;
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            costInUSDP += gearSystemEntry.getCostInUSDP();
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            costInUSDP += gearItemEntry.getCostInUSDP();
                        }
                        return costInUSDP;
                    };
                    GearCollection.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    GearCollection.prototype.getCostPerUnitInCurrency = function () {
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                        var weightInUnits = Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units);
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    GearCollection.prototype.update = function (gearCollection) {
                        this.Name = gearCollection.Name;
                        this.Note = gearCollection.Note;
                        this.GearSystems = [];
                        for (var i = 0; i < gearCollection.GearSystems.length; ++i) {
                            var gearSystemEntry = gearCollection.GearSystems[i];
                            this.GearSystems.push(new Gear.GearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count, gearSystemEntry.IsPacked));
                        }
                        this.GearItems = [];
                        for (var i = 0; i < gearCollection.GearItems.length; ++i) {
                            var gearItemEntry = gearCollection.GearItems[i];
                            this.GearItems.push(new Gear.GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
                        }
                    };
                    GearCollection.prototype.loadFromDevice = function ($q, gearCollectionResource) {
                        var deferred = $q.defer();
                        this.Id = gearCollectionResource.Id;
                        this.Name = gearCollectionResource.Name;
                        this.Note = gearCollectionResource.Note;
                        for (var i = 0; i < gearCollectionResource.GearSystems.length; ++i) {
                            var gearSystemEntry = gearCollectionResource.GearSystems[i];
                            this.GearSystems.push(new Gear.GearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count, gearSystemEntry.IsPacked));
                        }
                        for (var i = 0; i < gearCollectionResource.GearItems.length; ++i) {
                            var gearItemEntry = gearCollectionResource.GearItems[i];
                            this.GearItems.push(new Gear.GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    GearCollection.prototype.saveToDevice = function ($q) {
                        alert("GearCollection.saveToDevice");
                        return $q.defer().promise;
                    };
                    return GearCollection;
                })();
                Gear.GearCollection = GearCollection;
                var GearCollectionEntry = (function () {
                    function GearCollectionEntry(gearCollectionId, count, isPacked) {
                        this.GearCollectionId = -1;
                        this.Count = 1;
                        this.IsPacked = false;
                        this.GearCollectionId = gearCollectionId;
                        if (count) {
                            this.Count = count;
                        }
                        if (isPacked) {
                            this.IsPacked = isPacked;
                        }
                    }
                    GearCollectionEntry.prototype.getName = function () {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
                        if (!gearCollection) {
                            return "";
                        }
                        return gearCollection.Name;
                    };
                    GearCollectionEntry.prototype.getTotalGearItemCount = function () {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this.Count * gearCollection.getTotalGearItemCount();
                    };
                    GearCollectionEntry.prototype.getGearSystemCount = function () {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this.Count * gearCollection.getGearSystemCount();
                    };
                    GearCollectionEntry.prototype.getGearItemCount = function () {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this.Count * gearCollection.getGearItemCount();
                    };
                    GearCollectionEntry.prototype.getWeightInGrams = function () {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this.Count * gearCollection.getWeightInGrams();
                    };
                    GearCollectionEntry.prototype.getWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units).toFixed(2));
                    };
                    GearCollectionEntry.prototype.getCostInUSDP = function () {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this.Count * gearCollection.getCostInUSDP();
                    };
                    GearCollectionEntry.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    return GearCollectionEntry;
                })();
                Gear.GearCollectionEntry = GearCollectionEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Models/Gear/GearCollection.ts" />
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
                        this.Id = -1;
                        this.Name = "";
                        this.Url = "";
                        this.Meal = "Other";
                        this.ServingCount = 1;
                        this.WeightInGrams = 0;
                        this.CostInUSDP = 0;
                        this.Calories = 0;
                        this.ProteinInGrams = 0;
                        this.FiberInGrams = 0;
                        this.Note = "";
                    }
                    /* Weight/Cost */
                    Meal.prototype.getCaloriesPerUnit = function () {
                        return 0 == this.Calories ? 0 : this.Calories / this.weightInUnits();
                    };
                    Meal.prototype.weightInUnits = function (weight) {
                        return arguments.length
                            ? (this.WeightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().Units))
                            : parseFloat(Mockup.convertGramsToUnits(this.WeightInGrams, Mockup.AppState.getInstance().getAppSettings().Units).toFixed(2));
                    };
                    Meal.prototype.costInCurrency = function (cost) {
                        return arguments.length
                            ? (this.CostInUSDP = Mockup.convertCurrencyToUSDP(cost, Mockup.AppState.getInstance().getAppSettings().Currency))
                            : Mockup.convertUSDPToCurrency(this.CostInUSDP, Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    Meal.prototype.getCostPerUnitInCurrency = function () {
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.CostInUSDP, Mockup.AppState.getInstance().getAppSettings().Currency);
                        var weightInUnits = Mockup.convertGramsToUnits(this.WeightInGrams, Mockup.AppState.getInstance().getAppSettings().Units);
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    Meal.prototype.update = function (meal) {
                        this.Name = meal.Name;
                        this.Url = meal.Url;
                        this.Meal = meal.Meal;
                        this.ServingCount = meal.ServingCount;
                        this.WeightInGrams = meal.WeightInGrams;
                        this.CostInUSDP = meal.CostInUSDP;
                        this.Calories = meal.Calories;
                        this.ProteinInGrams = meal.ProteinInGrams;
                        this.FiberInGrams = meal.FiberInGrams;
                        this.Note = meal.Note;
                    };
                    Meal.prototype.loadFromDevice = function ($q, mealResource) {
                        var deferred = $q.defer();
                        this.Id = mealResource.Id;
                        this.Name = mealResource.Name;
                        this.Url = mealResource.Url;
                        this.Meal = mealResource.Meal;
                        this.ServingCount = mealResource.ServingCount;
                        this.WeightInGrams = mealResource.WeightInGrams;
                        this.CostInUSDP = mealResource.CostInUSDP;
                        this.Calories = mealResource.Calories;
                        this.ProteinInGrams = mealResource.ProteinInGrams;
                        this.FiberInGrams = mealResource.FiberInGrams;
                        this.Note = mealResource.Note;
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    Meal.prototype.saveToDevice = function ($q) {
                        alert("Meal.saveToDevice");
                        return $q.defer().promise;
                    };
                    return Meal;
                })();
                Meals.Meal = Meal;
                var MealEntry = (function () {
                    function MealEntry(mealId, count, isPacked) {
                        this.MealId = -1;
                        this.Count = 1;
                        this.IsPacked = false;
                        this.MealId = mealId;
                        if (count) {
                            this.Count = count;
                        }
                        if (isPacked) {
                            this.IsPacked = isPacked;
                        }
                    }
                    MealEntry.prototype.getName = function () {
                        var meal = Mockup.AppState.getInstance().getMealState().getMealById(this.MealId);
                        if (!meal) {
                            return "";
                        }
                        return meal.Name;
                    };
                    MealEntry.prototype.getCalories = function () {
                        var meal = Mockup.AppState.getInstance().getMealState().getMealById(this.MealId);
                        if (!meal) {
                            return 0;
                        }
                        return this.Count * meal.Calories;
                    };
                    MealEntry.prototype.getWeightInGrams = function () {
                        var meal = Mockup.AppState.getInstance().getMealState().getMealById(this.MealId);
                        if (!meal) {
                            return 0;
                        }
                        return this.Count * meal.WeightInGrams;
                    };
                    MealEntry.prototype.getWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units).toFixed(2));
                    };
                    MealEntry.prototype.getCostInUSDP = function () {
                        var meal = Mockup.AppState.getInstance().getMealState().getMealById(this.MealId);
                        if (!meal) {
                            return 0;
                        }
                        return this.Count * meal.CostInUSDP;
                    };
                    MealEntry.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    return MealEntry;
                })();
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
                var RouteDescription = (function () {
                    function RouteDescription(id, description) {
                        this.Id = -1;
                        this.Description = "";
                        this.Id = id;
                        this.Description = description;
                    }
                    return RouteDescription;
                })();
                Trips.RouteDescription = RouteDescription;
                var PointOfInterest = (function () {
                    function PointOfInterest(id, name, gpsCoordinate) {
                        this.Id = -1;
                        this.Name = "";
                        this.GpsCoordinate = "";
                        this.Id = id;
                        this.Name = name;
                        this.GpsCoordinate = gpsCoordinate;
                    }
                    return PointOfInterest;
                })();
                Trips.PointOfInterest = PointOfInterest;
                var TripItinerary = (function () {
                    function TripItinerary() {
                        this.Id = -1;
                        this.Name = "";
                        this.Note = "";
                        this.RouteDescriptions = [];
                        this.PointsOfInterest = [];
                    }
                    /* Load/Save */
                    TripItinerary.prototype.update = function (tripItinerary) {
                        this.Name = tripItinerary.Name;
                        this.Note = tripItinerary.Note;
                        this.RouteDescriptions = [];
                        for (var i = 0; i < tripItinerary.RouteDescriptions.length; ++i) {
                            var routeDescription = tripItinerary.RouteDescriptions[i];
                            this.RouteDescriptions.push(new RouteDescription(routeDescription.Id, routeDescription.Description));
                        }
                        this.PointsOfInterest = [];
                        for (var i = 0; i < tripItinerary.PointsOfInterest.length; ++i) {
                            var pointOfInterest = tripItinerary.PointsOfInterest[i];
                            this.PointsOfInterest.push(new PointOfInterest(pointOfInterest.Id, pointOfInterest.Name, pointOfInterest.GpsCoordinate));
                        }
                    };
                    TripItinerary.prototype.loadFromDevice = function ($q, tripItineraryResource) {
                        var deferred = $q.defer();
                        this.Id = tripItineraryResource.Id;
                        this.Name = tripItineraryResource.Name;
                        this.Note = tripItineraryResource.Note;
                        for (var i = 0; i < tripItineraryResource.RouteDescriptions.length; ++i) {
                            var routeDescription = tripItineraryResource.RouteDescriptions[i];
                            this.RouteDescriptions.push(new RouteDescription(routeDescription.Id, routeDescription.Description));
                        }
                        for (var i = 0; i < tripItineraryResource.PointsOfInterest.length; ++i) {
                            var pointOfInterest = tripItineraryResource.PointsOfInterest[i];
                            this.PointsOfInterest.push(new PointOfInterest(pointOfInterest.Id, pointOfInterest.Name, pointOfInterest.GpsCoordinate));
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    TripItinerary.prototype.saveToDevice = function ($q) {
                        alert("TripItinerary.saveToDevice");
                        return $q.defer().promise;
                    };
                    return TripItinerary;
                })();
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
//<reference path="../../Resources/Trips/TripPlanResource.ts"/>
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
                        this.Id = -1;
                        this.Name = "";
                        this.StartDate = "";
                        this.EndDate = "";
                        this.TripItineraryId = -1;
                        this.Note = "";
                        this.GearCollections = [];
                        this.GearSystems = [];
                        this.GearItems = [];
                        this.Meals = [];
                        this.StartDateAsDate = new Date();
                        this.EndDateAsDate = new Date();
                    }
                    TripPlan.prototype.getTotalGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearCollections.length; ++i) {
                            var gearCollectionEntry = this.GearCollections[i];
                            count += gearCollectionEntry.getGearItemCount();
                        }
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            count += gearSystemEntry.getGearItemCount();
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
                    /* Gear Collections */
                    TripPlan.prototype.getGearCollectionCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearCollections.length; ++i) {
                            var gearCollectionEntry = this.GearCollections[i];
                            count += gearCollectionEntry.Count;
                        }
                        return count;
                    };
                    TripPlan.prototype.getGearCollectionEntryIndexById = function (gearCollectionId) {
                        for (var i = 0; i < this.GearCollections.length; ++i) {
                            var gearCollectionEntry = this.GearCollections[i];
                            if (gearCollectionEntry.GearCollectionId == gearCollectionId) {
                                return i;
                            }
                        }
                        return -1;
                    };
                    TripPlan.prototype.containsGearCollection = function (gearCollection) {
                        return this.getGearCollectionEntryIndexById(gearCollection.Id) >= 0;
                    };
                    TripPlan.prototype.addGearCollection = function (gearCollection) {
                        if (this.containsGearCollection(gearCollection)) {
                            return;
                        }
                        this.GearCollections.push(new Models.Gear.GearCollectionEntry(gearCollection.Id));
                    };
                    TripPlan.prototype.removeGearCollection = function (gearCollection) {
                        var idx = this.getGearCollectionEntryIndexById(gearCollection.Id);
                        if (idx < 0) {
                            return;
                        }
                        this.GearCollections.splice(idx, 1);
                    };
                    TripPlan.prototype.removeAllGearCollections = function () {
                        this.GearCollections = [];
                    };
                    /* Gear Systems */
                    TripPlan.prototype.getGearSystemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            count += gearSystemEntry.Count;
                        }
                        return count;
                    };
                    TripPlan.prototype.getGearSystemEntryIndexById = function (gearSystemId) {
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            if (gearSystemEntry.GearSystemId == gearSystemId) {
                                return i;
                            }
                        }
                        return -1;
                    };
                    TripPlan.prototype.containsGearSystem = function (gearSystem) {
                        return this.getGearSystemEntryIndexById(gearSystem.Id) >= 0;
                    };
                    TripPlan.prototype.addGearSystem = function (gearSystem) {
                        if (this.containsGearSystem(gearSystem)) {
                            return;
                        }
                        this.GearSystems.push(new Models.Gear.GearSystemEntry(gearSystem.Id));
                    };
                    TripPlan.prototype.removeGearSystem = function (gearSystem) {
                        var idx = this.getGearSystemEntryIndexById(gearSystem.Id);
                        if (idx < 0) {
                            return;
                        }
                        this.GearSystems.splice(idx, 1);
                    };
                    TripPlan.prototype.removeAllGearSystems = function () {
                        this.GearSystems = [];
                    };
                    /* Gear Items */
                    TripPlan.prototype.getGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
                    TripPlan.prototype.getGearItemEntryIndexById = function (gearItemId) {
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            if (gearItemEntry.GearItemId == gearItemId) {
                                return i;
                            }
                        }
                        return -1;
                    };
                    TripPlan.prototype.containsGearItem = function (gearItem) {
                        return this.getGearItemEntryIndexById(gearItem.Id) >= 0;
                    };
                    TripPlan.prototype.addGearItem = function (gearItem) {
                        if (this.containsGearItem(gearItem)) {
                            return;
                        }
                        this.GearItems.push(new Models.Gear.GearItemEntry(gearItem.Id));
                    };
                    TripPlan.prototype.removeGearItem = function (gearItem) {
                        var idx = this.getGearItemEntryIndexById(gearItem.Id);
                        if (idx < 0) {
                            return;
                        }
                        this.GearItems.splice(idx, 1);
                    };
                    TripPlan.prototype.removeAllGearItems = function () {
                        this.GearItems = [];
                    };
                    /* Meals */
                    TripPlan.prototype.getMealCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.Meals.length; ++i) {
                            var mealEntry = this.Meals[i];
                            count += mealEntry.Count;
                        }
                        return count;
                    };
                    TripPlan.prototype.getMealEntryIndexById = function (mealId) {
                        for (var i = 0; i < this.Meals.length; ++i) {
                            var mealEntry = this.Meals[i];
                            if (mealEntry.MealId == mealId) {
                                return i;
                            }
                        }
                        return -1;
                    };
                    TripPlan.prototype.containsMeal = function (meal) {
                        return this.getMealEntryIndexById(meal.Id) >= 0;
                    };
                    TripPlan.prototype.addMeal = function (meal) {
                        if (this.containsMeal(meal)) {
                            return;
                        }
                        this.Meals.push(new Models.Meals.MealEntry(meal.Id));
                    };
                    TripPlan.prototype.removeMeal = function (meal) {
                        var idx = this.getMealEntryIndexById(meal.Id);
                        if (idx < 0) {
                            return;
                        }
                        this.Meals.splice(idx, 1);
                    };
                    TripPlan.prototype.removeAllMeals = function () {
                        this.Meals = [];
                    };
                    /* Pack List */
                    TripPlan.prototype.getPackedGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearCollections.length; ++i) {
                            var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this.GearCollections[i].GearCollectionId);
                            count += gearCollection.getPackedGearItemCount();
                        }
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this.GearSystems[i].GearSystemId);
                            count += gearSystem.getPackedGearItemCount();
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            if (gearItemEntry.IsPacked) {
                                ++count;
                            }
                        }
                        return count;
                    };
                    TripPlan.prototype.getPackedMealCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.Meals.length; ++i) {
                            var mealEntry = this.Meals[i];
                            if (mealEntry.IsPacked) {
                                ++count;
                            }
                        }
                        return count;
                    };
                    TripPlan.prototype.getPackList = function () {
                        var entries = [];
                        for (var i = 0; i < this.GearCollections.length; ++i) {
                            var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this.GearCollections[i].GearCollectionId);
                            entries = entries.concat(gearCollection.getPackList());
                        }
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this.GearSystems[i].GearSystemId);
                            entries = entries.concat(gearSystem.getPackList());
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            entries.push(this.GearItems[i]);
                        }
                        for (var i = 0; i < this.Meals.length; ++i) {
                            entries.push(this.Meals[i]);
                        }
                        return entries;
                    };
                    /* Weight/Cost */
                    TripPlan.prototype.getTotalCalories = function () {
                        var calories = 0;
                        for (var i = 0; i < this.Meals.length; ++i) {
                            var mealEntry = this.Meals[i];
                            calories += mealEntry.getCalories();
                        }
                        return calories;
                    };
                    //// TODO: MOVE THIS INTO A UTILITY CLASS OR SOMETHING
                    //// AND MAKE THE CLASSES CONFIGURABLE
                    TripPlan.prototype.getWeightClass = function () {
                        var weightInGrams = this.getWeightInGrams();
                        if (weightInGrams < 4500) {
                            return "Ultralight";
                        }
                        else if (weightInGrams < 9000) {
                            return "Lightweight";
                        }
                        return "Traditional";
                    };
                    TripPlan.prototype.getWeightInGrams = function () {
                        var weightInGrams = 0;
                        for (var i = 0; i < this.GearCollections.length; ++i) {
                            var gearCollectionEntry = this.GearCollections[i];
                            weightInGrams += gearCollectionEntry.getWeightInGrams();
                        }
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            weightInGrams += gearSystemEntry.getWeightInGrams();
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            weightInGrams += gearItemEntry.getWeightInGrams();
                        }
                        for (var i = 0; i < this.Meals.length; ++i) {
                            var mealEntry = this.Meals[i];
                            weightInGrams += mealEntry.getWeightInGrams();
                        }
                        return weightInGrams;
                    };
                    TripPlan.prototype.getWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units);
                    };
                    TripPlan.prototype.getCostInUSDP = function () {
                        var costInUSDP = 0;
                        for (var i = 0; i < this.GearCollections.length; ++i) {
                            var gearCollectionEntry = this.GearCollections[i];
                            costInUSDP += gearCollectionEntry.getCostInUSDP();
                        }
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            costInUSDP += gearSystemEntry.getCostInUSDP();
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            costInUSDP += gearItemEntry.getCostInUSDP();
                        }
                        for (var i = 0; i < this.Meals.length; ++i) {
                            var mealEntry = this.Meals[i];
                            costInUSDP += mealEntry.getCostInUSDP();
                        }
                        return costInUSDP;
                    };
                    TripPlan.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    TripPlan.prototype.getCostPerUnitInCurrency = function () {
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                        var weightInUnits = Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units);
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    TripPlan.prototype.update = function (tripPlan) {
                        this.Name = tripPlan.Name;
                        this.StartDateAsDate = this.StartDateAsDate;
                        this.StartDate = tripPlan.StartDateAsDate.toString();
                        this.EndDateAsDate = this.EndDateAsDate;
                        this.EndDate = tripPlan.EndDateAsDate.toString();
                        this.TripItineraryId = tripPlan.TripItineraryId;
                        this.Note = tripPlan.Note;
                        this.GearCollections = [];
                        for (var i = 0; i < tripPlan.GearCollections.length; ++i) {
                            var gearCollectionEntry = tripPlan.GearCollections[i];
                            this.GearCollections.push(new Models.Gear.GearCollectionEntry(gearCollectionEntry.GearCollectionId, gearCollectionEntry.Count, gearCollectionEntry.IsPacked));
                        }
                        this.GearSystems = [];
                        for (var i = 0; i < tripPlan.GearSystems.length; ++i) {
                            var gearSystemEntry = tripPlan.GearSystems[i];
                            this.GearSystems.push(new Models.Gear.GearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count, gearSystemEntry.IsPacked));
                        }
                        this.GearItems = [];
                        for (var i = 0; i < tripPlan.GearItems.length; ++i) {
                            var gearItemEntry = tripPlan.GearItems[i];
                            this.GearItems.push(new Models.Gear.GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
                        }
                        this.Meals = [];
                        for (var i = 0; i < tripPlan.Meals.length; ++i) {
                            var mealEntry = tripPlan.Meals[i];
                            this.Meals.push(new Models.Meals.MealEntry(mealEntry.MealId, mealEntry.Count, mealEntry.IsPacked));
                        }
                    };
                    TripPlan.prototype.loadFromDevice = function ($q, tripPlanResource) {
                        var deferred = $q.defer();
                        this.Id = tripPlanResource.Id;
                        this.Name = tripPlanResource.Name;
                        this.StartDate = tripPlanResource.StartDate;
                        this.StartDateAsDate = new Date(this.StartDate);
                        this.EndDate = tripPlanResource.EndDate;
                        this.EndDateAsDate = new Date(this.EndDate);
                        this.TripItineraryId = tripPlanResource.TripItineraryId;
                        this.Note = tripPlanResource.Note;
                        for (var i = 0; i < tripPlanResource.GearCollections.length; ++i) {
                            var gearCollectionEntry = tripPlanResource.GearCollections[i];
                            this.GearCollections.push(new Models.Gear.GearCollectionEntry(gearCollectionEntry.GearCollectionId, gearCollectionEntry.Count, gearCollectionEntry.IsPacked));
                        }
                        for (var i = 0; i < tripPlanResource.GearSystems.length; ++i) {
                            var gearSystemEntry = tripPlanResource.GearSystems[i];
                            this.GearSystems.push(new Models.Gear.GearSystemEntry(gearSystemEntry.GearSystemId, gearSystemEntry.Count, gearSystemEntry.IsPacked));
                        }
                        for (var i = 0; i < tripPlanResource.GearItems.length; ++i) {
                            var gearItemEntry = tripPlanResource.GearItems[i];
                            this.GearItems.push(new Models.Gear.GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
                        }
                        for (var i = 0; i < tripPlanResource.Meals.length; ++i) {
                            var mealEntry = tripPlanResource.Meals[i];
                            this.Meals.push(new Models.Meals.MealEntry(mealEntry.MealId, mealEntry.Count, mealEntry.IsPacked));
                        }
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    TripPlan.prototype.saveToDevice = function ($q) {
                        alert("TripPlan.saveToDevice");
                        return $q.defer().promise;
                    };
                    return TripPlan;
                })();
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
                // TODO: remove the item from the systems, collections, and trip plans it belongs to
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
            GearState.prototype.deleteGearSystem = function (gearSystem) {
                var idx = this.getGearSystemIndexById(gearSystem.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearSystems.splice(idx, 1);
                // TODO: remove the system from the collections, and trip plans it belongs to
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
            GearState.prototype.deleteGearCollection = function (gearCollection) {
                var idx = this.getGearCollectionIndexById(gearCollection.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearCollections.splice(idx, 1);
                // TODO: remove the collection from the trip plans it belongs to
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
        })();
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
                // TODO: remove the meal from the trip plans it belongs to
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
        })();
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
                // TODO: remove the itinerary from the trip plans it belongs to
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
        })();
        Mockup.TripState = TripState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../scripts/typings/angularjs/angular.d.ts" />
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
        })();
        Mockup.AppState = AppState;
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
                        this.Id = -1;
                        this.Name = "";
                        this.Url = "";
                        this.Make = "";
                        this.Model = "";
                        this.Carried = "Carried";
                        this.WeightInGrams = 0;
                        this.CostInUSDP = 0;
                        this.IsConsumable = false;
                        this.ConsumedPerDay = 1;
                        this.Note = "";
                    }
                    /* Weight/Cost */
                    GearItem.prototype.weightInUnits = function (weight) {
                        return arguments.length
                            ? (this.WeightInGrams = Mockup.convertUnitsToGrams(weight, Mockup.AppState.getInstance().getAppSettings().Units))
                            : parseFloat(Mockup.convertGramsToUnits(this.WeightInGrams, Mockup.AppState.getInstance().getAppSettings().Units).toFixed(2));
                    };
                    GearItem.prototype.costInCurrency = function (cost) {
                        return arguments.length
                            ? (this.CostInUSDP = Mockup.convertCurrencyToUSDP(cost, Mockup.AppState.getInstance().getAppSettings().Currency))
                            : Mockup.convertUSDPToCurrency(this.CostInUSDP, Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    GearItem.prototype.getCostPerUnitInCurrency = function () {
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.CostInUSDP, Mockup.AppState.getInstance().getAppSettings().Currency);
                        var weightInUnits = Mockup.convertGramsToUnits(this.WeightInGrams, Mockup.AppState.getInstance().getAppSettings().Units);
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
                    /* Load/Save */
                    GearItem.prototype.update = function (gearItem) {
                        this.Name = gearItem.Name;
                        this.Url = gearItem.Url;
                        this.Make = gearItem.Make;
                        this.Model = gearItem.Model;
                        this.Carried = gearItem.Carried;
                        this.WeightInGrams = gearItem.WeightInGrams;
                        this.CostInUSDP = gearItem.CostInUSDP;
                        this.IsConsumable = gearItem.IsConsumable;
                        this.ConsumedPerDay = gearItem.ConsumedPerDay;
                        this.Note = gearItem.Note;
                    };
                    GearItem.prototype.loadFromDevice = function ($q, gearItemResource) {
                        var deferred = $q.defer();
                        this.Id = gearItemResource.Id;
                        this.Name = gearItemResource.Name;
                        this.Url = gearItemResource.Url;
                        this.Make = gearItemResource.Make;
                        this.Model = gearItemResource.Model;
                        this.Carried = gearItemResource.Carried;
                        this.WeightInGrams = gearItemResource.WeightInGrams;
                        this.CostInUSDP = gearItemResource.CostInUSDP;
                        this.IsConsumable = gearItemResource.IsConsumable;
                        this.ConsumedPerDay = gearItemResource.ConsumedPerDay;
                        this.Note = gearItemResource.Note;
                        deferred.resolve(this);
                        return deferred.promise;
                    };
                    GearItem.prototype.saveToDevice = function ($q) {
                        alert("GearItem.saveToDevice");
                        return $q.defer().promise;
                    };
                    return GearItem;
                })();
                Gear.GearItem = GearItem;
                var GearItemEntry = (function () {
                    function GearItemEntry(gearItemId, count, isPacked) {
                        this.GearItemId = -1;
                        this.Count = 1;
                        this.IsPacked = false;
                        this.GearItemId = gearItemId;
                        if (count) {
                            this.Count = count;
                        }
                        if (isPacked) {
                            this.IsPacked = isPacked;
                        }
                    }
                    GearItemEntry.prototype.getName = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
                        if (!gearItem) {
                            return "";
                        }
                        return gearItem.Name;
                    };
                    GearItemEntry.prototype.getWeightInGrams = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
                        if (!gearItem) {
                            return 0;
                        }
                        return this.Count * gearItem.WeightInGrams;
                    };
                    GearItemEntry.prototype.getWeightInUnits = function () {
                        return parseFloat(Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units).toFixed(2));
                    };
                    GearItemEntry.prototype.getCostInUSDP = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
                        if (!gearItem) {
                            return 0;
                        }
                        return this.Count * gearItem.CostInUSDP;
                    };
                    GearItemEntry.prototype.getCostInCurrency = function () {
                        return Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    return GearItemEntry;
                })();
                Gear.GearItemEntry = GearItemEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../../Models/Gear/GearItem.ts" />
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
///<reference path="../../scripts/typings/angular-material/angular-material.d.ts" />
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
                function AppCtrl($scope, $q, $location, $mdSidenav, $mdDialog, $mdToast, appSettingsService, userInformationService, gearItemService, gearSystemService, gearCollectionService, mealService, tripItineraryService, tripPlanService) {
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
                        return Mockup.getUnitsWeightString(Mockup.AppState.getInstance().getAppSettings().Units);
                    };
                    $scope.getUnitsLengthString = function () {
                        return Mockup.getUnitsLengthString(Mockup.AppState.getInstance().getAppSettings().Units);
                    };
                    $scope.getCurrencyString = function () {
                        return Mockup.getCurrencyString(Mockup.AppState.getInstance().getAppSettings().Currency);
                    };
                    $scope.getDaysBetween = function (startDate, endDate) {
                        var oneDayInMs = 86400000;
                        var startDateInMs = startDate.getTime();
                        var endDateInMs = endDate.getTime();
                        var daysBetweenInMs = endDateInMs - startDateInMs;
                        return Math.round(daysBetweenInMs / oneDayInMs);
                    };
                    // view utilities
                    $scope.isActive = function (viewLocation) {
                        // set the nav item as active when we're looking at its location
                        return $location.path() === viewLocation;
                    };
                    $scope.toggleSidenav = function () {
                        $mdSidenav("left").toggle();
                    };
                }
                return AppCtrl;
            })();
            Controllers.AppCtrl = AppCtrl;
            AppCtrl.$inject = ["$scope", "$q", "$location", "$mdSidenav", "$mdDialog", "$mdToast",
                "AppSettingsService", "UserInformationService",
                "GearItemService", "GearSystemService", "GearCollectionService",
                "MealService", "TripItineraryService", "TripPlanService"];
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                    .content("Added gear collection: " + $scope.gearCollection.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoAddToast = $mdToast.simple()
                                    .content("Removed gear collection: " + $scope.gearCollection.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/collections");
                                $mdToast.show(addToast).then(function (response) {
                                    if ("ok" == response) {
                                        Mockup.AppState.getInstance().getGearState().deleteGearCollection($scope.gearCollection);
                                        $mdToast.show(undoAddToast);
                                    }
                                });
                            };
                            $scope.resetGearCollection = function () {
                                $scope.gearCollection = new Mockup.Models.Gear.GearCollection();
                            };
                        }
                        return AddGearCollectionCtrl;
                    })();
                    Collections.AddGearCollectionCtrl = AddGearCollectionCtrl;
                    AddGearCollectionCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                $location.path("/gear/collections");
                                // TODO: toast!
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
                                    .content("Are you sure you wish to delete this gear collection?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Gear collection deleted!")
                                    .content("The gear collection has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .content("Deleted gear collection: " + $scope.gearCollection.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .content("Restored gear collection: " + $scope.gearCollection.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        if (!Mockup.AppState.getInstance().getGearState().deleteGearCollection($scope.gearCollection)) {
                                            alert("Couldn't find the gear collection to delete!");
                                            return;
                                        }
                                        $location.path("/gear/collections");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                // TODO: this does *not* restore the collection to its containers
                                                // and it should probably do so... but how?
                                                Mockup.AppState.getInstance().getGearState().addGearCollection($scope.gearCollection);
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/gear/collections/" + $scope.gearCollection.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return GearCollectionCtrl;
                    })();
                    Collections.GearCollectionCtrl = GearCollectionCtrl;
                    GearCollectionCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
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
                    })();
                    Collections.GearCollectionsCtrl = GearCollectionsCtrl;
                    GearCollectionsCtrl.$inject = ["$scope", "$mdDialog"];
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                    .content("Added gear item: " + $scope.gearItem.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoAddToast = $mdToast.simple()
                                    .content("Removed gear item: " + $scope.gearItem.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/items");
                                $mdToast.show(addToast).then(function (response) {
                                    if ("ok" == response) {
                                        Mockup.AppState.getInstance().getGearState().deleteGearItem($scope.gearItem);
                                        $mdToast.show(undoAddToast);
                                    }
                                });
                            };
                            $scope.resetGearItem = function () {
                                $scope.gearItem = new Mockup.Models.Gear.GearItem();
                            };
                        }
                        return AddGearItemCtrl;
                    })();
                    Items.AddGearItemCtrl = AddGearItemCtrl;
                    AddGearItemCtrl.$inject = ["$scope", "$location", "$mdToast"];
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                $location.path("/gear/items");
                                // TODO: toast!
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
                                    .content("Are you sure you wish to delete this gear item?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Gear item deleted!")
                                    .content("The gear item has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .content("Deleted gear item: " + $scope.gearItem.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .content("Restored gear item: " + $scope.gearItem.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        if (!Mockup.AppState.getInstance().getGearState().deleteGearItem($scope.gearItem)) {
                                            alert("Couldn't find the gear item to delete!");
                                            return;
                                        }
                                        $location.path("/gear/items");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                // TODO: this does *not* restore the item to its containers
                                                // and it should probably do so... but how?
                                                Mockup.AppState.getInstance().getGearState().addGearItem($scope.gearItem);
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/gear/items/" + $scope.gearItem.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return GearItemCtrl;
                    })();
                    Items.GearItemCtrl = GearItemCtrl;
                    GearItemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
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
                    })();
                    Items.GearItemsCtrl = GearItemsCtrl;
                    GearItemsCtrl.$inject = ["$scope", "$mdDialog"];
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                    .content("Added gear system: " + $scope.gearSystem.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoAddToast = $mdToast.simple()
                                    .content("Removed gear system: " + $scope.gearSystem.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/systems");
                                $mdToast.show(addToast).then(function (response) {
                                    if ("ok" == response) {
                                        Mockup.AppState.getInstance().getGearState().deleteGearSystem($scope.gearSystem);
                                        $mdToast.show(undoAddToast);
                                    }
                                });
                            };
                            $scope.resetGearSystem = function () {
                                $scope.gearSystem = new Mockup.Models.Gear.GearSystem();
                            };
                        }
                        return AddGearSystemCtrl;
                    })();
                    Systems.AddGearSystemCtrl = AddGearSystemCtrl;
                    AddGearSystemCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                $location.path("/gear/systems");
                                // TODO: toast!
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
                                    .content("Are you sure you wish to delete this gear system?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Gear system deleted!")
                                    .content("The gear system has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .content("Deleted gear system: " + $scope.gearSystem.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .content("Restored gear system: " + $scope.gearSystem.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        if (!Mockup.AppState.getInstance().getGearState().deleteGearSystem($scope.gearSystem)) {
                                            alert("Couldn't find the gear system to delete!");
                                            return;
                                        }
                                        $location.path("/gear/systems");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                // TODO: this does *not* restore the system to its containers
                                                // and it should probably do so... but how?
                                                Mockup.AppState.getInstance().getGearState().addGearSystem($scope.gearSystem);
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/gear/systems/" + $scope.gearSystem.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return GearSystemCtrl;
                    })();
                    Systems.GearSystemCtrl = GearSystemCtrl;
                    GearSystemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
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
                    })();
                    Systems.GearSystemsCtrl = GearSystemsCtrl;
                    GearSystemsCtrl.$inject = ["$scope", "$mdDialog"];
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                        $scope.orderBy = "Name";
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
                })();
                Meals.MealsCtrl = MealsCtrl;
                MealsCtrl.$inject = ["$scope", "$mdDialog"];
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $location.path("/meals");
                            // TODO: toast!
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
                                .content("Are you sure you wish to delete this meal?")
                                .ok("Yes")
                                .cancel("No")
                                .targetEvent(event);
                            var receipt = $mdDialog.alert()
                                .parent(angular.element(document.body))
                                .title("Meal deleted!")
                                .content("The meal has been deleted.")
                                .ok("OK")
                                .targetEvent(event);
                            var deleteToast = $mdToast.simple()
                                .content("Deleted meal: " + $scope.meal.Name)
                                .action("Undo")
                                .position("bottom left");
                            var undoDeleteToast = $mdToast.simple()
                                .content("Restored meal: " + $scope.meal.Name)
                                .action("OK")
                                .position("bottom left");
                            $mdDialog.show(confirm).then(function () {
                                $mdDialog.show(receipt).then(function () {
                                    if (!Mockup.AppState.getInstance().getMealState().deleteMeal($scope.meal)) {
                                        alert("Couldn't find the meal to delete!");
                                        return;
                                    }
                                    $location.path("/meals");
                                    $mdToast.show(deleteToast).then(function (response) {
                                        if ("ok" == response) {
                                            // TODO: this does *not* restore the meal to its containers
                                            // and it should probably do so... but how?
                                            Mockup.AppState.getInstance().getMealState().addMeal($scope.meal);
                                            $mdToast.show(undoDeleteToast);
                                            $location.path("/meals/" + $scope.meal.Id);
                                        }
                                    });
                                });
                            });
                        };
                    }
                    return MealCtrl;
                })();
                Meals.MealCtrl = MealCtrl;
                MealCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                .content("Added meal: " + $scope.meal.Name)
                                .action("Undo")
                                .position("bottom left");
                            var undoAddToast = $mdToast.simple()
                                .content("Removed meal: " + $scope.meal.Name)
                                .action("OK")
                                .position("bottom left");
                            $location.path("/meals");
                            $mdToast.show(addToast).then(function (response) {
                                if ("ok" == response) {
                                    Mockup.AppState.getInstance().getMealState().deleteMeal($scope.meal);
                                    $mdToast.show(undoAddToast);
                                }
                            });
                        };
                        $scope.resetMeal = function () {
                            $scope.meal = new Mockup.Models.Meals.Meal();
                        };
                    }
                    return AddMealCtrl;
                })();
                Meals.AddMealCtrl = AddMealCtrl;
                AddMealCtrl.$inject = ["$scope", "$location", "$mdToast"];
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
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
                    })();
                    Itineraries.TripItinerariesCtrl = TripItinerariesCtrl;
                    TripItinerariesCtrl.$inject = ["$scope", "$mdDialog"];
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                $location.path("/trips/itineraries");
                                // TODO: toast!
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
                                    .content("Are you sure you wish to delete this trip itinerary?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Trip itinerary deleted!")
                                    .content("The trip itinerary has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .content("Deleted trip itinerary: " + $scope.tripItinerary.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .content("Restored trip itinerary: " + $scope.tripItinerary.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        if (!Mockup.AppState.getInstance().getTripState().deleteTripItinerary($scope.tripItinerary)) {
                                            alert("Couldn't find the trip itinerary to delete!");
                                            return;
                                        }
                                        $location.path("/trips/itineraries");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                // TODO: this does *not* restore the itinerary to its containers
                                                // and it should probably do so... but how?
                                                Mockup.AppState.getInstance().getTripState().addTripItinerary($scope.tripItinerary);
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/trips/itineraries/" + $scope.tripItinerary.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return TripItineraryCtrl;
                    })();
                    Itineraries.TripItineraryCtrl = TripItineraryCtrl;
                    TripItineraryCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                    .content("Added trip itinerary: " + $scope.tripItinerary.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoAddToast = $mdToast.simple()
                                    .content("Removed trip itinerary: " + $scope.tripItinerary.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/itineraries");
                                $mdToast.show(addToast).then(function (response) {
                                    if ("ok" == response) {
                                        Mockup.AppState.getInstance().getTripState().deleteTripItinerary($scope.tripItinerary);
                                        $mdToast.show(undoAddToast);
                                    }
                                });
                            };
                            $scope.resetTripItinerary = function () {
                                $scope.tripItinerary = new Mockup.Models.Trips.TripItinerary();
                            };
                        }
                        return AddTripItineraryCtrl;
                    })();
                    Itineraries.AddTripItineraryCtrl = AddTripItineraryCtrl;
                    AddTripItineraryCtrl.$inject = ["$scope", "$location", "$mdToast"];
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
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
                    })();
                    Plans.TripPlansCtrl = TripPlansCtrl;
                    TripPlansCtrl.$inject = ["$scope", "$mdDialog"];
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.showPackListDlg = function (event) {
                                $mdDialog.show({
                                    controller: Plans.PackListDlgCtrl,
                                    templateUrl: "content/partials/trips/plans/packlist.html",
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
                                $location.path("/trips/plans");
                                // TODO: toast!
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
                                    .content("Are you sure you wish to delete this trip plan?")
                                    .ok("Yes")
                                    .cancel("No")
                                    .targetEvent(event);
                                var receipt = $mdDialog.alert()
                                    .parent(angular.element(document.body))
                                    .title("Trip plan deleted!")
                                    .content("The trip plan has been deleted.")
                                    .ok("OK")
                                    .targetEvent(event);
                                var deleteToast = $mdToast.simple()
                                    .content("Deleted trip plan: " + $scope.tripPlan.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoDeleteToast = $mdToast.simple()
                                    .content("Restored trip plan: " + $scope.tripPlan.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $mdDialog.show(confirm).then(function () {
                                    $mdDialog.show(receipt).then(function () {
                                        if (!Mockup.AppState.getInstance().getTripState().deleteTripPlan($scope.tripPlan)) {
                                            alert("Couldn't find the trip plan to delete!");
                                            return;
                                        }
                                        $location.path("/trips/plans");
                                        $mdToast.show(deleteToast).then(function (response) {
                                            if ("ok" == response) {
                                                Mockup.AppState.getInstance().getTripState().addTripPlan($scope.tripPlan);
                                                $mdToast.show(undoDeleteToast);
                                                $location.path("/trips/plans/" + $scope.tripPlan.Id);
                                            }
                                        });
                                    });
                                });
                            };
                        }
                        return TripPlanCtrl;
                    })();
                    Plans.TripPlanCtrl = TripPlanCtrl;
                    TripPlanCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                                    .content("Added trip plan: " + $scope.tripPlan.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoAddToast = $mdToast.simple()
                                    .content("Removed trip plan: " + $scope.tripPlan.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/trips/plans");
                                $mdToast.show(addToast).then(function (response) {
                                    if ("ok" == response) {
                                        Mockup.AppState.getInstance().getTripState().deleteTripPlan($scope.tripPlan);
                                        $mdToast.show(undoAddToast);
                                    }
                                });
                            };
                            $scope.resetTripPlan = function () {
                                $scope.tripPlan = new Mockup.Models.Trips.TripPlan();
                            };
                        }
                        return AddTripPlanCtrl;
                    })();
                    Plans.AddTripPlanCtrl = AddTripPlanCtrl;
                    AddTripPlanCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $location.path("/");
                            // TODO: toast!
                        };
                        $scope.resetUserInformation = function () {
                            $scope.userInfo = angular.copy(Mockup.AppState.getInstance().getUserInformation());
                        };
                    }
                    return UserInformationCtrl;
                })();
                Personal.UserInformationCtrl = UserInformationCtrl;
                UserInformationCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
            })(Personal = Controllers.Personal || (Controllers.Personal = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/angular-material.d.ts" />
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
                        $location.path("/");
                        // TODO: toast!
                    };
                    $scope.resetAppSettings = function () {
                        $scope.appSettings = angular.copy(Mockup.AppState.getInstance().getAppSettings());
                    };
                    $scope.deleteAllGearItems = function (event) {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Gear Items")
                            .content("Are you sure you wish to delete all gear items?")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All gear items deleted!")
                            .content("All gear items have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .content("Deleted all gear items")
                            .action("Undo")
                            .position("bottom left");
                        var undoDeleteToast = $mdToast.simple()
                            .content("Restored all gear items")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getGearState().deleteAllGearItems();
                                $mdToast.show(deleteToast).then(function (response) {
                                    if ("ok" == response) {
                                        // TODO: this does *not* restore anything
                                        // and it should probably do so... but how?
                                        $mdToast.show(undoDeleteToast);
                                    }
                                });
                            });
                        });
                    };
                    $scope.deleteAllGearSystems = function (event) {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Gear Systems")
                            .content("Are you sure you wish to delete all gear systems?")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All gear systems deleted!")
                            .content("All gear systems have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .content("Deleted all gear systems")
                            .action("Undo")
                            .position("bottom left");
                        var undoDeleteToast = $mdToast.simple()
                            .content("Restored all gear systems")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getGearState().deleteAllGearSystems();
                                $mdToast.show(deleteToast).then(function (response) {
                                    if ("ok" == response) {
                                        // TODO: this does *not* restore anything
                                        // and it should probably do so... but how?
                                        $mdToast.show(undoDeleteToast);
                                    }
                                });
                            });
                        });
                    };
                    $scope.deleteAllGearCollections = function (event) {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Gear Collections")
                            .content("Are you sure you wish to delete all gear collections?")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All gear collections deleted!")
                            .content("All gear collections have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .content("Deleted all gear collections")
                            .action("Undo")
                            .position("bottom left");
                        var undoDeleteToast = $mdToast.simple()
                            .content("Restored all gear collections")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getGearState().deleteAllGearCollections();
                                $mdToast.show(deleteToast).then(function (response) {
                                    if ("ok" == response) {
                                        // TODO: this does *not* restore anything
                                        // and it should probably do so... but how?
                                        $mdToast.show(undoDeleteToast);
                                    }
                                });
                            });
                        });
                    };
                    $scope.deleteAllMeals = function (event) {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Meals")
                            .content("Are you sure you wish to delete all meals?")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All meals deleted!")
                            .content("All meals have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .content("Deleted all meals")
                            .action("Undo")
                            .position("bottom left");
                        var undoDeleteToast = $mdToast.simple()
                            .content("Restored all meals")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getMealState().deleteAllMeals();
                                $mdToast.show(deleteToast).then(function (response) {
                                    if ("ok" == response) {
                                        // TODO: this does *not* restore anything
                                        // and it should probably do so... but how?
                                        $mdToast.show(undoDeleteToast);
                                    }
                                });
                            });
                        });
                    };
                    $scope.deleteAllTripItineraries = function (event) {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Trip Itineraries")
                            .content("Are you sure you wish to delete all trip itineraries?")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All trip itineraries deleted!")
                            .content("All trip itineraries have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .content("Deleted all trip itineraries")
                            .action("Undo")
                            .position("bottom left");
                        var undoDeleteToast = $mdToast.simple()
                            .content("Restored all trip itineraries")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getTripState().deleteAllTripItineraries();
                                $mdToast.show(deleteToast).then(function (response) {
                                    if ("ok" == response) {
                                        // TODO: this does *not* restore anything
                                        // and it should probably do so... but how?
                                        $mdToast.show(undoDeleteToast);
                                    }
                                });
                            });
                        });
                    };
                    $scope.deleteAllTripPlans = function (event) {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Trip Plans")
                            .content("Are you sure you wish to delete all trip plans?")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All trip plans deleted!")
                            .content("All trip plans have been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .content("Deleted all trip plans")
                            .action("Undo")
                            .position("bottom left");
                        var undoDeleteToast = $mdToast.simple()
                            .content("Restored all trip plans")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().getTripState().deleteAllTripPlans();
                                $mdToast.show(deleteToast).then(function (response) {
                                    if ("ok" == response) {
                                        // TODO: this does *not* restore anything
                                        // and it should probably do so... but how?
                                        $mdToast.show(undoDeleteToast);
                                    }
                                });
                            });
                        });
                    };
                    $scope.deleteAllData = function (event) {
                        var confirm = $mdDialog.confirm()
                            .parent(angular.element(document.body))
                            .title("Delete All Data")
                            .content("Are you sure you wish to delete all data?")
                            .ok("Yes")
                            .cancel("No")
                            .targetEvent(event);
                        var receipt = $mdDialog.alert()
                            .parent(angular.element(document.body))
                            .title("All data deleted!")
                            .content("All data has been deleted.")
                            .ok("OK")
                            .targetEvent(event);
                        var deleteToast = $mdToast.simple()
                            .content("Deleted all data")
                            .action("Undo")
                            .position("bottom left");
                        var undoDeleteToast = $mdToast.simple()
                            .content("Restored all data")
                            .action("OK")
                            .position("bottom left");
                        $mdDialog.show(confirm).then(function () {
                            $mdDialog.show(receipt).then(function () {
                                Mockup.AppState.getInstance().deleteAllData();
                                $mdToast.show(deleteToast).then(function (response) {
                                    if ("ok" == response) {
                                        // TODO: this does *not* restore anything
                                        // and it should probably do so... but how?
                                        $mdToast.show(undoDeleteToast);
                                    }
                                });
                            });
                        });
                    };
                }
                return AppSettingsCtrl;
            })();
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
        })();
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
        var RouteConfig = (function () {
            function RouteConfig($routeProvider) {
                $routeProvider.when("/", {
                    redirectTo: "/index"
                })
                    .when("/index", {
                    templateUrl: "content/partials/main.html",
                    title: "Backpacking Planner"
                })
                    .when("/gear/items", {
                    templateUrl: "content/partials/gear/items/items.html",
                    controller: "GearItemsCtrl",
                    title: "Gear Items"
                })
                    .when("/gear/items/add", {
                    templateUrl: "content/partials/gear/items/add.html",
                    controller: "AddGearItemCtrl",
                    title: "Add a Gear Item"
                })
                    .when("/gear/items/:gearItemId", {
                    templateUrl: "content/partials/gear/items/item.html",
                    controller: "GearItemCtrl",
                    title: "Gear Item"
                })
                    .when("/gear/systems", {
                    templateUrl: "content/partials/gear/systems/systems.html",
                    controller: "GearSystemsCtrl",
                    title: "Gear Systems"
                })
                    .when("/gear/systems/add", {
                    templateUrl: "content/partials/gear/systems/add.html",
                    controller: "AddGearSystemCtrl",
                    title: "Add a Gear System"
                })
                    .when("/gear/systems/:gearSystemId", {
                    templateUrl: "content/partials/gear/systems/system.html",
                    controller: "GearSystemCtrl",
                    title: "Gear System"
                })
                    .when("/gear/collections", {
                    templateUrl: "content/partials/gear/collections/collections.html",
                    controller: "GearCollectionsCtrl",
                    title: "Gear Collections"
                })
                    .when("/gear/collections/add", {
                    templateUrl: "content/partials/gear/collections/add.html",
                    controller: "AddGearCollectionCtrl",
                    title: "Add a Gear Collection"
                })
                    .when("/gear/collections/:gearCollectionId", {
                    templateUrl: "content/partials/gear/collections/collection.html",
                    controller: "GearCollectionCtrl",
                    title: "Gear Collection"
                })
                    .when("/meals", {
                    templateUrl: "content/partials/meals/meals.html",
                    controller: "MealsCtrl",
                    title: "Meals"
                })
                    .when("/meals/add", {
                    templateUrl: "content/partials/meals/add.html",
                    controller: "AddMealCtrl",
                    title: "Add a Meal"
                })
                    .when("/meals/:mealId", {
                    templateUrl: "content/partials/meals/meal.html",
                    controller: "MealCtrl",
                    title: "Meal"
                })
                    .when("/trips/itineraries", {
                    templateUrl: "content/partials/trips/itineraries/itineraries.html",
                    controller: "TripItinerariesCtrl",
                    title: "Trip Itineraries"
                })
                    .when("/trips/itineraries/add", {
                    templateUrl: "content/partials/trips/itineraries/add.html",
                    controller: "AddTripItineraryCtrl",
                    title: "Add a Trip Itinerary"
                })
                    .when("/trips/itineraries/:tripItineraryId", {
                    templateUrl: "content/partials/trips/itineraries/itinerary.html",
                    controller: "TripItineraryCtrl",
                    title: "Trip Itinerary"
                })
                    .when("/trips/plans", {
                    templateUrl: "content/partials/trips/plans/plans.html",
                    controller: "TripPlansCtrl",
                    title: "Trip Plans"
                })
                    .when("/trips/plans/add", {
                    templateUrl: "content/partials/trips/plans/add.html",
                    controller: "AddTripPlanCtrl",
                    title: "Add a Trip Plan"
                })
                    .when("/trips/plans/:tripPlanId", {
                    templateUrl: "content/partials/trips/plans/plan.html",
                    controller: "TripPlanCtrl",
                    title: "Trip Plan"
                })
                    .when("/personal", {
                    templateUrl: "content/partials/personal/personal.html",
                    controller: "UserInformationCtrl",
                    title: "Personal Information"
                })
                    .when("/settings", {
                    templateUrl: "content/partials/settings.html",
                    controller: "AppSettingsCtrl",
                    title: "Settings"
                })
                    .when("/help", {
                    templateUrl: "content/partials/help.html",
                    title: "Help"
                })
                    .when("/404", {
                    templateUrl: "content/partials/404.html",
                    title: "Backpacking Planner"
                }).when("/500", {
                    templateUrl: "content/partials/500.html",
                    title: "Backpacking Planner"
                })
                    .otherwise({
                    redirectTo: "/404"
                });
            }
            return RouteConfig;
        })();
        Mockup.RouteConfig = RouteConfig;
        ;
        RouteConfig.$inject = ["$routeProvider"];
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../Scripts/typings/angular-material/angular-material.d.ts" />
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
        })();
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
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.getGearItems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.isGearItemSelected = function (gearItem) {
                                return $scope.gearCollection.containsGearItem(gearItem);
                            };
                            $scope.toggleGearItemSelected = function (gearItem) {
                                if (!$scope.gearCollection.containsGearItem(gearItem)) {
                                    $scope.gearCollection.addGearItem(gearItem);
                                }
                                else {
                                    $scope.gearCollection.removeGearItem(gearItem);
                                }
                            };
                        }
                        return AddGearItemDlgCtrl;
                    })();
                    Collections.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
                            $scope.getGearSystems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearSystems();
                            };
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.isGearSystemSelected = function (gearSystem) {
                                return $scope.gearCollection.containsGearSystem(gearSystem);
                            };
                            $scope.toggleGearSystemSelected = function (gearSystem) {
                                if (!$scope.gearCollection.containsGearSystem(gearSystem)) {
                                    $scope.gearCollection.addGearSystem(gearSystem);
                                }
                                else {
                                    $scope.gearCollection.removeGearSystem(gearSystem);
                                }
                            };
                        }
                        return AddGearSystemDlgCtrl;
                    })();
                    Collections.AddGearSystemDlgCtrl = AddGearSystemDlgCtrl;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                    })();
                    Collections.WhatIsGearCollectionDlgCtrl = WhatIsGearCollectionDlgCtrl;
                })(Collections = Gear.Collections || (Gear.Collections = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                    })();
                    Items.WhatIsGearItemDlgCtrl = WhatIsGearItemDlgCtrl;
                })(Items = Gear.Items || (Gear.Items = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.getGearItems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.isGearItemSelected = function (gearItem) {
                                return $scope.gearSystem.containsGearItem(gearItem);
                            };
                            $scope.toggleGearItemSelected = function (gearItem) {
                                if (!$scope.gearSystem.containsGearItem(gearItem)) {
                                    $scope.gearSystem.addGearItem(gearItem);
                                }
                                else {
                                    $scope.gearSystem.removeGearItem(gearItem);
                                }
                            };
                        }
                        return AddGearItemDlgCtrl;
                    })();
                    Systems.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                    })();
                    Systems.WhatIsGearSystemDlgCtrl = WhatIsGearSystemDlgCtrl;
                })(Systems = Gear.Systems || (Gear.Systems = {}));
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                })();
                Meals.WhatIsMealDlgCtrl = WhatIsMealDlgCtrl;
            })(Meals = Controllers.Meals || (Controllers.Meals = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                })();
                Personal.WhatIsPersonalDlgCtrl = WhatIsPersonalDlgCtrl;
            })(Personal = Controllers.Personal || (Controllers.Personal = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                    })();
                    Itineraries.WhatIsTripItineraryDlgCtrl = WhatIsTripItineraryDlgCtrl;
                })(Itineraries = Trips.Itineraries || (Trips.Itineraries = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.getGearCollections = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearCollections();
                            };
                            $scope.isGearCollectionSelected = function (gearCollection) {
                                return $scope.tripPlan.containsGearCollection(gearCollection);
                            };
                            $scope.toggleGearCollectionSelected = function (gearCollection) {
                                if (!$scope.tripPlan.containsGearCollection(gearCollection)) {
                                    $scope.tripPlan.addGearCollection(gearCollection);
                                }
                                else {
                                    $scope.tripPlan.removeGearCollection(gearCollection);
                                }
                            };
                        }
                        return AddGearCollectionDlgCtrl;
                    })();
                    Plans.AddGearCollectionDlgCtrl = AddGearCollectionDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.getGearItems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.isGearItemSelected = function (gearItem) {
                                return $scope.tripPlan.containsGearItem(gearItem);
                            };
                            $scope.toggleGearItemSelected = function (gearItem) {
                                if (!$scope.tripPlan.containsGearItem(gearItem)) {
                                    $scope.tripPlan.addGearItem(gearItem);
                                }
                                else {
                                    $scope.tripPlan.removeGearItem(gearItem);
                                }
                            };
                        }
                        return AddGearItemDlgCtrl;
                    })();
                    Plans.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
                            $scope.getGearSystems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearSystems();
                            };
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.isGearSystemSelected = function (gearSystem) {
                                return $scope.tripPlan.containsGearSystem(gearSystem);
                            };
                            $scope.toggleGearSystemSelected = function (gearSystem) {
                                if (!$scope.tripPlan.containsGearSystem(gearSystem)) {
                                    $scope.tripPlan.addGearSystem(gearSystem);
                                }
                                else {
                                    $scope.tripPlan.removeGearSystem(gearSystem);
                                }
                            };
                        }
                        return AddGearSystemDlgCtrl;
                    })();
                    Plans.AddGearSystemDlgCtrl = AddGearSystemDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                            $scope.orderBy = "Name";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.getMeals = function () {
                                return Mockup.AppState.getInstance().getMealState().getMeals();
                            };
                            $scope.isMealSelected = function (meal) {
                                return $scope.tripPlan.containsMeal(meal);
                            };
                            $scope.toggleMealSelected = function (meal) {
                                if (!$scope.tripPlan.containsMeal(meal)) {
                                    $scope.tripPlan.addMeal(meal);
                                }
                                else {
                                    $scope.tripPlan.removeMeal(meal);
                                }
                            };
                        }
                        return AddMealDlgCtrl;
                    })();
                    Plans.AddMealDlgCtrl = AddMealDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                    var PackListDlgCtrl = (function () {
                        function PackListDlgCtrl($scope, $mdDialog, tripPlan) {
                            $scope.tripPlan = tripPlan;
                            $scope.orderBy = "getName()";
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.getGearItems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.getMeals = function () {
                                return Mockup.AppState.getInstance().getMealState().getMeals();
                            };
                        }
                        return PackListDlgCtrl;
                    })();
                    Plans.PackListDlgCtrl = PackListDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../../scripts/typings/angular-material/angular-material.d.ts" />
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
                    })();
                    Plans.WhatIsTripPlanDlgCtrl = WhatIsTripPlanDlgCtrl;
                })(Plans = Trips.Plans || (Trips.Plans = {}));
            })(Trips = Controllers.Trips || (Controllers.Trips = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
//# sourceMappingURL=mockup.js.map