/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net
{

    public class ServiceInfo
    {
        public ServiceInfo(string serviceName, string displayName, string description)
        {
            this.ServiceName = serviceName;
            this.DisplayName = displayName;
            this.Description = description;
        }

        public string ServiceName { get; protected set; }
        public string DisplayName { get; protected set; }
        public string Description { get; protected set; }
    }
}
