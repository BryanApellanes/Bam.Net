using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Repositories
{
    public class DefaultRepoDataHydrator : RepoDataHydrator
    {
        public override void Hydrate(RepoData data, IRepository repository)
        {
            Log.Debug("Hydrate called on ({0}) for repo ({1}).", data?.ToString(), repository?.ToString());
            Hydrator?.Invoke(data, repository);
        }

        public Action<RepoData, IRepository> Hydrator { get; set; }
    }
}
