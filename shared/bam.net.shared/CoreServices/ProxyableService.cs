using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.UserAccounts;
using Bam.Net.Web;
using U = Bam.Net.UserAccounts.Data;
using Bam.Net.CoreServices.Diagnostic;
using Bam.Net.Presentation;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// The base abstract class for any service that might be proxied.
    /// Provides common features for User, Roles, Session, Application
    /// and Data tracking.
    /// </summary>
    [Encrypt]
    public abstract partial class ProxyableService: Loggable, IRequiresHttpContext, IDiagnosable
    {
        protected ProxyableService()
        {
            AppConf = new AppConf();
            RepositoryResolver = new ApplicationRepositoryResolver();
            Logger = Log.Default;
            Repository = new DaoRepository();
            RepositoryResolver = new DefaultRepositoryResolver(Repository);
            DiagnosticName = GetType().Name;
        }

        public ProxyableService(ApplicationRepositoryResolver repoResolver, AppConf appConf)
        {
            AppConf = appConf;
            RepositoryResolver = repoResolver;
            Logger = appConf?.Logger ?? Log.Default;
            DiagnosticName = GetType().Name;
        }

        public ProxyableService(DaoRepository repository, AppConf appConf, IRepositoryResolver repositoryResolver = null)
        {
            AppConf = appConf;
            DaoRepository = repository;
            Repository = repository;
            Logger = appConf?.Logger ?? Log.Default;
            RepositoryResolver = repositoryResolver ?? new DefaultRepositoryResolver(repository);
            DiagnosticName = GetType().Name;
        }

        public ProxyableService(IRepository genericRepo, DaoRepository daoRepo, AppConf appConf) : this(daoRepo, appConf)
        {
            Repository = genericRepo;
            RepositoryResolver = new DefaultRepositoryResolver(genericRepo);
        }

        public IDatabaseProvider DatabaseProvider { get; set; }
        public IRepositoryResolver RepositoryResolver { get; set; }
        
        public string UserName
        {
            get
            {
                return CurrentUser.UserName;
            }
        }

        protected void IsLoggedInOrDie()
        {
            if (CurrentUser.Equals(U.User.Anonymous))
            {
                throw new InvalidOperationException("Current user not logged in");
            }
        }

        /// <summary>
        /// Determines whether the current user is in the specified role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        ///   <c>true</c> if the current user is in the specified role name; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsInRole(string roleName)
        {
            return RoleResolver.IsInRole(UserResolver, roleName);
        }

        [Exclude]
        public IUserResolver UserResolver { get; set; }

        [Exclude]
        public IRoleResolver RoleResolver { get; set; }

        /// <summary>
        /// Connect the specified client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public virtual LoginResponse ConnectClient(Client client)
        {
            return Login(client.ToString(), client.Secret.Sha1());
        }

        public virtual LoginResponse Login(string userName, string passHash)
        {
            IUserManager mgr = (IUserManager)UserManager.Clone();
            mgr.HttpContext = HttpContext;
            return mgr.Login(userName, passHash);
        }
        
        public virtual SignOutResponse EndSession()
        {
            IUserManager mgr = (IUserManager)UserManager.Clone();
            mgr.HttpContext = HttpContext;
            return mgr.SignOut();
        }

        public virtual string WhoAmI()
        {
            return UserName;
        }
        
        [Exclude]
        public abstract object Clone();
        
        [Exclude]
        public ILogger Logger { get; set; }

        IHttpContext _context;
        [Exclude]
        public IHttpContext HttpContext
        {
            get
            {
                return _context;
            }
            set
            {
                if(value != null)
                {
                    _context = value;
                    SetHttpContext();
                }
            }
        }

        [Exclude]
        public string HostName
        {
            get
            {
                return HttpContext?.Request?.Url?.Host;
            }
        }

        [Exclude]
        public virtual string ApplicationName
        {
            get
            {
                string fromHeader = HttpContext?.Request?.Headers[CustomHeaders.ApplicationName];
                return fromHeader.Or(ApplicationRegistration.Data.Application.Unknown.Name);
            }
        }

        [Exclude]
        public string ClientIpAddress
        {
            get
            {
                return HttpContext?.Request?.Headers["X-Forwarded-For"]
                        .Or(HttpContext?.Request?.Headers["Remote-Addr"])
                        .Or(HttpContext?.Request?.UserHostAddress);
            }
        }

        [Exclude]
        public U.Session Session
        {
            get
            {
                return U.Session.Init(HttpContext);
            }
        }

        [Exclude]
        public SecureSession SecureSession
        {
            get
            {
                return SecureSession.Init(HttpContext);
            }
        }

        [Exclude]
        public U.User CurrentUser
        {
            get
            {                
                return UserManager.GetUser(HttpContext);
            }
        }

        IUserManager _userManager;
        [Exclude]
        public virtual IUserManager UserManager
        {
            get
            {
                if(_userManager == null)
                {
                    _userManager = GetUserManager();
                    _userManager.HttpContext = HttpContext;
                }
                return _userManager;
            }
            set
            {
                _userManager = value;
            }
        }
        
        DaoRepository _daoRepository;
        [Exclude]
        public DaoRepository DaoRepository
        {
            get
            {
                return _daoRepository ?? RepositoryResolver?.GetRepository<DaoRepository>(HttpContext);
            }
            set
            {
                _daoRepository = value;
            }
        }

        IRepository _repository;
        [Exclude]
        public IRepository Repository
        {
            get
            {
                return _repository ?? RepositoryResolver?.GetRepository(HttpContext);
            }
            set
            {
                _repository = value;
            }
        }
        
        [Exclude]
        public AppConf AppConf { get; set; }

        [Exclude]
        public virtual Database Database { get; set; }

        [Exclude]
        public void RenderAppTemplate(string templateName, object toRender, Stream output)
        {
            RenderCommonTemplate($"{AppConf.Name}.{templateName}", toRender, output);
        }

        [Exclude]
        public void RenderAppTemplate(string templateName, object toRender, string filePath)
        {
            RenderCommonTemplate($"{AppConf.Name}.{templateName}", toRender, filePath);
        }

        [Exclude]
        public void RenderCommonTemplate(string templateName, object toRender, Stream output)
        {
            Args.ThrowIfNull(AppConf, "AppConf");
            Args.ThrowIfNull(AppConf.BamConf, "AppConf.BamConf");
            Args.ThrowIfNull(AppConf.BamConf.Server, "AppConf.BamConf.Server");
            BamServer server = AppConf.BamConf.Server;
            ITemplateManager renderer = server.GetAppTemplateRenderer(AppConf.Name);
            renderer.Render(templateName, toRender, output);
        }
        
        [Exclude]
        public void RenderCommonTemplate(string templateName, object toRender, string filePath)
        {
            MemoryStream ms = new MemoryStream();
            RenderCommonTemplate(templateName, toRender, ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            using (StreamReader sr = new StreamReader(ms))
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.Write(sr.ReadToEnd());
                }
            }
        }

        [Local]
        public Type GetProxiedType()
        {
            if (this is IProxy proxy)
            {
                return proxy.ProxiedType;
            }
            return GetType();
        }

        protected internal void SetHttpContext()
        {
            GetType().GetProperties().Where(pi => pi.PropertyType.ImplementsInterface<IRequiresHttpContext>()).Each(pi =>
            {
                object propertyValue = pi.GetValue(this);
                if(propertyValue != null)
                {
                    object propertyContext = propertyValue.Property("HttpContext");
                    if (propertyContext != HttpContext)
                    {
                        propertyValue.Property("HttpContext", HttpContext);
                    }
                }
            });
        }

        protected internal IUserManager GetUserManager()
        {
            if (_userManager == null)
            {
                _userManager = AppConf.UserManagerConfig.Create(AppConf.Logger);
                _userManager.ApplicationNameProvider = new BamApplicationNameProvider(AppConf);
            }
            IUserManager copy = (IUserManager)_userManager.Clone();
            copy.HttpContext = HttpContext;
            return copy;
        }

        public string DiagnosticName
        {
            get;set;
        }
    }
}
