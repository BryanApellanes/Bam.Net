using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices.AccessControl
{
    public class PermissionSpecificationInfo
    {
        public string ResourceUuid { get; set; }
        public string UserIdentifier { get; set; }
        public string RoleIdentifier { get; set; }
        public Permissions Permission { get; set; }
    }
}
