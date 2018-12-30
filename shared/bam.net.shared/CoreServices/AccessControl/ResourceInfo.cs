using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices.AccessControl
{
    public class ResourceInfo
    {
        public string Uri { get; set; }
        public string Path { get; set; }
        public List<PermissionSpecificationInfo> Permissions { get; set; }
    }
}
