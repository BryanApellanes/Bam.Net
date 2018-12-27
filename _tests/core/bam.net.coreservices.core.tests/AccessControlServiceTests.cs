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

        [SpecTest]
        public void SpecificationTest()
        {
            //Feature: Serve coffee
            //  In order to earn money
            //  Customers should be able to
            //  buy coffee at all times

            //  Scenario: Buy last coffee
            //	Given there are 1 coffees left in the machine
            //	And I have deposited 1 dollar
            //	When I press the coffee button
            //Then I should be served a coffee
            ulong testResourceId = 555;
            string testUser = "testUser";
            PermissionSpecification permSpec = new PermissionSpecification();
            Console.WriteLine("spec test");
            Feature<AccessControlService>("Set permissions to restrict access to resources", (svc) =>
            {
                Expect.IsInstanceOfType<AccessControlService>(svc);
                Scenario("Set deny permission on resource", () =>
                {
                    Given("I have been denied access to a resource", () =>
                    {
                        svc.Deny(testResourceId, testUser);
                        Console.WriteLine("body of given --");
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

        [UnitTest]
        public void CanGetResourceInfo()
        {
            throw new NotImplementedException();
        }

        [UnitTest]
        public void CanSetResourcePermissions()
        {
            throw new NotImplementedException();
        }

        [UnitTest]
        public void CanAddResourcePermission()
        {
            throw new NotImplementedException();
        }

        [UnitTest]
        public void CanRemoveResourcePermission()
        {
            throw new NotImplementedException();
        }
    }
}
