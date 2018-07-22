using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.Testing.Unit
{
    [Serializable]
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

        public string Description { get { return Information; } }

        public static List<UnitTestMethod> FromAssembly(Assembly assembly)
        {
            List<UnitTestMethod> tests = new List<UnitTestMethod>();
            tests.AddRange(FromAssembly<UnitTestMethod>(assembly, typeof(UnitTestAttribute)));
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }
    }
}
