using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices.AccessControl.Data
{
    [Serializable]
    public class ResourceHost: AuditRepoData
    {
        public string Name { get; set; }

        public virtual List<Resource> Resources { get; set; }
    }
}
