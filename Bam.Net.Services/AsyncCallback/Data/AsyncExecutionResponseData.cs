using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.AsyncCallback.Data
{
    public class AsyncExecutionResponseData: RepoData
    {
        public ulong RequestId { get; set; }
        /// <summary>
        /// The request that this AysncExecutionResponse
        /// is in response to
        /// </summary>
        public virtual AsyncExecutionRequestData Request { get; set; }
        public string ResponseHash { get; set; }
        public string RequestHash { get; set; }
        public string ResultJson { get; set; }
    }
}
