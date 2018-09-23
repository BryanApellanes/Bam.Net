using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class Nic: RepoData
    {
        public ulong MachineId { get; set; }
        public virtual Machine Machine { get; set; }
        public string AddressFamily { get; set; }
        public string Address { get; set; }
        public string MacAddress { get; set; }
        public override int GetHashCode()
        {
            return $"{AddressFamily}:{Address}".GetHashCode();
        }
        public override bool Equals(object obj)
        {
            Nic input = obj as Nic;
            if(input != null)
            {
                return input.AddressFamily.Equals(AddressFamily) && input.Address.Equals(Address);
            }
            return false;
        }
    }
}
