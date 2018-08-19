using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Services.Catalog;
using Bam.Net.Services.Catalog.Data;

namespace Bam.Net.Services
{
    [ServiceRegistryContainer]
    public class ClientServiceRegistryContainer
    {
        public const string RegistryName = "ClientServiceRegistry";
        static ServiceRegistry _clientServiceRegistry;
        static object _clientRegistryLock = new object();

        [ServiceRegistryLoader(RegistryName)]
        public static ServiceRegistry GetServiceRegistry()
        {
            return _clientRegistryLock.DoubleCheckLock(ref _clientServiceRegistry, Create);
        }

        public static ServiceRegistry Create()
        {
            AppConf conf = new AppConf(BamConf.Load(ServiceConfig.ContentRoot), ServiceConfig.ProcessName.Or(RegistryName));
            DaoRepository repo = new DaoRepository(DefaultDataSettingsProvider.Current.GetSysDatabase(nameof(CatalogRepository)), Log.Default);
            repo.AddNamespace(typeof(CatalogItem));
            CatalogRepository catalogRepo = new CatalogRepository(repo, Log.Default);
            ServiceRegistry coreReg = CoreServiceRegistryContainer.Create();

            ServiceRegistry reg = (ServiceRegistry)(new ServiceRegistry())
                .For<ILogger>().Use(Log.Default)
                .For<AppConf>().Use(conf)
                .For<IRepository>().Use(catalogRepo)
                .For<DaoRepository>().Use(repo)
                .For<ICatalogService>().Use<CatalogService>()
                .For<CatalogService>().Use<CatalogService>()
                .For<SystemPaths>().Use(DefaultDataSettingsProvider.GetPaths())
                .For<IDataDirectoryProvider>().Use(DefaultDataSettingsProvider.Current);

            
            reg.CombineWith(coreReg);

            return reg;
        }
    }
}
