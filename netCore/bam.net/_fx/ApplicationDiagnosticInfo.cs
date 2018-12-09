using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Bam.Net.Configuration;
using Bam.Net.Logging;

namespace Bam.Net
{
    public partial class ApplicationDiagnosticInfo
    {
        public override string ToString()
        {
            object names = new
            {
                ThreadHashCode = ThreadHashCode.ToString(),
                ThreadId = ThreadId.ToString(),
                ApplicationName,
                ProcessId = ProcessId.ToString(),
                UtcShortDate = Utc.ToShortDateString(),
                UtcShortTime = Utc.ToShortTimeString(),
                Message
            };
            return NamedMessageFormat.NamedFormat(names);
        }
    }
}
