/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rest
{
    /// <summary>
    /// Encapsulates a response to a rest request
    /// </summary>
	public class RestResponse
	{
		public RestResponse()
		{
		}
        /// <summary>
        /// True if the request succeeded
        /// </summary>
		public bool Success { get; set; }
        /// <summary>
        /// The failure message or null on success
        /// </summary>
		public string Message { get; set; }

        /// <summary>
        /// The result of the request
        /// </summary>
		public object Data { get; set; }
	}
}
