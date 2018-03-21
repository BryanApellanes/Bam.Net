using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class AppConfApplicationNameProvider : IApplicationNameProvider
    {
        public AppConfApplicationNameProvider(AppConf conf)
        {
            AppConf = conf;
        }

        public AppConf AppConf { get; }

        public string GetApplicationName()
        {
            Args.ThrowIfNull(AppConf, "AppConf");
            return AppConf.Name;
        }
    }
}
