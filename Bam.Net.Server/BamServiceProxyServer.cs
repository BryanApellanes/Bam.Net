/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    /// <summary>
    /// BamServer where EnableDao is false
    /// and EnableServiceProxy is true
    /// </summary>
    public class BamServiceProxyServer: BamServer
    {
        public BamServiceProxyServer(BamConf conf)
            : base(conf)
        {
            this.EnableDao = false;
            this.EnableServiceProxy = true;
        }
    }
}
