using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class TestInfo
    {
        public string RunOnHost { get; set; }
        public TestTypes Type { get; set; } 
        public string TestReportHost { get; set; }
        public int TestReportPort { get; set; } 
        public string Tag { get; set; }
        public string Search { get; set; }
    }
}
