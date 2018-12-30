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
	public class ScenarioContext
	{
        public ScenarioContext()
        {
            Scenarios = new Queue<ScenarioSetupContext>();
        }
        public Queue<ScenarioSetupContext> Scenarios { get; set; }
        public ScenarioSetupContext CurrentScenario { get; set; }

        public ScenarioSetupContext AddScenario(string scenarioDescription, Action scenarioSetup)
        {
            ScenarioSetupContext scenario = new ScenarioSetupContext(scenarioDescription, scenarioSetup);
            Scenarios.Enqueue(scenario);
            CurrentScenario = scenario;
            return scenario;
        }
	}
}
