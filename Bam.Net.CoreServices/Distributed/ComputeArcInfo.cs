/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Distributed
{
    public class ComputeArcInfo
    {
        public string ArcName { get; set; }
        public string HostName { get; set; }
        public string IPV4Address { get; set; }
        public string IPV6Address { get; set; }
        public string Port { get; set; }

        public static ComputeArcInfo FromComputeNode(ComputeArc computeNode)
        {
            ComputeArcInfo info = new ComputeArcInfo();
            info.CopyProperties(computeNode.GetInfo());
            return info;
        }
    }
}
