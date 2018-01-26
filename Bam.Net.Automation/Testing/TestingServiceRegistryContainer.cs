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

namespace Bam.Net.Automation.Testing
{
    [Serializable]
    [ServiceRegistryContainer]    
    public class TestingServiceRegistryContainer
    {
        static object _registryLock = new object();

        [ServiceRegistryLoader("TestingServicesRegistry", ProcessModes.Dev, ProcessModes.Test)]
        public static ServiceRegistry CreateTestingServicesRegistryForDev()
        {
            CoreClient coreClient = new CoreClient(DefaultConfiguration.GetAppSetting("CoreHostName", "localhost"), DefaultConfiguration.GetAppSetting("CorePort", "9101").ToInt());
            return GetServiceRegistry(coreClient);
        }

        [ServiceRegistryLoader("TestingServicesRegistry", ProcessModes.Prod)]
        public static ServiceRegistry CreateTestingServicesRegistryForProd()
        {
            CoreClient coreClient = new CoreClient("heart.bamapps.net", 80);
            return GetServiceRegistry(coreClient);
        }

        private static ServiceRegistry GetServiceRegistry(CoreClient coreClient)
        {
            SQLiteDatabase databaseProviderLoggerDb = DataSettings.Current.GetSysDatabase("TestServicesRegistry_DaoLogger2");
            ILogger logger = new DaoLogger2(databaseProviderLoggerDb);
            TestReportService testReportService = new TestReportService(new DataSettingsDatabaseProvider(DataSettings.Current, logger));
            NotificationService notificationService = new NotificationService(DataSettings.Current, logger)
            {
                UserManager = coreClient.UserRegistryService
            };
            return (ServiceRegistry)(new ServiceRegistry())
                .For<AppConf>().Use(new AppConf("TestingServiceRegistry"))
                .For<SystemLoggerService>().Use<SystemLoggerService>()
                .For<TestReportService>().Use(testReportService)
                .For<NotificationService>().Use(notificationService);
        }
    }
}
