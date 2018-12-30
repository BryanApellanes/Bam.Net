/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using KLGates.Extensions;
using KLGates.Helpers;
using KLGates.Configuration;

namespace KLGates.Javascript.JsonControls
{
    public class JsonMouseOverImage: JsonControl
    {
        public JsonMouseOverImage()
            : base()
        {
            this.SetHandCursor = true;
            this.InitialImageEvent = "mouseout";
            this.ToggleImageEvent = "mouseover";
        }

        public JsonMouseOverImage(string initialImageSource, string toggleImageSource)
            : this()
        {
            this.InitialImageSource = initialImageSource;
            this.ToggleImageSource = toggleImageSource;
        }

        public override void WireScriptsAndValidate()
        {
            if (string.IsNullOrEmpty(this.InitialImageSource))
                ExceptionHelper.ThrowInvalidOperation("The InitialImageSource property was not set: ['{0}']", this.ToString());

            if (string.IsNullOrEmpty(this.ToggleImageSource))
                ExceptionHelper.ThrowInvalidOperation("The ToggleImageSource property was not set: ['{0}']", this.ToString());

            
            base.WireScriptsAndValidate();
        }

        public string InitialImageSource { get; set; }
        public string ToggleImageSource { get; set; }

        public string InitialImageEvent { get; set; }
        public string ToggleImageEvent { get; set; }

        public bool SetHandCursor { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer); // always call Render first to set styles and attributes

            controlToRender.TagName = "img";
            controlToRender.Attributes.Add("src", this.InitialImageSource);
            controlToRender.Attributes.Add("id", this.DomId);
            controlToRender.RenderControl(writer);

            JsonFunction function = new JsonFunction();
            // name, outimage, overimage
            string functionBody = string.Format("JSUI.QueueSwapifyConfig(JSUI.construct(\"ToggleConfig\", ['{0}', '{1}', '{2}', '{3}', '{4}']));", this.JsonId, this.InitialImageSource, this.ToggleImageSource, this.InitialImageEvent, this.ToggleImageEvent);
            functionBody += string.Format("\r\nJSUI.SetToggleImage('{0}', '{1}');", this.DomId, this.JsonId);
            //functionBody += string.Format("\r\nJSUI.ImageSwapify('{0}', '{0}');", this.JsonId);

            if (this.SetHandCursor)
                functionBody += string.Format("\r\nJSUI.SetHandCursor('{0}');", this.DomId);

            function.FunctionBody = functionBody;
            function.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
            this.AddJsonFunction(function);
            if (RenderScripts)
            {
                function.RenderControl(writer);
            }            
        }

    }
}
