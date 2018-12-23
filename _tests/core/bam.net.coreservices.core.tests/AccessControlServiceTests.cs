using Bam.Net.Testing;
using Bam.Net.Testing.Specification;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Tests
{
    [Serializable]
    public class AccessControlServiceTests : SpecificationContainer
    {
        [UnitTest]
        public void GherkinLikeDelegatesTest()
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
            Feature("Serve coffee in order to earn money " +
                "Customers should be able to buy coffee at all times", () =>
                {
                    Scenario("Buy last coffee", () =>
                    {
                        Given("there are 1 coffees left in the machine", () =>
                        {

                        })
                        .And("I have deposited 1 dollar", () =>
                        {

                        })
                        .When("I press the coffee button", () =>
                        {

                        })
                        .Then("I should be served a coffee", () =>
                        {

                        })
                        .But("I should not be sad", () =>
                        {
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
