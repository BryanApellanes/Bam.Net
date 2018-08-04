using Bam.Net.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Data.SQLite;
using Bam.Net.Server;
using Bam.Net.Services.Clients;
using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.UserAccounts;

namespace Bam.Net.Application
{
    [Serializable]
    [ServiceRegistryContainer]
    public class ContentServicesRegistryContainer
    {
        public const string Name = "ContentServicesRegistry";
        static object _registryLock = new object();

        [ServiceRegistryLoader(Name, ProcessModes.Dev)]
        public static ServiceRegistry CreateTestingServicesRegistryForDev()
        {
            CoreClient coreClient = new CoreClient(DefaultConfiguration.GetAppSetting("CoreHostName", "localhost"), DefaultConfiguration.GetAppSetting("CorePort", "9101").ToInt());
            return GetServiceRegistry(coreClient);
        }

        [ServiceRegistryLoader(Name, ProcessModes.Test)]
        public static ServiceRegistry CreateTestingServicesRegistryForTest()
        {
            CoreClient coreClient = new CoreClient("int-heart.bamapps.net", 80);
            return GetServiceRegistry(coreClient);
        }

        [ServiceRegistryLoader(Name, ProcessModes.Prod)]
        public static ServiceRegistry CreateTestingServicesRegistryForProd()
        {
            CoreClient coreClient = new CoreClient("heart.bamapps.net", 80);
            return GetServiceRegistry(coreClient);
        }

        private static ServiceRegistry GetServiceRegistry(CoreClient coreClient)
        {
            SQLiteDatabase loggerDb = DefaultDatabaseProvider.Current.GetSysDatabase($"{Name}_DaoLogger2");
            ILogger logger = new DaoLogger2(loggerDb);

            return (ServiceRegistry)(new ServiceRegistry())
                .For<IUserManager>().Use(coreClient.UserRegistryService)
                .For<DefaultDatabaseProvider>().Use(DefaultDatabaseProvider.Current)
                .For<ILogger>().Use(logger)
                .For<IDatabaseProvider>().Use<DataSettingsDatabaseProvider>();                
        }
    }
}
