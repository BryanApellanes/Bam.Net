using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class ApplicationRegistrationResult
    {
        public ApplicationRegistrationStatus Status { get; set; }
        public string ClientId { get; set; }
        /// <summary>
        /// The shared secret.  Keep this value private
        /// </summary>
        public string ApiKey { get; set; }
    }
}
