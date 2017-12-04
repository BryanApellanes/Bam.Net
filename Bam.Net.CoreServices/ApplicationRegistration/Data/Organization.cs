using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class Organization: AuditRepoData
    {
        static Organization _public;
        static object _publicLock = new object();
        public static Organization Public
        {
            get
            {
                return _publicLock.DoubleCheckLock(ref _public, () => new Organization { Name = ApplicationDiagnosticInfo.PublicOrganization });
            }
        }

        public string Name { get; set; }
        public virtual Application[] Applications { get; set; }
        public virtual User[] Users { get; set; }
    }
}
