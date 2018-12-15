using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    public class DefaultConfigurationLogProvider : LogProvider
    {
        public DefaultConfigurationLogProvider(LogReaderFactory logReaderFactory) : base(Log.Default, logReaderFactory)
        { }

        public override ILogger GetLogger()
        {
            return Log.Default;
        }
    }
}
