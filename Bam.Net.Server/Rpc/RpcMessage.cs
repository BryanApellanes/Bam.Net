using Bam.Net.ServiceProxy;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public abstract class RpcMessage
    {
        public string JsonRpc { get { return "2.0"; } }

        public static IRpcRequest Parse(IHttpContext context)
        {
            IRpcRequest request = Parse(context.Request);
            request.HttpContext = context;
            return request;
        }

        protected internal static IRpcRequest Parse(IRequest request)
        {
            IRpcRequest result = null;
            using (StreamReader sr = new StreamReader(request.InputStream))
            {
                string json = sr.ReadToEnd();
                result = Parse(json);
            }

            return result;
        }

        /// <summary>
        /// Parse the Json as an RpcMessage the 
        /// specific type of the message will depend
        /// on the json itself as described here http://www.jsonrpc.org/specification
        /// </summary>
        /// <returns></returns>
        protected internal static IRpcRequest Parse(string json)
        {
            IRpcRequest result = null;
            JToken parsed = JToken.Parse(json);
            JArray batch;
            if (parsed.Is<JObject>())
            {
                result = ParseRequest(parsed, json);
            }
            else if (parsed.Is<JArray>(out batch))
            {
                result = ParseBatch(batch);
            }

            return result;
        }

        protected static internal IRpcRequest ParseBatch(JArray batch)
        {
            RpcBatch rpcBatch = new RpcBatch();
            List<IRpcRequest> requests = new List<IRpcRequest>();
            batch.Each(jToken =>
            {
                requests.Add(ParseRequest(jToken, jToken.ToString()));
            });
            rpcBatch.Requests = requests.ToArray();
            return rpcBatch;
        }
        
        protected static internal IRpcRequest ParseRequest(JToken parsed, string json)
        {
            bool isNotification = parsed["id"] == null && parsed["Id"] == null;
            RpcNotification rpcMessage = isNotification ? json.FromJson<RpcNotification>() : json.FromJson<RpcRequest>();
            SetParams(parsed, rpcMessage);
            return rpcMessage;
        }

        protected static void SetParams(JToken parsed, RpcNotification notification)
        {
            JToken parms = parsed["params"];
            if (parms != null)
            {
                if (parms.Is<JObject>())
                {
                    notification.RpcParams.By = new RpcParameters.Structure { Name = parms };
                }
                else if (parms.Is<JArray>())
                {
                    notification.RpcParams.By = new RpcParameters.Structure { Position = parms.ToString().FromJson<object[]>() };
                }
            }
        }
    }
}
