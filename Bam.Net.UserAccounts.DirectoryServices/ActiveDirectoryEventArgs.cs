using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.DirectoryServices
{
    public class ActiveDirectoryEventArgs: EventArgs
    {
        public string UserName { get; set; }
        public string Server { get; set; }
    }
}
