///<reference path="../scripts/typings/angularjs/angular.d.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export interface IRootScope extends ng.IScope {
        title: string;
    }

    export class RootScopeConfig {
        constructor($rootScope: IRootScope) {
            $rootScope.$on("$routeChangeSuccess",
                // TODO: find a way to make current/previous static typed
                (event : ng.IAngularEvent, currentRoute : any, previousRoute : any) => {
                    // change the app menu title when the route changes
                    $rootScope.title = currentRoute.title;
                });
        }
    };

    RootScopeConfig.$inject = ["$rootScope"];
}
