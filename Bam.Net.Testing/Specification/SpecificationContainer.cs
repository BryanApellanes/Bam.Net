/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Specification
{
	public class SpecificationContainer: CommandLineTestInterface
	{
		static Feature _currentFeature;
		static Scenario _currentScenario;
		static Given _currentGiven;
		
		static SpecificationContainer()
		{
			FeatureContext = new FeatureContext();
			//AfterRunAllTests = () =>
			//{
			//	while (FeatureContext.Features.Count > 0)
			//	{
			//		_currentFeature = FeatureContext.Features.Dequeue();
			//		_currentFeature.Action();
			//		while (ScenarioContext.Scenarios.Count > 0)
			//		{
			//			_currentScenario = ScenarioContext.Scenarios.Dequeue();
			//			_currentScenario.Action();
			//		}
			//	}
			//};
		}
		internal static FeatureContext FeatureContext { get; set; }
		internal static ScenarioContext ScenarioContext { get; set; }

		public static void Feature(string feature, Action featureAction)
		{
			FeatureContext.Features.Enqueue(new Feature(feature, featureAction));
		}

		public static void Scenario(string scenario, Action scenarioAction)
		{
			ScenarioContext.Scenarios.Enqueue(new Scenario(scenario, scenarioAction));
		}
			
		public GivenContext Given(string given, Action givenAction)
		{
			return new GivenContext(given, givenAction);
		}

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

					});
				});
			});
				
		}

	}
}
