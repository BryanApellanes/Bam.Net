using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using System.IO;
using Bam.Net.Logging;
using System.Collections.Concurrent;

namespace Bam.Net.CoreServices
{ 
    public class ApplicationRepositoryResolver : RepositoryResolver
    {
        ConcurrentDictionary<string, IRepository> _repositoriesByAppName;
        public ApplicationRepositoryResolver(DefaultDataDirectoryProvider settings = null, ILogger logger = null)
        {
            DataSettings = settings ?? DefaultDataDirectoryProvider.Current;
            Logger = logger ?? Log.Default;
            _repositoriesByAppName = new ConcurrentDictionary<string, IRepository>();
            GetRepositoryFunc = GetDaoRepository;
        }

        public IApplicationNameResolver ApplicationNameResolver { get; set; }
        
        public override IRepository GetRepository(IHttpContext context)
        {
            return GetRepository(context, 3);
        }
        public IRepository GetRepository(IHttpContext context, int retryCount = 3)
        {
            string appName = ApplicationNameResolver.ResolveApplicationName(context);
            if(_repositoriesByAppName.TryGetValue(appName, out IRepository repo))
            {
                return repo;
            }
            else
            {
                if(!_repositoriesByAppName.TryAdd(appName, GetRepositoryFunc(context)))
                {
                    if(retryCount > 0)
                    {
                        return GetRepository(context, --retryCount);
                    }
                }
            }
            return GetRepositoryFunc(context);
        }

        protected internal IRepository GetDaoRepository(IHttpContext context)
        {
            IRequest request = context.Request;
            Uri url = new Uri(request.RawUrl);
            DirectoryInfo dbDirectory = new DirectoryInfo(Path.Combine(DataSettings.GetSysDatabaseDirectory().FullName, url.Host));
            if (!dbDirectory.Exists)
            {
                dbDirectory.Create();
            }
            SQLiteDatabase db = new SQLiteDatabase(dbDirectory.FullName, ApplicationNameResolver.ResolveApplicationName(context));
            return new DaoRepository(db, Logger);
        }
    }
}
