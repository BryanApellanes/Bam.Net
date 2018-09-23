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
    public class ViewServicesRegistryContainer
    {
        public const string Name = "ViewServicesRegistry";
        static object _registryLock = new object();

        [ServiceRegistryLoader(Name, ProcessModes.Dev)]
        public static ServiceRegistry CreateViewServicesRegistryForDev()
        {
            CoreClient coreClient = new CoreClient(DefaultConfiguration.GetAppSetting("CoreHostName", "int-heart.bamapps.net"), DefaultConfiguration.GetAppSetting("CorePort", "80").ToInt());
            return GetServiceRegistry(coreClient);
        }

        [ServiceRegistryLoader(Name, ProcessModes.Test)]
        public static ServiceRegistry CreateViewServicesRegistryForTest()
        {
            CoreClient coreClient = new CoreClient("int-heart.bamapps.net", 80);
            return GetServiceRegistry(coreClient);
        }

        [ServiceRegistryLoader(Name, ProcessModes.Prod)]
        public static ServiceRegistry CreateViewServicesRegistryForProd()
        {
            CoreClient coreClient = new CoreClient("heart.bamapps.net", 80);
            return GetServiceRegistry(coreClient);
        }

        private static ServiceRegistry GetServiceRegistry(CoreClient coreClient)
        {
            SQLiteDatabase loggerDb = DefaultDataDirectoryProvider.Current.GetSysDatabase($"{Name}_DaoLogger2");
            ILogger logger = new DaoLogger2(loggerDb);

            return (ServiceRegistry)(new ServiceRegistry())
                .For<IUserManager>().Use(coreClient.UserRegistryService)
                .For<DefaultDataDirectoryProvider>().Use(DefaultDataDirectoryProvider.Current)
                .For<ILogger>().Use(logger)
                .For<IDatabaseProvider>().Use<DefaultDataDirectoryProvider>();                
        }
    }
}
