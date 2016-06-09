using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts
{
    public class UserManagerEventArgs: EventArgs
    {
        public User User { get; set; }
    }
}
