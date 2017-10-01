using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.CoreServices.ApplicationRegistration.Dao.Repository;
using Bam.Net.UserAccounts;
using Bam.Net.Caching;
using System.Collections.Specialized;
using System.Net;
using Bam.Net.Server;
using Bam.Net.Web;
using Bam.Net.Configuration;

namespace Bam.Net.CoreServices
{
    [Proxy("appRegistrySvc")]
    [Encrypt]
    [ServiceSubdomain("appregistry")]
    [RoleRequired("/appreg/accessdenied", "Admin")]
    public class CoreApplicationRegistrationService : ApplicationProxyableService, IApiKeyResolver, IApiKeyProvider, IApplicationNameProvider
    {
        CacheManager _cacheManager;
        ApiKeyResolver _apiKeyResolver;

        protected CoreApplicationRegistrationService() { }

        public CoreApplicationRegistrationService(CoreApplicationRegistryServiceConfig config, AppConf conf, ApplicationRegistrationRepository coreRepo, ILogger logger)
        {
            ApplicationRegistrationRepository = coreRepo;
            ApplicationRegistrationRepository.WarningsAsErrors = false;
            config.DatabaseProvider.SetDatabases(this);
            CompositeRepository = new CompositeRepository(ApplicationRegistrationRepository, config.WorkspacePath);
            _cacheManager = new CacheManager(100000000);
            _apiKeyResolver = new ApiKeyResolver(this, this);
            AppConf = conf;
            Config = config;
            Logger = logger;
            HashAlgorithm = HashAlgorithms.SHA256;         
        }

        Database _database;
        public Database Database
        {
            get
            {
                if(ApplicationRegistrationRepository != null && ApplicationRegistrationRepository.Database != null)
                {
                    _database = ApplicationRegistrationRepository.Database;
                }
                return _database;
            }
            set
            {
                if(ApplicationRegistrationRepository != null)
                {
                    ApplicationRegistrationRepository.Database = value;
                }
                _database = value;
            }
        }
        
        public CompositeRepository CompositeRepository { get; set; }

        [ApiKeyRequired]
        public virtual ApiKeyInfo[] ListApiKeys()
        {
            return Application?.ApiKeys.Select(k => k.ToKeyInfo()).ToArray();
        }

        [ApiKeyRequired]
        public virtual ApiKeyInfo AddApiKey()
        {
            if (ApplicationName.Equals(ApplicationDiagnosticInfo.UnknownApplication))
            {
                throw new ApplicationNameNotSpecifiedException();
            }
            ApplicationRegistration.Application app = CompositeRepository.Query<ApplicationRegistration.Application>(a => a.Name.Equals(ApplicationName)).FirstOrDefault();
            if(app == null)
            {
                throw new InvalidOperationException("Application not registered");
            }
            AddApiKey(ApplicationRegistrationRepository, app, out ApplicationRegistration.ApiKey key);
            return new ApiKeyInfo { ApplicationClientId = key.ClientId, ApiKey = key.SharedSecret, ApplicationName = ApplicationName };
        }

        [ApiKeyRequired]
        public virtual ApiKeyInfo SetActiveApiKeyIndex(int index)
        {
            return SetActiveApiKeyIndex(this, index);
        }

        [Local]
        public virtual ApiKeyInfo SetActiveApiKeyIndex(IApplicationNameProvider nameProvider, int index)
        {
            string clientId = GetApplicationClientId(nameProvider);
            ActiveApiKeyIndex apiKeyIndex = ApplicationRegistrationRepository.OneActiveApiKeyIndexWhere(c => c.ApplicationCuid == clientId);
            if(apiKeyIndex == null)
            {
                apiKeyIndex = new ActiveApiKeyIndex { ApplicationCuid = clientId };
            }

            if (Application?.ApiKeys.Count - 1 > index || index < 0)
            {
                throw new IndexOutOfRangeException($"Specified ApiKeyIndex index is invalid: {index}");
            }
            apiKeyIndex.Value = index;
            ApplicationRegistrationRepository.Save(apiKeyIndex);
            return new ApiKeyInfo()
            {
                ApiKey = GetApplicationApiKey(clientId, index),
                ApplicationClientId = clientId
            };
        }

        [Local]
        public virtual int GetActiveApiKeyIndex(IApplicationNameProvider nameProvider)
        {
            string clientId = GetApplicationClientId(nameProvider);
            ActiveApiKeyIndex apiKeyIndex = ApplicationRegistrationRepository.OneActiveApiKeyIndexWhere(c => c.ApplicationCuid == clientId);
            if (apiKeyIndex != null)
            {
                return apiKeyIndex.Value;
            }
            return 0;
        }

        public virtual string GetApplicationName()
        {
            return ApplicationName.Or(ApplicationDiagnosticInfo.UnknownApplication);
        }
        
        public virtual ApiKeyInfo GetClientApiKeyInfo()
        {
            return GetApiKeyInfo(this);
        }

        /// <summary>
        /// Establishes the means by which the client will 
        /// communicate securely with the server.  Creates 
        /// a machine account for the client; used primarily 
        /// for .Net client assemblies using CoreClient
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public virtual CoreServiceResponse RegisterClient(Client client)
        {
            try
            {
                Args.ThrowIfNullOrEmpty(client?.Secret, nameof(client.Secret));
                Args.ThrowIfNullOrEmpty(client?.ServerHost, nameof(client.ServerHost));
                Args.ThrowIfNull(client?.Machine, nameof(client.Machine));
                Args.ThrowIf(client.Port <= 0, "Server Port not specified");
                IUserManager mgr = (IUserManager)UserManager.Clone();
                mgr.HttpContext = HttpContext;
                string clientName = client.ToString();
                CoreServiceResponse response = new CoreServiceResponse();
                CheckUserNameResponse checkUserName = mgr.IsUserNameAvailable(clientName);
                if (!(bool)checkUserName.Data) // already exists
                {
                    response.Success = true;
                    response.Message = "Already registered";
                }
                else
                {
                    SignUpResponse signupResponse = mgr.SignUp(client.GetPseudoEmail(), clientName, client.Secret.Sha1(), false);
                    if (!signupResponse.Success)
                    {
                        throw new Exception(response.Message);
                    }
                    Machine machine = ApplicationRegistrationRepository.GetOneMachineWhere(m => m.Name == client.MachineName);
                    client = ApplicationRegistrationRepository.GetOneClientWhere(c => c.MachineId == machine.Id && c.MachineName == client.MachineName && c.ApplicationName == client.ApplicationName && c.ServerHost == client.ServerHost && c.Port == client.Port);                    
                    response = new CoreServiceResponse { Success = true, Data = client.ToDynamicData().ToJson() };
                }
                return response;
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(CoreApplicationRegistrationService.RegisterClient));
            }
        }

        public virtual CoreServiceResponse RegisterApplication(string applicationName)
        {
            if (CurrentUser.Equals(UserAccounts.Data.User.Anonymous))
            {
                return new CoreServiceResponse<ApplicationRegistrationResult> { Success = false, Message = "You must be logged in to do that", Data = new ApplicationRegistrationResult { Status = ApplicationRegistrationStatus.Unauthorized } };
            }
            User user = GetApplicationRegistrationRepositoryUser();
            CoreServiceResponse<Organization> response = AssociateUserToOrganization(user, Organization.Public.Name);
            if (!response.Success)
            {
                return response;
            }
            ClientApplicationFactory appFactory = new ClientApplicationFactory(this, user);
            return GetApplicationRegistrationResponse(appFactory);
        }

        public virtual CoreServiceResponse RegisterApplicationProcess(ProcessDescriptor descriptor)
        {
            try
            {
                Args.ThrowIfNull(descriptor?.Application?.Name, "descriptor.Application.Name");
                Args.ThrowIfNull(descriptor?.Application?.Organization?.Name, "descriptor.Application.Organization.Name");

                if (CurrentUser.Equals(UserAccounts.Data.User.Anonymous))
                {
                    return new CoreServiceResponse<ApplicationRegistrationResult> { Success = false, Message = "You must be logged in to do that", Data = new ApplicationRegistrationResult { Status = ApplicationRegistrationStatus.Unauthorized } };
                }
                User user = GetApplicationRegistrationRepositoryUser();

                string organizationName = descriptor.Application.Organization.Name;
                CoreServiceResponse<Organization> response = AssociateUserToOrganization(user, organizationName);
                if (!response.Success)
                {
                    return response;
                }

                ClientApplicationFactory appFactory = new ClientApplicationFactory(this, user, organizationName, descriptor);
                return GetApplicationRegistrationResponse(appFactory);
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception occurred in {0}", ex, nameof(CoreApplicationRegistrationService.RegisterApplicationProcess));
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }

        [Exclude]
        public ApplicationRegistration.Application Application
        {
            get
            {
                return CompositeRepository.Query<ApplicationRegistration.Application>(a => a.Name.Equals(ApplicationName)).FirstOrDefault();
            }
        }

        [Exclude]
        public HashAlgorithms HashAlgorithm
        {
            get; set;
        }

        [Exclude]
        public override object Clone()
        {
            CoreApplicationRegistrationService result = new CoreApplicationRegistrationService(Config, AppConf, ApplicationRegistrationRepository, Logger);
            result.CopyProperties(this);
            return result;
        }

        [Exclude]
        public string CreateKeyToken(string stringToHash)
        {
            ApiKeyInfo apiKey = GetApiKeyInfo(this);
            return $"{apiKey.ApiKey}:{stringToHash}".Hash(HashAlgorithm);
        }

        [Exclude]
        public ApiKeyInfo GetApiKeyInfo(IApplicationNameProvider nameProvider)
        {
            string clientId = GetApplicationClientId(nameProvider);
            ApiKeyInfo info = new ApiKeyInfo()
            {
                ApiKey = GetApplicationApiKey(clientId, GetActiveApiKeyIndex(nameProvider)), 
                ApplicationClientId = clientId
            };
            return info;
        }

        [Local]
        public virtual string GetApplicationApiKey(string applicationClientId, int index)
        {
            ApplicationRegistration.Application app = ApplicationRegistrationRepository.OneApplicationWhere(c => c.Cuid == applicationClientId);
            if(app != null)
            {
                return app.ApiKeys[index]?.SharedSecret;
            }
            return string.Empty;
        }

        [Exclude]
        public string GetApplicationClientId(IApplicationNameProvider nameProvider)
        {
            ApplicationRegistration.Application app = ApplicationRegistrationRepository.OneApplicationWhere(c => c.Name == nameProvider.GetApplicationName());
            return app?.Cuid;
        }

        [Local]
        public virtual string GetCurrentApiKey()
        {
            return Application?.ApiKeys.FirstOrDefault()?.SharedSecret;
        }

        [Exclude]
        public bool IsValidRequest(ExecutionRequest request)
        {
            Args.ThrowIfNull(request, "request");

            string className = request.ClassName;
            string methodName = request.MethodName;
            string stringToHash = ApiParameters.GetStringToHash(className, methodName, request.JsonParams);

            string token = request.Context.Request.Headers[Headers.KeyToken];
            bool result = false;
            if (!string.IsNullOrEmpty(token))
            {
                result = IsValidKeyToken(stringToHash, token);
            }

            return result;
        }

        [Exclude]
        public bool IsValidKeyToken(string stringToHash, string token)
        {
            string checkToken = CreateKeyToken(stringToHash);
            return token.Equals(checkToken);
        }

        [Exclude]
        public void SetKeyToken(NameValueCollection headers, string stringToHash)
        {
            throw new InvalidOperationException($"It isn't appropriate for this service to be used for this purpose: {nameof(CoreApplicationRegistrationService)}.{nameof(CoreApplicationRegistrationService.SetKeyToken)}");
        }

        [Exclude]
        public void SetKeyToken(HttpWebRequest request, string stringToHash)
        {
            throw new InvalidOperationException($"It isn't appropriate for this service to be used for this purpose: {nameof(CoreApplicationRegistrationService)}.{nameof(CoreApplicationRegistrationService.SetKeyToken)}");
        }

        protected CoreApplicationRegistryServiceConfig Config { get; set; }

        protected internal ApiKeyInfo GenerateApiKeyInfo(ApplicationRegistration.Application app)
        {
            ApiKeyInfo info = new ApiKeyInfo();
            info.ApplicationNameProvider = new StaticApplicationNameProvider(app.Name);
            info.ApplicationClientId = app.Cuid;
            info.ApiKey = ServiceProxySystem.GenerateId();
            return info;
        }

        /// <summary>
        /// Adds an api key and saves the app to the specified repository
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        protected internal ApplicationRegistration.Application AddApiKey(ApplicationRegistrationRepository repo, ApplicationRegistration.Application app)
        {
            ApplicationRegistration.ApiKey ignore;
            return AddApiKey(repo, app, out ignore);
        }
        protected internal ApplicationRegistration.Application AddApiKey(ApplicationRegistrationRepository repo, ApplicationRegistration.Application app, out ApplicationRegistration.ApiKey key)
        {
            ApiKeyInfo keyInfo = GenerateApiKeyInfo(app);
            key = ApplicationRegistration.ApiKey.FromKeyInfo(keyInfo);
            key.Created = DateTime.UtcNow;
            key.CreatedBy = CurrentUser.UserName;
            app.ApiKeys.Add(key);
            app = repo.Save(app);
            return app;
        }

        private CoreServiceResponse<Organization> AssociateUserToOrganization(User user, string organizationName)
        {
            OrganizationFactory orgEnforcer = new OrganizationFactory(ApplicationRegistrationRepository, user, organizationName);
            CoreServiceResponse<Organization> response = orgEnforcer.Execute();
            return response;
        }

        private static CoreServiceResponse GetApplicationRegistrationResponse(ClientApplicationFactory appFactory)
        {
            CoreServiceResponse<ApplicationRegistration.Application> appResponse = appFactory.Execute();
            if (appResponse.Success)
            {
                return GetApplicationRegistrationSuccessResult(appResponse);
            }
            return appResponse;
        }

        private User GetApplicationRegistrationRepositoryUser()
        {
            User user = ApplicationRegistrationRepository.OneUserWhere(c => c.UserName == CurrentUser.UserName);
            if (user == null)
            {
                user = new User()
                {
                    UserName = CurrentUser.UserName,
                    Email = CurrentUser.Email
                };
                user = ApplicationRegistrationRepository.Save(user);
            }

            return user;
        }

        private static CoreServiceResponse GetApplicationRegistrationSuccessResult(CoreServiceResponse<ApplicationRegistration.Application> appResponse)
        {
            ApplicationRegistration.Application app = appResponse.TypedData();
            return new CoreServiceResponse<ApplicationRegistrationResult>(
                new ApplicationRegistrationResult
                {
                    Status = ApplicationRegistrationStatus.Success,
                    ClientId = app.Cuid,
                    ApiKey = app.ApiKeys.First().SharedSecret
                })
            { Success = true };
        }

        private CoreServiceResponse HandleException(Exception ex, string methodName)
        {
            Logger.AddEntry("Exception occurred in {0}", ex, methodName);
            return new CoreServiceResponse { Success = false, Message = ex.Message };
        }

    }
}
