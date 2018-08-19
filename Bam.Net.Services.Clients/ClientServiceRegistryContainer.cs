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
    public class ClientServiceRegistryContainer
    {
        public const string RegistryName = "ApplicationServiceRegistry";

        [ServiceRegistryLoader(RegistryName, ProcessModes.Dev)]
        public static ServiceRegistry CreateTestingServicesRegistryForDev()
        {
            CoreClient coreClient = new CoreClient(DefaultConfiguration.GetAppSetting("CoreHostName", "int-heart.bamapps.net"), DefaultConfiguration.GetAppSetting("CorePort", "80").ToInt());
            return GetServiceRegistry(coreClient);
        }

        [ServiceRegistryLoader(RegistryName, ProcessModes.Test)]
        public static ServiceRegistry CreateTestingServicesRegistryForTest()
        {
            CoreClient coreClient = new CoreClient("int-heart.bamapps.net", 80);
            return GetServiceRegistry(coreClient);
        }

        [ServiceRegistryLoader(RegistryName, ProcessModes.Prod)]
        public static ServiceRegistry CreateTestingServicesRegistryForProd()
        {
            CoreClient coreClient = new CoreClient("heart.bamapps.net", 80);
            return GetServiceRegistry(coreClient);
        }

        private static ServiceRegistry GetServiceRegistry(CoreClient coreClient)        
        {
            string contentRoot = DefaultConfiguration.GetAppSetting("ContentRoot", "c:\\bam\\content");
            string organization = DefaultConfiguration.GetAppSetting("Organization", "PUBLIC");
            string applicationName = DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN");
            string databasesPath = Path.Combine(contentRoot, "Databases");
            string workspaceDirectory = Path.Combine(contentRoot, "Workspace");

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
