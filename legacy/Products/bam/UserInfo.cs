using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    internal class UserInfo
    {
        public string Org { get; set; }
        public string Email { get; set; }
        public string App { get; set; }

        string _password;
        public string Password
        {
            get { return "*****"; }
            set { _password = value; }
        }

        internal string GetPassword()
        {
            return _password;
        }
    }
}
