/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Naizari.Extensions;
using System.ComponentModel;
using System.Reflection;
using Naizari.Javascript.BoxControls;
using Naizari.Helpers.Web;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    [Serializable]
    public abstract class JsonControl: Control, INamingContainer, IJsonControl
    {
        List<string> requiredScriptFileNames;
        protected JsonAttributes attributes;
        protected JsonStyles styles;
        protected HtmlGenericControl controlToRender;
        /// <summary>
        /// All scripts that will be rendered either individually
        /// or as a conglomeration depending on the Parent of the 
        /// control.
        /// </summary>
        protected Dictionary<string, JsonFunction> scripts;
        protected bool renderScripts;

        public JsonControl():base()
        {
            scripts = new Dictionary<string, JsonFunction>();
            requiredScriptFileNames = new List<string>();
            controlToRender = new HtmlGenericControl();

            AutoRegisterScript = false;
            randomJsonId = StringExtensions.RandomString(8, false, false);
            DomId = StringExtensions.RandomString(8, false, false);
            
            attributes = new JsonAttributes();
            styles = new JsonStyles();
            renderScripts = true;

            this.Text = string.Empty;
        }

        protected override void OnInit(EventArgs e)
        {
            if (Page is JavascriptPage && ((JavascriptPage)Page).FindJavascriptControls == false)
            {
                JavascriptPage page = (JavascriptPage)Page;
                page.AddJsonControl(this);
            }
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.EnsureID();
            if (this.Parent is UserControl)
            {
                this.ID = string.Format("{0}_{1}", this.Parent.ID, this.ID);
            }

            base.OnLoad(e);
        }
 
        public void AddJsonFunction(JsonFunction script)
        {
            if(!this.scripts.ContainsKey(script.JsonId))
                this.scripts.Add(script.JsonId, script);
        }

        public void RemoveJsonFunction(JsonFunction script)
        {
            if (this.scripts.ContainsKey(script.JsonId))
                this.scripts.Remove(script.JsonId);
        }

        public void ClearScripts()
        {
            this.scripts.Clear();
        }

        public JsonFunction[] Scripts
        {
            get
            {
                return DictionaryExtensions.ValuesToArray<string, JsonFunction>(this.scripts);
            }
        }

        public bool HasScripts
        {
            get
            {
                return scripts.Count > 0;
            }
        }

        /// <summary>
        /// If true this control will render any required client scripts.
        /// This is primarliy used by the BoxServer and should possibly be
        /// made protected or private.
        /// </summary>
        public bool RenderScripts
        {
            get
            {
                return renderScripts;// && HasScripts;
            }

            internal set
            {
                renderScripts = value;
            }
        }

        /// <summary>
        /// When overridden in a derived class should perform any initialization required
        /// by the class instance.
        /// </summary>
        //public virtual void JsonInitialize() { }

        /// <summary>
        /// When overridden in a derived class should do any final wiring prior to
        /// render. This method is intended to serve as the main entry point into the wiring
        /// of JsonControls who need information from other JsonControls. This method will be 
        /// called by the JavascriptPage.  
        /// </summary>
        public virtual void WireScriptsAndValidate() { }

        internal void EnsureInitScript()
        {
            string key = this.GetType().Name + "Init.js";
            if (Resources.JavaScriptFriendlyNamesToQualifiedScriptPath.ContainsKey(key))
            {
                string longKey = Resources.JavaScriptFriendlyNamesToQualifiedScriptPath[key];
                if (!string.IsNullOrEmpty(longKey))
                {
                    string scriptText = Resources.JavaScript[longKey];
                    Type thisType = this.GetType();
                    foreach (string varName in BoxVariable.GetPropertyVariables(thisType))
                    {
                        string propertyName = varName.Replace(BoxServer.VariablePrefix, "").Replace(BoxServer.VariableSuffix, "");
                        PropertyInfo prop = thisType.GetProperty(propertyName);

                        object objValue = prop.GetValue(this, null);
                        string value = objValue == null ? "" : objValue.ToString();

                        if (prop.PropertyType == typeof(DateTime))
                        {
                            DateTime dateTime = (DateTime)objValue;
                            value = dateTime.ToShortDateString() + " " + dateTime.ToLongTimeString();
                        }
                        
                        scriptText = scriptText.Replace(varName, value);
                    }

                    JsonFunction initScript = new JsonFunction();
                    initScript.Prepend = string.Format("JSUI.{0} = {1}{2};", this.JsonId, "{", "}");
                    initScript.FunctionBody = scriptText;
                    this.AddJsonFunction(initScript);
                }
            }
        }

        public JavascriptPage ParentJavascriptPage
        {
            get
            {
                return Page as JavascriptPage;                
            }
        }
        
        internal void RegisterRequiredScripts()
        {
            if (AutoRegisterScript)
            {
                this.AddRequiredScript(this.ResourceScriptName); // register the js file associated with the current instance type
            }

            foreach (string scriptName in requiredScriptFileNames)
            {
                if (this.ParentJavascriptPage == null)
                    throw new JsonInvalidOperationException("Parent page must be a JavascriptPage");

                this.ParentJavascriptPage.JavascriptResourceManager.AddResourceHeaderScript(scriptName);
                //JavascriptServer.RegisterResourceScriptByName(Page, scriptName, this.GetType().Assembly);
            }
        }

        protected string jsonId;
        protected string randomJsonId;
        /// <summary>
        /// The unique json identifier for this control.  If one is not specified a random 
        /// 8 character JsonId will be generated.
        /// </summary>
        [Description(@"The unique json identifier for this control.  If one is not specified a random
            8 character JsonId will be generated")]
        public virtual string JsonId
        {
            get
            {
                if (string.IsNullOrEmpty(jsonId))
                    return randomJsonId;

                return jsonId;
            }
            set
            {
                this.jsonId = value;
            }
        }

        [JsonIgnore]
        public JsonAttributes Attributes
        {
            get
            {
                return attributes;
            }
        }

        [JsonIgnore]
        public JsonStyles Styles
        {
            get
            {
                return styles;
            }
        }

        /// <summary>
        /// Gets or sets a $ delimited string of key value pairs separated by =.
        /// </summary>
        public virtual string ElementAttributes
        {
            get;
            set;
        }

        protected void ApplyElementAttributes()
        {
            if (!string.IsNullOrEmpty(this.ElementAttributes))
            {
                Dictionary<string, string> elementAttributeDictionary =
                    StringExtensions.ToDictionary(
                        this.ElementAttributes,
                        new string[] { "$", ";" }, new string[] { "=", ":" });//DictionaryExtensions.FromDelimited(this.ElementAttributes, "$", "=");
                foreach (string attribute in elementAttributeDictionary.Keys)
                {
                    this.Attributes.SetAttribute(attribute, elementAttributeDictionary[attribute]);
                }
            }
        }

        [JsonIgnore]
        public virtual string CssStyle
        {
            get
            {
                return styles.ToString();
            }
            set
            {
                styles = JsonStyles.FromString(value);
                //styles.Clear();
                //Dictionary<string, string> styleDictionary = StringExtensions.ToDictionary(value, ";", ":");
                //foreach (string styleKey in styleDictionary.Keys)
                //{
                //    styles.SetStyle(styleKey, styleDictionary[styleKey]);
                //}
            }
        }


        internal HtmlGenericControl ControlToRender
        {
            get
            {
                return controlToRender;
            }
        }

        public virtual string CssClass { get; set; }
        
        [Description("The Document Object Model Id of the most significant element rendered by the control")]
        public string DomId
        {
            get;
            set;
        }

        public string AspId
        {
            get
            {
                return this.ID;
            }
            set
            {
                this.ID = value;
            }
        }

        /// <summary>
        /// Used for client side json serialization.
        /// </summary>
        public virtual string JsonProperty
        {
            get;
            set;
        }

        public string Value { get; set; }

        bool autoRegister;
        /// <summary>
        /// If true the script file with the same name as the current control will be 
        /// registered on the current page.  The script file must be compiled as an
        /// embedded resource in the current assembly.
        /// </summary>
        public bool AutoRegisterScript
        {
            get
            {
                return this.autoRegister;
            }
            set
            {
                this.autoRegister = value;
                if (value)
                {
                    this.AddRequiredScript(this.ResourceScriptName);
                }
            }
        }

        public string[] RequiredScripts
        {
            get
            {
                return requiredScriptFileNames.ToArray();
            }
        }

        /// <summary>
        /// Gets the resource script name of the current instance.  This will be in the 
        /// form [namespace].[classname].js.  The actual path to the embedded resource
        /// must match the namespace path.
        /// </summary>
        public string ResourceScriptName
        {
            get
            {
                Type currentType = this.GetType();
                return GetScriptName(currentType);
            }
        }

        private string GetScriptName(Type type)
        {
            return type.Namespace.ToLowerInvariant() + "." + type.Name.ToLowerInvariant() + ".js";
        }

        public void AddRequiredScript(Type type)
        {
            this.AddRequiredScript(GetScriptName(type));
        }

        /// <summary>
        /// Notifies the page that the specified script file is required by the current control
        /// and should be registered on the client.  The script file should be compiled as
        /// an embedded resource in the same assembly as the control that extends JsonControl.
        /// </summary>
        /// <param name="scriptFileNameCompiledAsEmbeddedResourceInCurrentAssembly"></param>
        public void AddRequiredScript(string scriptFileNameCompiledAsEmbeddedResourceInCurrentAssembly)
        {
            if (!requiredScriptFileNames.Contains(scriptFileNameCompiledAsEmbeddedResourceInCurrentAssembly))
                requiredScriptFileNames.Add(scriptFileNameCompiledAsEmbeddedResourceInCurrentAssembly);
        }

        public string Text
        {
            get;
            set;
        }


        /// <summary>
        /// Adds any styles and attributes to the controlToRender member.  Responsibility for rendering to the output
        /// stream should be taken by the derived class.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            ApplyAttributesAndStyles(controlToRender, styles, attributes, CssClass);
        }

        protected static void ApplyAttributesAndStyles(HtmlGenericControl targetControl, JsonStyles styles, JsonAttributes attributes)
        {
            ApplyAttributesAndStyles(targetControl, styles, attributes, string.Empty);
        }

        protected static void ApplyAttributesAndStyles(HtmlGenericControl targetControl, JsonStyles styles, JsonAttributes attributes, string cssClass)
        {
            attributes.AddAttributes(targetControl);
            styles.AddStyles(targetControl);
            if (!string.IsNullOrEmpty(cssClass))
                targetControl.Attributes.Add("class", cssClass);
        }

        public virtual void RenderConglomerateScript(HtmlTextWriter writer)
        {
            RenderConglomerateScript(writer, true);
        }

        /// <summary>
        /// If true, will cause a call to RenderCongolemerateScript to switch all JsonFunctions
        /// whose ExecutionType is set to OnWindowLoad to OnParse.  This is used to execute
        /// scripts required by Box controls.
        /// </summary>
        internal bool PostWindowLoad { get; set; }

        /// <summary>
        /// Don't want to make many/any changes to this method because the DataBox framework
        /// is heavily dependent on this.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="includeScriptTags"></param>
        internal virtual void RenderConglomerateScript(HtmlTextWriter writer, bool includeScriptTags)
        {            
            Dictionary<JavascriptExecutionTypes, List<JsonFunction>> sortedFunctions = new Dictionary<JavascriptExecutionTypes, List<JsonFunction>>();
            
            if (!includeScriptTags)
            {
                UsingResourceScriptChain boxedScript = new UsingResourceScriptChain();
                //List<AnonymousJsFunction> usingChain = new List<AnonymousJsFunction>();
                foreach (string scriptName in this.RequiredScripts)
                {
                    boxedScript.AddScript(scriptName);
                    /*
                     * JSUI.Conf.usingResource([scriptName1, scriptName2...], function(origArr){
                     *  BODY
                     * });
                     */
                }
                foreach (JsonFunction function in this.Scripts)
                {
                    function.IncludeScriptTags = false;
                    if (this.PostWindowLoad && function.ExecutionType == JavascriptExecutionTypes.OnWindowLoad)
                        function.ExecutionType = JavascriptExecutionTypes.OnParse;

                    boxedScript.FunctionBody += ControlHelper.GetRenderedString(function);
                }
                boxedScript.FunctionBody = boxedScript.FunctionBody.Replace("\r\n", "\r\n\t");
                boxedScript.RenderControl(writer);
            }
            else
            {
                foreach (JsonFunction function in this.Scripts)
                {
                    function.IncludeScriptTags = includeScriptTags;
                    if (this.PostWindowLoad && function.ExecutionType == JavascriptExecutionTypes.OnWindowLoad)
                        function.ExecutionType = JavascriptExecutionTypes.OnParse;

                    function.RenderControl(writer);

                }
            }
        }

        public override string ToString()
        {
            string parentId = this.Parent == null ? "" : this.Parent.ID;
            return string.Format("{3}: [jsonid: '{0}', aspid: '{1}', parent aspid: '{2}']", this.JsonId, this.AspId, parentId, this.GetType().Name);
        }
    }
}
