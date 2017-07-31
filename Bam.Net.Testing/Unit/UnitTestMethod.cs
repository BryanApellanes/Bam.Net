using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.Testing.Unit
{
    public class UnitTestMethod : TestMethod
    {
        public UnitTestMethod() :base()
        {
        }

        public UnitTestMethod(MethodInfo method) : base(method)
        {
        }

        public UnitTestMethod(MethodInfo method, Attribute actionInfo) : base(method, actionInfo)
        {
        }
    }
}
