using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Data
{
    public class IpAddress: RepoData
    {
        public long MachineId { get; set; }
        public virtual Machine Machine { get; set; }
        public string AddressFamily { get; set; }
        public string Value { get; set; }

        public override int GetHashCode()
        {
            return $"{AddressFamily}:{Value}".GetHashCode();
        }
        public override bool Equals(object obj)
        {
            IpAddress input = obj as IpAddress;
            if(input != null)
            {
                return input.AddressFamily.Equals(AddressFamily) && input.Value.Equals(Value);
            }
            return false;
        }
    }
}
