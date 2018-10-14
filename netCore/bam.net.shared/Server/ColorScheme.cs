/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Bam.Net;

namespace Bam.Net.Server
{
    public class AccentColors
    {
        public string blue { get; set; }
        public string blueDark { get; set; }
        public string green { get; set; }
        public string red { get; set; }
        public string yellow { get; set; }
        public string orange { get; set; }
        public string pink { get; set; }
        public string purple { get; set; }
    }

    public class Grays
    {
        public string black { get; set; }
        public string grayDarker { get; set; }
        public string grayDark { get; set; }
        public string gray { get; set; }
        public string grayLight { get; set; }
        public string grayLighter { get; set; }
        public string white { get; set; }
    }

    public class Links
    {
        public string linkColor { get; set; }
    }

    public class Tables
    {
        public string tableBackground { get; set; }
        public string tableBackgroundAccent { get; set; }
        public string tableBackgroundHover { get; set; }
        public string tableBorder { get; set; }
    }

    public class Buttons
    {
        public string btnBorder { get; set; }

        public string btnInfoBackground { get; set; }
        public string btnInfoBackgroundHighlight { get; set; }

        public string btnSuccessBackground { get; set; }
        public string btnSuccessBackgroundHighlight { get; set; }

        public string btnDangerBackground { get; set; }
        public string btnDangerBackgroundHighlight { get; set; }

        public string btnInverseBackground { get; set; }
    }

    public class Navbar
    {
        public string navbarBackgroundHighlight { get; set; }
        public string navbarText { get; set; }
        public string navbarLinkColor { get; set; }

        public string navbarInverseBackground { get; set; }
        public string navbarInverseBackgroundHighlight { get; set; }
        public string navbarInverseBorder { get; set; }
        public string navbarInverseSearchPlaceholderColor { get; set; }
    }

    public class Pagination
    {
        public string paginationBackground { get; set; }
        public string paginationBorder { get; set; }
        public string paginationActiveBackground { get; set; }
    }

    public class StatesAndAlerts
    {
        public string warningText { get; set; }
        public string warningBackground { get; set; }

        public string errorText { get; set; }
        public string errorBackground { get; set; }

        public string successText { get; set; }
        public string successBackground { get; set; }

        public string infoText { get; set; }
        public string infoBackground { get; set; }
    }

    public class TooltipsAndPopovers
    {
        public string tooltipColor { get; set; }
        public string tooltipBackground { get; set; }

        public string popoverBackground { get; set; }
        public string popoverArrowColor { get; set; }
    }

    public class ColorScheme
    {

        public Grays Grays
        {
            get;
            set;
        }

        public AccentColors AccentColors
        {
            get;
            set;
        }

        public Links Links
        {
            get;
            set;
        }

        public Tables Tables
        {
            get;
            set;
        }


        public Buttons Buttons
        {
            get;
            set;
        }

        public Navbar Navbar
        {
            get;
            set;
        }

        public Pagination Pagination
        {
            get;
            set;
        }

        public StatesAndAlerts StatesAndAlerts
        {
            get;
            set;
        }

        public TooltipsAndPopovers TooltipsAndPopovers
        {
            get;
            set;
        }

        public static object Load(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path).FromJson<ColorScheme>();
            }
            else
            {
                return Default();
            }
        }

        public static dynamic Default()
        {
            return new
            {
                Grays = new
                {
                    black = "#000",
                    grayDarker = "#222",
                    grayDark = "#333",
                    gray = "#555",
                    grayLight = "#999",
                    grayLighter = "#eee",
                    white = "#fff"
                },
                AccentColors = new
                {
                    blue = "#049cdb",
                    blueDark = "#0064cd",
                    green = "#46a546",
                    red = "#9d261d",
                    yellow = "#ffc40d",
                    orange = "#f89406",
                    pink = "#c3325f",
                    purple = "#7a43b6"
                },
                Links = new
                {
                    linkColor = "#08c"
                },
                Tables = new
                {
                    tableBackground = "transparent",
                    tableBackgroundAccent = "#f9f9f9",
                    tableBackgroundHover = "#f5f5f5",
                    tableBorder = "#ddd"
                },
                Buttons = new
                {
                    btnBorder = "#ccc",

                    btnInfoBackground = "#5bc0de",
                    btnInfoBackgroundHighlight = "#2f96b4",

                    btnSuccessBackground = "#62c462",
                    btnSuccessBackgroundHighlight = "#51a351",

                    btnDangerBackground = "#ee5f5b",
                    btnDangerBackgroundHighlight = "#bd362f",

                    btnInverseBackground = "#444"
                },
                Navbar = new
                {
                    navbarBackgroundHighlight = "#ffffff",
                    navbarText = "#777",
                    navbarLinkColor = "#777",

                    navbarInverseBackground = "#111111",
                    navbarInverseBackgroundHighlight = "#222222",
                    navbarInverseBorder = "#252525",
                    navbarInverseSearchPlaceholderColor = "#ccc"
                },
                Pagination = new
                {
                    paginationBackground = "#fff",
                    paginationBorder = "#ddd",
                    paginationActiveBackground = "#f5f5f5"
                },
                StatesAndAlerts = new
                {
                    warningText = "#c09853",
                    warningBackground = "#fcf8e3",

                    errorText = "#b94a48",
                    errorBackground = "#f2dede",

                    successText = "#468847",
                    successBackground = "#dff0d8",

                    infoText = "#3a87ad",
                    infoBackground = "#d9edf7"
                },
                TooltipsAndPopovers = new
                {
                    tooltipColor = "#fff",
                    tooltipBackground = "#000",

                    popoverBackground = "#fff",
                    popoverArrowColor = "#fff"
                }
            };
        }
    }
}