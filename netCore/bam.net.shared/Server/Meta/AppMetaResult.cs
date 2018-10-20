/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Meta
{
    public class AppMetaResult
    {
        public AppMetaResult(bool success, string message, object data)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }
        
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
