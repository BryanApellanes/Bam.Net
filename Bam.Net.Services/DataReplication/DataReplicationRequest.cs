using Bam.Net.Services.DataReplication.Data;
using Bam.Net.UserAccounts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class DataReplicationRequest
    {
        public string SessionId { get; set; }
        public string Hmac { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string Data { get; set; }
    }
}
