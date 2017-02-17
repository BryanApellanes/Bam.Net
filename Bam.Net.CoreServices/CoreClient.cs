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
using Bam.Net.CoreServices.Services;
using Bam.Net.CoreServices.Data;
using Bam.Net.CoreServices.Data.Daos.Repository;
using Bam.Net.UserAccounts;

namespace Bam.Net.CoreServices
{
    public class CoreClient: Loggable, IApiKeyResolver, IApiKeyProvider, IApplicationNameProvider
    {
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
            ApplicationName = applicationName;
            OrganizationName = organizationName;
            HostName = hostName;
            Port = port;
            WorkingDirectory = workingDirectory ?? $".\\{nameof(CoreClient)}";
            HashAlgorithm = HashAlgorithms.SHA1;

            ProxyFactory factory = new ProxyFactory(WorkingDirectory, logger);
            ApplicationRegistryService = factory.GetProxy<CoreApplicationRegistryService>(HostName, Port);
            ConfigurationService = factory.GetProxy<CoreConfigurationService>(HostName, Port);
            DiagnosticService = factory.GetProxy<CoreDiagnosticService>(HostName, Port);
            LoggerService = factory.GetProxy<CoreLoggerService>(HostName, Port);
            TranslationService = factory.GetProxy<CoreTranslationService>(HostName, Port);
            UserRegistryService = factory.GetProxy<CoreUserRegistryService>(HostName, Port);

            SetApiKeyResolvers();
            SetClientApplicationNameProvider();

            LocalCoreRegistryRepository = new CoreRegistryRepository();
            LocalCoreRegistryRepository.Database = new SQLiteDatabase(WorkingDirectory, nameof(CoreClient));
            CoreRegistry.GetGlooRegistry().Get<IStorableTypesProvider>().AddTypes(LocalCoreRegistryRepository);

            ProcessDescriptor = ProcessDescriptor.ForApplicationRegistration(LocalCoreRegistryRepository, hostName, port, applicationName, organizationName);
        }

        public CoreClient(string organizationName, string applicationName, string hostName, int port, ILogger logger = null) 
            : this(organizationName, applicationName, hostName, port, null, logger)
        { }

        public ProcessDescriptor ProcessDescriptor { get; }
        public CoreRegistryRepository LocalCoreRegistryRepository { get; set; }
        
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{OrganizationName}:{ApplicationName} initializING")]
        public event EventHandler Initializing;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "{OrganizationName}:{ApplicationName} initializED")]
        public event EventHandler Initialized;

        [Verbosity(VerbosityLevel.Warning, MessageFormat = "{OrganizationName}:{ApplicationName} initialization failed: {Message}")]
        public event EventHandler InitializationFailed;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "{OrganizationName}:{ApplicationName}: {ApiKeyFilePath} saved")]
        public event EventHandler ApiKeyFileSaved;

        object _initLock = new object();
        protected internal bool Initialize()
        {
            lock (_initLock)
            {
                if (!IsInitialized)
                {
                    FireEvent(Initializing);
                    ServiceResponse response = ApplicationRegistryService.RegisterApplication(ProcessDescriptor);
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
        public string Message { get; set; } // used by InitializationFailed event
        public string ApplicationName { get; set; }
        public string OrganizationName { get; set; }
        public string WorkingDirectory { get; }
        public string ApiKeyFilePath { get { return Path.Combine(WorkingDirectory, HostName, Port.ToString(), $"{GetApplicationName()}.apikey"); } }
        public ILogger Logger { get; set; }
        #region IApiKeyResolver
        public HashAlgorithms HashAlgorithm
        {
            get; set;
        }

        public string CreateToken(string stringToHash)
        {
            ApiKeyInfo keyInfo = GetApiKeyInfo(this);
            return $"{keyInfo.ApiKey}:{stringToHash}".Hash(HashAlgorithm);
        }

        public bool IsValidRequest(ExecutionRequest request)
        {
            Args.ThrowIfNull(request, "request");
            string stringToHash = ApiParameters.GetStringToHash(request);
            string token = request.Context.Request.Headers[ApiParameters.KeyTokenName];
            bool result = false;
            if (!string.IsNullOrEmpty(token))
            {
                result = IsValidToken(stringToHash, token);
            }
            return result;
        }

        public bool IsValidToken(string stringToHash, string token)
        {
            string checkToken = CreateToken(stringToHash);
            return token.Equals(checkToken);
        }

        public void SetToken(HttpWebRequest request, string stringToHash)
        {
            SetToken(request.Headers, stringToHash);
        }

        public void SetToken(NameValueCollection headers, string stringToHash)
        {
            headers[ApiParameters.KeyTokenName] = CreateToken(stringToHash);
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

        public string GetApplicationApiKey(string applicationClientId, int index) // index ignored in this implementation
        {
            ApiKeyInfo key = GetApiKeyInfo(this);
            if (key.ApplicationClientId.Equals(applicationClientId))
            {
                return key.ApiKey;
            }
            throw new NotSupportedException("Specified applicationClientId not supported");
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
                Logger.AddEntry("ApplicatoinName not specified: {0}", LogEventType.Warning, Assembly.GetExecutingAssembly().GetFilePath());
            }
            return appName.Or($"{nameof(CoreClient)}.ApplicationName.Unspecified");
        }
        #endregion
        
        public LoginResponse Login(string userName, string passHash)
        {
            return UserRegistryService.Login(userName, passHash);
        }

        public ServiceResponse Register()
        {
            Machine current = Machine.ClientOf(LocalCoreRegistryRepository, HostName, Port);
            current.ServerHost = HostName;
            current.Port = Port;
            ServiceResponse registrationResponse = ApplicationRegistryService.RegisterClient(current);
            if (!registrationResponse.Success)
            {
                throw new ClientRegistrationFailedException(registrationResponse);
            }
            return registrationResponse;
        }

        public ServiceResponse Connect()
        {
            Machine current = Machine.ClientOf(LocalCoreRegistryRepository, HostName, Port);
            List<ServiceResponse> responses = new List<ServiceResponse>();
            foreach(ProxyableService svc in ServiceClients)
            {
                responses.Add(svc.ConnectClient(current).CopyAs<ServiceResponse>());
            }
            return new ServiceResponse { Data = responses, Success = !responses.Where(r => !r.Success).Any() };
        }

        protected bool IsInitialized { get; set; }
        protected string HostName { get; }
        protected int Port { get; }
        protected internal CoreUserRegistryService UserRegistryService { get; set; }
        protected internal CoreApplicationRegistryService ApplicationRegistryService { get; set; }
        protected internal CoreConfigurationService ConfigurationService { get; set; }
        protected internal CoreLoggerService LoggerService { get; set; }
        protected internal CoreTranslationService TranslationService { get; set; }
        protected internal CoreDiagnosticService DiagnosticService { get; set; }
        protected IEnumerable<ProxyableService> ServiceClients
        {
            get
            {
                yield return UserRegistryService;
                yield return ApplicationRegistryService;                
                yield return ConfigurationService;
                //yield return EventHubService;
                yield return LoggerService;
                yield return TranslationService;                
                yield return DiagnosticService;
            }
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
            TranslationService.Property(propertyName, this);
            UserRegistryService.Property(propertyName, this);
        }
    }
}
