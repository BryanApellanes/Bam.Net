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
        [Exclude]
        public abstract object Clone();

        UserManager _userManager;
        public ProxyableService(DaoRepository repository, AppConf appConf)
        {
            AppConf = appConf;
            DaoRepository = repository;
            Logger = appConf.Logger ?? Log.Default;
        }

        public ProxyableService(IRepository genericRepo, DaoRepository daoRepo, AppConf appConf) : this(daoRepo, appConf)
        {
            Repository = genericRepo;
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
        public DaoRepository DaoRepository { get; internal set; }

        [Exclude]
        public IRepository Repository { get; set; }

        [Exclude]
        public AppConf AppConf { get; protected set; }

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

        protected internal UserManager GetUserManager()
        {
            if (_userManager == null)
            {
                _userManager = AppConf.UserManager.Create(AppConf.Logger);
                _userManager.ApplicationNameProvider = new BamApplicationNameProvider(AppConf);
            }
            UserManager copy = (UserManager)_userManager.Clone();
            copy.HttpContext = HttpContext;
            return copy;
        }
    }
}
