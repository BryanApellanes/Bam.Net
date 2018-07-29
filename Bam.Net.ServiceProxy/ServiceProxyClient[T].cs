/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Bam.Net.Logging;
using Bam.Net.Encryption;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System.IO;
using System.Reflection;
using System.Collections;
using Bam.Net.Web;

namespace Bam.Net.ServiceProxy
{
    public class ServiceProxyClient<T> : ServiceProxyClient
    {
        public ServiceProxyClient(string baseAddress)
            : base(baseAddress)
        {
            if (!string.IsNullOrEmpty(BaseAddress) && !BaseAddress.EndsWith("/"))
            {
                BaseAddress = string.Format("{0}/", BaseAddress);
            }

            this.Format = "json";
            this.MethodUrlFormat = "{BaseAddress}{Verb}/{ClassName}/{MethodName}.{Format}?{Parameters}{NamedOrNumberd}";
            this.Numbered = true;
        }

        public ServiceProxyClient(string baseAddress, string implementingClassName)
            : this(baseAddress)
        {
            this.ClassName = implementingClassName;
            this.Numbered = true;
        }

        string _className;
        object _classNameLock = new object();
        /// <summary>
        /// The name of the implementing class on the server.  If typeof(T)
        /// is an interface as determined by typeof(T).IsInterface then it
        /// is assumed that the classname equals typeof(T).Name.Substring(1)
        /// which drops the first character of the name.
        /// </summary>
        public string ClassName
        {
            get
            {
                return _classNameLock.DoubleCheckLock(ref _className, () => typeof(T).IsInterface ? typeof(T).Name.Substring(1) : typeof(T).Name);
            }
            set
            {
                _className = value;
            }
        }

        string[] _methods;
        object _methodsLock = new object();
        public string[] Methods
        {
            get
            {
                return _methodsLock.DoubleCheckLock(ref _methods, () => ServiceProxySystem.GetProxiedMethods(typeof(T)).Select(m => m.Name).ToArray());
            }
        }

        public string MethodUrlFormat
        {
            get;
            set;
        }

        public string Format
        {
            get;
            set;
        }

        bool _numbered;
        /// <summary>
        /// If true, get requests will have numbered querystring parameters, for example
        /// 0=value1&1=value2&2={"some":"jsonValue"}
        /// </summary>
        public bool Numbered
        {
            get
            {
                return _numbered;
            }
            set
            {
                _numbered = value;
                _named = !value;
            }
        }

        bool _named;
        /// <summary>
        /// If true, get requests will have named querystring parameters, for example
        /// first=value1&another=value2&complex={"some":"jsonValue"}
        /// </summary>
        public bool Named
        {
            get
            {
                return _named;
            }
            set
            {
                _named = value;
                _numbered = !value;
            }
        }

        public string LastResponse { get; private set; }

        /// <summary>
        /// Invoke the specified methodName on the server side
        /// type T returning value of type T1
        /// </summary>
        /// <typeparam name="T1">The return type of the specified method</typeparam>
        /// <param name="methodName">The name of the method to invoke</param>
        /// <param name="parameters">parameters to be passed to the method</param>
        /// <returns></returns>
        public T1 Invoke<T1>(string methodName, params object[] parameters)
        {
            string result = Invoke(methodName, parameters);
            return result.FromJson<T1>();
        }

        /// <summary>
        /// Invoke the specified methodName on the specified
        /// server side className specified returning value of 
        /// type T1
        /// </summary>
        /// <typeparam name="T1">The return type of the specified method</typeparam>
        /// <param name="className">The name of the server side class to invoke the method on</param>
        /// <param name="methodName">The name of the method to invoke</param>
        /// <param name="parameters">parameters to be passed to the method</param>
        /// <returns></returns>
        public T1 Invoke<T1>(string className, string methodName, params object[] parameters)
        {
            string result = Invoke(className, methodName, parameters);
            return result.FromJson<T1>();
        }

        /// <summary>
        /// Invoke the specified methodName on the specified
        /// server side className at the specified baseAddress
        /// returning value of type T1
        /// </summary>
        /// <typeparam name="T1">The return type of the specified method</typeparam>
        /// <param name="baseAddress">The base uri to send the request to</param>
        /// <param name="className">The name of the server side class to invoke the method on</param>
        /// <param name="methodName">The name of the method to invoke</param>
        /// <param name="parameters">parameters to be passed to the method</param>
        /// <returns></returns>
        public T1 Invoke<T1>(string baseAddress, string className, string methodName, params object[] parameters)
        {
            string result = Invoke(baseAddress, className, methodName, parameters);
            return result.FromJson<T1>();
        }

        /// <summary>
        /// Invoke the specified methodName on the server side
        /// type T
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override string Invoke(string methodName, params object[] parameters)
        {
            return Invoke(ClassName, methodName, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Invoke(string className, string methodName, params object[] parameters)
        {
            return Invoke(BaseAddress, className, methodName, parameters);
        }

        /// <summary>
        /// This method provides core method invoke functionality.  
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Invoke(string baseAddress, string className, string methodName, params object[] parameters)
        {
            if (!Methods.Contains(methodName) && typeof(T).Name.Equals(className))
            {
                throw Args.Exception<InvalidOperationException>("{0} is not proxied from type {1}", methodName, className);
            }

            if (!baseAddress.EndsWith("/"))
            {
                baseAddress = string.Format("{0}", baseAddress);
            }

            ServiceProxyInvokeEventArgs args = new ServiceProxyInvokeEventArgs { BaseAddress = baseAddress, ClassName = className, Client = this, MethodName = methodName, PostParameters = parameters };
            OnInvokingMethod(args);

            string result = string.Empty;
            if (args.CancelInvoke)
            {
                OnInvokeCanceled(args);
            }
            else
            {
                string tmp = DoInvoke(args);

                result = tmp;
                LastResponse = result;

                OnInvokedMethod(args);
            }
            return result;
        }

        protected internal virtual string DoInvoke(ServiceProxyInvokeEventArgs args)
        {
            string baseAddress = args.BaseAddress;
            string className = args.ClassName;
            string methodName = args.MethodName;
            object[] parameters = args.PostParameters;
            try
            {
                string tmp = string.Empty;
                GetQueryStringAndVerb(methodName, parameters, out string queryStringParameters, out ServiceProxyVerbs verb);

                if (verb == ServiceProxyVerbs.POST)
                {
                    tmp = Post(args);
                }
                else
                {
                    args.QueryStringParameters = queryStringParameters;
                    tmp = Get(args);
                }
                return tmp;
            }
            catch (Exception ex)
            {
                args.Exception = ex;
                OnInvocationException(args);
            }
            return string.Empty;
        }

        /// <summary>
        /// Get an HttpWebRequest instance that represents a call to the 
        /// specified methodName of the current generic type T
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected internal HttpWebRequest GetServiceProxyRequest(string methodName, params object[] parameters)
        {
            return GetServiceProxyRequest<T>(methodName, parameters);
        }

        protected internal HttpWebRequest GetServiceProxyRequest(ServiceProxyVerbs verb, string methodName, string queryStringParameters = "")
        {
            return GetServiceProxyRequest<T>(verb, methodName, queryStringParameters);
        }

        /// <summary>
        /// Get an HttpWebRequest for the specified server generic type ST
        /// </summary>
        /// <typeparam name="ST"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected internal HttpWebRequest GetServiceProxyRequest<ST>(string methodName, params object[] parameters)
        {
            GetQueryStringAndVerb(methodName, parameters, out string queryStringParameters, out ServiceProxyVerbs verb);
            return GetServiceProxyRequest<ST>(verb, methodName, queryStringParameters);
        }

        protected internal HttpWebRequest GetServiceProxyRequest<ST>(ServiceProxyVerbs verb, string methodName, params object[] parameters)
        {
            GetQueryStringAndVerb(methodName, parameters, out string queryStringParameters, out ServiceProxyVerbs ignore);
            return GetServiceProxyRequest<ST>(verb, methodName, queryStringParameters);
        }

        /// <summary>
        /// Get an HttpWebRequest for the specified server generic type ST
        /// </summary>
        /// <typeparam name="ST">The server type that will execute the request</typeparam>
        /// <param name="verb"></param>
        /// <param name="methodName"></param>
        /// <param name="queryStringParameters"></param>
        /// <returns></returns>
        protected internal HttpWebRequest GetServiceProxyRequest<ST>(ServiceProxyVerbs verb, string methodName, string queryStringParameters = "")
        {
            return GetServiceProxyRequest(verb, typeof(ST).Name, methodName, queryStringParameters);
        }

        /// <summary>
        /// Get an HttpWebRequest for the specified server type of the specified className
        /// </summary>
        /// <param name="verb"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="queryStringParameters"></param>
        /// <returns></returns>
        protected virtual internal HttpWebRequest GetServiceProxyRequest(ServiceProxyVerbs verb, string className, string methodName, string queryStringParameters = "")
        {
            string namedOrNumbered = this.Numbered ? "&numbered=1" : "&named=1";
            string methodUrl = MethodUrlFormat.NamedFormat(new { BaseAddress, Verb = verb, ClassName = className, MethodName = methodName, Format, Parameters = queryStringParameters, NamedOrNumberd = namedOrNumbered });
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(methodUrl);
            request.CookieContainer = Cookies ?? new CookieContainer();
            request.UserAgent = UserAgent;
            request.Headers.Add(CustomHeaders.ProcessMode, ProcessMode.Current.Mode.ToString());
            Headers.AllKeys.Where(k=> !k.Equals("User-Agent")).Each(key =>
            {                
                request.Headers.Add(key, Headers[key]);
            });
            request.Proxy = null;
            request.Method = verb.ToString();
            
            if (!string.IsNullOrEmpty(Referer))
            {
                request.Referer = Referer;
            }

            return request;
        }
        
        private void GetQueryStringAndVerb(string methodName, object[] parameters, out string queryStringParameters, out ServiceProxyVerbs verb)
        {
            queryStringParameters = Numbered ? ParametersToQueryString(parameters) : ParametersToQueryString(NameParameters(methodName, parameters));
            verb = queryStringParameters.Length > 2048 ? ServiceProxyVerbs.POST : ServiceProxyVerbs.GET;
        }

        /// <summary>
        /// Gets the response for the specified request.  All ServiceProxy Post and Get calls result
        /// in this method being called.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual internal HttpWebResponse GetServiceProxyResponse(HttpWebRequest request)
        {
            if (request.CookieContainer == null)
            {
                request.CookieContainer = Cookies;
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            foreach (Cookie cookie in response.Cookies)
            {
                Cookies.Add(cookie);                
            }

            return response;
        }

        public string GetServiceProxyResponseString(HttpWebRequest request)
        {
            HttpWebResponse response = GetServiceProxyResponse(request);
            string result = string.Empty;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        public RT GetServiceProxyResponse<RT>(HttpWebRequest request)
        {
            return GetServiceProxyResponseString(request).FromJson<RT>();
        }

        public event EventHandler<ServiceProxyInvokeEventArgs<T>> Getting;
        public event EventHandler<ServiceProxyInvokeEventArgs<T>> Got;

        /// <summary>
        /// Fires the Getting event 
        /// </summary>
        /// <param name="args"></param>
        protected void OnGetting(ServiceProxyInvokeEventArgs<T> args)
        {
            if (Getting != null)
            {
                Getting(this, args);
            }
        }

        public event EventHandler<ServiceProxyInvokeEventArgs> GetCanceled;
        protected void OnGetCanceled(ServiceProxyInvokeEventArgs args)
        {
            if (GetCanceled != null)
            {
                GetCanceled(this, args);
            }

            OnInvokeCanceled(args);
        }

        /// <summary>
        /// Fires the Got event
        /// </summary>
        /// <param name="args"></param>
        protected void OnGot(ServiceProxyInvokeEventArgs<T> args)
        {
            if (Got != null)
            {
                Got(this, args);
            }
        }

        public event EventHandler<ServiceProxyInvokeEventArgs<T>> Posting;
        public event EventHandler<ServiceProxyInvokeEventArgs<T>> Posted;

        /// <summary>
        /// Fires the Posting event 
        /// </summary>
        /// <param name="args"></param>
        protected void OnPosting(ServiceProxyInvokeEventArgs<T> args)
        {
            if (Posting != null)
            {
                Posting(this, args);
            }
        }


        public event EventHandler<ServiceProxyInvokeEventArgs> PostCanceled;
        protected void OnPostCanceled(ServiceProxyInvokeEventArgs args)
        {
            if (PostCanceled != null)
            {
                PostCanceled(this, args);
            }

            OnInvokeCanceled(args);
        }
        /// <summary>
        /// Fires the Got event
        /// </summary>
        /// <param name="args"></param>
        protected void OnPosted(ServiceProxyInvokeEventArgs<T> args)
        {
            if (Posted != null)
            {
                Posted(this, args);
            }
        }

        public R Get<R>(string methodName, params object[] parameters)
        {
            return Get(methodName, parameters).FromJson<R>();
        }

        public R Get<R>(string className, string methodName, params object[] parameters)
        {
            return Get(BaseAddress, className, methodName, parameters).FromJson<R>();
        }

        public R Get<R>(string baseAddress, string className, string methodName, string queryStringParameters)
        {
            return Get(new ServiceProxyInvokeEventArgs { BaseAddress = baseAddress, ClassName = className, MethodName = methodName, QueryStringParameters = queryStringParameters }).FromJson<R>();// baseAddress, className, methodName, queryStringParameters).FromJson<R>();
        }

        public string Get(string methodName, params object[] parameters)
        {
            this.Numbered = true;
            return Get(new ServiceProxyInvokeEventArgs { BaseAddress = BaseAddress, ClassName = typeof(T).Name, MethodName = methodName, QueryStringParameters = new ServiceProxyParameters(parameters).NumberedQueryStringParameters });//BaseAddress, typeof(T).Name, methodName, new ServiceProxyParameters(parameters).NumberedQueryStringParameters);
        }

        public string Get(string className, string methodName, params object[] parameters)
        {
            return Get(BaseAddress, className, methodName, parameters);
        }

        public string Get(string baseAddress, string className, string methodName, params object[] parameters)
        {
            ServiceProxyParameters proxyParameters = new ServiceProxyParameters(parameters);
            string queryStringParameters = proxyParameters.NumberedQueryStringParameters;

            return Get(new ServiceProxyInvokeEventArgs<T> { BaseAddress = baseAddress, ClassName = className, MethodName = methodName, QueryStringParameters = queryStringParameters });// baseAddress, className, methodName, queryStringParameters);
        }

        protected virtual string Get(ServiceProxyInvokeEventArgs argsIn)
        {
            ServiceProxyInvokeEventArgs<T> args = argsIn.CopyAs<ServiceProxyInvokeEventArgs<T>>();
            args.Client = this;
            args.GenericClient = this;
            string className = args.ClassName;
            string methodName = args.MethodName;
            string queryStringParameters = args.QueryStringParameters;
            OnGetting(args);
            string result = string.Empty;
            if (args.CancelInvoke)
            {
                OnGetCanceled(args);
            }
            else
            {
                HttpWebRequest request = GetServiceProxyRequest(ServiceProxyVerbs.GET, className, methodName, queryStringParameters);
                result = GetServiceProxyResponseString(request);
                args.Request = request;
                OnGot(args);
            }
            return result; ;
        }

        public R Post<R>(string methodName, params object[] parameters)
        {
            return Post(new ServiceProxyInvokeEventArgs { BaseAddress = BaseAddress, ClassName = ClassName, MethodName = methodName, PostParameters = parameters }).FromJson<R>();//Post(BaseAddress, ClassName, methodName, parameters).FromJson<R>();
        }

        public R Post<R>(string className, string methodName, params object[] parameters)
        {
            return Post(new ServiceProxyInvokeEventArgs { BaseAddress = BaseAddress, ClassName = className, MethodName = methodName, PostParameters = parameters }).FromJson<R>();// BaseAddress, className, methodName, parameters).FromJson<R>();
        }

        public string Post(string methodName, params object[] parameters)
        {
            return Post(new ServiceProxyInvokeEventArgs { BaseAddress = BaseAddress, ClassName = typeof(T).Name, MethodName = methodName, PostParameters = parameters });// BaseAddress, typeof(T).Name, methodName, parameters);
        }

        public string Post(string className, string methodName, params object[] parameters)
        {
            return Post(new ServiceProxyInvokeEventArgs<T> { BaseAddress = BaseAddress, ClassName = className, MethodName = methodName, PostParameters = parameters });// BaseAddress, className, methodName, parameters);
        }

        /// <summary>
        /// Post to the url representing the specified method call.  Content type used
        /// will be "application/json; charset=utf-8";.  This can be overridden in a derived
        /// class by overriding WriteJsonParams or GetServiceProxyResponse, each of which
        /// is called after the ContentType is set on the request.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual string Post(ServiceProxyInvokeEventArgs args)
        {
            HttpWebRequest request = GetServiceProxyRequest(ServiceProxyVerbs.POST, args.ClassName, args.MethodName, "nocache=".RandomLetters(4));
            return Post(args, request);
        }

        protected virtual string Post(ServiceProxyInvokeEventArgs invokeArgs, HttpWebRequest request)
        {
            ServiceProxyInvokeEventArgs<T> args = invokeArgs.CopyAs<ServiceProxyInvokeEventArgs<T>>();
            args.Client = this;
            args.GenericClient = this;

            OnPosting(args);
            string result = string.Empty;
            if (args.CancelInvoke)
            {
                OnPostCanceled(args);
            }
            else
            {
                string jsonParamsString = ApiParameters.ParametersToJsonParamsObjectString(args.PostParameters);

                request.ContentType = "application/json; charset=utf-8";

                WriteJsonParams(jsonParamsString, request);

                result = GetServiceProxyResponseString(request);
                args.Request = request;
                OnPosted(args);
            }

            return result;
        }

        /// <summary>
        /// Writes the specified jsonParamsString to the request stream of the
        /// specified request.
        /// </summary>
        /// <param name="jsonParamsString"></param>
        /// <param name="request"></param>
        protected internal virtual void WriteJsonParams(string jsonParamsString, HttpWebRequest request)
        {
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(jsonParamsString);
            }
        }

        /// <summary>
        /// Names the specified parameters by aligning them with the
        /// parameters of the specified methodName.  The keys of 
        /// the resulting dictionary are the names of the parameters
        /// defined in the specified methodName
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected internal Dictionary<string, object> NameParameters(string methodName, object[] parameters)
        {
            if (!Methods.Contains(methodName))
            {
                throw Args.Exception<InvalidOperationException>("{0} is not proxied from type {1}", methodName, typeof(T).Name);
            }

            MethodInfo method = typeof(T).GetMethod(methodName);

            Dictionary<string, object> result = NameParameters(method, parameters);

            return result;
        }
    }
}
