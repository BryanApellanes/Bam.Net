using Bam.Net.CoreServices.Diagnostic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class DiagnosableSettings
    {
        public DiagnosableSettings(IDiagnosable diagnosable)
        {
            DiagnosticName = diagnosable.DiagnosticName;
            Settings = diagnosable.GetSettings();
        }

        public string DiagnosticName { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
