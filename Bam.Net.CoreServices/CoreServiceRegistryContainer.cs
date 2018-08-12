using System;
using System.Collections.Generic;
using Bam.Net.Incubation;
using Bam.Net.Messaging;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts;
using System.IO;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.Logging;
using Bam.Net.Data;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.Files;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.CoreServices.ServiceRegistration.Data.Dao.Repository;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Default application registry container for applications running locally.
    /// </summary>
    [ServiceRegistryContainer]
    public static class CoreServiceRegistryContainer
    {
        public const string RegistryName = "CoreServiceRegistry";
        static object _coreIncubatorLock = new object();
        static ServiceRegistry _coreServiceRegistry;

        static Dictionary<ProcessModes, Func<ServiceRegistry>> _factories;
        static CoreServiceRegistryContainer()
        {
            _factories = new Dictionary<ProcessModes, Func<ServiceRegistry>>
            {
                { ProcessModes.Dev, Dev },
                { ProcessModes.Test, Test },
                { ProcessModes.Prod, Prod }
            };
        }

        [ServiceRegistryLoader(RegistryName)]
        public static ServiceRegistry GetServiceRegistry()
        {
            return _coreIncubatorLock.DoubleCheckLock(ref _coreServiceRegistry, _factories[ProcessMode.Current.Mode]);
        }

        static ServiceRegistry _instance;
        static object _instanceLock = new object();
        public static ServiceRegistry Instance
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, Create);
            }
        }

        // place holders for customization if necessary
        public static ServiceRegistry Dev()
        {
            return Create();
        }

        public static ServiceRegistry Test()
        {
            return Create();
        }

        public static ServiceRegistry Prod()
        {
            return Create();
        }
        // --

        public static ServiceRegistry Create()
        {
            DefaultDataSettingsProvider dataSettings = DefaultDataSettingsProvider.Current;
            string databasesPath = dataSettings.GetSysDatabaseDirectory().FullName;
            string userDatabasesPath = Path.Combine(databasesPath, "UserDbs");

            AppConf conf = new AppConf(BamConf.Load(ServiceConfig.ContentRoot), ServiceConfig.ProcessName.Or(RegistryName));
            UserManager userMgr = conf.UserManagerConfig.Create();
            DaoUserResolver userResolver = new DaoUserResolver();
            DaoRoleResolver roleResolver = new DaoRoleResolver();
            SQLiteDatabaseProvider dbProvider = new SQLiteDatabaseProvider(databasesPath, Log.Default);
            ApplicationRegistrationRepository coreRepo = new ApplicationRegistrationRepository();
            dbProvider.SetDatabases(coreRepo);
            dbProvider.SetDatabases(userMgr);
            userMgr.Database.TryEnsureSchema(typeof(UserAccounts.Data.User), Log.Default);
            userResolver.Database = userMgr.Database;
            roleResolver.Database = userMgr.Database;

            ServiceRegistryRepository serviceRegistryRepo = new ServiceRegistryRepository();
            serviceRegistryRepo.Database = dataSettings.GetSysDatabaseFor(serviceRegistryRepo);
            serviceRegistryRepo.EnsureDaoAssemblyAndSchema();

            DaoRoleProvider daoRoleProvider = new DaoRoleProvider(userMgr.Database);
            RoleService coreRoleService = new RoleService(daoRoleProvider, conf);
            AssemblyServiceRepository assSvcRepo = new AssemblyServiceRepository();
            assSvcRepo.Database = dataSettings.GetSysDatabaseFor(assSvcRepo);
            assSvcRepo.EnsureDaoAssemblyAndSchema();

            ConfigurationService configSvc = new ConfigurationService(coreRepo, conf, userDatabasesPath);
            CompositeRepository compositeRepo = new CompositeRepository(coreRepo);
            SystemLoggerService loggerSvc = new SystemLoggerService(conf);
            dbProvider.SetDatabases(loggerSvc);
            loggerSvc.SetLogger();

            ServiceRegistry reg = (ServiceRegistry)(new ServiceRegistry())
                .ForCtor<ConfigurationService>("databaseRoot").Use(userDatabasesPath)
                .ForCtor<ConfigurationService>("conf").Use(conf)
                .ForCtor<ConfigurationService>("coreRepo").Use(coreRepo)
                .For<ILogger>().Use(Log.Default)
                .For<IRepository>().Use(coreRepo)
                .For<DaoRepository>().Use(coreRepo)
                .For<ApplicationRegistrationRepository>().Use(coreRepo)
                .For<AppConf>().Use(conf)
                .For<IDatabaseProvider>().Use(dbProvider)
                .For<IUserManager>().Use(userMgr)
                .For<UserManager>().Use(userMgr)
                .For<IUserResolver>().Use(userResolver)
                .For<DaoUserResolver>().Use(userResolver)
                .For<IRoleResolver>().Use(roleResolver)
                .For<DaoRoleResolver>().Use(roleResolver)
                .For<IRoleProvider>().Use(coreRoleService)
                .For<RoleService>().Use(coreRoleService)
                .For<EmailComposer>().Use(userMgr.EmailComposer)
                .For<IApplicationNameProvider>().Use<ApplicationRegistrationService>()
                .For<ApplicationRegistrationService>().Use<ApplicationRegistrationService>()
                .For<IApiKeyResolver>().Use<ApplicationRegistrationService>()
                .For<ISmtpSettingsProvider>().Use(userMgr)
                .For<UserRegistryService>().Use<UserRegistryService>()
                .For<ConfigurationService>().Use(configSvc)
                .For<IStorableTypesProvider>().Use<NamespaceRepositoryStorableTypesProvider>()
                .For<FileService>().Use<FileService>()
                .For<IFileService>().Use<FileService>()
                .For<AssemblyServiceRepository>().Use(assSvcRepo)
                .For<IAssemblyService>().Use<AssemblyService>()
                .For<ServiceRegistryRepository>().Use(serviceRegistryRepo)
                .For<ServiceRegistryService>().Use<ServiceRegistryService>()
                .For<OAuthService>().Use<OAuthService>()
                .For<ILog>().Use(loggerSvc)
                .For<SystemLoggerService>().Use(loggerSvc)
                .For<DefaultDataSettingsProvider>().Use(DefaultDataSettingsProvider.Current)
                .For<IApplicationNameResolver>().Use<ClientApplicationNameResolver>()
                .For<ClientApplicationNameResolver>().Use<ClientApplicationNameResolver>()
                .For<SmtpSettingsProvider>().Use(DataSettingsSmtpSettingsProvider.Default)
                .For<NotificationService>().Use<NotificationService>()
                .For<ILogReader>().Use<SystemLogReaderService>()
                .For<CredentialManagementService>().Use<CredentialManagementService>();

            reg.For<ServiceRegistry>().Use(reg)
                .For<DiagnosticService>().Use<DiagnosticService>();

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
