using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Unit
{
    public class UnitTestMethodProvider: TestMethodProvider<UnitTestMethod>
    {
        public UnitTestMethodProvider() { }

        public override List<UnitTestMethod> GetTests()
        {
            return UnitTestMethod.FromAssembly(Assembly);
        }
    }
}
