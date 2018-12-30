/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Web.UI.Design;
using System.Drawing.Design;
using Naizari.Javascript.JsonControls.Design;
using Naizari.Extensions;
using Naizari.Helpers;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    /// <summary>
    /// A JsonInvoker will invoke the JsonMethod specified in its 
    /// MethodName property.  If a JsonId is specified in its
    /// CallbackJsonId property that callback will be executed 
    /// upon return of the method.
    /// </summary>
    public abstract class JsonInvoker: JsonControl
    {
        protected List<JsonInput> parameterSources;
        internal JsonInvokerTypes type;
        List<string> parameterSourceJsonIds;
        protected List<string> eventSourceDomIds;
        protected List<string> eventSourceEventNames;
        protected List<string> requiredJsonTags;

        public event ParameterSourceAddedDelegate ParameterSourceAdded;

        public JsonInvoker()
            : base()
        {
            this.type = JsonInvokerTypes.a;
            this.parameterSourceJsonIds = new List<string>();
            this.parameterSources = new List<JsonInput>();
            this.Text = this.GetType().Name;
            this.ResponseFormat = JsonResponseFormats.json;
            this.ClientEventName = "click";
            this.eventSourceDomIds = new List<string>();
            this.eventSourceEventNames = new List<string>();
            this.requiredJsonTags = new List<string>();

            this.AddRequiredScript(typeof(JsonInvoker));//"naizari.javascript.jsoncontrols.jsoninvoker.js");
            
        } 

        private void OnParameterSourceAdded(JsonInput control)
        {
            if (this.ParameterSourceAdded != null)
                this.ParameterSourceAdded(this, control);
        }

        public string ClientEventName { get; set; }

        public string ClientInstanceName
        {
            get
            {
                if (JsonMethodProvider == null)
                    return string.Empty;

                return JavascriptServer.GetDefaultVarName(JsonMethodProvider.GetType());
            }
        }

        /// <summary>
        /// The name of the method on JsonMethodProvider to be invoked.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Set to true if the method provider for the current JsonInvoker is a master page.  If the
        /// current JsonInvoker is on a page that has no master page this property is ignored.
        /// The default is false.
        /// </summary>
        public bool MethodProviderIsMaster { get; set; }

        object jsonMethodProvider;
        /// <summary>
        /// The object that will provide the JsonMethods.  The default 
        /// is the current Page or UserControl that this JsonInvoker
        /// instance is on.
        /// </summary>
        public object JsonMethodProvider
        {
            get
            {
                if (jsonMethodProvider == null)
                {
                    if (Page is JavascriptPage)
                    {
                        jsonMethodProvider = Page;

                        //if the methodprovider is the master page
                        if (this.MethodProviderIsMaster && ((JavascriptPage)Page).Master != null)
                        {
                            jsonMethodProvider = ((JavascriptPage)Page).Master;
                            //JavascriptServer.RegisterHandlerByInstance(((JavascriptPage)Page).Master);
                        }
                    }

                    // if this invoker is in an ascx file the
                    // JsonMethods it invokes can/must be defined 
                    // in the ascx.cs file
                    if (Parent is UserControl)
                        jsonMethodProvider = Parent;

                    if (!this.DesignMode && jsonMethodProvider != null)
                        JavascriptServer.RegisterProvider(jsonMethodProvider);

                    
                }
                return jsonMethodProvider;
            }
            set
            {
                jsonMethodProvider = value;
                if (!this.DesignMode)
                    JavascriptServer.RegisterProvider(value);
            }
        }

        /// <summary>
        /// A comma separated list of JsonIds to use as input to the invocation method of this
        /// invoker.
        /// </summary>
        public string ParameterSourceJsonIds
        {
            get
            {
                return StringExtensions.ToCommaSeparated(parameterSourceJsonIds.ToArray());
            }
            set
            {
                parameterSourceJsonIds.Clear();
                parameterSourceJsonIds.AddRange(StringExtensions.DelimitSplit(value, ",", true));
                this.parameterSources.Clear();
            }
        }

        internal void SetCallback()
        {
            SetCallback(false);
        }

        internal void SetCallback(bool searchControls)
        {
            if (Callback == null)
            {
                JsonCallback callback = null;
                if (ParentJavascriptPage != null)
                {
                    callback = ParentJavascriptPage.FindCallback(CallbackJsonId, searchControls);
                }


                Callback = callback;
            }

            if (Callback != null)
                Callback.Invoker = this;
        }

        internal void SetParameterSources()
        {
            foreach (string parameterSourceJsonId in parameterSourceJsonIds)
            {
                if (Parent is UserControl)
                {
                    foreach (Control control in Parent.Controls)
                    {
                        if (control is JsonInput)
                        {
                            JsonInput inputControl = (JsonInput)control;
                            if (inputControl.JsonId.Equals(parameterSourceJsonId))
                            {
                                AddParameterSource(inputControl);

                            }
                        }
                    }
                }
                else if (ParentJavascriptPage != null)
                {
                    //ParentJavascriptPage.AddJsonControl(this);
                    IJsonControl control = ParentJavascriptPage.FindJsonControl(parameterSourceJsonId);
                    if (control != null && control is JsonInput)
                    {
                        AddParameterSource((JsonInput)control);
                    }
                    else
                    {
                        throw new ParameterSourceNotFoundException(parameterSourceJsonId);
                    }
                }
            }
        }

        internal List<JsonInput> ParameterSources
        {
            get
            {
                return parameterSources;
            }
        }

        public void AddParameterSource(JsonInput control)
        {
            if (parameterSources == null)
                parameterSources = new List<JsonInput>();

            parameterSources.Add(control);
            OnParameterSourceAdded(control);
        }

        public string CallbackJsonId
        {
            get;
            set;
        }

        JsonCallback jsonCallback;
        public JsonCallback Callback
        {
            get
            {
                return jsonCallback;
            }
            set
            {
                if (value == null)
                    throw ExceptionHelper.CreateException<JsonInvalidOperationException>("Callback cannot be null: {0}", this.ToString());

                this.jsonCallback = value;
                this.jsonCallback.Invoker = this;
            }
        }

        public JsonResponseFormats ResponseFormat
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the data template to use found in ~/App_Data/Boxes/Data, the
        /// Type and extension should not be specified.  For example, if the 
        /// Type to be templated is "JsonDataItem" and you wish to use the "TreeBranch"
        /// template, you would set this property to "TreeBranch" and not "JsonDataItem.TreeBranch.ascx".
        /// </summary>
        public string DataTemplateName { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            if (type != JsonInvokerTypes.extension)
            {
                controlToRender.Attributes.Add("jsonid", this.JsonId);
                controlToRender.Attributes.Add("id", this.DomId);
                if (!string.IsNullOrEmpty(this.CssClass))
                    controlToRender.Attributes.Add("class", this.CssClass);

                styles.AddStyles(controlToRender);
                attributes.AddAttributes(controlToRender);

                controlToRender.TagName = type.ToString();

                //string clientInvokeString = GetClientInvokeString();

                if (type == JsonInvokerTypes.a)
                {
                    //controlToRender.Attributes.Add("href", "#");//string.Format("javascript:{0}", clientInvokeString));

                    controlToRender.InnerText = this.Text;
                }

                if (type == JsonInvokerTypes.input)
                {
                    controlToRender.Attributes.Add("type", "button");
                    //controlToRender.Attributes.Add("onclick", clientInvokeString);
                }

                controlToRender.Attributes.Add("value", this.Text);
                controlToRender.RenderControl(writer);



                if (parameterSources.Count > 0 && this.RenderScripts)
                {
                    //script.RenderControl(writer);
                    this.RenderConglomerateScript(writer);
                }
            }
        }

        public override void WireScriptsAndValidate()
        {
            if (string.IsNullOrEmpty(EventSourceDomIds))
                EventSourceDomIds = this.JsonId;

            WireScriptsAndValidate(false);
        }

        /// <summary>
        /// Comma separated list of html tags that should be registered so they can
        /// be found by using JSUI.GetElementByJsonId in Javascript.
        /// </summary>
        public string RequiredJsonTags
        {
            get
            {
                return StringExtensions.ToCommaSeparated(this.requiredJsonTags.ToArray());
            }
            set
            {
                this.requiredJsonTags.Clear();
                this.requiredJsonTags.AddRange(StringExtensions.CommaSplit(value));
            }
        }

        /// <summary>
        /// Comma separated list of html element ids to listen to the "ClientEventName" of to
        /// invoke this invoker.
        /// </summary>
        public string EventSourceDomIds
        {
            get
            {
                return StringExtensions.ToCommaDelimited(this.eventSourceDomIds.ToArray());
            }
            set
            {
                this.eventSourceDomIds.Clear();
                this.eventSourceDomIds.AddRange(StringExtensions.CommaSplit(value));
            }
        }

        public string PreInvokeJsonIds
        {
            get;
            set;
        }

        public string EventSourceEventNames
        {
            get
            {
                return StringExtensions.ToCommaDelimited(this.eventSourceEventNames.ToArray());
            }
            set
            {
                this.eventSourceEventNames.Clear();
                this.eventSourceEventNames.AddRange(StringExtensions.CommaSplit(value));
            }
        }

        public void WireScriptsAndValidate(bool searchControls)
        {
            if (string.IsNullOrEmpty(this.EventSourceDomIds))
                throw new JsonInvalidOperationException("EventSourceDomIds was not specified: " + this.ToString());

            SetClientEventName();

            SetParameterSources();
            SetCallback(searchControls);

            controlToRender.Attributes.Add("jsonid", this.JsonId);
            JavascriptServer.ValidateInvoker(this);

            // wire scripts - new method?
            JsonFunction invokeScript = new JsonFunction(GetClientInvokeString());
            invokeScript.JsonId = this.JsonId; // THE INVOKESCRIPT JSONID MUST MATCH THE INVOKER

            invokeScript.ExecutionType = JavascriptExecutionTypes.Call;
            JsonFunction wireupScript = new JsonFunction();
            if(this.parameterSources.Count > 0)
                wireupScript.AddScriptTextLine("JSUI.UseParameterSources('" + this.JsonId + "', " + GetParameterIdArray() + ");");

            if (this.type == JsonInvokerTypes.a)
                this.AddRequiredJsonTag("a");//this.requiredJsonTags.Add("a");

            RegisterJsonTags(wireupScript);

            WireEventSources(wireupScript);

            wireupScript.ExecutionType = JavascriptExecutionTypes.OnParse;
            wireupScript.JsonId = this.JsonId + "_wireup";
            this.AddJsonFunction(invokeScript);
            this.AddJsonFunction(wireupScript);            
        }

        protected void WirePreInvokeFunctions()
        {
            if (!string.IsNullOrEmpty(this.PreInvokeJsonIds))
            {
                foreach (string jsonId in StringExtensions.CommaSplit(this.PreInvokeJsonIds))
                {
                    this.Callback.PreInvokeFunctions.AddDelegate(jsonId);
                }
            }


            this.Callback.PreInvokeFunctions.WireScriptsAndValidate();
            this.AddJsonFunction(this.Callback.PreInvokeFunctions);
        }

        protected void AddRequiredJsonTag(string tagName)
        {
            if (!this.requiredJsonTags.Contains(tagName))
                this.requiredJsonTags.Add(tagName);
        }

        private void SetClientEventName()
        {
            List<string> clientEventNames = new List<string>();
            foreach (string sourceId in this.eventSourceDomIds)
            {
                JsonEventSource eventSource = this.ParentJavascriptPage.FindJsonControlByDomIdAs<JsonEventSource>(sourceId);
                if (eventSource != null)
                {
                    if (clientEventNames.Count > 0 && !clientEventNames.Contains(eventSource.ClientEventName))
                        throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The ClientEventName of all event sources must be the same, found '{0}' and '{1}': {2}", clientEventNames[0], eventSource.ClientEventName, this.ToString());

                    clientEventNames.Add(eventSource.ClientEventName);
                }
            }

            if (clientEventNames.Count > 0)
                this.ClientEventName = clientEventNames[0];
            
            //JsonEventSource eventSource = this.ParentJavascriptPage.FindJsonControlByDomIdAs<JsonEventSource>(sourceId);
            //    if (eventSource != null && eventSource.ExecutionType == JavascriptExecutionTypes.OnWindowLoad)
            //        this.ClientEventName = eventSource.ClientEventName;//StringExtensions.RandomString(8, false, false); // if the event source is a JsonEventSource no need to specify ClientEventName
            
        }

        private void RegisterJsonTags(JsonFunction wireupScript)
        {
            foreach (string tag in requiredJsonTags)
            {
                wireupScript.AddScriptTextLine(string.Format("JSUI.RegisterJsonTag('{0}');", tag));
            }
        }

        private void WireEventSources(JsonFunction wireupScript)
        {
            for (int i = 0; i < this.eventSourceDomIds.Count; i++)
            {
                string sourceId = this.eventSourceDomIds[i];
                if (this.eventSourceEventNames.Count > i)
                {
                    //wireupScript.AddScriptTextLine(string.Format("jQuery('id={0}').bind('{1}', {2});", sourceId, this.eventSourceEventNames[i], this.JsonId));
                    wireupScript.AppendScriptBody(string.Format("JSUI.AddEventHandler('{0}',{1},'{2}');", sourceId, this.JsonId, this.eventSourceEventNames[i]));
                }
                else
                {
                    //wireupScript.AddScriptTextLine(string.Format("jQuery('id={0}').bind('{1}', {2});", sourceId, this.ClientEventName, this.JsonId));
                    wireupScript.AppendScriptBody(string.Format("JSUI.AddEventHandler('{0}',{1},'{2}');//monkey", sourceId, this.JsonId, this.ClientEventName));
                }
            }
        }

        public bool InvokeOnce { get; set; }
        
        private string GetClientInvokeString()
        {
            if(Callback == null)
                throw new JsonInvokerValidationException(this, JsonInvokerValidationResult.CallbackNotSpecified, this.JsonMethodProvider.GetType(), null, -1);

            string parameterGetter = this.parameterSources.Count > 0 ? string.Format("JSUI.GetParameterValues('{0}')", this.JsonId) : "''";
            string invokeOnce = this.InvokeOnce ? "true" : "false";
            return "JSUI.InvokeMethodWithCallback('" + Callback.JsonId + "', " + parameterGetter + ", JSUI.construct('DataRequestInfo', ['" + this.ResponseFormat + "', '" + this.DataTemplateName + "', " + invokeOnce + "]));";

        }

        protected string GetParameterIdArray()
        {
            string retVal = "[ ";
            for(int i = 0; i < parameterSources.Count; i++)
            {
                JsonInput source = parameterSources[i];
                retVal += string.Format("'{0}'", source.InputJsonId.Trim());
                if (i < parameterSources.Count - 1)
                    retVal += ", ";
            }
            retVal += " ]";
            return retVal;
        }


    }
}
