using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    public interface ITestResultCollector
    {
        void TestPassed(object sender, EventArgs e);
        void TestFailed(object sender, EventArgs e);
    }
}
