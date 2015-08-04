var mockupServices = angular.module("mockupServices", [ "ngResource" ]);

mockupServices.factory("AppSettings", ["$resource",
    function ($resource) {
        return $resource("data/settings.json", {}, {
            query: { method: "GET" }
        });
    }
]);

mockupServices.factory("UserInfo", ["$resource",
    function ($resource) {
        return $resource("data/user.json", {}, {
            query: { method: "GET" }
        });
    }
]);

mockupServices.factory("GearItem", ["$resource",
    function ($resource) {
        return $resource("data/gear/items.json", {}, {
            query: { method: "GET", isArray: true }
        });
    }
]);

mockupServices.factory("GearSystem", ["$resource",
    function ($resource) {
        return $resource("data/gear/systems.json", {}, {
            query: { method: "GET", isArray: true }
        });
    }
]);
