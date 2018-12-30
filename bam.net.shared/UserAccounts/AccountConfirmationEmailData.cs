/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.UserAccounts.Data;
using Bam.Net.ServiceProxy;

namespace Bam.Net.UserAccounts
{
    public class AccountConfirmationEmailData
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public string ConfirmationUrl { get; set; }

    }
}
