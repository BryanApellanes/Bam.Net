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
        public const string DefaultNumberedMessageFormat = "Thread=#{0}({1})~~App={2}~~PID={3}~~Utc={4}::{5}~~{6}";

        public override string ToString()
        {
            return string.Format(DefaultNumberedMessageFormat,
                ThreadHashCode.ToString(),
                ThreadId.ToString(),
                ApplicationName,
                ProcessId,
                Utc.ToShortDateString(),
                Utc.ToShortTimeString(),
                Message);
        }
    }
}
