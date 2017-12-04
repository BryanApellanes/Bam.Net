using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    public abstract class RepositoryResolver : Loggable, IRepositoryResolver
    {
        public ILogger Logger { get; set; }
        public DataSettings DataSettings { get; set; }
        public Func<IHttpContext, IRepository> GetRepositoryFunc { get; set; }
        public T GetRepository<T>(IHttpContext context) where T : IRepository
        {
            return (T)GetRepository(context);
        }
        public abstract IRepository GetRepository(IHttpContext context);
    }
}
