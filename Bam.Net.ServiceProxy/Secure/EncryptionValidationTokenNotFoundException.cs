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
    public class EncryptionValidationTokenNotFoundException: Exception 
    {
        public EncryptionValidationTokenNotFoundException(string message)
            : base(message)
        { }
    }
}
