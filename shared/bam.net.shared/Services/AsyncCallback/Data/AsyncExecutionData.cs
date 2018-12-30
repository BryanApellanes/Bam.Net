using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.AsyncCallback.Data
{
    public class AsyncExecutionData: RepoData
    {
        public string RequestCuid { get; set; }
        public string RequestHash { get; set; }

        public string ResponseCuid { get; set; }
        public string ResponseHash { get; set; }

        public bool Success { get; set; }

        public DateTime? Requested { get; set; }
        public DateTime? Responded { get; set; }
    }
}
