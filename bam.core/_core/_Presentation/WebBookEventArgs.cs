/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Server;

namespace Bam.Net.Presentation
{
    public class WebBookEventArgs: EventArgs
    {
        public WebBookEventArgs(AppConf conf)
        {
            this.AppConf = conf;
        }

        public AppConf AppConf { get; set; }
    }
}
