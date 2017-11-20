using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;

namespace Bam.Net.Application
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        static string contentRootConfigKey = "ContentRoot";
        static string defaultContentRoot = "C:\\bam\\content";
        static TrooServer trooServer;

        [ConsoleAction("startTrooServer", "Start the troo server")]
        public void StartConsole()
        {
            StartTrooServer(GetLogger(), GetRepository());
            Pause("Troo is running");
        }

        [ConsoleAction("killTrooServer", "Kill the troo server")]
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
            BamConf conf = BamConf.Load(DefaultConfiguration.GetAppSetting(contentRootConfigKey).Or(defaultContentRoot));
            trooServer = new TrooServer(conf, logger, repo)
            {
                HostPrefixes = new HashSet<HostPrefix> { GetConfiguredHostPrefix() },
                MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";")
            };
            trooServer.Start();
        }
        public static HostPrefix GetConfiguredHostPrefix()
        {
            HostPrefix hostPrefix = new HostPrefix()
            {
                HostName = DefaultConfiguration.GetAppSetting("HostName").Or("localhost"),
                Port = int.Parse(DefaultConfiguration.GetAppSetting("Port")),
                Ssl = bool.Parse(DefaultConfiguration.GetAppSetting("Ssl"))
            };
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