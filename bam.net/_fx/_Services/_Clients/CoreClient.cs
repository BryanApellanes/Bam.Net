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
using Bam.Net.CoreServices.OAuth;

namespace Bam.Net.Services.Clients
{
    /// <summary>
    /// A client to the core bam service server.
    /// </summary>
    /// <seealso cref="Bam.Net.Logging.Loggable" />
    /// <seealso cref="Bam.Net.ServiceProxy.Secure.IApiKeyResolver" />
    /// <seealso cref="Bam.Net.ServiceProxy.Secure.IApiKeyProvider" />
    /// <seealso cref="Bam.Net.IApplicationNameProvider" />
    public partial class CoreClient // fx
    {
        private void SetDownloadedServiceProxies()
        {
            ApplicationRegistryService = ProxyFactory.GetProxy<ApplicationRegistrationService>(HostName, Port, Logger);
            ConfigurationService = ProxyFactory.GetProxy<ConfigurationService>(HostName, Port, Logger);
            DiagnosticService = ProxyFactory.GetProxy<DiagnosticService>(HostName, Port, Logger);
            LoggerService = ProxyFactory.GetProxy<SystemLoggerService>(HostName, Port, Logger);
            UserRegistryService = ProxyFactory.GetProxy<UserRegistryService>(HostName, Port, Logger);
            RoleService = ProxyFactory.GetProxy<RoleService>(HostName, Port, Logger);
            OAuthService = ProxyFactory.GetProxy<OAuthService>(HostName, Port, Logger);
            ServiceRegistryService = ProxyFactory.GetProxy<ServiceRegistryService>(HostName, Port, Logger);
            SystemLogReaderService = ProxyFactory.GetProxy<SystemLogReaderService>(HostName, Port, Logger);
            OAuthSettingsService = ProxyFactory.GetProxy<OAuthSettingsService>(HostName, Port, Logger);
            ProxyAssemblyGeneratorService = ProxyFactory.GetProxy<ProxyAssemblyGeneratorService>(HostName, Port, Logger);
        }
    }
}
