/*
	Copyright © Bryan Apellanes 2015  
*/
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
    /// <summary>
    /// Diagnostic information about the current
    /// application process and thread
    /// </summary>
    [Serializable]
    public class ApplicationDiagnosticInfo
    {
        public const string DefaultMessageFormat = "Thread=#{ThreadHashCode}({ThreadId})~~App={ApplicationName}~~PID={ProcessId}~~Utc={UtcShortDate}::{UtcShortTime}~~{Message}";
        public const string UnknownApplication = "UNKNOWN_APPLICATION";
        public const string PublicOrganization = "PUBLIC_ORGANIZATION";

        public ApplicationDiagnosticInfo()
        {
            this.NamedMessageFormat = DefaultMessageFormat;
            this.Utc = DateTime.UtcNow;
            this.ThreadHashCode = Thread.CurrentThread.GetHashCode();
            this.ThreadId = Thread.CurrentThread.ManagedThreadId;
            this.ProcessId = Process.GetCurrentProcess().Id;
        }

        public ApplicationDiagnosticInfo(LogEvent logEvent)
            : this()
        {
            this.Message = logEvent.Message;
        }

        public override string ToString()
        {
            object names = new {
                ThreadHashCode = ThreadHashCode.ToString(),
                ThreadId = ThreadId.ToString(),
                ApplicationName = ApplicationName,
                ProcessId = ProcessId.ToString(),
                UtcShortDate = Utc.ToShortDateString(),
                UtcShortTime = Utc.ToShortTimeString(),
                Message = Message
            };
            return NamedMessageFormat.NamedFormat(names);
        }

        public string NamedMessageFormat
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public int ProcessId
        {
            get;
            set;
        }

        public DateTime Utc
        {
            get;
            set;
        }

        public int ThreadHashCode
        {
            get;
            set;
        }

        public int ThreadId
        {
            get;
            set;
        }
        
        string appName;
        public string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(appName) || appName.Equals(UnknownApplication))
                {
                    appName = ApplicationNameProvider.Default.GetApplicationName().Or(UnknownApplication);
                }
                return appName;
            }
            set
            {
                appName = value;
            }
        }
    }
}
