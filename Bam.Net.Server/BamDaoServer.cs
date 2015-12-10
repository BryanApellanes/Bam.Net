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
    /// BamServer where EnableDao is true
    /// and EnableServiceProxy is false
    /// </summary>
    public class BamDaoServer: BamServer
    {
        public BamDaoServer(BamConf conf)
            : base(conf)
        {
            this.EnableDao = true;
            this.EnableServiceProxy = true;
        }
    }
}
