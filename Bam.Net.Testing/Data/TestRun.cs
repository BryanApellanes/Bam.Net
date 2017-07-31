using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Data
{
    public class TestRun: AuditRepoData
    {
        public TestRun()
        {
            ComputerName = Environment.MachineName;
        }
        public string Build { get; set; }
        public string Branch { get; set; }
        public string ComputerName { get; set; }
        public virtual List<TestResult> TestResults { get; set; }
    }
}
