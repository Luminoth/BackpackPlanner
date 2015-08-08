///<reference path="../Scripts/typings/angular-material/angular-material.d.ts" />

module BackpackPlanner.Mockup {
    "use strict";

    export class ThemeConfig {
        constructor($mdThemingProvider: ng.material.IThemingProvider) {
            const primaryPalette = $mdThemingProvider.extendPalette("green", {
                "500": "668000",
                "A100": "501616",
                "contrastDefaultColor": "light"
            });

            const backgroundPalette = $mdThemingProvider.extendPalette("brown", {
                "500": "decd87"
            });

            const accentPalette = $mdThemingProvider.extendPalette("blue-grey", {
                //"500": "ffffff"
            });

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
    };

    ThemeConfig.$inject = ["$mdThemingProvider"];
}
