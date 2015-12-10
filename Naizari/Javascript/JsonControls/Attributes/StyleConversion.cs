/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Naizari.Extensions;

namespace Naizari.Javascript.JsonControls
{
    public struct StyleConversion
    {
        

        public StyleConversion(HtmlTextWriterStyle style): this()
        {
            switch (style)
            {
                case HtmlTextWriterStyle.BackgroundColor:
                    this.CssStyleName = "background-color";
                    break;
                case HtmlTextWriterStyle.BackgroundImage:
                    this.CssStyleName = "background-image";
                    break;
                case HtmlTextWriterStyle.BorderCollapse:
                    this.CssStyleName = "border-collapse";
                    break;
                case HtmlTextWriterStyle.BorderColor:
                    this.CssStyleName = "border-color";
                    break;
                case HtmlTextWriterStyle.BorderStyle:
                    this.CssStyleName = "border-style";
                    break;
                case HtmlTextWriterStyle.BorderWidth:
                    this.CssStyleName = "border-width";
                    break;
                case HtmlTextWriterStyle.Color:
                    this.CssStyleName = "color";
                    break;
                case HtmlTextWriterStyle.Cursor:
                    this.CssStyleName = "cursor";
                    break;
                case HtmlTextWriterStyle.Direction:
                    this.CssStyleName = "direction";
                    break;
                case HtmlTextWriterStyle.Display:
                    this.CssStyleName = "display";
                    break;
                case HtmlTextWriterStyle.Filter:
                    this.CssStyleName = "filter";
                    break;
                case HtmlTextWriterStyle.FontFamily:
                    this.CssStyleName = "font-family";
                    break;
                case HtmlTextWriterStyle.FontSize:
                    this.CssStyleName = "font-size";
                    break;
                case HtmlTextWriterStyle.FontStyle:
                    this.CssStyleName = "font-style";
                    break;
                case HtmlTextWriterStyle.FontVariant:
                    this.CssStyleName = "font-variant";
                    break;
                case HtmlTextWriterStyle.FontWeight:
                    this.CssStyleName = "font-weight";
                    break;
                case HtmlTextWriterStyle.Height:
                    this.CssStyleName = "height";
                    break;
                case HtmlTextWriterStyle.Left:
                    this.CssStyleName = "left";
                    break;
                case HtmlTextWriterStyle.ListStyleImage:
                    this.CssStyleName = "list-style-image";
                    break;
                case HtmlTextWriterStyle.ListStyleType:
                    this.CssStyleName = "list-style-type";
                    break;
                case HtmlTextWriterStyle.Margin:
                    this.CssStyleName = "margin";
                    break;
                case HtmlTextWriterStyle.MarginBottom:
                    this.CssStyleName = "margin-bottom";
                    break;
                case HtmlTextWriterStyle.MarginLeft:
                    this.CssStyleName = "margin-left";
                    break;
                case HtmlTextWriterStyle.MarginRight:
                    this.CssStyleName = "margin-right";
                    break;
                case HtmlTextWriterStyle.MarginTop:
                    this.CssStyleName = "margin-top";
                    break;
                case HtmlTextWriterStyle.Overflow:
                    this.CssStyleName = "overflow";
                    break;
                case HtmlTextWriterStyle.OverflowX:
                    this.CssStyleName = "overflowX";
                    break;
                case HtmlTextWriterStyle.OverflowY:
                    this.CssStyleName = "overflowY";
                    break;
                case HtmlTextWriterStyle.Padding:
                    this.CssStyleName = "padding";
                    break;
                case HtmlTextWriterStyle.PaddingBottom:
                    this.CssStyleName = "padding-bottom";
                    break;
                case HtmlTextWriterStyle.PaddingLeft:
                    this.CssStyleName = "padding-left";
                    break;
                case HtmlTextWriterStyle.PaddingRight:
                    this.CssStyleName = "padding-right";
                    break;
                case HtmlTextWriterStyle.PaddingTop:
                    this.CssStyleName = "padding-top";
                    break;
                case HtmlTextWriterStyle.Position:
                    this.CssStyleName = "position";
                    break;
                case HtmlTextWriterStyle.TextAlign:
                    this.CssStyleName = "text-align";
                    break;
                case HtmlTextWriterStyle.TextDecoration:
                    this.CssStyleName = "text-decoration";
                    break;
                case HtmlTextWriterStyle.TextOverflow:
                    this.CssStyleName = "text-overflow";
                    break;
                case HtmlTextWriterStyle.Top:
                    this.CssStyleName = "top";
                    break;
                case HtmlTextWriterStyle.VerticalAlign:
                    this.CssStyleName = "vertical-align";
                    break;
                case HtmlTextWriterStyle.Visibility:
                    this.CssStyleName = "visibility";
                    break;
                case HtmlTextWriterStyle.WhiteSpace:
                    this.CssStyleName = "white-space";
                    break;
                case HtmlTextWriterStyle.Width:
                    this.CssStyleName = "width";
                    break;
                case HtmlTextWriterStyle.ZIndex:
                    this.CssStyleName = "z-index";
                    break;
                default:
                    break;
            }

            DomStyleName = StringExtensions.PascalCase(style.ToString());
        }
        public string CssStyleName { get; internal set; }
        public string DomStyleName { get; internal set; }
    }
}
