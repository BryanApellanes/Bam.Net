using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using System.Reflection;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.UserAccounts;
using Bam.Net.Configuration;
using Bam.Net.Web;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.CoreServices.Auth;

namespace Bam.Net.Services.Clients
{
    /// <summary>
    /// A client to the core bam service server.
    /// </summary>
    /// <seealso cref="Bam.Net.Logging.Loggable" />
    /// <seealso cref="Bam.Net.ServiceProxy.Secure.IApiKeyResolver" />
    /// <seealso cref="Bam.Net.ServiceProxy.Secure.IApiKeyProvider" />
    /// <seealso cref="Bam.Net.IApplicationNameProvider" />
    public partial class CoreClient // core
    {
        private void SetDownloadedServiceProxies()
        {
            ApplicationRegistryService = new ApplicationRegistryServiceProxy();
            ConfigurationService = new ConfigurationServiceProxy();
            DiagnosticService = new DiagnosticServiceProxy();
            LoggerService = new SystemLoggerServiceProxy();
            UserRegistryService = new UserRegistryServiceProxy();
            RoleService = new RoleServiceProxy();

            AuthService = new OAuthServiceProxy();
            ServiceRegistryService = new ServiceRegistryServiceProxy();
            SystemLogReaderService = new SystemLogReaderServiceProxy();
            AuthSettingsService = new OAuthSettingsServiceProxy();
            ProxyAssemblyGeneratorService = new ProxyAssemblyGeneratorServiceProxy();
        }
    }
}
