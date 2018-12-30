using Bam.Net.Services.AsyncCallback.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    /// <summary>
    /// An extension to AsyncExecutionRequestData not intended to be
    /// persisted
    /// </summary>
    public class AsyncExecutionRequest: AsyncExecutionRequestData
    {
        public AsyncExecutionRequest()
        {
            ResponseMaxAgeInMinutes = 3;
            UseCachedResponse = true;
        }
        public int ResponseMaxAgeInMinutes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use a previously received
        /// response to the execution request.
        /// </summary>
        /// <value>
        ///   <c>true</c> to use a cached response; otherwise, <c>false</c>.
        /// </value>
        public bool UseCachedResponse { get; set; }
        public string RespondToHostName { get; set; }
        public int RespondToPort { get; set; }
        public bool Ssl { get; set; }
    }
}
