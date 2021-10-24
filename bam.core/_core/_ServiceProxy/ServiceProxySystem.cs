using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.ServiceProxy
{
    public partial class ServiceProxySystem // core
    {
        static bool initialized;
        static object initLock = new object();
        /// <summary>
        /// Initialize the underlying ServiceProxySystem, including registering the 
        /// necessary ServiceProxy routes in System.Web.Routing.RouteTable.Routes.
        /// </summary>
        public static void Initialize()
        {
            if (!initialized)
            {
                lock (initLock)
                {
                    initialized = true;
                }
            }
        }
    }
}
