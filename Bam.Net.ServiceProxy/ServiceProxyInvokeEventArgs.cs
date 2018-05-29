/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using NCuid;

namespace Bam.Net.ServiceProxy
{
    public class ServiceProxyInvokeEventArgs<T>: ServiceProxyInvokeEventArgs
    {
        public ServiceProxyClient<T> GenericClient
        {
            get;
            set;
        }
    }

    public class ServiceProxyInvokeEventArgs: EventArgs
    {
        public ServiceProxyInvokeEventArgs()
        {
            Cuid = NCuid.Cuid.Generate();
        }

        public ServiceProxyInvokeEventArgs(bool cancelInvoke) : this()
        {
            CancelInvoke = cancelInvoke;
        }

        public ServiceProxyClient Client { get; set; }
        public HttpWebRequest Request { get; set; }
        public Exception Exception { get; set; }
        /// <summary>
        /// Can be used to uniquely identify an invocation
        /// when subscribing to events
        /// </summary>
        public string Cuid { get; internal set; }
        public bool CancelInvoke { get; set; }
        public string Message { get; set; }
        public string BaseAddress { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string QueryStringParameters { get; set; }
        public object[] PostParameters { get; set; }        
    }
}
