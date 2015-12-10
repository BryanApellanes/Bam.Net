using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using System.IO;
using Bam.Net.Data;

namespace Bam.Net.Server.Tvg
{
    public static class ServiceConfig
    {
        public static string ContentRoot
        {
            get
            {
                return DefaultConfiguration.GetAppSetting("ContentRoot");
            }
        }
        public static Database GetConfiguredDatabase()
        {
            // read dialect from config and instantiate as appropriate
            throw new NotImplementedException();
        }
        public static HostPrefix GetConfiguredHostPrefix()
        {
            HostPrefix hostPrefix = new HostPrefix();
            hostPrefix.HostName = DefaultConfiguration.GetAppSetting("HostName").Or("localhost");
            hostPrefix.Port = int.Parse(DefaultConfiguration.GetAppSetting("Port"));
            hostPrefix.Ssl = bool.Parse(DefaultConfiguration.GetAppSetting("Ssl"));
            return hostPrefix;
        }
        static ILogger multiTargetLogger;
        public static ILogger GetMultiTargetLogger(ILogger logger)
        {
            if (multiTargetLogger == null)
            {
                MultiTargetLogger multiLogger = new MultiTargetLogger();
                TextFileLogger fileLogger = new TextFileLogger();
                fileLogger.Folder = new DirectoryInfo(ContentRoot);
                multiLogger.AddLogger(fileLogger);
                multiLogger.AddLogger(logger);
                multiLogger.StartLoggingThread();
                multiTargetLogger = multiLogger;
            }
            return multiTargetLogger;
        }
    }
}
