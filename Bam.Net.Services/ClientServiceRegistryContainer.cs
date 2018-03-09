using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Incubation;
using Bam.Net.Server;
using Bam.Net.Services.Catalog;

namespace Bam.Net.Services
{
    [ServiceRegistryContainer]
    public class ClientServiceRegistryContainer
    {
        public const string RegistryName = "ClientServiceRegistry";
        static ServiceRegistry _coreServiceRegistry;
        static object _coreIncubatorLock = new object();

        [ServiceRegistryLoader(RegistryName)]
        public static ServiceRegistry GetServiceRegistry()
        {
            return _coreIncubatorLock.DoubleCheckLock(ref _coreServiceRegistry, Create);
        }

        public static ServiceRegistry Create()
        {
            AppConf conf = new AppConf(BamConf.Load(ServiceConfig.ContentRoot), ServiceConfig.ProcessName.Or(RegistryName));
            ServiceRegistry reg = (ServiceRegistry)(new ServiceRegistry())
                .For<AppConf>().Use(conf)
                .For<DaoRepository>().Use<CatalogRepository>()
                .For<ICatalogService>().Use<CatalogService>()
                .For<CatalogService>().Use<CatalogService>();

                return reg;
        }
    }
}
