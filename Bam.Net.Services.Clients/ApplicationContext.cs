using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Messaging;
using Bam.Net.UserAccounts;

namespace Bam.Net.Services.Clients
{
    public class ApplicationContext
    {
        public ApplicationContext(CoreClient coreClient, IOrganizationNameProvider organizationNameProvider, IApplicationNameProvider applicationNameProvider, IConfigurationService configurationService, IDatabaseProvider databaseProvider, IDataDirectoryProvider dataDirectoryProvider, ILoggerProvider loggerProvider)
        {
            CoreClient = coreClient;
            OrganizationNameProvider = organizationNameProvider;
            ConfigurationService = configurationService;
            DataDirectoryProvider = dataDirectoryProvider;
            DatabaseProvider = databaseProvider;
            ApplicationNameProvider = applicationNameProvider;
            LoggerProvider = loggerProvider;
            CoreClient = coreClient;
        }

        protected ApplicationContext(string coreHostName, int corePort = 80)
            : this(new CoreClient(DefaultConfigurationOrganizationNameProvider.Instance.GetOrganizationName(), DefaultConfigurationApplicationNameProvider.Instance.GetApplicationName(), coreHostName, corePort, DefaultConfigurationLoggerProvider.Instance.GetLogger()),
                  DefaultConfigurationOrganizationNameProvider.Instance, DefaultConfigurationApplicationNameProvider.Instance, DefaultConfigurationService.Instance, DefaultDataSettingsProvider.Instance, DefaultDataSettingsProvider.Instance, DefaultConfigurationLoggerProvider.Instance)
        {               
        }

        public static ApplicationContext Create(ServiceRegistry serviceRegistry, string coreHostName = "core.bamapps.net", int corePort = 80)
        {
            IOrganizationNameProvider organizationNameProvider = serviceRegistry.Get<IOrganizationNameProvider>();
            IApplicationNameProvider applicationNameProvider = serviceRegistry.Get<IApplicationNameProvider>();
            IConfigurationService configurationService = serviceRegistry.Get<IConfigurationService>();
            IDatabaseProvider databaseProvider = serviceRegistry.Get<IDatabaseProvider>();
            IDataDirectoryProvider dataDirectoryProvider = serviceRegistry.Get<IDataDirectoryProvider>();
            ILoggerProvider loggerProvider = serviceRegistry.Get<ILoggerProvider>();
            CoreClient client = new CoreClient(
                organizationNameProvider.GetOrganizationName(),
                applicationNameProvider.GetApplicationName(),
                coreHostName,
                corePort,
                loggerProvider.GetLogger());
            return new ApplicationContext(client, organizationNameProvider, applicationNameProvider, configurationService, databaseProvider, dataDirectoryProvider, loggerProvider);
        }

        public CoreClient CoreClient
        {
            get;set;
        }

        public IOrganizationNameProvider OrganizationNameProvider { get; set; }
        public IApplicationNameProvider ApplicationNameProvider { get; set; }
        public IConfigurationService ConfigurationService { get; set; }
        public IDataDirectoryProvider DataDirectoryProvider { get; set; }
        public IDatabaseProvider DatabaseProvider { get; set; }
        public ILoggerProvider LoggerProvider { get; set; }

        public IUserManager UserManager
        {
            get
            {
                return CoreClient.UserRegistryService;
            }
        }

        public ISmtpSettingsProvider SmtpSettingsProvider
        {
            get
            {
                return CoreClient.UserRegistryService;
            }
        }

        public ApplicationSettings GetSettings()
        {
            return new ApplicationSettings
            {
                OrganizationName = OrganizationNameProvider.GetOrganizationName(),
                ApplicationName = ApplicationNameProvider.GetApplicationName(),
                Paths = SystemPaths.Get(DataDirectoryProvider),
                Configuration = ConfigurationService.GetApplicationConfiguration(ApplicationNameProvider.GetApplicationName())
            };
        }
    }
}
