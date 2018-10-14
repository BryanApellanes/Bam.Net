using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;

namespace Bam.Net.Testing.Unit
{
    public class UnitTestSetupMethodProvider : ISetupMethodProvider
    {
        public List<ConsoleMethod> GetBeforeAllMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<BeforeUnitTests>(assembly);
        }

        public List<ConsoleMethod> GetBeforeEachMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<BeforeEachUnitTest>(assembly);
        }
    }
}
