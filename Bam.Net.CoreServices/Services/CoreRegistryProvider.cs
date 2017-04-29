using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Incubation;
using Bam.Net.Messaging;
using Bam.Net.Server.Tvg;
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

namespace Bam.Net.CoreServices.Services
{
    [CoreRegistryContainer]
    public static class CoreRegistryProvider
    {
        static object _coreIncubatorLock = new object();
        static CoreRegistry _coreIncubator;
        
        [CoreRegistryProvider]
        public static CoreRegistry GetCoreRegistry()
        {
            return _coreIncubatorLock.DoubleCheckLock(ref _coreIncubator, Create);
        }

        public static CoreRegistry Create()
        {
            string databasesPath = Path.Combine(DefaultConfiguration.GetAppSetting("ContentRoot"), "Databases");
            string userDatabasesPath = Path.Combine(databasesPath, "UserDbs");
            string yandexVaultPath = Path.Combine(databasesPath, "YandexApiVault");
            YandexApiKeyVaultInfo yandexVault = new YandexApiKeyVaultInfo(yandexVaultPath);
            Database translationDatabase = new SQLiteDatabase(databasesPath, "Translations");
            YandexTranslationProvider translationProvider = new YandexTranslationProvider(yandexVault.Load(), translationDatabase, translationDatabase);
            translationProvider.EnsureLanguages();

            AppConf conf = new AppConf(BamConf.Load(ServiceConfig.ContentRoot), ServiceConfig.ApplicationName.Or("CoreRegistryService"));
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

            CoreConfigurationService configSvc = new CoreConfigurationService(coreRepo, conf, userDatabasesPath);
            CoreApplicationRegistryServiceConfig config = new CoreApplicationRegistryServiceConfig { DatabaseProvider = dbProvider, WorkspacePath = databasesPath, Logger = Log.Default };
            CompositeRepository compositeRepo = new CompositeRepository(coreRepo, databasesPath);
            CoreRegistry reg = (CoreRegistry)(new CoreRegistry())
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
                .For<EmailComposer>().Use(userMgr.EmailComposer)
                .For<CoreApplicationRegistryServiceConfig>().Use(config)
                .For<IApplicationNameProvider>().Use<CoreApplicationRegistryService>()
                .For<CoreApplicationRegistryService>().Use<CoreApplicationRegistryService>()
                .For<IApiKeyResolver>().Use<CoreApplicationRegistryService>()
                .For<ISmtpSettingsProvider>().Use(userMgr)
                .For<CoreUserRegistryService>().Use<CoreUserRegistryService>()
                .For<CoreConfigurationService>().Use(configSvc)
                .For<IDetectLanguage>().Use(translationProvider)
                .For<ITranslationProvider>().Use(translationProvider)
                .For<IStorableTypesProvider>().Use<NamespaceRepositoryStorableTypesProvider>()
                .For<CoreTranslationService>().Use<CoreTranslationService>()
                .For<CoreDiagnosticService>().Use<CoreDiagnosticService>();

            reg.SetProperties(userMgr);

            reg.For<CompositeRepository>().Use(() =>
            {
                compositeRepo.AddTypes(reg.Get<IStorableTypesProvider>().GetTypes());
                return compositeRepo;
            });

            return reg;
        }
    }
}
