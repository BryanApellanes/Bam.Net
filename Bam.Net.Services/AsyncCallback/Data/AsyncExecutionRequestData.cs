using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using NCuid;

namespace Bam.Net.Services.AsyncCallback.Data
{
    public class AsyncExecutionRequestData: RepoData
    {
        string _hash;
        public string RequestHash
        {
            get
            {
                if(_hash == null)
                {
                    _hash = GetRequestHash();
                }
                return _hash;
            }
            set
            {
                _hash = value;
            }
        }
        public string ClassName { get; set; }        
        public string MethodName { get; set; }        
        public string JsonParams { get; set; }

        public string GetRequestHash()
        {
            return $"{ClassName}\r\n{MethodName}\r\n{JsonParams}".Sha256();
        }
    }
}
