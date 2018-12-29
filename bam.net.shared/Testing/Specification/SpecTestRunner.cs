using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Testing.Specification
{
    public class SpecTestRunner : TestRunner<SpecTestMethod>
    {
        Dictionary<Type, SpecTestContainer> _specContainers;
        public SpecTestRunner(Assembly assembly, ILogger logger = null) : base(assembly, new SpecTestMethodProvider { Assembly = assembly }, logger)
        {
            SetupMethodProvider = new SpecTestSetupMethodProvider();
            TeardownMethodProvider = new SpecTestTeardownMethodProvider();
            _specContainers = new Dictionary<Type, SpecTestContainer>();
        }

        public override void RunTest(TestMethod test)
        {
            MethodInfo testMethod = test.Method;
            Type containerType = testMethod.DeclaringType;
            SpecTestContainer specContainer = GetContainer(test);
            specContainer.Setup();
            specContainer.RunSpecTest(specContainer, (SpecTestMethod)test);
            TestSummary.PassedTests.Add(test);
            specContainer.TearDown();
        }

        private SpecTestContainer GetContainer(TestMethod test)
        {
            Type type = test.Method.DeclaringType;
            if (!_specContainers.ContainsKey(type))
            {
                _specContainers.Add(type, type.Construct<SpecTestContainer>());
            }
            return _specContainers[type];
        }
    }
}
