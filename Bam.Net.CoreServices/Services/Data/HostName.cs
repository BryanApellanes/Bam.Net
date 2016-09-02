using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Data
{
    public class HostName: RepoData
    {
        public long ApplicationId { get; set; }
        public virtual Application Application { get; set; }
        public string Value { get; set; }
    }
}
