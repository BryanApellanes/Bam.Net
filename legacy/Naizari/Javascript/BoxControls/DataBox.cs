/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Naizari.Helpers.Web;
using Naizari.Helpers;
using System.ComponentModel;
using Naizari.Extensions;

[assembly: TagPrefix("Naizari.Javascript.BoxControls", "box")]
namespace Naizari.Javascript.BoxControls
{
    /// <summary>
    /// A DataBox is a specialized JsonInvoker that requests that the
    /// data returned by the method named in its MethodName property
    /// is templated by the BoxServer using a specialized ascx file
    /// found in ~/App_Data/Boxes/Data, where the name of the ascx file
    /// is in the format &lt;Type.Name&gt;.&lt;<i>DataTemplateName</i>&gt;, 
    /// or &lt;Type.Name&gt;/&lt;<i>DataTemplateName</i>&gt; where 
    /// <i>DataTemplateName</i> is the value specified in the 
    /// DataTemplateName property.
    /// </summary>
    public class DataBox: JsonInvoker
    {
        JsonFunction showWorkingPreInvoke;
        public class BoxCallback : JsonCallback
        {
            public BoxCallback()
                : base()
            {
                this.parameters.Clear();
                this.AddParameter("html");
                this.AddParameter("clientKey");
                this.ResponseFormat = JsonResponseFormats.box;
            }

            protected override void OnParameterAdded(JsonFunction function, string parameterName)
            {
                
            }

        }

        public DataBox()
            : base()
        {
            this.Callback = new BoxCallback();
            //this.Callback.PreInvokeFunctions = new JsonFunctionDelegate();
            this.Callback.Invoker = this;
            this.type = JsonInvokerTypes.extension;
            this.AddJsonFunction(this.Callback);

            this.ResponseFormat = JsonResponseFormats.box;
            this.InvokeOnce = true; //default
            this.ToggleEventName = "click";

            this.showWorkingPreInvoke = new JsonFunction();
            this.WorkingInline = true;
            //this.AddRequiredScript("naizari.javascript.boxcontrols.box.js");
        }

        //public string WorkingImagePath { get; set; }
        //public string WorkingText { get; set; }



        public string TogglerDomId { get; set; }

        /// <summary>
        /// The name of the client event, of the element specified by the 
        /// TogglerDomId property, that will cause the current DataBox (rendered as a div)
        /// to be hidden and shown.  
        /// </summary>
        public string ToggleEventName { get; set; }

        //public bool UseWorking
        //{
        //    get
        //    {
        //        return !string.IsNullOrEmpty(this.WorkingImagePath) || !string.IsNullOrEmpty(this.WorkingText);
        //    }
        //}

        public bool MakeScrollable
        {
            get
            {
                return !string.IsNullOrEmpty(this.Width) && !string.IsNullOrEmpty(this.Height);
            }
        }

        //public string WorkingCssClass { get; set; }

        string width;
        public string Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
                //this.Styles[HtmlTextWriterStyle.Width] = value + "px";
            }
        }

        string height;
        public string Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
                //this.Styles[HtmlTextWriterStyle.Height] = value + "px";
            }
        }

        public int BorderWidth { get; set; }
        public string BorderColor { get; set; }
        public string BorderStyle { get; set; }

        [Browsable(true),
        Description(@"The DomId of an element to show while processing this DataBox's async request. 
This can be the ClientId of an asp control or any valid id of a client side html element.")]
        public string WorkingDomId { get; set; }

        bool workingInline;
        /// <summary>
        /// If true the element specified by id &lt;WorkingDomId&gt; 
        /// will have its style display property set to inline.  Setting this
        /// property will toggle the property WorkingBlock to the 
        /// opposite of the value specified.
        /// </summary>
        public bool WorkingInline
        {
            get
            {
                return workingInline;
            }
            set
            {
                workingInline = value;
                workingBlock = !value;
            }
        }

        bool workingBlock;
        /// <summary>
        /// If true the element specified by id &lt;WorkingDomId&gt; 
        /// will have its style display property set to block.  Setting this
        /// property will toggle the property WorkingInline to the 
        /// opposite of the value specified.
        /// </summary>
        public bool WorkingBlock
        {
            get
            {
                return workingBlock;
            }
            set
            {
                workingBlock = value;
                workingInline = !value;
            }
        }

        public bool HideWhileWorking
        {
            get;
            set;
        }

        private void UnWireEventSources(JsonFunction function)
        {
            foreach (string eventSourceDomId in this.eventSourceDomIds)
            {
                //function.AppendScriptBody(string.Format("\r\njQuery('id={0}').unbind('{1}', {2});", eventSourceDomId, this.ClientEventName, this.JsonId));
                function.AppendScriptBody(string.Format("\r\nJSUI.RemoveEventHandler('{0}', {1}, '{2}');", eventSourceDomId, this.JsonId, this.ClientEventName));
            }
        }



        public string PostInvokeJsonIds
        {
            get;
            set;
        }

        bool wired;
        /// <summary>
        /// This method is called by the JavascriptPage.WireJsonControls method and is invoked by the 
        /// BoxServer as the final action in PostBoxLoad.
        /// </summary>
        public override void WireScriptsAndValidate()
        {
            if (!wired)
            {
                wired = true;
                base.WireScriptsAndValidate(false);

                this.Callback.AppendScriptBody(string.Format("JSUI.GetElement('{0}').innerHTML = html;\r\n", this.DomId));

                this.Callback.AppendScriptBody("DataBox.GetDataBoxScripts(clientKey);\r\n"); // causes the client to retrieve the scripts required by the specified DataTemplateName
                this.Callback.AppendScriptBody(string.Format("JSUI.MakeElementVisible('{0}');", this.DomId));
                this.Callback.AppendScriptBody(string.Format("JSUI.ShowElementBlock('{0}');", this.DomId));

                if (!string.IsNullOrEmpty(this.PostInvokeJsonIds))
                {
                    foreach (string jsonId in StringExtensions.CommaSplit(this.PostInvokeJsonIds))
                    {
                        this.Callback.AppendScriptBody(string.Format("{0}();", jsonId));
                    }
                }

                if (this.InvokeOnce)
                {
                    this.UnWireEventSources(this.Callback);
                }

                if (!string.IsNullOrEmpty(TogglerDomId))
                {
                    //this.Callback.AppendScriptBody(string.Format("\r\njQuery('id={0}').bind('{1}', function(){2}JSUI.ToggleElementDisplay('{3}');{4});", this.TogglerDomId, this.ToggleEventName, "{", this.DomId, "}"));
                    this.Callback.AppendScriptBody(string.Format("\r\nJSUI.AddEventHandler('{0}', function(){3}JSUI.ToggleElementDisplay('{1}');{4}, '{2}');", this.TogglerDomId, this.DomId, this.ToggleEventName, "{", "}"));
                }

                if (!string.IsNullOrEmpty(this.WorkingDomId))
                {

                    this.Callback.AppendScriptBody(string.Format("\r\nJSUI.HideElement('{0}');", this.WorkingDomId));

                    if (this.HideWhileWorking)
                        this.showWorkingPreInvoke.AppendScriptBody(string.Format("JSUI.MakeElementInvisible('{0}');", this.DomId));

                    string displayStyle = this.WorkingInline ? "Inline" : "Block";

                    this.showWorkingPreInvoke.AppendScriptBody(string.Format("JSUI.ShowElement{0}('{1}');", displayStyle, this.WorkingDomId));

                    this.showWorkingPreInvoke.ExecutionType = JavascriptExecutionTypes.Call;

                    this.Callback.PreInvokeFunctions.AddDelegate(this.showWorkingPreInvoke);

                    this.AddJsonFunction(this.showWorkingPreInvoke);
                }

                WirePreInvokeFunctions();

                if (MakeScrollable)
                {

                    this.Callback.AppendScriptBody(string.Format("\r\nJSUI.MakeScrollable('{0}', {1});", this.DomId, GetJsonDimensions()));

                    if (!string.IsNullOrEmpty(this.Height))
                        this.Callback.AppendScriptBody(string.Format("JSUI.GetScrollable('{0}').SetHeight('{1}');", this.DomId, this.Height));

                    if (!string.IsNullOrEmpty(this.BorderColor))
                        this.Callback.AppendScriptBody(string.Format("JSUI.GetScrollable('{0}').SetBorderColor('{1}');", this.DomId, this.BorderColor));
                    if (this.BorderWidth != 0)
                        this.Callback.AppendScriptBody(string.Format("JSUI.GetScrollable('{0}').SetBorderWidth('{1}');", this.DomId, this.BorderWidth));
                    if (!string.IsNullOrEmpty(this.BorderStyle))
                        this.Callback.AppendScriptBody(string.Format("JSUI.GetScrollable('{0}').SetBorderStyle('{1}');", this.DomId, this.BorderStyle));


                    if (!string.IsNullOrEmpty(this.Width))
                        this.Callback.AppendScriptBody(string.Format("JSUI.GetScrollable('{0}').SetWidth('{1}');", this.DomId, this.Width));

                    this.Callback.AppendScriptBody(";");
                }
            }
            //this.Callback.FunctionBody = callbackFunctionBody;
        }



        private string GetJsonDimensions()
        {
            return string.Format("{0}Width: '{1}', Height: '{2}'{3}", "{", this.Width, this.Height, "}");
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.WireScriptsAndValidate();
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            JsonControl.ApplyAttributesAndStyles(controlToRender, styles, attributes, CssClass);

            //if (this.UseWorking)
            //{
            //    SetupWorkingDiv(writer);
            //}

            controlToRender.TagName = "div";
            controlToRender.Attributes.Add("id", this.DomId);
            controlToRender.Attributes.Add("jsonid", this.JsonId);
            if (!string.IsNullOrEmpty(this.Height))
            {
                controlToRender.Style.Add(HtmlTextWriterStyle.Height, GetDimension(this.Height));
            }

            if (!string.IsNullOrEmpty(this.Width))
            {
                controlToRender.Style.Add(HtmlTextWriterStyle.Width, GetDimension(this.Width));
            }

            controlToRender.RenderControl(writer);           

            if(this.renderScripts)
                this.RenderConglomerateScript(writer);
        }

        private string GetDimension(string dimensionString)
        {
            return ControlHelper.GetCssDimension(dimensionString);
        }

        //private void SetupWorkingDiv(HtmlTextWriter writer)
        //{
        //    HtmlGenericControl workingDiv = new HtmlGenericControl("div");
        //    workingDiv.Attributes.Add("id", string.Format("{0}_working", this.JsonId));
        //    workingDiv.Style.Add("display", "none");
        //    this.styles.AddStyles(workingDiv);
            

        //    if (!string.IsNullOrEmpty(this.WorkingCssClass))
        //        workingDiv.Attributes.Add("class", this.WorkingCssClass);

        //    HtmlTable table = ControlHelper.CreateHtmlTable(1, 2);
        //    workingDiv.Controls.Add(table);

        //    if (this.WorkingImage != null)
        //    {
        //        ControlHelper.AddControlToHtmlTable(table, this.WorkingImage, 1, 1);
        //    }

        //    if (!string.IsNullOrEmpty(this.WorkingText))
        //    {
        //        ControlHelper.AddControlToHtmlTable(table, new LiteralControl(this.WorkingText), 1, 2);
        //        //table.Rows[0].Cells[1].Controls.Add(new LiteralControl(this.WorkingText));
        //    }

        //    //controlToRender.Controls.Add(workingDiv);
        //    workingDiv.RenderControl(writer);
        //}
    }
}
