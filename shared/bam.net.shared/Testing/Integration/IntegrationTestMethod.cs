using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Integration
{
    public class IntegrationTestMethod: TestMethod
    {
        public IntegrationTestMethod() : base()
        {
        }

        public IntegrationTestMethod(MethodInfo method) : base(method)
        {
        }

        public IntegrationTestMethod(MethodInfo method, Attribute actionInfo) : base(method, actionInfo)
        {
        }

        public string Description { get { return Information; } }

        public static List<IntegrationTestMethod> FromAssembly(Assembly assembly)
        {
            List<IntegrationTestMethod> tests = new List<IntegrationTestMethod>();
            tests.AddRange(FromAssembly<IntegrationTestMethod>(assembly, typeof(IntegrationTestAttribute)));
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }
    }
}
