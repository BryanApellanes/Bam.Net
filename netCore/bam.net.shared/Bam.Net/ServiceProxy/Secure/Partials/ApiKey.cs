/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.ServiceProxy.Secure
{
	public partial class ApiKey
	{
        public static ApiKey Blank
        {
            get
            {
                return new ApiKey { SharedSecret = string.Empty };
            }
        }
		  // Your code here
	}
}																								
