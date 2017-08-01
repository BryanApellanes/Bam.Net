using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Testing.Unit
{
    public class UnitTestRunner : TestRunner<UnitTestMethod>
    {
        public UnitTestRunner(Assembly assembly, ILogger logger = null) : base(assembly, new UnitTestMethodProvider { Assembly = assembly }, logger)
        {
            SetupMethodProvider = new UnitTestSetupMethodProvider();
            TeardownMethodProvider = new UnitTestTeardownMethodProvider();
        }
    }
}
