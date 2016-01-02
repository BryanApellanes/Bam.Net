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
    public class ValidationToken
    {
        public string NonceCipher
        {
            get;
            set;
        }

        public string HashCipher
        {
            get;
            set;
        }
    }
}
