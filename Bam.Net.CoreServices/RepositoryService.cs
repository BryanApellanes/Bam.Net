using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices
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
