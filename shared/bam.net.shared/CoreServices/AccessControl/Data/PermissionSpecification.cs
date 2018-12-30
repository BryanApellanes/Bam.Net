using Bam.Net.CoreServices.AccessControl.Data.Dao.Repository;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices.AccessControl.Data
{
    [Serializable]
    public class PermissionSpecification : KeyHashAuditRepoData
    {
        [CompositeKey]
        public ulong ResourceId { get; set; }
        public virtual Resource Resource { get; set; }

        [CompositeKey]
        public string RoleIdentifier { get; set; }

        [CompositeKey]
        public string UserIdentifier { get; set; }

        [CompositeKey]
        public Permissions Permission { get; set; }

        public PermissionSpecificationInfo ToInfo(AccessControlRepository repo)
        {
            if(Resource == null)
            {
                Resource = repo.Retrieve<Resource>(ResourceId);
            }
            return new PermissionSpecificationInfo
            {
                ResourceUuid = Resource.Uuid,
                UserIdentifier = UserIdentifier,
                Permission = Permission
            };
        }
    }
}
