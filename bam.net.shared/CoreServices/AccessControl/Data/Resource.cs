using Bam.Net;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Bam.Net.CoreServices.AccessControl.Data.Dao.Repository;

namespace Bam.Net.CoreServices.AccessControl.Data
{
    public class Resource: KeyHashAuditRepoData
    {
        [CompositeKey]
        public ulong ResourceHostId { get; set; }
        public virtual ResourceHost Host { get; set; }

        [CompositeKey]
        public ulong ParentId { get; set; }
        public virtual Resource Parent { get; set; }

        [CompositeKey]
        public string Name { get; set; }

        [CompositeKey]
        public string FullPath { get; set; }

        HashSet<PermissionSpecification> _permissions;
        public virtual List<PermissionSpecification> Permissions
        {
            get
            {
                return _permissions.ToList();
            }
            set
            {
                _permissions = new HashSet<PermissionSpecification>(value);
            }
        }

        /// <summary>
        /// Gets or sets a comma separated list of resource ids representing the children of this instance
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public string Children { get; set; }

        public IEnumerable<Resource> GetChildren(AccessControlRepository repo)
        {
            string[] resourceIds = Children.DelimitSplit(",");
            foreach(string resourceId in resourceIds)
            {
                yield return repo.Retrieve<Resource>(resourceId);
            }
        }

        public string GetPath(AccessControlRepository repo)
        {
            string path = Name;
            Resource parent = Parent;
            while(parent != null)
            {
                path = Path.Combine(parent.Name, path);
                parent = repo.Retrieve<Resource>(parent.ParentId);
            }
            return path;
        }

        public ResourceInfo ToInfo(AccessControlRepository repo)
        {
            return new ResourceInfo
            {
                Uri = FullPath,
                Path = GetPath(repo),
                Permissions = Permissions.Select(p => p.ToInfo(repo)).ToList()
            };
        }
    }
}
