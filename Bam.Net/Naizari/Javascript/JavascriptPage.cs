/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using Naizari.Logging;
using Naizari.Javascript;
using System.Web.UI.HtmlControls;
using System.Reflection;
using Naizari.Extensions;
using Naizari.Javascript.BoxControls;
using Naizari.Javascript.JsonControls;
using Naizari.Images;
using System.Threading;
using Naizari.Helpers.Web;
using Naizari.Helpers;

namespace Naizari.Javascript
{
    public class JavascriptPage: Page
    {
        Dictionary<string, JsonControl> jsonControls;
        Dictionary<string, JsonControl> jsonControlsByDomId;

        /// <summary>
        /// Create a new instance of a JavascriptPage page
        /// </summary>
        public JavascriptPage():base()
        {
            //this.EnableViewState = false;
            jsonControls = new Dictionary<string, JsonControl>();
            jsonControlsByDomId = new Dictionary<string, JsonControl>();
            FindJavascriptControls = false;
            JavascriptResourceManager = new JavascriptResourceManager(this);            
#if DEBUG
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.debug.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.qunit.js");
#endif
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsondata.js");
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.aes.js");
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsondatetime.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsonserializer.js");
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsonsearcher.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.popper.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.arrowlist.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.cookies.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.databox.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.boxmgr.js");
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.callbacks.js");
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsoninvoker.js");
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsoninputtoggler.js");
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsoninput.js");
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.animation.js"); // useless.  concept moved to Effects.animate
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.scrollable.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.effects.js");
            //JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsonfunction.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.msajaxinterop.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.events.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.dommgr.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsonproxy.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.sha1.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.conf.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jsui.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jquery-ui.js");
            JavascriptResourceManager.AddCoreScript("naizari.javascript.jsui.jquery.js");
        }

        public override bool EnableEventValidation
        {
            get
            {
                return false;
            }
            set
            {
                //
            }
        }

        protected override void OnInit(EventArgs e)
        {
            // This allows a developer to place JsonMethod attributes on 
            // methods inside their code behind to an aspx page
            if (!tickling)
            {
                JavascriptServer.RegisterProvider(this, this);
                JavascriptServer.RegisterProvider(typeof(JsLogger), this);
            }
            base.OnInit(e);
        }

        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
        }

        bool tickling;
        /// <summary>
        /// Causes the page to artificially call the page lifecycle.  
        /// To be used by the BoxServer only.  Access modifier will be
        /// changed after testing.
        /// </summary>
        [Obsolete(
@"This method may cause stack overflow in some cases, its existence is due to BoxServer
testing but it is not currently used")]
        public void Tickle()
        {
            Tickle(false);
        }
        /// <summary>
        /// Causes the page to artificially call the page lifecycle.  
        /// To be used by the BoxServer only.  Access modifier will be
        /// changed after testing.
        /// </summary>
        [Obsolete(
@"This method may cause stack overflow in some cases, its existence is due to BoxServer
testing but it is not currently used")]
        public void Tickle(bool doPostPreRender)
        {
            if (tickling)
                return;

            tickling = true;

            this.OnPreInit(new EventArgs());
            this.OnInit(new EventArgs());
            this.OnInitComplete(new EventArgs());
            this.OnPreLoad(new EventArgs());
            this.OnLoad(new EventArgs());
            this.OnLoadComplete(new EventArgs());
            this.OnPreRender(new EventArgs());
            if (doPostPreRender)
            {
                this.OnSaveStateComplete(new EventArgs());
                this.OnUnload(new EventArgs());
            }

            tickling = false;
        }

        public bool IncludeMessageBox { get; set; }
        public string MessageBoxTitle { get; set; }
        public string MessageBoxCssClass { get; set; }
        public string MessageBoxTitleCssClass { get; set; }
        public string MessageBoxTextCssClass { get; set; }

        /// <summary>
        /// Provides access to all JsonFunction class instances that
        /// have been added to the current page using the JsonId as a key.
        /// </summary>
        /// <param name="functionJsonId">The JsonId of the JsonFunction to retrieve.</param>
        /// <returns>JsonFunction</returns>
        public JsonFunction this[string functionJsonId]
        {
            get
            {
                return JavascriptResourceManager[functionJsonId];
            }
        }

        public JsonControl[] JsonControls
        {
            get
            {
                return DictionaryExtensions.ValuesToArray<string, JsonControl>(jsonControls);
            }
        }

        

        protected override void OnPreRender(EventArgs e)
        {
            AddMessageBox();

            if (FindJavascriptControls)
            {
                FillJsonControlDictionary(this, jsonControls);
            }

            WireJsonControls();


            JavascriptResourceManager.WritePageScripts(); 

            base.OnPreRender(e);
        }

        bool wiring;
        internal void WireJsonControls()
        {
            WireJsonControls(true);
        }

        internal void WireJsonControls(bool registerRequiredScripts)
        {
            
            List<JsonCallback> deffered = new List<JsonCallback>();
            foreach (JsonControl scriptControl in jsonControls.Values)
            {
                if (!tickling)
                {
                    if (registerRequiredScripts)
                        scriptControl.RegisterRequiredScripts();

                    if (scriptControl is JsonCallback)
                    {
                        deffered.Add(scriptControl as JsonCallback);
                    }
                    else
                    {
                        //scriptControl.JsonInitialize();
                        wiring = true;
                        scriptControl.WireScriptsAndValidate();
                        scriptControl.EnsureInitScript();
                        wiring = false;
                    }
                }
            }

            foreach(JsonCallback callback in deffered)
            {
                if(!tickling)
                {
                    if(registerRequiredScripts)
                        callback.RegisterRequiredScripts();

                    //callback.JsonInitialize();
                    wiring = true;
                    callback.WireScriptsAndValidate();
                    wiring = false;
                }
            }
        }

        private void AddMessageBox()
        {
            if (IncludeMessageBox)
            {
                JsonMessageBox messageBox = new JsonMessageBox();
                if (!string.IsNullOrEmpty(MessageBoxCssClass))
                    messageBox.BoxCssClass = MessageBoxCssClass;
                if (!string.IsNullOrEmpty(MessageBoxTitleCssClass))
                    messageBox.TitleCssClass = MessageBoxTitleCssClass;
                if (!string.IsNullOrEmpty(MessageBoxTextCssClass))
                    messageBox.MessageCssClass = MessageBoxTextCssClass;
                if (!string.IsNullOrEmpty(MessageBoxTitle))
                    messageBox.Title = MessageBoxTitle;
                Controls.Add(messageBox);
                AddJsonControl(messageBox);
            }
        }

        int pageTimeout;
        public int PageTimeout 
        {
            get
            {
                if (pageTimeout == 0)
                    return HttpContext.Current.Session.Timeout;

                return this.pageTimeout;
            }
            set
            {
                this.pageTimeout = value;
                HttpContext.Current.Session.Timeout = value;
            }
        }

        public string PageTimeoutMessage
        {
            get;
            set;
        }

        private void AddProviderInfo()
        {
            if (this.Form != null)
                this.Form.Controls.Add(JavascriptResourceManager.CreateScriptControl(HttpContextHelper.PageUrl + "?providerinfo"));
        }

        private void AddSessionTimeOut()
        {
            JsonSessionTimeOut timeout = new JsonSessionTimeOut(this.PageTimeout);
            if (!string.IsNullOrEmpty(PageTimeoutMessage))
                timeout.ExpireMessage = PageTimeoutMessage;

            this.AddJsonControl(timeout);
            if (this.Form != null)
                this.Form.Controls.Add(timeout);
            else
                this.Controls.Add(timeout);
        }

        internal static void FillJsonControlDictionary(Page page, Dictionary<string, JsonControl> listToFill)
        {
            foreach (Control control in page.Controls)
            {
                if (control is JsonControl && !listToFill.ContainsValue((JsonControl)control))
                {
                    JsonControl toAdd = (JsonControl)control;
                    listToFill.Add(toAdd.JsonId, toAdd);
                }
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            if (PixelServer.ProcessPixelRequest(HttpContext.Current))
                return;
            base.OnPreInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (JavascriptServer.ProcessRequest(HttpContext.Current))
                return;
            
            //if (PixelServer.ProcessRequest(HttpContext.Current))
            //    return;
            AddProviderInfo();
            AddSessionTimeOut();
            base.OnLoad(e);            
        }

        public static List<JsonControl> GetAllJsonControls(Control parent)
        {
            return BoxUserControl.GetAllJsonControls(parent);
        }

        /// <summary>
        /// Intended to register, by name, any dependent javascripts that have been
        /// compiled into the JSUI folder.  Documentation on 
        /// what these scripts are will come when time allows.
        /// </summary>
        /// <param name="coreScriptName"></param>
        internal void RegisterCoreJavaScript(string coreScriptName)
        {
            JavascriptResourceManager.AddCoreScript(coreScriptName);
        }

        /// <summary>
        /// Intended to register, by name, a script that has been compiled as an embedded
        /// resource.
        /// </summary>
        /// <param name="resourceQualifiedScriptName"></param>
        public void RegisterResourceHeaderScript(string resourceQualifiedScriptName)
        {
            this.RegisterResourceHeaderScript(resourceQualifiedScriptName, Assembly.GetExecutingAssembly());
        }

        public void RegisterResourceHeaderScript(string resourceQualifiedScriptName, Assembly assemblyToLoadFrom)
        {
            Resources.Load(assemblyToLoadFrom);
            JavascriptResourceManager.AddResourceHeaderScript(resourceQualifiedScriptName);
        }

        public void RegisterResourceBodyScript(string resourceQualifiedScriptName)
        {
            this.RegisterResourceBodyScript(resourceQualifiedScriptName, Assembly.GetExecutingAssembly());
        }

        public void RegisterResourceBodyScript(string resourceQualifiedScriptName, Assembly assemblyToLoadFrom)
        {
            Resources.Load(assemblyToLoadFrom);
            JavascriptResourceManager.AddResourceBodyScript(resourceQualifiedScriptName);
        }

        //public void RegisterResourceScript
        /// <summary>
        /// This is a convenience method intended to register scripts within a website project.
        /// </summary>
        /// <param name="path"></param>
        public void RegisterSiteHeaderJavascript(string pathRelativeToApplicationRoot)
        {
            string path = pathRelativeToApplicationRoot;
            if(!path.StartsWith("/"))
                path = "/" + path;

            JavascriptResourceManager.AddSiteHeaderScript(path);
        }

        public void RegisterSiteBodyJavascript(string pathRelativeToApplicationRoot)
        {
            string path = pathRelativeToApplicationRoot;
            if (!pathRelativeToApplicationRoot.StartsWith("/"))
                path = "/" + path;

            JavascriptResourceManager.AddSiteBodyScript(path);
        }

        object addJsonControlLock = new object();
        /// <summary>
        /// Prepares scripts required by the specified control for 
        /// registration on the client.  This will not add the 
        /// control to the Controls collection.
        /// </summary>
        /// <param name="control">The JavascriptControl to add</param>
        public void AddJsonControl(JsonControl control)
        {
            lock (addJsonControlLock)
            {
                if (wiring)
                    throw new JsonInvalidOperationException(string.Format("Can't add JsonControls during WireScriptsAndValidate phase."));

                if (jsonControls.ContainsKey(control.JsonId))
                    return;

                jsonControls.Add(control.JsonId, control);
                if (!jsonControlsByDomId.ContainsKey(control.DomId))
                    jsonControlsByDomId.Add(control.DomId, control);
                // commented this since this is an uninteresting event.  When this happens the only thing that will be affected is
                // finding JsonControls by DomId.  One should always use the JsonId to retrieve controls unless there is a specific
                // reason not to.
                //else if (jsonControlsByDomId[control.DomId] != control)
                //{
                //    LogManager.CurrentLog.AddEntry("Attempted to add JsonControl ({0}) but the control ({1}) has the same DomId.\r\n\r\n*** Stack Trace ***\r\n{2}", new string[] { control.ToString(), jsonControlsByDomId[control.DomId].ToString(), Environment.StackTrace });
                //}
                    //throw new JsonInvalidOperationException(string.Format("Attempted to add JsonControl ({0}) but the control ({1}) has the same DomId.", control.ToString(), jsonControlsByDomId[control.DomId].ToString()));
            }
        }

        public JsonCallback FindCallback(string jsonId)
        {
            return FindCallback(jsonId, false);
        }

        public IJsonControl FindJsonControl(string jsonId)
        {
            return FindJsonControlAs<JsonControl>(jsonId);
        }

        //public void RefreshIds()
        //{
        //    Dictionary<string, JsonControl> newControls = new Dictionary<string, JsonControl>();
        //    foreach (JsonControl control in jsonControls.Values)
        //    {
        //        newControls.Add(control.JsonId, control);
        //    }
        //    this.jsonControls = newControls;
        //}

        public T[] FindAllJsonControlsOfGenericType<T>() where T : JsonControl
        {
            List<T> results = new List<T>();
            foreach (JsonControl control in jsonControls.Values)
            {
                if (control is T)
                    results.Add((T)control);
            }

            return results.ToArray();
        }

        public JsonControl[] FindAllJsonControlsOfConcreteType(Type type)
        {
            List<JsonControl> results = new List<JsonControl>();
            foreach (JsonControl control in jsonControls.Values)
            {
                if (control.GetType().Equals(type))
                    results.Add(control);
            }

            return results.ToArray();
        }

        public T FindJsonControlAs<T>(string jsonId) where T : JsonControl
        {
            T retVal = default(T);

            if (string.IsNullOrEmpty(jsonId))
                return retVal;
            
            if (jsonControls.ContainsKey(jsonId))
            {
                retVal = jsonControls[jsonId] as T;
                if (retVal != null)
                    return retVal;
            }

            if (this.Master != null)
            {
                foreach (Control control in this.Master.Controls)
                {
                    if (control is JsonControl)
                    {
                        JsonControl jsonControl = (JsonControl)control;
                        if (jsonControl.JsonId.Equals(jsonId))
                        {
                            retVal = jsonControl as T;
                            if (retVal != null)
                                return retVal;
                        }
                    }
                }
            }

            return retVal;
        }

        public T FindJsonControlByDomIdAs<T>(string domId) where T : JsonControl
        {
            T retVal = default(T);
            if (string.IsNullOrEmpty(domId))
                return retVal;

            if (jsonControlsByDomId.ContainsKey(domId))
            {
                return jsonControlsByDomId[domId] as T;
            }

            return retVal;
        }

        public static HtmlGenericControl CreateDiv(string divId)
        {
            return CreateDiv(divId, "");
        }

        public static HtmlGenericControl CreateDiv(string divId, string cssClassName)
        {
            return CreateDiv(divId, cssClassName, string.Empty);
        }

        public static HtmlGenericControl CreateDiv(string divId, string cssClassName, string jsonId)
        {            
            HtmlGenericControl divToReturn = new HtmlGenericControl("div");
            divToReturn.ID = divId;
            if (!string.IsNullOrEmpty(cssClassName))
                divToReturn.Attributes.Add("class", cssClassName);

            if (!string.IsNullOrEmpty(jsonId))
                divToReturn.Attributes.Add("jsonid", jsonId);

            return divToReturn;
        }

        public T GetJsonControlFromControlsCollectionAs<T>(string jsonId) where T : JsonControl
        {
            T retVal = default(T);
            foreach (Control control in this.Controls)
            {
                retVal = control as T;
                if (retVal != null)
                {
                    if (retVal.JsonId.Equals(jsonId))
                        return retVal;
                }
            }
            return retVal;
        }
        public JsonCallback FindCallback(string jsonId, bool searchControlsCollection)
        {
            JsonCallback callback = FindJsonControlAs<JsonCallback>(jsonId);
            if (callback != null)
                return callback;

            if (searchControlsCollection)
            {
                callback = GetJsonControlFromControlsCollectionAs<JsonCallback>(jsonId);
            }

            return callback;
        }

        /// <summary>
        /// Retrieves the JsonInvoker with the specified jsonId.
        /// </summary>
        /// <param name="jsonId"></param>
        /// <returns></returns>
        public JsonInvoker FindInvoker(string jsonId)
        {
            return FindInvoker(jsonId, false);
        }

        /// <summary>
        /// Finds a JsonInvoker instance on the current page with the specified JsonId.
        /// </summary>
        /// <param name="jsonId">The JsonId of the JsonInvoker to return</param>
        /// <param name="searchControlsCollection">If true all controls in the Controls collection
        /// will be searched if the target isn't found in the JsonControls</param>
        /// <returns></returns>
        public JsonInvoker FindInvoker(string jsonId, bool searchControlsCollection)
        {
            JsonInvoker invoker = FindJsonControlAs<JsonInvoker>(jsonId);
            if (invoker != null)
                return invoker;

            if (searchControlsCollection)
            {
                invoker = GetJsonControlFromControlsCollectionAs<JsonInvoker>(jsonId);
            }

            return invoker;
        }

        internal JavascriptResourceManager JavascriptResourceManager { get; set; }

        /// <summary>
        /// Get or sets whether to search for any javascript controls in the current
        /// page so that their required scripts can be registered.  This is never
        /// necessary if all Javascript controls are added to the page using
        /// the AddJsonControl method.
        /// </summary>
        public bool FindJavascriptControls { get; set; }

        internal static JsonFunction CreateScript(string scriptText)
        {
            JsonFunction function = new JsonFunction();
            function.ExecutionType = JavascriptExecutionTypes.OnParse;
            function.FunctionBody = scriptText;
            return function;
        }
    }
}
