using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using System.IO;
using Bam.Net.ServiceProxy.Secure;
using Org.BouncyCastle.Security;

namespace Bam.Net.ServiceProxy
{
    public partial class ServiceProxySystem //fx
    {
        static bool initialized;
        static object initLock = new object();
        /// <summary>
        /// Initialize the underlying ServiceProxySystem, including registering the 
        /// necessary ServiceProxy routes in System.Web.Routing.RouteTable.Routes.
        /// </summary>
        public static void Initialize()
        {
            if (!initialized)
            {
                lock (initLock)
                {
                    initialized = true;
                    RegisterRoutes();
                    ServiceProxyController.Init();
                }
            }
        }

        /// <summary>
        /// Maps the ServiceProxy routes in the default Mvc RouteTable.
        /// This should be called from Global before setting the default
        /// action route.
        /// </summary>
        public static void RegisterRoutes()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Maps the ServiceProxy routes in the specified RouteCollection.
        /// This should be called from Global before setting the default
        /// action route.
        /// </summary>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "ServiceProxy",
                "{action}/{className}/{methodName}.{ext}",
                new { controller = "ServiceProxy", action = "Get", ext = "json" },
                new string[] { "Bam.Net.ServiceProxy" }
            );
        }


        /// <summary>
        /// Analyzes all the files in the bin directory of the current app that match the
        /// ProxySearchPattern and registers as services any class found addorned with the 
        /// ProxyAttribute
        /// </summary>
        /// <see cref="ServiceProxySystem.ProxySearchPattern" />
        public static void RegisterBinProviders()
        {
            HttpServerUtility server = HttpContext.Current.Server;
            DirectoryInfo bin = new DirectoryInfo(server.MapPath("~/bin"));
            RegisterTypesWithAttributeFrom<ProxyAttribute>(bin);
        }

        /// <summary>
        /// Register the specified handler to handle the specified file extension.
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="handler"></param>
        /// <param name="reset"></param>
        public static void RegisterServiceProxyRequestDelegate(string extension, ExecutionResultDelegate handler, bool reset = false)
        {
            ServiceProxyController.RegisterServiceProxyRequestDelegate(extension, handler, reset);
        }

        internal static bool ServiceProxyPartialExists(Type type, string viewName)
        {
            return ServiceProxyPartialExists(type.Name, viewName);
        }

        internal static bool ServiceProxyPartialExists(string typeName, string viewName)
        {
            List<string> fileExtensions = new List<string>
            {
                ".cshtml",
                ".vbhtml",
                ".aspx",
                ".ascx"
            };
            string path = System.Web.HttpContext.Current.Server.MapPath(string.Format(ServiceProxyPartialFormat, typeName, viewName));

            bool exists = false;
            foreach (string ext in fileExtensions)
            {
                if (System.IO.File.Exists(string.Format("{0}{1}", path, ext)))
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        internal static void WriteServiceProxyPartial(Type type, string viewName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(string.Format(ServiceProxyPartialFormat, type.Name, viewName));
            path = string.Format("{0}.cshtml", path);
            StringBuilder source = BuildPartialView(type);

            WriteServiceProxyPartial(path, source);
        }

        internal static void WriteVoidServiceProxyPartial(string viewName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(string.Format(ServiceProxyPartialFormat, "Void", viewName));
            path = string.Format("{0}.cshtml", path);
            StringBuilder builder = new StringBuilder();
            builder.Append("@* place holder *@\r\n\r\n<h1>Void place holder</h1>");
            WriteServiceProxyPartial(path, builder);
        }
    }
}
