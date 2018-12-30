using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.Testing
{
    [Serializable]
    public abstract class TestMethod : ConsoleMethod
    {
        public TestMethod() :base()
        {
        }

        public TestMethod(MethodInfo method) : base(method)
        {
        }

        public TestMethod(MethodInfo method, Attribute actionInfo) : base(method, actionInfo)
        {
        }
        public string Tag { get; set; }
    }
}
