/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using System.Reflection;
using System.Xml.Serialization;
using System.Web.Routing;
using Bam.Net.ServiceProxy.Js;
using Yahoo.Yui.Compressor;
using System.Web.Script.Serialization;

namespace Bam.Net.ServiceProxy
{
    public class ServiceProxyController : Controller
    {
        volatile static Dictionary<string, ExecutionResultDelegate> _actions;
        static object actionsLock = new object();
        public ServiceProxyController()
        {
            if (_actions == null)
            {
                lock (actionsLock)
                {
                    _actions = new Dictionary<string, ExecutionResultDelegate>();
                }
            }

            _actions.AddMissing("html", Html);
            _actions.AddMissing("htm", Html);
            _actions.AddMissing("txt", Text);
            _actions.AddMissing("text", Text);
            _actions.AddMissing("csv", Text);
            _actions.AddMissing("json", Json);
            _actions.AddMissing("jsonp", Jsonp);
            _actions.AddMissing("xml", Xml);
            _actions.AddMissing("script", JavaScript);
            _actions.AddMissing("js", JavaScript);
        }

        volatile static ServiceProxyController defaultServiceProxyController;
        static object defaultPocLock = new object();
        internal static ServiceProxyController Init()
        {
            if (defaultServiceProxyController == null)
            {
                lock (defaultPocLock)
                {
                    defaultServiceProxyController = new ServiceProxyController();
                }
            }

            return defaultServiceProxyController;
        }

        public static Incubator Incubator
        {
            get
            {
                return ServiceProxySystem.Incubator;
            }
        }

        /// <summary>
        /// Register the specified handler to handle the specified file extension.
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="handler"></param>
        /// <param name="reset"></param>
        public static void RegisterServiceProxyRequestDelegate(string extension, ExecutionResultDelegate handler, bool reset = false)
        {
            if (_actions == null)
            {
                lock (actionsLock)
                {
                    _actions = new Dictionary<string, ExecutionResultDelegate>();
                }
            }

            if (_actions.ContainsKey(extension) && reset)
            {
                _actions[extension] = handler;
            }
            else if (!_actions.ContainsKey(extension))
            {
                _actions.Add(extension, handler);
            }
            else if (_actions.ContainsKey(extension) && !reset)
            {
                throw new InvalidOperationException("The handler for the extension ({0}) has already been set.");
            }
        }

        /// <summary>
        /// Register all the types found in the specified assembly to 
        /// handle FileExt requests.
        /// </summary>
        /// <param name="assembly"></param>
        public static void Register(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                Incubator.Construct(type);
            }
        }


        /// <summary>
        /// The action to be referenced on the client to ensure all proxies
        /// are registered on the current client page.
        /// </summary>
        /// <param name="min"></param>
        /// <returns></returns>
        public ActionResult JsProxies(bool min = false)
        {
            if (DebugIsOff() && min == false)
            {
                min = true;
            }
            return new JsProxyResult(min);
        }

        public ActionResult JsProxy(string className, bool min = false)
        {
            if (DebugIsOff() && min == false)
            {
                min = true;
            }
            return new JsProxyResult(className, min);
        }

        /// <summary>
        /// Get the resource script with the specified scriptName
        /// </summary>
        /// <param name="scriptName"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public ActionResult ServiceProxyJsResource(string scriptName, bool min = false)
        {
            if (DebugIsOff() && min == false)
            {
                min = true;
            }
            return JavaScript(ResourceScripts.Get(scriptName, min));
        }

        private bool DebugIsOff()
        {
            return !System.Web.HttpContext.Current.IsDebuggingEnabled;
        }

        /// <summary>
        /// The action that is executed for all ServiceProxy requests.  This method
        /// returns an ActionResult based on the specified file extension.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="ext"></param>
        /// <param name="jsonParams"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public ActionResult Post(
            string className,
            string methodName,
            string ext,
            string jsonParams = null,
            string view = null)
        {
            ExecutionRequest request = new ExecutionRequest
            {
                ClassName = className,
                MethodName = methodName,
                Ext = ext,
                JsonParams = jsonParams,
                ViewName = view ?? "Default",
                Context = new HttpContextWrapper(ControllerContext.HttpContext)
            };

            try
            {
                request.Execute();
            }
            catch (Exception ex)
            {
                string format = "An error occurred executing {0}.{1}: {2}\r\n{3}";
                request.Result = (object)string.Format(format, className, methodName, ex.Message, ex.StackTrace ?? "");
                Log.AddEntry(format, ex, className, methodName, ex.Message, ex.StackTrace ?? "");
            }

            return GetActionResult(request);
        }

        public ActionResult Get(string className, string methodName, string ext)
        {
            ExecutionRequest request = new ExecutionRequest(className, methodName, ext);
            request.Request = new RequestWrapper(Request);
            request.Response = new ResponseWrapper(Response);
            try
            {
                request.Execute();
            }
            catch (Exception ex)
            {
                string format = "An error occurred executing {0}.{1}: {2}\r\n{3}";
                request.Result = (object)string.Format(format, className, methodName, ex.Message, ex.StackTrace ?? "");
                Log.AddEntry(format, ex, className, methodName, ex.Message, ex.StackTrace ?? "");
            }

            return GetActionResult(request); ;
        }

        public ActionResult CSharpProxies(string nameSpace = "Services", string[] classNames = null, bool interfacePrefix = false)
        {
            if (classNames == null)
            {
                classNames = ServiceProxySystem.Incubator.ClassNames;
            }

            return new CSharpProxyResult(nameSpace, nameSpace, classNames);
        }

        private static ActionResult GetActionResult(ExecutionRequest jsProxyRequest)
        {
            ExecutionResultDelegate handler;
            handler = _actions["json"];
            if (!string.IsNullOrEmpty(jsProxyRequest.Ext) && _actions.ContainsKey(jsProxyRequest.Ext))
            {
                handler = _actions[jsProxyRequest.Ext];
            }

            return handler(jsProxyRequest);
        }

        protected internal JavaScriptResult JavaScript(ExecutionRequest request)
        {
            return JavaScript(request.Result.ToString());
        }

        protected internal JsonResult Json(ExecutionRequest request)
        {
            return Json(request.Result, JsonRequestBehavior.AllowGet);
        }

        protected internal JsonpResult Jsonp(ExecutionRequest request)
        {
            return new JsonpResult(request.Result, request.Callback);
        }

        protected internal PartialViewResult Html(ExecutionRequest request)
        {
            string viewName = string.Format("Void/{0}", request.ViewName);
            if (request.Result != null)
            {
                Type t = request.Result.GetType();
                viewName = string.Format("{0}/{1}", t.Name, request.ViewName);
                if (!ServiceProxySystem.ServiceProxyPartialExists(t, request.ViewName))
                {
                    ServiceProxySystem.WriteServiceProxyPartial(t, request.ViewName);
                }
            }
            else if (!ServiceProxySystem.ServiceProxyPartialExists("Void", request.ViewName))
            {
                ServiceProxySystem.WriteVoidServiceProxyPartial(request.ViewName);
            }

            return PartialView(viewName, request.Result);
        }


        protected internal XmlResult Xml(ExecutionRequest request)
        {
            return new XmlResult(request.Result);
        }

        protected internal CsvResult Text(ExecutionRequest request)
        {
            return Csv(request);
        }

        protected internal CsvResult Csv(ExecutionRequest request)
        {
            return new CsvResult(request.Result, request.MethodName);
        }

        protected string RenderPartialView()
        {
            return RenderPartialView(null, null);
        }

        protected string RenderPartialView(string viewName)
        {
            return RenderPartialView(viewName, null);
        }

        protected string RenderPartialView(object model)
        {
            return RenderPartialView(null, model);
        }

        protected string RenderPartialView(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
