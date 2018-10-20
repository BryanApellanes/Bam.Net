using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    public interface ISetupMethodProvider
    {
        List<ConsoleMethod> GetBeforeAllMethods(Assembly assembly);
        List<ConsoleMethod> GetBeforeEachMethods(Assembly assembly);
    }
}
