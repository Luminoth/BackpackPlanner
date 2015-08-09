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

module BackpackPlanner.Mockup {
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
    mockupApp.run(RootScopeConfig);

    // configure routing
    mockupApp.config(RouteConfig);

    // configure the material design theme
    mockupApp.config(ThemeConfig);

    // inject services
    mockupApp.factory("AppSettingsService", ["$resource", Services.appSettingsServiceFactory]);
    mockupApp.factory("UserInformationService", ["$resource", Services.userInformationServiceFactory]);
    mockupApp.factory("GearItemService", ["$resource", Services.Gear.gearItemServiceFactory]);
    mockupApp.factory("GearSystemService", ["$resource", Services.Gear.gearSystemServiceFactory]);
    mockupApp.factory("GearCollectionService", ["$resource", Services.Gear.gearCollectionServiceFactory]);

    // inject controllers
    mockupApp.controller("AppCtrl", Controllers.AppCtrl);
    mockupApp.controller("GearItemCtrl", Controllers.Gear.GearItemCtrl);
    mockupApp.controller("GearItemsCtrl", Controllers.Gear.GearItemsCtrl);
    mockupApp.controller("AddGearItemCtrl", Controllers.Gear.AddGearItemCtrl);
    mockupApp.controller("GearSystemCtrl", Controllers.Gear.GearSystemCtrl);
    mockupApp.controller("GearSystemsCtrl", Controllers.Gear.GearSystemsCtrl);
    mockupApp.controller("AddGearSystemCtrl", Controllers.Gear.AddGearSystemCtrl);
    mockupApp.controller("GearCollectionsCtrl", Controllers.Gear.GearCollectionsCtrl);
}
