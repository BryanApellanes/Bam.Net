using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// When implemented by a derived class provides types
    /// to a repository indicating that those types should
    /// be supported for persistence operations
    /// </summary>
    public abstract class RepositoryTypesProvider : IStorableTypesProvider
    {
        public void AddTypes(IRepository repository)
        {
            repository.AddTypes(GetTypes());
        }

        public abstract HashSet<Type> GetTypes();
    }
}
