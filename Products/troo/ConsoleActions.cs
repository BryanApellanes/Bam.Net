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
using Bam.Net.Logging;
using Bam.Net.Server.Tvg;
using System.Reflection;
using Bam.Net.Data.Repositories;

namespace troo
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        static string contentRoot = "ContentRoot";
        static string defaultRoot = "C:\\Troo";
        static TrooServer trooServer;

        [ConsoleAction]
        public void StartConsole()
        {
            StartTrooServer(GetLogger(), GetRepository());
            Pause("Troo is running");
        }

        [ConsoleAction]
        public void StopConsole()
        {
            if (trooServer != null)
            {
                trooServer.Stop();
                Pause("Troo stopped");
            }
            else
            {
                OutLine("Troo server not running");
            }
        }

        public static void StartTrooServer(ConsoleLogger logger, IRepository repo)
        {
            BamConf conf = BamConf.Load(DefaultConfiguration.GetAppSetting(contentRoot).Or(defaultRoot));
            trooServer = new TrooServer(conf, logger, repo);
            trooServer.HostPrefixes = new HostPrefix[] { GetConfiguredHostPrefix() };
            trooServer.MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";");
            trooServer.Start();
        }
        public static HostPrefix GetConfiguredHostPrefix()
        {
            HostPrefix hostPrefix = new HostPrefix();
            hostPrefix.HostName = DefaultConfiguration.GetAppSetting("HostName").Or("localhost");
            hostPrefix.Port = int.Parse(DefaultConfiguration.GetAppSetting("Port"));
            hostPrefix.Ssl = bool.Parse(DefaultConfiguration.GetAppSetting("Ssl"));
            return hostPrefix;
        }

        private static IRepository GetRepository()
        {
            return new DaoRepository();
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