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
using Bam.Net.Services.Clients;

namespace Bam.Net.Services.Clients
{
    /// <summary>
    /// Dependency injection container for a locally 
    /// running application process
    /// </summary>
    [ServiceRegistryContainer]
    public class ApplicationServiceRegistry
    {
        static object _coreIncubatorLock = new object();
        static ServiceRegistry _coreIncubator;

        [ServiceRegistryLoader]
        public static ServiceRegistry GetRegistry()
        {
            return _coreIncubatorLock.DoubleCheckLock(ref _coreIncubator, Create);
        }

        private static ServiceRegistry Create()
        {
            string contentRoot = DefaultConfiguration.GetAppSetting("ContentRoot", "c:\\tvg\\gloo");
            string organization = DefaultConfiguration.GetAppSetting("Organization", "PUBLIC");
            string applicationName = DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN");
            string databasesPath = Path.Combine(contentRoot, "Databases");
            string workspaceDirectory = Path.Combine(contentRoot, "Workspace");

            CoreClient coreClient = new CoreClient(organization, applicationName, "bamapps.net", 80, workspaceDirectory, Log.Default);
            ApplicationLogDatabase logDb = new ApplicationLogDatabase(workspaceDirectory);

            return (ServiceRegistry)(new ServiceRegistry())
                .For<CoreClient>().Use(coreClient)
                .For<ApplicationLogDatabase>().Use(logDb)
                .For<ILogger>().Use<ApplicationLogger>()
                .For<ILog>().Use<ApplicationLogger>()
                .For<IConfigurationService>().Use<ApplicationConfigurationProvider>()
                .For<IUserManager>().Use(coreClient.UserRegistryService);
        }
    }
}
