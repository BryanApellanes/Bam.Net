using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Tests
{
    public class DataReplicationTestClass: CompositeKeyAuditRepoData
    {
        [CompositeKey]
        public string FirstName { get; set; }
        [CompositeKey]
        public string LastName { get; set; }
        
        public string Address { get; set; }
    }
}
