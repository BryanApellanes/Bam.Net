/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using Naizari.Extensions;
using Naizari.Helpers;
using System.IO;
using System.Collections.Specialized;
using System.Diagnostics;
using Naizari.Javascript;
using Naizari.Logging;

namespace Naizari.Javascript.BoxControls
{
    /// <summary>
    /// The BoxServer works in conjunction with the JavascriptServer when a request
    /// is made for the result of a JsonMethod invocation to be formated with a 
    /// "BoxUserControl" ascx webcontrol.
    /// </summary>
    public class BoxServer
    {
        class BoxResponses
        {
            Dictionary<string, BoxResponse> responses;
            public BoxResponses() 
            {
                this.responses = new Dictionary<string, BoxResponse>();
            }

            public BoxResponse this[string clientKey]
            {
                get
                {
                    return responses[clientKey];
                }
            }

            public bool ContainsKey(string clientKey)
            {
                return this.responses.ContainsKey(clientKey);
            }

            public void Add(string clientKey, BoxResponse dataBoxResponse)
            {
                responses.Add(clientKey, dataBoxResponse);
            }

            public void Remove(string clientKey)
            {
                responses.Remove(clientKey);
            }
        }
        /// <summary>
        /// The root folder for all box ascx files.
        /// </summary>
        public const string BoxDirectory = "~/App_Data/Boxes/";
        public const string BoxDataTemplateDirectory = "~/App_Data/Boxes/Data/";
        public const string VariablePrefix = "$$";
        public const string VariableSuffix = "$$";
        //static Dictionary<string, BoxResponse> staticTemplateResponses;
        //static Dictionary<string, Control> loadedControls;
        
        static BoxServer()
        {
            //staticTemplateResponses = new Dictionary<string, BoxResponse>();
            SingletonHelper.SetApplicationProvider<BoxResponses>(new BoxResponses(), false);
        }

        public static void SendBoxResponse(HttpContext context, string templateName)
        {
            HttpResponse response = context.Response;

            string clientKey = context.Request.QueryString["ck"];
            if (string.IsNullOrEmpty(clientKey))
                throw new JsonInvalidOperationException("No client key was specified");

            BoxResponses boxResponses = SingletonHelper.GetApplicationProvider<BoxResponses>();

            if(IsScriptRequest(context.Request))
            {
                response.Clear();
                response.Write(boxResponses[clientKey].Script);
                boxResponses.Remove(clientKey);
                response.Flush();
                response.SuppressContent = true;
                context.ApplicationInstance.CompleteRequest();
                return;
            }

            string virtualBoxPath = GetVirtualBoxPath(context, templateName);
            try
            {
                if (string.IsNullOrEmpty(virtualBoxPath))
                    throw new JsonInvalidOperationException("The box template file could not be found: [" + templateName + "]");
                
                // this is the dom id of the requesting "box" div element
                string requesterId = context.Request.QueryString["domid"]; // TODO: enumify magic strings

                BoxResponse boxResponse = GetBoxResponse(context, virtualBoxPath, null, true);

                boxResponses.Add(clientKey, boxResponse);

                response.Clear();
                response.Write(boxResponse.Html);
                response.Flush();
                response.SuppressContent = true;
                context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                SendBoxedError(context, ex);
            }

            return;
        }

        private static bool IsScriptRequest(HttpRequest request)
        {
            return !string.IsNullOrEmpty(request.QueryString["s"]) && request.QueryString["s"].Equals("y");
        }

        /// <summary>
        /// This method will send the "ConglomerateScript" for the specified templateName.
        /// </summary>
        /// <remarks>A ConglomerateScript represents all the scripts required by the JsonControls
        /// in an ascx file.   The BoxUserControl class prepares the resulting ascx control for 
        /// Json/Box rendering.</remarks>
        /// <param name="context">The HttpContext significant to the current request.</param>
        /// <param name="templateName">The name of an ascx file contained in the 
        /// BoxServer.BoxTemplateDirectory or the virutal path to an ascx file.</param>
        /// <returns></returns>
        //public static void SendBoxScriptResponse(HttpContext context, string templateName)
        //{
        //    string virtualBoxPath = GetVirtualBoxPath(context, templateName);
        //    try
        //    {
        //        if (string.IsNullOrEmpty(virtualBoxPath))
        //            throw new InvalidOperationException("The box template could not be found: [" + templateName + "]");

        //        string requesterId = context.Request.QueryString["domid"];

        //        HttpResponse response = context.Response;
        //        //string responseString = BoxServer.GetScriptString(virtualBoxPath, requesterId);
        //        //GetDataBoxResponse(context, virtualBoxPath, null);
        //        response.Clear();
        //        response.Write(responseString);
        //        response.Flush();
        //        response.SuppressContent = true;
        //        context.ApplicationInstance.CompleteRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        SendBoxedError(context, ex);
        //    }
        //}

        /// <summary>
        /// Applies a template to the Data property of the specified JsonResult object 
        /// and sends the result to the client as an html section or "Box".
        /// </summary>
        /// <param name="context">The HttpContext</param>
        /// <param name="jsonResult">The result to be Boxed</param>
        public static void SendDataBoxResponse(HttpContext context, JsonResult jsonResult)
        {
            #region check JsonResult
            HttpResponse response = context.Response;

            // Check the status of the jsonResult 
            // if status is error send default error box
            if (jsonResult.Status == JsonResultStatus.Error)
            {
                SendBoxedError(response, jsonResult);
                return;
            }

            if (jsonResult.Data == null)
            {
                SendBoxedError(response, jsonResult);
                return;
            }
            #endregion


            HttpRequest request = context.Request;
            
            string boxTemplateName = request.QueryString["dbx"];// TODO: create enums to represent all the magic strings used by the different script components
            string clientKey = request.QueryString["ck"]; 
            if (string.IsNullOrEmpty(boxTemplateName))
            {
                SendBoxedError(response, jsonResult);
                return;
            }

            object[] objectsToTemplate = new object[] { jsonResult.Data };
            if (jsonResult.Data is Array)
                objectsToTemplate = (object[])jsonResult.Data;

            BoxResponses responses = SingletonHelper.GetApplicationProvider<BoxResponses>();
            string allHtml = string.Empty;
            string allScript = string.Empty;
            try
            {
                foreach (object objectToTemplate in objectsToTemplate)
                {
                    // getting the property variables for each object will allow objects of different types to 
                    // be templated in one pass.  This incurs some processing overhead but the flexibility is
                    // worth it.
                    string[] propertyVariables = GetPropertyVariables(objectToTemplate.GetType());
                    string virtualBoxPath = GetVirtualDataBoxPath(context, objectToTemplate.GetType(), boxTemplateName);
                    if (string.IsNullOrEmpty(virtualBoxPath))
                        SendBoxedError(context, ExceptionHelper.CreateException<JsonInvalidOperationException>("Unable to find specified data box template ['{0}.{1}']", objectToTemplate.GetType().Name, boxTemplateName));

                    BoxResponse dataBoxResponse = GetBoxResponse(context, virtualBoxPath, objectToTemplate, true);
                    string html = dataBoxResponse.Html;
                    string script = dataBoxResponse.Script;

                    allHtml += GetVariableReplaceText(html, propertyVariables, objectToTemplate);
                    allScript += GetVariableReplaceText(script, propertyVariables, objectToTemplate);
                }
            }
            catch (Exception ex)
            {
                SendBoxedError(context, ex);
            }

            responses.Add(clientKey, new BoxResponse(allHtml, allScript));

            response.Clear();
            response.Write(allHtml);
            response.Flush();
            response.SuppressContent = true;
            
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //response.End();
        }

        public static string GetTemplatedString(object objectToTemplate, string templateName)
        {
            Type type = objectToTemplate.GetType();
            string boxPath = GetVirtualDataBoxPathWithoutFileCheck(type, templateName);
            string html = GetHtmlString(boxPath);
            html = GetVariableReplaceText(html, GetPropertyVariables(type), objectToTemplate);
            return html;
        }

        public static void SendDataBoxScriptResponse(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            string clientKey = request.QueryString["ck"];

            BoxResponses responses = SingletonHelper.GetApplicationProvider<BoxResponses>();
            string script;
            if (!responses.ContainsKey(clientKey))
                script = "// no scripts";
            else
                script = responses[clientKey].Script;

            responses.Remove(clientKey);
            response.Clear();
            response.Write(script);
            response.Flush();
            response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //response.End();
        }

        internal static string GetBoxName(HttpContext context, string virtualBoxPath)
        {
            string ascxPath = context.Server.MapPath(virtualBoxPath);
            FileInfo info = new FileInfo(ascxPath);
            return info.Name;
        }

        /// <summary>
        /// Returns the virtual path of the specified box template
        /// </summary>
        /// <param name="context"></param>
        /// <param name="boxTemplateName"></param>
        /// <returns></returns>
        internal static string GetVirtualBoxPath(HttpContext context, string boxTemplateName)
        {
            //HttpResponse response = context.Response;
            //HttpRequest request = context.Request;

            #region get box path
            //what ascx file?
            string virtualBoxPath = BoxServer.BoxDirectory + boxTemplateName;
            string ascxPath = context.Server.MapPath(virtualBoxPath);
            if (File.Exists(ascxPath))
                return virtualBoxPath;
            else
            {
                ascxPath = context.Server.MapPath(boxTemplateName);
                if (File.Exists(ascxPath))
                    return boxTemplateName;
                
            }
            #endregion
            return string.Empty;
            //throw new BoxException("The specified BoxTemplate was not found: ['" + boxTemplateName + "']");
        }

        public static string GetVirtualDataBoxPathWithoutFileCheck(Type templateType, string templateName)
        {
            return BoxServer.BoxDataTemplateDirectory + templateType.Name + "/" + templateName + ".ascx";
        }

        internal static string GetVirtualDataBoxPath(HttpContext context, Type templateType, string dataBoxTemplateName)
        {
            string virtualBoxPath = BoxServer.BoxDataTemplateDirectory + templateType.Name + "." + dataBoxTemplateName + ".ascx";
            string ascxPath = context.Server.MapPath(virtualBoxPath);
            if (File.Exists(ascxPath))
                return virtualBoxPath;
            else
            {
                virtualBoxPath = BoxServer.BoxDataTemplateDirectory + templateType.Name + "/" + dataBoxTemplateName + ".ascx";
                ascxPath = context.Server.MapPath(virtualBoxPath);
                if (File.Exists(ascxPath))
                    return virtualBoxPath;
            }

            return string.Empty;
        }

        public static string GetScriptString(string virtualBoxPath)
        {
            return GetScriptString(virtualBoxPath, string.Empty);
        }

        public static string GetScriptString(string virutalBoxPath, string requesterId)
        {
            return GetScriptString(virutalBoxPath, requesterId, true);
        }

        public static string GetScriptString(string virtualBoxPath, string requesterId, bool postWindowLoad)
        {
            return GetScriptString(virtualBoxPath, requesterId, null, postWindowLoad);
        }

        public static string GetScriptString(string virtualBoxPath, string requesterId, object injectionObject, bool postWindowLoad)
        {
            Control control = LoadControl(virtualBoxPath, true, requesterId, injectionObject, postWindowLoad);
            byte[] scriptBytes = GetBytes(control, true);
            return Encoding.UTF8.GetString(scriptBytes);
        }

        public static string GetHtmlString(string virtualBoxPath)
        {
            return GetHtmlString(virtualBoxPath, true);
        }

        public static string GetHtmlString(string virtualBoxPath, bool renderScripts)
        {
            return GetHtmlString(virtualBoxPath, renderScripts, string.Empty);
        }

        public static string GetHtmlString(string virtualBoxPath, bool renderScripts, string requesterId)
        {
            Control control = LoadControl(virtualBoxPath, renderScripts, requesterId);

            //controlLoader.Tickle(false);

            byte[] htmlBytes = GetBytes(control);


            return Encoding.UTF8.GetString(htmlBytes);
        }

        public static BoxResponse GetBoxResponse(string virtualPath, object templatedObject)
        {
            return GetBoxResponse(HttpContext.Current, virtualPath, templatedObject);
        }

        public static BoxResponse GetBoxResponse(HttpContext context, string virtualBoxPath)
        {
            return GetBoxResponse(context, virtualBoxPath, null);
        }

        public static BoxResponse GetBoxResponse(HttpContext context, string virtualBoxPath, object injectionObject)
        {
            return GetBoxResponse(context, virtualBoxPath, injectionObject, false);
        }

        public static BoxResponse GetBoxResponse(HttpContext context, string virtualBoxPath, object injectionObject, bool postWindowLoad)
        {
            try
            {
                BoxResponse retVal = new BoxResponse();
                Control control = LoadControl(virtualBoxPath, false, string.Empty, injectionObject, postWindowLoad);
                //Control scriptVersion = LoadControl(virtualBoxPath, true);
                byte[] htmlBytes = GetBytes(control);
                byte[] scriptBytes = GetBytes(control, true);
                string html = Encoding.UTF8.GetString(htmlBytes); //SecreteHtml(htmlVersion);//
                string script = Encoding.ASCII.GetString(scriptBytes); //SecreteJavascript(scriptVersion);// 
                retVal.Html = html;
                retVal.Script = script;
                return retVal;
            }
            catch (Exception ex)
            {
                SendBoxedError(context, ex);
            }

            return null;
        }

        /// <summary>
        /// Renders the specified control and returns the output
        /// as a string.
        /// </summary>
        /// <param name="control">The control to render as a string</param>
        /// <returns>string</returns>
        //public static string SecreteHtml(Control control)
        //{
        //    byte[] htmlBytes = GetBytes(control);
        //    return Encoding.UTF8.GetString(htmlBytes);
        //}

        ///// <summary>
        ///// Renders only javascript code from all the child
        ///// JsonControls of the specified control and returns it
        ///// as a string.
        ///// </summary>
        ///// <param name="control">The control to secrete scripts for.</param>
        ///// <returns>string</returns>
        //public static string SecreteJavascript(Control control)
        //{
        //    byte[] scriptBytes = GetBytes(control, true);
        //    return Encoding.UTF8.GetString(scriptBytes);
        //}

        private static byte[] GetBytes(Control control)
        {
            return GetBytes(control, false);
        }

        private static byte[] GetBytes(Control control, bool scriptsOnly)
        {
            MemoryStream renderedControlText = new MemoryStream();
            StreamWriter sw = new StreamWriter(renderedControlText);
            HtmlTextWriter textWriter = new HtmlTextWriter(sw);
            

            if (scriptsOnly) // this is a response to a databox script request
            {
                List<JsonControl> jsonControls = BoxUserControl.GetAllJsonControls(control);
                foreach (JsonControl jsonControl in jsonControls)
                {
                    jsonControl.RenderScripts = true;
                    jsonControl.RenderConglomerateScript(textWriter, false);
                }
            }
            else
            {
                control.RenderControl(textWriter);
            }

            textWriter.Flush();

            byte[] htmlBytes = new byte[renderedControlText.Length];
            renderedControlText.Seek(0, 0);

            renderedControlText.Read(htmlBytes, 0, (int)renderedControlText.Length);
            return htmlBytes;
        }
        private static Control LoadControl(string virtualBoxPath, bool renderScripts)
        {
            return LoadControl(virtualBoxPath, renderScripts, string.Empty);
        }

        private static Control LoadControl(string virtualBoxPath, bool renderScripts, string requesterId)
        {
            return LoadControl(virtualBoxPath, renderScripts, requesterId, null);
        }
        private static Control LoadControl(string virtualBoxPath, bool renderScripts, string requesterId, object injectionObject)
        {
            return LoadControl(virtualBoxPath, renderScripts, requesterId, injectionObject, false);
        }

        private static Control LoadControl(string virtualBoxPath, bool renderScripts, string requesterId, object injectionObject, bool postWindowLoad)
        {
            JavascriptPage controlLoader = new JavascriptPage();

            Control control = controlLoader.LoadControl(virtualBoxPath);
            //if (control.Controls.Count == 0)
            //    throw new UserControlIsNotBoxUserControlException(virtualBoxPath);

            BoxUserControl evolved = control as BoxUserControl;//control.Controls[0] as BoxUserControl;
            if (evolved == null)
                throw new UserControlIsNotBoxUserControlException(virtualBoxPath);

            Type controlType = control.GetType();
            JavascriptServer.RegisterProvider(controlType);// makes any JsonMethod methods available to the client

            controlLoader.Controls.Add(control);

            if (injectionObject != null)
            {
                MethodInfo boxInject = CustomAttributeExtension.GetFirstMethodWithAttributeOfType<BoxInject>(controlType);
                if (boxInject != null)
                {
                    ParameterInfo[] paramInfo = boxInject.GetParameters();
                    if (paramInfo.Length != 1 || paramInfo[0].ParameterType != injectionObject.GetType())
                        throw ExceptionHelper.CreateException<JsonInvalidOperationException>("The BoxInject method of control type {0} has the wrong type or number of parameters.", control.GetType().Name);

                    boxInject.Invoke(control, new object[] { injectionObject });
                }

                evolved.Inject(injectionObject);
            }

            MethodInfo boxInit = CustomAttributeExtension.GetFirstMethodWithAttributeOfType<BoxInit>(control.GetType());
            if (boxInit != null)
                boxInit.Invoke(control, null);

            evolved.PostBoxLoad(renderScripts, requesterId, postWindowLoad);
            //loadedControls.Add(virtualBoxPath, control);
            return control;
        }

        private static string GetVariableReplaceText(string html, string[] propertyVariables, object objectToTemplate)
        {
            Type objectType = objectToTemplate.GetType();
            foreach (string propertyVariable in propertyVariables)
            {
                BoxVariable variable = new BoxVariable(propertyVariable, objectToTemplate);
                //PropertyInfo prop = objectType.GetProperty(propertyVariable.Replace(VariablePrefix, "").Replace(VariableSuffix, ""));
                //object value = prop.GetValue(objectToTemplate, null);
                //string stringValue = value == null ? "": value.ToString();
                if (html.Contains(propertyVariable))
                    html = html.Replace(propertyVariable, variable.Value);
            }
            

            return html;
        }

        public static string[] GetPropertyVariables(Type typeToGetVariablesFor)
        {
            return BoxVariable.GetPropertyVariables(typeToGetVariablesFor);
        }

        
        /// <summary>
        /// Sets the status of the specified JsonResult to JsonResultStatus.Error and
        /// sends the error message to the client in a div.  This is used to 
        /// respond to "DataBox" requests that fail due to an exception.
        /// </summary>
        /// <param name="response">The HttpResponse object to be used to send the error.</param>
        /// <param name="jsonResult">The JsonResult to box the error for.</param>
        public static void SendBoxedError(HttpResponse response, JsonResult jsonResult)
        {
            LogManager.CurrentLog.AddEntry("An error occurred fulfilling a box request: {0}\r\n{1}", LogEventType.Error, jsonResult.ErrorMessage, jsonResult.StackTrace);
            jsonResult.Status = JsonResultStatus.Error;
            string html = "<div class='error'>An error occurred fulfilling the request, we apologize for the inconvenience please try again shortly (";
            html += jsonResult.ErrorMessage + ").</br>";
#if DEBUG
            html += jsonResult.StackTrace + "</br>";
#endif
            html += "</div>";
            response.Clear();
            response.Write(html);
            response.Flush();
            response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //response.End();
        }

        private static void SendBoxedError(HttpContext context, Exception ex)
        {
            JsonResult result = new JsonResult();
            if (ex.InnerException != null)
                ex = ex.InnerException;
            result.ErrorMessage = ex.Message;
            result.StackTrace = ex.StackTrace;
            result.Status = JsonResultStatus.Error;
            SendBoxedError(context.Response, result);
        }
    }
}
