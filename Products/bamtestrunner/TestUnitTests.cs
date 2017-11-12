using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bamtestrunner
{
    [Serializable]
    public class TestUnitTests: CommandLineTestInterface
    {
        [UnitTest]
        public void PassingTest()
        {
            Expect.IsTrue(true);
            Pass("Passing test should pass");
        }

        [UnitTest]
        public void FailingTest()
        {
            Expect.IsTrue(false);            
        }
    }
}
