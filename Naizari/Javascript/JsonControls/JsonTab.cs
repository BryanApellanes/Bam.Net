/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Naizari.Images;
using System.Drawing;
using Naizari.Extensions;
using Naizari.Helpers;
using Naizari.Javascript;
using Naizari.Helpers.Web;

namespace Naizari.Javascript.JsonControls
{
    public class JsonTab: JsonControl
    {
        JsonFunction mouseoverFunction;
        JsonFunction mouseoutFunction;
        JsonFunction wireupFunction;
        JsonFunction clickFunction;

        public JsonTab()
            : base()
        {
            
            controlToRender.TagName = "span";
            this.Styles[HtmlTextWriterStyle.Display] = "inline-block";
            if (BrowserAccessHelper.IsIELessThanOrEqualTo(6))
                this.Styles[HtmlTextWriterStyle.Width] = "100%";
            this.TabHeight = 20;
            this.VerticalMargin = 3;
            this.LineWidth = 1;
            this.LineColor = "#000000";
            this.MouseOverLineColor = "#000000";
            this.BackgroundColor = "#FFFFFF";
            this.MouseOverBackgroundColor = "#FFFFFF";
            this.FillColor = "#787878";
            this.MouseOverFillColor = "#9A9A9A";
            this.TextPadding = 10;
            this.CornerRadius = 8;
            this.HorizontalMargin = 3;
            this.IsFirst = true;

            this.mouseoverFunction = new JsonFunction();
            this.mouseoverFunction.ExecutionType = JavascriptExecutionTypes.Call;
            this.mouseoutFunction = new JsonFunction();
            this.mouseoutFunction.ExecutionType = JavascriptExecutionTypes.Call;
            this.wireupFunction = new JsonFunction();
            this.clickFunction = new JsonFunction();
            this.clickFunction.ExecutionType = JavascriptExecutionTypes.Call;            
        }

        public JsonTab(string headerText)
            : this()
        {
            this.Text = headerText;
        }

        int tabHeight;
        public int TabHeight
        {
            get { return this.tabHeight; }
            set
            {
                this.tabHeight = value;
                //this.SetMouseOverProperties();
            }
        }
        int verticalMargin;
        public int VerticalMargin 
        {
            get { return this.verticalMargin; }
            set
            {
                this.verticalMargin = value;
                //this.SetMouseOverProperties();
            }
        }
        int horizontalMargin;
        public int HorizontalMargin
        {
            get { return this.horizontalMargin; }
            set
            {
                this.horizontalMargin = value; 
                //this.SetMouseOverProperties();
            }
        }
        public int LineWidth { get; set; }
        public string LineColor { get; set; }
        public string BackgroundColor { get; set; }
        public string FillColor { get; set; }
        public string MouseOverFillColor { get; set; }
        public string MouseOverBackgroundColor { get; set; }
        public string MouseOverLineColor { get; set; }

        public int CornerRadius { get; set; }

        private int SpanHeight { get { return this.TabHeight + this.VerticalMargin; } } // -1 for the line

        internal bool IsFirst { get; set; }

        protected virtual string ClassType
        {
            get
            {
                return "top";
            }
        }

        /// <summary>
        /// The class that will be applied to the text of the tab.
        /// Note: The CssClass specified here should not effect the 
        /// tab's graphical appearance, use the TabHeight, VerticalMargin, HorizontalMargin,
        /// LineWidth, LineColor, BackgroundColor, FillColor, MouseOverFillColor,
        /// MouseOverBackgroundColor and MouseOverLineColor properties
        /// for that purpose.
        /// </summary>
        public override string CssClass
        {
            get
            {
                return base.CssClass;
            }
            set
            {
                base.CssClass = value;
            }
        }

        public ClientClickActionType ClickActionType
        {
            get;
            set;
        }

        public string ClickActionTarget
        {
            get;
            set;
        }

        JsonTab mouseOverProxy;
        public override void WireScriptsAndValidate()
        {
            this.ValidateColors();
            controlToRender.Attributes.Add("id", this.DomId);
            if (!this.IsFirst)
                controlToRender.Style.Add("margin-left", "-4");
            else
                controlToRender.Style.Add("margin-bottom", "-1");

            this.mouseOverProxy = new JsonTab();
            
            this.SetMouseOverProperties();

            if (!this.Selected)
            {
                this.CreateEventScript(mouseoverFunction, mouseOverProxy);
                this.CreateEventScript(mouseoutFunction, this);

                this.wireupFunction.AppendScriptBodyFormat("JSUI.AddEventHandler('{0}', {1}, 'mouseover');", this.DomId, mouseoverFunction.JsonId);
                this.wireupFunction.AppendScriptBodyFormat("JSUI.AddEventHandler('{0}', {1}, 'mouseout');", this.DomId, mouseoutFunction.JsonId);

                this.AddJsonFunction(this.mouseoverFunction);
                this.AddJsonFunction(this.mouseoutFunction);
            }

            if (this.ClickActionType != ClientClickActionType.None)
            {
                this.wireupFunction.AppendScriptBodyFormat("JSUI.AddEventHandler('{0}', {1}, 'click');", this.DomId, clickFunction.JsonId);                
                switch (this.ClickActionType)
                {
                    case ClientClickActionType.Script:
                        this.clickFunction.AppendScriptBodyFormat("{0}();", this.ClickActionTarget);
                        break;
                    case ClientClickActionType.Navigate:
                        this.clickFunction.AppendScriptBodyFormat("window.location = '{0}';", this.ClickActionTarget);
                        break;
                }
                this.AddJsonFunction(this.clickFunction);
            }            

            this.wireupFunction.AppendScriptBodyFormat("JSUI.SetHandCursor('{0}');", this.DomId);
            this.wireupFunction.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
            this.AddJsonFunction(wireupFunction);

            
        }

        internal void SetMouseOverProperties()
        {
            if (!string.IsNullOrEmpty(this.MouseOverFillColor))
                this.mouseOverProxy.FillColor = this.MouseOverFillColor;

            if (!string.IsNullOrEmpty(this.MouseOverBackgroundColor))
                this.mouseOverProxy.BackgroundColor = this.MouseOverBackgroundColor;

            if (!string.IsNullOrEmpty(this.MouseOverLineColor))
                this.mouseOverProxy.LineColor = this.MouseOverLineColor;

            this.mouseOverProxy.TabHeight = this.TabHeight;
            this.mouseOverProxy.LineWidth = this.LineWidth;
            this.mouseOverProxy.CornerRadius = this.CornerRadius;
            this.mouseOverProxy.HorizontalMargin = this.HorizontalMargin;
            this.mouseOverProxy.VerticalMargin = this.VerticalMargin;
        }

        private void CreateEventScript(JsonFunction targetFunction, JsonTab tabDefinition)
        {
            targetFunction.AppendScriptBodyFormat(
                "JSUI.GetElement('{0}').style.backgroundImage = \"url({1})\";",
                this.JsonId + "_mid",
                tabDefinition.GetTabImageUrl("MiddleSection"));

            targetFunction.AppendScriptBodyFormat(
                "JSUI.GetElement('{0}').style.backgroundImage = \"url({1})\";",
                this.JsonId + "_left",
                tabDefinition.GetTabImageUrl("FirstSide"));

            targetFunction.AppendScriptBodyFormat(
                "JSUI.GetElement('{0}').style.backgroundImage = \"url({1})\";",
                this.JsonId + "_right",
                tabDefinition.GetTabImageUrl("LastSide"));

            //this.AddJsonFunction(targetFunction);
        }

        public void ValidateColors()
        {
            if (!StringExtensions.IsValidHtmlHexColor(this.LineColor))
                throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The specified LineColor ({0}) is not a valid html color", this.LineColor);

            if (!StringExtensions.IsValidHtmlHexColor(this.BackgroundColor))
                throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The specified BackgroundColor ({0}) is not a valid html color", this.BackgroundColor);

            if (!StringExtensions.IsValidHtmlHexColor(this.FillColor))
                throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The specified FillColor ({0}) is not a valid html color", this.FillColor);

            if (!StringExtensions.IsValidHtmlHexColor(this.MouseOverFillColor))
                throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The specified MouseOverFillColor is not a valid html color", this.MouseOverFillColor);

            if (!StringExtensions.IsValidHtmlHexColor(this.MouseOverBackgroundColor))
                throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The specified MouseOverBackgroundColor is not a valid html color", this.MouseOverBackgroundColor);

            if (!StringExtensions.IsValidHtmlHexColor(this.MouseOverLineColor))
                throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The specified MouseOverLineColor is not a valid html color", this.MouseOverLineColor);
        }

        public int TextPadding { get; set; }
        public bool Selected { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            

            this.controlToRender.Style.Add(HtmlTextWriterStyle.Height, this.SpanHeight + "px");

            HtmlGenericControl preloadLeft = GetPreLoadControl(mouseOverProxy, "FirstSide");
            HtmlGenericControl preloadMiddle = GetPreLoadControl(mouseOverProxy, "MiddleSection");
            HtmlGenericControl preloadRight = GetPreLoadControl(mouseOverProxy, "LastSide");

            this.controlToRender.Style.Add("vertical-align", "bottom");
            this.controlToRender.Style.Add("padding", "0px");
            HtmlGenericControl middleImage = new HtmlGenericControl("span");
            middleImage.Attributes.Add("id", this.JsonId + "_mid");
            middleImage.Style.Add(HtmlTextWriterStyle.BackgroundImage, string.Format("url('{0}')", this.GetTabImageUrl("MiddleSection")));
            middleImage.Style.Add("background-repeat", "repeat-x");
            middleImage.Style.Add("height", this.SpanHeight + "px");
            //middleImage.Style.Add("height", "100%");
            middleImage.Style.Add("display", "block");
            middleImage.Style.Add("vertical-align", "bottom");

            HtmlGenericControl leftSide = new HtmlGenericControl("span");
            leftSide.Attributes.Add("id", this.JsonId + "_left");
            leftSide.Style.Add(HtmlTextWriterStyle.BackgroundImage, string.Format("url('{0}')", this.GetTabImageUrl("FirstSide")));
            leftSide.Style.Add("background-repeat", "no-repeat");
            leftSide.Style.Add("height", this.SpanHeight + "px");
            //leftSide.Style.Add("height", "100%");
            leftSide.Style.Add("display", "block");

            HtmlGenericControl rightSide = new HtmlGenericControl("span");
            rightSide.Attributes.Add("id", this.JsonId + "_right");
            rightSide.Style.Add(HtmlTextWriterStyle.BackgroundImage, string.Format("url('{0}')", this.GetTabImageUrl("LastSide")));
            rightSide.Style.Add("background-repeat", "no-repeat");
            rightSide.Style.Add("background-position", "right top");
            rightSide.Style.Add("display", "block");
            rightSide.Style.Add("height", this.SpanHeight + "px");
            //rightSide.Style.Add("height", "100%");

            HtmlGenericControl tabText = new HtmlGenericControl("span");
            tabText.Attributes.Add("id", this.JsonId + "_text");

            tabText.Attributes.Add("class", this.CssClass);
            //tabText.Style.Add("height", "100%");
            tabText.Style.Add("display", "block");
            if (BrowserAccessHelper.IsIELessThanOrEqualTo(6))
                tabText.Style.Add("width", "100%");
            tabText.Style.Add("vertical-align", "middle");
            tabText.Style.Add("padding", this.TextPadding + "px");            
            tabText.Controls.Add(new LiteralControl(this.Text));

            rightSide.Controls.Add(tabText);
            leftSide.Controls.Add(rightSide);
            middleImage.Controls.Add(leftSide);
            controlToRender.Controls.Add(middleImage);

            controlToRender.Controls.Add(preloadLeft);
            controlToRender.Controls.Add(preloadMiddle);
            controlToRender.Controls.Add(preloadRight);

            controlToRender.RenderControl(writer);
            if (RenderScripts)
                this.RenderConglomerateScript(writer);
        }

        private HtmlGenericControl GetPreLoadControl(JsonTab mouseOverTemp, string imageName)
        {
            HtmlGenericControl preload = new HtmlGenericControl("span");
            preload.Attributes.Add("id", this.JsonId + "_preload_" + imageName);
            preload.Style.Add("position", "absolute");
            preload.Style.Add("top", "-1000");
            preload.Style.Add("left", "-1000");
            preload.Style.Add(HtmlTextWriterStyle.BackgroundImage, string.Format("url('{0}')", mouseOverTemp.GetTabImageUrl(imageName)));

            return preload;
        }



        //<span style="height: 30px; display: inline-block;">
        //    <span id="smiddle" style="
        //            background-image: url('http://localhost:3077/EDWSite/Pixerve?tabh=30&classtype=top&in=MiddleSection&t=Naizari.Images.Tab&linewidth=3
        //&radius=15
        //&hmargin=5&vmargin=3&linecolor=%23000000&backgroundcolor=%23FF0000&fillcolor=%230000FF');
        //            background-repeat: repeat-x;
        //            height: 100%;
        //            display: block;
        //        ">
                
        //        <span id="sLeftCorner" 
        //            style="
        //                background-image: url('http://localhost:3077/EDWSite/Pixerve?tabh=30&classtype=top&in=FirstSide&t=Naizari.Images.Tab&linewidth=3&radius=15&hmargin=5&vmargin=3&linecolor=%23000000&backgroundcolor=%23FF0000&fillcolor=%230000FF');
        //                background-repeat: no-repeat;
        //                display: block;
        //                height: 100%;
        //            ">
                                    
        //            <span id="sRightCorner"
        //                    style="background-image: url('http://localhost:3077/EDWSite/Pixerve?tabh=30&classtype=top&in=LastSide&t=Naizari.Images.Tab&linewidth=3&radius=15&hmargin=5&vmargin=3&linecolor=%23000000&backgroundcolor=%23FF0000&fillcolor=%230000FF');
        //                    background-repeat: no-repeat;
        //                    background-position: right top;
        //                    vertical-align: middle;
        //                    display: block;
        //                    height: 100%;
        //                    "
        //                >
        //                 <span style="height: 100%; display: block; width: 100%; padding: 10px;">
        //                 The quick brown fox jumped over the lazy dog.
        //                 </span>
        //            </span>
                    
        //        </span>
        //    </span>
        //</span>

        protected string GetTabImageUrl(string imageName)
        {
            //string pixervepath = HttpContext.Current.Request.ApplicationPath;
            //if (!pixervepath.EndsWith("/"))
            //    pixervepath += "/";
            
            //pixervepath += PixelServer.Prefix + "?";

            string pixervepath = HttpContext.Current.Request.FilePath + "?pix=true&";

            pixervepath += GetParameterQueryString(imageName);
            return pixervepath;
        }

        protected string GetParameterQueryString(string imageName)
        {
            //tabh=30&classtype=top&in=MiddleSection&t=Naizari.Images.Tab&linewidth=3&radius=15&hmargin=5&vmargin=3&linecolor=%23000000&backgroundcolor=%23FF0000&fillcolor=%230000FF'
            //string retVal = string.Empty;
            //retVal += "tabh=" + this.TabHeight;
            //retVal += "&classtype=" + this.ClassType;
            //retVal += "&in=" + imageName;
            //retVal += "&t=" + typeof(Tab).Namespace + "." + typeof(Tab).Name;
            //retVal += "&linewidth=" + this.LineWidth;
            //retVal += "&radius=" + this.CornerRadius;
            //retVal += "&hmargin=" + this.HorizontalMargin;
            //retVal += "&vmargin=" + this.VerticalMargin;
            //retVal += "&linecolor=" + HttpUtility.UrlEncode(this.LineColor);
            //retVal += "&backgroundcolor=" + HttpUtility.UrlEncode(this.BackgroundColor);
            //retVal += "&fillcolor=" + HttpUtility.UrlEncode(this.FillColor);
            //return retVal;

            return string.Format(ParameterQueryStringFormat, 
                this.ClassType, 
                imageName, 
                this.TabHeight, 
                typeof(Tab).Namespace + "." + typeof(Tab).Name, 
                this.LineWidth, 
                this.CornerRadius, 
                this.HorizontalMargin, 
                this.VerticalMargin, 
                HttpUtility.UrlEncode(this.LineColor), 
                HttpUtility.UrlEncode(this.BackgroundColor), 
                HttpUtility.UrlEncode(this.FillColor));
        }

        public static string ParameterQueryStringFormat
        {
            get
            {
                return "classtype={0}&in={1}&tabh={2}&t={3}&linewidth={4}&radius={5}&hmargin={6}&vmargin={7}&linecolor={8}&backgroundcolor={9}&fillcolor={10}";
            }
        }
    }
}
