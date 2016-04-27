using Bam.Net.Incubation;
using Bam.Net.ServiceProxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public class RpcNotification : RpcMessage, IRpcRequest
    {
        public RpcNotification()
        {
            this.RpcParams = new RpcParameters();
        }
        [Exclude]
        public object Clone()
        {
            RpcNotification clone = new RpcNotification();
            clone.CopyProperties(this);
            return clone;
        }

        public IHttpContext HttpContext { get; set; }

        public string Method { get; set; }

        Incubator _incubator;
        protected internal Incubator Incubator
        {
            get
            {
                return _incubator;
            }
            set
            {
                _incubator = value;
            }
        }

        Type[] _types;
        object _typesLock = new object();
        protected internal Type[] RpcTypes
        {
            get
            {
                return _typesLock.DoubleCheckLock(ref _types, () => RpcMethods.Select(m => m.DeclaringType).ToArray());
            }
        }

        MethodInfo[] _methods;
        object _methodsLock = new object();
        protected internal MethodInfo[] RpcMethods
        {
            get
            {
                return _methodsLock.DoubleCheckLock(ref _methods, () => Incubator.Types.SelectMany(type => type.GetMethodsWithAttributeOfType<RpcMethodAttribute>()).ToArray());
            }
        }

        protected internal MethodInfo[] AllMethods
        {
            get
            {
                return Incubator.Types.SelectMany(type => type.GetMethods()).ToArray();
            }
        }

        /// <summary>
        /// The parameters.  This object will be either
        /// a JObject or JArray as indicated by the state
        /// of RpcParams
        /// </summary>
        public JToken Params { get; set; }
        public RpcParameters RpcParams { get; set; }


        public virtual RpcResponse Execute()
        {
            RpcResponse response = new RpcResponse();
            // get the method from RpcMethods
            MethodInfo mi = RpcMethods.FirstOrDefault(m => m.Name.Equals(Method, StringComparison.InvariantCultureIgnoreCase));
            // if its not there get it from all methods
            if (mi == null)
            {
                mi = AllMethods.FirstOrDefault(m => m.Name.Equals(Method, StringComparison.InvariantCultureIgnoreCase));
            }
            // if its not there set error in the response
            if (mi == null)
            {
                response = GetErrorResponse(RpcFaultCodes.MethodNotFound);
            }
            else
            {
                ExecutionRequest execRequest = ExecutionRequest.Create(Incubator, mi, GetInputParameters(mi));
                ValidationResult validation = execRequest.Validate();
                if (validation.Success)
                {
                    if(execRequest.ExecuteWithoutValidation())
                    {
                        response.Result = execRequest.Result;
                    }
                    else
                    {
                        response = GetErrorResponse(RpcFaultCodes.InternalError);
                    }                  
                }
                else
                {
                    response = GetErrorResponse(RpcFaultCodes.InvalidRequest);
                }
            }

            return response;
        }

        protected virtual RpcResponse GetErrorResponse(RpcFaultCodes faultCode)
        {
            RpcResponse response = new RpcResponse();
            RpcFaultCode code = RpcFaults.ByCode[(long)faultCode];
            RpcError error = new RpcError();
            error.Code = code.Code;
            error.Message = code.Message;
            error.Data = code.Meaning;
            response.Error = error;

            return response;
        }

        private object[] GetInputParameters(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            List<object> results = new List<object>(parameters.Length);
            if (RpcParams.Ordered)
            {
                ((JArray)Params).Each((jToken, i) =>
                {
                    ParameterInfo parameter = parameters[i];
                    AddParameter(results, jToken, parameter);
                });
            }
            else if (RpcParams.Named)
            {
                JObject parameterJson = (JObject)Params;
                parameters.Each(parameter =>
                {
                    JToken token = parameterJson[parameter.Name];
                    AddParameter(results, token, parameter);
                });
            }
            else
            {
                throw new InvalidOperationException("RpcParams is neither Named nor Ordered, it must be one or the other");
            }
            return results.ToArray();
        }

        public static RpcNotification Create<T>(Incubator serviceProvider, string methodName, params object[] parameters)
        {
            RpcNotification result = Create<T>(methodName, parameters);
            result.Incubator = serviceProvider;
            return result;
        }

        public static RpcNotification Create(MethodInfo method, params object[] parameters)
        {
            return Create(Incubator.Default, method, parameters);
        }

        public static RpcNotification Create<T>(string methodName, params object[] parameters)
        {
            return Create(typeof(T).GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray()), parameters);
        }

        public static RpcNotification Create(Incubator serviceProvider, MethodInfo method, params object[] parameters)
        {
            RpcNotification result = new RpcNotification();
            result.Incubator = serviceProvider;
            result.Method = method.Name;
            result.RpcParams.By.Position = parameters;
            result.Params = JToken.Parse(parameters.ToJson());
            return result;
        }

        private static void AddParameter(List<object> results, JToken jToken, ParameterInfo parameter)
        {
            if (jToken.HasValues)
            {
                object paramObject = JsonConvert.DeserializeObject(jToken.ToString(), parameter.ParameterType);
                results.Add(paramObject);
            }
            else
            {
                results.Add(Convert.ChangeType(jToken, parameter.ParameterType));
            }
        }
    }
}
