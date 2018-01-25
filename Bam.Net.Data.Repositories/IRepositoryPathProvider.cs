using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public interface IRepositoryPathProvider
    {
        string GetRepositoryFileSystemPath(IRepository repo);
        string GetRepositoryFileSystemPath();
    }
}
