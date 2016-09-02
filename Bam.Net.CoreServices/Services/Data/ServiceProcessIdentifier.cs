using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Data
{
    public class ServiceProcessIdentifierData: RepoData
    {
        public string MachineName { get; set; }
        public int ProcessId { get; set; }
        public string FilePath { get; set; }
        public string CommandLine { get; set; }
        public string IpAddresses { get; set; }
        public override string ToString()
        {
            return this.PropertiesToLine();
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if(!(obj is ServiceProcessIdentifierData))
            {
                return false;
            }
            return obj.ToString().Equals(this.ToString());
        }
    }
}
