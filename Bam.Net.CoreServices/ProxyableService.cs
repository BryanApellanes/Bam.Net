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
using Bam.Net.UserAccounts;

namespace Bam.Net.CoreServices
{
    public abstract class ProxyableService: Loggable, IRequiresHttpContext
    {
        UserManager _userManager;

        public ProxyableService(DaoRepository repository, AppConf appConf)
        {
            AppConf = appConf;
            Repository = repository;
            Logger = appConf.Logger ?? Log.Default;
        }

        [Exclude]
        public ILogger Logger { get; set; }

        [Exclude]
        public IHttpContext HttpContext
        {
            get;
            set;
        }
        
        [Exclude]
        public DaoRepository Repository { get; internal set; }

        [Exclude]
        public AppConf AppConf { get; protected set; }

        public void Render(string templateName, object toRender, Stream output)
        {
            Args.ThrowIfNull(AppConf, "AppConf");
            Args.ThrowIfNull(AppConf.BamConf, "AppConf.BamConf");
            Args.ThrowIfNull(AppConf.BamConf.Server, "AppConf.BamConf.Server");
            BamServer server = AppConf.BamConf.Server;
            AppDustRenderer renderer = server.GetAppDustRenderer(AppConf.Name);
            renderer.Render(templateName, toRender, output);
        }
        protected internal UserManager GetUserManager()
        {
            if (_userManager == null)
            {
                _userManager = AppConf.UserManager.Create(AppConf.Logger);
            }
            UserManager copy = _userManager.Clone();
            copy.HttpContext = HttpContext;
            return copy;
        }
    }
}
