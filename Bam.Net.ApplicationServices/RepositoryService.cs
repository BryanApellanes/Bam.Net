using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.ApplicationServices
{
    [Encrypt]
    [Proxy("repo")]
    [ApiKeyRequired]
    public class RepositoryService
    {
        IRepository _repo;
        public RepositoryService(IRepository repo)
        {
            _repo = repo;
        }

        //public object Create()
    }
}
