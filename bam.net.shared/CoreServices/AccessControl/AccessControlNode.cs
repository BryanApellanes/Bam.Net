using Bam.Net;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bam.Net.CoreServices.AccessControl
{
    public class AccessControlNode: AuditRepoData
    {
        public string Name { get; set; }
        public AccessControlNode Parent { get; set; }
        public HashSet<AccessControlNode> Children { get; set; }

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
            AccessControlNode parent = Parent;
            while(parent != null)
            {
                path = Path.Combine(parent.Name, path);
                parent = parent.Parent;
            }
            return path;
        }
    }
}
