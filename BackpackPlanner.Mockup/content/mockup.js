///<reference path="../Resources/AppSettingsResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            "use strict";
            (function (Units) {
                Units[Units["Imperial"] = 0] = "Imperial";
                Units[Units["Metric"] = 1] = "Metric";
            })(Models.Units || (Models.Units = {}));
            var Units = Models.Units;
            var AppSettings = (function () {
                function AppSettings(appSettingsResource) {
                    this.Units = Units.Imperial;
                    if (appSettingsResource) {
                        this.Units = appSettingsResource.Units;
                    }
                }
                return AppSettings;
            })();
            Models.AppSettings = AppSettings;
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
///<reference path="../Resources/UserInformationResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            "use strict";
            (function (Sex) {
                Sex[Sex["NotSpecified"] = 0] = "NotSpecified";
                Sex[Sex["Male"] = 1] = "Male";
                Sex[Sex["Female"] = 2] = "Female";
            })(Models.Sex || (Models.Sex = {}));
            var Sex = Models.Sex;
            var UserInformation = (function () {
                function UserInformation(userInfoResource) {
                    this.FirstName = "";
                    this.LastName = "";
                    this.BirthDate = new Date();
                    this.Sex = Sex.NotSpecified;
                    this.HeightInInches = 0;
                    this.WeightInOunces = 0;
                    if (userInfoResource) {
                        this.FirstName = userInfoResource.FirstName;
                        this.LastName = userInfoResource.LastName;
                        this.BirthDate = userInfoResource.BirthDate;
                        this.Sex = userInfoResource.Sex;
                        this.HeightInInches = userInfoResource.HeightInInches;
                        this.WeightInOunces = userInfoResource.WeightInOunces;
                    }
                }
                return UserInformation;
            })();
            Models.UserInformation = UserInformation;
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../Models/UserInformation.ts" />
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
///<reference path="../../Resources/Gear/GearItemResource.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Models;
        (function (Models) {
            var Gear;
            (function (Gear) {
                "use strict";
                (function (GearCarried) {
                    GearCarried[GearCarried["NotCarried"] = 0] = "NotCarried";
                    GearCarried[GearCarried["Carried"] = 1] = "Carried";
                    GearCarried[GearCarried["Worn"] = 2] = "Worn";
                })(Gear.GearCarried || (Gear.GearCarried = {}));
                var GearCarried = Gear.GearCarried;
                var GearItem = (function () {
                    function GearItem(gearItemResource) {
                        this.Id = -1;
                        this.Name = "";
                        this.Url = "";
                        this.Make = "";
                        this.Model = "";
                        this.Carried = GearCarried.Carried;
                        this.WeightInOunces = 0;
                        this.CostInUSD = 0;
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
                            this.WeightInOunces = gearItemResource.WeightInOunces;
                            this.CostInUSD = gearItemResource.CostInUSD;
                            this.IsConsumable = gearItemResource.IsConsumable;
                            this.ConsumedPerDay = gearItemResource.ConsumedPerDay;
                            this.Note = gearItemResource.Note;
                        }
                    }
                    GearItem.prototype.CarriedAsString = function () {
                        return GearCarried[this.Carried];
                    };
                    return GearItem;
                })();
                Gear.GearItem = GearItem;
                var GearItemEntry = (function () {
                    function GearItemEntry(gearItemId) {
                        this.GearItemId = -1;
                        this.Count = 1;
                        this.IsPacked = false;
                        this.GearItemId = gearItemId;
                    }
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
///<reference path="../../AppManager.ts" />
///<reference path="../../Resources/Gear/GearSystemResource.ts"/>
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
                            this.GearItems = gearSystemResource.GearItems;
                        }
                    }
                    GearSystem.prototype.getNumberOfItems = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
                    GearSystem.prototype.getWeightInOunces = function () {
                        var weightInOunces = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            var gearItem = Mockup.AppManager.getInstance().getGearItemById(gearItemEntry.GearItemId);
                            if (null == gearItem) {
                                continue;
                            }
                            weightInOunces += gearItemEntry.Count * gearItem.WeightInOunces;
                        }
                        return weightInOunces;
                    };
                    GearSystem.prototype.getCostInUSD = function () {
                        var costInUSD = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            var gearItem = Mockup.AppManager.getInstance().getGearItemById(gearItemEntry.GearItemId);
                            if (null == gearItem) {
                                continue;
                            }
                            costInUSD += gearItemEntry.Count * gearItem.CostInUSD;
                        }
                        return costInUSD;
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
                    function GearSystemEntry(gearSystemId) {
                        this.GearSystemId = -1;
                        this.Count = 1;
                        this.IsPacked = false;
                        this.GearSystemId = gearSystemId;
                    }
                    return GearSystemEntry;
                })();
                Gear.GearSystemEntry = GearSystemEntry;
            })(Gear = Models.Gear || (Models.Gear = {}));
        })(Models = Mockup.Models || (Mockup.Models = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="Models/AppSettings.ts" />
///<reference path="Models/UserInformation.ts" />
///<reference path="Models/Gear/GearCollection.ts" />
///<reference path="Models/Gear/GearItem.ts" />
///<reference path="Models/Gear/GearSystem.ts" />
///<reference path="Resources/AppSettingsResource.ts" />
///<reference path="Resources/UserInformationResource.ts" />
///<reference path="Resources/Gear/GearCollectionResource.ts" />
///<reference path="Resources/Gear/GearItemResource.ts" />
///<reference path="Resources/Gear/GearSystemResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        "use strict";
        var AppManager = (function () {
            function AppManager() {
                if (AppManager._instance) {
                    throw new Error("Error: AppManager already instantiated!");
                }
            }
            AppManager.getInstance = function () {
                return AppManager._instance;
            };
            AppManager.prototype.getAppSettings = function () {
                return this._appSettings;
            };
            AppManager.prototype.setAppSettings = function (appSettingsResource) {
                if (this._appSettings) {
                    throw new Error("Application settings already set!");
                }
                this._appSettings = new Mockup.Models.AppSettings(appSettingsResource);
            };
            AppManager.prototype.getUserInformation = function () {
                return this._userInformation;
            };
            AppManager.prototype.setUserInformation = function (userInfoResource) {
                if (this._userInformation) {
                    throw new Error("User information already set!");
                }
                this._userInformation = new Mockup.Models.UserInformation(userInfoResource);
            };
            AppManager.prototype.getGearItems = function () {
                return this._gearItems;
            };
            AppManager.prototype.setGearItems = function (gearItemsResource) {
                if (this._gearItems) {
                    throw new Error("Gear items already set!");
                }
                this._gearItems = [];
                for (var i = 0; i < gearItemsResource.length; ++i) {
                    this._gearItems.push(new Mockup.Models.Gear.GearItem(gearItemsResource[i]));
                }
            };
            AppManager.prototype.getNextGearItemId = function () {
                // TODO: write this
                return -1;
            };
            AppManager.prototype.getGearItemIndexById = function (gearItemId) {
                for (var i = 0; i < this._gearItems.length; ++i) {
                    var gearItem = this._gearItems[i];
                    if (gearItem.Id == gearItemId) {
                        return i;
                    }
                }
                return -1;
            };
            AppManager.prototype.getGearItemById = function (gearItemId) {
                var idx = this.getGearItemIndexById(gearItemId);
                return idx < 0 ? null : this._gearItems[idx];
            };
            AppManager.prototype.deleteGearItem = function (gearItem) {
                var idx = this.getGearItemIndexById(gearItem.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearItems.splice(idx, 1);
                // TODO: remove the item from the systems, collections, and trip plans it belongs to
                return true;
            };
            AppManager.prototype.getGearSystems = function () {
                return this._gearSystems;
            };
            AppManager.prototype.setGearSystems = function (gearSystemsResource) {
                if (this._gearSystems) {
                    throw new Error("Gear systems already set!");
                }
                this._gearSystems = [];
                for (var i = 0; i < gearSystemsResource.length; ++i) {
                    this._gearSystems.push(new Mockup.Models.Gear.GearSystem(gearSystemsResource[i]));
                }
            };
            AppManager.prototype.getNextGearSystemId = function () {
                // TODO: write this
                return -1;
            };
            AppManager.prototype.getGearSystemIndexById = function (gearSystemId) {
                for (var i = 0; i < this._gearSystems.length; ++i) {
                    var gearSystem = this._gearSystems[i];
                    if (gearSystem.Id == gearSystemId) {
                        return i;
                    }
                }
                return -1;
            };
            AppManager.prototype.getGearSystemById = function (gearSystemId) {
                var idx = this.getGearSystemIndexById(gearSystemId);
                return idx < 0 ? null : this._gearSystems[idx];
            };
            AppManager.prototype.deleteGearSystem = function (gearSystem) {
                var idx = this.getGearSystemIndexById(gearSystem.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearSystems.splice(idx, 1);
                // TODO: remove the system from the collections, and trip plans it belongs to
                return true;
            };
            AppManager.prototype.getGearCollections = function () {
                return this._gearCollections;
            };
            AppManager.prototype.setGearCollections = function (gearCollectionsResource) {
                if (this._gearCollections) {
                    throw new Error("Gear collections already set!");
                }
                this._gearCollections = [];
                for (var i = 0; i < gearCollectionsResource.length; ++i) {
                    this._gearCollections.push(new Mockup.Models.Gear.GearCollection(gearCollectionsResource[i]));
                }
            };
            AppManager.prototype.getNextGearCollectionId = function () {
                // TODO: write this
                return -1;
            };
            AppManager.prototype.getGearCollectionIndexById = function (gearCollectionId) {
                for (var i = 0; i < this._gearCollections.length; ++i) {
                    var gearCollection = this._gearCollections[i];
                    if (gearCollection.Id == gearCollectionId) {
                        return i;
                    }
                }
                return -1;
            };
            AppManager.prototype.getGearCollectionById = function (gearCollectionId) {
                var idx = this.getGearCollectionIndexById(gearCollectionId);
                return idx < 0 ? null : this._gearCollections[idx];
            };
            AppManager.prototype.deleteGearCollection = function (gearCollection) {
                var idx = this.getGearCollectionIndexById(gearCollection.Id);
                if (idx < 0) {
                    return false;
                }
                this._gearCollections.splice(idx, 1);
                // TODO: remove the collection from the trip plans it belongs to
                return true;
            };
            AppManager._instance = new AppManager();
            return AppManager;
        })();
        Mockup.AppManager = AppManager;
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../AppManager.ts" />
///<reference path="../../Resources/Gear/GearCollectionResource.ts"/>
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
                            this.GearSystems = gearCollectionResource.GearSystems;
                            this.GearItems = gearCollectionResource.GearItems;
                        }
                    }
                    GearCollection.prototype.getTotalNumberOfItems = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearSystems[i];
                            var gearSystem = Mockup.AppManager.getInstance().getGearSystemById(gearSystemEntry.GearSystemId);
                            count += gearSystemEntry.Count * gearSystem.getNumberOfItems();
                        }
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
                    GearCollection.prototype.getNumberOfSystems = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearSystems.length; ++i) {
                            var gearSystemEntry = this.GearItems[i];
                            count += gearSystemEntry.Count;
                        }
                        return count;
                    };
                    GearCollection.prototype.getNumberOfItems = function () {
                        var count = 0;
                        for (var i = 0; i < this.GearItems.length; ++i) {
                            var gearItemEntry = this.GearItems[i];
                            count += gearItemEntry.Count;
                        }
                        return count;
                    };
                    GearCollection.prototype.getWeightInOunces = function () {
                        var weightInOunces = 0;
                        // TODO: calculate this
                        return weightInOunces;
                    };
                    GearCollection.prototype.getGearCollectionCostInUSD = function () {
                        var costInUSD = 0;
                        // TODO: calculate this
                        return costInUSD;
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
                    GearCollection.prototype.getGearItemEntryById = function (gearItemId) {
                        var idx = this.getGearItemEntryIndexById(gearItemId);
                        return idx < 0 ? null : this.GearItems[idx];
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
                    GearCollection.prototype.getGearSystemmEntryById = function (gearSystemId) {
                        var idx = this.getGearSystemEntryIndexById(gearSystemId);
                        return idx < 0 ? null : this.GearSystems[idx];
                    };
                    return GearCollection;
                })();
                Gear.GearCollection = GearCollection;
                var GearCollectionEntry = (function () {
                    function GearCollectionEntry(gearCollectionId) {
                        this.GearCollectionId = -1;
                        this.Count = 1;
                        this.IsPacked = false;
                        this.GearCollectionId = gearCollectionId;
                    }
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
///<reference path="../../scripts/typings/angularjs/angular-resource.d.ts" />
///<reference path="../Resources/UserInformationResource.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Services;
        (function (Services) {
            "use strict";
            function userInformationServiceFactory($resource) {
                return $resource("data/user.json", {}, {
                    get: { method: "GET", isArray: false }
                });
            }
            Services.userInformationServiceFactory = userInformationServiceFactory;
        })(Services = Mockup.Services || (Mockup.Services = {}));
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
///<reference path="../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../Models/AppSettings.ts" />
///<reference path="../Models/UserInformation.ts" />
///<reference path="../Models/Gear/GearItem.ts" />
///<reference path="../Models/Gear/GearSystem.ts" />
///<reference path="../Models/Gear/GearCollection.ts" />
///<reference path="../Services/AppSettingsService.ts"/>
///<reference path="../Services/UserInformationService.ts"/>
///<reference path="../Services/gear/GearCollectionService.ts"/>
///<reference path="../Services/gear/GearItemService.ts"/>
///<reference path="../Services/gear/GearSystemService.ts"/>
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            "use strict";
            var AppCtrl = (function () {
                function AppCtrl($scope, $location, $mdSidenav, appSettingsService, userInformationService, gearItemService, gearSystemService, gearCollectionService) {
                    // load the application settings
                    $scope.appSettingsLoading = true;
                    appSettingsService.get().$promise.then(function (appSettingsResource) {
                        Mockup.AppManager.getInstance().setAppSettings(appSettingsResource);
                        $scope.appSettingsLoading = false;
                    });
                    $scope.getAppSettings = function () {
                        return Mockup.AppManager.getInstance().getAppSettings();
                    };
                    // load the user's personal information
                    $scope.userInfoLoading = true;
                    userInformationService.get().$promise.then(function (userInfoResource) {
                        Mockup.AppManager.getInstance().setUserInformation(userInfoResource);
                        $scope.userInfoLoading = false;
                    });
                    $scope.getUserInfo = function () {
                        return Mockup.AppManager.getInstance().getUserInformation();
                    };
                    // load the gear items
                    $scope.gearItemsLoading = true;
                    gearItemService.query().$promise.then(function (gearItemsResource) {
                        Mockup.AppManager.getInstance().setGearItems(gearItemsResource);
                        $scope.gearItemsLoading = false;
                    });
                    $scope.getGearItems = function () {
                        return Mockup.AppManager.getInstance().getGearItems();
                    };
                    $scope.getGearItemById = function (gearItemId) {
                        return Mockup.AppManager.getInstance().getGearItemById(gearItemId);
                    };
                    // load the gear systems
                    $scope.gearSystemsLoading = true;
                    gearSystemService.query().$promise.then(function (gearSystemsResource) {
                        Mockup.AppManager.getInstance().setGearSystems(gearSystemsResource);
                        $scope.gearSystemsLoading = false;
                    });
                    $scope.getGearSystems = function () {
                        return Mockup.AppManager.getInstance().getGearSystems();
                    };
                    $scope.getGearSystemById = function (gearSystemId) {
                        return Mockup.AppManager.getInstance().getGearSystemById(gearSystemId);
                    };
                    // load the gear collections
                    $scope.gearCollectionsLoading = true;
                    gearCollectionService.query().$promise.then(function (gearCollectionsResource) {
                        Mockup.AppManager.getInstance().setGearCollections(gearCollectionsResource);
                        $scope.gearCollectionsLoading = false;
                    });
                    $scope.getGearCollections = function () {
                        return Mockup.AppManager.getInstance().getGearCollections();
                    };
                    $scope.getGearCollectionById = function (gearCollectionId) {
                        return Mockup.AppManager.getInstance().getGearCollectionById(gearCollectionId);
                    };
                    // load the meals
                    $scope.mealsLoading = true;
                    $scope.getMeals = function () {
                        return [];
                    };
                    $scope.getMealById = function (mealId) {
                        return null;
                    };
                    $scope.mealsLoading = false;
                    // load the trip itineraries
                    $scope.tripItinerariesLoading = true;
                    $scope.getTripItineraries = function () {
                        return [];
                    };
                    $scope.getTripItineraryById = function (tripItineraryId) {
                        return null;
                    };
                    $scope.tripItinerariesLoading = false;
                    // load the trip plans
                    $scope.tripPlansLoading = true;
                    $scope.getTripPlans = function () {
                        return [];
                    };
                    $scope.getTripPlanById = function (tripPlanId) {
                        return null;
                    };
                    $scope.tripPlansLoading = false;
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
            AppCtrl.$inject = ["$scope", "$location", "$mdSidenav", "AppSettingsService", "UserInformationService",
                "GearItemService", "GearSystemService", "GearCollectionService"];
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
            var Gear;
            (function (Gear) {
                "use strict";
                var AddGearItemCtrl = (function () {
                    function AddGearItemCtrl($scope, $location, $mdToast) {
                        $scope.gearItem = new Mockup.Models.Gear.GearItem();
                        $scope.addItem = function (gearItem) {
                            $scope.gearItem = angular.copy(gearItem);
                            $scope.gearItem.Id = Mockup.AppManager.getInstance().getNextGearItemId();
                            Mockup.AppManager.getInstance().getGearItems().push($scope.gearItem);
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
                                Mockup.AppManager.getInstance().deleteGearItem($scope.gearItem);
                                $mdToast.show(undoAddToast);
                            });
                        };
                    }
                    return AddGearItemCtrl;
                })();
                Gear.AddGearItemCtrl = AddGearItemCtrl;
                AddGearItemCtrl.$inject = ["$scope", "$location", "$mdToast"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
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
            var Gear;
            (function (Gear) {
                "use strict";
                var GearItemCtrl = (function () {
                    function GearItemCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                        $scope.gearItem = Mockup.AppManager.getInstance().getGearItemById($routeParams.gearItemId);
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
                                    if (!Mockup.AppManager.getInstance().deleteGearItem($scope.gearItem)) {
                                        alert("Couldn't find the gear item to delete!");
                                        return;
                                    }
                                    $location.path("/gear/items");
                                    $mdToast.show(deleteToast).then(function () {
                                        // TODO: this does *not* restore the item to its containers
                                        // and it should probably do so... but how?
                                        Mockup.AppManager.getInstance().getGearItems().push($scope.gearItem);
                                        $mdToast.show(undoDeleteToast);
                                        $location.path("/gear/items/" + $scope.gearItem.Id);
                                    });
                                });
                            });
                        };
                    }
                    return GearItemCtrl;
                })();
                Gear.GearItemCtrl = GearItemCtrl;
                GearItemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearItemsCtrl = (function () {
                    function GearItemsCtrl($scope) {
                        $scope.orderBy = "Name";
                    }
                    return GearItemsCtrl;
                })();
                Gear.GearItemsCtrl = GearItemsCtrl;
                GearItemsCtrl.$inject = ["$scope"];
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
            var Gear;
            (function (Gear) {
                "use strict";
                var AddGearSystemCtrl = (function () {
                    function AddGearSystemCtrl($scope, $location, $mdDialog, $mdToast) {
                        $scope.gearSystem = new Mockup.Models.Gear.GearSystem();
                        $scope.showAddGearItem = function (event) {
                            $mdDialog.show({
                                controller: Gear.AddGearItemDlgCtrl,
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
                            $scope.gearSystem.Id = Mockup.AppManager.getInstance().getNextGearSystemId();
                            Mockup.AppManager.getInstance().getGearSystems().push($scope.gearSystem);
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
                                Mockup.AppManager.getInstance().deleteGearSystem($scope.gearSystem);
                                $mdToast.show(undoAddToast);
                            });
                        };
                    }
                    return AddGearSystemCtrl;
                })();
                Gear.AddGearSystemCtrl = AddGearSystemCtrl;
                AddGearSystemCtrl.$inject = ["$scope", "$location", "$mdDialog", "$mdToast"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
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
            var Gear;
            (function (Gear) {
                "use strict";
                var GearSystemCtrl = (function () {
                    function GearSystemCtrl($scope, $routeParams, $location, $mdDialog, $mdToast) {
                        $scope.gearSystem = Mockup.AppManager.getInstance().getGearSystemById($routeParams.gearSystemId);
                        if (null == $scope.gearSystem) {
                            alert("The gear system does not exist!");
                            $location.path("/gear/system");
                            return;
                        }
                        $scope.showAddGearItem = function (event) {
                            $mdDialog.show({
                                controller: Gear.AddGearItemDlgCtrl,
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
                                    if (!Mockup.AppManager.getInstance().deleteGearSystem($scope.gearSystem)) {
                                        alert("Couldn't find the gear system to delete!");
                                        return;
                                    }
                                    $location.path("/gear/systems");
                                    $mdToast.show(deleteToast).then(function () {
                                        // TODO: this does *not* restore the system to its containers
                                        // and it should probably do so... but how?
                                        Mockup.AppManager.getInstance().getGearSystems().push($scope.gearSystem);
                                        $mdToast.show(undoDeleteToast);
                                        $location.path("/gear/systems/" + $scope.gearSystem.Id);
                                    });
                                });
                            });
                        };
                    }
                    return GearSystemCtrl;
                })();
                Gear.GearSystemCtrl = GearSystemCtrl;
                GearSystemCtrl.$inject = ["$scope", "$routeParams", "$location", "$mdDialog", "$mdToast"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearSystemsCtrl = (function () {
                    function GearSystemsCtrl($scope) {
                        $scope.orderBy = "Name";
                    }
                    return GearSystemsCtrl;
                })();
                Gear.GearSystemsCtrl = GearSystemsCtrl;
                GearSystemsCtrl.$inject = ["$scope"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../AppCtrl.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var GearCollectionsCtrl = (function () {
                    function GearCollectionsCtrl($scope) {
                        $scope.orderBy = "Name";
                    }
                    return GearCollectionsCtrl;
                })();
                Gear.GearCollectionsCtrl = GearCollectionsCtrl;
                GearCollectionsCtrl.$inject = ["$scope"];
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
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
                    .when("/meals", {
                    templateUrl: "content/partials/meals.html",
                    title: "Meals"
                })
                    .when("/trip/itineraries", {
                    templateUrl: "content/partials/trip/itineraries/itineraries.html",
                    title: "Trip Itineraries"
                })
                    .when("/trip/plans", {
                    templateUrl: "content/partials/trip/plans/plans.html",
                    title: "Trip Plans"
                })
                    .when("/personal", {
                    templateUrl: "content/partials/personal.html",
                    title: "Personal Information"
                })
                    .when("/settings", {
                    templateUrl: "content/partials/settings.html",
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
///<reference path="Controllers/AppCtrl.ts" />
///<reference path="Controllers/Gear/AddGearItemCtrl.ts" />
///<reference path="Controllers/Gear/GearItemCtrl.ts" />
///<reference path="Controllers/Gear/GearItemsCtrl.ts" />
///<reference path="Controllers/Gear/AddGearSystemCtrl.ts" />
///<reference path="Controllers/Gear/GearSystemCtrl.ts" />
///<reference path="Controllers/Gear/GearSystemsCtrl.ts" />
///<reference path="Controllers/Gear/GearCollectionsCtrl.ts" />
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
        mockupApp.factory("UserInformationService", ["$resource", Mockup.Services.userInformationServiceFactory]);
        mockupApp.factory("GearItemService", ["$resource", Mockup.Services.Gear.gearItemServiceFactory]);
        mockupApp.factory("GearSystemService", ["$resource", Mockup.Services.Gear.gearSystemServiceFactory]);
        mockupApp.factory("GearCollectionService", ["$resource", Mockup.Services.Gear.gearCollectionServiceFactory]);
        // inject controllers
        mockupApp.controller("AppCtrl", Mockup.Controllers.AppCtrl);
        mockupApp.controller("GearItemCtrl", Mockup.Controllers.Gear.GearItemCtrl);
        mockupApp.controller("GearItemsCtrl", Mockup.Controllers.Gear.GearItemsCtrl);
        mockupApp.controller("AddGearItemCtrl", Mockup.Controllers.Gear.AddGearItemCtrl);
        mockupApp.controller("GearSystemCtrl", Mockup.Controllers.Gear.GearSystemCtrl);
        mockupApp.controller("GearSystemsCtrl", Mockup.Controllers.Gear.GearSystemsCtrl);
        mockupApp.controller("AddGearSystemCtrl", Mockup.Controllers.Gear.AddGearSystemCtrl);
        mockupApp.controller("GearCollectionsCtrl", Mockup.Controllers.Gear.GearCollectionsCtrl);
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
///<reference path="../../../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../scripts/typings/angular-material/angular-material.d.ts" />
///<reference path="../../AppManager.ts" />
var BackpackPlanner;
(function (BackpackPlanner) {
    var Mockup;
    (function (Mockup) {
        var Controllers;
        (function (Controllers) {
            var Gear;
            (function (Gear) {
                "use strict";
                var AddGearItemDlgCtrl = (function () {
                    function AddGearItemDlgCtrl($scope, $mdDialog, gearSystem) {
                        $scope.gearSystem = gearSystem;
                        $scope.orderBy = "Name";
                        $scope.getGearItems = function () {
                            return Mockup.AppManager.getInstance().getGearItems();
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
                Gear.AddGearItemDlgCtrl = AddGearItemDlgCtrl;
            })(Gear = Controllers.Gear || (Controllers.Gear = {}));
        })(Controllers = Mockup.Controllers || (Mockup.Controllers = {}));
    })(Mockup = BackpackPlanner.Mockup || (BackpackPlanner.Mockup = {}));
})(BackpackPlanner || (BackpackPlanner = {}));
//# sourceMappingURL=mockup.js.map