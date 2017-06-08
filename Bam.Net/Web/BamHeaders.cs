using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Web
{
    public static class BamHeaders
    {
        public static string ApplicationName { get { return "X-Bam-AppName"; } }
        public static string SecureSession { get { return "X-Bam-Sps-Session"; } }
        public static string ValidationToken { get { return "X-Bam-Validation-Token"; } }
        public static string Nonce { get { return "X-Bam-Timestamp"; } }
        public static string Padding { get { return "X-Bam-Padding"; } }
    }
}
