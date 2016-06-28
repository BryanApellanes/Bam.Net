using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts
{
    public class UserManagerEventArgs: EventArgs, IJsonable
    {
        public string UserJson { get; set; }
        public SignUpOptions SignUpOptions { get; set; }

        public string ToJson()
        {
            return Extensions.ToJson(this);
        }
    }
}
