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
using System.Reflection;
using Bam.Net.UserAccounts;
using Bam.Net.Data.Repositories;
using Bam.Net.UserAccounts.Data;
using Bam.Net.Data;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.Application
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        static string contentRootConfigKey = "ContentRoot";
        static string defaultRoot = "C:\\bam\\content";
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
            DataSettings.Default.SetDefaultDatabaseFor<Session>(out Database userDb);
            userDb.TryEnsureSchema<Session>();
            DataSettings.Default.SetDefaultDatabaseFor<SecureSession>(out Database sessionDb);
            sessionDb.TryEnsureSchema<SecureSession>();
            BamConf conf = BamConf.Load(DefaultConfiguration.GetAppSetting(contentRootConfigKey).Or(defaultRoot));
            vyooServer = new VyooServer(conf, logger, GetArgument("verbose", "Log responses to the console?").IsAffirmative())
            {
                HostPrefixes = new HashSet<HostPrefix>(HostPrefix.FromDefaultConfiguration("localhost", 7400)),
                MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";")
            };
            vyooServer.Start();
        }

        private static ConsoleLogger GetLogger()
        {
            ConsoleLogger logger = new ConsoleLogger()
            {
                AddDetails = false,
                UseColors = true
            };
            logger.StartLoggingThread();
            return logger;
        }
    }
}