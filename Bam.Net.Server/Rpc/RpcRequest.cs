using Bam.Net.Incubation;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public class RpcRequest: RpcNotification, IRpcRequest
    {
        public object Id { get; set; }

        public IHttpContext HttpContext { get; set; }
        /// <summary>
        /// Execute the request and return the response
        /// </summary>
        /// <returns></returns>
        public override RpcResponse Execute()
        {
            RpcResponse response = base.Execute();
            response.Id = Id;
            return response;
        }

        public static RpcRequest Parse(string json)
        {
            return json.FromJson<RpcRequest>();
        }

        public static RpcRequest Create<T>(Incubator incubator, string methodName, params object[] parameters)
        {
            return Create<T>(incubator, (object)Guid.NewGuid().ToString(), methodName, parameters);            
        }

        public static RpcRequest Create<T>(string methodName, params object[] parameters)
        {
            return Create<T>((object)Guid.NewGuid().ToString(), methodName, parameters);
        }

        public static RpcRequest Create<T>(object id, string methodName, params object[] parameters)
        {
            return Create<T>(Incubator.Default, id, methodName, parameters);
        }

        public static RpcRequest Create<T>(Incubator incubator, object id, string methodName, params object[] parameters)
        {
            return Create(incubator, id, typeof(T).GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray()), parameters);
        }

        public static RpcRequest Create(MethodInfo method, params object[] parameters)
        {
            return Create(Guid.NewGuid().ToString(), method, parameters);
        }

        public static RpcRequest Create(object id, MethodInfo method, params object[] parameters)
        {
            return Create(Incubator.Default, id, method, parameters);
        }

        public static RpcRequest Create(Incubator incubator, MethodInfo method, params object[] parameters)
        {
            return Create(incubator, (object)Guid.NewGuid().ToString(), method, parameters);            
        }

        public static RpcRequest Create(Incubator incubator, object id, MethodInfo method, params object[] parameters)
        {
            RpcNotification notification = RpcNotification.Create(incubator, method, parameters);
            RpcRequest request = notification.CopyAs<RpcRequest>();
            request.Incubator = incubator;
            request.Id = id;
            return request;
        }
    }
}
