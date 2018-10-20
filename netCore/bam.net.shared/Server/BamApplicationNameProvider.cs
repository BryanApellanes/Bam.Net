/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server
{
    public class BamApplicationNameProvider: IApplicationNameProvider
    {
        public BamApplicationNameProvider(AppConf conf)
        {
            this.Conf = conf;
        }

        protected internal AppConf Conf
        {
            get;
            set;
        }
        public string GetApplicationName()
        {
            return Conf.Name;
        }
    }
}
