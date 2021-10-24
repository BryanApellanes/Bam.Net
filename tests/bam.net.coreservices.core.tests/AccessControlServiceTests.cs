using Bam.Net.CommandLine;
using Bam.Net.CoreServices.AccessControl;
using Bam.Net.CoreServices.AccessControl.Data;
using Bam.Net.Testing;
using Bam.Net.Testing.Specification;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bam.Net.Tests
{
    [Serializable]
    public class AccessControlServiceTests : SpecTestContainer
    {
        [ConsoleAction]
        public void RunSpecificationTests()
        {
            ConsoleLogger logger = new ConsoleLogger();
            
            RunAllSpecTests(Assembly.GetEntryAssembly(), logger);
        }

        [UnitTest]
        public void ResourceUriTest()
        {
            ResourceUri uri = new ResourceUri("b://hostName/parent/child");
            Expect.AreEqual("b://", uri.Scheme);
            Expect.AreEqual("hostName", uri.Host);
            Expect.AreEqual("/parent/child", uri.Path);

            ResourceUri uri2 = new ResourceUri("b://hostName/parent/child?param1=baloney&monkey=true");
            Expect.AreEqual("param1=baloney&monkey=true", uri2.QueryString);
            Expect.AreEqual("baloney", uri2.QueryParams["param1"]);
            Expect.AreEqual("true", uri2.QueryParams["monkey"]);
        }

        [SpecTest]
        public void AccessControlSpecificationTest()
        {
            ulong testResourceId = 555;
            string testUser = "testUser";
            PermissionSpecification permSpec = new PermissionSpecification();
            SpecTestRegistry.For<SpecTestReporter>().Use<ConsoleSpecTestReporter>();
            Console.WriteLine("spec test");
            Feature<AccessControlService>("Set permissions to restrict access to resources", (svc) =>
            {
                svc.IsInstanceOfType<AccessControlService>();
                Scenario("Set deny permission on resource", () =>
                {
                    Given("I have been denied access to a resource", () =>
                    {
                        svc.Deny(testResourceId, testUser);
                    })
                    .And("I am logged in", () =>
                    {
                        // TODO: setup logged in user
                        Console.WriteLine("body of and --");
                    })
                    .When("I request the resource", () =>
                    {
                        // TODO: request resource
                        Console.WriteLine("body of when -- ");
                    })
                    .Then("I should be denied access", (it) =>
                    {
                        Expect.Fail("not done");
                        it(svc)
                            .Should("add resource permission", () => svc.AddResourcePermission(testResourceId, permSpec))
                            .WithoutThrowing();

                        it(svc).IsEqualTo(svc);
                        it(true).IsTrue();

                        Console.WriteLine("then");
                    });
                });
            });
        }

        [UnitTest(IgnoreBecause = "Not implemented")]
        public void CanGetResourceInfo()
        {
            throw new NotImplementedException();
        }

        [UnitTest(IgnoreBecause = "Not implemented")]
        public void CanSetResourcePermissions()
        {
            throw new NotImplementedException();
        }

        [UnitTest(IgnoreBecause = "Not implemented")]
        public void CanAddResourcePermission()
        {
            throw new NotImplementedException();
        }

        [UnitTest(IgnoreBecause = "Not implemented")]
        public void CanRemoveResourcePermission()
        {
            throw new NotImplementedException();
        }
    }
}
