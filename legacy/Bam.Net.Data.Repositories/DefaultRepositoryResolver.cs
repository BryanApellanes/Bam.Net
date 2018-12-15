using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    public class DefaultRepositoryResolver : RepositoryResolver
    {
        public DefaultRepositoryResolver(IRepository repository)
        {
            Repository = repository;
        }
        public IRepository Repository { get; set; }
        public override IRepository GetRepository(IHttpContext context)
        {
            return Repository;
        }
    }
}
