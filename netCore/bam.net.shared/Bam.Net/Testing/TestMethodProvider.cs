using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    public abstract class TestMethodProvider<TTestMethod> where TTestMethod: TestMethod
    {
        public Assembly Assembly { get; set; }

        public abstract List<TTestMethod> GetTests();
    }
}
