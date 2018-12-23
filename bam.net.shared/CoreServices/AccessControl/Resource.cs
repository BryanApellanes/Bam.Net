using Bam.Net;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Bam.Net.CoreServices.AccessControl
{
    public class Resource: AuditRepoData
    {
        public string Name { get; set; }
        public Resource Parent { get; set; }

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

        HashSet<Resource> _children;
        public virtual List<Resource> Children
        {
            get
            {
                return _children.ToList();
            }
            set
            {
                _children = new HashSet<Resource>(value);                
            }
        }

        object _pathLock = new object();
        string _path;
        public string FullPath
        {
            get
            {
                return _pathLock.DoubleCheckLock(ref _path, () => GetPath());
            }
        }

        protected string GetPath()
        {
            string path = Name;
            Resource parent = Parent;
            while(parent != null)
            {
                path = Path.Combine(parent.Name, path);
                parent = parent.Parent;
            }
            return path;
        }
    }
}
