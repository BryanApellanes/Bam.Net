using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    [Serializable]
    public abstract class AuditRepoData: RepoData
    {
        public AuditRepoData() : base()
        {
            Modified = Created;
        }
        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }
    }
}
