using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class Configuration: AuditRepoData
    {
        public string Name { get; set; }
        public virtual ulong MachineId { get; set; }
        public virtual Machine Machine { get; set; } 
        public virtual ulong ApplicationId { get; set; }
        public virtual Application Application { get; set; }
        public virtual List<ConfigurationSetting> Settings { get; set; }
    }
}
