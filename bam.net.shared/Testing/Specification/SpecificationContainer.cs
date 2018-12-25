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
	public class SpecificationContainer: CommandLineTestInterface
	{
        Dictionary<Type, SpecificationContainer> _specContainers;
        public SpecificationContainer()
        {
            FeatureContext = new FeatureContext();
            ScenarioContext = new ScenarioContext();
            SpecificationTestRegistry = new ServiceRegistry();
            _specContainers = new Dictionary<Type, SpecificationContainer>();
        }

        public ServiceRegistry SpecificationTestRegistry { get; set; }
        internal FeatureContext FeatureContext { get; set; }
        internal ScenarioContext ScenarioContext { get; set; }

        FeatureSetup _currentFeature;
        Scenario _currentScenario;

        public virtual void Setup() { }
        public virtual void TearDown() { }

        public void RunSpecTest(SpecificationContainer container, SpecTestMethod testMethod)
        {
            testMethod.Provider = container;
            testMethod.Invoke();
            while(container.FeatureContext.Features.Count > 0)
            {
                _currentFeature = container.FeatureContext.Features.Dequeue();
                if (!_currentFeature.TrySetup((f, x) => Logger.AddEntry("Feature ({0}) failed: {1}", x, f.Description, x.Message)))
                {
                    Logger.Error("Feature prep failed");
                    break;
                }
                else
                {
                    while (ScenarioContext.Scenarios.Count > 0)
                    {
                        _currentScenario = ScenarioContext.Scenarios.Dequeue();
                        _currentScenario.FeatureContext = container.FeatureContext;
                        _currentScenario.CurrentFeature = _currentFeature;
                        _currentScenario.Execute();
                    }
                }
            }
        }

		public void Feature(string feature, Action featureAction)
		{
			FeatureContext.Features.Enqueue(new FeatureSetup(feature, featureAction));
		}

		public Scenario Scenario(string scenario, Action scenarioAction)
		{
            return ScenarioContext.AddScenario(scenario, scenarioAction);
		}
			
		public Scenario Given(string given, Action givenAction)
		{
            return ScenarioContext.CurrentScenario.Given(given, givenAction);
		}
        
        private SpecificationContainer GetContainer(TestMethod test)
        {
            Type type = test.Method.DeclaringType;
            if (!_specContainers.ContainsKey(type))
            {
                _specContainers.Add(type, type.Construct<SpecificationContainer>());
            }
            return _specContainers[type];
        }
    }
}
