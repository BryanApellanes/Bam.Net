/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Reflection;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Web;
using Bam.Net.Configuration;
using Bam.Net.Incubation;

namespace Bam.Net.ServiceProxy
{
    public abstract class ServiceProxyClient : CookieEnabledWebClient
    {
        public ServiceProxyClient()
            : base() //Ensure that the cookiecontainer is initialized
        {
        }

        public ServiceProxyClient(string baseAddress)
            : this()
        {
            this.BaseAddress = baseAddress;
            this.Headers["User-Agent"] = UserAgents.ServiceProxyClient();
            this.UseDefaultCredentials = true;
        }

        ILogger _logger;
        object _loggerSync = new object();
        public ILogger Logger
        {
            get
            {
                return _loggerSync.DoubleCheckLock(ref _logger, () => Log.Default);
            }
            set
            {
                _logger = value;
            }
        }

        /// <summary>
        /// The class responsible for providing the name of the
        /// current application.
        /// </summary>
        public IApplicationNameProvider ClientApplicationNameProvider { get; set; }

        public string UserAgent
        {
            get
            {
                return this.Headers["User-Agent"];
            }
            set
            {
                this.Headers["User-Agent"] = value;
            }
        }

        /// <summary>
        /// Convert the specified type into a string or a json string if
        /// it is something other than a string or number (int, decimal, long)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static protected string TranslateParameter(object value)
        {
            if (value == null)
            {
                return "null";
            }

            Type type = value.GetType();
            if (type == typeof(string) ||
                type == typeof(int) ||
                type == typeof(decimal) ||
                type == typeof(long))
            {
                return value.ToString();
            }
            else
            {
                return WebUtility.UrlEncode(value.ToJson());
            }
        }

        internal static protected string ParametersToQueryString(Dictionary<string, object> parameters)
        {
            StringBuilder result = new StringBuilder();
            bool first = true;
            foreach (string key in parameters.Keys)
            {
                if (!first)
                {
                    result.Append("&");
                }

                result.AppendFormat("{0}={1}", key, TranslateParameter(parameters[key]));
                first = false;
            }
            
            return result.ToString();            
        }

        internal static protected string ParametersToQueryString(object[] parameters)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i != 0)
                {
                    result.Append("&");
                }

                result.AppendFormat("{0}={1}", i, TranslateParameter(parameters[i]));
            }

            return result.ToString();            
        }

        protected internal static Dictionary<string, object> NameParameters(MethodInfo method, object[] parameters)
        {
            List<ParameterInfo> parameterInfos = new List<ParameterInfo>(method.GetParameters());
            parameterInfos.Sort((l, r) => l.MetadataToken.CompareTo(r.MetadataToken));

            if (parameters.Length != parameterInfos.Count)
            {
                throw new InvalidOperationException("Parameter count mismatch");
            }

            Dictionary<string, object> result = new Dictionary<string, object>();
            parameterInfos.Each((pi, i) =>
            {
                result[pi.Name] = parameters[i];
            });
            return result;
        }

        /// <summary>
        /// Make a GET request to the specified path expecting json
        /// and deserialize it as the specified generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pathAndQuery"></param>
        /// <returns></returns>
        public T GetFromJson<T>(string pathAndQuery)
        {
            string url = GetUrl(pathAndQuery);
            string result = DownloadString(url);
            return result.FromJson<T>();
        }

        /// <summary>
        /// Make a GET request to the specified pathAndQuery expecting xml
        /// and deserialize it as the specified generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pathAndQuery"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public T GetFromXml<T>(string pathAndQuery, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            string url = GetUrl(pathAndQuery);
            string result = DownloadString(url);
            return result.FromXml<T>(encoding);
        }

        /// <summary>
        /// Post the specified postData to the specified pathAndQuery expecting
        /// json and deserializing it as the specified generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pathAndQuery"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public T PostFromJson<T>(string pathAndQuery, string postData)
        {
            string url = GetUrl(pathAndQuery);
            string result = UploadString(url, postData);
            return result.FromJson<T>();
        }

        /// <summary>
        /// Post the specified postData to the specified pathAndQuery expecting
        /// xml and deserializing it as the specified generic typ T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pathAndQuery"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public T PostFromXml<T>(string pathAndQuery, string postData)
        {
            string url = GetUrl(pathAndQuery);
            string result = UploadString(url, postData);
            return result.FromXml<T>();
        }

        private string GetUrl(string pathAndQuery)
        {
            string url = string.Format("{0}{1}", BaseAddress, pathAndQuery);
            return url;
        }
        /// <summary>
        /// The event that will occur if an exception occurs during
        /// method invocation
        /// </summary>
        public event EventHandler<ServiceProxyInvokeEventArgs> InvocationException;
        protected void OnInvocationException(ServiceProxyInvokeEventArgs args)
        {
            InvocationException?.Invoke(this, args);
        }
        public event EventHandler<ServiceProxyInvokeEventArgs> InvokingMethod;
        protected void OnInvokingMethod(ServiceProxyInvokeEventArgs args)
        {
            InvokingMethod?.Invoke(this, args);
        }

        public event EventHandler<ServiceProxyInvokeEventArgs> InvokedMethod;
        protected void OnInvokedMethod(ServiceProxyInvokeEventArgs args)
        {
            InvokedMethod?.Invoke(this, args);
        }

        public event EventHandler<ServiceProxyInvokeEventArgs> InvokeCanceled;
        protected void OnInvokeCanceled(ServiceProxyInvokeEventArgs args)
        {
            InvokeCanceled?.Invoke(this, args);
        }

        public abstract string Invoke(string methodName, object[] parameters);
    }
}
