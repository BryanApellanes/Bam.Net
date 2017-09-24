using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public class DefaultRepositoryResolver : IRepositoryResolver
    {
        public DefaultRepositoryResolver(IRepository repository)
        {
            Repository = repository;
        }
        public IRepository Repository { get; set; }
        public IRepository GetRepository(IHttpContext context)
        {
            return Repository;
        }
        public T GetRepository<T>(IHttpContext context) where T: IRepository
        {
            return (T)Repository;
        }
    }
}
