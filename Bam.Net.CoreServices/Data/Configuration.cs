using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Data
{
    public class Configuration: AuditRepoData
    {
        public virtual List<Machine> Machines { get; set; }
        public virtual List<Application> Applications { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            Configuration conf = obj as Configuration;
            if(conf == null)
            {
                return false;
            }

            return conf.Key.Equals(Key) && conf.Value.Equals(Value);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(Key, Value);
        }
    }
}
