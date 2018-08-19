using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    /// <summary>
    /// A ProxyableService that is also a client of a core services server.
    /// </summary>
    /// <seealso cref="Bam.Net.CoreServices.ProxyableService" />
    /// <seealso cref="Bam.Net.Services.Clients.IHasCoreClient" />
    [Encrypt]
    public abstract class CoreProxyableService: ProxyableService, IHasCoreClient
    {
        IUserManager _userManager;
        public CoreProxyableService()
        {
            CoreClient = new CoreClient();
            ServiceRegistry registry = CoreServiceRegistryContainer.Instance;
            UserManager userMgr = registry.Get<UserManager>();
            DefaultDataSettingsProvider.Current.Init(userMgr);
            _userManager = userMgr;
        }

        public override IUserManager UserManager
        {
            get { return _userManager; }
            set
            {
                // make this read only to avoid accidental overwrite 
            }
        }

        public CoreClient CoreClient { get; set; }
    }
}
