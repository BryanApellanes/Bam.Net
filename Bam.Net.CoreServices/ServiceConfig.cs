using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using System.IO;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Server
{
    public static class ServiceConfig
    {
        public static string ContentRoot
        {
            get
            {
                return DefaultConfiguration.GetAppSetting("ContentRoot").Or(".\\");
            }
        }

        public static string ApplicationName
        {
            get
            {
                return DefaultConfiguration.GetAppSetting("ApplicationName").Or(DefaultConfiguration.DefaultApplicationName);
            }
        }

        public static Database GetConfiguredDatabase()
        {
            string dialect = DefaultConfiguration.GetAppSetting("SqlDialect");
            string connectionString = DefaultConfiguration.GetAppSetting("SqlConnectionString");

            SqlDialect sqlDialect;
            if(!Enum.TryParse<SqlDialect>(dialect, out sqlDialect))
            {
                sqlDialect = SqlDialect.SQLite;
                connectionString = "Default";
            }

            return DatabaseFactory.Instance.GetDatabase(sqlDialect, connectionString);
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
