using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Data.Repositories
{
    public interface IRepositoryStorableTypesProvider
    {
        void AddTypes(IRepository repository);
        IEnumerable<Type> GetTypes();
    }
}
