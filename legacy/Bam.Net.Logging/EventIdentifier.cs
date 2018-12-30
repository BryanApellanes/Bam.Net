using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Logging
{
    public class EventIdentifier: RepoData
    {
        public int EventId { get; set; }
        public string ApplicationName { get; set; }
        public string MessageSignature { get; set; }
    }
}
