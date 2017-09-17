/*
	Copyright © Bryan Apellanes 2015  
*/
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
    public class ServiceProxySystem
    {
        public const string ServiceProxyPartialFormat = "~/Views/ServiceProxy/{0}/{1}";

        static ServiceProxySystem()
        {
            UserResolvers = new ServiceProxy.UserResolvers();
            UserResolvers.AddResolver(new DefaultWebUserResolver());
            RoleResolvers = new RoleResolvers();
            RoleResolvers.AddResolver(new DefaultRoleResolver());
        }
        
        public static string GenerateId()
        {
            SecureRandom random = new SecureRandom();            
            return random.GenerateSeed(64).ToBase64().Sha256();            
        }

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

        static ServiceProxySystem current;
        static object currentLock = new object();
        /// <summary>
        /// Provides an extension point to add functionality to the ServiceProxySystem
        /// </summary>
        public static ServiceProxySystem Current
        {
            get
            {
                if (current == null)
                {
                    lock (currentLock)
                    {
                        current = new ServiceProxySystem();
                    }
                }

                return current;
            }
        }

        static string _proxySearchPattern;
        static object _proxySearchPatternLock = new object();
        /// <summary>
        /// The search pattern used to find assemblies that host
        /// service proxies (classes addorned with the ProxyAttribute custom attribute).
        /// This value is retrieved from the config file, the default is "*.dll" if none
        /// is provided.
        /// </summary>
        public static string ProxySearchPattern
        {
            get
            {
                return _proxySearchPatternLock.DoubleCheckLock(ref _proxySearchPattern, () => DefaultConfiguration.GetAppSetting("ProxySearchPattern", "*.dll"));
            }
        }
        
        public static UserResolvers UserResolvers
        {
            get;
            private set;
        }

        public static RoleResolvers RoleResolvers
        {
            get;
            private set;
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
        /// Searches the specified folder for assemblies that contain types 
        /// addorned with the specified attribute and registers each as
        /// services
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folderPath"></param>
        public static void RegisterTypesWithAttributeFrom<T>(string folderPath) where T : Attribute
        {
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            if (dir.Exists)
            {
                RegisterTypesWithAttributeFrom<T>(dir);
            }
        }

        /// <summary>
        /// Searches the specified folder for assemblies that contain types 
        /// addorned with the specified attribute and registers each as
        /// services
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dir"></param>
        public static void RegisterTypesWithAttributeFrom<T>(DirectoryInfo dir, ILogger logger = null) where T : Attribute
        {
            FileInfo[] files = dir.GetFiles(ProxySearchPattern);
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                try
                {
                    Assembly container = Assembly.LoadFrom(file.FullName);
                    RegisterTypesWithAttribute<T>(container);
                }
                catch (Exception ex)
                {
                    if (logger == null)
                    {
                        logger = Log.Default;
                    }
                    logger.AddEntry("Non fatal exception occurred registering proxy types from {0}", ex, file.Name);
                }
            }
        }

        /// <summary>
        /// Registers the types from the specified assemblyToLoadFrom that are addorned with
        /// the specified generic type attribute T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyToLoadFrom"></param>
        public static void RegisterTypesWithAttribute<T>(Assembly assemblyToLoadFrom) where T : Attribute
        {
            Type[] types = assemblyToLoadFrom.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Type current = types[i];
                if (current.HasCustomAttributeOfType<T>())
                {
                    Register(current);
                }
            }
        }

        /// <summary>
        /// Register the specified type as a ServiceProxy responder.
        /// </summary>
        /// <param name="type"></param>
        public static void Register(Type type)
        {
            Initialize();
            Incubator.Construct(type);
        }

        /// <summary>
        /// Register the speicified generic type T as a ServiceProxy responder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
        {
            Initialize();
            Incubator.Construct<T>();
        }

        /// <summary>
        /// Register the instance of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public static void Register<T>(T instance)
        {
            Initialize();
            Incubator.Set<T>(instance);
        }

        public static void Unregister<T>()
        {
            Incubator.Remove<T>();
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

        public static ServiceProxyClient<T> CreateClient<T>(string baseAddress)
        {
            return new ServiceProxyClient<T>(baseAddress);
        }

		/// <summary>
		/// Get the MethodInfos for the specified type that will
		/// be proxied if the specified type is registered as 
		/// a service proxy.
		/// </summary>
		/// <param name="type"></param>
        /// <param name="includeLocalMethods"></param>
		/// <returns></returns>
        public static MethodInfo[] GetProxiedMethods(Type type, bool includeLocalMethods = false)
        {
            List<MethodInfo> results = new List<MethodInfo>();
            foreach (MethodInfo method in type.GetMethods())
            {
                if (WillProxyMethod(method, includeLocalMethods))
                {
                    results.Add(method);
                }
            }

            return results.ToArray();
        }

        public static bool WillProxyMethod(MethodInfo method, bool includeLocal = false)
        {
            bool baseCheck = !method.Name.StartsWith("remove_") &&
                                    !method.Name.StartsWith("add_") &&
                                    method.MemberType == MemberTypes.Method &&
                                    !method.IsProperty() &&                                    
                                    method.DeclaringType != typeof(object);
            bool hasExcludeAttribute = method.HasCustomAttributeOfType(out ExcludeAttribute attr);
            bool result = false;
            if (includeLocal)
            {
                result = hasExcludeAttribute ? (attr is LocalAttribute && baseCheck) : baseCheck;
            }
            else
            {
                result = hasExcludeAttribute ? false : baseCheck;                
            }
            return result;
        }

        static Incubator incubator;
        static object incubatorLock = new object();
        /// <summary>
        /// Gets or sets the default Incubator instance used by the ServiceProxy system.
        /// </summary>
        public static Incubator Incubator
        {
            get
            {
                if (incubator == null)
                {
                    lock (incubatorLock)
                    {
                        incubator = new Incubator();
                    }
                }
                return incubator;
            }
            set
            {
                incubator = value;
            }
        }

        public static string GetBaseAddress(Uri uri)
        {
            string defaultBaseAddress = string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, uri.IsDefaultPort ? "/" : string.Format(":{0}/", uri.Port));
            return defaultBaseAddress;
        }

        public static StringBuilder GenerateCSharpProxyCode(string protocol, string hostName, int port, string nameSpace, params Type[] types)
        {
            string baseAddress = $"{protocol}://{hostName}:{port}";
            return GenerateCSharpProxyCode(baseAddress, nameSpace, "{0}.Contracts"._Format(nameSpace), types);
        }

        public static StringBuilder GenerateCSharpProxyCode(string defaultBaseAddress, string[] classNames, string nameSpace, string contractNamespace)
        {
            return GenerateCSharpProxyCode(defaultBaseAddress, classNames, nameSpace, contractNamespace, ServiceProxySystem.Incubator);
        }

        public static string HeaderFormat
        {
            get
            {
                return @"/**
This file was generated from {0}serviceproxy/csharpproxies.  This file should not be modified directly
**/

";
            }
        }
        protected static string MethodFormat
        {
            get
            {
                return @"{0}
        public {1} {2}({3})
        {{
            object[] parameters = new object[] {{ {4} }};
            {5}(""{2}"", parameters);
        }}";
            }
        }

        protected static string UsingFormat { get { return "using {0};\r\n"; } }

        protected static string NameSpaceFormat
        {
            get
            {
                return @"
namespace {0}
{{
{1}
    {2}
}}";
            }
        }

        protected static string ClassFormat
        {
            get
            {
                return @"{0}
    public class {1}Client: ServiceProxyClient<{2}.I{3}>, {2}.I{3}
    {{
        public {1}Client(): base(DefaultConfiguration.GetAppSetting(""{3}Url"", ""{4}""))
        {{            
        }}

        public {1}Client(string baseAddress): base(baseAddress)
        {{
        }}
        
        {5}
    }}
";
            }
        }

        protected static string SecureClassFormat
        {
            get
            {
                return @"{0}
    public class {1}Client: SecureServiceProxyClient<{2}.I{3}>, {2}.I{3}
    {{
        public {1}Client(): base(DefaultConfiguration.GetAppSetting(""{3}Url"", ""{4}""))
        {{
        }}

        public {1}Client(string baseAddress): base(baseAddress)
        {{
        }}
        
        {5}
    }}
";
            }
        }
        protected static string InterfaceFormat
        {
            get
            {
                return @"
        public interface I{0}
        {{
{1}
        }}
";
            }
        }
        protected static string InterfaceMethodFormat
        {
            get
            {
                return "\t{0} {1}({2});\r\n";
            }
        }
        public static StringBuilder GenerateCSharpProxyCode(string defaultBaseAddress, string[] classNames, string nameSpace, string contractNamespace, Incubator incubator, ILogger logger = null, bool includeLocalMethods = false)
        {
            logger = logger ?? Log.Default;
            List<Type> types = new List<Type>();
            classNames.Each(new { Logger = logger, Types = types }, (ctx, cn) =>
            {
                Type type = incubator[cn];
                if(type == null)
                {
                    ctx.Logger.AddEntry("Specified class name was not registered: {0}", LogEventType.Warning, cn);
                }
                else
                {
                    ctx.Types.Add(type);
                }
            });
            Args.ThrowIf(types.Count == 0, "None of the specified classes were found: {0}", string.Join(", ", classNames));
            return GenerateCSharpProxyCode(defaultBaseAddress, nameSpace, contractNamespace, types.ToArray(), includeLocalMethods);
        }
        public static StringBuilder GenerateCSharpProxyCode(string defaultBaseAddress, string nameSpace, string contractNamespace, Type[] types, bool includeLocalMethods = false)
        {
            StringBuilder code = new StringBuilder();
            StringBuilder classes = new StringBuilder();
            StringBuilder interfaces = new StringBuilder();
            HashSet<string> usingNamespaces = new HashSet<string>
            {
                "System",
                "Bam.Net.Configuration",
                "Bam.Net.ServiceProxy",
                "Bam.Net.ServiceProxy.Secure",
                contractNamespace
            };
            foreach (Type type in types)
            {
                StringBuilder methods = new StringBuilder();
                StringBuilder interfaceMethods = new StringBuilder();
                foreach (MethodInfo method in ServiceProxySystem.GetProxiedMethods(type, includeLocalMethods))
                {
                    System.Reflection.ParameterInfo[] parameters = method.GetParameters();
                    MethodGenerationInfo methodGenInfo = new MethodGenerationInfo(method);
                    bool isVoidReturn = methodGenInfo.IsVoidReturn;
                    string returnType = methodGenInfo.ReturnTypeCodeString;
                    methodGenInfo.UsingNamespaces.Each(ns =>
                    {
                        usingNamespaces.Add(ns);
                    });

                    string returnOrBlank = isVoidReturn ? "" : "return ";
                    string genericTypeOrBlank = isVoidReturn ? "" : string.Format("<{0}>", returnType);
                    string invoke = string.Format("{0}Invoke{1}", returnOrBlank, genericTypeOrBlank);

                    string methodParams = methodGenInfo.MethodSignature;
                    string wrapped = parameters.ToDelimited(p => p.Name.CamelCase()); // wrapped as object array
                    string methodApiKeyRequired = method.HasCustomAttributeOfType<ApiKeyRequiredAttribute>() ? "\r\n\t[ApiKeyRequired]" : "";
                    methods.AppendFormat(MethodFormat, methodApiKeyRequired, returnType, method.Name, methodParams, wrapped, invoke);
                    interfaceMethods.AppendFormat(InterfaceMethodFormat, returnType, method.Name, methodParams);
                }

                string serverName = type.Name;
                string clientName = serverName;
                if (clientName.EndsWith("Server"))
                {
                    clientName = clientName.Truncate(6);
                }

                string classFormatToUse = type.HasCustomAttributeOfType<EncryptAttribute>() ? SecureClassFormat : ClassFormat;
                string typeApiKeyRequired = type.HasCustomAttributeOfType<ApiKeyRequiredAttribute>() ? "\r\n\t[ApiKeyRequired]" : "";
                classes.AppendFormat(classFormatToUse, typeApiKeyRequired, clientName, contractNamespace, serverName, defaultBaseAddress, methods.ToString());
                interfaces.AppendFormat(InterfaceFormat, serverName, interfaceMethods.ToString());
            }

            StringBuilder usings = new StringBuilder();
            usingNamespaces.Each(ns =>
            {
                usings.AppendFormat(UsingFormat, ns);
            });

            string usingStatements = usings.ToString();
            code.AppendFormat(HeaderFormat, defaultBaseAddress);
            code.AppendFormat(NameSpaceFormat, nameSpace, usingStatements, classes.ToString());
            code.AppendFormat(NameSpaceFormat, contractNamespace, usingStatements, interfaces.ToString());
            return code;
        }
        
        internal static StringBuilder GenerateJsProxyScript(Incubator incubator, string[] classes, bool includeLocal = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("(function(b, d, $, win){");
            stringBuilder.AppendLine(DaoProxyRegistration.GetDaoJsCtorScript(incubator, classes).ToString());

            foreach (string className in classes)
            {
                Type type = incubator[className];
                string varName = GetVarName(type);
                string var = string.Format("\tb.{0}", className);
                stringBuilder.Append(var);
                stringBuilder.Append(" = {};\r\n");
                stringBuilder.Append(var);
                stringBuilder.Append(".proxyName = \"{0}\";\r\n"._Format(varName));

                foreach (MethodInfo method in type.GetMethods())
                {
                    if (WillProxyMethod(method, includeLocal))
                    {
                        stringBuilder.AppendLine(GetMethodCall(type, method));
                    }
                }

                MethodInfo modelTypeMethod = type.GetMethod("GetDaoType");
                if (modelTypeMethod != null && modelTypeMethod.ReturnType == typeof(Type))
                {
                    Type modelType = (Type)modelTypeMethod.Invoke(null, null);
                    stringBuilder.Append("\td.entities = d.entities || {};");
                    stringBuilder.Append("\td.fks = d.fks || [];");
                    stringBuilder.AppendFormat("\td.entities.{0} = b.{0};\r\n", className);
                    stringBuilder.AppendFormat("\td.entities.{0}.ctx = '{1}';\r\n", className, Dao.ConnectionName(modelType));
                    stringBuilder.AppendFormat("\td.entities.{0}.cols = [];\r\n", className);

                    PropertyInfo[] modelProps = modelType.GetProperties();
                    foreach (PropertyInfo prop in modelProps)
                    {
                        ColumnAttribute col;
                        if (prop.HasCustomAttributeOfType<ColumnAttribute>(out col))
                        {
                            string typeName = prop.PropertyType.Name;
                            if (prop.PropertyType.IsGenericType &&
                                prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                typeName = prop.PropertyType.GetGenericArguments()[0].Name;
                            }

                            stringBuilder.AppendFormat("\td.entities.{0}.cols.push({{name: '{1}', type: '{2}', nullable: {3} }});\r\n", className, col.Name, typeName, col.AllowNull ? "true" : "false");
                        }

                        ForeignKeyAttribute fk;
                        if (prop.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                        {
                            stringBuilder.AppendFormat("\td.fks.push({{ pk: '{0}', pt: '{1}', fk: '{2}', ft: '{3}', nullable: {4} }});\r\n", fk.ReferencedKey, fk.ReferencedTable, fk.Name, fk.Table, fk.AllowNull ? "true" : "false");
                        }
                    }
                }

                stringBuilder.AppendFormat("\twin.{0} = win.{0} || {{}};\r\n", varName);
                stringBuilder.AppendFormat("\t$.extend(win.{0}, {1});\r\n", varName, var.Trim());
                stringBuilder.AppendFormat("\twin.{0}.className = '{1}';\r\n", varName, className);
                stringBuilder.AppendFormat("\td.{0} = b.{1};\r\n", varName, className);
            }

            stringBuilder.AppendLine("})(bam, dao, jQuery, window || {});");

            return stringBuilder;
        }

        internal static string GetMethodCall(Type type, MethodInfo method)
        {
            StringBuilder builder = new StringBuilder();
            System.Reflection.ParameterInfo[] parameterInfos = method.GetParameters();
            string defaultMethodName;
            string otherMethodName;
            GetMethodDetails(method, out defaultMethodName, out otherMethodName);

            string parameters = parameterInfos.ToArray().ToDelimited((pi) => pi.Name);
            string comma = parameterInfos.Length > 0 ? ", " : "";
            builder.AppendFormat("\tb.{0}.{1} = function({2}{3}options)", type.Name, defaultMethodName, parameters, comma);
            builder.AppendLine("{");

            if (type.HasCustomAttributeOfType<EncryptAttribute>())
            {
                builder.AppendFormat("\t\treturn b.secureInvoke('{0}', '{1}', [{2}], options != null ? (options.format == null ? 'json': options.format) : 'json', $.isFunction(options) ? {3} : options);\r\n", type.Name, method.Name, parameters, "{success: options}");
            }
            else
            {
                builder.AppendFormat("\t\treturn b.invoke('{0}', '{1}', [{2}], options != null ? (options.format == null ? 'json': options.format) : 'json', $.isFunction(options) ? {3} : options);\r\n", type.Name, method.Name, parameters, "{success: options}");
            }
            
            builder.AppendLine("\t};");

            MethodCase methodCase = GetMethodCase(type.GetCustomAttributeOfType<ProxyAttribute>());
            if (methodCase == MethodCase.Both)
            {
                builder.AppendFormat("\tb.{0}.{1} = b.{0}.{2};", type.Name, otherMethodName, defaultMethodName);
                builder.AppendLine();
            }
            return builder.ToString();
        }

        internal static void GetMethodDetails(MethodInfo method, out string defaultMethodName, out string otherMethodName)
        {
            string camelCasedMethodName = method.Name.CamelCase();
            string pascalCasedMethodName = method.Name.PascalCase();
            defaultMethodName = camelCasedMethodName;
            otherMethodName = pascalCasedMethodName;

            MethodCase methodCase = GetMethodCase(method.DeclaringType.GetCustomAttributeOfType<ProxyAttribute>());

            switch (methodCase)
            {
                case MethodCase.Invalid:
                    throw new InvalidOperationException("Invalid MethodCase specified");
                case MethodCase.CamelCase:
                    defaultMethodName = camelCasedMethodName;
                    otherMethodName = pascalCasedMethodName;
                    break;
                case MethodCase.PascalCase:
                    defaultMethodName = pascalCasedMethodName;
                    otherMethodName = camelCasedMethodName;
                    break;
                case MethodCase.Both:
                    break;
                default:
                    break;
            }
        }

        internal static MethodCase GetMethodCase(ProxyAttribute proxyAttr)
        {
            MethodCase methodCase = MethodCase.Both;

            if (proxyAttr != null)
            {
                methodCase = proxyAttr.MethodCase;
            }
            return methodCase;
        }

        internal static string GetVarName(Type type)
        {
            string varName = type.Name;
            ProxyAttribute proxyAttr = null;
            if (type.HasCustomAttributeOfType<ProxyAttribute>(true, out proxyAttr))
            {
                if(!string.IsNullOrEmpty(proxyAttr.VarName))
                {
                    varName = proxyAttr.VarName;
                }
            }

            return varName;
        }

        internal static bool ServiceProxyPartialExists(Type type, string viewName)
        {
            return ServiceProxyPartialExists(type.Name, viewName);
        }

        internal static bool ServiceProxyPartialExists(string typeName, string viewName)
        {
            List<string> fileExtensions = new List<string>();
            fileExtensions.Add(".cshtml");
            fileExtensions.Add(".vbhtml");
            fileExtensions.Add(".aspx");
            fileExtensions.Add(".ascx");
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

        private static void WriteServiceProxyPartial(string path, StringBuilder source)
        {
            FileInfo file = new FileInfo(path);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(source.ToString());
            }
        }

        private static StringBuilder BuildPartialView(Type type)
        {
            StringBuilder source = new StringBuilder();
            string typeName = type.Name.DropTrailingNonLetters();
            Type[] genericTypes = type.GetGenericArguments();
            if (genericTypes.Length > 0)
            {
                typeName = "{0}<{1}>"._Format(typeName, genericTypes.ToDelimited(t => t.Name));
            }
            source.AppendFormat("@model {0}.{1}", type.Namespace, typeName);
            source.AppendLine();
            source.Append("<div type=\"object\" itemscope itemtype=\"http://schema.org/Thing\">\r\n");

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (!property.HasCustomAttributeOfType<ExcludeAttribute>())
                {
                    source.AppendFormat("\t{0}: <span itemprop=\"{0}\">@Model.{0}</span>", property.Name);
                    source.AppendLine("<br />");
                }
            }
            source.AppendLine("</div>");
            return source;
        }


    }
}
