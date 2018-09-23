using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Web
{
    public static class CustomHeaders
    {
        public static string ProcessMode { get { return "X-Bam-Process-Mode"; } }

        public static string ApplicationName { get { return "X-Bam-AppName"; } }
        public static string SecureSession { get { return "X-Bam-Sps-Session"; } }
        public static string ValidationToken { get { return "X-Bam-Validation-Token"; } }
        public static string Signature { get { return "X-Bam-Signature"; } }
        public static string Nonce { get { return "X-Bam-Timestamp"; } }
        public static string Padding { get { return "X-Bam-Padding"; } }

        /// <summary>
        /// Header used to prove that the client knows the shared secret by using 
        /// it to create a hash value that this header is set to.
        /// </summary>
        public static string KeyToken { get { return "X-Bam-Key-Token"; } }

        /// <summary>
        /// Header used to request a specific responder on the server
        /// handle a given request.
        /// </summary>
        public static string Responder { get { return "X-Bam-Responder"; } }
    }
}
