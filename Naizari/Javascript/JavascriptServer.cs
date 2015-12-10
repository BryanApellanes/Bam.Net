/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Naizari;
using System.Web;
using System.Web.UI;
using Naizari.Extensions;
using Naizari.Logging;
using Naizari.Javascript.JsonControls;
using Naizari.Javascript.BoxControls;
using Naizari.Helpers;
using System.Web.Script.Serialization;
using System.IO;
using Naizari.Test;
using JsonExSerializer;
using Naizari.Configuration;
using Naizari.Data.Common;

namespace Naizari.Javascript
{
    public class JavascriptServer
    {
        public class JsonProviderInfo
        {
            public JsonProviderInfo(object provider)
            {
                this.Provider = provider;
            }

            public string VarName
            {
                get
                {
                    if (this.Provider != null)
                        return JavascriptServer.GetDefaultVarName(this.Provider.GetType());

                    return string.Empty;
                }
            }
            public string Namespace
            {
                get
                {
                    return this.ProviderType != null ? this.ProviderType.Namespace: string.Empty;
                }
            }

            [JsonIgnore]
            public Type ProviderType
            {
                get
                {
                    if (this.Provider != null)
                        return this.Provider.GetType();

                    return null;
                }
            }

            [JsonIgnore]
            public object Provider { get; set; }

            public string ClientTypeName
            {
                get
                {
                    if (this.Provider != null)
                        return JavascriptServer.GetClientTypeName(this.Provider.GetType());

                    return string.Empty;
                }
            }
        }

        class JsonProviders
        {
            Dictionary<string, JsonProviderInfo> providersByTypeName;
            Dictionary<string, JsonProviderInfo> providersByVarName;

            
            public JsonProviders() 
            {
                this.providersByTypeName = new Dictionary<string, JsonProviderInfo>();
                this.providersByVarName = new Dictionary<string, JsonProviderInfo>();
            }

            object providerLock = new object();
            public JsonProviderInfo Add(object provider)
            {
                JsonProviderInfo info = new JsonProviderInfo(provider);
                JsonProviderInfo retVal = info;
                lock (providerLock)
                {
                    if (providersByVarName.ContainsKey(info.VarName))
                    {
                        JsonProviderInfo registered = providersByVarName[info.VarName];
                        if (registered.ProviderType != info.ProviderType && !info.Namespace.StartsWith("ASP"))
                        {
                            throw ExceptionHelper.CreateException<JsonInvalidOperationException>(
                                @"JsonProxy Var Name Already In Use: {0}, Type: {1}
                        Failed To Register: {0}, Type: {2}", registered.VarName, registered.ClientTypeName, info.VarName, info.ClientTypeName);
                        }
                        else if (info.Namespace.StartsWith("ASP"))
                        {
                            this.providersByTypeName[info.ClientTypeName] = info;
                            this.providersByVarName[info.VarName] = info;
                            retVal = info;
                        }
                        else
                        {
                            retVal = registered;
                        }
                    }
                    else
                    {
                        this.providersByVarName.Add(info.VarName, info);
                        this.providersByTypeName.Add(info.ClientTypeName, info);
                    }
                }

                return retVal;
            }

            public JsonProviderInfo[] ProviderInfos
            {
                get
                {
                    return DictionaryExtensions.ValuesToArray<string, JsonProviderInfo>(this.providersByVarName);
                }
            }

            public bool ContainsKey(string typeOrVarName)
            {
                Dictionary<string, JsonProviderInfo> ignore;
                return ContainsKey(typeOrVarName, out ignore);
            }

            private bool ContainsKey(string typeOrVarName, out Dictionary<string, JsonProviderInfo> container)
            {
                if(this.providersByTypeName.ContainsKey(typeOrVarName))
                {
                    container = this.providersByTypeName;
                    return true;
                }
                else
                {                    
                    bool retVal = this.providersByVarName.ContainsKey(typeOrVarName);
                    if(retVal)
                    {
                        container = this.providersByVarName;
                        return retVal;
                    }
                }

                container = null;
                return false;
            }

            public object this[string typeOrVarName]
            {
                get
                {
                    Dictionary<string, JsonProviderInfo> container;
                    if(this.ContainsKey(typeOrVarName, out container))
                    {
                        return container[typeOrVarName].Provider;
                    }
                    return null;
                }
            }           
        }

        public const string ParameterDelimiter = "$#%";

        public static void ProcessRequest(HttpContext context, HttpExceptionHandler exceptionHandler)
        {
            if (!ProcessRequest(context))
                exceptionHandler(context.ApplicationInstance.Server.GetLastError() as HttpException);
        }

        public static bool IsResourceScriptRequest(HttpRequest request, out string scriptName)
        {
            scriptName = request.QueryString["script"];
            return !string.IsNullOrEmpty(scriptName);
        }

        public static bool ProcessRequest(HttpContext context)
        {

            HttpRequest request = context.Request;
            try
            {
                if (context == null)
                    throw NewInvalidOperationException();

                Resources.Load(Assembly.GetExecutingAssembly());

                if (IsProviderInfoRequest(request))
                {
                    return SendProviderInfo(context);
                }

                if (IsInitRequest(request))
                {
                    // is this an init request
                    // ?init={typename}&varname={clientInstanceName}
                    string initType = request.QueryString["t"];
                    string varname = request.QueryString["varname"];
                    if (!string.IsNullOrEmpty(initType) &&
                        !string.IsNullOrEmpty(varname))
                    {
                        return SendClientInstantiationScript(context, initType, varname);
                    }
                }

                // this is a resource script request
                // this component doesn't use the standard axd style resource loading 
                // mechanism. 
                string scriptName;
                if (IsResourceScriptRequest(request, out scriptName))
                {
                    return SendRequestedScript(context, scriptName);
                }

                if (IsInvokeRequest(request))
                {
                    JsonResponseFormats format = JsonResponseFormats.invalid;

                    // is this an invoke request
                    // ?t={type}&m={method}&params={delimited}
                    string typeName = request.QueryString["t"];
                    string methodName = request.QueryString["m"];
                    string formatString = request.QueryString["f"];
                    
                    if (!string.IsNullOrEmpty(formatString))
                    {
                        EnumExtensions.TryParseAs<JsonResponseFormats>(formatString, out format);
                    }

                    if (format == JsonResponseFormats.invalid)
                        format = JsonResponseFormats.json;

                    if (!string.IsNullOrEmpty(typeName) &&
                        !string.IsNullOrEmpty(methodName))
                    {
                        return SendInvokeResult(context, request, typeName, methodName, format);
                    }
                }

                string boxPath;
                if (IsBoxRequest(request, out boxPath))
                {
                    SendBoxResponse(context, boxPath);
                    return true;
                }

                if (IsDataBoxScriptRequest(request))
                {
                    SendDataBoxScriptResponse(context);
                    return true;
                }
            }
            catch (Exception ex)
            {
                string url = request.Url.PathAndQuery;
                Logging.LogManager.CurrentLog.AddEntry("An error of type ({0}) occurred processing javascript request: {1}", ex, new string[] { ex.GetType().Name, url });
            }

            return false;
        }

        private static void SendBoxResponse(HttpContext context, string boxPath)
        {
            BoxServer.SendBoxResponse(context, boxPath);
        }

        private static void SendDataBoxScriptResponse(HttpContext context)
        {
            BoxServer.SendDataBoxScriptResponse(context);
        }

        public static bool IsInitRequest(HttpRequest request)
        {
            return !string.IsNullOrEmpty(request.QueryString["init"]);
        }

        public static bool IsProviderInfoRequest(HttpRequest request)
        {
            return request.QueryString["providerinfo"] != null || request.RawUrl.EndsWith("providerinfo");
        }

        public static bool IsInvokeRequest(HttpRequest request)
        {
            return !string.IsNullOrEmpty(request.QueryString["inv"]) &&
                                request.QueryString["inv"].Equals("true");
        }

        public static bool IsBoxRequest(HttpRequest request)
        {
            string ignore = null;
            return IsBoxRequest(request, out ignore);
        }

        public static bool IsBoxRequest(HttpRequest request, out string virtualBoxPath)
        {
            virtualBoxPath = request.QueryString["box"];
            return !string.IsNullOrEmpty(virtualBoxPath);
        }

        public static bool IsDataBoxScriptRequest(HttpRequest request)
        {
            return !string.IsNullOrEmpty(request.QueryString["dbs"]); // TODO: enumify magic strings and document
        }

        private static bool SendProviderInfo(HttpContext context)
        {
            HttpResponse response = context.Response;
            JsonProviders providers = SingletonHelper.GetSessionSingleton<JsonProviders>();
            string providerInfoJson = JsonSerializer.ToJson(providers.ProviderInfos);
            string providerInfoScript = @"JSUI.providerInfo = " + providerInfoJson +";\r\n";
            providerInfoScript += "JSUI.providerInfo = JSUI.toDictionary(\"VarName\", JSUI.providerInfo);";
            response.Clear();
            response.Write(providerInfoScript);
            response.Flush();
            response.SuppressContent = true;
            return true;
        }

        private static bool SendClientInstantiationScript(HttpContext context, string initType, string varname)
        {
            HttpResponse response = context.Response;

            response.Clear();
            response.Write(GetClientInstantiationScript(varname, GetType(initType)));
            response.Flush();
            response.SuppressContent = true;
            return true;
        }

        private static bool SendInvokeResult(HttpContext context, HttpRequest request, string typeName, string methodName)
        {
            return SendInvokeResult(context, request, typeName, methodName, JsonResponseFormats.json);
        }

        private static bool SendInvokeResult(HttpContext context, HttpRequest request, string typeName, string methodName, JsonResponseFormats format)
        {
            if (format == JsonResponseFormats.invalid)
                throw new InvalidOperationException("The specified response format requested was invalid.");

            string[] parameters = null;
            if (request.QueryString["params"] != null)
            {
                parameters = HttpUtility.UrlDecode(request.QueryString["params"]).Split(new string[] { ParameterDelimiter }, StringSplitOptions.RemoveEmptyEntries);
                List<string> newParameters = new List<string>();
                foreach(string parameter in parameters)
                {
                    // if for some strange reason someone enters this sequence of characters the javascript will reverse and replace the sequence
                    // here we're un-reversing
                    string reversed = StringExtensions.Reverse(ParameterDelimiter);
                    newParameters.Add(parameter.Replace(reversed, ParameterDelimiter)); 
                }
                parameters = newParameters.ToArray();
            }

            HttpResponse response = context.Response;

            JsonResult result = GetJsonResultFromInvoke(typeName, methodName, parameters);

            switch (format)
            {
                case JsonResponseFormats.json:
                    response.Clear();
                    response.Write(JsonSerializer.ToJson(result));
                    response.Flush();
                    response.SuppressContent = true; // necessary to suppress html that may have been added to the response.
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //response.End();
                    break;
                case JsonResponseFormats.box:

                    BoxServer.SendDataBoxResponse(context, result);
                    break;
            }            

            return true;
        }

        

        private static bool SendRequestedScript(HttpContext context, string scriptName)
        {
            HttpResponse response = context.Response;
            HttpRequest request = context.Request;
            string script = string.Empty;
            if (Resources.JavaScript.ContainsKey(scriptName))
            {
                script = Resources.JavaScript[scriptName];
                script += "\r\n;try{JSUI.Conf.LoadedScripts[\"" + scriptName + "\"] = \"" + request.RawUrl + "\";}catch(e){};";//, scriptName, request.RawUrl + "?" + request.QueryString);
            }
            else
            {
                //script = "\r\nalert(\"The requested script '" + scriptName + "' was not found.\");";
                string message = string.Empty;
#if DEBUG
                message = "The requested script '" + scriptName + "' was not found.\");";
#else
                message = @"There was an error that occurred on the server. 
                We are working to correct the problem.  Please restart your browser.";
#endif
                string errorScript = "\r\nif(MessageBox !== undefined)\r\n";
                errorScript += string.Format("\tMessageBox.Show('{0}');\r\n", message);
                errorScript += "else";
                errorScript += string.Format("\talert('{0}');", message);
                LogManager.CurrentLog.AddEntry("Failed to retrieve script by resource name: [scriptName: {0}]", LogEventType.Warning, scriptName);
            }

            string callBack = request.QueryString["cb"];
            if (!string.IsNullOrEmpty(callBack))
                script += ";" + callBack;

            response.Write(script);
            response.Flush();
            response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            return true;
        }

        private static JsonResult GetJsonResultFromInvoke(string typeName, string methodName, string[] parameters)
        {
            Expect.IsNotNullOrEmpty(typeName, "typeName not specified");
            Expect.IsNotNullOrEmpty(methodName, "methodName not specified");
            JsonResult ret = new JsonResult();
            ret.Status = JsonResultStatus.Unknown;
            ret.Input = parameters;
            if (HttpContext.Current == null)
            {
                ret.Status = JsonResultStatus.Error;
                ret.Data = NewInvalidOperationException();
            }
            else
            {
                object handler = GetInstance(typeName);//HttpContext.Current.Session[typeName.ToLowerInvariant()];
                if (handler == null)
                {
                    ret.Status = JsonResultStatus.Error;
                    ret.Data = new Exception("The specified type was not registered to handle json requests");
                }
                else
                {
                    MethodInfo[] methods = handler.GetType().GetMethods();
                    foreach (MethodInfo method in methods)
                    {
                        JsonMethod jsonMethodAttribute = null;
                        if (!CustomAttributeExtension.HasCustomAttributeOfType<JsonMethod>(method, out jsonMethodAttribute))
                        {
                            continue;
                        }

                        if (method.Name.ToLowerInvariant().Equals(methodName.ToLowerInvariant()))
                        {
                            try
                            {
                                switch (jsonMethodAttribute.Verb)
                                {
                                    case Verbs.GET:
                                        ret.Data = method.Invoke(handler, (object[])parameters);
                                        ret.Status = JsonResultStatus.Success;                                        
                                        break;
                                    case Verbs.POST:
                                        ParameterInfo[] parameterInfos = method.GetParameters();
                                        string[] jsonParameters = null;
                                        using(StreamReader sr = new StreamReader(HttpContext.Current.Request.InputStream))
                                        {
                                            string inputString = sr.ReadToEnd();
                                            jsonParameters = StringExtensions.DelimitSplit(inputString, ParameterDelimiter);
                                        }

                                        if(jsonParameters.Length != parameterInfos.Length)
                                            ExceptionHelper.Throw<JsonInvalidOperationException>("Parameter count mismatch in call to method {0}, required {1} parameters received {2}", method.Name, parameterInfos.Length, jsonParameters.Length);


                                        List<object> parameterList = new List<object>();
                                        for(int i = 0 ; i < parameterInfos.Length; i++)
                                        {
                                            ParameterInfo parameterInfo = parameterInfos[i];
                                            Serializer serializer = new Serializer(parameterInfo.ParameterType);
                                            parameterList.Add(serializer.Deserialize(jsonParameters[i]));
                                        }

                                        ret.Data = method.Invoke(handler, parameterList.ToArray());
                                        ret.Status = JsonResultStatus.Success;
                                        ret.Input = jsonParameters;
                                        break;
                                }

                                break;
                            }
                            catch (Exception ex)
                            {
                                string parms = parameters != null ? StringExtensions.ToDelimited(parameters, ","): "";
                                LogManager.CurrentLog.AddEntry("An error occurred during Javascript Method Invokation: typeName={0};methodName={1};parameters={2}", ex, typeName, methodName, parms);
                                if (ex.InnerException != null)
                                    ex = ex.InnerException;
                                ret.Status = JsonResultStatus.Error;
                                ret.Data = ex.Message;
                                ret.ErrorMessage = ex.Message;
#if DEBUG
                                ret.StackTrace = ex.StackTrace;
#endif
                                break;
                            }
                        }
                    }
                }
            }

            return ret;
        }

        internal static object GetInstance(string typeName)
        {
            JsonProviders providers = SingletonHelper.GetSessionSingleton<JsonProviders>();//if (javascriptHandlers == null)

            return providers[typeName];
        }

        internal static Type GetType(string typeName)
        {
            object inst = GetInstance(typeName);
            if (inst == null)
                return null;
            else
                return inst.GetType();
        }
        
        /// <summary>
        /// Includes the embedded Javascript files from the specified assembly
        /// using script tags with a 'src' attribute pointing to the JavascriptServer
        /// by way of the specified page.
        /// </summary>
        /// <param name="page">The page to write script tags to.</param>
        /// <param name="assemblyToLoad">The assembly to load embedded Javascript files from.</param>
        public static void RegisterResourceScripts(Page page, Assembly assemblyToLoad)
        {
            if (HttpContext.Current == null)
                throw NewInvalidOperationException();

            string goodPart = "";
            string[] splitted = page.Request.Url.GetLeftPart(UriPartial.Path).Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            goodPart += splitted[splitted.Length - 1];

            Resources.Load(assemblyToLoad);
            foreach (string script in Resources.JavaScript.Keys)
            {
                page.ClientScript.RegisterClientScriptInclude(script, goodPart + "?script=" + script);
            }
        }

        public static void RegisterResourceScripts(Assembly assemblyToLoadFrom)
        {
            Resources.LoadJavascript(assemblyToLoadFrom);
        }

        /// <summary>
        /// Includes the specified script onto the specified page using a script tag with a 'src' 
        /// attribute.
        /// </summary>
        /// <param name="page">The page to include the script on.</param>
        /// <param name="scriptName">The file name of the script minus the embedded assembly path.</param>
        /// <param name="assemblyToLoadFrom">The assembly to load the script from.</param>
        public static void RegisterResourceScriptByName(Page page, string scriptName, Assembly assemblyToLoadFrom)
        {
            if( HttpContext.Current == null )
                throw NewInvalidOperationException();

            Resources.Load(assemblyToLoadFrom);

            RegisterResourceScripts(page, assemblyToLoadFrom, Resources.JavaScriptFriendlyNamesToQualifiedScriptPath[scriptName]);
        }

        public static void RegisterResourceScripts(Page page, Assembly assemblyToLoadFrom, params string[] scriptNames)
        {
            if (HttpContext.Current == null)
                throw NewInvalidOperationException();

            Resources.Load(assemblyToLoadFrom);

            foreach (string script in scriptNames)
            {
                string scriptPath = GetResourceScriptPath(page.Request.Url, script);
                JavascriptPage javascriptPage = page as JavascriptPage; // "as" casting to prevent InvalidCastException                

                if (javascriptPage != null)
                {
                    javascriptPage.JavascriptResourceManager.AddResourceHeaderScript(script);
                }
                else
                {
                    page.ClientScript.RegisterClientScriptInclude(script, scriptPath);
                }                
            }
        }

        public static string GetResourceScriptPath(Uri uri, string scriptName)
        {
            scriptName = scriptName.ToLower();
            string goodPart = "";
            string[] splitted = uri.GetLeftPart(UriPartial.Path).Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            goodPart += splitted[splitted.Length - 1];
            return goodPart + "?script=" + scriptName;
        }

        public static string GetClientInitScriptPath(Uri uri, JsonProviderInfo info)//string varName, Type type)
        {
            string varName = info.VarName;
            Type type = info.ProviderType;
            UriBuilder builder = new UriBuilder(uri.ToString());
            builder.Query = "init=true&t=" + GetClientTypeName(type) + "&varname=" + varName + "&ienocache=" + StringExtensions.RandomString(4, false, false);
            return builder.ToString();
        }

        public static string GetClientTypeName(Type type)
        {
            if (type.Namespace.StartsWith("ASP"))
                return type.Name.ToLower();
            else
                return (type.Namespace + "." + type.Name).ToLower();
        }

        /// <summary>
        /// Registers the proxy for the specified type onto the specified page 
        /// using the specified varName.
        /// </summary>
        /// <param name="page">The Page</param>
        /// <param name="varName">The client side name of the proxy instance.</param>
        /// <param name="type">The .Net type to register</param>
        private static void RegisterClientInitScript(Page page, JsonProviderInfo info)
        {
            if (page == null)
                return;

            if (HttpContext.Current == null)
                throw NewInvalidOperationException();

            string scriptPath = GetClientInitScriptPath(page.Request.Url, info);

            page.ClientScript.RegisterClientScriptInclude(info.ClientTypeName, scriptPath);           

        }

        public static void SessionStart()
        {
            RegisterProviders();
            //SingletonHelper.GetApplicationProvider<IDoodadManager>(new DoodadManager()).SessionStart();
        }
        /// <summary>
        /// Registers all json providers found in the 
        /// assemblies found in the bin directory that 
        /// conform to the JsonProviderFilter property.
        /// This method is intended to be called from Session_Start in
        /// Global.asax.
        /// </summary>
        public static void RegisterProviders()
        {
            string path = HttpContext.Current.Server.MapPath("~/bin");
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            foreach (string filter in StringExtensions.SemiColonSplit(JsonProviderFilter))
            {
                foreach (FileInfo fileInfo in dirInfo.GetFiles(filter))
                {
                    try
                    {
                        Assembly toBeRegistered = Assembly.LoadFile(fileInfo.FullName);
                        RegisterProviders(toBeRegistered);
                    }
                    catch (Exception ex)
                    {
                        LogManager.CurrentLog.AddEntry("Unable to register JsonProviders from file: {0}", ex, fileInfo.FullName);
                    }
                }
            }
        }

        /// <summary>
        /// File filter to determine what assemblies to analyze
        /// for JsonProxy and JsonMethod attributes.
        /// </summary>
        public static string JsonProviderFilter
        {
            get
            {
                string retVal = "*JsonProviders.dll;*JsonProviders.exe;";
                string fromConf = DefaultConfiguration.GetAppSetting(typeof(JavascriptServer), "JsonProviderFilter");
                return string.IsNullOrEmpty(fromConf) ? retVal : fromConf;
            }
        }

        /// <summary>
        /// Registers the specified Type to respond to JsonMethod invokation
        /// requests.
        /// </summary>
        /// <param name="serverHandler"></param>
        public static void RegisterProvider(Type serverHandler)
        {
            RegisterProvider(serverHandler, null);
        }

        /// <summary>
        /// Register all json providers (classes adornded with the JsonProxy attribute
        /// or having methods adorned with the JsonMethod attribute) defined in the 
        /// assembly where the specified type is defined.
        /// </summary>
        /// <param name="type"></param>
        public static void RegisterProviders(Type type)
        {
            RegisterProviders(type.Assembly);
        }

        /// <summary>
        /// Register all json providers (classes adornded with the JsonProxy attribute
        /// or having methods adorned with the JsonMethod attribute) defined in the 
        /// specified assembly.
        /// </summary>
        /// <param name="type"></param>
        public static void RegisterProviders(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (CustomAttributeExtension.GetFirstMethodWithAttributeOfType<JsonMethod>(type) != null)
                {
                    RegisterProvider(type);
                }
            }
        }

        /// <summary>
        /// Register the specified serverHandler Type as a Javascript
        /// handler on the specified page.  A new instance of the specified
        /// type will be created so it must define a parameterless constructor.
        /// </summary>
        /// <param name="serverHandler">The Javascript handler Type</param>
        /// <param name="page">The page to register</param>
        public static void RegisterProvider(Type serverHandler, Page page)
        {
            if (HttpContext.Current == null)
                throw NewInvalidOperationException();

            ValidateProvider(serverHandler); 

            object handlerInstance = CreateInstance(serverHandler);

            Resources.Load(Assembly.GetExecutingAssembly());// this may need to be moved
            Resources.Load(serverHandler.Assembly);

            SetProvider(handlerInstance, page);
        }

        /// <summary>
        /// Register the specified object instance as a Javascript handler on the 
        /// specified page. 
        /// </summary>
        /// <param name="serverHandlerInstance">The Javascript handler</param>
        public static void RegisterProvider(object serverHandlerInstance)
        {
            RegisterProvider(serverHandlerInstance, null);
        }

        public static void RegisterProvider(object serverHandlerInstance, Page page)
        {
            if (HttpContext.Current == null)
                throw NewInvalidOperationException();

            Type serverHandlerType = serverHandlerInstance.GetType();
            ValidateProvider(serverHandlerType); 

            Resources.Load(Assembly.GetExecutingAssembly());
            Resources.Load(serverHandlerType.Assembly);

            SetProvider(serverHandlerInstance, page);
        }

        public static string GetDefaultVarName(Type serverHandler)
        {
            JsonProxy proxyAttribute = null;
            if (CustomAttributeExtension.HasCustomAttributeOfType<JsonProxy>(serverHandler, out proxyAttribute))
            {
                if (!string.IsNullOrEmpty(proxyAttribute.VarName))
                    return proxyAttribute.VarName;
            }
                
            return GetClientTypeName(serverHandler);
        }

        internal static void SetProvider(object handlerInstance)
        {
            SetProvider(handlerInstance, null);
        }

        internal static void SetProvider(object handlerInstance, Page page)
        {
            Type serverHandler = handlerInstance.GetType();
            if (serverHandler == typeof(RuntimeTypeHandle))
                return;

            JsonProviders providers = SingletonHelper.GetSessionSingleton<JsonProviders>();//if (javascriptHandlers == null)
            JsonProviderInfo provider = providers.Add(handlerInstance);

            if (page != null)
                RegisterClientInitScript(page, provider);
        }
        
        public static MethodInfo[] GetJsonMethods(Type serverHandler)
        {
            List<MethodInfo> methods = new List<MethodInfo>();
            foreach (MethodInfo method in serverHandler.GetMethods())
            {

                if (CustomAttributeExtension.HasCustomAttributeOfType<JsonMethod>(method))
                {
                    methods.Add(method);
                }
            }

            return methods.ToArray();
        }

        public static JsonProviderValidationResult ValidateProvider(Type serverHandler)
        {
            return ValidateProvider(serverHandler, false);
        }
        /// <summary>
        /// Validates the specified type ensuring that no overriddes exist for any JsonMethods
        /// </summary>
        /// <param name="serverHandler">The Type to validate</param>
        /// <param name="throwOnFail">true to throw an error if validation fails.</param>
        /// <returns>JavascriptHandlerValidationResult</returns>
        public static JsonProviderValidationResult ValidateProvider(Type serverHandler, bool throwOnFail)
        {
            MethodInfo bad = null;
            JsonProviderValidationResult result = ValidateProvider(serverHandler, out bad);
            if (result != JsonProviderValidationResult.Success)
            {
                if(throwOnFail)
                    throw new JsonProviderValidationException(result, bad); 
            }
            
            
            return result;            
        }

        internal static void ValidateInvoker(JsonInvoker invoker)
        {
            Type handler = invoker.JsonMethodProvider.GetType();
            if (string.IsNullOrEmpty(invoker.MethodName))
                throw new JsonInvokerValidationException(invoker, JsonInvokerValidationResult.MethodNotSpecified, handler, null, invoker.ParameterSources.Count);
            
            MethodInfo method = handler.GetMethod(invoker.MethodName);
            if (method == null)
            {
                Type pageHandler = handler;
                // check the master page
                if (invoker.JsonMethodProvider is JavascriptPage && ((JavascriptPage)invoker.JsonMethodProvider).Master != null)
                {
                    handler = ((JavascriptPage)invoker.JsonMethodProvider).Master.GetType();
                    method = handler.GetMethod(invoker.MethodName);
                }

                if(method == null)
                    throw new JsonInvokerValidationException(invoker, JsonInvokerValidationResult.MethodNotFound, pageHandler, method, invoker.ParameterSources.Count, invoker.MethodName);
            }
            
            if (method == null)
                throw new JsonInvokerValidationException(invoker, JsonInvokerValidationResult.MethodNotFound, handler, method, invoker.ParameterSources.Count, invoker.MethodName);

            ParameterInfo[] paramters = method.GetParameters();
            if (invoker.ParameterSources.Count != paramters.Length)
                throw new JsonInvokerValidationException(invoker, JsonInvokerValidationResult.ParameterCountMismatch, handler, method, invoker.ParameterSources.Count);

            if (string.IsNullOrEmpty(invoker.CallbackJsonId) && invoker.Callback == null)
                throw new JsonInvokerValidationException(invoker, JsonInvokerValidationResult.CallbackNotSpecified, invoker.GetType(), method, invoker.ParameterSources.Count);

            // paramaters are validated at the time the handler is registered
            // so no need to check them again here
        }
        /// <summary>
        /// Validates the specified type is a valid JsonMethodProvider sending out the first faulting method found if a method is found
        /// that doesn't conform to the requirements of a JsonMethod.
        /// </summary>
        /// <param name="serverHandler"></param>
        /// <param name="faultingMethod"></param>
        /// <returns></returns>
        public static JsonProviderValidationResult ValidateProvider(Type serverHandler, out MethodInfo faultingMethod)
        {
            faultingMethod = null;

            // make sure there is at least one JSONMethod
            MethodInfo[] methods = serverHandler.GetMethods();
            bool hasJsonMethod = false;

            // make sure there aren't any "overridden" methods.  ensure unique names
            List<string> jsonMethodNames = new List<string>();

            foreach (MethodInfo method in methods)
            {
                JsonMethod attr;
                if (CustomAttributeExtension.HasCustomAttributeOfType<JsonMethod>(method, out attr))
                {
                    hasJsonMethod = true;
                    if(jsonMethodNames.Contains(method.Name))
                    {
                        faultingMethod = method;
                        return JsonProviderValidationResult.OverrideFound;
                    }

                    jsonMethodNames.Add(method.Name);

                }
            }

            if (!hasJsonMethod)
                return JsonProviderValidationResult.NoJsonMethodsFound;

            return JsonProviderValidationResult.Success;
        }

        /// <summary>
        /// Primary entry point for this method is from the private SendClientInstantiationScript method, but
        /// in theory this method could be used to generate client instantiation scripts for any Type.
        /// </summary>
        /// <param name="clientName">The name of the "var"ed instance on the client.</param>
        /// <param name="serverHandler">The Type to generate the script for.</param>
        /// <returns></returns>
        public static string GetClientInstantiationScript(string clientName, Type serverHandler)
        {
            HttpRequest request = HttpContext.Current.Request;
            string callback = request.QueryString["cb"];// +";\r\n"; // a callback is only used for dynamic loading of ascx JsonMethod providers
            if (!string.IsNullOrEmpty(callback))
                callback += ";";
            string baseUrl = request.Url.PathAndQuery.Replace(request.Url.Query, "");//GetLeftPart(UriPartial.Authority) + request.ApplicationPath;

            // if no handler is specified send an error back to the client
            if (serverHandler == null && !string.IsNullOrEmpty(callback))
            {
                string message = string.Empty;
#if DEBUG
                message = "The specified serverHandler Type was not registered or null (" + clientName + ").";
#else
                message = @"There was an error that occurred on the server. 
                We are working to correct the problem.  Please restart your browser.";
#endif
                string errorScript = "\r\nif(MessageBox !== undefined)\r\n";
                errorScript += string.Format("\tMessageBox.Show('{0}');\r\n", message);
                errorScript += "else";
                errorScript += string.Format("\talert('{0}');", message);
                string serverType = serverHandler == null ? "null" : serverHandler.Namespace + "." + serverHandler.Name;
                LogManager.CurrentLog.AddEntry("Failed to create instantiation script: [clientName: {0}, serverHandler: {1}]", LogEventType.Warning, clientName, serverType);
                return errorScript;
            }

            string script = "window." + clientName + " = new JsonProxy();\r\n";
            script += "var " + clientName + " = window." + clientName + ";\r\n";
            script += clientName + ".type = '" + GetClientTypeName(serverHandler) + "';";
            script += string.Format("{0}.name = \"{0}\";\r\n", clientName);
            script += "ProxyUtil.AddServerProxy('" + clientName + "', " + clientName + ");\r\n";

            string methodHooks = string.Empty;
            string methodHooksEx = string.Empty;
            string methodVerbs = string.Empty;

            methodHooks += clientName + ".MethodEndpoints = {};\r\n";//string.Format("{0}.MethodEndpoints = [];\r\n", clientName);
            methodVerbs += clientName + ".MethodVerbs = {};\r\n";//string.Format("{0}.MethodVerbs = [];\r\n", clientName);
            MethodInfo[] methods = serverHandler.GetMethods();
            
            foreach (MethodInfo method in methods)
            {
                //object[] obj = method.GetCustomAttributes(typeof(JsonMethod), true);
                
                //if (obj.Length > 0)
                JsonMethod jsonMethodAttribute;
                if(CustomAttributeExtension.HasCustomAttributeOfType<JsonMethod>(method, out jsonMethodAttribute))
                {
                    string verb = jsonMethodAttribute.Verb.ToString();//((JsonMethod)obj[0]).Verb.ToString();
                    string multiInvoke = jsonMethodAttribute.MultiInvoke ? "true": "false";
                    string argJsArray = "[";

                    string methodCallPath = string.Format("{0}?t={1}&m={2}&inv=true", baseUrl, GetClientTypeName(serverHandler), method.Name.ToLowerInvariant());

                    methodHooks += string.Format("{0}.MethodEndpoints['{1}'] = '{2}';\r\n", clientName, method.Name, methodCallPath);
                    methodHooks += clientName + ".MethodEndpoints['" + method.Name + "'].multiInvoke = " + multiInvoke + ";\r\n";
                    methodVerbs += string.Format("{0}.MethodVerbs['{1}'] = '{2}';\r\n", clientName, method.Name, verb);

                    ParameterInfo[] parameters = method.GetParameters();
                    methodHooks += string.Format("{0}.{1} = function(", clientName, method.Name);
                    methodHooksEx += string.Format("{0}.{1}Ex = function(", clientName, method.Name);

                    if (parameters.Length > 0)
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            methodHooks += string.Format("{0}", parameters[i].Name);
                            methodHooksEx += string.Format("{0}", parameters[i].Name);

                            argJsArray += parameters[i].Name;
                            methodHooks += ",";
                            methodHooksEx += ",";

                            if (i != parameters.Length - 1)
                            {
                                argJsArray += ",";
                            }
                        }
                    }
                    argJsArray += "]";
                    methodHooks += "callBack, format){ \r\n\tthis.InvokeMethod('" + method.Name + "', " + argJsArray + ", JSUI.IsFunction(callBack) ? callBack : this." + method.Name + "Callback, format ? format: 'json');\r\n}\r\n";
                    methodHooks += clientName + "." + method.Name + "Callback = function(){};// this should be overridden if functionality is required\r\n";

                    methodHooksEx += "options, callBack){\r\n\tthis.InvokeMethodEx('" + method.Name + "', " + argJsArray + ", options, callBack);\r\n}\r\n";
                }
            }

            return script + methodHooks + methodHooksEx + methodVerbs + callback;
        }

        private static InvalidOperationException NewInvalidOperationException()
        {
            return new InvalidOperationException("JavascriptServer should be used inside a web application");
        }

        private static object CreateInstance(Type serverHandler)
        {
            ConstructorInfo ctor = serverHandler.GetConstructor(new Type[] { });
            if (ctor == null)
                throw new InvalidOperationException(string.Format("The type {0} doesn't define a parameterless constructor", serverHandler.Name));
                    
            return ctor.Invoke(null);
        }
    }
}
