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
using Bam.Net;
using Newtonsoft.Json;

namespace Bam.Net.ServiceProxy
{
    /// <summary>
    /// A class used to properly format parameters for service proxy calls. :( Sadness; this should be
    /// called ApiArguments and all instances of jsonParams should be jsonArgs.
    /// </summary>
    public class ApiParameters
    {
        public static string GetStringToHash(ExecutionRequest request)
        {
            return GetStringToHash(request.ClassName, request.MethodName, request.JsonParams);
        }
        public static string GetStringToHash(string className, string methodName, string jsonParams)
        {
            return string.Format("{0}.{1}.{2}", className, methodName, jsonParams);
        }
        /// <summary>
        /// Turn the specified parameter array into a JSON object in the form {jsonParams: &lt;json string array&gt;}.
        /// Where &lt;json string array&gt; can be obtained by callig ParametersToJsonParamsArray(parameters)
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string ParametersToJsonParamsObjectString(params object[] parameters)
        {
            string[] jsonParams = ParametersToJsonParamsArray(parameters);

            // stringify the array
            string jsonParamsString = (new { jsonParams = jsonParams.ToJson() }).ToJson();
            return jsonParamsString;
        }

        /// <summary>
        /// Returns an array of json strings that represent each parameter
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string[] ParametersToJsonParamsArray(params object[] parameters)
        {
            // create a string array
            string[] jsonParams = new string[parameters.Length];

            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            // for each parameter stringify it and shove it into the array
            parameters.Each((o, i) =>
            {
                jsonParams[i] = parameters[i].ToJson(settings);
            });
            return jsonParams;
        }

        public static Dictionary<string, object> NameParameters(MethodInfo method, params object[] parameters)
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
    }
}
