/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Bam.Net.Data.Schema
{
    public class SchemaResult
    {
        public SchemaResult(string message)
        {
            this.Message = message;
            this.Success = true;
        }

        public SchemaResult(string message, bool success)
        {
            this.Message = message;
            this.Success = success;
        }

        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }

        public bool Success { get; set; }
        public string Namespace { get; set; }
        public string SchemaName { get; set; }

        [Exclude]
        [JsonIgnore]
		public FileInfo DaoAssembly { get; set; }
    }
}
