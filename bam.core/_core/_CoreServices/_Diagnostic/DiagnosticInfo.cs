using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.CoreServices.Diagnostic;

namespace Bam.Net.CoreServices // core
{
    [Serializable]
    public partial class DiagnosticInfo
    {
        public DiagnosticInfo(ServiceRegistry registry = null)
        {
            ServiceRegistry = registry;

            SetAssemblies();

            SetDatabases();
            
            SetServiceProxies();

            SetDiagnosableSettings();

            AppDiagInfo = new ApplicationDiagnosticInfo();
        }

        static DiagnosticInfo _current;
        static object _currentLock = new object();
        public static DiagnosticInfo Current
        {
            get { return _currentLock.DoubleCheckLock(ref _current, () => new DiagnosticInfo()); }
        }
    }
}
