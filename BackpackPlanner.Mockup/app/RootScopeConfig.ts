///<reference path="../scripts/typings/angularjs/angular.d.ts" />
///<reference path="../scripts/typings/angularjs/angular-route.d.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export interface IRootScope extends ng.IScope {
        title: string;
    }

    export interface ICustomRoute extends ng.route.ICurrentRoute {
        title: string;
    }

    export class RootScopeConfig {
        constructor($rootScope: IRootScope) {
            $rootScope.$on("$routeChangeSuccess",
                (event: ng.IAngularEvent, currentRoute: ICustomRoute, previousRoute: ICustomRoute) => {
                    // change the app menu title when the route changes
                    $rootScope.title = currentRoute.title;
                });
        }
    };

    RootScopeConfig.$inject = ["$rootScope"];
}
