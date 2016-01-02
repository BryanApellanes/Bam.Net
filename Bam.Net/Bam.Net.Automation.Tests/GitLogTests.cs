using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Automation;
using Bam.Net.Automation.SourceControl;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class GitLogTests: CommandLineTestInterface
    {
        [UnitTest("Git: Get logs")]
        public void ShouldPopulateGitLogInstances()
        {
            List<GitLog> logs = GitLog.GetLogs("c:\\src\\Bam.Net\\Bam.Net", 1);
            logs.Each(log =>
            {
                OutLine(log.PropertiesToString());
            });
        }
    }
}
