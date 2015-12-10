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
    public class ApiKeyNotFoundException: Exception
    {
        public ApiKeyNotFoundException(string clientId)
            : base("The key for the specified clientId ({0}) was not found"._Format(clientId))
        { }
    }
}
