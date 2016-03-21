/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    public class PasswordResetEmailData
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public string PasswordResetUrl { get; set; }
    }
}
