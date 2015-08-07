///<reference path="../scripts/typings/angularjs/angular.d.ts" />

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

    // inject controllers
}