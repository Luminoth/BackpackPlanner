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
                    function UserInformation(userInfoResource) {
                        this.FirstName = "";
                        this.LastName = "";
                        this.BirthDate = "";
                        this.Sex = "NotSpecified";
                        this.HeightInCm = 0;
                        this.WeightInGrams = 0;
                        this.BirthDateAsDate = new Date();
                        if (userInfoResource) {
                            this.FirstName = userInfoResource.FirstName;
                            this.LastName = userInfoResource.LastName;
                            this.BirthDate = userInfoResource.BirthDate;
                            this.Sex = userInfoResource.Sex;
                            this.HeightInCm = userInfoResource.HeightInCm;
                            this.WeightInGrams = userInfoResource.WeightInGrams;
                            this.BirthDateAsDate = new Date(this.BirthDate);
                        }
                    }
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
///<reference path="../Resources/AppSettingsResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            "use strict";
            var AppSettings = (function () {
                function AppSettings(appSettingsResource) {
                    this.Units = "Metric";
                    this.Currency = "USD";
                    if (appSettingsResource) {
                        this.Units = appSettingsResource.Units;
                        this.Currency = appSettingsResource.Currency;
                    }
                }
                return AppSettings;
            })();
            Models.AppSettings = AppSettings;
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
///<reference path="../../Resources/Gear/GearCollectionResource.ts"/>
///<reference path="../../AppState.ts"/>
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
                    function GearCollection(gearCollectionResource) {
                        this.Id = -1;
                        this.Name = "";
                        this.Note = "";
                        this.GearSystems = [];
                        this.GearItems = [];
                        if (gearCollectionResource) {
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
                        }
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
                    GearCollection.prototype.getGearSystemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearItems[i];
                            count += gearSystemEntry.Count;
                        }
                        return count;
                    };
                    GearCollection.prototype.getGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
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
                    GearCollection.prototype.getCostPerGramInUSDP = function () {
                        var costInUSDP = this.getCostInUSDP();
                        var weightInGrams = this.getWeightInGrams();
                        return 0 == weightInGrams
                            ? costInUSDP
                            : costInUSDP / weightInGrams;
                    };
                    GearCollection.prototype.getCostPerUnitInCurrency = function () {
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                        var weightInUnits = Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units);
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
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
                    GearCollection.prototype.getGearSystemEntryById = function (gearSystemId) {
                        var idx = this.getGearSystemEntryIndexById(gearSystemId);
                        return idx < 0 ? null : this.GearSystems[idx];
                    };
                    // TODO: add/remove system entries
                    GearCollection.prototype.getGearItemEntryIndexById = function (gearItemId) {
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            if (gearItemEntry.GearItemId == gearItemId) {
                                return i;
                            }
                        }
                        return -1;
                    };
                    GearCollection.prototype.getGearItemEntryById = function (gearItemId) {
                        var idx = this.getGearItemEntryIndexById(gearItemId);
                        return idx < 0 ? null : this.GearItems[idx];
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
                    GearCollectionEntry.prototype.getCostInUSDP = function () {
                        var gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById(this.GearCollectionId);
                        if (!gearCollection) {
                            return 0;
                        }
                        return this.Count * gearCollection.getCostInUSDP();
                    };
                    return GearCollectionEntry;
                })();
                Gear.GearCollectionEntry = GearCollectionEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
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
///<reference path="../../Resources/Gear/GearSystemResource.ts"/>
///<reference path="../../AppState.ts"/>
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
                    function GearSystem(gearSystemResource) {
                        this.Id = -1;
                        this.Name = "";
                        this.Note = "";
                        this.GearItems = [];
                        if (gearSystemResource) {
                            this.Id = gearSystemResource.Id;
                            this.Name = gearSystemResource.Name;
                            this.Note = gearSystemResource.Note;
                            for (var i = 0; i < gearSystemResource.GearItems.length; ++i) {
                                var gearItemEntry = gearSystemResource.GearItems[i];
                                this.GearItems.push(new Gear.GearItemEntry(gearItemEntry.GearItemId, gearItemEntry.Count, gearItemEntry.IsPacked));
                            }
                        }
                    }
                    GearSystem.prototype.getGearItemCount = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
                    GearSystem.prototype.getWeightInGrams = function () {
                        var weightInGrams = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            weightInGrams += gearItemEntry.getWeightInGrams();
                        }
                        return weightInGrams;
                    };
                    GearSystem.prototype.getWeightInUnits = function () {
                        return Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units);
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
                    GearSystem.prototype.getCostPerGramInUSDP = function () {
                        var costInUSDP = this.getCostInUSDP();
                        var weightInGrams = this.getWeightInGrams();
                        return 0 == weightInGrams
                            ? costInUSDP
                            : costInUSDP / weightInGrams;
                    };
                    GearSystem.prototype.getCostPerUnitInCurrency = function () {
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.getCostInUSDP(), Mockup.AppState.getInstance().getAppSettings().Currency);
                        var weightInUnits = Mockup.convertGramsToUnits(this.getWeightInGrams(), Mockup.AppState.getInstance().getAppSettings().Units);
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
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
                    GearSystem.prototype.getGearItemEntryById = function (gearItemId) {
                        var idx = this.getGearItemEntryIndexById(gearItemId);
                        return idx < 0 ? null : this.GearItems[idx];
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
                    GearSystemEntry.prototype.getCostInUSDP = function () {
                        var gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById(this.GearSystemId);
                        if (!gearSystem) {
                            return 0;
                        }
                        return this.Count * gearSystem.getCostInUSDP();
                    };
                    return GearSystemEntry;
                })();
                Gear.GearSystemEntry = GearSystemEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="Models/Gear/GearCollection.ts" />
///<reference path="Models/Gear/GearItem.ts" />
///<reference path="Models/Gear/GearSystem.ts" />
///<reference path="Resources/Gear/GearCollectionResource.ts" />
///<reference path="Resources/Gear/GearItemResource.ts" />
///<reference path="Resources/Gear/GearSystemResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var GearState = (function () {
            function GearState() {
            }
            GearState.prototype.getGearItems = function () {
                return this._gearItems;
            };
            GearState.prototype.loadGearItems = function (gearItemsResource) {
                if (this._gearItems) {
                    throw new Error("Gear items already loaded!");
                }
                this._gearItems = [];
                for (var i = 0; i < gearItemsResource.length; ++i) {
                    this._gearItems.push(new Mockup.Models.Gear.GearItem(gearItemsResource[i]));
                }
            };
            GearState.prototype.getNextGearItemId = function () {
                // TODO: write this
                return -1;
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
            GearState.prototype.addGearItem = function (gearItem) {
                if (gearItem.Id < 0) {
                    gearItem.Id = this.getNextGearItemId();
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
            GearState.prototype.getGearSystems = function () {
                return this._gearSystems;
            };
            GearState.prototype.loadGearSystems = function (gearSystemsResource) {
                if (this._gearSystems) {
                    throw new Error("Gear systems already loaded!");
                }
                this._gearSystems = [];
                for (var i = 0; i < gearSystemsResource.length; ++i) {
                    this._gearSystems.push(new Mockup.Models.Gear.GearSystem(gearSystemsResource[i]));
                }
            };
            GearState.prototype.getNextGearSystemId = function () {
                // TODO: write this
                return -1;
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
            GearState.prototype.addGearSystem = function (gearSystem) {
                if (gearSystem.Id < 0) {
                    gearSystem.Id = this.getNextGearSystemId();
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
            GearState.prototype.getGearCollections = function () {
                return this._gearCollections;
            };
            GearState.prototype.loadGearCollections = function (gearCollectionsResource) {
                if (this._gearCollections) {
                    throw new Error("Gear collections already loaded!");
                }
                this._gearCollections = [];
                for (var i = 0; i < gearCollectionsResource.length; ++i) {
                    this._gearCollections.push(new Mockup.Models.Gear.GearCollection(gearCollectionsResource[i]));
                }
            };
            GearState.prototype.getNextGearCollectionId = function () {
                // TODO: write this
                return -1;
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
            GearState.prototype.addGearCollection = function (gearCollection) {
                if (gearCollection.Id < 0) {
                    gearCollection.Id = this.getNextGearCollectionId();
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
            /* Load/Save */
            GearState.prototype.loadFromDevice = function () {
                // TODO: load from the resources here and return a promise
            };
            GearState.prototype.saveToDevice = function () {
                // TODO: don't do anything here, just return a promise
            };
            return GearState;
        })();
        Mockup.GearState = GearState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
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
                    }
                    return Meal;
                })();
                Meals.Meal = Meal;
                var MealEntry = (function () {
                    function MealEntry() {
                    }
                    return MealEntry;
                })();
                Meals.MealEntry = MealEntry;
            })(Meals = Models.Meals || (Models.Meals = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="Models/Meals/Meal.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var MealState = (function () {
            function MealState() {
            }
            MealState.prototype.getMeals = function () {
                return this._meals;
            };
            MealState.prototype.loadMeals = function (mealResource) {
                if (this._meals) {
                    throw new Error("Meals already loaded!");
                }
                this._meals = [];
                for (var i = 0; i < mealResource.length; ++i) {
                }
            };
            MealState.prototype.getNextMealId = function () {
                // TODO: write this
                return -1;
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
            MealState.prototype.addMeal = function (meal) {
                if (meal.Id < 0) {
                    meal.Id = this.getNextMealId();
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
            /* Load/Save */
            MealState.prototype.loadFromDevice = function () {
                // TODO: load from the resources here and return a promise
            };
            MealState.prototype.saveToDevice = function () {
                // TODO: don't do anything here, just return a promise
            };
            return MealState;
        })();
        Mockup.MealState = MealState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
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
                        this.Id = -1;
                    }
                    return TripItinerary;
                })();
                Trips.TripItinerary = TripItinerary;
                var TripItineraryEntry = (function () {
                    function TripItineraryEntry() {
                    }
                    return TripItineraryEntry;
                })();
                Trips.TripItineraryEntry = TripItineraryEntry;
            })(Trips = Models.Trips || (Models.Trips = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
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
                    }
                    return TripPlan;
                })();
                Trips.TripPlan = TripPlan;
                var TripPlanEntry = (function () {
                    function TripPlanEntry() {
                    }
                    return TripPlanEntry;
                })();
                Trips.TripPlanEntry = TripPlanEntry;
            })(Trips = Models.Trips || (Models.Trips = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="Models/Trips/TripItinerary.ts" />
///<reference path="Models/Trips/TripPlan.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var TripState = (function () {
            function TripState() {
            }
            TripState.prototype.getTripItineraries = function () {
                return this._tripItineraries;
            };
            TripState.prototype.loadTripItineraries = function (tripItineraryResource) {
                if (this._tripItineraries) {
                    throw new Error("Trip itineraries already loaded!");
                }
                this._tripItineraries = [];
                for (var i = 0; i < tripItineraryResource.length; ++i) {
                }
            };
            TripState.prototype.getNextTripItineraryId = function () {
                // TODO: write this
                return -1;
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
            TripState.prototype.addTripItinerary = function (tripItinerary) {
                if (tripItinerary.Id < 0) {
                    tripItinerary.Id = this.getNextTripItineraryId();
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
            TripState.prototype.getTripPlans = function () {
                return this._tripPlans;
            };
            TripState.prototype.loadTripPlans = function (tripPlanResource) {
                if (this._tripPlans) {
                    throw new Error("Trip plans already loaded!");
                }
                this._tripPlans = [];
                for (var i = 0; i < tripPlanResource.length; ++i) {
                }
            };
            TripState.prototype.getNextTripPlanId = function () {
                // TODO: write this
                return -1;
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
            TripState.prototype.addTripPlan = function (tripPlan) {
                if (tripPlan.Id < 0) {
                    tripPlan.Id = this.getNextTripPlanId();
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
            /* Load/Save */
            TripState.prototype.loadFromDevice = function () {
                // TODO: load from the resources here and return a promise
            };
            TripState.prototype.saveToDevice = function () {
                // TODO: don't do anything here, just return a promise
            };
            return TripState;
        })();
        Mockup.TripState = TripState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="Models/Personal/UserInformation.ts" />
///<reference path="Models/AppSettings.ts" />
///<reference path="Resources/Personal/UserInformationResource.ts" />
///<reference path="Resources/AppSettingsResource.ts" />
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
            AppState.prototype.loadAppSettings = function (appSettingsResource) {
                if (this._appSettings) {
                    throw new Error("Application settings already loaded!");
                }
                this._appSettings = new Mockup.Models.AppSettings(appSettingsResource);
            };
            AppState.prototype.getUserInformation = function () {
                return this._userInformation;
            };
            AppState.prototype.loadUserInformation = function (userInfoResource) {
                if (this._userInformation) {
                    throw new Error("User information already loaded!");
                }
                this._userInformation = new Mockup.Models.Personal.UserInformation(userInfoResource);
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
            /* Load/Save */
            AppState.prototype.loadFromDevice = function () {
                // TODO: load from the resources here and return a promise
                this._gearState.loadFromDevice();
                this._mealState.loadFromDevice();
                this._tripState.loadFromDevice();
            };
            AppState.prototype.saveToDevice = function () {
                // TODO: don't do anything here, just return a promise
                this._gearState.saveToDevice();
                this._mealState.saveToDevice();
                this._tripState.saveToDevice();
            };
            /* Import/Export */
            AppState.prototype.importFromCloudStorage = function (cloudStorage) {
                // TODO: don't do anything here, just return a promise
            };
            AppState.prototype.exportToCloudStorage = function (cloudStorage) {
                // TODO: don't do anything here, just return a promise
            };
            /* Singleton */
            AppState._instance = new AppState();
            return AppState;
        })();
        Mockup.AppState = AppState;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../Resources/Gear/GearItemResource.ts"/>
///<reference path="../../AppState.ts"/>
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
                    function GearItem(gearItemResource) {
                        this.Id = -1;
                        this.Name = "";
                        this.Url = "";
                        this.Make = "";
                        this.Model = "";
                        this.Carried = "Carried";
                        this.WeightInGrams = 0;
                        this.CostInUSDP = 0;
                        this.IsConsumable = false;
                        this.ConsumedPerDay = 0;
                        this.Note = "";
                        if (gearItemResource) {
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
                        }
                    }
                    GearItem.prototype.getCostPerGramInUSDP = function () {
                        return 0 == this.WeightInGrams
                            ? this.CostInUSDP
                            : this.CostInUSDP / this.WeightInGrams;
                    };
                    GearItem.prototype.getCostPerUnitInCurrency = function () {
                        var costInCurrency = Mockup.convertUSDPToCurrency(this.CostInUSDP, Mockup.AppState.getInstance().getAppSettings().Currency);
                        var weightInUnits = Mockup.convertGramsToUnits(this.WeightInGrams, Mockup.AppState.getInstance().getAppSettings().Units);
                        return 0 == weightInUnits
                            ? costInCurrency
                            : costInCurrency / weightInUnits;
                    };
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
                    GearItemEntry.prototype.getWeightInGrams = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
                        if (!gearItem) {
                            return 0;
                        }
                        return this.Count * gearItem.WeightInGrams;
                    };
                    GearItemEntry.prototype.getCostInUSDP = function () {
                        var gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById(this.GearItemId);
                        if (!gearItem) {
                            return 0;
                        }
                        return this.Count * gearItem.CostInUSDP;
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
                function AppCtrl($scope, $location, $mdSidenav, appSettingsService, userInformationService, gearItemService, gearSystemService, gearCollectionService) {
                    $scope.appStateLoading = true;
                    Mockup.AppState.getInstance().loadFromDevice() /*.$promise.then(
                        $scope.appStateLoading = false;
                    )*/;
                    /* TODO: move all of this into the AppState.loadFromDevice() call */
                    // load the application settings
                    appSettingsService.get().$promise.then(function (appSettingsResource) {
                        Mockup.AppState.getInstance().loadAppSettings(appSettingsResource);
                    });
                    // load the user's personal information
                    userInformationService.get().$promise.then(function (userInfoResource) {
                        Mockup.AppState.getInstance().loadUserInformation(userInfoResource);
                    });
                    $scope.getUserInfo = function () {
                        return Mockup.AppState.getInstance().getUserInformation();
                    };
                    // load the gear items
                    gearItemService.query().$promise.then(function (gearItemsResource) {
                        Mockup.AppState.getInstance().getGearState().loadGearItems(gearItemsResource);
                    });
                    $scope.getGearItems = function () {
                        return Mockup.AppState.getInstance().getGearState().getGearItems();
                    };
                    $scope.getGearItemById = function (gearItemId) {
                        return Mockup.AppState.getInstance().getGearState().getGearItemById(gearItemId);
                    };
                    // load the gear systems
                    gearSystemService.query().$promise.then(function (gearSystemsResource) {
                        Mockup.AppState.getInstance().getGearState().loadGearSystems(gearSystemsResource);
                    });
                    $scope.getGearSystems = function () {
                        return Mockup.AppState.getInstance().getGearState().getGearSystems();
                    };
                    $scope.getGearSystemById = function (gearSystemId) {
                        return Mockup.AppState.getInstance().getGearState().getGearSystemById(gearSystemId);
                    };
                    // load the gear collections
                    gearCollectionService.query().$promise.then(function (gearCollectionsResource) {
                        Mockup.AppState.getInstance().getGearState().loadGearCollections(gearCollectionsResource);
                    });
                    $scope.getGearCollections = function () {
                        return Mockup.AppState.getInstance().getGearState().getGearCollections();
                    };
                    $scope.getGearCollectionById = function (gearCollectionId) {
                        return Mockup.AppState.getInstance().getGearState().getGearCollectionById(gearCollectionId);
                    };
                    // load the meals
                    $scope.getMeals = function () {
                        return [];
                    };
                    $scope.getMealById = function (mealId) {
                        return null;
                    };
                    // load the trip itineraries
                    $scope.getTripItineraries = function () {
                        return [];
                    };
                    $scope.getTripItineraryById = function (tripItineraryId) {
                        return null;
                    };
                    // load the trip plans
                    $scope.getTripPlans = function () {
                        return [];
                    };
                    $scope.getTripPlanById = function (tripPlanId) {
                        return null;
                    };
                    /* TODO: end move all of this into the AppState.loadFromDevice() call */
                    $scope.getUnitsWeightString = function () {
                        return Mockup.getUnitsWeightString(Mockup.AppState.getInstance().getAppSettings().Units);
                    };
                    $scope.getUnitsLengthString = function () {
                        return Mockup.getUnitsLengthString(Mockup.AppState.getInstance().getAppSettings().Units);
                    };
                    $scope.getCurrencyString = function () {
                        return Mockup.getCurrencyString(Mockup.AppState.getInstance().getAppSettings().Currency);
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
            })();
            Controllers.AppCtrl = AppCtrl;
            AppCtrl.$inject = ["$scope", "$location", "$mdSidenav",
                "AppSettingsService", "UserInformationService",
                "GearItemService", "GearSystemService", "GearCollectionService" /*
                MealService,
                TripItineraryService, TripPlanService*/
            ];
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
                            $scope.gearCollection = new Mockup.Models.Gear.GearCollection();
                            $scope.showAddGearItem = function (event) {
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
                            $scope.showAddGearSystem = function (event) {
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
                            $scope.addCollection = function (gearCollection) {
                                $scope.gearCollection = angular.copy(gearCollection);
                                $scope.gearCollection.Id = Mockup.AppState.getInstance().getGearState().addGearCollection($scope.gearCollection);
                                var addToast = $mdToast.simple()
                                    .content("Added gear collection: " + $scope.gearCollection.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoAddToast = $mdToast.simple()
                                    .content("Removed gear collection: " + $scope.gearCollection.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/collections");
                                $mdToast.show(addToast).then(function () {
                                    Mockup.AppState.getInstance().getGearState().deleteGearCollection($scope.gearCollection);
                                    $mdToast.show(undoAddToast);
                                });
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
                            $scope.gearCollection = Mockup.AppState.getInstance().getGearState().getGearCollectionById($routeParams.gearCollectionId);
                            if (null == $scope.gearCollection) {
                                alert("The gear collection does not exist!");
                                $location.path("/gear/collections");
                                return;
                            }
                            $scope.showAddGearItem = function (event) {
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
                            $scope.showAddGearSystem = function (event) {
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
                            $scope.showDeleteConfirm = function (event) {
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
                                        $mdToast.show(deleteToast).then(function () {
                                            // TODO: this does *not* restore the collection to its containers
                                            // and it should probably do so... but how?
                                            Mockup.AppState.getInstance().getGearState().addGearCollection($scope.gearCollection);
                                            $mdToast.show(undoDeleteToast);
                                            $location.path("/gear/collections/" + $scope.gearCollection.Id);
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
                            $scope.showWhatIsGearCollection = function (event) {
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
                            $scope.addItem = function (gearItem) {
                                $scope.gearItem = angular.copy(gearItem);
                                $scope.gearItem.Id = Mockup.AppState.getInstance().getGearState().addGearItem($scope.gearItem);
                                var addToast = $mdToast.simple()
                                    .content("Added gear item: " + $scope.gearItem.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoAddToast = $mdToast.simple()
                                    .content("Removed gear item: " + $scope.gearItem.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/items");
                                $mdToast.show(addToast).then(function () {
                                    Mockup.AppState.getInstance().getGearState().deleteGearItem($scope.gearItem);
                                    $mdToast.show(undoAddToast);
                                });
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
                            $scope.gearItem = Mockup.AppState.getInstance().getGearState().getGearItemById($routeParams.gearItemId);
                            if (null == $scope.gearItem) {
                                alert("The gear item does not exist!");
                                $location.path("/gear/items");
                                return;
                            }
                            $scope.showDeleteConfirm = function (event) {
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
                                        $mdToast.show(deleteToast).then(function () {
                                            // TODO: this does *not* restore the item to its containers
                                            // and it should probably do so... but how?
                                            Mockup.AppState.getInstance().getGearState().addGearItem($scope.gearItem);
                                            $mdToast.show(undoDeleteToast);
                                            $location.path("/gear/items/" + $scope.gearItem.Id);
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
                            $scope.showWhatIsGearItem = function (event) {
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
                            $scope.gearSystem = new Mockup.Models.Gear.GearSystem();
                            $scope.showAddGearItem = function (event) {
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
                            $scope.addSystem = function (gearSystem) {
                                $scope.gearSystem = angular.copy(gearSystem);
                                $scope.gearSystem.Id = Mockup.AppState.getInstance().getGearState().addGearSystem($scope.gearSystem);
                                var addToast = $mdToast.simple()
                                    .content("Added gear system: " + $scope.gearSystem.Name)
                                    .action("Undo")
                                    .position("bottom left");
                                var undoAddToast = $mdToast.simple()
                                    .content("Removed gear system: " + $scope.gearSystem.Name)
                                    .action("OK")
                                    .position("bottom left");
                                $location.path("/gear/systems");
                                $mdToast.show(addToast).then(function () {
                                    Mockup.AppState.getInstance().getGearState().deleteGearSystem($scope.gearSystem);
                                    $mdToast.show(undoAddToast);
                                });
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
                            $scope.gearSystem = Mockup.AppState.getInstance().getGearState().getGearSystemById($routeParams.gearSystemId);
                            if (null == $scope.gearSystem) {
                                alert("The gear system does not exist!");
                                $location.path("/gear/systems");
                                return;
                            }
                            $scope.showAddGearItem = function (event) {
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
                            $scope.showDeleteConfirm = function (event) {
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
                                        $mdToast.show(deleteToast).then(function () {
                                            // TODO: this does *not* restore the system to its containers
                                            // and it should probably do so... but how?
                                            Mockup.AppState.getInstance().getGearState().addGearSystem($scope.gearSystem);
                                            $mdToast.show(undoDeleteToast);
                                            $location.path("/gear/systems/" + $scope.gearSystem.Id);
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
                            $scope.showWhatIsGearSystem = function (event) {
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
                        $scope.showWhatIsMeal = function (event) {
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
                            $scope.showWhatIsTripItinerary = function (event) {
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
                    function UserInformationCtrl($scope, $mdDialog) {
                        $scope.showWhatIsPersonal = function (event) {
                            $mdDialog.show({
                                controller: Personal.WhatIsPersonalDlgCtrl,
                                templateUrl: "content/partials/personal/what.html",
                                parent: angular.element(document.body),
                                targetEvent: event
                            });
                        };
                    }
                    return UserInformationCtrl;
                })();
                Personal.UserInformationCtrl = UserInformationCtrl;
                UserInformationCtrl.$inject = ["$scope", "$mdDialog"];
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
                function AppSettingsCtrl($scope) {
                    $scope.getAppSettings = function () {
                        return Mockup.AppState.getInstance().getAppSettings();
                    };
                }
                return AppSettingsCtrl;
            })();
            Controllers.AppSettingsCtrl = AppSettingsCtrl;
            AppSettingsCtrl.$inject = ["$scope"];
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
                    .when("/trips/itineraries", {
                    templateUrl: "content/partials/trips/itineraries/itineraries.html",
                    controller: "TripItinerariesCtrl",
                    title: "Trip Itineraries"
                })
                    .when("/trips/plans", {
                    templateUrl: "content/partials/trips/plans/plans.html",
                    controller: "TripPlansCtrl",
                    title: "Trip Plans"
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
///<reference path="Controllers/Trips/Itineraries/TripItinerariesCtrl.ts" />
///<reference path="Controllers/Trips/Plans/TripPlansCtrl.ts" />
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
        mockupApp.controller("TripItinerariesCtrl", Mockup.Controllers.Trips.Itineraries.TripItinerariesCtrl);
        mockupApp.controller("TripPlansCtrl", Mockup.Controllers.Trips.Plans.TripPlansCtrl);
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
                            $scope.getGearItems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.isSelected = function (gearItem) {
                                return $scope.gearCollection.getGearItemEntryIndexById(gearItem.Id) >= 0;
                            };
                            $scope.toggle = function (gearItem) {
                                var idx = $scope.gearCollection.getGearItemEntryIndexById(gearItem.Id);
                                if (idx < 0) {
                                    $scope.gearCollection.GearItems.push(new Mockup.Models.Gear.GearItemEntry(gearItem.Id));
                                }
                                else {
                                    $scope.gearCollection.GearItems.splice(idx, 1);
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
                            $scope.isSelected = function (gearSystem) {
                                return $scope.gearCollection.getGearSystemEntryIndexById(gearSystem.Id) >= 0;
                            };
                            $scope.toggle = function (gearSystem) {
                                var idx = $scope.gearCollection.getGearSystemEntryIndexById(gearSystem.Id);
                                if (idx < 0) {
                                    $scope.gearCollection.GearSystems.push(new Mockup.Models.Gear.GearSystemEntry(gearSystem.Id));
                                }
                                else {
                                    $scope.gearCollection.GearSystems.splice(idx, 1);
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
                            $scope.getGearItems = function () {
                                return Mockup.AppState.getInstance().getGearState().getGearItems();
                            };
                            $scope.close = function () {
                                $mdDialog.hide();
                            };
                            $scope.isSelected = function (gearItem) {
                                return $scope.gearSystem.getGearItemEntryIndexById(gearItem.Id) >= 0;
                            };
                            $scope.toggle = function (gearItem) {
                                var idx = $scope.gearSystem.getGearItemEntryIndexById(gearItem.Id);
                                if (idx < 0) {
                                    $scope.gearSystem.GearItems.push(new Mockup.Models.Gear.GearItemEntry(gearItem.Id));
                                }
                                else {
                                    $scope.gearSystem.GearItems.splice(idx, 1);
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