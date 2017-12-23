using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class UserSetting: AuditRepoData
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
