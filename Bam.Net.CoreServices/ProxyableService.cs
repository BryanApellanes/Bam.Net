using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.CoreServices
{
    [Encrypt]
    public abstract class ProxyableService: Loggable, IRequiresHttpContext
    {
        public ProxyableService() { }
        public ProxyableService(DaoRepository repository, AppConf appConf)
        {
            AppConf = appConf;
            DaoRepository = repository;
            Logger = appConf?.Logger ?? Log.Default;
        }

        public ProxyableService(IRepository genericRepo, DaoRepository daoRepo, AppConf appConf) : this(daoRepo, appConf)
        {
            Repository = genericRepo;
        }

        public IDatabaseProvider DatabaseProvider { get; set; }

        public string UserName
        {
            get
            {
                return CurrentUser.UserName;
            }
        }
        
        public virtual LoginResponse Login(string userName, string passHash)
        {
            IUserManager mgr = (IUserManager)UserManager.Clone();
            mgr.HttpContext = HttpContext;
            return mgr.Login(userName, passHash);
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
        public string ApplicationName
        {
            get
            {
                return HttpContext?.Request?.Headers[ServiceProxyClient.ApplicationNameHeader]
                        .Or($"{ServiceProxyClient.ApplicationNameHeader}-UNKNOWN");
            }
        }


        [Exclude]
        public string ClientIp
        {
            get
            {
                return HttpContext?.Request?.Headers["HTTP_X_FORWARDED_FOR"]
                        .Or(HttpContext?.Request?.Headers["REMOTE_ADDR"]);
            }
        }

        [Exclude]
        public Session Session
        {
            get
            {
                return Session.Init(HttpContext);
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
        public User CurrentUser
        {
            get
            {                
                return UserManager.GetUser(HttpContext);
            }
        }

        IUserManager _userManager;
        [Exclude]
        public IUserManager UserManager
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

        [Exclude]
        public IUserResolver UserResolver { get; set; }

        [Exclude]
        public IRoleResolver RoleResolver { get; set; }

        [Exclude]
        public DaoRepository DaoRepository { get; set; }

        [Exclude]
        public IRepository Repository { get; set; }

        [Exclude]
        public ObjectRepository ObjectRepository { get; set; }

        [Exclude]
        public AppConf AppConf { get; set; }

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
            ITemplateRenderer renderer = server.GetAppTemplateRenderer(AppConf.Name);
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

        protected internal void SetHttpContext()
        {
            GetType().GetProperties().Where(pi => pi.PropertyType.ImplementsInterface<IRequiresHttpContext>()).Each(pi =>
            {
                pi.GetValue(this)?.Property("HttpContext", HttpContext);
            });
        }

        protected internal UserManager GetUserManager()
        {
            if (_userManager == null)
            {
                _userManager = AppConf.UserManagerConfig.Create(AppConf.Logger);
                _userManager.ApplicationNameProvider = new BamApplicationNameProvider(AppConf);
            }
            UserManager copy = (UserManager)_userManager.Clone();
            copy.HttpContext = HttpContext;
            return copy;
        }
    }
}
