using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Diagnostic
{
    public interface IDiagnosable
    {
        string DiagnosticName { get; set; }
        /// <summary>
        /// When implemented in a derived class, should return
        /// diagnostic key value pairs.  These values would typically
        /// be property names and their values but may also be
        /// any other values of importance for diagnosing the
        /// state of an application.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetSettings();
    }
}
