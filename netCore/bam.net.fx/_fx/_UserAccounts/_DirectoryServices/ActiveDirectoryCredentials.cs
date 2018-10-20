using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.DirectoryServices
{
    public class ActiveDirectoryCredentials
    {
        public string Domain { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DomainControllerInfo DomainControllerInfo { get; set; }

        public override string ToString()
        {
            return $"{Domain}\\{UserName}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is ActiveDirectoryCredentials creds)
            {
                return creds.ToString().Equals(ToString());
            }
            return false;
        }
    }
}
