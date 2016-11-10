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
using Bam.Net.CoreServices.Data.Daos.Repository;
using Bam.Net.UserAccounts;
using Bam.Net.Caching;
using System.Collections.Specialized;
using System.Net;
using Bam.Net.Server;

namespace Bam.Net.CoreServices
{
    [Proxy("appRegistrySvc")]
    [Encrypt]
    public class CoreApplicationRegistryService : ProxyableService, IApiKeyResolver, IApiKeyProvider, IApplicationNameProvider
    {
        public const string AppNameNotSpecified = "X-APPNAME-HEADER-NOT-SPECIFIED";
        CacheManager _cacheManager;
        ApiKeyResolver _apiKeyResolver;

        protected CoreApplicationRegistryService() { }

        public CoreApplicationRegistryService(CoreApplicationRegistryServiceConfig config, AppConf conf)
        {
            ApplicationRegistryRepository = new ApplicationRegistryRepository();
            ApplicationRegistryRepository.WarningsAsErrors = false;
            config.DatabaseProvider.SetDatabases(this);
            CompositeRepository = new CompositeRepository(ApplicationRegistryRepository, config.WorkspacePath);
            _cacheManager = new CacheManager(100000000);
            _apiKeyResolver = new ApiKeyResolver(this, this);
            AppConf = conf;
            Config = config;
        }

        Database _database;
        public Database Database
        {
            get
            {
                if(ApplicationRegistryRepository != null && ApplicationRegistryRepository.Database != null)
                {
                    _database = ApplicationRegistryRepository.Database;
                }
                return _database;
            }
            set
            {
                if(ApplicationRegistryRepository != null)
                {
                    ApplicationRegistryRepository.Database = value;
                }
                _database = value;
            }
        }

        public ApplicationRegistryRepository ApplicationRegistryRepository { get; set; }

        public CompositeRepository CompositeRepository { get; set; }

        public MaximumLimitEnforcer<ServiceResponse> OrganizationLimitEnforcer { get; set; }

        public MaximumLimitEnforcer<ServiceResponse> ApplicationLimitEnforcer { get; set; }

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
            AddApiKey(ApplicationRegistryRepository, app, out key);
            return key.SharedSecret;
        }

        public virtual string GetApplicationName()
        {
            return ApplicationName.Or(AppNameNotSpecified);
        }

        [ApiKeyRequired]
        public virtual ApiKeyInfo GetApiKeyInfo()
        {
            return GetApiKeyInfo(this);
        }

        public virtual ServiceResponse RegisterApplication(string organization, string appName)
        {
            if (CurrentUser.Equals(UserAccounts.Data.User.Anonymous))
            {
                return new ServiceResponse<Data.Application> { Success = false, Message = "You must be logged in to do that", Data = new ApplicationRegistrationResult { Status = ApplicationRegistrationStatus.Unauthorized } };
            }
            User user = ApplicationRegistryRepository.OneUserWhere(c => c.UserName == CurrentUser.UserName);
            if (user == null)
            {
                user = new User();
                user.UserName = CurrentUser.UserName;
                user.Email = CurrentUser.Email;
                user = ApplicationRegistryRepository.Save(user);
            }
            OrganizationFactory orgEnforcer = new OrganizationFactory(ApplicationRegistryRepository, user, organization);
            ServiceResponse<Organization> response = orgEnforcer.Execute();
            if (!response.Success)
            {
                return response;
            }
            Organization org = response.TypedData();
            ApplicationFactory appEnforcer = new ApplicationFactory(ApplicationRegistryRepository, user, this, organization, appName);
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
            CoreApplicationRegistryService result = new CoreApplicationRegistryService(Config, AppConf);
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
            Data.Application app = ApplicationRegistryRepository.OneApplicationWhere(c => c.Cuid == applicationClientId);
            if(app != null)
            {
                return app.ApiKeys[index]?.SharedSecret;
            }
            return string.Empty;
        }

        [Exclude]
        public string GetApplicationClientId(IApplicationNameProvider nameProvider)
        {
            Data.Application app = ApplicationRegistryRepository.OneApplicationWhere(c => c.Name == nameProvider.GetApplicationName());
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

        protected internal static ApiKeyInfo GenerateApiKeyInfo(Data.Application app)
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
        protected internal static Data.Application AddApiKey(ApplicationRegistryRepository repo, Data.Application app)
        {
            Data.ApiKey ignore;
            return AddApiKey(repo, app, out ignore);
        }
        protected internal static Data.Application AddApiKey(ApplicationRegistryRepository repo, Data.Application app, out Data.ApiKey key)
        {
            ApiKeyInfo keyInfo = GenerateApiKeyInfo(app);
            key = Data.ApiKey.FromKeyInfo(keyInfo);
            app.ApiKeys.Add(key);
            app = repo.Save(app);
            return app;
        }
    }
}
