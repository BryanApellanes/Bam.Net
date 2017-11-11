using Bam.Net.ServiceProxy;
using Bam.Net.Automation.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.Testing
{
    public class TestReportResponse: ServiceResponse
    {
        public string Uuid { get; set; }
        public CreateStatus CreateStatus { get; set; }
    }
}
