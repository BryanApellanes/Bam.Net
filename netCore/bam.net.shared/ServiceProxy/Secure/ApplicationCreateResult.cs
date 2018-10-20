/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ApplicationCreateResult
    {
        public ApplicationCreateStatus Status
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public Application Application
        {
            get;
            set;
        }
    }
}
