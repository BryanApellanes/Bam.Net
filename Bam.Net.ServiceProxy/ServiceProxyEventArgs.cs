/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Bam.Net.ServiceProxy
{
    public class ServiceProxyEventArgs<T>: ServiceProxyEventArgs
    {
        public ServiceProxyClient<T> GenericClient
        {
            get;
            set;
        }
    }

    public class ServiceProxyEventArgs: EventArgs
    {
        public ServiceProxyEventArgs()
        {

        }

        public ServiceProxyEventArgs(bool cancelInvoke)
        {
            this.CancelInvoke = cancelInvoke;
        }

        public bool CancelInvoke { get; set; }

        public string Message { get; set; }

        public ServiceProxyClient Client
        {
            get;
            set;
        }

        public string BaseAddress
        {
            get;
            set;
        }

        public string ClassName
        {
            get;
            set;
        }

        public string MethodName
        {
            get;
            set;
        }

        public string QueryStringParameters
        {
            get;
            set;
        }

        public object[] PostParameters
        {
            get;
            set;
        }

        public HttpWebRequest Request
        {
            get;
            set;
        }
    }
}
