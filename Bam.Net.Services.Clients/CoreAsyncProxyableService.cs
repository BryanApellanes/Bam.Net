using Bam.Net.Data.Repositories;
using Bam.Net.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    public abstract class CoreAsyncProxyableService : AsyncProxyableService, IHasCoreClient
    {
        IUserManager _userManager;
        public CoreAsyncProxyableService() 
            : base(
                  new AsyncCallbackService(new AsyncCallback.Data.Dao.Repository.AsyncCallbackRepository(), Server.AppConf.FromDefaultConfig()), 
                  DefaultDataDirectoryProvider.Current.GetSysDaoRepository(), Server.AppConf.FromDefaultConfig())
        {
            CoreClient = new CoreClient();
            _userManager = CoreClient.UserRegistryService;
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
