using Bam.Net.Services;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;

namespace Bam.Net.Tests
{
    [Serializable]
    public class AppModuleAttributeTests : CommandLineTestInterface
    {
        [Singleton]
        class TestClass { }
        [UnitTest]
        public void ClassWithSingletonAttributeTest()
        {
            Expect.IsTrue(typeof(TestClass).HasCustomAttributeOfType<AppModuleAttribute>(), "didn't have AppModuleAttribute");
        }
    }
}
