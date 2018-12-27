/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Specification
{
	public abstract class SpecTestContainer: CommandLineTestInterface
	{
        Dictionary<string, object> _features;
        public SpecTestContainer()
        {
            FeatureContext = new FeatureContext();
            ScenarioContext = new ScenarioContext();
            SpecTestRegistry = new ServiceRegistry();
            _features = new Dictionary<string, object>();
            SpecTestResults = new SpecTestReporter();
        }

        public ServiceRegistry SpecTestRegistry { get; set; }
        internal FeatureContext FeatureContext { get; set; }
        internal ScenarioContext ScenarioContext { get; set; }
        internal SpecTestReporter SpecTestResults;

        FeatureContextSetup _currentFeatureSetupContext;
        ScenarioContextSetup _currentScenarioContextSetup;

        public virtual void Setup() { }
        public virtual void TearDown() { }

        public void RunSpecTest(SpecTestContainer container, SpecTestMethod testMethod)
        {
            testMethod.Provider = container;
            testMethod.Invoke();
            while(container.FeatureContext.Features.Count > 0)
            {
                _currentFeatureSetupContext = container.FeatureContext.Features.Dequeue();
                if (!_currentFeatureSetupContext.TrySetup((f, x) => Logger.AddEntry("Feature ({0}) failed: {1}", x, f.Description, x.Message)))
                {
                    Logger.Error("Feature prep failed");
                    break;
                }
                else
                {
                    while (ScenarioContext.Scenarios.Count > 0)
                    {
                        _currentScenarioContextSetup = ScenarioContext.Scenarios.Dequeue();
                        _currentScenarioContextSetup.FeatureContext = container.FeatureContext;
                        _currentScenarioContextSetup.CurrentFeature = _currentFeatureSetupContext;
                        _currentScenarioContextSetup.SpecTestContainer = this;
                        _currentScenarioContextSetup.Execute();
                    }
                }
            }
        }

        public void Feature<T>(string feature, Action<T> featureSetup)
        {
            FeatureContext.Features.Enqueue(new FeatureContextSetup<T>(feature, featureSetup));
        }

		public void Feature(string feature, Action featureAction)
		{
			FeatureContext.Features.Enqueue(new FeatureContextSetup(feature, featureAction));
		}

		public ScenarioContextSetup Scenario(string scenario, Action scenarioAction)
		{
            return ScenarioContext.AddScenario(scenario, scenarioAction);
		}
			
		public ScenarioContextSetup Given(string given, Action givenAction)
		{
            return ScenarioContext.CurrentScenario.Given(given, givenAction);
		}

        public SpecTestReporter GetReporter()
        {
            if(SpecTestRegistry.TryGet<SpecTestReporter>(out SpecTestReporter reporter))
            {
                return reporter;
            }
            return new SpecTestReporter();
        }
    }
}
