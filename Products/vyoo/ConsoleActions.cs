using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Server;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;
using Bam.Net.Server.Tvg;
using System.Reflection;

namespace vyoo
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        static string contentRootConfigKey = "ContentRoot";
        static string defaultRoot = "C:\\tvg";
        static VyooServer vyooServer;
        
        [ConsoleAction("startVyooServer", "Start the vyoo server")]
        public void StartVyooServer()
        {
            ConsoleLogger logger = GetLogger();
            StartVyooServer(logger);
            Pause("Vyoo is running");
        }

        [ConsoleAction("killVyooServer", "Kill the vyoo server")]
        public void StopVyooServer()
        {
            if (vyooServer != null)
            {
                vyooServer.Stop();
                Pause("Vyoo stopped");
            }
            else
            {
                OutLine("Vyoo server not running");
            }
        }

        public static void StartVyooServer(ConsoleLogger logger)
        {
            BamConf conf = BamConf.Load(DefaultConfiguration.GetAppSetting(contentRootConfigKey).Or(defaultRoot));
            vyooServer = new VyooServer(conf, logger);
            vyooServer.HostPrefixes = new HostPrefix[] { GetConfiguredHostPrefix() };
            vyooServer.MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";");
            vyooServer.Start();
        }
        public static HostPrefix GetConfiguredHostPrefix()
        {
            HostPrefix hostPrefix = new HostPrefix();
            hostPrefix.HostName = DefaultConfiguration.GetAppSetting("HostName").Or("localhost");
            hostPrefix.Port = int.Parse(DefaultConfiguration.GetAppSetting("Port"));
            hostPrefix.Ssl = bool.Parse(DefaultConfiguration.GetAppSetting("Ssl"));
            return hostPrefix;
        }
        private static ConsoleLogger GetLogger()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.UseColors = true;
            logger.StartLoggingThread();
            return logger;
        }
    }
}