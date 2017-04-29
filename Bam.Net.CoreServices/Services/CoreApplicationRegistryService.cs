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
using Bam.Net.CoreServices.Data;
using Bam.Net.CoreServices.Data.Dao.Repository;
using Bam.Net.UserAccounts;
using Bam.Net.Caching;
using System.Collections.Specialized;
using System.Net;
using Bam.Net.Server;

namespace Bam.Net.CoreServices
{
    [Proxy("appRegistrySvc")]
    [Encrypt]
    public class CoreApplicationRegistryService : CoreProxyableService, IApiKeyResolver, IApiKeyProvider, IApplicationNameProvider
    {
        public const string AppNameNotSpecified = "X-APPNAME-HEADER-NOT-SPECIFIED";
        CacheManager _cacheManager;
        ApiKeyResolver _apiKeyResolver;

        protected CoreApplicationRegistryService() { }

        public CoreApplicationRegistryService(CoreApplicationRegistryServiceConfig config, AppConf conf, CoreRegistryRepository coreRepo, ILogger logger)
        {
            CoreRegistryRepository = coreRepo;
            CoreRegistryRepository.WarningsAsErrors = false;
            config.DatabaseProvider.SetDatabases(this);
            CompositeRepository = new CompositeRepository(CoreRegistryRepository, config.WorkspacePath);
            _cacheManager = new CacheManager(100000000);
            _apiKeyResolver = new ApiKeyResolver(this, this);
            AppConf = conf;
            Config = config;
            Logger = logger;
            HashAlgorithm = HashAlgorithms.SHA1;         
        }

        Database _database;
        public Database Database
        {
            get
            {
                if(CoreRegistryRepository != null && CoreRegistryRepository.Database != null)
                {
                    _database = CoreRegistryRepository.Database;
                }
                return _database;
            }
            set
            {
                if(CoreRegistryRepository != null)
                {
                    CoreRegistryRepository.Database = value;
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
        public virtual string AddApiKey()
        {
            if (ApplicationName.Equals(AppNameNotSpecified))
            {
                throw new InvalidOperationException(AppNameNotSpecified);
            }
            Data.Application app = CompositeRepository.Query<Data.Application>(a => a.Name.Equals(ApplicationName)).FirstOrDefault();
            if(app == null)
            {
                throw new InvalidOperationException("Application not registered");
            }
            Data.ApiKey key;
            AddApiKey(CoreRegistryRepository, app, out key);
            return key.SharedSecret;
        }

        public virtual string GetApplicationName()
        {
            return ApplicationName.Or(AppNameNotSpecified);
        }
        
        public virtual ApiKeyInfo GetClientApiKeyInfo()
        {
            return GetApiKeyInfo(this);
        }

        public virtual ServiceResponse RegisterClient(Machine machine)
        {
            try
            {
                Args.ThrowIfNullOrEmpty(machine?.Secret, nameof(machine.Secret));
                Args.ThrowIfNullOrEmpty(machine?.ServerHost, nameof(machine.ServerHost));
                Args.ThrowIf(machine.Port <= 0, "Server Port not specified");                
                IUserManager mgr = (IUserManager)UserManager.Clone();
                mgr.HttpContext = HttpContext;
                string machineName = machine.ToString();
                ServiceResponse response = new ServiceResponse();
                CheckUserNameResponse checkUserName = mgr.IsUserNameAvailable(machineName);
                if (!(bool)checkUserName.Data) // already exists
                {
                    response.Success = true;
                    response.Message = "Already registered";
                }
                else
                {
                    SignUpResponse signupResponse = mgr.SignUp($"{machine.Name}@{machine.Name}", machineName, machine.Secret.Sha1(), false);
                    if (!signupResponse.Success)
                    {
                        throw new Exception(response.Message);
                    }
                    machine = CoreRegistryRepository.GetOneMachineWhere(m => m.Name == machine.Name && m.ServerHost == machine.ServerHost && m.Port == machine.Port);                    
                    response = new ServiceResponse { Success = true, Data = machine.ToJson() };
                }
                return response;
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(CoreApplicationRegistryService.RegisterClient));
            }
        }

        public virtual ServiceResponse RegisterApplication(ProcessDescriptor descriptor)
        {
            try
            {
                Args.ThrowIfNull(descriptor?.Application?.Name, "descriptor.Application.Name");
                Args.ThrowIfNull(descriptor?.Application?.Organization?.Name, "descriptor.Application.Organization.Name");

                string organizationName = descriptor.Application.Organization.Name;
                if (CurrentUser.Equals(UserAccounts.Data.User.Anonymous))
                {
                    return new ServiceResponse<Data.Application> { Success = false, Message = "You must be logged in to do that", Data = new ApplicationRegistrationResult { Status = ApplicationRegistrationStatus.Unauthorized } };
                }
                User user = CoreRegistryRepository.OneUserWhere(c => c.UserName == CurrentUser.UserName);
                if (user == null)
                {
                    user = new User();
                    user.UserName = CurrentUser.UserName;
                    user.Email = CurrentUser.Email;
                    user = CoreRegistryRepository.Save(user);
                }
                OrganizationFactory orgEnforcer = new OrganizationFactory(CoreRegistryRepository, user, organizationName);
                ServiceResponse<Organization> response = orgEnforcer.Execute();
                if (!response.Success)
                {
                    return response;
                }
                Organization org = response.TypedData();
                ClientApplicationFactory appEnforcer = new ClientApplicationFactory(this, user, organizationName, descriptor);
                ServiceResponse<Data.Application> appResponse = appEnforcer.Execute();
                if (appResponse.Success)
                {
                    Data.Application app = appResponse.TypedData();
                    return new ServiceResponse<ApplicationRegistrationResult>(
                        new ApplicationRegistrationResult
                        {
                            Status = ApplicationRegistrationStatus.Success,
                            ClientId = app.Cuid,
                            ApiKey = app.ApiKeys.First().SharedSecret
                        })
                    { Success = true };
                }
                return appResponse;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception occurred in {0}", ex, nameof(CoreApplicationRegistryService.RegisterApplication));
                return new ServiceResponse { Success = false, Message = ex.Message };
            }
        }

        [Exclude]
        public Data.Application Application
        {
            get
            {
                return CompositeRepository.Query<Data.Application>(a => a.Name.Equals(ApplicationName)).FirstOrDefault();
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
            CoreApplicationRegistryService result = new CoreApplicationRegistryService(Config, AppConf, CoreRegistryRepository, Logger);
            result.CopyProperties(this);
            return result;
        }

        [Exclude]
        public string CreateToken(string stringToHash)
        {
            ApiKeyInfo apiKey = GetApiKeyInfo(this);
            return $"{apiKey.ApiKey}:{stringToHash}".Hash(HashAlgorithm);
        }

        [Exclude]
        public ApiKeyInfo GetApiKeyInfo(IApplicationNameProvider nameProvider)
        {
            string clientId = GetApplicationClientId(nameProvider);
            ApiKeyInfo info = new ApiKeyInfo();
            info.ApiKey = GetApplicationApiKey(clientId, 0);
            info.ApplicationClientId = clientId;
            return info;
        }

        [Exclude]
        public string GetApplicationApiKey(string applicationClientId, int index)
        {
            Data.Application app = CoreRegistryRepository.OneApplicationWhere(c => c.Cuid == applicationClientId);
            if(app != null)
            {
                return app.ApiKeys[index]?.SharedSecret;
            }
            return string.Empty;
        }

        [Exclude]
        public string GetApplicationClientId(IApplicationNameProvider nameProvider)
        {
            Data.Application app = CoreRegistryRepository.OneApplicationWhere(c => c.Name == nameProvider.GetApplicationName());
            return app?.Cuid;
        }

        [Exclude]
        public string GetCurrentApiKey()
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

            string token = request.Context.Request.Headers[ApiParameters.KeyTokenName];
            bool result = false;
            if (!string.IsNullOrEmpty(token))
            {
                result = IsValidToken(stringToHash, token);
            }

            return result;
        }

        [Exclude]
        public bool IsValidToken(string stringToHash, string token)
        {
            string checkToken = CreateToken(stringToHash);
            return token.Equals(checkToken);
        }

        [Exclude]
        public void SetToken(NameValueCollection headers, string stringToHash)
        {
            throw new NotImplementedException();// it isn't appropriate for this service to be used for this purpose
        }

        [Exclude]
        public void SetToken(HttpWebRequest request, string stringToHash)
        {
            throw new NotImplementedException();// it isn't appropriate for this service to be used for this purpose
        }

        protected CoreApplicationRegistryServiceConfig Config { get; set; }

        protected internal ApiKeyInfo GenerateApiKeyInfo(Data.Application app)
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
        protected internal Data.Application AddApiKey(CoreRegistryRepository repo, Data.Application app)
        {
            Data.ApiKey ignore;
            return AddApiKey(repo, app, out ignore);
        }
        protected internal Data.Application AddApiKey(CoreRegistryRepository repo, Data.Application app, out Data.ApiKey key)
        {
            ApiKeyInfo keyInfo = GenerateApiKeyInfo(app);
            key = Data.ApiKey.FromKeyInfo(keyInfo);
            key.Created = DateTime.UtcNow;
            key.CreatedBy = CurrentUser.UserName;
            app.ApiKeys.Add(key);
            app = repo.Save(app);
            return app;
        }

        private ServiceResponse HandleException(Exception ex, string methodName)
        {
            Logger.AddEntry("Exception occurred in {0}", ex, methodName);
            return new ServiceResponse { Success = false, Message = ex.Message };
        }

    }
}
