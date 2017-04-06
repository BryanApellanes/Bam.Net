using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class FsRepoData: AuditRepoData
    {
        public FsRepoData() : base()
        {
            Name = Cuid;
        }
        public FsRepoData(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
