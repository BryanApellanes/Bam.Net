using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Server
{
    public interface IAppInitializer
    {
        void Initialize(AppConf conf);
        void Subscribe(ILogger logger);
    }
}
