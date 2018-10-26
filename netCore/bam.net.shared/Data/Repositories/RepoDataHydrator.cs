using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Repositories
{
    public abstract class RepoDataHydrator : Hydrator, IRepoDataHydrator
    {
        static RepoDataHydrator()
        {
            DefaultRepoDataHydrator = new DefaultRepoDataHydrator();
        }

        public static RepoDataHydrator DefaultRepoDataHydrator { get; set; }

        public bool TryHydrate(RepoData data, IRepository repository)
        {
            try
            {
                Hydrate(data, repository);
                return true;
            } catch (Exception ex)
            {
                Logger.AddEntry("Exception hydrating RepoData of type ({0}): {1}", ex, data?.GetType()?.Name, ex.Message);
                return false;
            }
        }

        public abstract void Hydrate(RepoData data, IRepository repository);
    }
}
