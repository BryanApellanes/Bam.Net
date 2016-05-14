using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices
{
    public class EventMessage: RepoData
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Json { get; set; }
    }
}
