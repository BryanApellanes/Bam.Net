using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Repositories
{
    public interface IRepoDataHydrator
    {
        bool TryHydrate(RepoData data, IRepository repository);
        void Hydrate(RepoData data, IRepository repository);
    }
}
