using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices.AccessControl.Data
{
    [Serializable]
    public class PermissionSpecification : AuditRepoData
    {
        public ulong ResourceId { get; set; }
        public virtual Resource Resource { get; set; }

        public string UserIdentifier { get; set; }
        public Permissions Permission { get; set; }
    }
}
