using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public interface IRepositoryResolver
    {
        bool TryGetRepository<T>(IHttpContext context, out T repository) where T : IRepository;
        IRepository GetRepository(IHttpContext context);
        T GetRepository<T>(IHttpContext context) where T : IRepository;
    }
}
