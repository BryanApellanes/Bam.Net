using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Caching;
using Bam.Net.Logging;
using Bam.Net.Services.Catalog.Data;

namespace Bam.Net.Services.Catalog
{
    public class CatalogRepository : CachingRepository
    {
        public CatalogRepository(IRepository sourceRepo, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            SetSourceRepository(sourceRepo);
            SetCacheManager();
        }
    }
}
