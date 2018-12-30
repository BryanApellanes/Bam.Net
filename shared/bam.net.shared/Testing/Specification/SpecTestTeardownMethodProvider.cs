using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;

namespace Bam.Net.Testing.Specification
{
    public class SpecTestTeardownMethodProvider : ITeardownMethodProvider
    {
        public List<ConsoleMethod> GetAfterAllMethods(Assembly assembly)
        {
            return new List<ConsoleMethod>();//ConsoleMethod.FromAssembly<AfterUnitTests>(assembly);
        }

        public List<ConsoleMethod> GetAfterEachMethods(Assembly assembly)
        {
            return new List<ConsoleMethod>();// ConsoleMethod.FromAssembly<AfterEachUnitTest>(assembly);
        }
    }
}
