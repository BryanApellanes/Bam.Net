/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;

namespace Bam.Net.UserAccounts
{
    public abstract class RequestEmailResponse:RequestResponse
    {
        public bool EmailSent { get; set; }
    }
}
