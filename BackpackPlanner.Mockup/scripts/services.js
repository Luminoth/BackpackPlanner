var mockupServices = angular.module("mockupServices", [ "ngResource" ]);

mockupServices.factory("UserInfo", ["$resource",
    function ($resource) {
        return $resource("data/user.json", {}, {
            query: { method: "GET" }
        });
    }
]);

mockupServices.factory("GearItem", ["$resource",
    function ($resource) {
        return $resource("data/gear/items/:gearItemId.json", {}, {
            query: { method: "GET", params: { gearItemId: "items" }, isArray: true }
        });
    }
]);
