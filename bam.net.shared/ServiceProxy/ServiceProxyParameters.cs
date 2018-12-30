/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.ServiceProxy
{
    /// <summary>
    /// Encapsulates method and parameters for 
    /// a ServiceProxy call
    /// </summary>
    public class ServiceProxyParameters
    {
        public ServiceProxyParameters(object[] parameters)
        {
            this.Parameters = parameters;
        }

        public ServiceProxyParameters(MethodInfo method, object[] parameters)
        {
            this.Method = method;
            this.Parameters = parameters;
        }

        public MethodInfo Method
        {
            get;
            set;
        }

        public object[] Parameters
        {
            get;
            set;
        }

        public string NumberedQueryStringParameters
        {
            get
            {
                return ServiceProxyClient.ParametersToQueryString(Parameters);
            }
        }

        public string NamedQueryStringParameters
        {
            get
            {
                return ServiceProxyClient.ParametersToQueryString(ServiceProxyClient.NameParameters(Method, Parameters));
            }
        }
    }
}
