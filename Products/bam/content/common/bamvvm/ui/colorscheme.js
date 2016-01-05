/*
	Copyright © Bryan Apellanes 2015  
*/
var colorscheme = (function () {

    var defaultColorScheme = {
        Grays: {
            black: "#000",
            grayDarker: "#222",
            grayDark: "#333",
            gray: "#555",
            grayLight: "#999",
            grayLighter: "#eee",
            white: "#fff"
        },
        AccentColors: {
            blue: "#049cdb",
            blueDark: "#0064cd",
            green: "#46a546",
            red: "#9d261d",
            yellow: "#ffc40d",
            orange: "#f89406",
            pink: "#c3325f",
            purple: "#7a43b6"
        },
        Links: {
            linkColor: "#08c"
        },
        Tables: {
            tableBackground: "transparent",
            tableBackgroundAccent: "#f9f9f9",
            tableBackgroundHover: "#f5f5f5",
            tableBorder: "#ddd"
        },
        Buttons: {
            btnBorder: "#ccc",

            btnInfoBackground: "#5bc0de",
            btnInfoBackgroundHighlight: "#2f96b4",

            btnSuccessBackground: "#62c462",
            btnSuccessBackgroundHighlight: "#51a351",

            btnDangerBackground: "#ee5f5b",
            btnDangerBackgroundHighlight: "#bd362f",

            btnInverseBackground: "#444"
        },
        Navbar: {
            navbarBackgroundHighlight: "#ffffff",
            navbarText: "#777",
            navbarLinkColor: "#777",

            navbarInverseBackground: "#111111",
            navbarInverseBackgroundHighlight: "#222222",
            navbarInverseBorder: "#252525",
            navbarInverseSearchPlaceholderColor: "#ccc"
        },
        Pagination: {
            paginationBackground: "#fff",
            paginationBorder: "#ddd",
            paginationActiveBackground: "#f5f5f5"
        },
        StatesAndAlerts: {
            warningText: "#c09853",
            warningBackground: "#fcf8e3",

            errorText: "#b94a48",
            errorBackground: "#f2dede",

            successText: "#468847",
            successBackground: "#dff0d8",

            infoText: "#3a87ad",
            infoBackground: "#d9edf7"
        },
        TooltipsAndPopovers: {
            tooltipColor: "#fff",
            tooltipBackground: "#000",

            popoverBackground: "#fff",
            popoverArrowColor: "#fff"
        }
    };

    _.act("design", "getcolorscheme", null).done(function (r) {
        colorscheme = r;
    });
    return defaultColorScheme;
})();