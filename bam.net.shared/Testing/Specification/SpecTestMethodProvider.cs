using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Specification
{
    public class SpecTestMethodProvider : TestMethodProvider<SpecTestMethod>
    {
        public SpecTestMethodProvider() { }

        public override List<SpecTestMethod> GetTests()
        {
            return SpecTestMethod.FromAssembly(Assembly);
        }
    }
}
