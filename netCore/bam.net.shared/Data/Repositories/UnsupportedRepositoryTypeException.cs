using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class UnsupportedRepositoryTypeException: Exception
    {
        public UnsupportedRepositoryTypeException(Type unsupportedRepoType) : base($"Query method taking parameter of type QueryFilter must delegate to DaoRepositories, designated SourceRepository is a {unsupportedRepoType.Name}")
        { }
    }
}
