using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.AssemblyManagement.Data
{
    public class AssemblyRevision: AuditRepoData
    {
        public string FileName { get; set; }
        public string FileHash { get; set; }
        /// <summary>
        /// The Sha1Int of the FileHash
        /// </summary>
        public int Number { get; set; }
    }
}
