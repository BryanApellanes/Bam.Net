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
using Bam.Net.Messaging;

namespace Bam.Net.Automation.Testing
{
    [Serializable]
    [ServiceRegistryContainer]    
    public class TestingServicesRegistryContainer
    {
        public const string RegistryName = "TestingServicesRegistry";
        static object _registryLock = new object();

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
            SQLiteDatabase loggerDb = DefaultDataDirectoryProvider.Current.GetSysDatabase("TestServicesRegistry_DaoLogger2");
            ILogger logger = new DaoLogger2(loggerDb);
            IDatabaseProvider dbProvider = DefaultDataDirectoryProvider.Current;
            coreClient.UserRegistryService.DatabaseProvider = dbProvider;
            coreClient.UserRegistryService.ApplicationNameProvider = new DefaultConfigurationApplicationNameProvider();
            AppConf conf = new AppConf(BamConf.Load(ServiceConfig.ContentRoot), ServiceConfig.ProcessName.Or(RegistryName));
            SystemLoggerService loggerSvc = new SystemLoggerService(conf);
            dbProvider.SetDatabases(loggerSvc);
            loggerSvc.SetLogger();

            return (ServiceRegistry)(new ServiceRegistry())
                .For<IDatabaseProvider>().Use(dbProvider)
                .For<IUserManager>().Use(coreClient.UserRegistryService)
                .For<DefaultDataDirectoryProvider>().Use(DefaultDataDirectoryProvider.Current)
                .For<ILogger>().Use(logger)
                .For<IDaoLogger>().Use(logger)
                .For<AppConf>().Use(conf)
                .For<SystemLoggerService>().Use(loggerSvc)
                .For<SystemLogReaderService>().Use<SystemLogReaderService>()
                .For<TestReportService>().Use<TestReportService>()
                .For<SmtpSettingsProvider>().Use(DataSettingsSmtpSettingsProvider.Default)
                .For<NotificationService>().Use<NotificationService>();
        }
    }
}
