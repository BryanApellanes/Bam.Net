using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server;
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
