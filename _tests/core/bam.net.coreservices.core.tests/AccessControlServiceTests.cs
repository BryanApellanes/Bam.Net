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
    public class AccessControlServiceTests : SpecificationContainer
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
            AccessControlService svc = new AccessControlService();
            ulong testResourceId = 555;
            PermissionSpecification permSpec = new PermissionSpecification();
            Console.WriteLine("spec test");
            Feature("Serve coffee in order to earn money " +
            "Customers should be able to buy coffee at all times", () =>
            {
                Scenario("Buy last coffee", () =>
                {
                    Given("there are 1 coffees left in the machine", () =>
                    {
                        Console.WriteLine("given --");
                    })
                    .And("I have deposited 1 dollar", () =>
                    {
                        Console.WriteLine("and --");
                    })
                    .When("I press the coffee button", () =>
                    {
                        Console.WriteLine("when -- ");
                    })
                    .Then("I should be served a coffee", (it) =>
                    {
                        it(svc)
                            .Should("add resource permission", () => svc.AddResourcePermission(testResourceId, permSpec))
                            .WithoutThrowing();

                        it(svc).IsEqualTo(svc);
                        it(true).IsTrue();

                        Console.WriteLine("then");
                    })
                    .But("I should not be sad", (it) =>
                    {
                        Console.WriteLine("but");
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
