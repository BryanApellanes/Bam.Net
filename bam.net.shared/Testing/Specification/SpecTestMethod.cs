using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.Testing.Specification
{
    [Serializable]
    public class SpecTestMethod : TestMethod
    {
        public SpecTestMethod() : base()
        {
        }

        public SpecTestMethod(MethodInfo method) : base(method)
        {
        }

        public SpecTestMethod(MethodInfo method, Attribute actionInfo) : base(method, actionInfo)
        {
        }

        public string Description { get { return Information; } }

        public static List<SpecTestMethod> FromAssembly(Assembly assembly)
        {
            List<SpecTestMethod> tests = new List<SpecTestMethod>();
            tests.AddRange(FromAssembly<SpecTestMethod>(assembly, typeof(SpecTestAttribute)));
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }
    }
}
