using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    // TODO: rename this to NamedRepoData
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
