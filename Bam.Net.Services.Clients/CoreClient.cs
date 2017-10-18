﻿using System;
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
using Bam.Net.CoreServices.Services;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.CoreServices.ApplicationRegistration.Dao.Repository;
using Bam.Net.UserAccounts;
using Bam.Net.Configuration;
using Bam.Net.Web;
using Bam.Net.CoreServices;

namespace Bam.Net.Services.Clients
{
    public class CoreClient: Loggable, IApiKeyResolver, IApiKeyProvider, IApplicationNameProvider
    {
        internal CoreClient(string organizationName, string applicationName, string workingDirectory = null, ILogger logger = null)
        {
            string hostName = "localhost";
            int port = 9100;
            SetMainProperties(organizationName, applicationName, hostName, port, workingDirectory, logger);
            SetLocalServiceProxies();
            SetApiKeyResolvers();
            SetClientApplicationNameProvider();
            SetLocalProperties(organizationName, applicationName, hostName, port);
            WireInvocationEventHandlers();
        }

        public CoreClient(ILogger logger = null) : this(Organization.Public.Name, DefaultApplicationName, "bamapps.net", 80, null, logger ?? Log.Default)
        { }

        /// <summary>
        /// Instanciate a new CoreClient
        /// </summary>
        /// <param name="organizationName">The name of your organization</param>
        /// <param name="applicationName">The name of your application</param>
        /// <param name="hostName">The host to connect to</param>
        /// <param name="port">The port to connect to</param>
        /// <param name="workingDirectory">The local working directory to place temporary files in</param>
        /// <param name="logger">The logger to use to log activity</param>
        public CoreClient(string organizationName, string applicationName, string hostName, int port, string workingDirectory = null, ILogger logger = null)
        {
            SetMainProperties(organizationName, applicationName, hostName, port, workingDirectory, logger);
            SetDownloadedServiceProxies();
            SetApiKeyResolvers();
            SetClientApplicationNameProvider();
            SetLocalProperties(organizationName, applicationName, hostName, port);
            WireInvocationEventHandlers();
        }
        
        public CoreClient(string applicationName, string hostName, int port, string workingDirectory = null, ILogger logger = null)
            : this(Organization.Public.Name, applicationName, hostName, port, workingDirectory, logger)
        {
        }

        public CoreClient(string organizationName, string applicationName, string hostName, int port, ILogger logger = null) 
            : this(organizationName, applicationName, hostName, port, null, logger)
        { }

        public CoreClient(string hostName, int port, ILogger logger = null) : this(Organization.Public.Name, CoreServices.ApplicationRegistration.Application.Unknown.Name, hostName, port, logger)
        { }


        static CoreClient _local;
        static object _localLock = new object();

        /// <summary>
        /// A CoreClient configured for localhost on port 9100
        /// </summary>
        public static CoreClient Local
        {
            get
            {
                return _localLock.DoubleCheckLock(ref _local, () => new CoreClient(Organization.Public.Name, DefaultApplicationName));
            }
        }

        public static string DefaultApplicationName
        {
            get
            {
                return $"{UserUtil.GetCurrentWindowsUser(true)}:{ProcessDescriptor.Current.FilePath}@{ProcessDescriptor.Current.MachineName}";
            }
        }
        public ProcessDescriptor ProcessDescriptor { get; private set; }

        /// <summary>
        /// The local instance of the ApplicationRegistryRepository
        /// </summary>
        public ApplicationRegistrationRepository LocalCoreRegistryRepository { get; set; }
        
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{OrganizationName}:{ApplicationName} initializING")]
        public event EventHandler Initializing;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "{OrganizationName}:{ApplicationName} initializED")]
        public event EventHandler Initialized;

        [Verbosity(VerbosityLevel.Warning, MessageFormat = "{OrganizationName}:{ApplicationName} initialization failed: {Message}")]
        public event EventHandler InitializationFailed;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "{OrganizationName}:{ApplicationName}: {ApiKeyFilePath} saved")]
        public event EventHandler ApiKeyFileSaved;
        
        public string Message { get; set; } // used by InitializationFailed event
        public string ApplicationName { get; set; }
        public string OrganizationName { get; set; }
        public string WorkspaceDirectory { get; internal set; }
        public string ApiKeyFilePath { get { return Path.Combine(WorkspaceDirectory, HostName, Port.ToString(), $"{GetApplicationName()}.apikey"); } }
        public ILogger Logger { get; set; }
        #region IApiKeyResolver
        public HashAlgorithms HashAlgorithm
        {
            get; set;
        }

        public string CreateKeyToken(string stringToHash)
        {
            ApiKeyInfo keyInfo = GetApiKeyInfo(this);
            return $"{keyInfo.ApiKey}:{stringToHash}".Hash(HashAlgorithm);
        }

        public bool IsValidRequest(ExecutionRequest request)
        {
            Args.ThrowIfNull(request, "request");
            string stringToHash = ApiParameters.GetStringToHash(request);
            string token = request.Context.Request.Headers[Headers.KeyToken];
            bool result = false;
            if (!string.IsNullOrEmpty(token))
            {
                result = IsValidKeyToken(stringToHash, token);
            }
            return result;
        }

        public bool IsValidKeyToken(string stringToHash, string token)
        {
            string checkToken = CreateKeyToken(stringToHash);
            return token.Equals(checkToken);
        }

        public void SetKeyToken(HttpWebRequest request, string stringToHash)
        {
            SetKeyToken(request.Headers, stringToHash);
        }

        /// <summary>
        /// Set the key token header (X-Bam-Keytoken) using the secret (ApiKey)
        /// for the current application
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="stringToHash"></param>
        public void SetKeyToken(NameValueCollection headers, string stringToHash)
        {
            headers[Headers.KeyToken] = CreateKeyToken(stringToHash);
        }
        #endregion
        [Verbosity(VerbosityLevel.Warning, MessageFormat = "ApiKeyFile {ApiKeyFilePath} was not found")]
        public event EventHandler ApiKeyFileNotFound;

        #region IApiKeyProvider

        ApiKeyInfo _apiKeyInfo;
        public ApiKeyInfo GetApiKeyInfo(IApplicationNameProvider nameProvider)
        {
            if (_apiKeyInfo == null)
            {
                if (File.Exists(ApiKeyFilePath))
                {
                    _apiKeyInfo = ApiKeyFilePath.FromJsonFile<ApiKeyInfo>();
                }
                else
                {
                    FireEvent(ApiKeyFileNotFound);
                    _apiKeyInfo = ApplicationRegistryService.GetClientApiKeyInfo();
                    _apiKeyInfo.ApplicationName = nameProvider.GetApplicationName();
                    EnsureApiKeyFileDirectory();
                    _apiKeyInfo.ToJsonFile(ApiKeyFilePath);
                }
            }
            return _apiKeyInfo;
        }

        public string GetApplicationApiKey(string applicationClientId, int index) // index ignored in this implementation //TODO: take into account the index
        {
            ApiKeyInfo key = GetApiKeyInfo(this);
            if (key.ApplicationClientId.Equals(applicationClientId))
            {
                return key.ApiKey;
            }
            throw new NotSupportedException("Specified applicationClientId not supported");
        }

        public string GetApplicationClientId()
        {
            return GetApplicationClientId(this);
        }

        public string GetApplicationClientId(IApplicationNameProvider nameProvider)
        {
            ApiKeyInfo key = GetApiKeyInfo(this);
            if (key.ApplicationName.Equals(nameProvider.GetApplicationName()))
            {
                return key.ApplicationClientId;
            }
            throw new NotSupportedException("Specified applicationClientId not supported");
        }

        public string GetCurrentApiKey()
        {
            ApiKeyInfo key = GetApiKeyInfo(this);
            return key.ApiKey;
        }
        #endregion

        #region IApplicationNameProvider

        public string GetApplicationName()
        {
            string appName = ApplicationName;
            if (string.IsNullOrEmpty(appName))
            {
                Logger.AddEntry("ApplicationName not specified: {0}", LogEventType.Warning, Assembly.GetEntryAssembly().GetFilePath());
            }
            return appName.Or($"{nameof(CoreClient)}.ApplicationName.Unspecified");
        }
        #endregion
        
        public SignUpResponse SignUp(string emailAddress, string password, string userName = null)
        {
            userName = userName ?? emailAddress;
            return UserRegistryService.SignUp(emailAddress, userName, password.Sha1(), true);
        }

        public ApiKeyInfo RegisterApplication()
        {
            RegisterApplicationProcess();
            return ApiKeyFilePath.FromJsonFile<ApiKeyInfo>();
        }

        public LoginResponse Login(string userName, string passHash)
        {
            return UserRegistryService.Login(userName, passHash);
        }
        object _registerLock = new object();
        /// <summary>
        /// Register the current application and ensure that the local ApiKeyInfo is set and 
        /// written to ApiKeyFilePath
        /// </summary>
        /// <returns></returns>
        protected internal bool RegisterApplicationProcess()
        {
            lock (_registerLock)
            {
                if (!IsInitialized)
                {
                    FireEvent(Initializing);
                    CoreServiceResponse response = ApplicationRegistryService.RegisterApplicationProcess(ProcessDescriptor);
                    ApplicationRegistrationResult appRegistrationResult = response.Data.FromJObject<ApplicationRegistrationResult>();
                    if (response.Success)
                    {
                        IsInitialized = true;
                        FireEvent(Initialized);
                        ApiKeyInfo keyInfo = new ApiKeyInfo
                        {
                            ApiKey = appRegistrationResult.ApiKey,
                            ApplicationClientId = appRegistrationResult.ClientId,
                            ApplicationName = GetApplicationName()
                        };
                        EnsureApiKeyFileDirectory();
                        keyInfo.ToJsonFile(ApiKeyFilePath);
                        FireEvent(ApiKeyFileSaved);
                    }
                    else
                    {
                        Message = response.Message;
                        FireEvent(InitializationFailed);
                    }
                }
                return IsInitialized;
            }
        }
        /// <summary>
        /// Register this client machine/process with the remote host
        /// </summary>
        /// <returns></returns>
        public CoreServiceResponse RegisterClient()
        {
            Client client = Client.Of(LocalCoreRegistryRepository, ApplicationName, HostName, Port);            
            CoreServiceResponse registrationResponse = ApplicationRegistryService.RegisterClient(client);
            if (registrationResponse == null || !registrationResponse.Success)
            {
                throw new ClientRegistrationFailedException(registrationResponse);
            }
            return registrationResponse;
        }

        /// <summary>
        /// Connect to the host specified by this client.  This entails
        /// "logging in" the current machine to the core services on the
        /// remote host
        /// </summary>
        /// <returns></returns>
        public CoreServiceResponse Connect()
        {
            Client current = Client.Of(LocalCoreRegistryRepository, ApplicationName, HostName, Port);
            List<CoreServiceResponse> responses = new List<CoreServiceResponse>();
            foreach(ProxyableService svc in ServiceClients)
            {
                responses.Add(svc.ConnectClient(current).CopyAs<CoreServiceResponse>());
            }
            return new CoreServiceResponse { Data = responses, Success = !responses.Where(r => !r.Success).Any() };
        }

        public T GetProxy<T>()
        {
            return ProxyFactory.GetProxy<T>(HostName, Port);
        }
        public bool UseServiceSubdomains
        {
            get
            {
                return ProxyFactory.MungeHostNames;
            }
            set
            {
                ProxyFactory.MungeHostNames = value;
            }
        }
        protected ProxyFactory ProxyFactory { get; set; }
        protected bool IsInitialized { get; set; }

        /// <summary>
        /// The hostname of the server this client
        /// is a client of
        /// </summary>
        public string HostName { get; private set; }
        
        /// <summary>
        /// The port that the server is listening on
        /// </summary>
        public int Port { get; private set; }

        protected internal CoreUserRegistryService UserRegistryService { get; set; }
        protected internal CoreRoleService RoleService { get; set; }
        protected internal CoreOAuthService OAuthService { get; set; }
        protected internal CoreApplicationRegistrationService ApplicationRegistryService { get; set; }
        protected internal CoreConfigurationService ConfigurationService { get; set; }
        protected internal CoreLoggerService LoggerService { get; set; }
        protected internal CoreDiagnosticService DiagnosticService { get; set; }

        /// <summary>
        /// Each of the Core service proxies
        /// </summary>
        protected internal IEnumerable<ProxyableService> ServiceClients
        {
            get
            {
                yield return UserRegistryService;
                yield return ApplicationRegistryService;                
                yield return ConfigurationService;
                yield return LoggerService;
                yield return RoleService;
                yield return DiagnosticService;                
            }
        }

        private void SetLocalProperties(string organizationName, string applicationName, string hostName, int port)
        {
            LocalCoreRegistryRepository = new ApplicationRegistrationRepository()
            {
                Database = new SQLiteDatabase(WorkspaceDirectory, nameof(CoreClient))
            };
            CoreServiceRegistryContainer.GetServiceRegistry().Get<IStorableTypesProvider>().AddTypes(LocalCoreRegistryRepository);
            ProcessDescriptor = ProcessDescriptor.ForApplicationRegistration(LocalCoreRegistryRepository, hostName, port, applicationName, organizationName);
        }

        private void SetMainProperties(string organizationName, string applicationName, string hostName, int port, string workingDirectory, ILogger logger)
        {
            OrganizationName = organizationName;
            ApplicationName = applicationName;
            HostName = hostName;
            Port = port;
            WorkspaceDirectory = workingDirectory ?? DataSettings.Default.GetWorkspaceDirectory(typeof(CoreClient)).FullName;
            HashAlgorithm = HashAlgorithms.SHA256;
            Logger = logger ?? Log.Default;
            ProxyFactory = new ProxyFactory(WorkspaceDirectory, Logger);
        }

        private void SetDownloadedServiceProxies()
        {
            ApplicationRegistryService = ProxyFactory.GetProxy<CoreApplicationRegistrationService>(HostName, Port);
            ConfigurationService = ProxyFactory.GetProxy<CoreConfigurationService>(HostName, Port);
            DiagnosticService = ProxyFactory.GetProxy<CoreDiagnosticService>(HostName, Port);
            LoggerService = ProxyFactory.GetProxy<CoreLoggerService>(HostName, Port);
            UserRegistryService = ProxyFactory.GetProxy<CoreUserRegistryService>(HostName, Port);
            RoleService = ProxyFactory.GetProxy<CoreRoleService>(HostName, Port);
            OAuthService = ProxyFactory.GetProxy<CoreOAuthService>(HostName, Port);
        }

        private void SetLocalServiceProxies()
        {
            ApplicationRegistryService = ProxyFactory.GetProxy<CoreApplicationRegistrationService>();
            ConfigurationService = ProxyFactory.GetProxy<CoreConfigurationService>();
            DiagnosticService = ProxyFactory.GetProxy<CoreDiagnosticService>();
            LoggerService = ProxyFactory.GetProxy<CoreLoggerService>();
            UserRegistryService = ProxyFactory.GetProxy<CoreUserRegistryService>();
            RoleService = ProxyFactory.GetProxy<CoreRoleService>();
            OAuthService = ProxyFactory.GetProxy<CoreOAuthService>();
        }

        private void WireInvocationEventHandlers()
        {
            foreach(ProxyableService service in ServiceClients)
            {
                ServiceProxyClient client = service.Property<ServiceProxyClient>("Client");
                client.InvocationException += (o, a) => InvocationExceptionHandler(o, a);
                client.InvokedMethod += (o, a) => InvocationHandler(o, a);
            }
        }
        public event EventHandler InvocationException;
        public event EventHandler MethodInvoked;
        private void InvocationExceptionHandler(object sender, ServiceProxyInvokeEventArgs args)
        {
            FireEvent(InvocationException, sender, args);
        }

        private void InvocationHandler(object sender, ServiceProxyInvokeEventArgs args)
        {
            FireEvent(MethodInvoked, sender, args);
        }

        private void EnsureApiKeyFileDirectory()
        {
            FileInfo apiKeyFile = new FileInfo(ApiKeyFilePath);
            if (!apiKeyFile.Directory.Exists)
            {
                apiKeyFile.Directory.Create();
            }
        }

        private void SetApiKeyResolvers()
        {
            SetProperty("ApiKeyResolver");
        }

        private void SetClientApplicationNameProvider()
        {
            SetProperty("ClientApplicationNameProvider");
        }

        private void SetProperty(string propertyName)
        {
            ApplicationRegistryService.Property(propertyName, this);
            ConfigurationService.Property(propertyName, this);
            DiagnosticService.Property(propertyName, this);
            LoggerService.Property(propertyName, this);
            UserRegistryService.Property(propertyName, this);
            RoleService.Property(propertyName, this);
        }
    }
}
