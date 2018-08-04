using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    public class DefaultConfigurationLoggerProvider : ILoggerProvider
    {
        static DefaultConfigurationLoggerProvider _instance;
        static object _instanceLock = new object();
        public static DefaultConfigurationLoggerProvider Instance
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, () => new DefaultConfigurationLoggerProvider());
            }
        }

        public ILogger GetLogger()
        {
            return Log.Default;
        }
    }
}
