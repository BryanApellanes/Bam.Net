using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Incubation;
using Bam.Net.Messaging;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts;
using System.IO;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.Logging;
using Bam.Net.Data.SQLite;
using Bam.Net.Data;
using Bam.Net.Translation.Yandex;
using Bam.Net.Translation;
using Bam.Net.CoreServices.Data.Dao.Repository;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.Files;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.CoreServices.ServiceRegistration.Data.Dao.Repository;
using Bam.Net.CoreServices.ServiceRegistration;
using Bam.Net.CoreServices.OAuth;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Registry for the Core service
    /// provider of all applications
    /// </summary>
    [ServiceRegistryContainer]
    public static class CoreServiceRegistryContainer
    {
        public const string RegistryName = "CoreServiceRegistry";
        static object _coreIncubatorLock = new object();
        static ServiceRegistry _coreIncubator;
        
        [ServiceRegistryLoader(RegistryName)]
        public static ServiceRegistry GetServiceRegistry()
        {
            return _coreIncubatorLock.DoubleCheckLock(ref _coreIncubator, Create);
        }

        public static ServiceRegistry Create()
        {
            string databasesPath = Path.Combine(DefaultConfiguration.GetAppSetting("ContentRoot"), "Databases");
            string userDatabasesPath = Path.Combine(databasesPath, "UserDbs");

            AppConf conf = new AppConf(BamConf.Load(ServiceConfig.ContentRoot), ServiceConfig.ApplicationName.Or(RegistryName));
            UserManager userMgr = conf.UserManagerConfig.Create();
            DaoUserResolver userResolver = new DaoUserResolver();
            DaoRoleResolver roleResolver = new DaoRoleResolver();
            SQLiteDatabaseProvider dbProvider = new SQLiteDatabaseProvider(databasesPath, Log.Default);
            CoreRegistryRepository coreRepo = new CoreRegistryRepository();
            dbProvider.SetDatabases(coreRepo);
            dbProvider.SetDatabases(userMgr);
            userMgr.Database.TryEnsureSchema(typeof(UserAccounts.Data.User), Log.Default);
            userResolver.Database = userMgr.Database;
            roleResolver.Database = userMgr.Database;

            DaoRoleProvider daoRoleProvider = new DaoRoleProvider(userMgr.Database);
            CoreRoleService coreRoleService = new CoreRoleService(daoRoleProvider, conf);
            AssemblyServiceRepository assSvcRepo = new AssemblyServiceRepository();
            assSvcRepo.EnsureDaoAssemblyAndSchema();

            CoreConfigurationService configSvc = new CoreConfigurationService(coreRepo, conf, userDatabasesPath);
            CoreApplicationRegistryServiceConfig config = new CoreApplicationRegistryServiceConfig { DatabaseProvider = dbProvider, WorkspacePath = databasesPath, Logger = Log.Default };
            CompositeRepository compositeRepo = new CompositeRepository(coreRepo, databasesPath);
            ServiceRegistry reg = (ServiceRegistry)(new ServiceRegistry())
                .ForCtor<CoreConfigurationService>("databaseRoot").Use(userDatabasesPath)
                .ForCtor<CoreConfigurationService>("conf").Use(conf)
                .ForCtor<CoreConfigurationService>("coreRepo").Use(coreRepo)
                .For<ILogger>().Use(Log.Default)
                .For<IRepository>().Use(coreRepo)
                .For<DaoRepository>().Use(coreRepo)
                .For<CoreRegistryRepository>().Use(coreRepo)
                .For<AppConf>().Use(conf)
                .For<IDatabaseProvider>().Use(dbProvider)
                .For<IUserManager>().Use(userMgr)
                .For<IUserResolver>().Use(userResolver)
                .For<DaoUserResolver>().Use(userResolver)
                .For<IRoleResolver>().Use(roleResolver)
                .For<DaoRoleResolver>().Use(roleResolver)
                .For<IRoleProvider>().Use(coreRoleService)
                .For<EmailComposer>().Use(userMgr.EmailComposer)
                .For<CoreApplicationRegistryServiceConfig>().Use(config)
                .For<IApplicationNameProvider>().Use<CoreApplicationRegistrationService>()
                .For<CoreApplicationRegistrationService>().Use<CoreApplicationRegistrationService>()
                .For<IApiKeyResolver>().Use<CoreApplicationRegistrationService>()
                .For<ISmtpSettingsProvider>().Use(userMgr)
                .For<CoreUserRegistryService>().Use<CoreUserRegistryService>()
                .For<CoreConfigurationService>().Use(configSvc)
                .For<IStorableTypesProvider>().Use<NamespaceRepositoryStorableTypesProvider>()
                .For<CoreDiagnosticService>().Use<CoreDiagnosticService>()
                .For<CoreFileService>().Use<CoreFileService>()
                .For<IFileService>().Use<CoreFileService>()
                .For<AssemblyServiceRepository>().Use(assSvcRepo)
                .For<IAssemblyService>().Use<AssemblyService>()
                .For<ServiceRegistryRepository>().Use<ServiceRegistryRepository>()
                .For<CoreServiceRegistrationService>().Use<CoreServiceRegistrationService>()
                .For<CoreOAuthService>().Use<CoreOAuthService>();

            reg.SetProperties(userMgr);
            userMgr.ServiceProvider = reg;

            reg.For<CompositeRepository>().Use(() =>
            {
                compositeRepo.AddTypes(reg.Get<IStorableTypesProvider>().GetTypes());
                return compositeRepo;
            });

            ServiceProxySystem.UserResolvers.Clear();
            ServiceProxySystem.RoleResolvers.Clear();
            ServiceProxySystem.UserResolvers.AddResolver(userResolver);
            ServiceProxySystem.RoleResolvers.AddResolver(roleResolver);
            reg.Name = RegistryName;
            return reg;
        }
    }
}
