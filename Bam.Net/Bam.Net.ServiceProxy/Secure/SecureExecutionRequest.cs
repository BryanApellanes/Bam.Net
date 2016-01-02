/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;

namespace Bam.Net.ServiceProxy.Secure
{
    public class SecureExecutionRequest: ExecutionRequest
    {
        public SecureExecutionRequest(IHttpContext context, string className, string methodName, string jsonParams)
        {
            Args.ThrowIfNull(context, "context");
            Args.ThrowIfNullOrEmpty(className, "className");
            Args.ThrowIfNullOrEmpty(methodName, "methodName");

            this.ClassName = className;
            this.MethodName = methodName;
            this.JsonParams = jsonParams;
            this.Ext = "json";
            this.Context = context;
            this.IsUnencrypted = true;

            this.Executed += (o, t) =>
            {
                EncryptResult();
            };
        }

        protected virtual void EncryptResult()
        {
            string resultJson = Result.ToJson();
            string jsonCipher = Session.Encrypt(resultJson);
            Result = jsonCipher;
        }

        public static SecureExecutionRequest Create<T>(IHttpContext context, string methodName, params object[] parameters)
        {
            return Create<T>(context, methodName, Incubator.Default, parameters);
        }
        public static SecureExecutionRequest Create<T>(IHttpContext context, string methodName, Incubator serviceProvider, params object[] parameters)
        {
            string jsonParams = ApiParameters.ParametersToJsonParamsArray(parameters).ToJson();
            SecureExecutionRequest request = new SecureExecutionRequest(context, typeof(T).Name, methodName, jsonParams);            
            request.ServiceProvider = serviceProvider;
            return request;
        }

        protected internal override void Initialize()
        {
            // turn off initialization for this type
            //base.Initialize();
        }

        protected internal override void ParseRequestUrl()
        {
            // effectively turns off parsing of the url since
            // everything is explicitly set already
            //base.ParseRequestUrl();
        }

        SecureSession _session;
        object _sessionSync = new object();
        public SecureSession Session
        {
            get
            {
                return _sessionSync.DoubleCheckLock(ref _session, () => SecureSession.Get(Context));
            }
        }

        public SecureSession ReloadSession()
        {
            _session = null;
            return Session;
        }

        /// <summary>
        /// Decrypts the result and returns it as the specified type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetResultAs<T>()
        {
            string json = Session.Decrypt((string)Result);
            return json.FromJson<T>();
        }
    }
}
