using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    public class SignUpOptions
    {
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool SendConfirmation { get; set; }
    }
}
