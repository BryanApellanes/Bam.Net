/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Specification
{
	public class SpecificationContainer: CommandLineTestInterface
	{
		public SpecificationContainer()
        {
            CurrentFeatureContext = new FeatureContext();
            CurrentScenarioContext = new ScenarioContext();
        }

        internal FeatureContext CurrentFeatureContext { get; set; }
        internal ScenarioContext CurrentScenarioContext { get; set; }

        [AfterUnitTests]
        public void RunSpecificationTests()
        {
            FeatureAction feature = null;
            Scenario scenario = null;
            while(CurrentFeatureContext.Features.Count > 0)
            {
                feature = CurrentFeatureContext.Features.Dequeue();
                if (!feature.TryAction((f, x) => Logger.AddEntry("Feature ({0}) failed: {1}", x, f.Description, x.Message)))
                {
                    Logger.Error("Feature prep failed");
                    break;
                }
            }
            while(CurrentScenarioContext.Scenarios.Count > 0)
            {
                scenario = CurrentScenarioContext.Scenarios.Dequeue();
                if (!scenario.TryAction((s, x) => Logger.AddEntry("Scenario ({0}) failed: {1}\r\n{2}", x, s.Description, x.Message)))
                {
                    Logger.Error("Scenario prep failed");
                    break;
                }
            }
        }
        		
		public void Feature(string feature, Action featureAction)
		{
			CurrentFeatureContext.Features.Enqueue(new FeatureAction(feature, featureAction));
		}

		public void Scenario(string scenario, Action scenarioAction)
		{
			CurrentScenarioContext.Scenarios.Enqueue(new Scenario(scenario, scenarioAction));
		}
			
		public StepContext Given(string given, Action givenAction)
		{
            return new StepContext(given, givenAction);
		}
	}
}
