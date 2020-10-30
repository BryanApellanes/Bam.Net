using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using Bam.Net.Web;
using Bam.Net.Encryption;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;
using Bam.Net.Configuration;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// A secure communication channel.  Provides 
    /// application layer encrypted communication
    /// </summary>
    public partial class SecureChannel : IRequiresHttpContext // fx
    {
        public SecureChannelMessage<string> Invoke(string className, string methodName, string jsonParams)
        {
            SecureChannelMessage<string> result = new SecureChannelMessage<string>();

            HttpArgs args = new HttpArgs();
            args.ParseJson(jsonParams);
            string parameters = args["jsonParams"];
            SecureExecutionRequest request = new SecureExecutionRequest(HttpContext, className, methodName, parameters)
            {
                ApiKeyResolver = ApiKeyResolver,
                ServiceProvider = ServiceProvider
            };
            bool success = request.Execute();

            string data = request.Result as string;
            if (string.IsNullOrEmpty(data))
            {
                throw new SecureChannelInvokeException(className, methodName, jsonParams);
            }
            result.Data = data;
            result.Success = success;

            return result;
        }
    }
}
