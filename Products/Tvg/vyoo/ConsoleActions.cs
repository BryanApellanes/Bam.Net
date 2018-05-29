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
        static string hostAppMapsFileName = "hostAppMaps.json";

        static VyooServer vyooServer;
        
        [ConsoleAction("startVyooServer", "Start the vyoo server")]
        public void StartVyooServer()
        {
            StartVyooServer(GetLogger());
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

        [ConsoleAction("restartVyooServer", "Restart the vyoo server")]
        public void RestartVyooServer()
        {
            if(vyooServer != null)
            {
                vyooServer.Stop();
                StartVyooServer();
            }
        }

        public static void StartVyooServer(ConsoleLogger logger)
        {
            DataSettings.Current.SetDefaultDatabaseFor<Session>(out Database userDb);
            userDb.TryEnsureSchema<Session>();
            DataSettings.Current.SetDefaultDatabaseFor<SecureSession>(out Database sessionDb);
            sessionDb.TryEnsureSchema<SecureSession>();
            BamConf conf = BamConf.Load(DefaultConfiguration.GetAppSetting(contentRootConfigKey).Or(defaultRoot));
            AppConf[] appConfigs = conf.AppConfigs;

            List<HostPrefix> hostPrefixes = new List<HostPrefix>(HostPrefix.FromDefaultConfiguration("localhost", 7400));
            string hostAppMapsFilePath = Path.Combine(conf.ContentRoot, "apps", hostAppMapsFileName);
            if (File.Exists(hostAppMapsFilePath))
            {
                HostAppMap[] hostAppMaps = HostAppMap.Load(hostAppMapsFilePath);
                if (Arguments.Contains("apps"))
                {
                    string[] appNamesToServe = Arguments["apps"].DelimitSplit(",", ";");
                    appConfigs = conf.AppConfigs.Where(ac => ac.Name.In(appNamesToServe)).ToArray();
                    hostPrefixes.AddRange(hostAppMaps.Where(ham => ham.AppName.In(appNamesToServe)).Select(ham => new HostPrefix { HostName = ham.Host, Port = 80 }).ToArray());
                }
                else
                {
                    hostPrefixes.AddRange(HostPrefix.FromHostAppMaps(hostAppMaps));
                }
            }

            vyooServer = new VyooServer(appConfigs, logger, GetArgument("verbose", "Log responses to the console?").IsAffirmative())
            {
                HostPrefixes = new HashSet<HostPrefix>(hostPrefixes),
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