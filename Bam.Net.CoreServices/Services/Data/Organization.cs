using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Data
{
    public class Organization: RepoData
    {
        public string Name { get; set; }
        public virtual Application[] Applications { get; set; }
        public virtual User[] Users { get; set; }
    }
}
