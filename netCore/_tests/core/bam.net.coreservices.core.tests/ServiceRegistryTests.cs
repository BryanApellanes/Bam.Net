using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using Bam.Net.UserAccounts;
using NSubstitute;

namespace Bam.Net.CoreServices.Tests
{
    using System.IO;
    using Bam.Net.CoreServices.ApplicationRegistration;
    using Bam.Net.Testing.Unit;
    using Net.Data.SQLite;
    using Server;
    using ServiceProxySecure = ServiceProxy.Secure;

    public interface TestInterface
    {

    }

    public class TestConcreteClass: TestInterface
    {
    }

    public class TestConcreteClass2: TestInterface
    {
    }

    public interface ISecondTestInterface
    { }

    public class SecondConcrete : ISecondTestInterface
    { }


    public class ComplexClass
    {
        public ComplexClass(TestInterface ctorParam, SecondConcrete paramTwo)
        {
            TestInterface = ctorParam;
        }

        public TestInterface TestInterface { get; set; }
        public SecondConcrete PropTwo { get; set; }
    }

    public class SecondComplexClass
    {
        public SecondComplexClass(ComplexClass paramOne, SecondConcrete concrete2)
        {
            PropOne = paramOne;
            PropTwo = concrete2;
        }

        public ComplexClass PropOne { get; set; }
        public SecondConcrete PropTwo { get; set; }
    }

    [Serializable]
    public class ServiceRegistryTests : CommandLineTestInterface
    {
        [UnitTest]
        public void ServiceTypeResolvesFromFunc()
        {
            ServiceRegistry registry = new ServiceRegistry()
                .For<TestInterface>().Use<TestConcreteClass2>()
                .For<SecondComplexClass>().Use((reg) => new SecondComplexClass(reg.Get<ComplexClass>(), reg.Get<SecondConcrete>()));

            SecondComplexClass instance = registry.Get<SecondComplexClass>();
            
            Expect.IsNotNull(instance);
            Expect.IsInstanceOfType<ComplexClass>(instance.PropOne);
            Expect.IsInstanceOfType<SecondConcrete>(instance.PropTwo);
        }

        [UnitTest]
        public void ServiceTypeResolves()
        {
            ServiceRegistry registry = new ServiceRegistry();
            registry.For<TestInterface>().Use<TestConcreteClass2>();

            ComplexClass instance = registry.Get<ComplexClass>();
            Expect.IsNotNull(instance);
            Expect.IsInstanceOfType<TestConcreteClass2>(instance.TestInterface);
        }

        [UnitTest]
        public void ServiceTypeResolvesConveniently()
        {
            ServiceRegistry registry = For.Type<TestInterface>().Use<TestConcreteClass2>();

            ComplexClass instance = registry.Get<ComplexClass>();
            Expect.IsNotNull(instance);
            Expect.IsInstanceOfType<TestConcreteClass2>(instance.TestInterface);
        }
    }
}
