using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using Bam.Net.UserAccounts;
using NSubstitute;

namespace Bam.Net.CoreServices.Tests
{
    using System.IO;
    using Bam.Net.CoreServices.ApplicationRegistration.Data;
    using Net.Data.SQLite;
    using Server;
    using ServiceProxySecure = ServiceProxy.Secure;
    using Bam.Net.Testing.Unit;
    using System.Collections.Specialized;
    using Bam.Net.Web;
    using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;
    using Bam.Net.CoreServices.Configuration;

    [Serializable]
    public class ConfigurationTests : CommandLineTestInterface
    {
        [UnitTest("Config: can set and get app config")]
        public void CanSetAndGetCommonAppConfig()
        {
            string appName = $"{nameof(CanSetAndGetCommonAppConfig)}_TestAppName";
            string configurationName = $"{nameof(CanSetAndGetCommonAppConfig)}_TestConfigName";
            ConfigurationService configSvc = GetTestCoreConfigurationService(appName);

            configSvc.SetApplicationConfiguration(new Dictionary<string, string>
            {
                {"key1", "value1" }
            }, appName);

            configSvc.SetApplicationConfiguration(new Dictionary<string, string>
            {
                {"key1", "value1" },
                {"key2", "value2" }
            }, appName);

            Dictionary<string, string> config = configSvc.GetApplicationConfiguration(appName);
            Expect.AreEqual(2, config.Keys.Count);
            Expect.AreEqual(2, config.Values.Count);
            Expect.AreEqual("value1", config["key1"]);
            Expect.AreEqual("value2", config["key2"]);
        }

        [UnitTest("Config: can set and get machine config")]
        public void CanSetAndGetCommonMachineConfig()
        {
            string machineName = $"{nameof(CanSetAndGetCommonMachineConfig)}_TestMachineName";
            string configurationName = $"{nameof(CanSetAndGetCommonMachineConfig)}_TestConfigName";
            ConfigurationService configSvc = GetTestCoreConfigurationService(machineName);

            configSvc.SetMachineConfiguration(machineName, new Dictionary<string, string>
            {
                {"key1", "value1" }
            });

            configSvc.SetMachineConfiguration(machineName, new Dictionary<string, string>
            {
                {"key1", "value1" },
                {"key2", "value2" }
            });

            Dictionary<string, string> config = configSvc.GetMachineConfiguration(machineName);
            Expect.AreEqual(2, config.Keys.Count);
            Expect.AreEqual(2, config.Values.Count);
            Expect.AreEqual("value1", config["key1"]);
            Expect.AreEqual("value2", config["key2"]);
        }

        [UnitTest("Config: can set and get common configuration")]
        public void CanSetAndGetCommonConfiguration()
        {
            string appName = $"{nameof(CanSetAndGetCommonConfiguration)}_TestAppName";
            string configurationName = $"{nameof(CanSetAndGetCommonConfiguration)}_TestConfigName";
            ConfigurationService configSvc = GetTestCoreConfigurationService(nameof(CanSetAndGetCommonConfiguration));

            configSvc.SetCommonConfiguration(new Dictionary<string, string>
            {
                {"key1", "value1" },
                {"key2", "value2" }
            });

            Dictionary<string, string> config = configSvc.GetCommonConfiguration();
            Expect.AreEqual(2, config.Keys.Count);
            Expect.AreEqual(2, config.Values.Count);
            Expect.AreEqual("value1", config["key1"]);
            Expect.AreEqual("value2", config["key2"]);
        }

        [UnitTest("Config: application setting overrides machine")]
        public void ApplicationSettingOverridesMachine()
        {
            string appName = $"{nameof(ApplicationSettingOverridesMachine)}_TestAppName";
            string configurationName = $"{nameof(ApplicationSettingOverridesMachine)}_TestConfigName";
            string expectedValue = "ApplicationValue";
            string machineName = Machine.Current.Name;
            ConfigurationService configSvc = GetTestCoreConfigurationService(appName);
            configSvc.SetMachineConfiguration(machineName, new Dictionary<string, string>
            {
                {"key1", "MachineValue" }
            }, configurationName);
            configSvc.SetApplicationConfiguration(new Dictionary<string, string>
            {
                {"key1", expectedValue },
                {"key2", "value2" }
            }, appName, configurationName);

            Dictionary<string, string> config = configSvc.GetConfiguration(appName, machineName, configurationName).ToDictionary();
            Expect.AreEqual(expectedValue, config["key1"]);
            Expect.AreEqual("value2", config["key2"]);
        }

        [UnitTest("Config: aggregates settings")]
        public void ConfigurationAggregatesCommonMachineAndApplication()
        {
            string appName = $"{nameof(ConfigurationAggregatesCommonMachineAndApplication)}_TestAppName";
            string machineName = $"{nameof(ConfigurationAggregatesCommonMachineAndApplication)}_TestMachineName";
            ConfigurationService configSvc = GetTestCoreConfigurationService(appName);
            configSvc.SetCommonConfiguration(new Dictionary<string, string>
            {
                {"CommonKey1", "CommonValue1" },
                {"CommonKey2", "CommonValue2" },
                {"CommonOverride", "BAD-Comomon" },
                {"CommonOverride2", "BAD-Common" }
            });
            configSvc.SetMachineConfiguration(machineName, new Dictionary<string, string>
            {
                {"MachineKey1", "MachineValue1" },
                {"MachineKey2", "MachineValue2" },
                {"MachineOverride", "BAD-Machine" },
                {"CommonOverride2", "GOOD-Machine" }
            });
            configSvc.SetApplicationConfiguration(new Dictionary<string, string>
            {
                {"ApplicationKey1", "ApplicationValue1" },
                {"ApplicationKey2", "ApplicationValue2" },
                {"CommonOverride", "GOOD-AppOverrideCommon" },
                {"MachineOverride", "GOOD-AppOverrideMachine" }
            }, appName);
            ApplicationConfiguration appConfig = configSvc.GetConfiguration(appName, machineName);
            AssertExpectations(appConfig);
        }

        private static void AssertExpectations(ApplicationConfiguration appConfig)
        {
            Expect.AreEqual(SettingSource.ApplicationSetting, appConfig["CommonOverride"].SettingSource);
            Expect.AreEqual(SettingSource.MachineSetting, appConfig["CommonOverride2"].SettingSource);
            Expect.AreEqual(SettingSource.ApplicationSetting, appConfig["MachineOverride"].SettingSource);
            Expect.AreEqual("GOOD-AppOverrideCommon", appConfig["CommonOverride"].Value);
            Expect.AreEqual("GOOD-AppOverrideMachine", appConfig["MachineOverride"].Value);

            Expect.AreEqual(SettingSource.CommonSetting, appConfig["CommonKey1"].SettingSource);
            Expect.AreEqual(SettingSource.CommonSetting, appConfig["CommonKey2"].SettingSource);
            Expect.AreEqual("CommonValue1", appConfig["CommonKey1"].Value);
            Expect.AreEqual("CommonValue2", appConfig["CommonKey2"].Value);

            Expect.AreEqual(SettingSource.MachineSetting, appConfig["MachineKey1"].SettingSource);
            Expect.AreEqual(SettingSource.MachineSetting, appConfig["MachineKey2"].SettingSource);
            Expect.AreEqual("MachineValue1", appConfig["MachineKey1"].Value);
            Expect.AreEqual("MachineValue2", appConfig["MachineKey2"].Value);

            Expect.AreEqual(SettingSource.ApplicationSetting, appConfig["ApplicationKey1"].SettingSource);
            Expect.AreEqual(SettingSource.ApplicationSetting, appConfig["ApplicationKey2"].SettingSource);
            Expect.AreEqual("ApplicationValue1", appConfig["ApplicationKey1"].Value);
            Expect.AreEqual("ApplicationValue2", appConfig["ApplicationKey2"].Value);

            Dictionary<string, string> config = appConfig.ToDictionary();
            Expect.AreEqual("CommonValue1", config["CommonKey1"]);
            Expect.AreEqual("CommonValue2", config["CommonKey2"]);
            Expect.AreEqual("MachineValue1", config["MachineKey1"]);
            Expect.AreEqual("MachineValue2", config["MachineKey2"]);
            Expect.AreEqual("ApplicationValue1", config["ApplicationKey1"]);
            Expect.AreEqual("ApplicationValue2", config["ApplicationKey2"]);
        }

        private ConfigurationService GetTestCoreConfigurationService(string testName)
        {
            string appName = $"{testName}_TestAppName";
            string userDbPath = $".\\{testName}_Test";
            string configurationName = $"{testName}_TestConfigName";
            string testUserName = $"{testName}_TestUserName";
            Database userDb = new SQLiteDatabase(testName);
            ApplicationRegistrationRepository coreRepo = new ApplicationRegistrationRepository();
            coreRepo.Database = new SQLiteDatabase($"{testName}_coredb");
            userDb.TryEnsureSchema<UserAccounts.Data.User>();
            Db.For<UserAccounts.Data.User>(userDb);

            ConfigurationService configSvc = new ConfigurationService(coreRepo, new Server.AppConf(), userDbPath);
            configSvc.DaoRepository = coreRepo;
            IApplicationNameProvider appNameProvider = Substitute.For<IApplicationNameProvider>();
            appNameProvider.GetApplicationName().Returns(appName);
            configSvc.ApplicationNameProvider = appNameProvider;

            IUserManager userMgr = Substitute.For<IUserManager>();
            UserAccounts.Data.User testUser = new UserAccounts.Data.User()
            {
                UserName = testUserName
            };
            userMgr.GetUser(Arg.Any<IHttpContext>()).Returns(testUser);
            configSvc.UserManager = userMgr;
            IHttpContext ctx = Substitute.For<IHttpContext>();
            ctx.Request = Substitute.For<IRequest>();
            NameValueCollection headers = new NameValueCollection
            {
                [CustomHeaders.ApplicationName] = testName
            };
            ctx.Request.Headers.Returns(headers);
            ctx.Request.Cookies.Returns(new System.Net.CookieCollection());
            ctx.Response = Substitute.For<IResponse>();
            ctx.Response.Cookies.Returns(new System.Net.CookieCollection());
            configSvc.HttpContext = ctx;
            return configSvc;
        }
    }
}
