using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.UserAccounts;

namespace gloo.services
{
    [CoreRegistryContainer]
    public class ApplicationServiceRegistry
    {
        static object _coreIncubatorLock = new object();
        static CoreRegistry _coreIncubator;

        [CoreRegistryProvider]
        public static CoreRegistry GetRegistry()
        {
            return _coreIncubatorLock.DoubleCheckLock(ref _coreIncubator, Create);
        }

        private static CoreRegistry Create()
        {
            string contentRoot = DefaultConfiguration.GetAppSetting("ContentRoot", "c:\\tvg\\gloo");
            string organization = DefaultConfiguration.GetAppSetting("Organization", "PUBLIC");
            string applicationName = DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN");
            string databasesPath = Path.Combine(contentRoot, "Databases");
            string workspaceDirectory = Path.Combine(contentRoot, "Workspace");

            CoreClient coreClient = new CoreClient(organization, applicationName, "bamapps.net", 80, workspaceDirectory, Log.Default);
            ApplicationLogDatabase logDb = new ApplicationLogDatabase(workspaceDirectory);

            return (CoreRegistry)(new CoreRegistry())
                .For<CoreClient>().Use(coreClient)
                .For<ApplicationLogDatabase>().Use(logDb)
                .For<ILogger>().Use<ApplicationLogger>()
                .For<ILog>().Use<ApplicationLogger>()
                .For<IConfigurationService>().Use<ApplicationConfigurationService>()
                .For<IUserManager>().Use(coreClient.UserRegistryService);
        }
    }
}
