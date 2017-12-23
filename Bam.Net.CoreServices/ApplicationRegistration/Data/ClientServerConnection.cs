using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class ClientServerConnection: AuditRepoData
    {
        public long ClientId { get; set; }
        public virtual ProcessDescriptor Client { get; set; }
        public long ServerId { get; set; }
        public virtual ProcessDescriptor Server { get; set; }
    }
}
