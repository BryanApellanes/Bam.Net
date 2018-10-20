/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Yaml
{
    public static class ServiceProxySystemYaml
    {
        /// <summary>
        /// Registers the yaml file extension and the delegate used
        /// to handle it.
        /// </summary>
        public static void Register()
        {
            ServiceProxySystem.RegisterServiceProxyRequestDelegate("yaml", Yaml);
        }

        public static YamlResult Yaml(ExecutionRequest request)
        {
            return new YamlResult(request.Result);
        }

        public static void RegisterYaml(this ServiceProxySystem sys)
        {
            Register();
        }
    }
}
