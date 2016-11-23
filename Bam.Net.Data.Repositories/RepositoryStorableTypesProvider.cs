using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public abstract class RepositoryStorableTypesProvider : IRepositoryStorableTypesProvider
    {
        public void AddTypes(IRepository repository)
        {
            repository.AddTypes(GetTypes());
        }

        public abstract IEnumerable<Type> GetTypes();
    }
}
